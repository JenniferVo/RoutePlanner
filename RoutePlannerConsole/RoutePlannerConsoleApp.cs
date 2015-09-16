using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{
    class RoutePlannerConsoleApp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to RoutePlanner (Version {0})", Assembly.GetExecutingAssembly().GetName().Version);
            Console.ReadKey();
        }
    }
}
