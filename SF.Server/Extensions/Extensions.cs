using SF.Logger;

namespace FizzBuzz.Core.Extensions;

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
    }

    public static void RegisterAuth(this IServiceCollection collection)
    {
        //Register authentication services.
    }
}