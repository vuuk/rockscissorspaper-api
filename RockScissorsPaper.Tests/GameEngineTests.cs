using RockScissorsPaper.Api.BusinessLogic;
using RockScissorsPaper.Api.Dtos;
using RockScissorsPaper.Api.Contracts;

namespace RockScissorsPaper.Tests;

public class GameEngineTests
{

    private const string PlayerOne = "PlayerOne";
    private const string PlayerTwo = "PlayerTwo";
    [Fact]
    public void SameMoves_EndsInDraw()
    {
        var engine = new GameEngine(null);
        
        var state = MakeDefaultGame();

        state = engine.Move(state, new MoveDto(PlayerOne, Moves.Rock));
        state = engine.Move(state, new MoveDto(PlayerTwo, Moves.Rock));
    }


    private static GameStateDto MakeDefaultGame()
    {
        var engine = new GameEngine();
        var state = engine.CreateGame(PlayerOne);

        return engine.AddPlayer(PlayerTwo);
    }
}