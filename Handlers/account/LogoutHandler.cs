using BankApplication.Models.account;
using BankApplication.Service.Account;
using MediatR;

namespace BankApplication.Handlers.account
{
    public class LogoutHandler : IRequestHandler<LogoutRequest, Unit>
    {
        private readonly IAccountService _accountService;

        public LogoutHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<Unit> Handle(LogoutRequest request, CancellationToken cancellationToken)
        {
            await _accountService.LogoutAsync();
            return Unit.Value;
        }
    }


}
