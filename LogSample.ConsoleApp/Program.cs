using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LogSample.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Starting ***");

            // Creating a factory for ILooger (with filter config)
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("LogSample.ConsoleApp", LogLevel.Trace) // show all logs
                .AddConsole()
                .AddEventLog();
            });

            // Call a instance a instance
            ILogger logger = loggerFactory.CreateLogger<Program>();
            logger.LogDebug("my ILogger log");

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, loggerFactory);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var logRecord = serviceProvider.GetService<ILogRecord>();
            logRecord.LogLevel();

            Console.WriteLine("*** End ***");
        }

        public static void ConfigureServices(IServiceCollection services, ILoggerFactory loggerFactory)
        {
            //services.AddScoped<ILogRecord, LogRecord>();

            // Configuring a DI with a ILooger
            services.AddScoped<ILogRecord>((container) =>
            {
                ILogger<LogRecord> logger = loggerFactory.CreateLogger<LogRecord>();
                return new LogRecord() { _logger = logger };
            });
        }
    }
}
