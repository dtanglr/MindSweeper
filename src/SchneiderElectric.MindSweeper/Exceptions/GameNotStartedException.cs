namespace SchneiderElectric.MindSweeper.Exceptions;

public sealed class GameNotStartedException : SystemException
{
    public GameNotStartedException() : base()
    {
    }

    public GameNotStartedException(string? message) : base(message)
    {
    }

    public GameNotStartedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
