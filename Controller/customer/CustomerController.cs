using BankApplication.Entity;
using BankApplication.Models.customer;
using BankApplication.Service.LocalizationService;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BankApplication.Controller.customer
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
         
        }

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="id"> The ID of the user.</param>
        /// <returns>The user object if found, otherwise, a  404 response.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _mediator.Send(new GetUserByIdRequest { Id = id });
            if (!response.IsSuccess)
                return NotFound(response);

            return Ok(response);
        }

        /// <summary>
        /// Gets a list of all users.
        /// </summary>
        /// <returns>A list of user objects.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _mediator.Send(new GetAllUsersRequest());
            return Ok(response);
        }

        /// <summary>
        /// Add a new user.
        /// </summary>
        /// <param name="user">The user object to add.</param>
        /// <returns>The created user object with a 201 status code.</returns>
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] CustomerEntity user)
        {
            var response = await _mediator.Send(new AddUserRequest { User = user });
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, response);
        }

        /// <summary>
        /// Updates the balance of a user.
        /// </summary>
        /// <param name="id">The ID of the user to update</param>
        /// <param name="newBalance">The new balance value.</param>
        /// <returns>A success message if the update is successful</returns>
        [HttpPut("update-balance/{id}")]
        public async Task<IActionResult> UpdateUserBalance(int id, [FromBody] decimal newBalance)
        {
            var response = await _mediator.Send(new UpdateUserBalanceRequest { Id = id, NewBalance = newBalance });
            if (!response.IsSuccess)
                return NotFound(response);

            return Ok(response);
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A success message if the user is deleted successfully</returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _mediator.Send(new DeleteUserRequest { Id = id });
            if (!response.IsSuccess)
                return NotFound(response);

            return Ok(response);
        }
    }
}