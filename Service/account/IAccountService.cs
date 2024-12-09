using BankApplication.DTO;

namespace BankApplication.Service.Account
{
    public interface IAccountService
    {
        Task<(bool Success, string ErrorMessage, AuthenticationResponse? Response)> RegisterAsync(RegisterDTO registerDTO);
        Task<(bool IsRegistered, string ErrorMessage)> IsEmailRegisteredAsync(string email);
        Task<(bool Success, string ErrorMessage, AuthenticationResponse? Response)> LoginAsync(LoginDTO loginDTO);
        Task LogoutAsync();
    }
}
