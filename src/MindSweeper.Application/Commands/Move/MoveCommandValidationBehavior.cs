using FluentValidation;
using Microsoft.Extensions.Logging;
using MindSweeper.Application.Behaviors;
using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.Move;

/// <summary>
/// Represents the behavior for validating the MoveCommand.
/// </summary>
public sealed class MoveCommandValidationBehavior : BaseValidationBehavior<MoveCommand, Result<MoveCommandResponse>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCommandValidationBehavior"/> class.
    /// </summary>
    /// <param name="validators">The validators to be used for validating the MoveCommand.</param>
    /// <param name="logger">The logger.</param>
    public MoveCommandValidationBehavior(
        IEnumerable<IValidator<MoveCommand>> validators,
        ILogger<MoveCommandValidationBehavior> logger) : base(validators, logger)
    {
    }

    /// <summary>
    /// Handles the invalid request by returning an invalid result with the validation errors.
    /// </summary>
    /// <param name="errors">The validation errors.</param>
    /// <returns>An invalid result with the validation errors.</returns>
    protected override Result<MoveCommandResponse> HandleInvalidRequest(List<ValidationIssue> errors)
        => Result<MoveCommandResponse>.Invalid(errors);
}
