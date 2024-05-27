using FluentValidation;

namespace MindSweeper.Application.Commands.End;

/// <summary>
/// Validator for the EndCommand class.
/// </summary>
public class EndCommandValidator : AbstractValidator<EndCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EndCommandValidator"/> class.
    /// </summary>
    public EndCommandValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty();
    }
}
