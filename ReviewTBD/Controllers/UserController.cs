using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Services;

namespace ReviewTBDAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService, SignInManager<IdentityUser> signInManager) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<IdentityUser>> GetUserById(string id) {
        var entry = await userService.GetUserByIdAsync(id);

        if (entry is null)
        {
            return NotFound();
        }

        return Ok(entry);
    }

    [HttpPost("Logout")]
    public async Task Logout()
    {
        await signInManager.SignOutAsync();
    }

    [HttpPost("Register")]
    public async Task<ActionResult<IdentityUser>> Register(RegisterDto input)
    {
        var user = await userService.RegisterUserAsync(input);

        return Ok(user);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<IdentityUser>> Login(LoginDto input)
    {
        var user = await userService.LoginUserAsync(input);

        return Ok(user);
    }
}
