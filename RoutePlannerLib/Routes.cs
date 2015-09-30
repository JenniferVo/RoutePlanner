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

		public List<Link> FindShortestRouteBetween(string fromCity, string toCity, TransportMode mode)
		{
            //TODO
            RouteRequested?.Invoke(this,
                    new RouteRequestEventArgs(new City(fromCity, "", 0, 0, 0),
                                              new City(toCity, "", 0, 0, 0), mode));
            this.routes = null;
            return routes;
		}
	}
}