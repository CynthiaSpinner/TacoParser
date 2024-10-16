using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingKata
{
    public class Startup
    {
        public static void InitializeLogger() 
        {
            //created logger with Serilog to log information to my log file and to the console for TDD purposes, set to create a new log file per day with a time stamp.
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(path: "logs/LogInfo_TacoParser-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
