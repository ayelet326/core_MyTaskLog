//מחלקה extention
//שתצור מופע יחיד של יומן משימות
using myTaskLog.Interfaces;
using MyTaskLog.Services;
using TokenService.Interfaces;
using TokenService.Services;

namespace TaskLogUtils.Utilites;
public static class TaskLogUtils
{
    public static void AddTaskLog(this IServiceCollection services)
    {
        services.AddSingleton<ITaskLogService, TaskLogcs>();
        services.AddSingleton<ITokenService, TokenToLogin>();
    }
}