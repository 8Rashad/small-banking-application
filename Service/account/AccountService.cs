using BankApplication.DTO;
using BankApplication.Identity;
using BankApplication.Repository.account;
using BankApplication.Service.Account;
using BankApplication.ServiceContracts;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IJwtService _jwtService;

    public AccountService(IAccountRepository accountRepository, IJwtService jwtService)
    {
        _accountRepository = accountRepository;
        _jwtService = jwtService;
    }

    public async Task<(bool Success, string ErrorMessage, AuthenticationResponse? Response)> RegisterAsync(RegisterDTO registerDTO)
    {
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            Email = registerDTO.Email,
            PhoneNumber = registerDTO.PhoneNumber,
            UserName = registerDTO.Email,
            PersonName = registerDTO.PersonName,
        };

        var result = await _accountRepository.RegisterUserAsync(user, registerDTO.Password);
        if (!result.Succeeded)
        {
            string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
            return (false, errorMessage, null);
        }

        await _accountRepository.SignInUserAsync(user, isPersistent: false);

        var token = _jwtService.CreateJwtToken(user);
        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpirationDateTime = token.RefreshTokenExpirationDateTime;
        await _accountRepository.UpdateUserAsync(user);

        return (true, null, token);
    }

    public async Task<(bool IsRegistered, string ErrorMessage)> IsEmailRegisteredAsync(string email)
    {
        var user = await _accountRepository.FindByEmailAsync(email);
        return (user != null, user == null ? null : "Email is already registered.");
    }

    public async Task<(bool Success, string ErrorMessage, AuthenticationResponse? Response)> LoginAsync(LoginDTO loginDTO)
    {
        var result = await _accountRepository.SignInWithPasswordAsync(loginDTO.Email, loginDTO.Password);
        if (!result.Succeeded)
        {
            return (false, "Invalid email or password.", null);
        }

        var user = await _accountRepository.FindByEmailAsync(loginDTO.Email);
        if (user == null)
        {
            return (false, "User not found.", null);
        }

        await _accountRepository.SignInUserAsync(user, isPersistent: false);

        var token = _jwtService.CreateJwtToken(user);
        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpirationDateTime = token.RefreshTokenExpirationDateTime;
        await _accountRepository.UpdateUserAsync(user);

        return (true, null, token);
    }

    public async Task LogoutAsync()
    {
        await _accountRepository.SignOutAsync();
    }
}

