using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MicroserviceTester.Areas.UserService.Services;
using MicroserviceTester.Areas.UserService.Models;

namespace MicroserviceTester.Tests.PerformanceTest
{
    public class UserServiceBenchmark
    {
        private readonly IUserService _userService;

        public UserServiceBenchmark()
        {
            _userService = new UserService();
            for (int i = 0; i < 1000; i++)
            {
                _userService.AddUser(new User { Id = i, Username = $"User{i}" });
            }
        }

        [Benchmark]
        public void GetUserById()
        {
            var user = _userService.GetUser(500);
        }

        [Benchmark]
        public void DeleteUser()
        {
            _userService.DeleteUser(500);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<UserServiceBenchmark>();
        }
    }
}
