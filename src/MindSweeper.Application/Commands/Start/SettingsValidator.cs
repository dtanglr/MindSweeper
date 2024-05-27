using FluentValidation;
using MindSweeper.Domain;

namespace MindSweeper.Application.Commands.Start;

/// <summary>
/// Validator for the Settings class.
/// </summary>
public class SettingsValidator : AbstractValidator<Settings>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsValidator"/> class.
    /// </summary>
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
