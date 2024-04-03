using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myTaskLog.Interfaces;
using MyTaskLog.Models;
using TokenService.Interfaces;

namespace _TaskLog.Controllers;

[ApiController]
[Route("api/todo")]
public class TaskLogController : ControllerBase
{
    ITaskLogService TaskService;
    private readonly int CurrentUserID;
    public TaskLogController(ITaskLogService TaskService,ITokenService TokenService, IHttpContextAccessor httpContextAccessor)
    {
        this.TaskService = TaskService;
        this.CurrentUserID = TokenService.GetUserIdFromToken(httpContextAccessor);
    }

    [HttpGet]
    [Authorize(Policy = "User")]
    public ActionResult<List<TaskLog>> Get()
    {
        return TaskService.GetAll(this.CurrentUserID);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult<TaskLog> Get(int id)
    {
        var TaskLog = TaskService.GetById(id,this.CurrentUserID);
        if (TaskLog == null )
            return NotFound();
        return TaskLog;
    }

    [HttpPost]
    [Authorize(Policy = "User")]
    public ActionResult Post(TaskLog newTaskLog)
    {
        int newId = TaskService.Add(newTaskLog, this.CurrentUserID);
        //If the user tried to add a task to another user
        if (newId == -1)
        {
            return BadRequest();
        }
        return CreatedAtAction("Post",
            new { id = newId }, TaskService.GetById(newId,this.CurrentUserID));
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult Put(int id, TaskLog newTaskLog)
    {
        newTaskLog.UserId = this.CurrentUserID;
        var result = TaskService.Update(id, newTaskLog, this.CurrentUserID);
        if (!result)
        {
            return BadRequest();
        }

        return Content("update in sussefully!");
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult Delete(int id)
    {
        var IsDeleted = TaskService.Delete(id, this.CurrentUserID);
        if (!IsDeleted)
        {
            return BadRequest();
        }
        return Content("the task deleted in  successfully!👏👏👏👏👏👏");

    }
    
}