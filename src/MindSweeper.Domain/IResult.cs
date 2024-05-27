namespace MindSweeper.Domain;

/// <summary>
/// Represents the result of an operation.
/// </summary>
public interface IResult
{
    /// <summary>
    /// Gets the status of the result.
    /// </summary>
    ResultStatus Status { get; }

    /// <summary>
    /// Gets the list of errors associated with the result.
    /// </summary>
    List<string> Errors { get; }

    /// <summary>
    /// Gets the list of validation issues associated with the result.
    /// </summary>
    List<ValidationIssue> ValidationIssues { get; }

    /// <summary>
    /// Gets the operation issue associated with the result.
    /// </summary>
    OperationIssue OperationIssue { get; }

    /// <summary>
    /// Gets a value indicating whether the result has an operation issue.
    /// </summary>
    bool HasOperationIssue { get; }

    /// <summary>
    /// Gets a value indicating whether the result is successful.
    /// </summary>
    bool IsSuccess { get; }
}
