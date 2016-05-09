using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WASiGeMun.Services
{
    public interface IWcfServSiGeMun
    {
        long InsertScript(Stream file);
        string getEPSG(string concepto, string texto);
    }
}
