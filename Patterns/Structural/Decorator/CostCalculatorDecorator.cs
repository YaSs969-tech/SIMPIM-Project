using SimPim.Api.Models;

namespace SimPim.Api.Patterns.Structural;

public abstract class CostCalculatorDecorator : ICostCalculator
{
    protected readonly ICostCalculator Inner;

    protected CostCalculatorDecorator(ICostCalculator inner)
    {
        Inner = inner;
    }

    public abstract decimal Calculate(ComandaInvestigatie order, Investigatie investigatie);
}
