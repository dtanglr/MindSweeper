using FluentValidation;
using Microsoft.Extensions.Logging;
using MindSweeper.Application.Behaviors;
using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.End;

/// <summary>
/// Represents the behavior for validating the EndCommand.
/// </summary>
public sealed class EndCommandValidationBehavior : BaseValidationBehavior<EndCommand, Result>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EndCommandValidationBehavior"/> class.
    /// </summary>
    /// <param name="validators">The validators to be used for validating the EndCommand.</param>
    /// <param name="logger">The logger.</param>
    public EndCommandValidationBehavior(
        IEnumerable<IValidator<EndCommand>> validators,
        ILogger<EndCommandValidationBehavior> logger) : base(validators, logger)
    {
    }

    /// <summary>
    /// Handles the invalid request by returning an invalid result with the validation errors.
    /// </summary>
    /// <param name="errors">The validation errors.</param>
    /// <returns>An invalid result with the validation errors.</returns>
    protected override Result HandleInvalidRequest(List<ValidationIssue> errors)
        => Result.Invalid(errors);
}
