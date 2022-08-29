using RockScissorsPaper.Api.Contracts;
using RockScissorsPaper.Api.Dtos;

namespace RockScissorsPaper.Api.Validators;

public class GameValidator : IInputValidator
{
    public IValidationResult Validate(MoveRequestDto move)
    {
        if (string.IsNullOrWhiteSpace(move.Player))
        {
            return new Failure($"Field: {nameof(MoveRequestDto.Player)} is required");
        }
        
        if (string.IsNullOrWhiteSpace(move.Move))
        {
            return new Failure($"Field: {nameof(MoveRequestDto.Move)} is required");
        }

        return new Success();
    }

    public IValidationResult Validate(GameCreationRequestDto game)
    {
        if (string.IsNullOrWhiteSpace(game.Player))
        {
            return new Failure($"Field: {nameof(GameCreationRequestDto.Player)} is required");
        }

        return new Success();
    }
}