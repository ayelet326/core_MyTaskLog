using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Login.Interfaces;

namespace TokenService.Services;

public class TokenToLogin : ILoginService
{
    private static SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ayhgfdgel465sdr**yjdjfjdjf45hFGyYll55Lj3mkn"));
    private static string issuer = "https://localhost:7150";
//מייצר את התוקן עי רשימת הטענות שמקבל 
    public SecurityToken GetToken(List<Claim> claims) =>
            new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.Now.AddDays(30.0),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
//מחזיר את הפרמטרים שנוכל לוודא איתם את תקינות התוקן
//סטטית כיוון שערכיה קבועים ולא תלויים במופע מסוים
    public static TokenValidationParameters GetTokenValidationParameters() =>
        new TokenValidationParameters
        {
                ValidIssuer = issuer,
                ValidAudience = issuer,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero 
        };

    public string WriteToken(SecurityToken token) =>
        new JwtSecurityTokenHandler().WriteToken(token);

}