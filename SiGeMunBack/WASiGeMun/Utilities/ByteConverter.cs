using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WASiGeMun.Utilities
{
    public class ByteConverter
    {
        public Byte[] getByteArray(Stream file, out string archivoS)
        {
            archivoS = "";
            List<int> limites = new List<int>();
            bool retorno = false;
            Byte[] archivo, buffer;

            try
            {
                buffer = new Byte[file.Length];
                for (int i = 0; i < file.Length; i++)
                {
                    buffer[i] = (byte)file.ReadByte();
                    switch ((Char)buffer[i])
                    {
                        case '\r':
                            retorno = true;
                            break;
                        case '\n':
                            if (retorno)
                            {
                                limites.Add(i);
                            }

                            retorno = false;
                            break;
                        default:
                            retorno = false;
                            break;
                    }
                }

                archivo = new Byte[limites[2] - limites[1]];
                for (int j = 0, k = limites[1] + 1; j < archivo.Length - 2; j++, k++)
                {
                    archivo[j] = buffer[k];
                    archivoS += (Char)buffer[k];
                }

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new Byte[0];
            }

            return archivo;
        }

        //Puede que la deje de usar
        public Byte[] convertToByte(string texto, int tamanio)
        {
            Char[] delimitador = { ' ' };
            Byte[] buffer = new byte[tamanio];
            string[] words = texto.Split(delimitador);
            int a = 0;

            foreach (string so in words)
            {
                if (so != "")
                    try
                    {
                        buffer[a] = CallTryParse(so);
                        a++;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return new Byte[0];
                    }
            }

            return buffer;
        }

        private Byte CallTryParse(string stringToConvert)
        {
            Byte byteValue;
            bool result = Byte.TryParse(stringToConvert, out byteValue);
            if (!result)
            {
                throw new Exception();
            }

            return byteValue;
        }
    }
}
