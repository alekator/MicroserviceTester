using MicroserviceTester.Areas.UserService.Models;
using System.Collections.Generic;

namespace MicroserviceTester.Areas.UserService.Services
{
    public interface IUserService
    {
        void AddUser(User user);
        User? GetUser(int id);
        bool UserExists(int id);
        void DeleteUser(int id);
        void ClearAllUsers();
        IEnumerable<User> GetAllUsers();
    }
}
