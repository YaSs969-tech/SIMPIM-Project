namespace SimPim.Api.Patterns.Structural;


/// Client "extern"  API de email / SMS.

public interface INotificationClient
{
    Task SendEmailAsync(string subject, string body, CancellationToken ct = default);
}
