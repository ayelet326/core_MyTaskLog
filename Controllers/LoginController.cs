using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Login.Interfaces;
using TokenService.Services;
using IUser.Models;
using User_.Interfaces;
using UserService.Services;
using Microsoft.AspNetCore.Authorization;

namespace Login.Controllers;

[ApiController]
[Route("api")]
public class loginController : ControllerBase
{
    private ILoginService tokenService;
    private IUserService userService;
    public loginController(ILoginService tokenService,IUserService userService)
    {
        this.tokenService = tokenService;
        this.userService= userService;
    }

    [HttpPost]
    [Route("[action]")]
    public ActionResult<String> Login([FromBody] User User)
        {
            var dt = DateTime.Now;
            var claims = new List<Claim>();
            var userExist = userService.GetAll().FirstOrDefault(u=> u.Name == User.Name && u.Password == User.Password);
            if (userExist==null)
            {
                return Unauthorized();
            }
            //המנהל מוגדר לפי שם משתמש של המנהל וסיסמת מנהל
            if (User.Name == "Manager" && User.Password == $"m{dt.Year}#{dt.Day}#456")
            {
                claims.Add(new Claim("type", "Admin"));
            }

            claims.Add(new Claim("type", "User"));

            var token = tokenService.GetToken(claims);

            return new OkObjectResult(tokenService.WriteToken(token));
        }

}