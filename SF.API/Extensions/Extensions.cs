using Serilog.Core;
using Serilog;
using Serilog.Exceptions;
using SF.Data.Context;
using SF.Model;
using SF.Data.Repositories;

namespace SF.API.Extensions;
public static class ServiceExtensions
{
    public static void RegisterRepos(this IServiceCollection collection)
    {
        //collection.AddScoped<GenericRepository<Home>, HomeRepository>();
        //collection.AddScoped(typeof(GenericRepository<Home>), typeof(HomeRepository));
        collection.AddTransient<IRepository<Home>, HomeRepository>();

    }

    public static void SetupDbContext(this IServiceCollection collection, IConfigurationRoot config)
    {
        collection.AddSingleton<DapperContext>(provider =>
        {
            // Resolve the AppSettings instance from the service provider
            var connString = config.GetValue<string>("ConnectionStrings:SFConnectionString");
            return new DapperContext(connString);
        });
    }

    public static Serilog.Core.Logger RegisterAndSetupLogging(this WebApplicationBuilder builder, IConfigurationRoot config )
    {
        builder.Host.UseSerilog();

        builder.Services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddSeq(config.GetSection("Seq:MinimumLevel"))
                          .AddConfiguration(config.GetSection("Seq:ServerUrl"));
        });

        var levelSwitch = new LoggingLevelSwitch();

        var log = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(levelSwitch)
            .WriteTo.Seq("http://localhost:5341")
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .CreateLogger();

        builder.Services.AddSingleton<Serilog.ILogger>(log);

        return log;
    }
}