using BankApplication.DTO;
using MediatR;

namespace BankApplication.Models.account
{
    public class RegisterRequest : IRequest<(bool Success, string ErrorMessage, AuthenticationResponse Response)>
    {
        public string PersonName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
