using MediatR;
using TaskApi.Response;

namespace BankApplication.Models.customer
{
    public class DeleteUserRequest : IRequest<ResponseModel<string>>
    {
        public int Id { get; set; }
    }
}
