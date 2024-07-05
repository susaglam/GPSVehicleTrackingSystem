using System;namespace YTSDAL
{
    public class DurakTanımlama
    {
        private const double WGS84_a = 6378137;

        private const double WGS84_b = 6356752.3;

        public DurakTanımlama()
        {
        }

        private static double Deg2rad(double degrees)
        {
            return 3.14159265358979 * degrees / 180;
        }

        public static BoundingBox GetBoundingBox(MapPoint point, double halfSideInKm)
        {
            double num = DurakTanımlama.Deg2rad(point.Latitude);
            double num1 = DurakTanımlama.Deg2rad(point.Longitude);
            double num2 = 1000 * halfSideInKm;
            double num3 = DurakTanımlama.WGS84EarthRadius(num);
            double num4 = num3 * Math.Cos(num);
            double num5 = num - num2 / num3;
            double num6 = num + num2 / num3;
            double num7 = num1 - num2 / num4;
            double num8 = num1 + num2 / num4;
            BoundingBox boundingBox = new BoundingBox();
            MapPoint mapPoint = new MapPoint()
            {
                Latitude = DurakTanımlama.Rad2deg(num5),
                Longitude = DurakTanımlama.Rad2deg(num7)
            };
            boundingBox.MinPoint = mapPoint;
            MapPoint mapPoint1 = new MapPoint()
            {
                Latitude = DurakTanımlama.Rad2deg(num6),
                Longitude = DurakTanımlama.Rad2deg(num8)
            };
            boundingBox.MaxPoint = mapPoint1;
            return boundingBox;
        }

        public BoundingBox GetValue(MapPoint point, double halfSideInKm)
        {
            return DurakTanımlama.GetBoundingBox(point, halfSideInKm);
        }

        public bool isBound(MapPoint point, BoundingBox box)
        {
            bool flag = false;
            if (point.Latitude <= box.MaxPoint.Latitude && point.Latitude >= box.MinPoint.Latitude && point.Longitude <= box.MaxPoint.Longitude && point.Longitude >= box.MinPoint.Longitude)
            {
                flag = true;
            }
            return flag;
        }

        private static double Rad2deg(double radians)
        {
            return 180 * radians / 3.14159265358979;
        }

        private static double WGS84EarthRadius(double lat)
        {
            double num = 40680631590769 * Math.Cos(lat);
            double num1 = 40408299803555.3 * Math.Sin(lat);
            double num2 = 6378137 * Math.Cos(lat);
            double num3 = 6356752.3 * Math.Sin(lat);
            return Math.Sqrt((num * num + num1 * num1) / (num2 * num2 + num3 * num3));
        }
    }
}