using Microsoft.AspNetCore.Mvc;
using RockScissorsPaper.Api.Contracts;
using RockScissorsPaper.Api.Dtos;
using RockScissorsPaper.Api.BusinessLogic;

namespace RockScissorsPaper.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<GamesController> _logger;
    private readonly IInputValidator _validator;
    private readonly IGameEngine _engine;
    private readonly IGameStateRepository _repository;

    public GamesController(ILogger<GamesController> logger, IInputValidator validator, IGameEngine engine, IGameStateRepository repository)
    {
        _logger = logger;
        _validator = validator;
        _engine = engine;
        _repository = repository;
    }

    [HttpGet("{id}")]
    public IActionResult Index(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return new BadRequestObjectResult("id cannot be empty");
        }

    var state = _repository.Get(id);

    if (state is null)
    {
        return new BadRequestObjectResult($"Game not found: {id}");
    }

       return new OkObjectResult(state);
    }

    [HttpPost]
    public IActionResult Index([FromBody] GameCreationRequestDto request)
    {
       var result = _validator.Validate(request);

       if (result is ValidationFailure f)
       {
            return new BadRequestObjectResult(f.Message);
       }

       var state = _engine.CreateGame(request.Player);

        _repository.Save(state);

       return new OkObjectResult(state);
    }

    [HttpPost("{id}/Join")]
    public IActionResult Join(string id, [FromBody] JoinGameRequest request)
    {
        try
        {
            var result = _validator.Validate(request);

            if (result is ValidationFailure f)
            {
                return new BadRequestObjectResult(f.Message);
            }

            var state = _repository.Get(id);

            if (state is null)
            {
                return new BadRequestObjectResult("Game not found");
            }

            var newState = _engine.AddPlayer(state, request.Player);

            _repository.Save(newState);

            return new OkObjectResult(newState);
        }
        catch (InvalidGameInputException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }

    [HttpPost("{id}/Move")]
    public ActionResult<GameStateDto> Move(string id, [FromBody] MoveRequestDto request)
    {
        try 
        {
            var result = _validator.Validate(request);

            if (result is ValidationFailure f)
            {
                return new BadRequestObjectResult(f.Message);
            }

            var state = _repository.Get(id);

            if (state is null)
            {
                return new BadRequestObjectResult("Game not found");
            }

            var newState = _engine.Move(state, move: new MoveDto(request.Player, request.Move));

            _repository.Save(newState);

            return newState;
        }
        catch (InvalidGameInputException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }
}
