using Microsoft.AspNetCore.Identity;

namespace BankApplication.Identity
{
    public class ApplicationUser :IdentityUser<string>
    {
        public string? PersonName { get; set; }

        public string? RefreshToken { get; set; }
        private DateTime _refreshTokenExpirationDateTime;

        public DateTime RefreshTokenExpirationDateTime
        {
            get => _refreshTokenExpirationDateTime;
            set => _refreshTokenExpirationDateTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }
}
