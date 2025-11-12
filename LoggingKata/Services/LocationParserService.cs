using System.Linq;
using Microsoft.Extensions.Logging;

namespace LoggingKata.Services
{
    //service that orchestrates parsing multiple location lines
    //uses ILocationParser interface (dependency inversion principle)
    public class LocationParserService
    {
        private readonly ILocationParser _parser;
        private readonly ILogger<LocationParserService> _logger;

        public LocationParserService(ILocationParser parser, ILogger<LocationParserService> logger)
        {
            _parser = parser;
            _logger = logger;
        }

        //parses all lines and returns an array of trackable locations
        public ITrackable[] ParseAll(string[] lines)
        {
            _logger.LogInformation($"Starting to parse {lines.Length} lines");
            
            //using LINQ Select to transform each line into an ITrackable location
            var locations = lines.Select(_parser.Parse)
                                .Where(loc => loc != null) //filter out null results from invalid lines
                                .ToArray();
            
            _logger.LogInformation($"Successfully parsed {locations.Length} locations from CSV file");
            
            return locations;
        }
    }
}

