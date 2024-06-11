namespace MindSweeper.Application.Mediator.Commands.End;

/// <summary>
/// Represents a command to end the game for the current player.
/// </summary>
public record EndCommandRequest() : IRequest<Result>;
