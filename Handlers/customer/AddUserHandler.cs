using BankApplication.Entity;
using BankApplication.Models.customer;
using BankApplication.Service.CustomerService;
using MediatR;
using TaskApi.Response;

namespace BankApplication.Handlers.customer
{
    public class AddUserHandler : IRequestHandler<AddUserRequest, ResponseModel<CustomerEntity>>
    {
        private readonly ICustomerService _customerService;

        public AddUserHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<ResponseModel<CustomerEntity>> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            await _customerService.AddUserAsync(request.User);
            return ResponseModel<CustomerEntity>.Success(request.User, 201);
        }
    }
}
