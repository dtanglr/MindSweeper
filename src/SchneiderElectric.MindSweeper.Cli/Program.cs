using System.CommandLine;
using SchneiderElectric.MindSweeper.Moves;

namespace SchneiderElectric.MindSweeper.Cli;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var rootCommand = new RootCommand("A game to sweep the mind of it's creator by the players :-P");

        var startGameCommand = new Command("start", "Start a new game");
        startGameCommand.SetHandler(() =>
        {
            var startSquare = "D1";
            Console.WriteLine("You have started a new mind game!");
            Console.WriteLine($"Your current square is: {startSquare}.");
            Console.WriteLine($"");
        });

        var directionArgument = new Argument<Move>("direction")
        {
            Arity = ArgumentArity.ExactlyOne,
            Completions = { "up", "down", "left", "right" },
            Description = "Choices can be either 'up', 'down', 'left' or 'right' but are dependent on your current square.",
            HelpName = "direction"
        };

        var moveCommand = new Command("move", "Move to a square");
        moveCommand.AddArgument(directionArgument);
        moveCommand.SetHandler((direction) =>
        {
            Console.WriteLine($"Moving {direction}...");
        }, directionArgument);

        rootCommand.AddCommand(startGameCommand);
        rootCommand.AddCommand(moveCommand);
        rootCommand.SetHandler(() =>
        {
            Console.WriteLine("Welcome to this mind game! To start a new game, type:");
            Console.WriteLine("mindgame start");
        });

        await rootCommand.InvokeAsync(args);
    }
}
