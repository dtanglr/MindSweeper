using FluentValidation;
using Microsoft.Extensions.Logging;
using MindSweeper.Application.Behaviors;
using MindSweeper.Domain;

namespace MindSweeper.Application.Requests.GetGame;

/// <summary>
/// Represents the behavior for validating the GetGameRequest.
/// </summary>
public sealed class GetGameRequestValidationBehavior : BaseValidationBehavior<GetGameRequest, Result<GetGameRequestResponse>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetGameRequestValidationBehavior"/> class.
    /// </summary>
    /// <param name="validators">The validators to be used for validating the request.</param>
    /// <param name="logger">The logger.</param>
    public GetGameRequestValidationBehavior(
        IEnumerable<IValidator<GetGameRequest>> validators,
        ILogger<GetGameRequestValidationBehavior> logger) : base(validators, logger)
    {
    }

    /// <summary>
    /// Handles the invalid request by returning the validation errors.
    /// </summary>
    /// <param name="errors">The list of validation issues.</param>
    /// <returns>The result indicating an invalid request with the validation errors.</returns>
    protected override Result<GetGameRequestResponse> HandleInvalidRequest(List<ValidationIssue> errors)
        => Result<GetGameRequestResponse>.Invalid(errors);
}
