using SimPim.Api.Models;
using SimPim.Api.Patterns.Structural;

namespace SimPim.Api.Patterns.Behavioral;


/// Command – incapsulează logica de creare a unei comenzi.

public class CreateOrderCommand : ICommand
{
    private readonly OrderFacade _facade;
    private readonly int _patientId;
    private readonly int _investigationId;
    private readonly bool _isUrgent;

    public ComandaInvestigatie? CreatedOrder { get; private set; }

    public CreateOrderCommand(OrderFacade facade, int patientId, int investigationId, bool isUrgent)
    {
        _facade = facade;
        _patientId = patientId;
        _investigationId = investigationId;
        _isUrgent = isUrgent;
    }

    public async Task ExecuteAsync(CancellationToken ct = default)
    {
        CreatedOrder = await _facade.CreateOrderAsync(_patientId, _investigationId, _isUrgent, ct);
    }
}
