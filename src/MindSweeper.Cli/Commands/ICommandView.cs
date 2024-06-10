namespace MindSweeper.Cli.Commands;

/// <summary>
/// Represents a command view.
/// </summary>
internal interface ICommandView
{
}

/// <summary>
/// Represents a command view with one argument.
/// </summary>
/// <typeparam name="T1">The type of the argument.</typeparam>
internal interface ICommandView<T1> : ICommandView
    where T1 : class
{
    /// <summary>
    /// Renders the command view with the specified argument.
    /// </summary>
    /// <param name="arg1">The argument.</param>
    void Render(T1 arg1);
}

/// <summary>
/// Represents a command view with two arguments.
/// </summary>
/// <typeparam name="T1">The type of the first argument.</typeparam>
/// <typeparam name="T2">The type of the second argument.</typeparam>
internal interface ICommandView<T1, T2> : ICommandView
    where T1 : class
    where T2 : class
{
    /// <summary>
    /// Renders the command view with the specified arguments.
    /// </summary>
    /// <param name="arg1">The first argument.</param>
    /// <param name="arg2">The second argument.</param>
    void Render(T1 arg1, T2 arg2);
}
