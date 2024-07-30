using System.ComponentModel.DataAnnotations;

namespace Todo.Api.DTOs.Identity
{
    public class UserDto
    {
        public int UserId { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required]
        public string Username { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must contain at least 8 characters including one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }
        public string? RoleId { get; set; }

    }
}
