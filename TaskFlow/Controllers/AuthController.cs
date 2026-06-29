using Microsoft.AspNetCore.Mvc;
using TaskFlow.Dtos.Auth;
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
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authService.RegisterAsync(dto);

        if (result == null)
        {
            return BadRequest();
        }
        
        return Ok(result);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var token = await _authService.LoginAsync(dto);

        if (token == null)
        {
            return  BadRequest();
        }
        
        return Ok(new { Token = token });
    }
}