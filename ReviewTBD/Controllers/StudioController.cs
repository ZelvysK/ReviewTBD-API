using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Enums;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class StudioController(IStudioService studioService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<StudioDto[]>> GetAllStudios() {
        var entries = await studioService.GetAllStudiosAsync();

        return Ok(entries);
    }

    [HttpGet("Type")]
    public async Task<ActionResult<StudioDto[]>> GetAllStudiosByType([FromQuery] StudioType studioType) {
        var entries = await studioService.GetAllStudiosByTypeAsync(studioType);

        return Ok(entries);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudioDto[]>> GetStudioById(Guid id) {
        var entry = await studioService.GetStudioByIdAsync(id);

        if (entry is null)
        {
            return NotFound();
        }

        return Ok(entry);
    }

}
