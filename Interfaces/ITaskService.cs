using MyTaskLog.Models;

namespace myTaskLog.Interfaces
{
    public interface ITaskLogService
    {
       
     List<TaskLog> GetAll();
     TaskLog? GetById(int id) ;
    int Add(TaskLog newTaskLog);
    bool Update(int id, TaskLog newTaskLog);  
    bool Delete(int id);
 
    }
}