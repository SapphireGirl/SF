using SF.Data.Context;
using SF.Data.Repositories;
using SF.Data.Services;
using SF.Model;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Events;
using Serilog.Core;
using ILogger = Serilog.ILogger;

namespace SF.API.Extensions;

public static class ServiceExtensions
{
    public static void RegisterRepos(this IServiceCollection collection)
    {
        collection.AddSingleton<DapperContext>();
        collection.AddScoped<IHomeRepository, HomeRepository>();
        collection.AddScoped(typeof(IGenericRepository<Home>), typeof(HomeRepository));
    }

    public static void SetupDbContext(this IServiceCollection collection)
    {
       
    }

    public static void RegisterLogging(this IServiceCollection collection)
    {
        //collection.AddSingleton<Microsoft.Extensions.Logging.ILogger>(provider =>
        //{
        //    var logger = new LoggerConfiguration()
        //        .MinimumLevel.Debug()
        //        .WriteTo.Seq("http://localhost:5341")
        //        .CreateLogger();

        //    return new SerilogLoggerProvider(logger).CreateLogger("SF.API");
        //});

    }

    public static void RegisterAuth(this IServiceCollection collection)
    {
        //Register authentication services.
    }

    public static void InitLogging(this IServiceCollection collection)
    {
        //var levelSwitch = new LoggingLevelSwitch();
        //var log = new LoggerConfiguration();
            
        //collection.AddSingleton<Serilog.ILogger>(provider =>
        //{
        //   log
        //   .MinimumLevel.ControlledBy(levelSwitch)
        //   .WriteTo.Seq("http://localhost:5341")
        //   .CreateLogger();

        //    return new SerilogLoggerProvider(log).CreateLogger("SF.API");
        //});
       // return new SerilogLoggerProvider(log).CreateLogger("SF.API");
    }
}