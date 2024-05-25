using MediatR;
using MindSweeper.Domain;

namespace MindSweeper.Application.Requests.GetGame;

public record GetGameRequest(string PlayerId) : IRequest<Result<GetGameRequestResponse>>;
