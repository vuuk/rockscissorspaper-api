namespace RockScissorsPaper.Api.Contracts;

public sealed record ValidationFailure(string Message): IValidationResult;