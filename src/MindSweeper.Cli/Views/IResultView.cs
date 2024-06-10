using MindSweeper.Domain.Results;

namespace MindSweeper.Cli.Views;

/// <summary>
/// Represents a view for rendering the result of a game.
/// </summary>
internal interface IResultView
{
    /// <summary>
    /// Renders the specified result.
    /// </summary>
    /// <param name="result">The result to be rendered.</param>
    void Render(Result result);
}

/// <summary>
/// Represents a view for rendering the result of a generic operation.
/// </summary>
/// <typeparam name="TResponse">The type of the result.</typeparam>
internal interface IResultView<TResponse> where TResponse : class
{
    /// <summary>
    /// Renders the specified result.
    /// </summary>
    /// <param name="result">The result to render.</param>
    void Render(Result<TResponse> result);
}

/// <summary>
/// Represents a view for rendering the result of a generic operation.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the result.</typeparam>
internal interface IResultView<TRequest, TResponse>
    where TRequest : class
    where TResponse : class
{
    /// <summary>
    /// Renders the specified result.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="result">The result to render.</param>
    void Render(TRequest request, Result<TResponse> result);
}

/// <summary>
/// Represents a view for rendering the result of a game.
/// </summary>
internal interface IParseResultView
{
    /// <summary>
    /// Renders the specified result.
    /// </summary>
    /// <param name="result">The parse result to be rendered.</param>
    void Render(ParseResult result);
}

/// <summary>
/// Represents a view for rendering the result of a game.
/// </summary>
internal interface IGameView
{
    /// <summary>
    /// Renders the specified game result.
    /// </summary>
    /// <param name="result">The game result to be rendered.</param>
    void Render(Game result);
}
