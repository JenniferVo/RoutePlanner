using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {
        private List<City> cities;
        public int Count { get; }

        public Cities()
        {
            this.cities = new List<City>();
        }

        public int ReadCities(string filename)
        {                      
            string line;
            char[] delimiters = new char[] { '\t' };

            try
            {
                using (var reader = new StreamReader(filename))
                {
                    while ((line = reader.ReadLine()) != null)
                    {                       
                        string[] parts = line.Split(delimiters);
                        new City(parts[0], parts[1], Int32.Parse(parts[2]), Double.Parse(parts[3]), Double.Parse(parts[4]));    
                    }
                }    
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read: " + e.Message);
            }
            return Count;
        }
    
        public City this[int indexOfCity]
        {            
            get
            {
                if(indexOfCity > cities.Count || indexOfCity < cities.Count)
                {
                    throw new IndexOutOfRangeException("Please enter a valid index!");
                }
                else
                {
                    IEnumerator<City> enumerateOverCities = cities.GetEnumerator();

                    for (int i = 0; i < indexOfCity; i++)
                    {
                        enumerateOverCities.MoveNext();
                    }
                    enumerateOverCities.MoveNext();
                    return enumerateOverCities.Current;
                }
            }
            set { }
        }

        public City this[string cityName]
        {
            get
            {
                City cityFound = cities.Find(delegate (City c)
                {
                    return c.Name == cityName;
                });
                if(cityFound == null)
                {
                    throw new KeyNotFoundException("No such city was found");
                }
                else
                {
                    return cityFound;
                }                
            }
          
            //zwischen Gross- und Kleinschreibung unterscheiden      
        }

        public IEnumerable<City> FindNeighbours(WayPoint location, double distance)
        {            
            return cities.Where(c => location.Distance(c.Location) <= distance).ToList();
        }

    }
}
