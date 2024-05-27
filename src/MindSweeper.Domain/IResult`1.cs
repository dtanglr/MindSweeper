namespace MindSweeper.Domain;

/// <summary>
/// Represents a result that contains a value of type T.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public interface IResult<out T> : IResult, IResultValue
{
    /// <summary>
    /// Gets the value of the result.
    /// </summary>
    T? Value { get; }
}

