using SimPim.Api.Patterns.Structural;

namespace SimPim.Api.Patterns.Creational;


/// Abstract Factory concretă – folosește același notifier email pentru ambele scenarii.

public class EmailNotificationFactory : INotificationAbstractFactory
{
    private readonly INotifier _notifier;

    public EmailNotificationFactory(INotifier notifier)
    {
        _notifier = notifier;
    }

    public INotifier CreateOrderCreatedNotifier() => _notifier;

    public INotifier CreateResultsReadyNotifier() => _notifier;
}
