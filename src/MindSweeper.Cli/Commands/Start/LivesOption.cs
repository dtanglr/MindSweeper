namespace MindSweeper.Cli.Commands.Start;

/// <summary>
/// Represents an option for specifying the number of lives the player has in the game.
/// </summary>
public class LivesOption : CliOption<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LivesOption"/> class.
    /// </summary>
    public LivesOption() : base("--lives", "-l")
    {
        Arity = ArgumentArity.ExactlyOne;
        Description = Resources.StartCommandLivesOptionDescription;
        HelpName = Resources.StartCommandLivesOptionHelpName;
        Required = false;
        DefaultValueFactory = (arg) => GameSettings.DefaultLives;
        Validators.Add((result) =>
        {
            var value = result.GetValueOrDefault<int>();

            if (value < GameSettings.MinimumLives)
            {
                result.AddError(Resources.CommandOptionLessThanMinimum, result.IdentifierToken, GameSettings.MinimumLives, value);
            }
        });
    }
}
