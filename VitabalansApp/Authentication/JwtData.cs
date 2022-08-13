using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace VitabalansApp.Authentication;

public static class JwtData
{
    public static string CreateJWTToken(UsersModel user)
    {
        var secret = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("supersalainenavain"));
        var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
            new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
            new Claim(ClaimTypes.Role, "employee"),
            new Claim(ClaimTypes.Expiration, DateTime.Now.ToString())
        };

        var jwtToken = new JwtSecurityToken(
            issuer: "ennakkotehtava-olli-nokkonen",
            audience: "ennakkotehtava-olli-nokkonen",
            claims: claims,
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
