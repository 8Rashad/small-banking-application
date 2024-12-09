using BankApplication.DTO;
using BankApplication.Identity;

namespace BankApplication.ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user);
    }
}
