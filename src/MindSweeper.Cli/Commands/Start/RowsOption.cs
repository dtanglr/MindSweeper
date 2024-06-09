namespace MindSweeper.Cli.Commands.Start;

/// <summary>
/// Represents an option for specifying the number of rows in the game.
/// </summary>
internal class RowsOption : CliOption<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RowsOption"/> class.
    /// </summary>
    public RowsOption() : base("--rows", "-r")
    {
        Arity = ArgumentArity.ExactlyOne;
        Description = Resources.StartCommandRowsOptionDescription;
        HelpName = Resources.StartCommandRowsOptionHelpName;
        Required = false;
        DefaultValueFactory = (arg) => GameSettings.DefaultRows;
        Validators.Add((result) =>
        {
            var value = result.GetValueOrDefault<int>();

            if (value < GameSettings.MinimumRows)
            {
                result.AddError(Resources.CommandOptionLessThanMinimum, result.IdentifierToken, GameSettings.MinimumRows, value);
            }
        });
        Validators.Add((result) =>
        {
            var value = result.GetValueOrDefault<int>();

            if (value > GameSettings.MaximumRows)
            {
                result.AddError(Resources.CommandOptionGreaterThanMaximum, result.IdentifierToken, GameSettings.MaximumRows, value);
            }
        });
    }
}
