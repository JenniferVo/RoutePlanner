using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class WayPoint
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public WayPoint(string _name, double _latitude, double _longitude)
        {
            this.Name = _name;
            this.Latitude = _latitude;
            this.Longitude = _longitude;
        }

        public override string ToString()
        {
            string LongitudeToBeReturned = String.Format("{0:0.00}", Longitude);
            string LatitudeToBeReturned = String.Format("{0:0.00}", Latitude);

            if (this.Name != null)
            {
                return "Waypoint: " + Name + " " + LatitudeToBeReturned + "/" + LongitudeToBeReturned;
            }
            else
            {
                return "Waypoint: " + LatitudeToBeReturned + "/" + LongitudeToBeReturned;
            }            
        }

        public double Distance(WayPoint _target)
        {
            const int radius = 6371;
            var d =
                Math.Acos(Math.Sin(Latitude * Math.PI / 180) * Math.Sin(_target.Latitude * Math.PI / 180) +
                          Math.Cos(Latitude * Math.PI / 180) * Math.Cos(_target.Latitude * Math.PI / 180) *
                          Math.Cos(Longitude * Math.PI / 180 - _target.Longitude * Math.PI / 180));
            return d * radius;
        }
    }
}
