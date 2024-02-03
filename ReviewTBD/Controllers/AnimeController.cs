using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimeController(IAnimeService animeService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<AnimeDto[]>> GetAllAnimes([FromQuery] EntryQuery filters) {
        var entries = await animeService.GetAllAnimesAsync(filters);

        return Ok(entries);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AnimeDto[]>> GetAnimeById(Guid id) {
        var entry = await animeService.GetAnimeWithStudioByIdAsync(id);

        if (entry is null)
        {
            return NotFound();
        }

        return Ok(entry);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudio([FromBody] AnimeDto input) {

        var id = await animeService.CreateAnimeAsync(input);

        return CreatedAtAction(nameof(GetAnimeById), new { id }, new { Message = "Anime created successfully", Id = id });
    }
}
