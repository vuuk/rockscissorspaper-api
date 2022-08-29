using Microsoft.AspNetCore.Mvc;
using RockScissorsPaper.Api.Contracts;
using RockScissorsPaper.Api.Dtos;

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
    private readonly IGameRepository _repository;

    public GamesController(ILogger<GamesController> logger, IInputValidator validator, IGameEngine engine, IGameRepository repository)
    {
        _logger = logger;
        _validator = validator;
        _engine = engine;
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Index([FromBody] GameCreationRequestDto request)
    {
       var result = _validator.Validate(request);

       if (result is ValidationFailure f)
       {
            return new BadRequestObjectResult(f.Message);
       }

       var state = _engine.MakeState(request);

       await _repository.SaveGameStateAsync(state);

       return new OkObjectResult(state.Guid);
    }
}
