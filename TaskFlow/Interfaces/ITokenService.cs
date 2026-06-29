using TaskFlow.Models;

namespace TaskFlow.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}