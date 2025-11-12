using Microsoft.Extensions.Logging;

namespace LoggingKata.Services
{
    //service for converting distance units
    //separates unit conversion logic into its own service
    public class DistanceConverterService
    {
        private readonly ILogger<DistanceConverterService> _logger;

        public DistanceConverterService(ILogger<DistanceConverterService> logger)
        {
            _logger = logger;
        }

        //converts meters to miles using the standard conversion factor
        public double ConvertMetersToMiles(double meters)
        {
            var miles = meters / Constants.MetersToMiles;
            _logger.LogDebug($"Converted {meters:F2} meters to {miles:F2} miles");
            return miles;
        }
    }
}

