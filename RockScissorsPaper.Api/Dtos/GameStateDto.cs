namespace RockScissorsPaper.Api.Dtos;

public sealed record GameStateDto(string Guid, string[] Players, GameState State, string Description);