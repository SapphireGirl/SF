using Serilog;

namespace SF.Logger
{
    public class SFLogger : ISFLogger, IDisposable 
    {
        private readonly ILogger _logger;

        public SFLogger()
        {
            // Initialize Serilog configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();
        }

        public void LogInformation(string message)
        {
            // Implementation for logging information level messages
            Log.Information($"INFO: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message}");
            Log.CloseAndFlush();
        }

        public void Dispose()
        {
            Log.CloseAndFlush();
        }

        public void LogWarning(string message)
        {
            // Implementation for logging warning level messages
            Log.Warning($"WARNING: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message}");
            Log.CloseAndFlush();
        }

        public void LogError(string message, Exception? exception = null)
        {
            // Implementation for logging error level messages
            Log.Error($"ERROR: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message}");
            if (exception != null)
            {
                Log.Error($"Exception: {exception.Message}");
                Log.Error($"StackTrace: {exception.StackTrace}");

            }
            Log.CloseAndFlush();

        }

        public void LogDebug(string message)
        {
            // Implementation for logging debug level messages
            Log.Debug($"DEBUG: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message}");

        }
    }
}
