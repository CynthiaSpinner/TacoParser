using System;
using System.Linq;
using System.IO;
//using GeoCoordinatePortable; //OLD: using GeoCoordinate library for 2D distance calculations
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Extensions.Logging;

namespace LoggingKata
{
    class Program
    {
        // Objective: Find the two Taco Bells that are the farthest apart from one another.

        const string csvPath = "TacoBell-US-AL.csv"; // a constant for the csv file named csvPath

        static void Main(string[] args)
        {
            //initialized Serilog and got ILoggerFactory so I can use ILogger interface
            var loggerFactory = Startup.InitializeLogger();
            
            //created ILogger for Program class using the factory, backed by Serilog
            var logger = loggerFactory.CreateLogger<Program>();

            logger.LogInformation("Application Started");

            //NEW: setting up my dependency injection container
            //this configures DI so ILogger gets injected into my services automatically
            var services = new ServiceCollection();
            
            //registering logging with Serilog as the provider
            services.AddLogging(builder => builder.AddSerilog(Serilog.Log.Logger));
            
            //registering DistanceCalculatorService - ILogger will be automatically injected into it
            services.AddTransient<DistanceCalculatorService>();
            
            //building the service provider (DI container)
            var serviceProvider = services.BuildServiceProvider();
            
            logger.LogInformation("Dependency Injection container configured");

            var lines = File.ReadAllLines(csvPath); //creating a variable called lines and storing the csvPath as a string array.

            //this will display the first item in my lines array
            logger.LogInformation($"Lines: {lines[0]}");

            
            var parser = new TacoParser(); //a new instance of my TacoParser class

            //using the Select LINQ method to parse every line in my lines collection
            var locations = lines.Select(parser.Parse).ToArray();// we are transforming each element(line) of lines array to an new array stored in parser and sending it to the Parse method

            logger.LogInformation($"Parsed {locations.Length} locations from CSV file");

            //NEW: using dependency injection to get my DistanceCalculatorService
            //ILogger gets automatically injected into the service constructor
            var distanceCalculator = serviceProvider.GetRequiredService<DistanceCalculatorService>();
            
            //calling the service method - all logging happens inside the service using the injected ILogger
            var (tacoBell1, tacoBell2, meters) = distanceCalculator.FindFarthestLocations(locations);

            double miles2 = ConvertDistance.ConvertMetersToMiles(meters); //Changed meters to miles with convert distance class.
            logger.LogInformation($"Meters converted to miles:{miles2}");

            // I logged the two TacoBell and added the actual distance in miles.
            logger.LogInformation($"{tacoBell1.Name} and {tacoBell2.Name} are the TacoBells' that are the farthest apart. The distance is: {Math.Round(miles2, 2)} miles!");

            //cleaning up by flushing and closing Serilog
            Serilog.Log.CloseAndFlush();
        }
    }
}
