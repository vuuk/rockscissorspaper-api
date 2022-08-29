using RockScissorsPaper.Api.Dtos;

namespace RockScissorsPaper.Api.Contracts;

public interface IGameEngine
{
    GameStateDto MakeState(GameCreationRequestDto creationDto);
}