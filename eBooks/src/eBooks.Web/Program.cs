using eBooks.DataLayer.Context;
using eBooks.Datalayer.SeedExtension;
using eBooks.IocConfig;
using Microsoft.EntityFrameworkCore;
using eBooks.ViewModel.SiteSetting;
using NLog;
using NLog.Web;
using System;
using eBooks.Web.Middlewares;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using System.Security.Claims;
using eBooks.ServiceLayer.Contracts.Logs;
using eBooks.ServiceLayer.Services.Logs;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllersWithViews();
    builder.Services.Configure<SiteSettings>(options => builder.Configuration.Bind(options));
    builder.Services.AddCustomServices(builder.Configuration);
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddScoped<IElmahLogService, ElmahLogService>();
    builder.Services.AddElmah<SqlErrorLog>(options =>
    {
        options.Path = "/elmah-log";
        options.ConnectionString = builder.Configuration.GetConnectionString("Elmah");
        options.OnPermissionCheck = context =>
        {
            return context.User.HasClaim(claim => claim is { Type: ClaimTypes.System, Value: "True" });
        };
    });

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseMiddleware<ExceptionLoggingMiddleware>();
    app.UseElmah();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Library}/{action=Index}/{id?}");
    });

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<BestDbContext>();
        if (context != null)
        {
            await context.Database.MigrateAsync();
            context.Seed();
        }
    }

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}