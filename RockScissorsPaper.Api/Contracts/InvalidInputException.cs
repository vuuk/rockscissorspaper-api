namespace RockScissorsPaper.Api.Contracts;

public class InvalidGameInputException : Exception {
    public InvalidGameInputException(string message) : base(message)
    {
        
    }
}