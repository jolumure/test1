using NetTopologySuite.IO;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Entity
{
    public class SHPEntity
    {
        public SHPInfo info { get; set; }
        public ArrayList features { get; set; }
        public ShapefileDataReader shpDataReader { get; set; }
    }

    public class SHPInfo
    {
        public string nombre { get; set; }
        public string tipo { get; set; }
        public string numElementos { get; set; }
        public List<string> atributos { get; set; }
        public string ToString_()
        {
            return string.Format("Nombre {0} \n Tipo {1} \n Número de elementos {2}\n Proyección {3} \n Atributos :: \n {4}", nombre, tipo, numElementos, proj, string.Join("@\n", atributos.ToArray()));
        }
        public string ToJSON()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
        public ExtractoProj proj { get; set; }
    }

    public class ExtractoProj
    {
        public string title { get; set; }
        public string ESRIString { get; set; }
        public string EPSGCode { get; set; }

    }

    public class SHPResultInsert
    {
        public SHPResultInsert()
        {
            observaciones = new List<string>();
        }
        public string nombreVista { get; set; }
        public List<string> observaciones { get; set; }
        public string status { get; set; }

        public string ToJSON()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
    }

}
