using BankApplication.Entity;
using MediatR;
using TaskApi.Response;

namespace BankApplication.Models.customer
{
    public class GetUserByIdRequest : IRequest<ResponseModel<CustomerEntity>>
    {
        public int Id { get; set; }
    }
}
