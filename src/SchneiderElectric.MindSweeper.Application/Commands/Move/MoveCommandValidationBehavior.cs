using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using SchneiderElectric.MindSweeper.Application.Behaviors;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Commands.Move;

public sealed class MoveCommandValidationBehavior<TRequest, TResponse> : BaseValidationBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : Result<MoveCommandResponse>
{
    public MoveCommandValidationBehavior(
        IEnumerable<IValidator<TRequest>> validators,
        ILogger<MoveCommandValidationBehavior<TRequest, TResponse>> logger) : base(validators, logger)
    {
    }

    protected override TResponse HandleInvalidRequest(List<ValidationIssue> errors)
        => (TResponse)Result<MoveCommandResponse>.Invalid(errors);
}
