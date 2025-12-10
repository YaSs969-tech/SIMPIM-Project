namespace SimPim.Api.Patterns.Behavioral;

public interface ICommand
{
    Task ExecuteAsync(CancellationToken ct = default);
}
