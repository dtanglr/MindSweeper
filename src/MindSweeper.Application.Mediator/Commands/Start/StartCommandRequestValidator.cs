using FluentValidation;

namespace MindSweeper.Application.Mediator.Commands.Start;

/// <summary>
/// Validator for the StartCommand class.
/// </summary>
public class StartCommandRequestValidator : AbstractValidator<StartCommandRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StartCommandRequestValidator"/> class.
    /// </summary>
    public StartCommandRequestValidator()
    {
        RuleFor(x => x.Settings).SetValidator(new GameSettingsValidator());
    }
}
