using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Contracts.Queries;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService, SignInManager<IdentityUser> signInManager) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<IdentityUser>> GetUserById(string id)
    {
        var entry = await userService.GetUserByIdAsync(id);

        if (entry is null) return NotFound();

        return Ok(entry);
    }

    [HttpPost("Logout")]
    public async Task Logout()
    {
        await signInManager.SignOutAsync();
    }

    [HttpPost("Register")]
    public async Task<ActionResult> Register(RegisterDto input)
    {
        var result = await userService.RegisterUserAsync(input);

        if (result.Succeeded)
        {
            var user = await userService.GetUserByUsernameAsync(input.Username);

            await signInManager.SignInAsync(user!, false, "Bearer");

            return Ok(user);
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<IdentityUser>> Login(LoginDto input)
    {
        var result = await userService.LoginUserAsync(input);

        if (result.Succeeded)
        {
            if (input.Username.Contains('@'))
            {
                var userByEmail = await userService.GetUserByEmailAsync(input.Username);
                await signInManager.SignInAsync(userByEmail!, false, "Bearer");

                return Ok(userByEmail);
            }

            var userByName = await userService.GetUserByUsernameAsync(input.Username);

            await signInManager.SignInAsync(userByName!, false, "Bearer");

            return Ok(userByName);
        }

        return BadRequest(result.ToString());
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<IdentityUser>>> GetAllUsers([FromQuery] UserQuery filters)
    {
        var result = await userService.GetAllUsersAsync(filters);

        return Ok(result);
    }

    [HttpPost("ChangeEmail")]
    public async Task<ActionResult> ChangeEmail(string id, UserEmailDto input)
    {
        var updated = await userService.ChangeEmailAsync(id, input);

        return updated.Succeeded
            ? Ok(updated)
            : BadRequest(updated.Errors);
    }

    [HttpPost("ChangePhoneNumber")]
    public async Task<ActionResult> ChangePhoneNumber(string id, UserPhoneDto input)
    {
        var updated = await userService.ChangePhoneNumberAsync(id, input);

        return updated.Succeeded
            ? Ok(updated)
            : BadRequest(updated.Errors);
    }

    [HttpPost("ChangePassword")]
    public async Task<ActionResult> ChangePassword(string id, UserPasswordDto input)
    {
        var updated = await userService.ChangePasswordAsync(id, input);

        return updated.Succeeded
            ? Ok(updated)
            : BadRequest(updated.Errors);
    }

    [HttpPost("ResetPassword")]
    public async Task<ActionResult> ResetPassword(string id, UserPasswordDto input)
    {
        var updated = await userService.ResetPasswordAsync(id, input);

        return updated.Succeeded
            ? Ok(updated)
            : BadRequest(updated.Errors);
    }

    [HttpPut]
    public async Task<ActionResult<IdentityUser>> UpdateUser(string id, EdidUserDto input)
    {
        var updated = await userService.UpdateUserAsync(id, input);

        return updated is not null
            ? Ok(updated)
            : NotFound();
    }
}