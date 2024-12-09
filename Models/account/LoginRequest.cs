using BankApplication.DTO;
using MediatR;

namespace BankApplication.Models.account
{
    public class LoginRequest : IRequest<(bool Success, string ErrorMessage, AuthenticationResponse Response)>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
