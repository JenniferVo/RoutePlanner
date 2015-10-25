using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RoutesFactory
    {
        static public IRoutes Create(Cities cities)
        {
            //Assembly[] temp = AppDomain.CurrentDomain.GetAssemblies();

            //foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            //{

            //    foreach (Type t in a.GetTypes())
            //    {
            //        if (t != null)
            //        {                        
            //            //return CallConstructor(t, cities);

            //        }
            //        //throw new NotSupportedException();
            //    }           
            //}
            //return null;

            String path = Settings.Default.RouteAlgorithm;
            Type type = Assembly.GetExecutingAssembly().GetType(path);

            if (type.GetInterface("IRoutes") != null)
            {

                Object[] param = { cities };

                return (IRoutes)Activator.CreateInstance(type, param);

            }

            return null;
        }

        static public IRoutes Create(Cities cities, string algorithmClassName)
        {
            try
            {
                Type type = Assembly.GetExecutingAssembly().GetType(algorithmClassName);
                if (type.GetInterface("IRoutes") != null)
                {

                    Object[] param = { cities };

                    return (IRoutes)Activator.CreateInstance(type, param);

                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //private static IRoutes CallConstructor(Type t, Cities c)
        //{
        //    ConstructorInfo ctor = t.GetConstructor(new Type[] { typeof(Cities) });
        //    var r = ctor.Invoke(new object[] { c });
        //    if (r == null)
        //    {
        //        throw new NotSupportedException();
        //    }                
        //    return (Routes)r;
        //}
    }
}
