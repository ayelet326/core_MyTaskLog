using Microsoft.AspNetCore.Mvc;
using myTaskLog.Interfaces;
using MyTaskLog.Models;
using MyTaskLog.Services;

namespace ksTaskLog.Controllers;

[ApiController]
[Route("todo")]
public class TaskLogController : ControllerBase
{
    ITaskLogService TaskService;
    public TaskLogController(ITaskLogService TaskService)
    {
        this.TaskService = TaskService;
    }
    [HttpGet]
    public ActionResult<List<TaskLog>> Get()
    {
        return TaskService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<TaskLog> Get(int id)
    {
        var TaskLog = TaskService.GetById(id);
        if (TaskLog == null)
            return NotFound();
        return TaskLog;
    }

    [HttpPost]
    public ActionResult Post(TaskLog newTaskLog)
    {
        var newId = TaskService.Add(newTaskLog);

        return CreatedAtAction("Post",
            new { id = newId }, TaskService.GetById(newId));
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, TaskLog newTaskLog)
    {
        var result = TaskService.Update(id, newTaskLog);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var IsDeleted = TaskService.Delete(id);
        if (!IsDeleted)
        {
            return BadRequest();
        }
        return Content("the task deleted in  successfully!👏👏👏👏👏👏");

    }
}