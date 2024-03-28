using MyTaskLog.Models;

namespace myTaskLog.Interfaces
{
    public interface ITaskLogService
    {
       
     List<TaskLog> GetAll();
     TaskLog? GetById(int id) ;
    int Add(TaskLog newTaskLog,int userId);
    bool Update(int id, TaskLog newTaskLog, int userId);  
    bool Delete(int id, int UserId);
 
    }
}