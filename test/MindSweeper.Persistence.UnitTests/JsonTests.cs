using System.Text.Json;
using MindSweeper.Domain;

namespace MindSweeper.Persistence.UnitTests;

public class JsonTests
{
    [Fact]
    public void Deserialize_Game_Json()
    {
        // Assign
        var json = "[{ \"Id\":\"d3e0bc61-d5d7-4733-9875-c60a61775578\",\"PlayerId\":\"ABC123\",\"Settings\":{ \"Columns\":8,\"Rows\":8,\"Bombs\":12,\"Lives\":3},\"Bombs\":[3,40,8,60,9,38,29,46,41,22,37,50],\"CurrentSquare\":\"E1\",\"AvailableMoves\":{ \"Up\":\"E2\",\"Left\":\"D1\",\"Right\":\"F1\"} }]";

        // Act
        var games = JsonSerializer.Deserialize<List<Game>>(json);
        games.Should().NotBeNull();
        games!.Count.Should().Be(1);

        var game = games[0];
        game.Id.Should().Be(Guid.Parse("d3e0bc61-d5d7-4733-9875-c60a61775578"));
    }
}
