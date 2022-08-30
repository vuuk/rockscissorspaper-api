using RockScissorsPaper.Api.Game;
namespace RockScissorsPaper.Api.DataAccess;

public interface IGameStateRepository
{
    GameState Get(string id);
    string Save(GameState state);
    void Update(string id, GameState oldState, GameState newState);
}