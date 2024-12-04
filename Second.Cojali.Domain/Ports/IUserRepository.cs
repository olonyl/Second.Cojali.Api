using Second.Cojali.Domain.Entities;

namespace Second.Cojali.Domain.Ports;

public interface IUserRepository
{
    IEnumerable<User> GetAllUsers();
    User GetById(int id);
    void AddUser(User user);
    void UpdateUser(User user);
    bool Exists(int id);
}
