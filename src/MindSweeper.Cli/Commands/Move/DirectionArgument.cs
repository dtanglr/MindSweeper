namespace MindSweeper.Cli.Commands.Move;

/// <summary>
/// Represents a command line argument for specifying the direction.
/// </summary>
public class DirectionArgument : CliArgument<Direction>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DirectionArgument"/> class.
    /// </summary>
    public DirectionArgument() : base("direction")
    {
        Arity = ArgumentArity.ExactlyOne;
        Description = Resources.MoveCommandDirectionArgumentDescription;
        HelpName = Resources.MoveCommandDirectionArgumentHelpName;
    }
}
