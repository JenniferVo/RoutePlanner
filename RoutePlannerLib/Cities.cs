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
            //TODO: Find path to the txt file          
            string line;
            string filePath = System.IO.Path.GetFullPath("filename");

            try
            {
                using (var reader = new StreamReader(filename))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        char[] delimiters = new char[] { '\t' };
                        string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                        for(int i = 0; i < parts.Length; i++)
                        {
                            Console.WriteLine(parts[i]);                            
                        }
                        
                        //IEnumerable<string[]> allCitiesAsSplittedStrings = reader.GetSplittedLines('\t');           
                        //foreach (string[] currentCity in parts)                     
                        //{
                        //    int tempPopulation;
                        //    double tempLatitude;
                        //    double tempLongtitude;
                        //    string nameTemp = currentCity[0];
                        //    string countryTemp = currentCity[1];
                        //    Int32.TryParse(currentCity[2], out tempPopulation);
                        //    double.TryParse(currentCity[3], out tempLatitude);
                        //    double.TryParse(currentCity[4], out tempLongtitude);
                        //    City newCity = new City(nameTemp, countryTemp, tempPopulation, tempLatitude, tempLongtitude);
                        //    cities.Add(newCity);
                        //    numberOfNewCities++;
                        //}
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
            //TODO: add exception
            get
            {
                IEnumerator<City> enumerateOverCities = cities.GetEnumerator();

                for (int i = 0; i < indexOfCity; i++)
                {
                    enumerateOverCities.MoveNext();
                }
                enumerateOverCities.MoveNext();
                return enumerateOverCities.Current;
            }
            set { }
        }



        public IEnumerable<City> FindNeighbours(WayPoint location, double distance)
        {            
            return cities.Where(c => location.Distance(c.Location) <= distance).ToList();
        }

    }
}
