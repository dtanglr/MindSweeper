namespace SchneiderElectric.MindSweeper.Application.Exceptions;

public sealed class ColumnIndexOutOfRangeException : SystemException
{
    public ColumnIndexOutOfRangeException() : base()
    {
    }

    public ColumnIndexOutOfRangeException(string? message) : base(message)
    {
    }

    public ColumnIndexOutOfRangeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
