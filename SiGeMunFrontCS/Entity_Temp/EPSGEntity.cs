using Entity_Temp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Temp
{
    public class EPSGEntity : IEPSGEntity
    {
        public int epsg { get; set; }
        public string texto { get; set; }

        public override string ToString()
        {
            return this.epsg + "    " + this.texto;
        }
    }
}
