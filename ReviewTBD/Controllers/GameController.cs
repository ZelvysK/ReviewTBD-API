using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController(IGameService gameService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<GameDto[]>> GetAllGames() {
        var entries = await gameService.GetAllGamesAsync();

        return Ok(entries);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GameDto[]>> GetGamesById(Guid id) {
        var entry = await gameService.GetGameWithCreatorByIdAsync(id);

        if (entry is null)
        {
            return NotFound();
        }

        return Ok(entry);
    }

    [HttpGet("GameCreator/{creatorId}")]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGamesByCreator(Guid creatorId) {
        var entries = await gameService.GetGamesByCreatorAsync(creatorId);

        return Ok(entries);
    }
}
