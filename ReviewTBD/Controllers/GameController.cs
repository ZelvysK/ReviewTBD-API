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

    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] GameDto input) {

        var id = await gameService.CreateGameAsync(input);

        return CreatedAtAction(nameof(GetGamesById), new { id }, new { Message = "Game created successfully", Id = id });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteGame(Guid id) {
        var deleted = await gameService.DeleteGameAsync(id);

        return deleted ? NoContent() : NotFound();

    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<GameDto>> UpdateGame(Guid id, [FromBody] GameDto input) {

        var updated = await gameService.UpdateGameAsync(id, input);

        return updated is not null
            ? Ok(updated)
            : NotFound();
    }
}
