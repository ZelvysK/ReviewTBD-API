using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class MediaController(IMediaService mediaService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<MediaDto>>> GetAllMedia([FromQuery] EntryQuery filters) {
        var entries = await mediaService.GetAllMediaAsync(filters);

        return Ok(entries);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MediaDto[]>> GetMediaById(Guid id) {
        var entry = await mediaService.GetMediaWithStudioByIdAsync(id);

        if (entry is null)
        {
            return NotFound();
        }

        return Ok(entry);
    }

    [HttpGet("Studio/{studioId}")]
    public async Task<ActionResult<IEnumerable<MediaDto>>> GetMediaByStudio([FromQuery] EntryQuery filters, Guid studioId) {
        var entries = await mediaService.GetMediaByStudioAsync(filters, studioId);

        return Ok(entries);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMedia([FromBody] MediaDto input) {
        var id = await mediaService.CreateMediaAsync(input);

        return CreatedAtAction(nameof(GetMediaById), new { id }, new { Message = "Media created successfully", Id = id });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMedia(Guid id) {
        var deleted = await mediaService.DeleteMediaAsync(id);

        return deleted ? NoContent() : NotFound();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MediaDto>> UpdateMedia(Guid id, [FromBody] MediaDto input) {
        var updated = await mediaService.UpdateMediaAsync(id, input);

        return updated is not null
            ? Ok(updated)
            : NotFound();
    }
}
