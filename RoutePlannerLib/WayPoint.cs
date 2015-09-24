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
                return "Waypoint: " + Name + " " + LongitudeToBeReturned + "/" + LatitudeToBeReturned;
            }
            else
            {
                return "Waypoint: " + LongitudeToBeReturned + "/" + LatitudeToBeReturned;
            }            
        }
    }
}
