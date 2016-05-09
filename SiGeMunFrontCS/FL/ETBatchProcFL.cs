/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FL
{
    public class ETBatchProcFL
    {
        public long insertScript(Byte[] zip)
        {
            WCFServSiGeMun.WcfServSiGeMunClient serv = new WCFServSiGeMun.WcfServSiGeMunClient();
            Stream s = new MemoryStream(zip);
            return serv.InsertScript(s);
        }
    }
}*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FL.WCFServSiGeMun;
using System.Net;
using System.Net.Http;
using HTTPService;

namespace FL
{

    public class ETBatchProcFL
    {
        /*public long insertScript(Byte[] Bzip)
        {
            string url = "http://localhost:51640/InsertScript";

            //Convertir Byte[] a una cadena de String 89 574 87  
            //El problema aquí es que el peso del mensaje se multiplica. 
            string datos = "";

            for (int i = 0; i <= Bzip.Length - 1; i++)
                datos += Bzip[i].ToString() + " ";

            string datosJson = "{\"data\":\"" + datos + "\", \"tamanio\" : " + Bzip.Length.ToString() + "}";
            long respuesta = 1L;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";

            //codificando los datos de la solicitud 
            byte[] postBytes = Encoding.UTF8.GetBytes(datosJson);
            req.ContentLength = postBytes.Length;


            //Dando formato a los datos de la solicitud
            using (Stream dataStream = req.GetRequestStream())
            {
                dataStream.Write(postBytes, 0, postBytes.Length);
            }

            //Obteniendo una respuesta
            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string res = reader.ReadToEnd();

                    if (res == "")
                        respuesta = 1L;
                    else
                    {
                        if (res == datos)
                            respuesta = 1L;
                        else
                            respuesta = 0L;
                    }
                }
            }

            return respuesta;
             WCFServSiGeMun.WcfServSiGeMunClient serv = new WCFServSiGeMun.WcfServSiGeMunClient();
             //Stream s = new MemoryStream(zip);
             //return serv.InsertScript(s);
        }*/

        public void insertScript(string nombre, string path, Byte[] Bzip, string puerto, string host)
        {
            new Requests(puerto, host).insertScript(path + "/" + nombre, Bzip.Length);
        }
    }
}
