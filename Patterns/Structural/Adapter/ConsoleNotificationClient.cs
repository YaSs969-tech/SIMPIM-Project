namespace SimPim.Api.Patterns.Structural;


/// Implementare simplă de client "extern"

public class ConsoleNotificationClient : INotificationClient
{
    public Task SendEmailAsync(string subject, string body, CancellationToken ct = default)
    {
        Console.WriteLine($"[EMAIL] {subject}: {body}");
        return Task.CompletedTask;
    }
}
