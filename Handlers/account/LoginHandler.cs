using BankApplication.DTO;
using BankApplication.Models.account;
using BankApplication.Service.Account;
using MediatR;

namespace BankApplication.Handlers.account
{
    public class LoginHandler : IRequestHandler<LoginRequest, (bool Success, string ErrorMessage, AuthenticationResponse Response)>
    {
        private readonly IAccountService _accountService;

        public LoginHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<(bool Success, string ErrorMessage, AuthenticationResponse Response)> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            return await _accountService.LoginAsync(new LoginDTO
            {
                Email = request.Email,
                Password = request.Password
            });
        }
    }
}
