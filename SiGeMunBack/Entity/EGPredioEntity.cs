using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EGPredioEntity
    {
        public long IdEGPredio { get; set; }
        public string Geom { get; set; }
        public string Clave { get; set; }
        public int IdEGManzana { get; set; }
        public int IdTipoCuenta { get; set; }
        public string NumOficial { get; set; }
        public float RotacionNumOficial { get; set; }
        public string UbicaNumOfical { get; set; }
    }
}
