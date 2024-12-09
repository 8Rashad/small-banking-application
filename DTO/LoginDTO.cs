using System.ComponentModel.DataAnnotations;

namespace BankApplication.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        public required string Password { get; set; }
    }
}
