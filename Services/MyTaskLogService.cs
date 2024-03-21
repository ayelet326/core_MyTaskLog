using MyTaskLog.Models;
using myTaskLog.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using SharedLogicInServices;

namespace MyTaskLog.Services;


public class TaskLogcs : ITaskLogService
{
    private List<TaskLog> TaskLogs;
    private string fileName = "TaskList.json";
    private SharedLogic<TaskLog> sharedService;


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public TaskLogcs(IWebHostEnvironment webHost)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        this.fileName = Path.Combine(webHost.ContentRootPath, "wwwroot", "Data", "TaskList.json");

        using (var jsonFile = File.OpenText(fileName))
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            TaskLogs = JsonSerializer.Deserialize<List<TaskLog>>(jsonFile.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
#pragma warning restore CS8601 // Possible null reference assignment.
        }
#pragma warning disable CS8604 // Possible null reference argument.
        this.sharedService = new SharedLogic<TaskLog>(TaskLogs, fileName);
#pragma warning restore CS8604 // Possible null reference argument.
    }
    public List<TaskLog> GetAll() => sharedService.GetAll();
    public TaskLog? GetById(int id) => sharedService.GetById(id);
    public int Add(TaskLog newTaskLog) => sharedService.Add(newTaskLog);
    public bool Update(int id, TaskLog newTaskLog) => sharedService.Update(id, newTaskLog);
    public bool Delete(int id) => sharedService.Delete(id);

}
