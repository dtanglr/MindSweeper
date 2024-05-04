namespace SchneiderElectric.MindSweeper.Exceptions;

public class GameOverException : Exception
{
    public GameOverException()
    {
    }

    public GameOverException(string? message) : base(message)
    {
    }

    public GameOverException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
