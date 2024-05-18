using FluentValidation;

namespace SchneiderElectric.MindSweeper.Application.Requests.GetGame;

public class GetGameRequestValidator : AbstractValidator<GetGameRequest>
{
    public GetGameRequestValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty();
    }
}
