using RockScissorsPaper.Api.Dtos;
namespace RockScissorsPaper.Api.Contracts;

public interface IInputValidator
{
    IValidationResult Validate(MoveRequestDto move);
    IValidationResult Validate(GameCreationRequestDto game);
}