using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Filters;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class StudioController(IStudioService studioService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<StudioDto>>> GetAllStudios(
        [FromQuery] StudioQuery filters
    )
    {
        var result = await studioService.GetAllStudiosAsync(filters);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<StudioDto>> GetStudioById(Guid id)
    {
        var entry = await studioService.GetStudioByIdAsync(id);

        return entry is not null ? Ok(entry) : NotFound();
    }

    [Admin]
    [HttpPost]
    public async Task<IActionResult> CreateStudio([FromBody] StudioDto input)
    {
        var id = await studioService.CreateStudioAsync(input);

        return CreatedAtAction(
            nameof(GetStudioById),
            new { id },
            new { Message = "Studio created successfully", Id = id }
        );
    }

    [Admin]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteStudio(Guid id)
    {
        var deleted = await studioService.DeleteStudioAsync(id);

        return deleted ? NoContent() : NotFound();
    }

    [Admin]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<StudioDto>> UpdateStudio(Guid id, [FromBody] StudioDto input)
    {
        var updated = await studioService.UpdateStudioAsync(id, input);

        return updated is not null ? Ok(updated) : NotFound();
    }
}
