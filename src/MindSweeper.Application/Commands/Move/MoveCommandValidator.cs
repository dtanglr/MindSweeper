using FluentValidation;

namespace MindSweeper.Application.Commands.Move;

public class MoveCommandValidator : AbstractValidator<MoveCommand>
{
    public MoveCommandValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty();
    }
}
