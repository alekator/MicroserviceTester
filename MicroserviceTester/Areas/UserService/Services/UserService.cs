using MicroserviceTester.Areas.UserService.Models;
using System.Collections.Concurrent;

namespace MicroserviceTester.Areas.UserService.Services
{
    public class UserService : IUserService
    {
        private readonly ConcurrentDictionary<int, User> _users = new();

        public void AddUser(User user)
        {
            if (!_users.TryAdd(user.Id, user))
            {
                throw new System.InvalidOperationException("User with this ID already exists.");
            }
        }

        public User? GetUser(int id)
        {
            _users.TryGetValue(id, out var user);
            return user;
        }

        public bool UserExists(int id)
        {
            return _users.ContainsKey(id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users.Values.ToList();
        }

        public void DeleteUser(int id)
        {
            _users.TryRemove(id, out var _);
        }
        public void ClearAllUsers()
        {
            _users.Clear();
        }
    }
}
