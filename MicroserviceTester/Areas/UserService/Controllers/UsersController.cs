using MicroserviceTester.Areas.UserService.Models;
using MicroserviceTester.Areas.UserService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTester.Areas.UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetUser(id);
            return user != null ? Ok(user) : NotFound();
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (_userService.UserExists(user.Id))
            {
                return Conflict("User with this ID already exists.");
            }

            _userService.AddUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        // DELETE api/Users/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            // Логика удаления пользователя
            // Например:
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            _userService.DeleteUser(id);
            return NoContent(); // Возвращает HTTP 204
        }

    }

}
