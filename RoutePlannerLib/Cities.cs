using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {
        private List<City> cities;
        public int Count { get; private set; }

        public Cities()
        {
            this.cities = new List<City>();
        }

        //old version
        //public int ReadCities(string filename)
        //{                      
        //    string line;
        //    char[] delimiters = new char[] { '\t' };
        //    int countNewCities = 0;

        //        using (var reader = new StreamReader(filename))
        //        {
        //            while ((line = reader.ReadLine()) != null)
        //            {                       
        //                string[] parts = line.Split(delimiters);
        //                // Make name lowercase and then...
        //                string cityNameTemp1 = parts[0].ToLower();
        //                // ...make first letter uppercase
        //                string cityNameTemp2 = cityNameTemp1.First().ToString().ToUpper() + cityNameTemp1.Substring(1);
        //                City newCity = new City(cityNameTemp2, parts[1], Int32.Parse(parts[2]), Double.Parse(parts[3]), Double.Parse(parts[4]));
        //                cities.Add(newCity);
        //                countNewCities++;   
        //            }
        //        }    

        //    this.Count += countNewCities;
        //    return countNewCities;
        //}

        // Lab 4 Version
        //public int ReadCities(string filename)
        //{
        //    int countNewCities = 0;
        //    using (TextReader reader = new StreamReader(filename))
        //    {
        //        IEnumerable<string[]> citiesAsStrings = reader.GetSplittedLines('\t');
        //        foreach (string[] cs in citiesAsStrings)
        //        {
        //            cities.Add(new City(cs[0].Trim(), cs[1].Trim(),
        //                                        int.Parse(cs[2]),
        //                                        double.Parse(cs[3]),
        //                                        double.Parse(cs[4])));
        //        }
        //        this.Count += countNewCities;
        //        return countNewCities;
        //    }
        //}

        //Lab 6 Version
        public int ReadCities(string filename)
        {
            int countNewCities = 0;
            using (TextReader reader = new StreamReader(filename))
            {
                var x = from cs in reader.GetSplittedLines('\t')
                        select new City(cs[0].Trim(), cs[1].Trim(),
                            int.Parse(cs[2]),
                            double.Parse(cs[3]),
                            double.Parse(cs[4]));
                cities.AddRange(x);
            }
            this.Count += countNewCities;
            return countNewCities;

        }

        public City this[int indexOfCity]
        {            
            get
            {
                if(indexOfCity > cities.Count || indexOfCity < 0)
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
                    cityFound = new City(c.Name, c.Country, c.Population, c.Location.Latitude, c.Location.Longitude);                 
                    return c.Name.Equals(cityName, StringComparison.InvariantCultureIgnoreCase);
                });
                if (cityFound == null)
                {
                    throw new KeyNotFoundException("No such city was found");
                }
                else
                {
                    return cityFound;
                }
            }
        }

        public IEnumerable<City> FindNeighbours(WayPoint location, double distance)
        {            
            return cities.Where(c => location.Distance(c.Location) <= distance).ToList();
        }

    }
}
