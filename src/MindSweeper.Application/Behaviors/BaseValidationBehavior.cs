using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSweeper.Domain;

namespace MindSweeper.Application.Behaviors;

public abstract class BaseValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : class, IResult
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger _logger;

    protected BaseValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger logger)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected abstract TResponse HandleInvalidRequest(List<ValidationIssue> errors);

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var results = _validators.Select(x => x.Validate(context));
        var errors = new List<ValidationIssue>();

        foreach (var result in results)
        {
            errors.AddRange(result.AsErrors());
        }

        if (errors.Count != 0)
        {
            _logger.LogDebug("Request had {ValidationIssueCount} validation issues", errors.Count);

            foreach (var error in errors)
            {
                _logger.LogDebug("@{ValidationIssue}", error);
            }

            return HandleInvalidRequest(errors);
        }

        return await next();
    }
}
