using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
	///	<summary>
	///	Manages	a routes from a	city to	another	city.
	///	</summary>
	public class Routes
	{
		private List<Link> routes = new List<Link>();
		private Cities cities;

        public delegate void RouteRequestHandler(object sender, RouteRequestEventArgs e);

        public event RouteRequestHandler RouteRequested; 

		public int Count
		{
			get
			{
				return routes.Count;
			}
		}

		///	<summary>
		///	Initializes	the	Routes with	the	cities.
		///	</summary>
		///	<param name="cities"></param>
		public Routes(Cities _cities)
		{
			this.cities = _cities;
		}

		///	<summary>
		///	Reads a	list of	links from the given file.
		///	Reads only links where the cities exist.
		///	</summary>
		///	<param name="filename">name	of links file</param>
		///	<returns>number	of read	route</returns>
		public int ReadRoutes(string _filename)
		{
			using (var reader = new StreamReader(_filename))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					var tokens = line.Split('\t');
					
					var city1 = cities[tokens[0]];
					var city2 = cities[tokens[1]];
					
					// only add links, where both cities are found 
					if ((city1 != null)	&& (city2 != null))
						routes.Add(new Link(city1, city2, city1.Location.Distance(city2.Location), TransportMode.Rail));
				}
			}
			
			return Count;
		}

        //old version 
        //public List<Link> FindShortestRouteBetween(string fromCity, string toCity, TransportMode mode)
        //{
        //          //TODO

        //          if (cities[fromCity].Name == fromCity && cities[toCity].Name == toCity)
        //          {
        //              // Make fromCity name lowercase and then...
        //              string fromCityNameTemp1 = fromCity.ToLower();
        //              // ...make first letter uppercase
        //              string fromCityNameTemp2 = fromCityNameTemp1.First().ToString().ToUpper() + fromCityNameTemp1.Substring(1);

        //              // Make toCity name lowercase and then...
        //              string toCityNameTemp1 = toCity.ToLower();
        //              // ...make first letter uppercase
        //              string toCityNameTemp2 = toCityNameTemp1.First().ToString().ToUpper() + toCityNameTemp1.Substring(1);

        //              RouteRequested?.Invoke(this,
        //                      new RouteRequestEventArgs(new City(fromCityNameTemp2, "", 0, 0, 0),
        //                                                new City(toCityNameTemp2, "", 0, 0, 0), mode));
        //              this.routes = null;                
        //          }
        //          return routes;
        //      }

        public List<Link> FindShortestRouteBetween(string fromCity, string toCity, TransportMode mode)
        {
            //inform listeners
            if (cities[fromCity].Name == fromCity && cities[toCity].Name == toCity)
            {
                // Make fromCity name lowercase and then...
                string fromCityNameTemp1 = fromCity.ToLower();
                // ...make first letter uppercase
                string fromCityNameTemp2 = fromCityNameTemp1.First().ToString().ToUpper() + fromCityNameTemp1.Substring(1);

                // Make toCity name lowercase and then...
                string toCityNameTemp1 = toCity.ToLower();
                // ...make first letter uppercase
                string toCityNameTemp2 = toCityNameTemp1.First().ToString().ToUpper() + toCityNameTemp1.Substring(1);

                RouteRequested?.Invoke(this,
                        new RouteRequestEventArgs(new City(fromCityNameTemp2, "", 0, 0, 0),
                                                  new City(toCityNameTemp2, "", 0, 0, 0), mode));
                this.routes = null;
            }

            //use dijkstra's algorithm to look for all single-source shortest paths
            var visited = new Dictionary<City, DijkstraNode>();
            var pending = new SortedSet<DijkstraNode>(new DijkstraNode[]
            {
                new DijkstraNode()
                {
                    VisitingCity = cities[fromCity],
                    Distance = 0
                }
            });

            while (pending.Any())
            {
                var cur = pending.Last();
                pending.Remove(cur);

                if (!visited.ContainsKey(cur.VisitingCity))
                {
                    visited[cur.VisitingCity] = cur;

                    foreach (var link in GetListOfAllOutgoingRoutes(cur.VisitingCity, mode))
                        pending.Add(new DijkstraNode()
                        {
                            VisitingCity = (link.FromCity == cur.VisitingCity) ? link.ToCity : link.FromCity,
                            Distance = cur.Distance + link.Distance,
                            PreviousCity = cur.VisitingCity
                        });
                }
            }

            //did we find any route?
            if (!visited.ContainsKey(cities[toCity]))
                return null;

            //create a list of cities that we passed along the way
            var citiesEnRoute = new List<City>();
            for (var c = cities[toCity]; c != null; c = visited[c].PreviousCity)
                citiesEnRoute.Add(c);
            citiesEnRoute.Reverse();

            //convert that city-list into a list of links
            IEnumerable<Link> paths = ConvertListOfCitiesToListOfLinks(citiesEnRoute);
            return paths.ToList();
        }

        public Routes GetListOfAllOutgoingRoutes(City visitingCity, TransportMode mode)
        {

        }

        public List<Link> ConvertListOfCitiesToListOfLinks(List<City> cietienEnRoute)
        {

        }



        private class DijkstraNode : IComparable<DijkstraNode>
        {
            public City VisitingCity;
            public double Distance;
            public City PreviousCity;

            public int CompareTo(DijkstraNode other)
            {
                return other.Distance.CompareTo(Distance);
            }
        }
    }
}
