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
	public class Routes :IRoutes
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

        #region Lab04: Dijkstra implementation
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

        //with LINQ
        public List<Link> GetListOfAllOutgoingRoutes(City visitingCity, TransportMode mode)
        {
            var allCities = this.routes.Where(r => r.FromCity.Equals(visitingCity) && r.TransportMode.Equals(mode));
            return (List<Link>) allCities;
        }

        //public List<Link> ConvertListOfCitiesToListOfLinks(List<City> citiesEnRoute)
        //{
        //    List<Link> listToBeReturned = new List<Link>();
        //    for(int i = 0; i<citiesEnRoute.Count; i++)
        //    {
        //        Link toBeAdded = new Link(citiesEnRoute[i], citiesEnRoute[i+1], 0);
        //        listToBeReturned.Add(toBeAdded);
        //    }
        //    return listToBeReturned;
        //}

        //as LINQ
        public List<Link> ConvertListOfCitiesToListOfLinks(List<City> citiesEnRoute)
        {
            List<Link> listToBeReturned = new List<Link>();
            City to = null;

            foreach (City c in citiesEnRoute)
            {
                City from = to;
                to = c;
                if(from != null)
                {
                    Link toBeAdded = new Link(from, to, 0);
                    listToBeReturned.Add(toBeAdded);
                }               
            }
            return listToBeReturned;
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
        #endregion

        public City[] FindCities(TransportMode transportMode)
        {
            //get all cities for LINQ to iterate over it
            List<City> allCities = new List<City>();

            //here we get all the cities from  the cities object to fill them in the list
            int numberOfCities = this.cities.Count;
            for (int i = 0; i < numberOfCities; i++)
            {
                allCities.Add(this.cities[i]);
            }            

            //LINQ Query gets all the Link Objects in this.routes that contain the transport mode and puts them into the IEnoumerable list allValidLinks
            var allValidLinks = from linkToBeChecked in this.routes
                                where linkToBeChecked.TransportMode.Equals(transportMode)
                                select linkToBeChecked;

            //lambda funtion for checking if a city is in the toCity or fromCity of a link. lambda function realised with delegate Func
            Func<City, bool> CheckIfCityContained = (City cityToBeChecked) =>
            {
                bool contained = false;
                foreach (var currentLink in allValidLinks)
                {
                    if (currentLink.FromCity.Equals(cityToBeChecked))
                    {
                        contained = true;
                        break;
                    }
                    else if (currentLink.ToCity.Equals(cityToBeChecked))
                    {
                        contained = true;
                        break;
                    }
                }
                return contained;
            };            

            //LINQ Query that fills all cities from list allCities if they are in a to or from location of one of the links with the trasportMode. This check is done with the above specified lambda method.
            var allCitiesToBeReturned = from cityToBeChecked in allCities
                                       where (CheckIfCityContained(cityToBeChecked) == true)
                                       select cityToBeChecked;

            return allCitiesToBeReturned.ToArray();
        }
    }
}
