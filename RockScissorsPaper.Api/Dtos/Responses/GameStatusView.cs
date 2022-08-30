using System.Text.Json.Serialization;

namespace RockScissorsPaper.Api.Dtos.Responses;

public record GameStatusView 
{
    public GameStatusView(string playerOne, string? playerTwo)
    {
        PlayerOne = playerOne;
        PlayerTwo = playerTwo;
    }
    [JsonPropertyName("playerOne")]
    public string PlayerOne {get;}
    [JsonPropertyName("playerTwo")]
    public string? PlayerTwo {get;}
}
