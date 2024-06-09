namespace MindSweeper.Cli.Commands.Start;

/// <summary>
/// Represents an option for specifying the number of bombs in the game.
/// </summary>
internal class BombsOption : CliOption<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BombsOption"/> class.
    /// </summary>
    public BombsOption() : base("--bombs", "-b")
    {
        Arity = ArgumentArity.ExactlyOne;
        Description = Resources.StartCommandBombsOptionDescription;
        HelpName = Resources.StartCommandBombsOptionHelpName;
        Required = false;
        Validators.Add((result) =>
        {
            var value = result.GetValueOrDefault<int>();

            if (value < GameSettings.MinimumBombs)
            {
                result.AddError(Resources.CommandOptionLessThanMinimum, result.IdentifierToken, GameSettings.MinimumBombs, value);
            }
        });
    }
}
