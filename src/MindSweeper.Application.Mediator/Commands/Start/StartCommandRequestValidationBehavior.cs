using FluentValidation;
using Microsoft.Extensions.Logging;
using MindSweeper.Application.Mediator.Behaviors;

namespace MindSweeper.Application.Mediator.Commands.Start;

/// <summary>
/// Represents the behavior for validating the StartCommand.
/// </summary>
public sealed class StartCommandRequestValidationBehavior : BaseValidationBehavior<StartCommandRequest, Result<StartCommandResponse>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StartCommandRequestValidationBehavior"/> class.
    /// </summary>
    /// <param name="validators">The validators to be used for validating the StartCommand.</param>
    /// <param name="logger">The logger.</param>
    public StartCommandRequestValidationBehavior(IEnumerable<IValidator<StartCommandRequest>> validators, ILogger<StartCommandRequestValidationBehavior> logger)
        : base(validators, logger)
    {
    }

    /// <summary>
    /// Handles the invalid request by returning an invalid result with the validation errors.
    /// </summary>
    /// <param name="errors">The validation errors.</param>
    /// <returns>An invalid result with the validation errors.</returns>
    protected override Result<StartCommandResponse> HandleInvalidRequest(List<ValidationIssue> errors)
        => Result<StartCommandResponse>.Invalid(errors);
}
