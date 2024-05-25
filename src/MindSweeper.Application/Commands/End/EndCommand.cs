using MediatR;
using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.End;

public record EndCommand(string PlayerId) : IRequest<Result>;
