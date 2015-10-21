using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public class SimpleObjectWriter
    {
        private TextWriter writer;

        public SimpleObjectWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Next(Object city)
        {

        }
    }
}
