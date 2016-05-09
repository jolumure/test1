using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PointTextEntity
    {
        public WKTPoint originGeom { get; set; }
        public string text { get; set; }
        public double rotation { get; set; }        
    }

    public class WKTPoint
    {
        public double XD { get; set; }
        public double YD { get; set; }
        public decimal SRID { get; set; }
    }
}
