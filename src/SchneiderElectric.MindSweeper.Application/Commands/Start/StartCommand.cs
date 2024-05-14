using MediatR;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Commands.Start;

public record StartCommand(string PlayerId, Settings Settings) : IRequest<Result<StartCommandResponse>>;
