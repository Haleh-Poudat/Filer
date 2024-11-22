using eBooks.IocConfig;
using eBooks.ServiceLayer.Contracts.Libraray;
using eBooks.ServiceLayer.Contracts.Logs;
using eBooks.ServiceLayer.Services.Libraray;
using eBooks.ServiceLayer.Services.Logs;
using eBooks.ViewModel.SiteSetting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eBooks.IocConfig
{
    public static class AddCustomServicesExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            var siteSettings = GetSiteSettings(configuration);
            services.AddCustomDbContext(siteSettings);
            services.AddAutoMapperServices();
            services.AddCustomIdentityServices(siteSettings);
            services.AddCustomCaptchaValidator();
            services.AddScoped<IEbookService, EbookService>();
            services.AddScoped<ILibraryCategoryService, LibraryCategoryService>();

            return services;
        }

        private static SiteSettings GetSiteSettings(this IConfiguration configuration)
        {
            return configuration.Get<SiteSettings>();
        }
    }
}