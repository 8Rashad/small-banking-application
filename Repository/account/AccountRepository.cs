using BankApplication.Identity;
using BankApplication.Repository.account;
using Microsoft.AspNetCore.Identity;

public class AccountRepository : IAccountRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> RegisterUserAsync(ApplicationUser user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<ApplicationUser?> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task SignInUserAsync(ApplicationUser user, bool isPersistent)
    {
        await _signInManager.SignInAsync(user, isPersistent);
    }

    public async Task<SignInResult> SignInWithPasswordAsync(string email, string password)
    {
        return await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task UpdateUserAsync(ApplicationUser user)
    {
        await _userManager.UpdateAsync(user);
    }
}

