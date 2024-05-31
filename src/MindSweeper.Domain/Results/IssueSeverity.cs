namespace MindSweeper.Domain.Results;

/// <summary>
/// Represents the severity of an issue.
/// </summary>
public enum IssueSeverity
{
    /// <summary>
    /// Indicates a fatal issue.
    /// </summary>
    Fatal = 0,

    /// <summary>
    /// Indicates an error issue.
    /// </summary>
    Error = 1,

    /// <summary>
    /// Indicates a warning issue.
    /// </summary>
    Warning = 2,

    /// <summary>
    /// Indicates an informational issue.
    /// </summary>
    Info = 3
}
