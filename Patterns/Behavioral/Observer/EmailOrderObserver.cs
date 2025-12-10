using SimPim.Api.Models;
using SimPim.Api.Patterns.Creational;
using SimPim.Api.Patterns.Structural;

namespace SimPim.Api.Patterns.Behavioral;


/// Observer – trimite notificări când se creează / finalizează comanda.

public class EmailOrderObserver : IOrderObserver
{
    private readonly INotificationAbstractFactory _factory;

    public EmailOrderObserver(INotificationAbstractFactory factory)
    {
        _factory = factory;
    }

    public Task OnStatusChangedAsync(ComandaInvestigatie order, CancellationToken ct = default)
    {
        if (order.Status.StartsWith("Created"))
        {
            var notifier = _factory.CreateOrderCreatedNotifier();
            return notifier.NotifyAsync(
                "Comandă creată",
                $"Comanda #{order.Id} pentru {order.CodInvestigatie} a fost înregistrată.",
                ct);
        }

        if (order.Status == "Completed")
        {
            var notifier = _factory.CreateResultsReadyNotifier();
            return notifier.NotifyAsync(
                "Rezultate disponibile",
                $"Comanda #{order.Id} are rezultate introduse.",
                ct);
        }

        return Task.CompletedTask;
    }
}
