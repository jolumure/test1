using Entity_Temp;
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
        public IEnumerable<EPSGEntity> getEPSGE(string concepto, string texto)
        {
            try
            {
                return Requests.getEPSG(concepto, texto).Result;
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
