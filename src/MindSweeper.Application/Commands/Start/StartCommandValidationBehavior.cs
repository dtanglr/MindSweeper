using FluentValidation;
using Microsoft.Extensions.Logging;
using MindSweeper.Application.Behaviors;
using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.Start;

public sealed class StartCommandValidationBehavior : BaseValidationBehavior<StartCommand, Result<StartCommandResponse>>
{
    public StartCommandValidationBehavior(
        IEnumerable<IValidator<StartCommand>> validators,
        ILogger<StartCommandValidationBehavior> logger) : base(validators, logger)
    {
    }

    protected override Result<StartCommandResponse> HandleInvalidRequest(List<ValidationIssue> errors)
        => Result<StartCommandResponse>.Invalid(errors);
}
