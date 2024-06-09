namespace MindSweeper.Cli.Commands.Start;

/// <summary>
/// Represents an option for specifying the number of columns in the game.
/// </summary>
public class ColumnsOption : CliOption<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ColumnsOption"/> class.
    /// </summary>
    public ColumnsOption() : base("--columns", "-c")
    {
        Arity = ArgumentArity.ExactlyOne;
        Description = Resources.StartCommandColumnsOptionDescription;
        HelpName = Resources.StartCommandColumnsOptionHelpName;
        Required = false;
        DefaultValueFactory = (arg) => GameSettings.DefaultColumns;
        Validators.Add((result) =>
        {
            var value = result.GetValueOrDefault<int>();

            if (value < GameSettings.MinimumColumns)
            {
                result.AddError(Resources.CommandOptionLessThanMinimum, result.IdentifierToken, GameSettings.MinimumColumns, value);
            }
        });
        Validators.Add((result) =>
        {
            var value = result.GetValueOrDefault<int>();

            if (value > GameSettings.MaximumColumns)
            {
                result.AddError(Resources.CommandOptionGreaterThanMaximum, result.IdentifierToken, GameSettings.MaximumColumns, value);
            }
        });
    }
}
