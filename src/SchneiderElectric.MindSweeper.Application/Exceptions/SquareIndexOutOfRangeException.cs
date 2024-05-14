namespace SchneiderElectric.MindSweeper.Application.Exceptions;

public sealed class SquareIndexOutOfRangeException : SystemException
{
    public SquareIndexOutOfRangeException() : base()
    {
    }

    public SquareIndexOutOfRangeException(string? message) : base(message)
    {
    }

    public SquareIndexOutOfRangeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
