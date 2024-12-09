using BankApplication.Models.customer;
using BankApplication.Service.CustomerService;
using MediatR;
using TaskApi.Response;
namespace BankApplication.Handlers.customer
{
    public class UpdateUserBalanceHandler : IRequestHandler<UpdateUserBalanceRequest, ResponseModel<string>>
    {
        private ICustomerService _customerService;

        public UpdateUserBalanceHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<ResponseModel<string>> Handle(UpdateUserBalanceRequest request, CancellationToken cancellationToken)
        {
            var user = await _customerService.GetUserByIdAsync(request.Id);
            if (user == null)
                return ResponseModel<string>.Error($"User with ID {request.Id} not found.", 404);

            user.Balance = request.NewBalance;
            await _customerService.UpdateUserAsync(user);
            return ResponseModel<string>.Success("Balance updated successfully");
        }
    }
}
