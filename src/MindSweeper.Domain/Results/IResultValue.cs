namespace MindSweeper.Domain.Results;

/// <summary>
/// Represents a result value.
/// </summary>
public interface IResultValue
{
    /// <summary>
    /// Gets the type of the value.
    /// </summary>
    Type ValueType { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <returns>The value.</returns>
    object? GetValue();
}
