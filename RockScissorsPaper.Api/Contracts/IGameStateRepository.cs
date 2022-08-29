using RockScissorsPaper.Api.Dtos;

namespace RockScissorsPaper.Api.Contracts;

public interface IGameStateRepository
{
    GameStateDto? Get(string guid);
    void Save(GameStateDto state);
}