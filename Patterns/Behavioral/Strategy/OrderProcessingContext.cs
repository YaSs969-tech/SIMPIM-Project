namespace SimPim.Api.Patterns.Behavioral;


/// Context – selectează strategia potrivită (standard / urgent).

public class OrderProcessingContext
{
    private readonly IEnumerable<IOrderProcessingStrategy> _strategies;

    public OrderProcessingContext(IEnumerable<IOrderProcessingStrategy> strategies)
    {
        _strategies = strategies;
    }

    public IOrderProcessingStrategy SelectStrategy(bool isUrgent)
    {
        var key = isUrgent ? "urgent" : "standard";
        return _strategies.First(s => s.Key == key);
    }
}
