using FluentValidation;
using Microsoft.Extensions.Logging;
using SchneiderElectric.MindSweeper.Application.Behaviors;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Commands.Move;

public sealed class MoveCommandValidationBehavior : BaseValidationBehavior<MoveCommand, Result<MoveCommandResponse>>
{
    public MoveCommandValidationBehavior(
        IEnumerable<IValidator<MoveCommand>> validators,
        ILogger<MoveCommandValidationBehavior> logger) : base(validators, logger)
    {
    }

    protected override Result<MoveCommandResponse> HandleInvalidRequest(List<ValidationIssue> errors)
        => Result<MoveCommandResponse>.Invalid(errors);
}
