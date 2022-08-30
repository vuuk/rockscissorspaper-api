namespace RockScissorsPaper.Api.Game;
public record struct GameState((string Player, Moves? Move) PlayerOneMove, (string? Player, Moves? Move) PlayerTwoMove, string? Winner);