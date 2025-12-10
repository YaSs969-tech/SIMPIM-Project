namespace SimPim.Api.Patterns.Structural;


/// Interfața internă  "Notifică". 

public interface INotifier
{
    Task NotifyAsync(string subject, string message, CancellationToken ct = default);
}
