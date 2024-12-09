using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BankApplication.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Person Name can't be blank")]
        public string PersonName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email should not be empty")]
        [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
        [Remote(action: "EmailIsAlreadyRegistered", controller: "Account", ErrorMessage = "Email is already in use")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Number should not be empty")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should contain digits only")]
        [Remote(action: "NumberIsAlreadyRegistered", controller: "Account", ErrorMessage = "Number is already in use")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password can't be blank")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password can't be blank")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
