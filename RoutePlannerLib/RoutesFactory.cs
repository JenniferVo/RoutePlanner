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
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type t in a.GetTypes())
                {
                    if (t != null)
                    {                        
                        return CallConstructor(t, cities);

                    }
                    throw new NotSupportedException();
                }           
            }
            return null;
        }

        static public IRoutes Create(Cities cities, string algorithmClassName)
        {
            Type type = Type.GetType(algorithmClassName);
            if (type != null)
            {
                return CallConstructor(type, cities);
            }
            throw new NotSupportedException();
        }

        private static IRoutes CallConstructor(Type t, Cities c)
        {
            ConstructorInfo ctor = t.GetConstructor(new Type[] { typeof(Cities) });
            var r = ctor.Invoke(new object[] { c });
            if (r == null)
            {
                throw new NotSupportedException();
            }                
            return (Routes)r;
        }
    }
}
