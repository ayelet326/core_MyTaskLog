//מחלקה extention
//שתצור מופע יחיד של יומן משימות
using myTaskLog.Interfaces;
using MyTaskLog.Services;

public static class TaskLogUtils
{
    public static void AddTaskLog(this IServiceCollection services)
    {
        services.AddSingleton<ITaskLogService, TaskLogcs>();
    }
}