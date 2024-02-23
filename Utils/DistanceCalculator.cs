namespace Mataeem.Utils
{
    public class DistanceCalculator
    {
        private const double EarthRadiusKm = 6371; // Radius of the Earth in kilometers

        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // Convert latitude and longitude from degrees to radians
            double lat1Rad = Math.PI * lat1 / 180.0;
            double lon1Rad = Math.PI * lon1 / 180.0;
            double lat2Rad = Math.PI * lat2 / 180.0;
            double lon2Rad = Math.PI * lon2 / 180.0;

            // Calculate the change in coordinates
            double deltaLat = lat2Rad - lat1Rad;
            double deltaLon = lon2Rad - lon1Rad;

            // Calculate the distance using the Haversine formula
            double a = Math.Pow(Math.Sin(deltaLat / 2), 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Pow(Math.Sin(deltaLon / 2), 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = EarthRadiusKm * c;

            return distance;
        }
    }
}
