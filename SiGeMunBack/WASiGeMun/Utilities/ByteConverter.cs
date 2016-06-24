using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * \d	-	J	e	c	i	l	\r	\n
	0	1	2	3	4	5	6	7	8
0		1							
1	 	2							
2		3							
3		4							
4		5							
5			6						
6				7					
7					8				
8						9			
9				10					
10							11		
11	11		12						
12				13					
13					14				
14						15			
15				16					
16							17		
17		18							
18		19							
19		20							
20		21							
21		22							
22	22	22	22	22	22	22	22	23	22
23	22	22	22	2	22	22	22	22	24
24	24	24	24	24	24	24	24	25	24
25	24	24	24	24	24	24	24	24	100

 * 
 * 
 */
namespace WASiGeMun.Utilities
{
    public class ByteConverter
    {
        private int[,] m;

        public ByteConverter()
        {
            this.m = new int[26, 9];
            this.m[0, 0] = 0;
            this.m[0, 1] = 1;
            this.m[1, 1] = 2;
            this.m[2, 1] = 3;
            this.m[3, 1] = 4;
            this.m[4, 1] = 5;
            this.m[5, 2] = 6;
            this.m[5, 1] = 5;

            this.m[6, 3] = 7;
            this.m[7, 4] = 8;
            this.m[8, 5] = 9;
            this.m[9, 3] = 10;
            this.m[10, 6] = 11;
            this.m[11, 0] = 11;
            this.m[11, 2] = 12;
            this.m[12, 3] = 13;
            this.m[13, 4] = 14;
            this.m[14, 5] = 15;
            this.m[15, 3] = 16;
            this.m[16, 6] = 17;
            this.m[17, 1] = 18;
            this.m[18, 1] = 19;
            this.m[19, 1] = 20;
            this.m[20, 1] = 21;
            this.m[21, 1] = 22;

            this.m[22, 0] = 22;
            this.m[22, 1] = 22;
            this.m[22, 2] = 22;
            this.m[22, 3] = 22;
            this.m[22, 4] = 22;
            this.m[22, 5] = 22;
            this.m[22, 6] = 22;
            this.m[22, 7] = 23;

            this.m[23, 0] = 22;
            this.m[23, 1] = 22;
            this.m[23, 2] = 22;
            this.m[23, 3] = 22;
            this.m[23, 4] = 22;
            this.m[23, 5] = 22;
            this.m[23, 6] = 22;
            this.m[23, 7] = 22;
            this.m[23, 8] = 24;

            this.m[24, 0] = 22;
            this.m[24, 1] = 22;
            this.m[24, 2] = 22;
            this.m[24, 3] = 22;
            this.m[24, 4] = 22;
            this.m[24, 5] = 22;
            this.m[24, 6] = 22;
            this.m[24, 7] = 25;
            this.m[24, 8] = 22;

            this.m[25, 0] = 22;
            this.m[25, 1] = 22;
            this.m[25, 2] = 22;
            this.m[25, 3] = 22;
            this.m[25, 4] = 22;
            this.m[25, 5] = 22;
            this.m[25, 6] = 22;
            this.m[25, 7] = 22;
            this.m[25, 8] = 100;


        }

        //archivoS es para tersteo
        public Byte[] getByteArray(Stream body, long tam)
        {
            int r = 0, c = 0, limite = 0;
            Byte[] archivo, buffer;
            Char a;
            String temp_tamanio = "";

            buffer = new Byte[tam];

            for (int i = 0; i < tam; i++)
            {
                buffer[i] = (Byte)body.ReadByte();
                a = (Char)buffer[i];

                if (a == '\u0030' || a == '\u0031' || a == '\u0032' || a == '\u0033' || a == '\u0034' || a == '\u0035' || a == '\u0036' || a == '\u0037' || a == '\u0038' || a == '\u0039')
                {
                    c = 0;
                    if (r == 11 && c == 0)
                        temp_tamanio += a;
                }
                else
                {
                    switch (a)
                    {
                        case '\u002D':
                            c = 1;
                            break;
                        case '\u004A':
                            c = 2;
                            break;
                        case '\u0065':
                            c = 3;
                            break;
                        case '\u0063':
                            c = 4;
                            break;
                        case '\u0069':
                            c = 5;
                            break;
                        case '\u006C':
                            c = 6;
                            break;
                        case '\r':
                            c = 7;
                            break;
                        case '\n':
                            c = 8;
                            break;
                        default:
                            c = 0;
                            break;
                    }
                }
                r = this.m[r, c];
                if (r == 100)
                {
                    limite = i + 1;
                    break;
                }

            }

            long tamanio;
            if (Int64.TryParse(temp_tamanio, out tamanio))
            {
                archivo = new Byte[tamanio];
                body.Read(archivo, 0, (Int32)tamanio);
            }
            else
            {
                throw new Exception("No se pudo obtener el tamaño del archivo");
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
