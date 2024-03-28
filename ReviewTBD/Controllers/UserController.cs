using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Filters;
using ReviewTBDAPI.Services;
using ReviewTBDAPI.Utilities;

namespace ReviewTBDAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> GetUserInfo()
    {
        var id = User.GetId();

        if (id is null)
        {
            return Unauthorized();
        }

        var user = await userService.GetUserByIdAsync(id.Value);

        return Ok(user);
    }

    // [Admin]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<IdentityUser>> GetUserById(Guid id)
    {
        var entry = await userService.GetUserByIdAsync(id);

        if (entry is null) return NotFound();

        return Ok(entry);
    }

    // [Admin]
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<UserDto>>> GetAllUsers([FromQuery] UserQuery filters)
    {
        var result = await userService.GetAllUsersAsync(filters);

        return Ok(result);
    }

    [HttpPost("Update/{id:guid}")]
    public async Task<ActionResult<UserDto>> UpdateUser(Guid id, UserUpdateDto input)
    {
        var updated = await userService.UpdateUserAsync(id, input);
    
        return updated is not null
            ? Ok(updated)
            : NotFound();
    }
    
    [Admin]
    [HttpPost("AdminUpdate/{id:guid}")]
    public async Task<ActionResult<UserDto>> AdminUpdateUser(Guid id, AdminUpdateDto input)
    {
        var updated = await userService.AdminUpdateUserAsync(id, input);
    
        return updated is not null
            ? Ok(updated)
            : NotFound();
    }
    
    [HttpPost("ChangePassword")]
    public async Task<ActionResult> ChangePassword(Guid id, UpdatePasswordDto input)
    {
        var updated = await userService.ChangePasswordAsync(id, input);
    
        return updated.Succeeded
            ? Ok(updated)
            : BadRequest(updated.Errors);
    }
    
    [HttpPost("ResetPassword")]
    public async Task<ActionResult> ResetPassword(Guid id, UpdatePasswordDto input)
    {
        var updated = await userService.ResetPasswordAsync(id, input);
    
        return updated.Succeeded
            ? Ok(updated)
            : BadRequest(updated.Errors);
    }
}