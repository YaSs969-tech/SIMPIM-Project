using SimPim.Api.Models;

namespace SimPim.Api.Patterns.Structural;

public class BaseCostCalculator : ICostCalculator
{
    // Prețuri STANDARD per cod de investigație
    private static readonly Dictionary<string, decimal> StandardPrices =
        new(StringComparer.OrdinalIgnoreCase)
    {
        ["CBC"]  = 200m,
        ["GLU"]  = 80m,
        ["LIP"]  = 250m,
        ["IMM"]  = 300m,
        ["ELEC"] = 180m,
        ["HEP"]  = 220m,
        ["REN"]  = 210m,
        ["URI"]  = 90m,
        ["THY"]  = 260m,
        ["HORF"] = 280m
    };

    public decimal Calculate(ComandaInvestigatie order, Investigatie investigatie)
    {
        if (!StandardPrices.TryGetValue(investigatie.Cod, out var price))
        {
            // Preț default 
            price = 100m;
        }

        return price;
    }
}
