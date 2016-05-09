using BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WASiGeMun.Services
{
    public class WcfServSiGeMun : IWcfServSiGeMun
    {
        public long InsertScript(Stream file)
        {
            long lst;
            string cs = Startup.ConnectionString;
            if (cs != null && cs != "")
            {
                ETBatchProcBLL obj = new ETBatchProcBLL(cs);
                lst = obj.InsertScriptBL(file);
                return lst;
            }
            else
            {
                return 0L;
            }
        }

        public string getEPSG(string concepto, string texto)
        {
            string cs = Startup.ConnectionString;
            if (cs != null && cs != "")
            {
                ETEPSG obj = new ETEPSG(cs);
                return obj.getEPSG(concepto, texto);
            }
            else
            {
                return "";
            }
        }
    }
}
