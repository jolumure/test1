using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ETEPSG
    {
        private string cs;

        public ETEPSG(string cs)
        {
            this.cs = cs;
        }

        public string getEPSG(string concepto, string texto)
        {
            ETEPSGDAL obj = new ETEPSGDAL(cs);
            return obj.getEPSG(concepto, texto);
        }
    }
}
