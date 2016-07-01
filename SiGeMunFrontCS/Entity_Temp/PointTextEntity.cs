using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Temp
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
        public string AsText
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format(
                    "POINT({0:#.##########} {1:#.##########})",
                    this.XD,
                    this.YD
                    ).Trim()
                );
                return sb.ToString();
            }
        }
    }
}
