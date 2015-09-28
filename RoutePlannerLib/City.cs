using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class City
    {
        string Name;
        string Country;
        int Population;
        public WayPoint Location;

        public City(string name, string country, int population, double latitude, double longitute)
        {
            this.Name = name;
            this.Country = country;
            this.Location = new WayPoint(name, latitude, longitute);
        }
    }
}
