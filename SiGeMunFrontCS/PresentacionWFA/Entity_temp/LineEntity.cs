using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentacionWFA.Entity_temp
{
    class LineEntity
    {

        public WKTLine lineGeom { get; set; }
        public string text { get; set; }
        public double rotation { get; set; }
    }

    public class WKTLine
    {
        public List<Coord2dEntity> secXYD { get; set; }
        public decimal SRID { get; set; }
        public string AsText
        {
            get
            {
                StringBuilder sbSecCoord = new StringBuilder();
                int i = 0;
                foreach(Coord2dEntity coord in this.secXYD)
                {
                    if (i == 0)
                    {
                        sbSecCoord.Append(string.Format("{0:#.##########} {1:#.##########}", coord.XD,coord.YD).Trim());
                        i++;
                    }
                    else
                    {
                        sbSecCoord.Append(string.Format(",{0:#.##########} {1:#.##########}", coord.XD, coord.YD).Trim());
                        i++;
                    }
                }
                if (sbSecCoord.ToString().Length > 0)
                {
                    return string.Format("LINESTRING({0})",sbSecCoord.ToString());
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
