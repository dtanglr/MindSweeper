using FluentValidation;

namespace MindSweeper.Application.Requests.GetGame;

/// <summary>
/// Validator for the GetGameRequest class.
/// </summary>
public class GetGameRequestValidator : AbstractValidator<GetGameRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetGameRequestValidator"/> class.
    /// </summary>
    public GetGameRequestValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty();
    }
}
