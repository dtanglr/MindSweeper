using FluentValidation;
using Microsoft.Extensions.Logging;
using SchneiderElectric.MindSweeper.Application.Behaviors;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Commands.End;

public sealed class EndCommandValidationBehavior : BaseValidationBehavior<EndCommand, Result>
{
    public EndCommandValidationBehavior(
        IEnumerable<IValidator<EndCommand>> validators,
        ILogger<EndCommandValidationBehavior> logger) : base(validators, logger)
    {
    }

    protected override Result HandleInvalidRequest(List<ValidationIssue> errors)
        => Result.Invalid(errors);
}
