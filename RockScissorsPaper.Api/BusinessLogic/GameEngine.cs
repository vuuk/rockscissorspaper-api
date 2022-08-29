using RockScissorsPaper.Api.Contracts;
using RockScissorsPaper.Api.Dtos;

namespace RockScissorsPaper.Api.BusinessLogic;

public class GameEngine : IGameEngine
{
    public const int MaxPlayerCount = 2;
    private readonly ILogger<GameEngine> _logger;

    public GameEngine(ILogger<GameEngine> logger)
    {
        _logger = logger;
    }

    public GameStateDto AddPlayer(GameStateDto state, string player)
    {
        if (state.Moves.Count > MaxPlayerCount)
        {
            throw new InvalidGameInputException("Game already has two players");
        }

        if (state.Moves.ContainsKey(player))
        {
            throw new InvalidGameInputException($"Game already has a player: {player}");
        }

        _logger.LogInformation("{Player} is joining game {gameId}", player, state.Id);

        state.Moves[player] = null;

        return state with {
            Message = $"Challenge accepted by {player}"
        };
    }

    public GameStateDto CreateGame(string player)
    {
        var guid = Guid.NewGuid().ToString("N");

        _logger.LogInformation("New game created for player: {Player} with id: {Guid}", player, guid);
        
        return new GameStateDto(guid, new () { {player, null} }, $"{player} started the game");
    }

    public GameStateDto Move(GameStateDto state, MoveDto move)
    {
        if (state.Moves[move.player] is not null)
        {
            throw new InvalidGameInputException($"Player {move.player} has already made a move");
        }

        state.Moves[move.player] = move.move;

        return state with {
            Message = state.Moves.Any(p => p.Value is null) ? $"Waiting on {move.player}" : DetermineWinner(state)
        };
    }

    private static string DetermineWinner(GameStateDto state)
    {
        if (state.Moves.Count != MaxPlayerCount)
        {
            throw new InvalidOperationException("More players than two was allowed to play");
        }

        var playerOne = state.Moves.First();
        var playerTwo = state.Moves.Last();

        var result = (playerOne.Value, playerTwo.Value) switch
        {
            (Moves.Rock, Moves.Scissors) => $"{playerOne.Key} wins!",
            (Moves.Paper, Moves.Rock) => $"{playerOne.Key} wins!",
            (Moves.Scissors, Moves.Paper) => $"{playerOne.Key} wins!",
            (Moves.Rock, Moves.Paper) => $"{playerTwo.Key} wins!",
            (Moves.Paper, Moves.Scissors) => $"{playerTwo.Key} wins!",
            (Moves.Scissors, Moves.Rock) => $"{playerTwo.Key} wins!",
            (_, _) => "It's a draw"
        };

        return result;
    }
}