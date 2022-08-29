using System.Collections.Concurrent;
using RockScissorsPaper.Api.Contracts;
using RockScissorsPaper.Api.Dtos;

namespace RockScissorsPaper.Api.Repositories;

public class GameStateRepository : IGameStateRepository
{
    private readonly ILogger<GameStateRepository> _logger;
    private readonly ConcurrentDictionary<string, GameStateDto> _states;

    public GameStateRepository(ILogger<GameStateRepository> logger)
    {
        _logger = logger;
        _states = new ConcurrentDictionary<string, GameStateDto>();
    }
    public GameStateDto? Get(string id)
    {
        if (_states.TryGetValue(id, out var state))
        {
            return state;
        }

        _logger.LogWarning("State with guid: {Id} was not found in states", id);
        return null;
    }

    public void Save(GameStateDto state)
    {
        _logger.LogInformation("Saving state: {@State}", state);

        _states.AddOrUpdate(state.Id, (_) => state, (_, _) => state);
    }
}