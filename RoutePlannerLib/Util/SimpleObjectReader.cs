using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public class SimpleObjectReader
    {
        private TextReader reader;

        public SimpleObjectReader(TextReader reader)
        {
            this.reader = reader;
        }

        public SimpleObjectReader(StringReader toBeRead)
        {

        }

        public City Next()
        {

        }
    }
}
