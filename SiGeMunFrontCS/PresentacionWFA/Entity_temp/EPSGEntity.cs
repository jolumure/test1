using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentacionWFA.Entity_temp
{
    class EPSGEntity
    {

        public int epsg { get; set; }
        public string text { get; set; }

        public EPSGEntity(int epsg, string srtext)
        {
            this.epsg = epsg;
            this.text = srtext;
        }

        //Falta poner en el Back
        public override string ToString()
        {
            return this.epsg + "\t" + this.text;
        }
    }
}
