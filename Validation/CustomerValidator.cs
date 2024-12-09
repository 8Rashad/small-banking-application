using BankApplication.Entity;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BankApplication.Validation
{
    public class CustomerValidator : AbstractValidator<CustomerEntity>
    {

        private readonly IStringLocalizer<CustomerValidator> _localizer;
        public CustomerValidator(IStringLocalizer<CustomerValidator> localizer)
        {
            _localizer = localizer;

            RuleFor(c => c.FullName).NotEmpty().WithMessage(_localizer["FullNameRequired"]);
            RuleFor(c => c.Email).EmailAddress().WithMessage(_localizer["InvalidEmail"]);
            RuleFor(c => c.PhoneNumber)
                        .NotEmpty()
                        .WithMessage(_localizer["PhoneNumberRequired"])
                        .Matches(@"^\+994\d{9}$")
                        .WithMessage(_localizer["PhoneNumberFormat"]);
            RuleFor(c => c.Address).NotEmpty().WithMessage(_localizer["AddressRequired"]);
            RuleFor(c => c.Balance).GreaterThan(0).WithMessage(_localizer["BalanceGreaterThanZero"]).NotNull().WithMessage(_localizer["BalanceCannotBeNull"]);
            RuleFor(customer => customer.FINCode).NotEmpty().WithMessage(_localizer["FINCodeRequired"]).Length(7).WithMessage(_localizer["FINCodeLength"])
            .Matches("^[A-Z0-9]+$")
            .WithMessage(_localizer["FINCodeFormat"]);
        }
    }
}
    
