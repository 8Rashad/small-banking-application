using MediatR;

namespace BankApplication.Models.account
{
    public class IsEmailRegisteredRequest : IRequest<(bool IsRegistered, string ErrorMessage)>
    {
        public string Email { get; set; }

    }
}
