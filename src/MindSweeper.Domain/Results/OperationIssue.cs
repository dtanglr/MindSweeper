using System.Net;

namespace MindSweeper.Domain.Results;

/// <summary>
/// Represents an operation issue.
/// </summary>
/// <param name="Code">The code associated with the issue.</param>
/// <param name="Message">The message describing the issue.</param>
/// <param name="StatusCode">The HTTP status code associated with the issue.</param>
/// <param name="Diagnostics">Additional diagnostic information for the issue.</param>
/// <param name="Severity">The severity level of the issue.</param>
public readonly record struct OperationIssue(
    string Code,
    string Message,
    HttpStatusCode StatusCode,
    string? Diagnostics = null,
    IssueSeverity Severity = IssueSeverity.Error);
