using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimeStudioController(IAnimeStudioService animeStudioService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<AnimeStudioDto[]>> GetAllAnimeStudios() {
        var entries = await animeStudioService.GetAllAnimeStudiosAsync();

        return Ok(entries);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AnimeStudioDto[]>> GetAnimeStudioById(Guid id) {
        var entry = await animeStudioService.GetAnimeStudioByIdAsync(id);

        if (entry is null)
        {
            return NotFound();
        }

        return Ok(entry);
    }
}
