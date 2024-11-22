using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBooks.AutoMapperProfiles;

namespace eBooks.IocConfig
{
    public static class AutoMapperServicesExtensions
    {
        public static void AddAutoMapperServices(this IServiceCollection services)
        {
            var profiles =
                from t in typeof(AutoMapperRegistryProfile).Assembly.GetTypes()
                where typeof(Profile).IsAssignableFrom(t)
                select (Profile)Activator.CreateInstance(t);

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });
            services.AddScoped<IMapper>(factory => factory.GetService<MapperConfiguration>()!.CreateMapper());
            services.AddScoped<MapperConfiguration>(factory => config);
        }
    }
}