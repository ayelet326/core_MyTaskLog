using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Login.Interfaces;
using TokenService.Services;
using IUser.Models;
using User_.Interfaces;
using UserService.Services;
using Microsoft.AspNetCore.Authorization;
using enumType.Models;

namespace Login.Controllers;

[ApiController]
[Route("api")]
public class loginController : ControllerBase
{
    private ILoginService tokenService;
    private IUserService userService;
    public loginController(ILoginService tokenService, IUserService userService)
    {
        this.tokenService = tokenService;
        this.userService = userService;
    }
    private bool ifAdmin(User User) => User.TypeUser == TypeUser.ADMIN;

    [HttpPost]
    [Route("[action]")]
    public ActionResult<String> Login([FromBody] User User)
    {
        var dt = DateTime.Now;
        var claims = new List<Claim>();
        var userExist = userService.GetAll().FirstOrDefault(u => u.Name == User.Name && u.Password == User.Password);
       
        if (userExist == null )
        {
            return Unauthorized();
        }
        if (ifAdmin(User))
        {
            claims.Add(new Claim("type", "Admin"));
        }

        claims.Add(new Claim("type", "User"));
        //Identify the specific user when making requests later
        claims.Add(new Claim("Id", User.Id.ToString()));

        var token = tokenService.GetToken(claims);

        return new OkObjectResult(tokenService.WriteToken(token));
    }

}