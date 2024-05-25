using FluentValidation;

namespace MindSweeper.Application.Requests.GetGame;

public class GetGameRequestValidator : AbstractValidator<GetGameRequest>
{
    public GetGameRequestValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty();
    }
}
