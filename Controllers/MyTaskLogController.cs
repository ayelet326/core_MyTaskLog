using Microsoft.AspNetCore.Mvc;
using ksTaskLog.Models;
using ksTaskLog.Services;

namespace ksTaskLog.Controllers;

[ApiController]
[Route("todo")]
public class TaskLogController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<TaskLog>> Get()
    {
        return TaskLogcs.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<TaskLog> Get(int id)
    {
        var TaskLog = TaskLogcs.GetById(id);
        if (TaskLog == null)
            return NotFound();
        return TaskLog;
    }

    [HttpPost]
    public ActionResult Post(TaskLog newTaskLog)
    {
        var newId = TaskLogcs.Add(newTaskLog);

        return CreatedAtAction("Post", 
            new {id = newId}, TaskLogcs.GetById(newId));
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id,TaskLog newTaskLog)
    {
        var result = TaskLogcs.Update(id, newTaskLog);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
    [HttpDelete("{id}")]
    public ActionResult  Delete(int id ){
        var IsDeleted=TaskLogcs.Delete(id);
         if (!IsDeleted)
        {
            return BadRequest();
        }
        return Content("the task deleted in  successfully!👏👏👏👏👏👏");

    }
}