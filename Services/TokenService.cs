using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TokenService.Interfaces;

namespace TokenService.Services;

public class TokenToLogin : ITokenService
{
    private static SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ayhgfdgel465sdr*AYELET*yjdjfjdjf45hFGyYll55Lj3mkn"));
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
    
    
    public int GetUserIdFromToken(IHttpContextAccessor httpContextAccessor)
    {
        var token = httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
        //substring 'Bearer '
        token = token?.FirstOrDefault()?.Substring(7);
        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            //JwtSecurityToken מפענח את הטוקן לאובייקט מסוג 
            //Claimsכדי שנוכל לגשת לרשימת ה
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            var idClaim = jsonToken!.Claims.FirstOrDefault(c => c.Type == "Id");

            if (idClaim != null)
            {
                return int.Parse(idClaim.Value);
            }
        }

        return -1;
    }

}