using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventProcessor.Location
{
    public class GpsUtility
    {
        private const double C_EARTH = 40000.0;

        public double DegToRad(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        /// <summary>
        /// 返回两个GPS坐标之间的距离(以公里为单位)。
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public double DistanceBetweenPoints(GpsCoordinate point1, GpsCoordinate point2)
        {
            double distance = 0.0;


            double lat1Rad = DegToRad(point1.Latitude);
            double long1Rad = DegToRad(point1.Longitude);
            double lat2Rad = DegToRad(point2.Latitude);
            double long2Rad = DegToRad(point2.Longitude);

            double longDiff = Math.Abs(long1Rad - long2Rad);

            if (longDiff > Math.PI)
            {
                longDiff = 2.0 * Math.PI - longDiff;
            }

            double angleCalculation =
                Math.Acos(
                    Math.Sin(lat2Rad) * Math.Sin(lat1Rad) +
                    Math.Cos(lat2Rad) * Math.Cos(lat1Rad) * Math.Cos(longDiff));

            distance = C_EARTH * angleCalculation / (2.0 * Math.PI);

            return distance;
        }
    }
}
