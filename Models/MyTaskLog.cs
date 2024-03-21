using SharedModel.Models;

namespace MyTaskLog.Models;

public class TaskLog:Shared
{

    public string? DateToDo {get;set;}
    public bool IsDo {get; set;}
    public int UserId{get;set;}
}