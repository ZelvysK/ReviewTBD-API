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
    [Admin]
    [HttpGet("me")]
    public async Task<ActionResult<MeDto>> GetUserInfo()
    {
        var id = User.GetId();

        if (id is null)
        {
            return Unauthorized();
        }

        var user = await userService.GetUserByIdAsync(id.Value);

        return Ok(user);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<IdentityUser>> GetUserById(Guid id)
    {
        var entry = await userService.GetUserByIdAsync(id);

        if (entry is null) return NotFound();

        return Ok(entry);
    }


    [HttpGet]
    public async Task<ActionResult<PaginatedResult<UserDto>>> GetAllUsers([FromQuery] UserQuery filters)
    {
        var result = await userService.GetAllUsersAsync(filters);

        return Ok(result);
    }

    // [HttpPost("ChangeEmail")]
    // public async Task<ActionResult> ChangeEmail(string id, UserEmailDto input)
    // {
    //     var updated = await userService.ChangeEmailAsync(id, input);
    //
    //     return updated.Succeeded
    //         ? Ok(updated)
    //         : BadRequest(updated.Errors);
    // }
    //
    // [HttpPost("ChangePhoneNumber")]
    // public async Task<ActionResult> ChangePhoneNumber(string id, UserPhoneDto input)
    // {
    //     var updated = await userService.ChangePhoneNumberAsync(id, input);
    //
    //     return updated.Succeeded
    //         ? Ok(updated)
    //         : BadRequest(updated.Errors);
    // }
    //
    // [HttpPost("ChangePassword")]
    // public async Task<ActionResult> ChangePassword(string id, UserPasswordDto input)
    // {
    //     var updated = await userService.ChangePasswordAsync(id, input);
    //
    //     return updated.Succeeded
    //         ? Ok(updated)
    //         : BadRequest(updated.Errors);
    // }
    //
    // [HttpPost("ResetPassword")]
    // public async Task<ActionResult> ResetPassword(string id, UserPasswordDto input)
    // {
    //     var updated = await userService.ResetPasswordAsync(id, input);
    //
    //     return updated.Succeeded
    //         ? Ok(updated)
    //         : BadRequest(updated.Errors);
    // }
    //
    // [HttpPost("Update/{id:guid}")]
    // public async Task<ActionResult<IdentityUser>> UpdateUser(string id, EditUserDto input)
    // {
    //     var updated = await userService.UpdateUserAsync(id, input);
    //
    //     return updated is not null
    //         ? Ok(updated)
    //         : NotFound();
    // }
}