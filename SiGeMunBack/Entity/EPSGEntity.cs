using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EPSGEntity : IEPSGEntity
    {
        public int epsg { get; set; }
        public string texto { get; set; }
    }
}
