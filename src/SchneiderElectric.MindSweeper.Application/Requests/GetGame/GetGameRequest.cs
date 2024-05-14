using MediatR;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Requests.GetGame;

public record GetGameRequest(string PlayerId) : IRequest<Result<GetGameRequestResponse>>;
