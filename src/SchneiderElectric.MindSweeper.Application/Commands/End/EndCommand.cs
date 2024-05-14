using MediatR;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Commands.End;

public record EndCommand(string PlayerId) : IRequest<Result>;
