using System;
using System.Linq;
using System.IO;
//using GeoCoordinatePortable; //OLD: using GeoCoordinate library for 2D distance calculations
using Microsoft.Extensions.Logging;

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

            var lines = File.ReadAllLines(csvPath); //creating a variable called lines and storing the csvPath as a string array.

            //this will display the first item in my lines array
            logger.LogInformation($"Lines: {lines[0]}");

            
            var parser = new TacoParser(); //a new instance of my TacoParser class

            //using the Select LINQ method to parse every line in my lines collection
            var locations = lines.Select(parser.Parse).ToArray();// we are transforming each element(line) of lines array to an new array stored in parser and sending it to the Parse method

            logger.LogInformation($"Parsed {locations.Length} locations from CSV file");

            
         
          
            // storing my tacobells'.
            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;

            //store// the distance.
            double distance = 0;
            
            //OLD: detailed debug logging (commented out for less verbose output)
            //logger.LogDebug("Starting distance calculations using 3D vector space (great circle distance)");
            logger.LogInformation("Starting distance calculations using 3D vector space (great circle distance)");
            

            // NESTED LOOPS SECTION----------------------------

            // FIRST FOR LOOP - using 3D vector space for distance calculations
            //creating a loop to go through each item in my collection of locations.
            for (int i = 0; i < locations.Length; i++)// cycles through object's (tacobells) location(latitude and longitude)
            {
                var locA = locations[i];//created variable to store each location updating as the loop cycles
                
                //NEW: converting latitude and longitude to 3D vector coordinates
                var vecA = Vector3D.FromLatLong(locA.Location.Latitude, locA.Location.Longitude);
                //this converts the spherical coordinates (lat, long) to 3D Cartesian coordinates (x, y, z) on Earth's surface
                //OLD: detailed trace logging (commented out for less verbose output)
                //logger.LogTrace($"Converted {locA.Name} (Lat: {locA.Location.Latitude}, Long: {locA.Location.Longitude}) to 3D vector coordinates");

                //OLD CODE - using GeoCoordinate for 2D distance:
                //var corA = new GeoCoordinate(); //Geolocation library to enable location comparisons: using GeoCoordinatePortable; this library has methods for calculating distances between coordinates.
                //corA.Latitude = locA.Location.Latitude;
                //created object of GeoCoordinate, with the location A's latitude/peramitor/property
                //corA.Longitude = locA.Location.Longitude;
                //created object of GeoCoordinate, with location A's longitude/peramitor/property

                for (int j = 0; j < locations.Length; j++) 
                {
                    var locB = locations[j];
                    
                    //NEW: converting second location to 3D vector coordinates
                    var vecB = Vector3D.FromLatLong(locB.Location.Latitude, locB.Location.Longitude);
                    
                    //NEW: calculating great circle distance (arc distance along Earth's surface following curvature)
                    double currentDistance = vecA.DistanceTo(vecB);
                    //OLD: detailed trace logging (commented out for less verbose output)
                    //logger.LogTrace($"Calculated distance between {locA.Name} and {locB.Name}: {currentDistance:F2} meters");
                    
                    //OLD CODE - using GeoCoordinate for 2D distance:
                    //var corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);
                    //GeoCoordinate(double, double) is a class, having 2 properties of Latitude and longitude.
                    //It represents a geographical location that is determined by latitude and longitude.
                    //Note: GeoCoordinate(double, double, double, double, double, double, double)
                    // (latitude, longitude, altitude, horizontal accuracy, vertical accuracy, speed, course) 
                    // collects information like a gyroscope in f-18's

                    //NEW: comparing great circle distance
                    if (currentDistance > distance)// if great circle distance is greater than current max distance
                    {
                        double oldDistance = distance;
                        distance = currentDistance; //updating the distance with great circle distance
                        tacoBell1 = locA; tacoBell2 = locB; // updating tacobell1 and 2
                        //OLD: detailed debug logging (commented out for less verbose output)
                        //logger.LogDebug($"New maximum distance found: {locA.Name} to {locB.Name} = {currentDistance:F2} meters (previous max: {oldDistance:F2} meters)");
                        logger.LogInformation($"New maximum distance found: {locA.Name} to {locB.Name} = {currentDistance:F2} meters (previous max: {oldDistance:F2} meters)");
                    }
                    
                    //OLD CODE - using GeoCoordinate GetDistanceTo method:
                    //if (corA.GetDistanceTo(corB) > distance)// if coordinate A, and Coordinate B is greater then distance
                    //    // a method under the Geocoordinate class to measure the distance between 2 objects.
                    //{
                    //    distance = corA.GetDistanceTo(corB); //updating the distance
                    //    tacoBell1 = locA; tacoBell2 = locB; // updating tacobell1 and 2
                    //}
                }
            }

            //distance is already in meters from great circle distance calculation
            double meters = distance;
            logger.LogInformation($"Completed distance calculations. Total comparisons: {locations.Length * locations.Length}");
            logger.LogInformation($"Maximum distance found: {meters:F2} meters (great circle distance)");

            double miles2 = ConvertDistance.ConvertMetersToMiles(meters); //Changed meters to miles with convert distance class.
            logger.LogInformation($"Meters converted to miles:{miles2}");

            // I logged the two TacoBell and added the actual distance in miles.
            logger.LogInformation($"{tacoBell1.Name} and {tacoBell2.Name} are the TacoBells' that are the farthest apart. The distance is: {Math.Round(miles2, 2)} miles!");

            //cleaning up by flushing and closing Serilog
            Serilog.Log.CloseAndFlush();
        }
    }
}
