using System.Diagnostics;

namespace LogMiddleware.Middlewares;

public class LogMiddleware
{
    private RequestDelegate next;
    private readonly string filePath;

    public LogMiddleware(RequestDelegate next, string logFilePath)
    {
        this.next = next;
        this.filePath = logFilePath;
    }

    public async Task Invoke(HttpContext http_context)
    {
        var startWork = new Stopwatch();
        startWork.Start();
        await next(http_context);
        writeLogToFile($"{http_context.Request.Path}.{http_context.Request.Method}  took {startWork.ElapsedMilliseconds}ms."
            + $"  User: {http_context.User?.FindFirst("Id")?.Value ?? "unknown"}");     
              
    }   

// (filePath)פונקציה זאת מקבלת את המחרוזת המתעדת את הפעולה הנוכחית וכותבת אל הקובץ שקיבלנו בבנאי
    private void writeLogToFile(string message){

        using(StreamWriter sw = File.AppendText(this.filePath))
        {
            sw.WriteLine(message);
        }
    } 
}

public static partial class LogMiddleExtensions
{
    public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder,string logFilePath)
    {
        return builder.UseMiddleware<LogMiddleware>(logFilePath);
    }
}