using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EGCondoHorizaltalEntity
    {
        public int IdEGCondoHorizontal { get; set; }
        public string Geom { get; set; }
        public string Clave { get; set; }
        public int IdPredio { get; set; }
        public string NumOficial { get; set; }
        public float RotacionNumOficial { get; set; }
    }
}
