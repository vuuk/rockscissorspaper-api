using RockScissorsPaper.Api.Dtos;

namespace RockScissorsPaper.Api.Contracts;

public interface IGameRepository
{
    Task<GameStateDto> GetGameStateAsync(string guid);
    Task SaveGameState(GameStateDto state);
}