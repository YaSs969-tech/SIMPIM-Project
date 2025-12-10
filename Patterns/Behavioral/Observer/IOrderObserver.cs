using SimPim.Api.Models;

namespace SimPim.Api.Patterns.Behavioral;

public interface IOrderObserver
{
    Task OnStatusChangedAsync(ComandaInvestigatie order, CancellationToken ct = default);
}
