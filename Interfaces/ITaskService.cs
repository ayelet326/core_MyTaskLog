using MyTaskLog.Models;

namespace myTaskLog.Interfaces
{
    public interface ITaskLogService
    {
       
     List<TaskLog> GetAll(int userID);
     TaskLog? GetById(int taskId,int userId) ;
    int Add(TaskLog newTaskLog,int userId);
    bool Update(int id, TaskLog newTaskLog, int userId);  
    bool Delete(int id, int UserId);
    bool DeleteTasksBelongedUser(int userId);
 
    }
}