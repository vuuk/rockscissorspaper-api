using RockScissorsPaper.Api.Game;
using System.Text.Json.Serialization;

namespace RockScissorsPaper.Api.Dtos.Requests;

public sealed record MoveRequestDto
{
    [JsonPropertyName("player")]
    public string? Player {get; init;}
    [JsonPropertyName("move")]
    public Moves? Move {get; init;}
}