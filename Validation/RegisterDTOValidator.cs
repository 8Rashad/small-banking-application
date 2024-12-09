using BankApplication.DTO;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BankApplication.Validation
{
    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        private readonly IStringLocalizer<RegisterDTOValidator> _localizer;

        public RegisterDTOValidator(IStringLocalizer<RegisterDTOValidator> localizer)
        {
            _localizer = localizer;

            RuleFor(r => r.PersonName).NotEmpty().WithMessage(_localizer["PersonNameRequired"]);

            RuleFor(r => r.Email)
            .NotEmpty().WithMessage(_localizer["EmailRequired"])
            .EmailAddress().WithMessage(_localizer["InvalidEmail"])
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage(_localizer["EmailFormat"])
            .Matches("^[^@]+@[a-zA-Z0-9.-]+$").WithMessage(_localizer["InvalidEmailFormat"]);

            RuleFor(r => r.PhoneNumber)
            .NotEmpty().WithMessage(_localizer["PhoneNumberRequired"])
            .Matches(@"^\+994\d{9}$").WithMessage(_localizer["PhoneNumberFormat"]);

            RuleFor(r => r.Password)
           .NotEmpty().WithMessage(_localizer["PasswordRequired"]);

            RuleFor(r => r.ConfirmPassword)
            .NotEmpty().WithMessage(_localizer["ConfirmPasswordRequired"])
            .Equal(r => r.Password).WithMessage(_localizer["PasswordsDoNotMatch"]);
        }
    }
}
