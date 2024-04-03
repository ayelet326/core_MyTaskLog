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

    public TaskLogcs(IWebHostEnvironment webHost)
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
    private bool IfTaskBelongedUser(int taskId, int userId)
    {
        TaskLog? taskById = sharedService.GetById(taskId);
        //Ensures that a user can only get/edit/delete their own tasks
        if (taskById == null || taskById.UserId != userId)
        {
            return false;
        }
        return true;
    }
    public List<TaskLog> GetAll(int userID) => sharedService.GetAll().FindAll(task => task.UserId == userID);
    public TaskLog? GetById(int taskId, int userId)
    {
        if (!IfTaskBelongedUser(taskId, userId))
        {
            return null;
        }
        return sharedService.GetById(taskId);
    }
    public int Add(TaskLog newTaskLog, int userId)
    {
        //Promises that user add a task only to himself
        if (newTaskLog.UserId != userId)
            return -1;
        return sharedService.Add(newTaskLog);
    }
    public bool Update(int taskId, TaskLog newTaskLog, int userId)
    {
        if (!IfTaskBelongedUser(taskId, userId))
        {
            return false;
        }
        return sharedService.Update(taskId, newTaskLog);
    }
    public bool Delete(int id, int userId)
    {
        if (!IfTaskBelongedUser(id, userId))
        {
            return false;
        }
        return sharedService.Delete(id);
    }
    public void DeleteTasksBelongedUser(int userId)
    {
        List<TaskLog> listTasks = GetAll(userId);
        listTasks.ForEach(task =>
        {
            Delete(task.Id, task.UserId);
        });


    }
}
