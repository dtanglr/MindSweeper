using MediatR;

namespace SchneiderElectric.MindSweeper.Application.Commands.Start;

public class StartCommand : IRequest
{
    public Guid GameId { get; init; }
    public int Columns { get; init; }
    public int Rows { get; init; }
    public int Bombs { get; init; }
    public int Lives { get; init; }
}
