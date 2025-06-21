using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth_ms.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Auth_ms.Config;

public class TokenService
{
    private readonly JWTconfig _jwtConfig;

    public TokenService(IConfiguration configuration)
    {
        _jwtConfig = configuration.GetSection("Jwt").Get<JWTconfig>()!;
    }

    public object GenerateToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", user.UserId.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(3),
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new
        {
            token = tokenString,
        };
    }
}