using System;
using Microsoft.Extensions.Logging;

namespace LoggingKata.Services
{
    //service responsible for formatting and displaying results
    //separates output formatting from business logic
    public class ResultFormatterService
    {
        private readonly ILogger<ResultFormatterService> _logger;

        public ResultFormatterService(ILogger<ResultFormatterService> logger)
        {
            _logger = logger;
        }

        //formats and logs the final result of the farthest locations
        public void DisplayResult(ITrackable location1, ITrackable location2, double distanceMiles)
        {
            var roundedMiles = Math.Round(distanceMiles, 2);
            var message = $"{location1.Name} and {location2.Name} are the TacoBells' that are the farthest apart. The distance is: {roundedMiles} miles!";
            
            _logger.LogInformation(message);
        }
    }
}

