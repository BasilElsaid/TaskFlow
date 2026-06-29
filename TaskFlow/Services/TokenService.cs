using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaskFlow.Authentication;
using TaskFlow.Interfaces;
using TaskFlow.Models;

namespace TaskFlow.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    
    public TokenService(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }
    
    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Issuer,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: creds
            );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}