using BankApplication.Entity;
using MediatR;
using TaskApi.Response;

namespace BankApplication.Models.customer
{
    public class GetAllUsersRequest : IRequest<ResponseModel<IEnumerable<CustomerEntity>>>
    {
    }
}
