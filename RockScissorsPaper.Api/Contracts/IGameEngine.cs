using RockScissorsPaper.Api.Dtos;

namespace RockScissorsPaper.Api.Contracts;

public interface IGameEngine
{
    GameStateDto CreateGame(string player);
    GameStateDto AddPlayer(GameStateDto state, string player);
    GameStateDto Move(GameStateDto state, MoveDto move);
}