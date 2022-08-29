using RockScissorsPaper.Api.Dtos;
namespace RockScissorsPaper.Api.Contracts;

public interface IInputValidator
{
    IValidationResult Validate(MoveRequestDto request);
    IValidationResult Validate(GameCreationRequestDto request);
    IValidationResult Validate(JoinGameRequest request);
}