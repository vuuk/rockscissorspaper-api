using System.Collections.Concurrent;
using RockScissorsPaper.Api.Game;

namespace RockScissorsPaper.Api.DataAccess;

public class GameStateRepository : IGameStateRepository
{
    private readonly ILogger<GameStateRepository> _logger;
    private readonly ConcurrentDictionary<string, GameState> _states;

    public GameStateRepository(ILogger<GameStateRepository> logger)
    {
        _logger = logger;
        _states = new ConcurrentDictionary<string, GameState>();
    }
    public GameState Get(string id)
    {
        if (_states.TryGetValue(id, out var state))
        {
            _logger.LogInformation("Getting state: {@State}", state);
            return state;
        }

        _logger.LogWarning("State with guid: {Id} was not found in states", id);
        throw new DataAccessException($"Game could not be found with id: {id} ");
    }

    public string Save(GameState state)
    {
        var id = Guid.NewGuid().ToString("N");

        _logger.LogInformation("Saving state: {@State}", state);

        if (!_states.TryAdd(id, state))
        {
            throw new DataAccessException("Failed to add state");
        }

        return id;
    }

    public void Update(string id, GameState oldState, GameState newState)
    {
        _logger.LogInformation("Updating state: {@OldDate} ==> {@NewState}", oldState, newState);
        if (!_states.TryUpdate(id, newState, oldState))
        {
            throw new DataAccessException("The state was already changed by another task");
        }
    }
}