using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HTTPService
{
    public class Requests
    {

        #region constantes
        private string URL;
        private const string BOUNDARY = "---7682340934589534098---",
            NEW_LINES = "\r\n";
        #endregion

        #region VarArchivos
        private Byte[] itemBytes, endBytes, newlineBytes;
        private FileStream fileStream;
        private Byte[] buffer;
        #endregion

        private HttpWebRequest request;
        private String respuesta;
        private WebResponse response;
        private Stream requestStream;
        private StreamReader reader;
        //private Byte[] postBytes;

        public Requests(string host, string puerto)
        {
            this.URL = "http://"+host + ":" + puerto;
        }
        public long insertScript(string fullPathZip, int tamanio)
        {
            request = (HttpWebRequest)WebRequest.Create(URL + "/insertScript");
            request.Method = "POST";
            request.KeepAlive = true;
            request.ContentType = "multipart/form-data; boundary=" + BOUNDARY;

            itemBytes = Encoding.UTF8.GetBytes(BOUNDARY + NEW_LINES + "Content-Disposition: form-data; name = \"file1\"; filename = \"archivozip\"" + NEW_LINES); //+ "Content - Type: application/octet-stream \r\n Content-Transfer-Encoding: binary\r\n"); //
            newlineBytes = Encoding.UTF8.GetBytes(NEW_LINES);
            endBytes = Encoding.UTF8.GetBytes("--" + BOUNDARY + "--");

            request.ContentLength = endBytes.Length + newlineBytes.Length + itemBytes.Length + tamanio;

            using (requestStream = request.GetRequestStream())
            {
                requestStream.Write(itemBytes, 0, itemBytes.Length);

                using (fileStream = new FileStream(fullPathZip + ".zip", FileMode.Open, FileAccess.Read))
                {
                    buffer = new Byte[tamanio];
                    int bytesRead = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }
                    fileStream.Close();
                }

                requestStream.Write(newlineBytes, 0, newlineBytes.Length);
                requestStream.Write(endBytes, 0, endBytes.Length);
            }

            using (response = request.GetResponse())
            {
                using (reader = new StreamReader(response.GetResponseStream()))
                {
                    respuesta = reader.ReadToEnd();
                }
            }

            //TESTEO COMPARA LAS DOS CADENAS; 
            /*if (respuesta != null && respuesta != "" && respuesta.Length != 0 && respuesta == fileString)
            {
                Console.WriteLine("Todo salió bien ");
            }
            else
            {
                throw new Exception("Fallo algo al enviar el archivo");
            }*/

            return (long)respuesta.Length;
        }

        public string getEPSG(string concepto, string texto)
        {
            request = (HttpWebRequest)WebRequest.Create(this.URL + "/getEPSG/" + concepto + "/" + texto);
            request.Method = "GET";

            using (response = request.GetResponse())
            {
                using (reader = new StreamReader(response.GetResponseStream()))
                {
                    respuesta = reader.ReadToEnd();
                }
            }
            return respuesta;
        }

        //web API
        /*public string getEPSG(string concepto, string texto)
        {
            try
            {
                respuesta = "";
                request = (HttpWebRequest)WebRequest.Create(URL + "/getEPSG/" + concepto + "/" + texto);
                request.Method = "GET";


                using (response = (HttpWebResponse)request.GetResponse())
                {
                    using (reader = new StreamReader(response.GetResponseStream()))
                    {
                        respuesta = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                respuesta = "";
            }

            return respuesta;
        }*/
    }
}
