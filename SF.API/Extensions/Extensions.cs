using SF.Data.Context;
using SF.Data.Repositories;
using SF.Model;

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

    }

    public static void RegisterAuth(this IServiceCollection collection)
    {
        //Register authentication services.
    }

    public static void InitLogging(this IServiceCollection collection)
    {

    }
}