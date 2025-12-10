using System;
using SimPim.Api.Models;

namespace SimPim.Api.Patterns.Structural;

public class ReferralFreeDecorator : CostCalculatorDecorator
{
    public ReferralFreeDecorator(ICostCalculator inner) : base(inner)
    {
    }

    public override decimal Calculate(ComandaInvestigatie order, Investigatie investigatie)
    {
        if (order.Status.Equals("Indreptare", StringComparison.OrdinalIgnoreCase))
        {
            // Comenzi pe bază de îndreptare sunt gratuite
            return 0m;
        }

        return Inner.Calculate(order, investigatie);
    }
}
