using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.solution.doublev.Models.Auth;
using Microsoft.IdentityModel.Tokens;

public interface ITokenService
{
    string GenerateToken(User user);
}

public class TokenService : ITokenService
{
    private readonly string _key;

    public TokenService(string key)
    {
        _key = key;
    }

    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "yourdomain.com",
            audience: "yourdomain.com",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}