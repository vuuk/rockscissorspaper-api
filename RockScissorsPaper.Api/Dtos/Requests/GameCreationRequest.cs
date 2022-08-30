using System.Text.Json.Serialization;

namespace RockScissorsPaper.Api.Dtos.Requests;

public sealed record GameCreationRequestDto
{
    [JsonPropertyName("player")]
    public string? Player {get; init;}
}