namespace MindSweeper.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a square index is out of range.
/// </summary>
public sealed class SquareIndexOutOfRangeException : SystemException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SquareIndexOutOfRangeException"/> class.
    /// </summary>
    public SquareIndexOutOfRangeException() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SquareIndexOutOfRangeException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public SquareIndexOutOfRangeException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SquareIndexOutOfRangeException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public SquareIndexOutOfRangeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
