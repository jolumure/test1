using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FL;
using System.IO;
using System.Configuration;
using Entity_Temp;

namespace PresentacionWFA.Controller
{
    class EPSGController
    {
        public IEnumerable<EPSGEntity> getEPSGEList(string concepto, string texto)
        {
            return new ETEPSG().getEPSGE(concepto, texto);
        }
    }
}
