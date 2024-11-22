using eBooks.Datalayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using eBooks.DataLayer.Context;
using eBooks.ViewModel.SiteSetting;

namespace eBooks.IocConfig
{
    public static class DbContextServiceExtension
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, SiteSettings siteSettings)
        {
            services.AddDbContext<BestDbContext>(options =>
            {
                options.UseSqlServer(siteSettings.ConnectionStrings.ExcellentSqlServer);
            });

            services.AddScoped<IUnitOfWork, BestDbContext>();

            return services;
        }
    }
}