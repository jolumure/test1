using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Mvc;
using WASiGeMun.Services;
using Entity;
using System.Net;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WASiGeMun.Controllers
{

    [Route("api")]
    public class SiGeMunBackController : ApiController
    {
        private IWcfServSiGeMun servicio;

        public SiGeMunBackController(IWcfServSiGeMun servicio)
        {
            this.servicio = servicio;
        }

        [HttpGet]
        [Route("getEPSG/{p1}/{p2}")]
        public IEnumerable<EPSGEntity> getEPSG(string p1, string p2)
        {
            IEnumerable<EPSGEntity> lista;

            try
            {
                if (p1 != "" && p2 != "")
                {
                    //lista = (IEnumerable<EPSGEntity>)this.servicio.getEPSG(p1, p2);
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                new HttpResponseException(HttpStatusCode.NotFound);
                lista = null;
            }

            return null ;
        }

    }
}
