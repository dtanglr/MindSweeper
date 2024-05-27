using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.Start;

/// <summary>
/// Represents the response of the StartCommand.
/// </summary>
public record StartCommandResponse(Game Game);
