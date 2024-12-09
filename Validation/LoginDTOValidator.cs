using BankApplication.DTO;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BankApplication.Validation
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        private readonly IStringLocalizer<LoginDTOValidator> _localizer;

        public LoginDTOValidator(IStringLocalizer<LoginDTOValidator> localizer)
        {
            _localizer = localizer;

            RuleFor(x => x.Email).NotEmpty().WithMessage(_localizer["EmailRequired"]).EmailAddress().WithMessage(_localizer["InvalidEmail"]);

            RuleFor(x => x.Password).NotEmpty().WithMessage(_localizer["PasswordRequired"]);
        }
    }
}
