using System.Text.Json.Serialization;

namespace RockScissorsPaper.Api.Dtos.Responses;

public record GameResultView
{
    public GameResultView(MoveDto[] moves, string winner)
    {
        Moves = moves;
        Winner = winner;
    }
    [JsonPropertyName("moves")]
    public MoveDto[] Moves {get;}
    [JsonPropertyName("winner")]
    public string Winner {get;}
}