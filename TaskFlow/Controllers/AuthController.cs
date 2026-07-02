using Microsoft.AspNetCore.Mvc;
using TaskFlow.Dtos.Requests.Auth;
using TaskFlow.Interfaces;

namespace TaskFlow.Controllers;


[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);

        if (result == null)
        {
            return BadRequest("Registration failed");
        }
        
        return Ok(result);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.LoginAsync(request);

        if (token == null)
        {
            return  BadRequest("Login failed");
        }
        
        return Ok(new { Token = token });
    }
}