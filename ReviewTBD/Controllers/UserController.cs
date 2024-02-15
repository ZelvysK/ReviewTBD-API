using Microsoft.AspNetCore.Mvc;
using ReviewTBDAPI.Contracts;
using ReviewTBDAPI.Services;
using ReviewTBDAPI.Utilities;

namespace ReviewTBDAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService, JwtService jwtService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetUserById(string id) {
        var entry = await userService.GetUserByIdAsync(id);

        if (entry is null)
        {
            return NotFound();
        }

        return Ok(entry);
    }

    [HttpGet]
    public async Task<ActionResult<UserDto>> GetUserByIdFromCookies() {
        try
        {
            var jwt = Request.Cookies["jwt"];

            var token = jwtService.Verify(jwt);

            var userId = token.Issuer;

            var user = await userService.GetUserByIdAsync(userId);

            return Ok(user);

        }
        catch (Exception _)
        {
            return Unauthorized();
        }
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserDto input) {
        var id = await userService.CreateUserAsync(input);

        return CreatedAtAction(nameof(GetUserById), new { id }, new { Message = "User registered successfully", Id = id });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginUser([FromBody] UserLoginDto input) {
        var user = await userService.GetUserByEmailAsync(input.Email);

        if (user == null || user.Password != input.Password)
        {
            return BadRequest(new { Message = "Invalid credentials" });
        }

        var token = jwtService.Generate(user.Id);

        Response.Cookies.Append("jwt", token, new CookieOptions
        {
            //Only backend
            HttpOnly = true,
        });

        return Ok(token);
    }

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout() {
        Response.Cookies.Delete("jwt");

        return Ok(new
        {
            Message = "Success"
        });
    }
}
