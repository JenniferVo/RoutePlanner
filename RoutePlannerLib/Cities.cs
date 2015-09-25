﻿using System;
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

        public Cities()
        {
            this.cities = new List<City>();
        }

        public int ReadCities(string filename)
        {
            int numberOfNewCities = 0;
            string line;

            try
            {
                using (var reader = new StreamReader("filename"))
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
            return numberOfNewCities;
        }

    }
}
