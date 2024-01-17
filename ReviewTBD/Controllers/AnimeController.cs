using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimeController(IAnimeService animeService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<AnimeDto[]>> GetAllAnimes() {
        var entries = await animeService.GetAllAnimesAsync();

        return Ok(entries);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AnimeDto[]>> GetAnimeById(Guid id) {
        var entry = await animeService.GetAnimeWithStudioByIdAsync(id);

        if (entry is null)
        {
            return NotFound();
        }

        return Ok(entry);
    }
}
