using System.Globalization;

namespace BankApplication.Middlewares.localization
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var culture = httpContext.Request.Headers["Accept-Language"].ToString();
            if (string.IsNullOrEmpty(culture))
            {
                culture = "en-US";
            }
            else
            {
                var cultureParts = culture.Split(',')
                                          .Select(part => part.Split(';')[0].Trim()) 
                                          .FirstOrDefault(c => IsValidCulture(c));

                culture = cultureParts ?? "en-US";
            }

            var cultureInfo = new CultureInfo(culture);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(httpContext);
        }
        private bool IsValidCulture(string culture)
        {
            try
            {
                CultureInfo.GetCultureInfo(culture);  
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
