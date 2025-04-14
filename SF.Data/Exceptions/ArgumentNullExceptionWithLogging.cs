using Serilog;

namespace SF.Data.Exceptions
{
    public class ArgumentNullExceptionWithLogging : ArgumentNullException
    {
        ILogger _logger { get; set; }
        public ArgumentNullExceptionWithLogging(string? paramName, ILogger logger) : base(paramName)
        {
            _logger = logger.ForContext<ArgumentNullExceptionWithLogging>();
            _logger.Error("Error: paramName: {@ParamName}", paramName);
        }

        public ArgumentNullExceptionWithLogging(string? message, Exception? innerException, ILogger logger) : base(message, innerException)
        {
            _logger = logger.ForContext<ArgumentNullExceptionWithLogging>();
            _logger.Error("Error: {@Message}", message);

        }

        public ArgumentNullExceptionWithLogging(string? paramName, string? message, ILogger logger) : base(paramName, message)
        {
            _logger = logger.ForContext<ArgumentNullExceptionWithLogging>();
            _logger.Error("Error: paramName: {@ParamName} : {@Message}", paramName, message);

        }
    }
}
