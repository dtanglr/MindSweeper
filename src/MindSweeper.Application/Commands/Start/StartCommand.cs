using MediatR;
using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.Start;

public record StartCommand(string PlayerId, Settings Settings) : IRequest<Result<StartCommandResponse>>;
