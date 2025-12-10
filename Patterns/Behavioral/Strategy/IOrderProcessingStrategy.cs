using SimPim.Api.Models;

namespace SimPim.Api.Patterns.Behavioral;


/// Strategy – definește modul de procesare a unei comenzi.

public interface IOrderProcessingStrategy
{
    string Key { get; }
    void Process(ComandaInvestigatie order);
}
