namespace MindSweeper.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when the row index is out of range.
/// </summary>
public sealed class RowIndexOutOfRangeException : SystemException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RowIndexOutOfRangeException"/> class.
    /// </summary>
    public RowIndexOutOfRangeException() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RowIndexOutOfRangeException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public RowIndexOutOfRangeException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RowIndexOutOfRangeException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public RowIndexOutOfRangeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
