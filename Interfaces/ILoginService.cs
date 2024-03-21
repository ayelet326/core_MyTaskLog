using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Login.Interfaces
{
    public interface ILoginService
    {
    TokenValidationParameters GetTokenValidationParameters();
    string WriteToken(SecurityToken token) ;
    SecurityToken GetToken(List<Claim> claims);   
    }
}