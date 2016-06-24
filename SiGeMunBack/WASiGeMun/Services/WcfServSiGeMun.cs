using BLL;
using Entity;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WASiGeMun.Services
{
    public class WcfServSiGeMun : IWcfServSiGeMun
    {
        public bool InsertScript(Stream file)
        {
            bool lst;
            string cs = Startup.ConnectionString;
            if (cs != null && cs != "")
            {
                ETBatchProcBLL obj = new ETBatchProcBLL(cs);
                lst = obj.InsertScriptBL(file);
                return lst;
            }
            else
            {
                return false;
            }
        }

        public IEPSGRepository getEPSG(string concepto, string texto)
        {
            string cs = Startup.ConnectionString;
            if (cs != null && cs != "")
            {
                ETEPSG obj = new ETEPSG(cs);
                return obj.getEPSG(concepto, texto);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<LogEntity> getLog()
        {
            string cs = Startup.ConnectionString;
            if (cs != null && cs != "")
            {
                Log obj = new Log(cs);
                return obj.getLog();
            }
            else
            {
                return null;
            }
        }

        public SHPInfo getShpInfo(string localPath)
        {
            try
            { 
                ETShpInfoBLL obj = new ETShpInfoBLL();
                return obj.getShpInfo(localPath);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public SHPInfo putshp(string localPath, string nombreFature, string EPSGOrig, string EPSGDest)
        {
            try
            {
                string cs = Startup.ConnectionString;
                if (cs != null && cs != "")
                {
                    ETShpInfoBLL obj = new ETShpInfoBLL();
                    return obj.setShp(cs,localPath, nombreFature, EPSGOrig, EPSGDest);
                }
                else
                {
                    return null;
                }
              
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
