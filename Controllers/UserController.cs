using Microsoft.AspNetCore.Mvc;
using User_.Interfaces;
using IUser.Models;
using Microsoft.AspNetCore.Authorization;
using TokenService.Interfaces;
using myTaskLog.Interfaces;


namespace _User.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService UserService;
    private readonly ITaskLogService TaskLogService;
    private int CurrentUserID;
    public UserController(IUserService UserService,ITaskLogService TaskLogService, ITokenService TokenService, IHttpContextAccessor httpContextAccessor)
    {
        this.TaskLogService=TaskLogService;
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
        if (newId == null)
            return Conflict("user exist,change your name or password!");

        int userId = newId.Value;
        return CreatedAtAction("Post", new { id = newId }, UserService.GetUserById(userId));


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
        var IsDeletedTasks=TaskLogService.DeleteTasksBelongedUser(id);
        if (!IsDeleted||!IsDeletedTasks)
        {
            return BadRequest();
        }
        return Content("the user deleted in  successfully!👏👏👏👏👏👏");

    }
}