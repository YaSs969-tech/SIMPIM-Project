namespace SimPim.Api.Models;

public record CreateOrderRequest(int PatientId, int InvestigatieId, string OrderType);
