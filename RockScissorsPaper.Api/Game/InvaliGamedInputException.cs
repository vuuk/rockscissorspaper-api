namespace RockScissorsPaper.Api.Game;

public class InvalidGameInputException : Exception {
    public InvalidGameInputException(string message) : base(message)
    {
        
    }
}