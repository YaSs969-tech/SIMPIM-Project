using SimPim.Api.Data;
using SimPim.Api.Models;
using SimPim.Api.Patterns.Creational;
using SimPim.Api.Patterns.Behavioral;

namespace SimPim.Api.Patterns.Structural;


/// Facade – unifică logica pentru comenzi și rezultate.

public class OrderFacade
{
    private readonly AppDbContext _db;
    private readonly IOrderFactory _factory;
    private readonly OrderProcessingContext _processingContext;
    private readonly IEnumerable<IOrderObserver> _observers;

    public OrderFacade(
        AppDbContext db,
        IOrderFactory factory,
        OrderProcessingContext processingContext,
        IEnumerable<IOrderObserver> observers)
    {
        _db = db;
        _factory = factory;
        _processingContext = processingContext;
        _observers = observers;
    }

    // 1) Creare comandă nouă
    public Task<ComandaInvestigatie?> CreateOrderAsync(
        int patientId,
        int investigatieId,
        bool isUrgent,
        CancellationToken ct = default)
    {
        // atenție: folosim metode SINCRONE, fără EF Core async

        var patient = _db.Patients.FirstOrDefault(p => p.Id == patientId);
        var investigatie = _db.Investigatii.FirstOrDefault(i => i.Id == investigatieId);

        if (patient is null || investigatie is null)
            return Task.FromResult<ComandaInvestigatie?>(null);

        // Factory Method – creăm comanda
        var order = _factory.CreateOrder(patient, investigatie, isUrgent);

        // Strategy – alegem strategia de procesare
        var strategy = _processingContext.SelectStrategy(isUrgent);
        strategy.Process(order);

        // Salvăm în DB 
        _db.ComenziInvestigatii.Add(order);
        _db.SaveChanges();

        // Observer – notificăm observatorii
        NotifyObservers(order);

        return Task.FromResult<ComandaInvestigatie?>(order);
    }

    // 2) Listare comenzi pentru grid
    public Task<List<object>> GetOrdersProjectionAsync(CancellationToken ct = default)
    {
        var list = _db.ComenziInvestigatii
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
            })
            .Cast<object>()
            .ToList();

        return Task.FromResult(list);
    }

    // 3) Template pentru rezultate (parametrii investigației)
    public Task<IEnumerable<object>?> GetResultTemplateAsync(
        int orderId,
        CancellationToken ct = default)
    {
        var order = _db.ComenziInvestigatii.FirstOrDefault(c => c.Id == orderId);
        if (order is null)
            return Task.FromResult<IEnumerable<object>?>(null);

        var investigatie = _db.Investigatii
            .FirstOrDefault(i => i.Id == order.InvestigatieId);

        if (investigatie is null || investigatie.Parametri is null || !investigatie.Parametri.Any())
            return Task.FromResult<IEnumerable<object>?>(null);

        var template = investigatie.Parametri.Select(p => new
        {
            p.CodParametru,
            DenumireParametru = p.Denumire,
            p.Unitate,
            p.ValoareMin,
            p.ValoareMax
        })
        .Cast<object>();

        return Task.FromResult<IEnumerable<object>?>(template);
    }

    // 4) Salvare rezultate
    public Task<bool> SaveResultsAsync(
        int orderId,
        List<RezultatInvestigatie> input,
        CancellationToken ct = default)
    {
        var order = _db.ComenziInvestigatii.FirstOrDefault(c => c.Id == orderId);
        if (order is null)
            return Task.FromResult(false);

        // Builder – construim lista finală de rezultate
        var builder = new InvestigationResultsBuilder(orderId);
        foreach (var r in input)
        {
            builder.Add(r);
        }

        var results = builder.Build();

        _db.RezultateInvestigatii.AddRange(results);

        // actualizăm statusul comenzii
        order.Status = "Completed";
        order.DataRezultate = DateTime.Now;

        _db.SaveChanges();

        // Observer – notificăm observatorii
        NotifyObservers(order);

        return Task.FromResult(true);
    }

    // Helper pentru Observer (sincron)
    private void NotifyObservers(ComandaInvestigatie order)
    {
        foreach (var obs in _observers)
        {
            // Observatorii 
            _ = obs.OnStatusChangedAsync(order);
        }
    }
}
