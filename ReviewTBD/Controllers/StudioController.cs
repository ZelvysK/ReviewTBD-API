using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class StudioController(IStudioService studioService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<StudioDto>>> GetAllStudios([FromQuery] StudioQuery filters) {
        var result = await studioService.GetAllStudiosAsync(filters);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<StudioDto[]>> GetStudioById(Guid id) {
        var entry = await studioService.GetStudioByIdAsync(id);

        return entry is not null
            ? Ok(entry)
            : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<StudioDto>> CreateStudio([FromBody] StudioDto data) {

        var studioDto = new StudioDto
        {
            Name = data.Name,
            Description = data.Description,
            DateCreated = data.DateCreated,
            ImageUrl = data.ImageUrl,
            Type = data.Type,
        };

        Guid newStudioId = await studioService.CreateStudioAsync(studioDto);

        return CreatedAtAction(nameof(GetStudioById), new { id = newStudioId }, new { Message = "Studio created successfully", Id = newStudioId });
    }

}
