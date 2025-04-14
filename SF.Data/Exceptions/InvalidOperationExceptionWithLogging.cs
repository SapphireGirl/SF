using Serilog;

namespace SF.Data.Exceptions
{
    [Serializable]
    public class InvalidOperationExceptionWithLogging : Exception
    {
        ILogger _logger { get; set; }
        public InvalidOperationExceptionWithLogging(ILogger logger)
        {
            _logger = logger.ForContext<InvalidOperationExceptionWithLogging>();
            _logger.Error("Error:");
        }

        public InvalidOperationExceptionWithLogging(ILogger logger, string? message) : base(message)
        {
            _logger = logger.ForContext<InvalidOperationExceptionWithLogging>();
            _logger.Error($"Error: {message}");
        }

        public InvalidOperationExceptionWithLogging(string? message, Exception? innerException) : base(message, innerException)
        {
            _logger = Log.ForContext<InvalidOperationExceptionWithLogging>();
            _logger.Error($"Error: {@Message}: Inner Exception: {@InnerException}", message, innerException);
        }
    }
}