using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myTaskLog.Interfaces;
using MyTaskLog.Models;

namespace _TaskLog.Controllers;

[ApiController]
[Route("api/todo")]
public class TaskLogController : ControllerBase
{
    ITaskLogService TaskService;
    private int CurrentUserID;
    private int GetUserIdFromToken(IHttpContextAccessor httpContextAccessor)
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

        return 1;
    }
    public TaskLogController(ITaskLogService TaskService, IHttpContextAccessor httpContextAccessor)
    {
        this.TaskService = TaskService;
        this.CurrentUserID = GetUserIdFromToken(httpContextAccessor);
    }

    [HttpGet]
    [Authorize(Policy = "User")]
    public ActionResult<List<TaskLog>> Get()
    {
        return TaskService.GetAll().FindAll(task => task.UserId == this.CurrentUserID);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult<TaskLog> Get(int id)
    {
        var TaskLog = TaskService.GetById(id);
        if (TaskLog == null || TaskLog.UserId != this.CurrentUserID)
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
            new { id = newId }, TaskService.GetById(newId));
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