using Microsoft.Extensions.Options;
using SchneiderElectric.MindSweeper.Exceptions;
using SchneiderElectric.MindSweeper.Moves;
using SchneiderElectric.MindSweeper.Squares;

namespace SchneiderElectric.MindSweeper;

public class GameService
{
    private readonly Settings _settings;
    private readonly Field.Squares _squares;

    private bool _isGameStarted;
    private Field.Bombs? _bombs;
    private Square? _currentSquare;
#pragma warning disable S4487 // Unread "private" fields should be removed
#pragma warning disable IDE0052 // Remove unread private members
    private int _moves;
#pragma warning restore IDE0052 // Remove unread private members
#pragma warning restore S4487 // Unread "private" fields should be removed
    private int _lives;

    public GameService(IOptions<Settings> settings)
    {
        _settings = settings.Value;
        _squares = new(_settings.Columns, _settings.Rows);
    }

    public void StartGame()
    {
        _isGameStarted = true;
        _bombs = new(_settings.Columns, _settings.Rows, _settings.Bombs);
        _currentSquare = _squares.GetStartSquare();
        _moves = 0;
        _lives = _settings.Lives;
    }

    public void Move(Move move)
    {
        if (!_isGameStarted)
        {
            throw new GameNotStartedException();
        }

        if (_lives == 0)
        {
            throw new GameOverException();
        }

        if (!_currentSquare!.TryMove(move, out var square))
        {
            throw new IllegalMoveException();
        }

        _currentSquare = square;
        _moves++;

        if (_bombs!.OnSquare(_currentSquare!))
        {
            _lives--;
        }
    }

    public void StopGame()
    {
        _isGameStarted = false;
        _bombs = null;
        _currentSquare = null;
        _moves = 0;
    }
}
