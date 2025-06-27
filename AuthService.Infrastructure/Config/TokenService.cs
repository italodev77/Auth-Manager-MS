using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth_ms.Entities;
using Auth_ms.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Auth_ms.Config;

public class TokenService
{
    private readonly JWTconfig _jwtConfig;

    public TokenService(IConfiguration configuration)
    {
        _jwtConfig = configuration.GetSection("Jwt").Get<JWTconfig>()!;
    }

    public TokenResponseDto GenerateToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
        var expires = DateTime.UtcNow.AddHours(3);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.email),
            new Claim("UserId", user.UserId.ToString()),
           
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new TokenResponseDto
        {
            Token = tokenString,
            Expiration = expires
        };
    }
}