using System.Text.Json;

namespace MindSweeper.Persistence.LocalFile;

/// <summary>
/// Represents the options for JSON file operations.
/// </summary>
public sealed class JsonFileOptions
{
    /// <summary>
    /// Gets or sets the options for JSON serialization and deserialization.
    /// </summary>
    public Action<JsonSerializerOptions>? JsonSerializerOptions { get; set; }
}
