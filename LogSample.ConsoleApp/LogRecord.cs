using System;
using Microsoft.Extensions.Logging;

namespace LogSample.ConsoleApp
{
    public class LogRecord : ILogRecord
    {
        public ILogger<LogRecord> _logger;

        public LogRecord()
        {
        }

        public LogRecord(ILogger<LogRecord> logger)
        {
            _logger = logger;
        }

        public void LogLevel()
        {
            Console.WriteLine("In : logLevel");

            _logger.LogTrace("my LogTrace");
            _logger.LogDebug("my LogDebug");
            _logger.LogInformation("my LogInformation");
            _logger.LogWarning("my LogWarning");
            _logger.LogError("my LogError");
            _logger.LogCritical("my LogCritical");
            _logger.Log(Microsoft.Extensions.Logging.LogLevel.None, "my Log None");
        }
    }
}