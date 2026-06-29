using Microsoft.AspNetCore.Identity;
using TaskFlow.Dtos.Auth;
using TaskFlow.Interfaces;
using TaskFlow.Models;

namespace TaskFlow.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    
    public AuthService(UserManager<User> userManager,  ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }
    
    public async Task<UserDto?> RegisterAsync(RegisterDto dto)
    {
        var user = new User

        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            return null;
        }

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    public async Task<string?> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            return null;

        var isValid = await _userManager.CheckPasswordAsync(user, dto.Password);

        if (!isValid)
            return null;

        return _tokenService.CreateToken(user);
    }
}