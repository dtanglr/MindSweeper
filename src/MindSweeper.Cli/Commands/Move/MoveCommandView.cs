using MindSweeper.Application.Mediator.Commands.Move;
using MindSweeper.Domain.Results;

namespace MindSweeper.Cli.Commands.Move;

/// <summary>
/// Represents a view for rendering the result of a move command.
/// </summary>
internal class MoveCommandView : ICommandView<MoveCommandRequest, Result<MoveCommandResponse>>
{
    private readonly IGameConsole _console;
    private readonly ErrorView _errorView;
    private readonly GameStatusView _gameStatusView;

    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCommandView"/> class.
    /// </summary>
    /// <param name="console">The game console.</param>
    public MoveCommandView(IGameConsole console)
    {
        _console = console;
        _errorView = new(_console);
        _gameStatusView = new(_console);
    }

    /// <summary>
    /// Renders the result of a move command.
    /// </summary>
    /// <param name="request">The move command request.</param>
    /// <param name="result">The result of the move command.</param>
    public void Render(MoveCommandRequest request, Result<MoveCommandResponse> result)
    {
        _console.WriteLine();

        switch (result.Status)
        {
            case ResultStatus.Accepted:
                var response = result.Value!;
                var game = response.Game;
                var move = game.LastMove!;

                _console.WriteLine(Resources.MoveCommandDetails,
                    move.Direction.ToString().ToLowerInvariant(),
                    move.FromSquare,
                    move.ToSquare);

                _console.WriteLine();

                switch (game.Status)
                {
                    case GameStatus.InProgress:
                        _console.WriteLine(move.HitBomb ? Resources.Boom : Resources.Yes);
                        _console.WriteLine(move.HitBomb ? Resources.MoveCommandDidHitBomb : Resources.MoveCommandDidNotHitBomb);
                        _console.WriteLine();
                        _console.WriteLine(Resources.MoveCommandResultStatusAccepted);
                        break;
                    case GameStatus.Won:
                        _console.Write(Resources.GameOver);
                        _console.Write(Resources.YouWin);
                        break;
                    case GameStatus.Lost:
                        _console.Write(Resources.Boom);
                        _console.Write(Resources.GameOver);
                        _console.Write(Resources.YouLose);
                        break;
                    default:
                        break;
                }

                _console.WriteLine();
                _gameStatusView.Render(game);
                break;
            case ResultStatus.Unprocessable:
                _console.WriteLine(Resources.MoveCommandResultStatusUnprocessable, request.Direction);
                break;
            case ResultStatus.NotFound:
                _console.WriteLine(Resources.MoveCommandResultStatusNotFound);
                break;
            default:
                _errorView.Render(result.ToResult());
                break;
        }

        _console.WriteLine();
    }
}
