using TaskFlow.Dtos.Auth;

namespace TaskFlow.Interfaces;

public interface IAuthService
{
    Task<UserDto?> RegisterAsync(RegisterDto dto);
    Task<string?> LoginAsync(LoginDto dto);
}