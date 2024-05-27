namespace MindSweeper.Domain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a column index is out of range.
/// </summary>
public sealed class ColumnIndexOutOfRangeException : SystemException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ColumnIndexOutOfRangeException"/> class.
    /// </summary>
    public ColumnIndexOutOfRangeException() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColumnIndexOutOfRangeException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public ColumnIndexOutOfRangeException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColumnIndexOutOfRangeException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public ColumnIndexOutOfRangeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
