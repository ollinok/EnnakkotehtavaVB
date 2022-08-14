using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace VitabalansApp.Authentication;

public static class JwtData
{
#nullable disable
    public static string CreateJWTToken(UsersModel user)
    {
        var secret = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("supersalainenavain"));
        var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, "employee"),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.Name)
        };

        var jwtToken = new JwtSecurityToken(
            issuer: "ennakkotehtava-olli-nokkonen",
            audience: "ennakkotehtava-olli-nokkonen",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(12),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
