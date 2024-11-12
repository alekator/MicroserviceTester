using System.ComponentModel.DataAnnotations;

namespace MicroserviceTester.Areas.UserService.Models
{
    public class User
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive integer.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Username field is required.")]
        [StringLength(50, ErrorMessage = "The Username field must be a maximum of 50 characters.")]
        public string Username { get; set; }
    }
}
