using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using Serilog;

namespace LoggingKata
{
    class Program
    {
        // Objective: Find the two Taco Bells that are the farthest apart from one another.

        const string csvPath = "TacoBell-US-AL.csv"; // a constant for the csv file named csvPath

        static void Main(string[] args)
        {
            Startup.InitializeLogger(); //logging initialized with Serilog for TDD purposes.

            Log.Information("Application Started");

            var lines = File.ReadAllLines(csvPath); //creating a variable called lines and storing the csvPath as a string array.

            // This will display the first item in your lines array
            Log.Information($"Lines: {lines[0]}");

            
            var parser = new TacoParser(); //a new instance of your TacoParser class

            // Use the Select LINQ method to parse every line in lines collection
            var locations = lines.Select(parser.Parse).ToArray();// we are transforming each element(line) of lines array to an new array stored in parser and sending it to the Parse method


            
         
          
            // storing my tacobells'.
            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;

            //store// the distance.
            double distance = 0;
            

            // NESTED LOOPS SECTION----------------------------

            // FIRST FOR LOOP -
            //Creating a loop to go through each item in your collection of locations.
            for (int i = 0; i < locations.Length; i++)// cycles through object's (tacobells) location(latitude and longitude)
            {
                var locA = locations[i];//created variable to store each location updating as the loop cycles
                var corA = new GeoCoordinate(); //Geolocation library to enable location comparisons: using GeoCoordinatePortable; Look up what methods you have access to within this library.
                
                corA.Latitude = locA.Location.Latitude;
                //created object of GeoCoordinate, with the location A's latitude/peramitor/property
                corA.Longitude = locA.Location.Longitude;
                //created object of GeoCoordinate, with location A's longitude/peramitor/property

                for (int j = 0; j < locations.Length; j++) 
                {
                    var locB = locations[j];
                    var corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);
                    //GeoCoordinate(double, double) is a class, having 2 properties of Latitude and longitude.
                    //It represents a geographical location that is determined by latitude and longitude.
                    //Note: GeoCoordinate(double, double, double, double, double, double, double)
                    // (latitude, longitude, altitude, horizontal accuracy, vertical accuracy, speed, course) 
                    // collects information like a gyroscope in f-18's

                    if (corA.GetDistanceTo(corB) > distance)// if coordinate A, and Coordinate B is greater then distance
                        // a method under the Geocoordinate class to measure the distance between 2 objects.
                    {
                        distance = corA.GetDistanceTo(corB); //updating the distance
                        tacoBell1 = locA; tacoBell2 = locB; // updating tacobell1 and 2
                    }
                }
            }

            double meters = distance;
            Log.Information($"Meters: {meters}");

            double miles2 = ConvertDistance.ConvertMetersToMiles(meters); //Changed meters to miles with convert distance class.
            Log.Information($"Meters converted to miles:{miles2}");

            // I logged the two TacoBell and added the actual distance in miles.
            Log.Information($"{tacoBell1.Name} and {tacoBell2.Name} are the TacoBells' that are the farthest apart. The distance is: {Math.Round(miles2, 2)} miles!");

        }
    }
}
