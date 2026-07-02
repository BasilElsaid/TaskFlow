using Microsoft.AspNetCore.Identity;
using TaskFlow.Dtos.Requests.Auth;
using TaskFlow.Dtos.Responses.Auth;
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
    
    public async Task<UserResponse?> RegisterAsync(RegisterRequest request)
    {
        var user = new User

        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return null;
        }

        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    public async Task<string?> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return null;

        var isValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isValid)
            return null;

        return _tokenService.CreateToken(user);
    }
}