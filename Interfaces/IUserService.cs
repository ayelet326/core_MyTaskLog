using IUser.Models;

namespace User_.Interfaces;

public interface IUserService
{
    List<User> GetAll();
    User? GetUserById(int id);
    int Add(User newUser);
    bool Update(int id, User newUser);
    bool Delete(int id);
}
