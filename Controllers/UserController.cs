using Microsoft.AspNetCore.Mvc;
using User_.Interfaces;
using IUser.Models;
using Microsoft.AspNetCore.Authorization;


namespace _User.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    IUserService UserService;
    public UserController(IUserService UserService)
    {
        this.UserService = UserService;
    }
    
    [HttpGet]
    [Authorize(Policy = "Admin")]
    public ActionResult<List<User>> Get()
    {
        return UserService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        var User = UserService.GetUserById(id);
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

    [HttpPut("{id}")]
    public ActionResult Put(int id, User newUser)
    {
        var result = UserService.Update(id, newUser);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
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