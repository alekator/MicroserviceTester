using MicroserviceTester.Areas.UserService.Models;
using System.Collections.Generic;
using System.Linq;

namespace MicroserviceTester.Areas.UserService.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new();

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public User? GetUser(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public bool UserExists(int id)
        {
            return _users.Any(u => u.Id == id);
        }

        public void DeleteUser(int id)
        {
            var user = GetUser(id);
            if (user != null)
            {
                _users.Remove(user);
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }
    }

}
