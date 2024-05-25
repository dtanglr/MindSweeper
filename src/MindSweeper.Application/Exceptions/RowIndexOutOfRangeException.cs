namespace MindSweeper.Application.Exceptions;

public sealed class RowIndexOutOfRangeException : SystemException
{
    public RowIndexOutOfRangeException() : base()
    {
    }

    public RowIndexOutOfRangeException(string? message) : base(message)
    {
    }

    public RowIndexOutOfRangeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
