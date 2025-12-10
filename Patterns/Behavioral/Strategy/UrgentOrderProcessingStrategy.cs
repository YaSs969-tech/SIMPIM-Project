using SimPim.Api.Models;

namespace SimPim.Api.Patterns.Behavioral;

public class UrgentOrderProcessingStrategy : IOrderProcessingStrategy
{
    public string Key => "urgent";

    public void Process(ComandaInvestigatie order)
    {
        // Marcăm clar comanda ca urgentă
        if (!order.Status.StartsWith("Created"))
            order.Status = "Created-Urgent";
    }
}
