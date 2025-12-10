using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimPim.Api.Data;
using SimPim.Api.Models;

namespace SimPim.Api.Endpoints;

public static class PatientEndpoints
{
    public static void MapPatientEndpoints(this WebApplication app)
    {
        app.MapGet("/api/patients", async (AppDbContext db) =>
            await db.Patients.ToListAsync())
           .WithSummary("Afișează lista pacienților")
           .WithTags("Pacienți");

        app.MapGet("/api/patients/{idnp}", async (AppDbContext db, string idnp) =>
        {
            var p = await db.Patients.FirstOrDefaultAsync(x => x.IDNP == idnp);
            return p is null ? Results.NotFound("Pacientul nu a fost găsit.") : Results.Ok(p);
        })
        .WithSummary("Caută pacient după IDNP")
        .WithTags("Pacienți");

        app.MapPost("/api/patients", async (AppDbContext db, Patient pacient) =>
        {
            if (string.IsNullOrWhiteSpace(pacient.IDNP) || pacient.IDNP.Length != 13 || !pacient.IDNP.All(char.IsDigit))
                return Results.BadRequest("IDNP trebuie să conțină exact 13 cifre.");
            if (await db.Patients.AnyAsync(x => x.IDNP == pacient.IDNP))
                return Results.BadRequest("Există deja un pacient cu acest IDNP.");

            pacient.DataInregistrarii = DateTime.Now;
            db.Patients.Add(pacient);
            await db.SaveChangesAsync();
            return Results.Created($"/api/patients/{pacient.Id}", pacient);
        })
        .WithSummary("Adaugă pacient nou")
        .WithTags("Pacienți");

        app.MapDelete("/api/patients/{idnp}", async (AppDbContext db, string idnp) =>
        {
            var p = await db.Patients.FirstOrDefaultAsync(x => x.IDNP == idnp);
            if (p is null) return Results.NotFound("Pacientul nu a fost găsit.");
            db.Patients.Remove(p);
            await db.SaveChangesAsync();
            return Results.Ok("Pacient șters cu succes.");
        })
        .WithSummary("Șterge pacient după IDNP")
        .WithTags("Pacienți");
    }
}
 