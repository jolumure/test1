using Entity;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WASiGeMun.Services
{
    public interface IWcfServSiGeMun
    {
        bool InsertScript(Stream file);
        IEPSGRepository getEPSG(string concepto, string texto);
        IEnumerable<LogEntity> getLog();
        SHPInfo getShpInfo(string localPath);
        SHPInfo putshp(string localPath, string nombreFature, string EPSGOrig, string EPSGDest);
    }
}
