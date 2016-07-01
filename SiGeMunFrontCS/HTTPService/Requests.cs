using Entity_Temp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http.Headers;

namespace HTTPService
{
    public class Requests
    {

        #region constantes

        private const string BOUNDARY = "-----Jeciel{0}Jeciel-----";
        private static string host = ConfigurationManager.AppSettings["host"];
        private static string port = ConfigurationManager.AppSettings["puerto"];
        #endregion

        //Web App Asíncrono
        public static async Task<bool> insertScript(byte[] zip)
        {
            String respuesta = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://" + host + ":" + port + "/");
                client.DefaultRequestHeaders.Accept.Clear();

                using (var content = new MultipartFormDataContent(String.Format(BOUNDARY, zip.Length)))
                {
                    content.Add(new StreamContent(new MemoryStream(zip)), "scripts", "scripts.zip");

                    using (var message = await client.PostAsync("api/InsertScript", content).ConfigureAwait(false))
                    {
                        respuesta = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (respuesta == "True")
                            return true;
                        else
                            return false;
                    }
                }
            }
        }

        //Web API Asíncrono
        public static async Task<IEnumerable<EPSGEntity>> getEPSG(string concepto, string texto)
        {
            IEnumerable<EPSGEntity> resultado;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://" + host + ":" + port + "/");
                client.DefaultRequestHeaders.Accept.Clear();

                var response = await client.GetAsync("api/getEPSG/" + concepto + "/" + texto).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    resultado = JsonConvert.DeserializeObject<IEnumerable<EPSGEntity>>(responseJson);
                }
                else
                {
                    resultado = null;
                }
            }

            return resultado;
        }

        //Web API Asíncrono
        public static async Task<IEnumerable<LogEntity>> getLog()
        {

            IEnumerable<LogEntity> resultado;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://" + host + ":" + port + "/");
                client.DefaultRequestHeaders.Accept.Clear();

                var response = await client.GetAsync("api/getLog").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    resultado = JsonConvert.DeserializeObject<IEnumerable<LogEntity>>(responseJson);
                }
                else
                {
                    resultado = null;
                }
            }
            return resultado;
        }

        /*public long insertScript(string fullPathZip, int tamanio)
        {
        string BOUNDARY2 = String.Format(BOUNDARY, tamanio);

        request = (HttpWebRequest)WebRequest.Create(URL + "/insertScript");
        request.Method = "POST";
        request.KeepAlive = true;
        request.ContentType = "multipart/form-data; boundary=" + BOUNDARY2;


        itemBytes = Encoding.UTF8.GetBytes(BOUNDARY2 + NEW_LINES + "Content-Disposition: form-data; name = \"file1\"; filename = \"archivozip\"" + NEW_LINES); //+ "Content - Type: application/octet-stream \r\n Content-Transfer-Encoding: binary\r\n"); //
        newlineBytes = Encoding.UTF8.GetBytes(NEW_LINES);
        endBytes = Encoding.UTF8.GetBytes("--" + BOUNDARY2 + "--");

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
                if (respuesta != "True")
                    Console.WriteLine("no Zip");
            }
        }
        string fileString = System.Text.Encoding.ASCII.GetString(buffer);

        //TESTEO COMPARA LAS DOS CADENAS; 
        if (respuesta != null && respuesta != "True" && respuesta.Length != 0 && respuesta != fileString)
        {
            List<char> lista = new List<char>();
            if (respuesta.Length == fileString.Length)
            {
                for (int i = 0; i < respuesta.Length; i++)
                {
                    if (respuesta[0] != fileString[0])
                        lista.Add(respuesta[0]);
                }
            }
        }

        return (long)respuesta.Length;
    }*/

        /*public string getEPSG(string concepto, string texto)
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
        }*/
    }
}
