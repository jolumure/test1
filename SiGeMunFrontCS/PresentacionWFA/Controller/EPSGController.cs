using PresentacionWFA.Entity_temp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FL;
using System.IO;
using System.Configuration;


namespace PresentacionWFA.Controller
{
    class EPSGController
    {
        public List<EPSGEntity> getEPSGEList(string concepto, string texto)
        {
            return setEPSGEntity(new ETEPSG().getEPSGE(concepto, texto, ConfigurationManager.AppSettings["host"], ConfigurationManager.AppSettings["puerto"]));
        }

        private List<EPSGEntity> setEPSGEntity(string respuesta)
        {
            List<EPSGEntity> lista = new List<EPSGEntity>();
            try
            {

                using (StreamReader sr = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(respuesta))))
                {
                    string line;
                    string[] token;
                    int t1 = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        token = line.Split('\t');
                        Int32.TryParse(token[0], out t1);
                        lista.Add(new EPSGEntity(t1, token[1]));
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new List<EPSGEntity>();
            }

            return lista;
            //return new List<EPSGEntity>();*/
        }
    }
}
