using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieStudioController(IMovieStudioService movieStudioService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<MovieStudioDto[]>> GetAllMovieStudios() {
        var entries = await movieStudioService.GetAllMovieStudiosAsync();

        return Ok(entries);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieStudioDto[]>> GetMovieStudioById(Guid id) {
        var entry = await movieStudioService.GetMovieStudioByIdAsync(id);

        if (entry is null)
        {
            return NotFound();
        }

        return Ok(entry);
    }
}
