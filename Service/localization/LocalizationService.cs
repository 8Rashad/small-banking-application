using System.Globalization;

namespace BankApplication.Service.LocalizationService
{
    public class LocalizationService : ILocalizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocalizationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetCultureFromRequest()
        {
            var acceptLanguageHeader = _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();

            if (!string.IsNullOrEmpty(acceptLanguageHeader))
            {
                try
                {
                    var currentCulture = acceptLanguageHeader.Split(',').FirstOrDefault()?.Trim();

                    if (!string.IsNullOrEmpty(currentCulture))
                    {
                        var cultureInfo = new CultureInfo(currentCulture);
                        CultureInfo.CurrentCulture = cultureInfo;
                        CultureInfo.CurrentUICulture = cultureInfo;
                    }
                }
                catch (CultureNotFoundException)
                {
                    CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
                    CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
                }
            }
        }
    }
}
