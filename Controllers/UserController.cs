using Microsoft.AspNetCore.Mvc;
using User_.Interfaces;
using IUser.Models;
using Microsoft.AspNetCore.Authorization;
using TokenService.Interfaces;


namespace _User.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private IUserService UserService;
    private int CurrentUserID;
    public UserController(IUserService UserService, ITokenService TokenService, IHttpContextAccessor httpContextAccessor)
    {
        this.UserService = UserService;
        this.CurrentUserID = TokenService.GetUserIdFromToken(httpContextAccessor);
        
    }

    [Route("/GetAll")]
    [HttpGet]
    [Authorize(Policy = "Admin")]
    public ActionResult<List<User>> Get()
    {
        return UserService.GetAll();
    }

    [HttpGet]
    [Authorize(Policy = "User")]
    public ActionResult<User> GetMyDetails()
    {
        var User = UserService.GetUserById(CurrentUserID);
        if (User == null)
            return NotFound();
        return User;
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public ActionResult Post(User newUser)
    {
        var newId = UserService.Add(newUser);

        return CreatedAtAction("Post",
            new { id = newId }, UserService.GetUserById(newId));
    }

    [HttpPut]
    [Authorize(Policy = "User")]
    public ActionResult Put(User newUser)
    {
        var result = UserService.Update(CurrentUserID, newUser);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    public ActionResult Delete(int id)
    {
        var IsDeleted = UserService.Delete(id);
        if (!IsDeleted)
        {
            return BadRequest();
        }
        return Content("the task deleted in  successfully!👏👏👏👏👏👏");

    }
}