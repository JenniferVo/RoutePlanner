using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class City
    {
        public string Name { get; }
        public string Country { get; }
        public int Population { get; }
        public WayPoint Location { get; }

        public City(string name, string country, int population, double latitude, double longitute)
        {
            this.Name = name;
            this.Country = country;
            this.Population = population;
            this.Location = new WayPoint(name, latitude, longitute);
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            City city = obj as City;

            if ((System.Object)city == null)
            {
                return false;
            }

            return (Name == city.Name);
        }

        public override int GetHashCode()
        {
            if (Name == null) return 0;
            return Name.GetHashCode();
        }
    }
}
