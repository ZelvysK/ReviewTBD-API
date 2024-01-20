using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController(IGameService gameService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<GameDto[]>> GetAllGames([FromQuery] EntryQuery filters) {
        var entries = await gameService.GetAllGamesAsync(filters);

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
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGamesByCreator([FromQuery] EntryQuery filters, Guid creatorId) {
        var entries = await gameService.GetGamesByCreatorAsync(filters, creatorId);

        return Ok(entries);
    }
}
