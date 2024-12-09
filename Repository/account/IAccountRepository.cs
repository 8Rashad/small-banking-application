using BankApplication.Identity;
using Microsoft.AspNetCore.Identity;

namespace BankApplication.Repository.account
{
    public interface IAccountRepository
    {
        Task<IdentityResult> RegisterUserAsync(ApplicationUser user, string password);
        Task<ApplicationUser?> FindByEmailAsync(string email);
        Task SignInUserAsync(ApplicationUser user, bool isPersistent);
        Task SignOutAsync();
        Task<SignInResult> SignInWithPasswordAsync(string email, string password);
        Task UpdateUserAsync(ApplicationUser user);

    }
}
