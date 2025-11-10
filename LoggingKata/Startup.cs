using Serilog;
using Serilog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingKata
{
    public class Startup
    {
        public static ILoggerFactory InitializeLogger() 
        {
            //created logger configuration with Serilog to log information to my log file and to the console, set to create a new log file per day with a time stamp.
            //using Information level for less verbose output (production-ready)
            //OLD: .MinimumLevel.Debug() //uncomment this and comment Information to see detailed Debug/Trace logs
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Information() //set to Information for less verbose output, change to Debug for detailed logging
                .WriteTo.Console()
                .WriteTo.File(path: "logs/LogInfo_TacoParser-.txt", rollingInterval: RollingInterval.Day);

            //created the Serilog logger
            Log.Logger = loggerConfiguration.CreateLogger();

            //created ILoggerFactory with Serilog as the provider so I can use ILogger interface
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddSerilog(Log.Logger);
            });

            return loggerFactory;
        }
    }
}
// using both Serilog and ILogger together - Serilog handles the file logging and console output,
// and ILogger gives me the standard .NET interface for dependency injection and testing