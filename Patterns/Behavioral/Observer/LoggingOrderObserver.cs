using Microsoft.Extensions.Logging;
using SimPim.Api.Models;

namespace SimPim.Api.Patterns.Behavioral;


/// Observer care loghează schimbările de status ale comenzii.

public class LoggingOrderObserver : IOrderObserver
{
    private readonly ILogger<LoggingOrderObserver> _logger;

    public LoggingOrderObserver(ILogger<LoggingOrderObserver> logger)
    {
        _logger = logger;
    }

    public Task OnStatusChangedAsync(ComandaInvestigatie order, CancellationToken ct = default)
    {
        _logger.LogInformation("Status comanda {Id} -> {Status}", order.Id, order.Status);
        return Task.CompletedTask;
    }
}
