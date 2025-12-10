using SimPim.Api.Models;

namespace SimPim.Api.Patterns.Structural;

public interface ICostCalculator
{
    
    ///  Costul pentru o comandă + investigația asociată.
    
    decimal Calculate(ComandaInvestigatie order, Investigatie investigatie);
}
