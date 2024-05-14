namespace SchneiderElectric.MindSweeper.Application.Exceptions;

public class IllegalMoveException : Exception
{
    public IllegalMoveException()
    {
    }

    public IllegalMoveException(string? message) : base(message)
    {
    }

    public IllegalMoveException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
