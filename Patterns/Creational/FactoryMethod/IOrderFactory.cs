using SimPim.Api.Models;

namespace SimPim.Api.Patterns.Creational;


/// Factory Method – definește contractul de creare a unei comenzi de investigație.

public interface IOrderFactory
{
    ComandaInvestigatie CreateOrder(Patient patient, Investigatie investigation, bool isUrgent);
}
