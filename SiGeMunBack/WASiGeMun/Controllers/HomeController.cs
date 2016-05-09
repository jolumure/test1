using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.IO;
using WASiGeMun.Services;
using WASiGeMun.Utilities;

namespace WASiGeMun.Controllers
{
    public class HomeController : Controller
    {
        private IWcfServSiGeMun servicio;

        public HomeController(IWcfServSiGeMun servicio)
        {
            this.servicio = servicio;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("InsertScript")]
        public string InsertScript([FromBody]Byte[] file)
        {
            string archivoS;
            try
            {
                Stream s = new MemoryStream(new ByteConverter().getByteArray(Request.Body, out archivoS));
                this.servicio.InsertScript(s);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return archivoS;
        }

        [HttpGet]
        [Route("getEPSG/{p1}/{p2}")]
        public string getEPSG(string p1, string p2)
        {
            try
            {
                if (p1 != "" && p2 != "")
                    return this.servicio.getEPSG(p1, p2);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return p1 + p2;
        }

    }
}
