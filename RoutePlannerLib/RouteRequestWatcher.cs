using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RouteRequestWatcher
    {
        private Dictionary<City, int> requestList;
        private int numberOfRequests;

        public RouteRequestWatcher()
        {
            requestList = new Dictionary<City, int>();
        }

        public void LogRouteRequests(object sender, RouteRequestEventArgs e)
        {
            if (requestList.ContainsKey(e.ToCity))
            {
                int howOften;
                requestList.TryGetValue(e.ToCity, out howOften);
                howOften++;
                requestList.Remove(e.ToCity);
                requestList.Add(e.ToCity, howOften);
            }
            else
            {
                requestList.Add(e.ToCity, 1);
            }
            printRequests();
        }

        public void printRequests()
        {
            var enumeratorInstance = requestList.GetEnumerator();
            if (requestList.Count > 0)
            {
                enumeratorInstance.MoveNext();
                for (int i = 0; i < requestList.Count; i++)
                {
                    City currentCity = enumeratorInstance.Current.Key;
                    int amountOfVisits = enumeratorInstance.Current.Value;
                    string toBeDisplayed = "ToCity: " + currentCity + " has been requested " + amountOfVisits + " times";
                    Console.WriteLine(toBeDisplayed);
                    enumeratorInstance.MoveNext();
                }
            }
        }

        public int GetCityRequests(City cityRequested)
        {
            requestList.TryGetValue(cityRequested, out numberOfRequests);
            return numberOfRequests;
        }
    }
}
