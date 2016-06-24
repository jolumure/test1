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
        public IActionResult Index()
        {
            return View();
        }
        /*private IWcfServSiGeMun servicio;

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
            bool respuesta = false;
            try
            {
                Stream s = new MemoryStream(new ByteConverter().getByteArray(Request.Body, (long)Request.ContentLength));
                respuesta = this.servicio.InsertScript(s);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            if(respuesta == false)
                return "False";

            return respuesta.ToString();
        }

        [HttpGet]
        [Route("getEPSG/{p1}/{p2}")]
        public string getEPSG(string p1, string p2)
        {
            try
            {
                if (p1 != "" && p2 != "")
                    return this.servicio.getEPSG(p1, p2).ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return p1 + p2;
        }*/

    }
}
