//מחלקה extention
//שתצור מופע יחיד של יומן משימות
using myTaskLog.Interfaces;
using MyTaskLog.Services;

namespace TaskLogUtils.Utilites;
public static class TaskLogUtils
{
    public static void AddTaskLog(this IServiceCollection services)
    {
        services.AddSingleton<ITaskLogService, TaskLogcs>();
    }
}