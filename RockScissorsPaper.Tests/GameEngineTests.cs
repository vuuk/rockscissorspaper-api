using RockScissorsPaper.Api.Game;

namespace RockScissorsPaper.Tests;

public class GameEngineTests
{

    private const string PlayerOne = "PlayerOne";
    private const string PlayerTwo = "PlayerTwo";

    [Fact]
    public void SameMoves_EndsInDraw()
    {
        var state = PlayGame(Moves.Rock, Moves.Rock);
        Assert.Equal(GameEngine.ResultDraw, state.Winner);

        state = PlayGame(Moves.Scissors, Moves.Scissors);
        Assert.Equal(GameEngine.ResultDraw, state.Winner);

        state = PlayGame(Moves.Paper, Moves.Paper);
        Assert.Equal(GameEngine.ResultDraw, state.Winner);
    }

    [Fact]
    public void Rock_Beats_Scissors()
    {
        var state = PlayGame(Moves.Rock, Moves.Scissors);
        Assert.Equal(PlayerOne, state.Winner);
    }

    [Fact]
    public void Paper_Beats_Rock()
    {
        var state = PlayGame(Moves.Paper, Moves.Rock);
        Assert.Equal(PlayerOne, state.Winner);
    }

    [Fact]
    public void Scissors_Beats_Paper()
    {
        var state = PlayGame(Moves.Scissors, Moves.Paper);
        Assert.Equal(PlayerOne, state.Winner);
    }

    private static GameState PlayGame(Moves playerOne, Moves playerTwo)
    {
        var state = GameEngine.CreateGame(PlayerOne);
        var engine = new GameEngine(state);
        state = engine.AddPlayerTwo(PlayerTwo);
        engine = new GameEngine(state);
        state = engine.Move(PlayerOne, playerOne);
        engine = new GameEngine(state);

        return engine.Move(PlayerTwo, playerTwo);
    }
}