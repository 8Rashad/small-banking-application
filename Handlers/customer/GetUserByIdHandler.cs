using BankApplication.Entity;
using BankApplication.Models.customer;
using BankApplication.Service.CustomerService;
using BankApplication.Service.LocalizationService;
using MediatR;
using Microsoft.Extensions.Localization;
using TaskApi.Response;

namespace BankApplication.Handlers.customer
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, ResponseModel<CustomerEntity>>
    {
        private readonly ICustomerService _customerService;
        private readonly IStringLocalizer<GetUserByIdHandler> _localizer;
        private readonly ILocalizationService _localizationService;



        public GetUserByIdHandler(ICustomerService customerService, IStringLocalizer<GetUserByIdHandler> localizer, ILocalizationService localizationService)
        {
            _customerService = customerService;
            _localizer = localizer;
            _localizationService = localizationService;
        }


        public async Task<ResponseModel<CustomerEntity>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            _localizationService.SetCultureFromRequest();
            var user = await _customerService.GetUserByIdAsync(request.Id);
            if (user == null)
            {
                var errorMessage = _localizer["UserNotFound", request.Id];
                return ResponseModel<CustomerEntity>.Error(errorMessage, 404);
            }
            return ResponseModel<CustomerEntity>.Success(user);
        }
    }
}
