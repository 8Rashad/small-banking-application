using MediatR;
using TaskApi.Response;

namespace BankApplication.Models.customer
{
    public class UpdateUserBalanceRequest : IRequest<ResponseModel<string>>
    {
        public int Id { get; set; }
        public decimal NewBalance { get; set; }
    }
}
