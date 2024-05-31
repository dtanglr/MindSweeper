using System.Text.Json.Serialization;

namespace MindSweeper.Domain;

/// <summary>
/// Represents the possible directions in a game.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Direction
{
    /// <summary>
    /// Up direction.
    /// </summary>
    Up,

    /// <summary>
    /// Right direction.
    /// </summary>
    Right,

    /// <summary>
    /// Down direction.
    /// </summary>
    Down,

    /// <summary>
    /// Left direction.
    /// </summary>
    Left
}
