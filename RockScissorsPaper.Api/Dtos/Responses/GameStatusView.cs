using System.Text.Json.Serialization;

namespace RockScissorsPaper.Api.Dtos.Responses;

public record GameStatusView 
{
    public GameStatusView(string playerOne, string? playerTwo, string message)
    {
        PlayerOne = playerOne;
        PlayerTwo = playerTwo;
        Message = message;
    }
    [JsonPropertyName("playerOne")]
    public string PlayerOne {get;}
    [JsonPropertyName("playerTwo")]
    public string? PlayerTwo {get;}
    [JsonPropertyName("message")]
    public string Message {get;}
}
