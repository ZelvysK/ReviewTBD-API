using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class StudioController(IStudioService studioService) : ControllerBase {
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<Contracts.StudioDto>>> GetAllStudios([FromQuery] StudioQuery filters) {
        var result = await studioService.GetAllStudiosAsync(filters);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Contracts.StudioDto[]>> GetStudioById(Guid id) {
        var entry = await studioService.GetStudioByIdAsync(id);

        return entry is not null
            ? Ok(entry)
            : NotFound();
    }

    [HttpPost]
    public async Task<Microsoft.AspNetCore.Mvc.IActionResult> CreateStudio([FromBody] StudioDto input) {

        var id = await studioService.CreateStudioAsync(input);

        return CreatedAtAction(nameof(GetStudioById), new { id }, new { Message = "Studio created successfully", Id = id });
    }

    [HttpDelete("{id:guid}")]
    public async Task<Microsoft.AspNetCore.Mvc.IActionResult> DeleteStudio(Guid id) {
        var deleted = await studioService.DeleteStudioAsync(id);

        return deleted ? NoContent() : NotFound();

    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Contracts.StudioDto[]>> UpdateStudio(Guid id, [FromBody] StudioDto input) {

        if (input == null)
        {
            return BadRequest("Input data is null");
        }

        await studioService.UpdateStudioAsync(id, input);

        return Ok(); 
    }

}
