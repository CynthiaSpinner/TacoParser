using System;

namespace LoggingKata
{
    //3D vector class for calculating great circle distances along Earth's surface
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

        //OLD: calculates the straight-line chord distance through 3D space
        //public double DistanceTo(Vector3D other)
        //{
        //    double dx = X - other.X;
        //    double dy = Y - other.Y;
        //    double dz = Z - other.Z;
        //    
        //    //3D Euclidean distance formula: sqrt((x2-x1)^2 + (y2-y1)^2 + (z2-z1)^2)
        //    return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        //}

        //NEW: calculates great circle distance (arc distance along Earth's surface following curvature)
        //this follows the Earth's topography/curvature, similar to GeoCoordinate.GetDistanceTo()
        public double DistanceTo(Vector3D other)
        {
            const double earthRadiusMeters = 6371000.0; //Earth's radius in meters
            
            //calculate dot product of the two vectors
            double dotProduct = X * other.X + Y * other.Y + Z * other.Z;
            
            //since both vectors are on the surface of a sphere with radius R,
            //the cosine of the angle between them is: dotProduct / (R * R)
            double cosAngle = dotProduct / (earthRadiusMeters * earthRadiusMeters);
            
            //clamp cosAngle to [-1, 1] to avoid numerical errors with arccos
            cosAngle = Math.Max(-1.0, Math.Min(1.0, cosAngle));
            
            //calculate the angle between the two vectors in radians
            double angle = Math.Acos(cosAngle);
            
            //great circle distance = angle (in radians) * Earth's radius
            //this gives us the arc distance along the Earth's curved surface
            return angle * earthRadiusMeters;
        }
    }
}

