using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankApplication.Controller.customer;
using BankApplication.Service.LocalizationService;
using Microsoft.Extensions.Localization;
using MediatR;
using BankApplication.Models.account;


namespace BankApplication.Controller.account
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<CustomerController> _localizer;
        private readonly ILocalizationService _localizationService;

        public AccountController(IMediator mediator, IStringLocalizer<CustomerController> localizer, ILocalizationService localizationService)
        {
            _mediator = mediator;
            _localizer = localizer;
            _localizationService = localizationService;
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="registerDTO">The data transfer object containing user registration details.</param>
        /// <returns>A response with the authentication details if successful, or an error message otherwise.</returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            _localizationService.SetCultureFromRequest();

            var (success, errorMessage, response) = await _mediator.Send(request);
            if (!success)
            {
                var message = _localizer[errorMessage];
                return Problem(message);
            }

            var successMessage = _localizer["RegisterSuccess"];
            return Ok(new { message = successMessage, data = response });
        }

        /// <summary>
        /// Checks if an email address is already registered.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>A response indicating whether the email is already registered.</returns>
        [HttpGet("is-email-registered")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailRegistered(string email)
        {
            _localizationService.SetCultureFromRequest();

            var (isRegistered, errorMessage) = await _mediator.Send(new IsEmailRegisteredRequest { Email = email });
            if (isRegistered)
            {
                var message = _localizer[errorMessage];
                return BadRequest(message);
            }

            var successMessage = _localizer["EmailAvailable"];
            return Ok(new { message = successMessage });
        }

        /// <summary>
        /// Logs in a user using their email and password.
        /// </summary>
        /// <param name="loginDTO">The data transfer object containing login details.</param>
        /// <returns>A response with the authentication details if successful, or an error message otherwise.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            _localizationService.SetCultureFromRequest();

            var (success, errorMessage, response) = await _mediator.Send(request);
            if (!success)
            {
                var message = _localizer[errorMessage];
                return Problem(message);
            }

            var successMessage = _localizer["LoginSuccess"];
            return Ok(new { message = successMessage, data = response });
        }

        /// <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        /// <returns>A 204 No Content response.</returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            _localizationService.SetCultureFromRequest();

            await _mediator.Send(new LogoutRequest());
            var successMessage = _localizer["LogoutSuccess"];
            return NoContent();
        }
    }

}
