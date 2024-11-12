using MicroserviceTester.Areas.UserService.Models;
using MicroserviceTester.Areas.UserService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MicroserviceTester.Areas.UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching users.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _userService.GetUser(id);
                return user != null ? Ok(user) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching user with ID {id}.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                if (_userService.UserExists(user.Id))
                {
                    return Conflict("User with this ID already exists.");
                }

                _userService.AddUser(user);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (System.InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"User creation conflict for ID {user.Id}.");
                return Conflict("User with this ID already exists.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while creating user with ID {user.Id}.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _userService.GetUser(id);
                if (user == null)
                {
                    return NotFound();
                }

                _userService.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting user with ID {id}.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
