using FluentValidation;

namespace MindSweeper.Application.Mediator.Commands.Start;

/// <summary>
/// Validator for the StartCommand class.
/// </summary>
public class StartCommandValidator : AbstractValidator<StartCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StartCommandValidator"/> class.
    /// </summary>
    public StartCommandValidator()
    {
        RuleFor(x => x.Settings).SetValidator(new SettingsValidator());
    }
}
