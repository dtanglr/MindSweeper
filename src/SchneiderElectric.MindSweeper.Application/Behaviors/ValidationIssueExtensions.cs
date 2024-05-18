using FluentValidation;
using FluentValidation.Results;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Behaviors;

internal static class ValidationIssueExtensions
{
    public static IEnumerable<ValidationIssue> AsErrors(this ValidationResult valResult)
    {
        foreach (var valFailure in valResult.Errors)
        {
            yield return new ValidationIssue
            {
                Severity = FromSeverity(valFailure.Severity),
                Message = valFailure.ErrorMessage,
                Identifier = valFailure.PropertyName
            };
        }
    }

    public static IssueSeverity FromSeverity(Severity severity) => severity switch
    {
        Severity.Error => IssueSeverity.Error,
        Severity.Warning => IssueSeverity.Warning,
        Severity.Info => IssueSeverity.Info,
        _ => throw new ArgumentOutOfRangeException(nameof(severity), "Unexpected Severity"),
    };
}
