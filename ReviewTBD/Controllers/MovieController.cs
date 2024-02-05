using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController(IMovieService movieService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<MovieDto[]>> GetAllMovies([FromQuery] EntryQuery filters) {
        var entries = await movieService.GetAllMoviesAsync(filters);

        return Ok(entries);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto[]>> GetMoviesById(Guid id) {
        var entry = await movieService.GetMovieWithStudioByIdAsync(id);

        if (entry is null)
        {
            return NotFound();
        }

        return Ok(entry);
    }

    [HttpGet("MovieStudio/{studioId}")]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMoviesByStudio([FromQuery] EntryQuery filters,Guid studioId) {
        var entries = await movieService.GetMoviesByStudioAsync(filters, studioId);

        return Ok(entries);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovie([FromBody] MovieDto input) {

        var id = await movieService.CreateMovieAsync(input);

        return CreatedAtAction(nameof(GetMoviesById), new { id }, new { Message = "Movie created successfully", Id = id });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMovie(Guid id) {
        var deleted = await movieService.DeleteMovieAsync(id);

        return deleted ? NoContent() : NotFound();

    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MovieDto>> UpdateMovie(Guid id, [FromBody] MovieDto input) {

        var updated = await movieService.UpdateMovieAsync(id, input);

        return updated is not null
            ? Ok(updated)
            : NotFound();
    }
}
