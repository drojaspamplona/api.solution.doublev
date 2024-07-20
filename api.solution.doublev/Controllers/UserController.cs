using api.solution.doublev.Models.Auth;
using api.solution.doublev.Services.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace api.solution.doublev.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest request)
    {
        var token = await _userService.AuthenticateAsync(request.Username, request.Password);

        if (token == null)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        return Ok(new { Token = token });
    }
}