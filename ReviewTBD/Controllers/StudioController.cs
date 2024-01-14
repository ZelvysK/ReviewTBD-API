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
    public async Task<ActionResult<StudioDto[]>> GetAllStudios([FromQuery] StudioType studioType) {
        var entries = await studioService.GetAllStudiosAsync();

        return Ok(entries.Where(t=>t.Type == studioType));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudioDto[]>> GetStudioById([FromQuery]StudioType studioType, Guid id) {
        var entry = await studioService.GetStudioByIdAsync(studioType, id);

        if (entry is null)
        {
            return NotFound();
        }

        return Ok(entry);
    }

}
