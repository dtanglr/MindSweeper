using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using MindSweeper.Domain.Results;

namespace MindSweeper.Application.Mediator.Behaviors;

/// <summary>
/// Base behavior for validation of requests.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public abstract class BaseValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : class, IResult
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseValidationBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="validators">The validators to be used for request validation.</param>
    /// <param name="logger">The logger to be used for logging validation issues.</param>
    protected BaseValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger logger)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Handles the invalid request by returning the appropriate response.
    /// </summary>
    /// <param name="errors">The list of validation issues.</param>
    /// <returns>The response representing the invalid request.</returns>
    protected abstract TResponse HandleInvalidRequest(List<ValidationIssue> errors);

    /// <summary>
    /// Handles the request by performing validation and calling the next handler in the pipeline.
    /// </summary>
    /// <param name="request">The request to be handled.</param>
    /// <param name="next">The delegate representing the next handler in the pipeline.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response from the next handler in the pipeline.</returns>
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

        if (errors.Count > 0)
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
