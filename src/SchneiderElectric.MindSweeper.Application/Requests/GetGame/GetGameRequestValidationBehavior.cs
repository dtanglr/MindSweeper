using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using SchneiderElectric.MindSweeper.Application.Behaviors;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Requests.GetGame;

public sealed class GetGameRequestValidationBehavior<TRequest, TResponse> : BaseValidationBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : Result<GetGameRequestResponse>
{
    public GetGameRequestValidationBehavior(
        IEnumerable<IValidator<TRequest>> validators,
        ILogger<GetGameRequestValidationBehavior<TRequest, TResponse>> logger) : base(validators, logger)
    {
    }

    protected override TResponse HandleInvalidRequest(List<ValidationIssue> errors)
        => (TResponse)Result<GetGameRequestResponse>.Invalid(errors);
}
