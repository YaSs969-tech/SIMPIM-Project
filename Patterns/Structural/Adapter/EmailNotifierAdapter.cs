namespace SimPim.Api.Patterns.Structural;


/// Adapter – adaptează INotificationClient la interfața internă INotifier.

public class EmailNotifierAdapter : INotifier
{
    private readonly INotificationClient _client;

    public EmailNotifierAdapter(INotificationClient client)
    {
        _client = client;
    }

    public Task NotifyAsync(string subject, string message, CancellationToken ct = default)
        => _client.SendEmailAsync(subject, message, ct);
}
