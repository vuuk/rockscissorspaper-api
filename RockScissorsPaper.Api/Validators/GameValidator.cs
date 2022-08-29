using RockScissorsPaper.Api.Contracts;
using RockScissorsPaper.Api.Dtos;

namespace RockScissorsPaper.Api.Validators;

public class GameValidator : IInputValidator
{
    public IValidationResult Validate(MoveRequestDto move)
    {
        if (string.IsNullOrWhiteSpace(move.Player))
        {
            return new ValidationFailure($"Field: {nameof(MoveRequestDto.Player)} is required");
        }

        return new ValidationSuccess();
    }

    public IValidationResult Validate(GameCreationRequestDto game)
    {
        if (string.IsNullOrWhiteSpace(game.Player))
        {
            return new ValidationFailure($"Field: {nameof(GameCreationRequestDto.Player)} is required");
        }

        return new ValidationSuccess();
    }

    public IValidationResult Validate(JoinGameRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Player))
        {
            return new ValidationFailure($"Field: {nameof(JoinGameRequest.Player)} is required");
        }
        return new ValidationSuccess();
    }
}