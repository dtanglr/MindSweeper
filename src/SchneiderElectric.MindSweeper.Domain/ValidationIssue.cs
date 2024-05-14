namespace SchneiderElectric.MindSweeper.Domain;

public readonly record struct ValidationIssue(string Identifier, string Message, IssueSeverity Severity = IssueSeverity.Error);
