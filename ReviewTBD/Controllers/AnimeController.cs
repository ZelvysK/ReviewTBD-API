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
    public async Task<ActionResult<PaginatedResult<AnimeDto>>> GetAllAnimes([FromQuery] EntryQuery filters) {
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

    [HttpGet("AnimeStudio/{animeStudioId}")]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetAnimeByStudio([FromQuery] EntryQuery filters, Guid animeStudioId) {
        var entries = await animeService.GetAnimeByStudioAsync(filters, animeStudioId);

        return Ok(entries);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAnime([FromBody] AnimeDto input) {

        var id = await animeService.CreateAnimeAsync(input);

        return CreatedAtAction(nameof(GetAnimeById), new { id }, new { Message = "Anime created successfully", Id = id });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAnime(Guid id) {
        var deleted = await animeService.DeleteAnimeAsync(id);

        return deleted ? NoContent() : NotFound();

    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<AnimeDto>> UpdateAnime(Guid id, [FromBody] AnimeDto input) {

        var updated = await animeService.UpdateAnimeAsync(id, input);

        return updated is not null
            ? Ok(updated)
            : NotFound();
    }
}
