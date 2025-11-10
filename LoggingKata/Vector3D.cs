using System;

namespace LoggingKata
{
    //3D vector class for calculating distances in 3D space
    public struct Vector3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        //converts latitude and longitude to 3D coordinates on Earth's surface
        //Earth's radius in meters is approximately 6,371,000 meters
        public static Vector3D FromLatLong(double latitude, double longitude)
        {
            const double earthRadiusMeters = 6371000.0; //Earth's radius in meters
            
            //convert degrees to radians
            double latRad = latitude * Math.PI / 180.0;
            double longRad = longitude * Math.PI / 180.0;

            //convert spherical coordinates (lat, long) to 3D Cartesian coordinates (x, y, z)
            double x = earthRadiusMeters * Math.Cos(latRad) * Math.Cos(longRad);
            double y = earthRadiusMeters * Math.Cos(latRad) * Math.Sin(longRad);
            double z = earthRadiusMeters * Math.Sin(latRad);

            return new Vector3D(x, y, z);
        }

        //calculates the straight-line distance between two 3D vectors
        public double DistanceTo(Vector3D other)
        {
            double dx = X - other.X;
            double dy = Y - other.Y;
            double dz = Z - other.Z;
            
            //3D Euclidean distance formula: sqrt((x2-x1)^2 + (y2-y1)^2 + (z2-z1)^2)
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
    }
}

