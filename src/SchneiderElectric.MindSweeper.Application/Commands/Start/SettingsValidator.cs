using FluentValidation;
using SchneiderElectric.MindSweeper.Domain;

namespace SchneiderElectric.MindSweeper.Application.Commands.Start;

public class SettingsValidator : AbstractValidator<Settings>
{
    public SettingsValidator()
    {
        RuleFor(x => x.Rows).GreaterThanOrEqualTo(Settings.MinimumRows);
        RuleFor(x => x.Rows).LessThanOrEqualTo(Settings.MaximumRows);
        RuleFor(x => x.Columns).GreaterThanOrEqualTo(Settings.MinimumColumns);
        RuleFor(x => x.Columns).LessThanOrEqualTo(Settings.MaximumColumns);
        RuleFor(x => x.Bombs).GreaterThanOrEqualTo(Settings.MinimumBombs);
        RuleFor(x => x.Bombs).LessThanOrEqualTo((x) => x.Squares);
        RuleFor(x => x.Lives).GreaterThanOrEqualTo(Settings.MinimumLives);
    }
}
