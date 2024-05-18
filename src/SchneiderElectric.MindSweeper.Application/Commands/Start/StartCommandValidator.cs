using FluentValidation;

namespace SchneiderElectric.MindSweeper.Application.Commands.Start;

public class StartCommandValidator : AbstractValidator<StartCommand>
{
    public StartCommandValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty();
        RuleFor(x => x.Settings).SetValidator(new SettingsValidator());
    }
}
