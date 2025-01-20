using Microsoft.AspNetCore.Mvc;
using SmartScheduler.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _authService.Authenticate(request.Username, request.Password);
        if (user == null)
            return Unauthorized();

        var token = _authService.GenerateToken(user);
        return Ok(new { Token = token });
    }
}

public record LoginRequest(string Username, string Password);
