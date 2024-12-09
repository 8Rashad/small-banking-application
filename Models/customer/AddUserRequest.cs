using BankApplication.Entity;
using MediatR;
using TaskApi.Response;

namespace BankApplication.Models.customer
{
    public class AddUserRequest : IRequest<ResponseModel<CustomerEntity>>
    {
        public required CustomerEntity User { get; set; }
    }
}
