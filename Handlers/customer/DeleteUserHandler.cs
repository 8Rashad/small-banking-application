using BankApplication.Models.customer;
using BankApplication.Service.CustomerService;
using MediatR;
using TaskApi.Response;

namespace BankApplication.Handlers.customer
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, ResponseModel<string>>
    {
        private readonly ICustomerService _customerService;

        public DeleteUserHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<ResponseModel<string>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _customerService.GetUserByIdAsync(request.Id);
            if (user == null)
            {
                return ResponseModel<string>.Error($"User with ID {request.Id} not found", 404);
            }

            await _customerService.DeleteUserAsync(request.Id);
            return ResponseModel<string>.Success("User deleted successfully.");
        }
    }
}
