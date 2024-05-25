using FluentValidation;

namespace MindSweeper.Application.Commands.End;

public class EndCommandValidator : AbstractValidator<EndCommand>
{
    public EndCommandValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty();
    }
}
