using FluentValidation;

namespace MindSweeper.Application.Commands.Move;

/// <summary>
/// Validator for the MoveCommand class.
/// </summary>
public class MoveCommandValidator : AbstractValidator<MoveCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCommandValidator"/> class.
    /// </summary>
    public MoveCommandValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty();
    }
}
