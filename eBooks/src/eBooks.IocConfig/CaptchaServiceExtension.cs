using GoogleReCaptcha.V3;
using Microsoft.Extensions.DependencyInjection;
using GoogleReCaptcha.V3.Interface;

namespace eBooks.IocConfig
{
    public static class CaptchaServiceExtension
    {
        public static IServiceCollection AddCustomCaptchaValidator(this IServiceCollection services)
        {
            services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();

            return services;
        }
    }
}