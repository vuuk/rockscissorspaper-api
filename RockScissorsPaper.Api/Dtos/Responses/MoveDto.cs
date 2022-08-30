using RockScissorsPaper.Api.Game;
using System.Text.Json.Serialization;

namespace RockScissorsPaper.Api.Dtos.Responses;

public record MoveDto
{
    public MoveDto(string player, Moves move)
    {
        Player = player;
        Move = move;
    }
    [JsonPropertyName("player")]
    public string Player { get; }
    [JsonPropertyName("move")]
    public Moves Move { get; }
}
