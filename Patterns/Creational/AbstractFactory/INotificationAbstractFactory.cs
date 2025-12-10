using SimPim.Api.Patterns.Structural;

namespace SimPim.Api.Patterns.Creational;


/// Abstract Factory – creează notificatori pentru diferite scenarii.

public interface INotificationAbstractFactory
{
    INotifier CreateOrderCreatedNotifier();
    INotifier CreateResultsReadyNotifier();
}
