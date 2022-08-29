namespace RockScissorsPaper.Api.Dtos;

public sealed record GameStateDto(string Id, Dictionary<string, Moves?> Moves, string Message);