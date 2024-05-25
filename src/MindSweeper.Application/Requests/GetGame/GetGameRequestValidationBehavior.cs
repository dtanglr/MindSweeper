using FluentValidation;
using Microsoft.Extensions.Logging;
using MindSweeper.Application.Behaviors;
using MindSweeper.Domain;

namespace MindSweeper.Application.Requests.GetGame;

public sealed class GetGameRequestValidationBehavior : BaseValidationBehavior<GetGameRequest, Result<GetGameRequestResponse>>
{
    public GetGameRequestValidationBehavior(
        IEnumerable<IValidator<GetGameRequest>> validators,
        ILogger<GetGameRequestValidationBehavior> logger) : base(validators, logger)
    {
    }

    protected override Result<GetGameRequestResponse> HandleInvalidRequest(List<ValidationIssue> errors)
        => Result<GetGameRequestResponse>.Invalid(errors);
}
