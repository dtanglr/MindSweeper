using System.Net;

namespace SchneiderElectric.MindSweeper.Domain;

public readonly record struct OperationIssue(
    string Code,
    string Message,
    HttpStatusCode StatusCode,
    string? Diagnostics = null,
    IssueSeverity Severity = IssueSeverity.Error);
