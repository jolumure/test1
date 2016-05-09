using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BLL;
using System.Configuration;

namespace WcfSiGeMun
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfServSiGeMun" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfServSiGeMun.svc or WcfServSiGeMun.svc.cs at the Solution Explorer and start debugging.
    public class WcfServSiGeMun : IWcfServSiGeMun
    {

        public long InsertScript(Stream file)
        {
            long lst;
            string cs = ConfigurationManager.ConnectionStrings["ConStrPG"].ConnectionString;
            ETBatchProcBLL obj = new ETBatchProcBLL(cs);
            lst = obj.InsertScriptBL(file);
            return lst;
        }
    }
}
