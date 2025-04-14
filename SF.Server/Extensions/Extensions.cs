using SF.Logger;
using SF.Data.Context;
using SF.Data.Repositories;
using SF.Model;

namespace SF.Server.Extensions;

public static class ServiceExtensions
{
    public static void RegisterRepos(this IServiceCollection collection)
    {
        
        
    }

    public static void SetupDbContext(this IServiceCollection collection)
    {
       
    }

    public static void RegisterLogging(this IServiceCollection collection)
    {
        collection.AddTransient<ISFLogger, SFLogger>();
        collection.AddSingleton<DapperContext>();
        collection.AddTransient<IRepository<Home>, HomeRepository>();
    }
}