using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingKata
{
    //OLD: static utility class for distance conversion
    //NEW: replaced by DistanceConverterService with dependency injection
    //keeping this for backward compatibility if needed, but new code should use DistanceConverterService
    [Obsolete("Use DistanceConverterService instead for better testability and dependency injection")]
    public class ConvertDistance
    {
        public static double ConvertMetersToMiles(double distance) 
        {
            return distance / Constants.MetersToMiles;
        }
    }
}
