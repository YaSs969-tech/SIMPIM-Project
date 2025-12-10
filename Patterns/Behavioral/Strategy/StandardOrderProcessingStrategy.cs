using SimPim.Api.Models;

namespace SimPim.Api.Patterns.Behavioral;

public class StandardOrderProcessingStrategy : IOrderProcessingStrategy
{
    public string Key => "standard";

    public void Process(ComandaInvestigatie order)
    {
        // Nimic special pentru standard – doar menținem statusul.
        if (string.IsNullOrEmpty(order.Status))
            order.Status = "Created";
    }
}
