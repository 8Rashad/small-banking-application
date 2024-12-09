using BankApplication.DTO;
using BankApplication.Models.account;
using BankApplication.Service.Account;
using MediatR;

namespace BankApplication.Handlers.account
{
    public class RegisterHandler : IRequestHandler<RegisterRequest, (bool Success, string ErrorMessage, AuthenticationResponse Response)>
    {
        private readonly IAccountService _accountService;

        public RegisterHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<(bool Success, string ErrorMessage, AuthenticationResponse Response)> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            return await _accountService.RegisterAsync(new RegisterDTO
            {
                PersonName = request.PersonName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword
            });
        }
    }
}
