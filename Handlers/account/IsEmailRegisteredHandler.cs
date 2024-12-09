using BankApplication.Models.account;
using BankApplication.Service.Account;
using MediatR;

namespace BankApplication.Handlers.account
{
    public class IsEmailRegisteredHandler : IRequestHandler<IsEmailRegisteredRequest, (bool IsRegistered, string ErrorMessage)>
    {
        private readonly IAccountService _accountService;

        public IsEmailRegisteredHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<(bool IsRegistered, string ErrorMessage)> Handle(IsEmailRegisteredRequest request, CancellationToken cancellationToken)
        {
            return await _accountService.IsEmailRegisteredAsync(request.Email);
        }
    }
}
