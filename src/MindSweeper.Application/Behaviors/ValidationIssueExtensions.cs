using FluentValidation;
using FluentValidation.Results;
using MindSweeper.Domain.Results;

namespace MindSweeper.Application.Behaviors;

internal static class ValidationIssueExtensions
{
    /// <summary>
    /// Converts the validation failures to a collection of <see cref="ValidationIssue"/> with severity set as errors.
    /// </summary>
    /// <param name="result">The <see cref="ValidationResult"/> containing the validation failures.</param>
    /// <returns>A collection of <see cref="ValidationIssue"/> with severity set as errors.</returns>
    public static IEnumerable<ValidationIssue> AsErrors(this ValidationResult result)
    {
        foreach (var valFailure in result.Errors)
        {
            yield return new ValidationIssue
            {
                Severity = FromSeverity(valFailure.Severity),
                Message = valFailure.ErrorMessage,
                Identifier = valFailure.PropertyName
            };
        }
    }

    /// <summary>
    /// Converts the <see cref="Severity"/> enum to <see cref="IssueSeverity"/> enum.
    /// </summary>
    /// <param name="severity">The <see cref="Severity"/> value to convert.</param>
    /// <returns>The corresponding <see cref="IssueSeverity"/> value.</returns>
    public static IssueSeverity FromSeverity(Severity severity) => severity switch
    {
        Severity.Error => IssueSeverity.Error,
        Severity.Warning => IssueSeverity.Warning,
        Severity.Info => IssueSeverity.Info,
        _ => throw new ArgumentOutOfRangeException(nameof(severity), "Unexpected Severity"),
    };
}
