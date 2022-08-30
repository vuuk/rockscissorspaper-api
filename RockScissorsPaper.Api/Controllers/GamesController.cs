using Microsoft.AspNetCore.Mvc;
using RockScissorsPaper.Api.Game;
using RockScissorsPaper.Api.Dtos.Requests;
using RockScissorsPaper.Api.Dtos.Responses;
using RockScissorsPaper.Api.DataAccess;

namespace RockScissorsPaper.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    private readonly ILogger<GamesController> _logger;
    private readonly IGameStateRepository _repository;

    public GamesController(ILogger<GamesController> logger, IGameStateRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet("{id}")]
    public IActionResult Index(string id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return new BadRequestObjectResult("id cannot be empty");
            }

            var state = _repository.Get(id);

            return new OkObjectResult(state.Winner is null ? state.ToStatusView() : state.ToResultView());
        }
        catch (DataAccessException e)
        {
            return new BadRequestObjectResult(e.Message);
        }
    }

    [HttpPost]
    public IActionResult Index([FromBody] GameCreationRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Player))
        {
            return new BadRequestObjectResult($"Field: {nameof(request.Player)} is required");
        }

        var state = GameEngine.CreateGame(request.Player);

        var id = _repository.Save(state);

        return new OkObjectResult(id);
    }

    [HttpPost("{id}/Join")]
    public IActionResult Join(string id, [FromBody] JoinGameRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Player))
            {
                return new BadRequestObjectResult($"Field: {nameof(request.Player)} is required");
            }

            var state = _repository.Get(id);
            var engine = new GameEngine(state);
            var newState = engine.AddPlayerTwo(request.Player);

            _repository.Update(id, state, newState);

            return new OkObjectResult(state.ToStatusView());
        }
        catch (InvalidGameInputException e)
        {
            _logger.LogWarning(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
        catch (DataAccessException e)
        {
            _logger.LogWarning(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }

    [HttpPost("{id}/Move")]
    public IActionResult Move(string id, [FromBody] MoveRequestDto request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Player))
            {
                return new BadRequestObjectResult($"Field: {nameof(request.Player)} is required");
            }

            if (!request.Move.HasValue)
            {
                return new BadRequestObjectResult($"Field: {nameof(request.Move)} is required");
            }

            var state = _repository.Get(id);

            var engine = new GameEngine(state);
            var newState = engine.Move(request.Player, request.Move.Value);

            _repository.Update(id, state, newState);

            return new OkObjectResult(newState.Winner is not null ? newState.ToResultView() : newState.ToStatusView());
        }
        catch (InvalidGameInputException e)
        {
            _logger.LogWarning(e.Message);
            return new BadRequestObjectResult(e.Message);
        }
    }
}
