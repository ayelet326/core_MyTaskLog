using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TokenService.Interfaces
{
    public interface ITokenService
    {
    // TokenValidationParameters GetTokenValidationParameters();
    string WriteToken(SecurityToken token) ;
    SecurityToken GetToken(List<Claim> claims);
    int GetUserIdFromToken(IHttpContextAccessor httpContextAccessor); 

    }
}