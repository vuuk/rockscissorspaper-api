using System.Text.Json.Serialization;

namespace RockScissorsPaper.Api.Dtos.Requests;

public sealed record JoinGameRequest
{
    [JsonPropertyName("player")]
    public string? Player {get; init;}
}