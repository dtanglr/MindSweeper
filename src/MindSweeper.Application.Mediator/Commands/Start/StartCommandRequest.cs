namespace MindSweeper.Application.Mediator.Commands.Start;

/// <summary>
/// Represents a command to start the game.
/// </summary>
public record StartCommandRequest(GameSettings Settings) : IRequest<Result<StartCommandResponse>>;
