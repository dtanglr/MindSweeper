using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using SchneiderElectric.MindSweeper.Application.Behaviors;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Commands.Start;

public sealed class StartCommandValidationBehavior<TRequest, TResponse> : BaseValidationBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : Result<StartCommandResponse>
{
    public StartCommandValidationBehavior(
        IEnumerable<IValidator<TRequest>> validators,
        ILogger<StartCommandValidationBehavior<TRequest, TResponse>> logger) : base(validators, logger)
    {
    }

    protected override TResponse HandleInvalidRequest(List<ValidationIssue> errors)
        => (TResponse)Result<StartCommandResponse>.Invalid(errors);
}
