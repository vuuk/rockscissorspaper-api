namespace RockScissorsPaper.Api.Game;

public class GameEngine
{
    private GameState _state;
    public const string ResultDraw = "Draw";
    public GameEngine(GameState state)
    {
        _state = state;
    }
    public GameState AddPlayerTwo(string player)
    {
        if (_state!.PlayerTwoMove.Player is not null)
        {
            throw new InvalidGameInputException("Game already has two players");
        }

        if (_state.PlayerTwoMove.Player == player)
        {
            throw new InvalidGameInputException($"Game already has a player with the name: {player}");
        }
        return _state with
        {
            PlayerTwoMove = (player, null)
        };
    }

    public static GameState CreateGame(string player) => new((player, null), (null, null),  null);

    public GameState Move(string player, Moves move)
    {
        if (_state.Winner is not null)
        {
            throw new InvalidGameInputException("Game is already over");
        }
        if (IsNotAPlayer(player))
        {
            throw new InvalidGameInputException($"Player {player} is not a member of the game");
        }

        if (PlayerHasAlreadyMadeAmove(player))
        {
            throw new InvalidGameInputException($"Player {player} has already made a move");
        }

        var newState = MakeMove(player, move);

        var winner = GetWinner(newState);

        return newState with
        {
            Winner = winner
        };
    }

    private GameState MakeMove(string player, Moves move)
    {
        var state = IsPlayerOne(player) ? _state with
        {
            PlayerOneMove = (player, move)
        } : _state with
        {
            PlayerTwoMove = (player, move)
        };

        return state;
    }

    private bool IsNotAPlayer(string player)
    {
        return player != _state.PlayerOneMove.Player && player != _state.PlayerTwoMove.Player;
    }

    private bool PlayerHasAlreadyMadeAmove(string player) => IsPlayerOne(player) ? _state.PlayerOneMove.Move is not null : _state.PlayerTwoMove.Move is not null;

    private bool IsPlayerOne(string player)
    {
        return _state.PlayerOneMove.Player == player;
    }
    private static string? GetWinner(GameState state)
    {
        if (!GameOver(state)) return null;

        var winner = (state.PlayerOneMove.Move, state.PlayerTwoMove.Move) switch
        {
            (Moves.Rock, Moves.Scissors) => state.PlayerOneMove.Player,
            (Moves.Paper, Moves.Rock) => state.PlayerOneMove.Player,
            (Moves.Scissors, Moves.Paper) => state.PlayerOneMove.Player,
            (Moves.Rock, Moves.Paper) => state.PlayerTwoMove!.Player,
            (Moves.Paper, Moves.Scissors) => state.PlayerTwoMove.Player,
            (Moves.Scissors, Moves.Rock) => state.PlayerTwoMove.Player,
            (_, _) => ResultDraw
        };

        return winner;
    }
    private static bool GameOver(GameState state) => state.PlayerOneMove.Move is not null && state.PlayerTwoMove.Move is not null;

}