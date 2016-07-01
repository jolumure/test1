using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WASiGeMun.Services;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using System.IO;
using System.Net.Http.Headers;
using Entity;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WASiGeMun.Controllers
{
    public class UploadingController : Controller
    {
        private IWcfServSiGeMun servicio;
        private IHostingEnvironment _environment;

        public UploadingController(IHostingEnvironment environment)
        {
            _environment = environment;
            this.servicio = new WcfServSiGeMun();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("api/getshpinfo")]
        public IActionResult getshpinfo(IFormFile file)
        {
            try
            {
                SHPInfo shpInfo = new SHPInfo();
                //var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                var uploads = Path.Combine(Startup.Dir_tmp__32);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    file.SaveAs(Path.Combine(uploads, fileName));
                    shpInfo = this.servicio.getShpInfo(Path.Combine(uploads, fileName));
                }
                return new ObjectResult(shpInfo.ToJSON());
            }
            catch(Exception ex)
            {
                return new ObjectResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/putshp/{departamento}/{nombreFeature}/{EPSGOrig}/{EPSGDest}")]
        //
        public IActionResult putshp(IFormFile file, string departamento, string nombreFeature, string EPSGOrig, string EPSGDest)
        {
            try
            {
                SHPResultInsert result = new SHPResultInsert();
                var uploads = Path.Combine(Startup.Dir_tmp__32);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    file.SaveAs(Path.Combine(uploads, fileName));
                    result = this.servicio.putshp(Path.Combine(uploads, fileName), departamento, nombreFeature, EPSGOrig, EPSGDest);
                }
                return new ObjectResult(result.ToJSON());
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }
        }
    }
}





//[HttpPost]
//[Route("api/shp/getinfo")]
//public IActionResult getShpInfo(Stream fileZip)
//{
//    SHPInfo retVal;
//    try
//    {
//        //if(fileZip.Length>0)
//        //{
//        //var targetDirectory = Path.Combine(, string.Format("Content\\Uploaded\\"));
//        //var fileName = GetFileName(fileZip);
//        //var savePath = Path.Combine(targetDirectory, fileName);

//        //fileZip.SaveAs(@"c:\temp\filename.zip");
//        //return Json(new { Status = "Ok" });
//        //}
//        var input = new StreamReader(Request.Body).ReadToEnd();
//        Stream s = new MemoryStream(new ByteConverter().getByteArray(Request.Body, (long)Request.ContentLength));
//        //retVal = this.servicio.getShpInfo(s);
//        return new ObjectResult(new SHPInfo());
//    }
//    catch (Exception e)
//    {
//        return new ObjectResult(e.Message);
//    }
//}