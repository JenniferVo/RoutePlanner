using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;
//test

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{
    class RoutePlannerConsoleApp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to RoutePlanner (Version {0})", Assembly.GetExecutingAssembly().GetName().Version);
            var wayPointTest = new WayPoint("Windisch", 47.479319847061966, 8.212966918945312);
            var wayPointBern = new WayPoint("Bern", 46.947346, 7.447742);
            var wayPointTripolis = new WayPoint("Tripolis", 32.885460, 13.190739);     
            Console.WriteLine(wayPointTest.ToString());
            Console.WriteLine("Distance between Bern and Tripoli: " + wayPointBern.Distance(wayPointTripolis) + " km");
            var citiesTest = new Cities();
            citiesTest.ReadCities(@"C:\Users\Jenny\Desktop\citiesTestDataLab2.txt");            
            //Console.WriteLine("{0}: {1}/{2}", wayPoint.Name, wayPoint.Latitude, wayPoint.Longitude);
            Console.ReadKey();
        }
    }
}
