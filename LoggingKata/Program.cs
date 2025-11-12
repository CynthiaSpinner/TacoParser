using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using LoggingKata.Services;

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

            //configuring all services using centralized service configuration
            //this sets up dependency injection for all my services
            var serviceProvider = ServiceConfiguration.ConfigureServices();
            
            logger.LogInformation("Dependency Injection container configured");

            //using services to handle file reading
            var fileReader = serviceProvider.GetRequiredService<CsvFileReaderService>();
            var lines = fileReader.ReadAllLines(csvPath);

            //displaying the first line as a sample
            logger.LogInformation($"First line sample: {lines[0]}");

            //using location parser service to parse all lines
            var locationParser = serviceProvider.GetRequiredService<LocationParserService>();
            var locations = locationParser.ParseAll(lines);

            //using distance calculator service to find farthest locations
            var distanceCalculator = serviceProvider.GetRequiredService<DistanceCalculatorService>();
            (ITrackable tacoBell1, ITrackable tacoBell2, double meters) = distanceCalculator.FindFarthestLocations(locations);

            //using distance converter service to convert meters to miles
            var distanceConverter = serviceProvider.GetRequiredService<DistanceConverterService>();
            var miles = distanceConverter.ConvertMetersToMiles(meters);
            logger.LogInformation($"Meters converted to miles: {miles:F2}");

            //using result formatter service to display the final result
            var resultFormatter = serviceProvider.GetRequiredService<ResultFormatterService>();
            resultFormatter.DisplayResult(tacoBell1, tacoBell2, miles);

            //cleaning up by flushing and closing Serilog
            Serilog.Log.CloseAndFlush();
        }
    }
}
