namespace MindSweeper.Domain.Results;

/// <summary>
/// Represents an issue that occurred during validation.
/// </summary>
/// <param name="Identifier">The identifier of the validation issue.</param>
/// <param name="Message">The message describing the validation issue.</param>
/// <param name="Severity">The severity of the validation issue. Defaults to Error.</param>
public readonly record struct ValidationIssue(string Identifier, string Message, IssueSeverity Severity = IssueSeverity.Error);
