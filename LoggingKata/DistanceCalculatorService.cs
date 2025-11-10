using System;
using Microsoft.Extensions.Logging;

namespace LoggingKata
{
    //service class that uses dependency injection for ILogger
    //ILogger gets injected automatically through the constructor
    public class DistanceCalculatorService
    {
        private readonly ILogger<DistanceCalculatorService> _logger;

        //constructor injection - the DI container automatically gives me the ILogger
        public DistanceCalculatorService(ILogger<DistanceCalculatorService> logger)
        {
            _logger = logger;
            _logger.LogDebug("DistanceCalculatorService initialized via dependency injection");
        }

        //finds the two locations that are farthest apart using my 3D vector space calculations
        public (ITrackable location1, ITrackable location2, double distanceMeters) FindFarthestLocations(ITrackable[] locations)
        {
            _logger.LogInformation("Starting distance calculations using 3D vector space (great circle distance)");

            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;
            double maxDistance = 0;

            //creating a loop to go through each item in my collection of locations
            for (int i = 0; i < locations.Length; i++)
            {
                var locA = locations[i];
                
                //converting latitude and longitude to 3D vector coordinates
                var vecA = Vector3D.FromLatLong(locA.Location.Latitude, locA.Location.Longitude);
                //this converts the spherical coordinates (lat, long) to 3D Cartesian coordinates (x, y, z) on Earth's surface

                for (int j = 0; j < locations.Length; j++) 
                {
                    var locB = locations[j];
                    
                    //converting second location to 3D vector coordinates
                    var vecB = Vector3D.FromLatLong(locB.Location.Latitude, locB.Location.Longitude);
                    
                    //calculating great circle distance (arc distance along Earth's surface following curvature)
                    double currentDistance = vecA.DistanceTo(vecB);

                    //comparing my great circle distance
                    if (currentDistance > maxDistance)
                    {
                        double oldDistance = maxDistance;
                        maxDistance = currentDistance;
                        tacoBell1 = locA;
                        tacoBell2 = locB;
                        _logger.LogInformation($"New maximum distance found: {locA.Name} to {locB.Name} = {currentDistance:F2} meters (previous max: {oldDistance:F2} meters)");
                    }
                }
            }

            _logger.LogInformation($"Completed distance calculations. Total comparisons: {locations.Length * locations.Length}");
            _logger.LogInformation($"Maximum distance found: {maxDistance:F2} meters (great circle distance)");

            return (tacoBell1, tacoBell2, maxDistance);
        }
    }
}

