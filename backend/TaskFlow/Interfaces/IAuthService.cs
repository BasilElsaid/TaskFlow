using TaskFlow.Dtos.Requests.Auth;
using TaskFlow.Dtos.Responses.Auth;

namespace TaskFlow.Interfaces;

public interface IAuthService
{
    Task<UserResponse?> RegisterAsync(RegisterRequest request);
    Task<string?> LoginAsync(LoginRequest request);
}