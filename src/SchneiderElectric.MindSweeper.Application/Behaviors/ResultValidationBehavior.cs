using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Behaviors;

public sealed class ResultValidationBehavior<TRequest, TResponse> : BaseValidationBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : Result
{
    public ResultValidationBehavior(
        IEnumerable<IValidator<TRequest>> validators,
        ILogger<ResultValidationBehavior<TRequest, TResponse>> logger) : base(validators, logger)
    {
    }

    protected override TResponse HandleInvalidRequest(List<ValidationIssue> errors)
        => (TResponse)Result.Invalid(errors);
}
