using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using SimPim.Api.Data;
using SimPim.Api.Models;
using SimPim.Api.Patterns.Creational;
using SimPim.Api.Patterns.Structural;
using SimPim.Api.Patterns.Behavioral;


// --- Builder ---
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IOrderFactory, StandardOrderFactory>();
builder.Services.AddScoped<INotificationAbstractFactory, EmailNotificationFactory>();

builder.Services.AddSingleton<INotificationClient, ConsoleNotificationClient>();
builder.Services.AddScoped<INotifier, EmailNotifierAdapter>();
builder.Services.AddScoped<OrderFacade>();

builder.Services.AddScoped<IOrderProcessingStrategy, StandardOrderProcessingStrategy>();
builder.Services.AddScoped<IOrderProcessingStrategy, UrgentOrderProcessingStrategy>();
builder.Services.AddScoped<OrderProcessingContext>();

builder.Services.AddScoped<IOrderObserver, EmailOrderObserver>();
builder.Services.AddScoped<IOrderObserver, LoggingOrderObserver>();

builder.Services.AddScoped<CommandInvoker>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "🧬 SIMPIM – Sistem informatic pentru managementul pacienților și investigațiilor medicale",
        Version = "v1.0 (Arhitectură Monolitică)",
        Description = @"<b>Universitatea Tehnică a Moldovei</b><br/>
                        <b>Disciplina:</b> Arhitectura sistemelor software<br/>
                        <b>Lucrare:</b> Sistem informatic pentru managementul pacienților și investigațiilor medicale<br/><br/>
                        <b>Descriere tehnică:</b><br/>
                        Aplicație realizată pe <b>arhitectură monolitică stratificată</b> (backend unificat .NET 8 + SQLite)."
    });
});

// --- Database ---
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Db")));

builder.Services.AddHealthChecks().AddSqlite(builder.Configuration.GetConnectionString("Db")!);

var app = builder.Build();
app.UseDefaultFiles();   
app.UseStaticFiles();    

// --- Middleware ---
app.UseSwagger();
app.UseSwaggerUI();
app.MapHealthChecks("/health");

// ======================= PACIENȚI =======================
app.MapGet("/api/patients", async (AppDbContext db) =>
    await db.Patients.ToListAsync())
    .WithSummary("Afișează lista pacienților")
    .WithTags("Pacienți");

app.MapGet("/api/patients/{idnp}", async (string idnp, AppDbContext db) =>
{
    var pacient = await db.Patients.FirstOrDefaultAsync(p => p.IDNP == idnp);
    return pacient is null ? Results.NotFound("Pacientul nu a fost găsit.") : Results.Ok(pacient);
}).WithTags("Pacienți");

app.MapPost("/api/patients", async (AppDbContext db, Patient pacient) =>
{
    if (string.IsNullOrWhiteSpace(pacient.IDNP) || pacient.IDNP.Length != 13 || !pacient.IDNP.All(char.IsDigit))
        return Results.BadRequest("IDNP trebuie să conțină exact 13 cifre.");
    if (await db.Patients.AnyAsync(p => p.IDNP == pacient.IDNP))
        return Results.BadRequest("Există deja un pacient cu acest IDNP.");

    pacient.DataInregistrarii = DateTime.Now;
    db.Patients.Add(pacient);
    await db.SaveChangesAsync();
    return Results.Created($"/api/patients/{pacient.Id}", pacient);
}).WithTags("Pacienți");

app.MapDelete("/api/patients/{idnp}", async (string idnp, AppDbContext db) =>
{
    var pacient = await db.Patients.FirstOrDefaultAsync(p => p.IDNP == idnp);
    if (pacient == null) return Results.NotFound("Pacientul nu a fost găsit.");
    db.Patients.Remove(pacient);
    await db.SaveChangesAsync();
    return Results.Ok("Pacient șters cu succes.");
}).WithTags("Pacienți");

// ================== INVESTIGAȚII (catalog) =================
app.MapGet("/api/investigatii", async (AppDbContext db) =>
    await db.Investigatii.Include(i => i.Parametri).ToListAsync())
    .WithTags("Investigații");

    // ================== COSTURI PACIENT =================
app.MapGet("/api/patients/{id}/costs", async (int id, AppDbContext db) =>
{
    var patient = await db.Patients.FirstOrDefaultAsync(p => p.Id == id);
    if (patient is null)
        return Results.NotFound("Pacientul nu există.");

    // toate comenzile pacientului
    var orders = await db.ComenziInvestigatii
        .Where(c => c.PatientId == id)
        .OrderByDescending(c => c.DataComanda)
        .ToListAsync();

    if (orders.Count == 0)
    {
        return Results.Ok(new
        {
            patient.Id,
            patient.Nume,
            patient.Prenume,
            patient.IDNP,
            Investigatii = Array.Empty<object>(),
            Total = 0m
        });
    }

    // catalogul de investigații (pentru Cod și Denumire)
    var investigatii = await db.Investigatii.ToListAsync();
    var invById = investigatii.ToDictionary(i => i.Id);

    // Decorator: Standard -> Urgent -> Îndreptare (gratis)
    ICostCalculator calc = new BaseCostCalculator();
    calc = new UrgentCostDecorator(calc, 1.5m);   // +50% la Urgent
    calc = new ReferralFreeDecorator(calc);       // Îndreptare = 0 lei

    var items = new List<object>();
    decimal total = 0m;

    foreach (var order in orders)
    {
        if (!invById.TryGetValue(order.InvestigatieId, out var inv))
            continue;

        var cost = calc.Calculate(order, inv);
        total += cost;

        items.Add(new
        {
            OrderId = order.Id,
            Investigatie = inv.Denumire,
            Cod = inv.Cod,
            DataComanda = order.DataComanda,
            Status = order.Status,
            Cost = cost
        });
    }

    return Results.Ok(new
    {
        patient.Id,
        patient.Nume,
        patient.Prenume,
        patient.IDNP,
        Investigatii = items,
        Total = total
    });
}).WithTags("Costuri");


// ================== COMENZI & REZULTATE ====================
app.MapPost("/api/orders", async (AppDbContext db, CreateOrderRequest req) =>
{
    var inv = await db.Investigatii.FindAsync(req.InvestigatieId);
    if (inv == null) return Results.NotFound("Investigația nu există.");
    if (!await db.Patients.AnyAsync(p => p.Id == req.PatientId))
        return Results.NotFound("Pacientul nu există.");

    // tip comandă după OrderType
    var status = (req.OrderType ?? "").ToLowerInvariant() switch
    {
        "urgent"     => "Urgent",
        "indreptare" => "Indreptare", // gratis
        _            => "Standard"
    };

    var comanda = new ComandaInvestigatie
    {
        PatientId = req.PatientId,
        InvestigatieId = req.InvestigatieId,
        CodInvestigatie = inv.Cod,
        DenumireInvestigatie = inv.Denumire,
        DataComanda = DateTime.Now,
        Status = status
    };

    db.ComenziInvestigatii.Add(comanda);
    await db.SaveChangesAsync();

    return Results.Created($"/api/orders/{comanda.Id}", comanda);
}).WithTags("Comenzi");


app.MapGet("/api/orders", async (AppDbContext db) =>
    await db.ComenziInvestigatii
        .OrderByDescending(o => o.DataComanda)
        .Select(o => new
        {
            o.Id,
            o.PatientId,
            o.InvestigatieId,
            o.CodInvestigatie,
            o.DenumireInvestigatie,
            o.Status,
            o.DataComanda,
            o.DataInvestigatie,
            o.DataRezultate
        }).ToListAsync())
    .WithTags("Comenzi");

// ---------- Template rezultate ----------
app.MapGet("/api/orders/{id}/results/template", async (int id, AppDbContext db) =>
{
    var comanda = await db.ComenziInvestigatii.FindAsync(id);
    if (comanda == null) return Results.NotFound("Comanda nu există.");

    var investigatie = await db.Investigatii
        .Include(i => i.Parametri)
        .FirstOrDefaultAsync(i => i.Id == comanda.InvestigatieId);

    if (investigatie == null || investigatie.Parametri.Count == 0)
        return Results.NotFound("Investigația nu are parametri.");

    var template = investigatie.Parametri.Select(p => new
    {
        p.CodParametru,
        p.Denumire,
        p.Unitate,
        p.ValoareMin,
        p.ValoareMax
    });

    return Results.Ok(template);
}).WithTags("Comenzi");

// ---------- POST Rezultate ----------
app.MapPost("/api/orders/{id}/results", async (
    int id,
    AppDbContext db,
    List<RezultatInvestigatie> rezultate) =>
{
    var comanda = await db.ComenziInvestigatii.FindAsync(id);
    if (comanda == null) return Results.NotFound("Comanda nu există.");

    foreach (var r in rezultate)
        r.ComandaId = id;

    db.RezultateInvestigatii.AddRange(rezultate);

    comanda.Status = "Completed";
    comanda.DataRezultate = DateTime.Now;   //  Data rezultatelor persistată

    await db.SaveChangesAsync();
    return Results.Ok("Rezultatele au fost salvate cu succes.");
}).WithTags("Rezultate");

app.MapGet("/api/orders/{id}/results", async (int id, AppDbContext db) =>
    await db.RezultateInvestigatii.Where(r => r.ComandaId == id).ToListAsync())
    .WithTags("Rezultate");

// --- Migrații + seed ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, "Database"));
    await db.Database.MigrateAsync();
    DbSeeder.Seed(db);
}

app.Run();
