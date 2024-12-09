using BankApplication.Entity;
using BankApplication.Models.customer;
using BankApplication.Service.CustomerService;
using MediatR;
using TaskApi.Response;

namespace BankApplication.Handlers.customer
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, ResponseModel<IEnumerable<CustomerEntity>>>
    {
        private readonly ICustomerService _customerService;

        public GetAllUsersHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public async Task<ResponseModel<IEnumerable<CustomerEntity>>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await _customerService.GetAllUsersAsync();
            return ResponseModel<IEnumerable<CustomerEntity>>.Success(users);
        }
    }
}
