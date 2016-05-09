using HTTPService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FL
{
    public class ETEPSG
    {
        public string getEPSGE(string concepto, string texto, string host, string puerto)
        {
            return new Requests(host, puerto).getEPSG(concepto, texto);
        }
    }
}
