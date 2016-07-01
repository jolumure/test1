using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Temp
{
    public class LogEntity
    {
        public String carta { get; set; }
        public STATUS estatus { get; set; }
        public String tiempo { get; set; }

        public override String ToString()
        {
            return "carta: " + this.carta + ",\t status:" + this.ToString() + ", \ttiempo:" + this.tiempo;
        }

        public override bool Equals(object obj)
        {
            LogEntity cartaObject = obj as LogEntity;
            if (cartaObject == null)
                return false;
            else
            {
                if (this.carta.Equals(cartaObject.carta))
                {
                    this.estatus = cartaObject.estatus;
                    this.tiempo = cartaObject.tiempo;
                    return true;
                }
                return false;
            }
        }

        public override int GetHashCode()
        {
            return this.carta.GetHashCode();
        }
    }



}
