using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public static class Extensions
    {
        public static IEnumerable<string[]> GetSplittedLines(this TextReader value, char delimiter)
        {
            List<string[]> allStrings = new List<string[]>();
               while (value.Peek() != -1)
               {
                   String currentLine = value.ReadLine();
                   string[] allSubstrings = currentLine.Split(delimiter);
                   allStrings.Add(allSubstrings);
               }            
           return allStrings;
        }
    }
}
