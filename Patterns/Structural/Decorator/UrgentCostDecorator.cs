using System;
using SimPim.Api.Models;

namespace SimPim.Api.Patterns.Structural;

public class UrgentCostDecorator : CostCalculatorDecorator
{
    private readonly decimal _urgentMultiplier;

    
    /// Multiplicator pentru comenzi urgente. +50%.
    
    public UrgentCostDecorator(ICostCalculator inner, decimal urgentMultiplier = 1.5m)
        : base(inner)
    {
        _urgentMultiplier = urgentMultiplier;
    }

    public override decimal Calculate(ComandaInvestigatie order, Investigatie investigatie)
    {
        var baseCost = Inner.Calculate(order, investigatie);

        if (order.Status.Equals("Urgent", StringComparison.OrdinalIgnoreCase))
        {
            return decimal.Round(baseCost * _urgentMultiplier, 2);
        }

        return baseCost;
    }
}
