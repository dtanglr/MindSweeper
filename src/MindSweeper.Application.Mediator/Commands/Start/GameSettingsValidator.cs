using FluentValidation;
using MindSweeper.Domain;

namespace MindSweeper.Application.Mediator.Commands.Start;

/// <summary>
/// Validator for the GameSettings class.
/// </summary>
public class GameSettingsValidator : AbstractValidator<GameSettings>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GameSettingsValidator"/> class.
    /// </summary>
    public GameSettingsValidator()
    {
        RuleFor(x => x.Rows).GreaterThanOrEqualTo(GameSettings.MinimumRows);
        RuleFor(x => x.Rows).LessThanOrEqualTo(GameSettings.MaximumRows);
        RuleFor(x => x.Columns).GreaterThanOrEqualTo(GameSettings.MinimumColumns);
        RuleFor(x => x.Columns).LessThanOrEqualTo(GameSettings.MaximumColumns);
        RuleFor(x => x.Bombs).GreaterThanOrEqualTo(GameSettings.MinimumBombs);
        RuleFor(x => x.Bombs).LessThanOrEqualTo((x) => x.Squares);
        RuleFor(x => x.Lives).GreaterThanOrEqualTo(GameSettings.MinimumLives);
    }
}
