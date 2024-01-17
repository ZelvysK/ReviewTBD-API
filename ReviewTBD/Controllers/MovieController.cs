using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController(IMovieService movieService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<MovieDto[]>> GetAllMovies() {
        var entries = await movieService.GetAllMoviesAsync();

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
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMoviesByStudio(Guid studioId) {
        var entries = await movieService.GetMoviesByStudioAsync(studioId);

        return Ok(entries);
    }
}
