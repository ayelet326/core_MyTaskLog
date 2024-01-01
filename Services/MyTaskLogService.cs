using MyTaskLog.Models;
using myTaskLog.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

namespace MyTaskLog.Services;


public class TaskLogcs : ITaskLogService
{
    List<TaskLog> TaskLogs ;
    private string fileName = "TaskList.json";

    public TaskLogcs(IWebHostEnvironment webHost)
    {
        this.fileName = Path.Combine(webHost.ContentRootPath, "wwwroot","Data", "TaskList.json");

        using (var jsonFile = File.OpenText(fileName))
        {
            TaskLogs = JsonSerializer.Deserialize<List<TaskLog>>(jsonFile.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

    }
    private void saveToFile()
    {
        File.WriteAllText(fileName, JsonSerializer.Serialize(TaskLogs));
    }

    public List<TaskLog> GetAll() => TaskLogs;

    public TaskLog? GetById(int id)
    {
        return TaskLogs.FirstOrDefault(p => p.Id == id);
    }


    public int Add(TaskLog newTaskLog)
    {
        if (TaskLogs.Count == 0)

        {
            newTaskLog.Id = 1;
        }
        else
        {
            newTaskLog.Id = TaskLogs.Max(p => p.Id) + 1;

        }

        TaskLogs.Add(newTaskLog);
        saveToFile();

        return newTaskLog.Id;
    }

    public bool Update(int id, TaskLog newTaskLog)
    {
        if (id != newTaskLog.Id)
            return false;

        var existingTaskLog = GetById(id);
        if (existingTaskLog == null)
            return false;

        var index = TaskLogs.IndexOf(existingTaskLog);
        if (index == -1)
            return false;

        TaskLogs[index] = newTaskLog;
        saveToFile();
        return true;
    }


    public bool Delete(int id)
    {
        var existingTaskLog = GetById(id);
        if (existingTaskLog == null)
            return false;

        var index = TaskLogs.IndexOf(existingTaskLog);
        if (index == -1)
            return false;

        TaskLogs.RemoveAt(index);
         saveToFile();
        return true;
    }
    public int Count=>TaskLogs.Count();



}
//מחלקה extention
//שתצור מופע יחיד של יומן משימות
public static class TaskLogUtils
{
    public static void AddTaskLog(this IServiceCollection services)
    {
        services.AddSingleton<ITaskLogService, TaskLogcs>();
    }
}