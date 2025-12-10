namespace SimPim.Api.Patterns.Behavioral;

public class CommandInvoker
{
    public Task ExecuteAsync(ICommand command, CancellationToken ct = default)
        => command.ExecuteAsync(ct);
}
