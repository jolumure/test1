using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WASiGeMun.Services;
using Entity;
using System.IO;
using WASiGeMun.Utilities;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Hosting;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WASiGeMun.Controllers
{
    public class SiGeMunBackController : Controller
    {
        private IWcfServSiGeMun servicio;

        public IActionResult Index()
        {
            return View();
        }

        public SiGeMunBackController(IWcfServSiGeMun servicio)
        {
            this.servicio = servicio;
        }

        [HttpPost]
        [Route("api/InsertScript")]
        public string InsertScript()
        {
            string respuesta = "False";
            try
            {
                Stream s = new MemoryStream(new ByteConverter().getByteArray(Request.Body, (long)Request.ContentLength));
                if (this.servicio.InsertScript(s))
                    respuesta = "True";
                else
                    respuesta = "False";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return respuesta;
        }

        [HttpGet]
        [Route("api/getEPSG/{p1}/{p2}")]
        public IActionResult getEPSG(string p1, string p2)
        {
            IEnumerable<EPSGEntity> lista;

            try
            {
                if (p1 != "" && p2 != "")
                {
                    if ((lista = this.servicio.getEPSG(p1, p2).GetAll()) != null)
                        return new ObjectResult(lista);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ObjectResult(e.Message);
            }

            return new ObjectResult("False");
        }

        [HttpGet]
        [Route("api/getLog")]
        public IActionResult getLog()
        {
            IEnumerable<LogEntity> lista;
            try
            {
                if ((lista = this.servicio.getLog()) != null)
                    return new ObjectResult(lista);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ObjectResult(e.Message);
            }

            return new ObjectResult("False");
        }

       
    }
}
