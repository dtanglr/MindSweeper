using System.CommandLine;

namespace SchneiderElectric.MindSweeper.Cli;

public interface IProgram
{
    abstract static CliRootCommand RootCommand { get; }
    abstract static CliCommand StartCommand { get; }
    abstract static CliCommand MoveCommand { get; }
    abstract static CliCommand EndCommand { get; }
    abstract static Task<int> Main(string[] args);
}
