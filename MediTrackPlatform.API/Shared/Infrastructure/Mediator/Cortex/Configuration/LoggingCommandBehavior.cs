using Cortex.Mediator.Commands;

namespace MediTrackPlatform.API.Shared.Infrastructure.Mediator.Cortex.Configuration;

public class LoggingCommandBehavior<TCommand> : ICommandPipelineBehavior<TCommand> where TCommand : ICommand
{
    public async Task Handle(TCommand command, CommandHandlerDelegate next, CancellationToken cancellationToken)
    {
        // Log command start
        Console.WriteLine($"Starting command: {typeof(TCommand).Name}");
        await next();
    }
}