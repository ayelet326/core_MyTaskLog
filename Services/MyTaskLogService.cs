using ksTaskLog.Models;
namespace ksTaskLog.Services;

public static class TaskLogcs
{
    private static List<TaskLog> TaskLogs;

    static TaskLogcs()
    {
        TaskLogs = new List<TaskLog>
        {
            new TaskLog { Id = 1, Name = "Ayelet",DateToDo="25-12-23", IsDo = false},
            new TaskLog { Id = 2, Name = "Rut", DateToDo="20-11-23", IsDo = false},
            new TaskLog { Id = 3, Name = "Sara",DateToDo="07-11-23", IsDo = true}
        };
    }

    public static List<TaskLog> GetAll() => TaskLogs;

    public static TaskLog? GetById(int id) 
    {
        return TaskLogs.FirstOrDefault(p => p.Id == id);
    }

    public static int Add(TaskLog newTaskLog)
    {
        if (TaskLogs.Count == 0)

            {
                newTaskLog.Id = 1;
            }
            else
            {
        newTaskLog.Id =  TaskLogs.Max(p => p.Id) + 1;

            }

        TaskLogs.Add(newTaskLog);

        return newTaskLog.Id;
    }
  
    public static bool Update(int id, TaskLog newTaskLog)
    {
        if (id != newTaskLog.Id)
            return false;

        var existingTaskLog = GetById(id);
        if (existingTaskLog == null )
            return false;

        var index = TaskLogs.IndexOf(existingTaskLog);
        if (index == -1 )
            return false;

        TaskLogs[index] = newTaskLog;

        return true;
    }  

      
    public static bool Delete(int id)
    {
        var existingTaskLog = GetById(id);
        if (existingTaskLog == null )
            return false;

        var index = TaskLogs.IndexOf(existingTaskLog);
        if (index == -1 )
            return false;

        TaskLogs.RemoveAt(index);
        return true;
    }  



}