using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GameStudioController(IGameStudioService gameStudioService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<GameStudioDto[]>> GetAllGameStudios() {
        var entries = await gameStudioService.GetAllGameStudiosAsync();

        return Ok(entries);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GameStudioDto[]>> GetGameStudioById(Guid id) {
        var entry = await gameStudioService.GetGameStudioByIdAsync(id);

        if (entry is null)
        {
            return NotFound();
        }

        return Ok(entry);
    }
}
