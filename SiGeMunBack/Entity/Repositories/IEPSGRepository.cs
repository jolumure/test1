using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public interface IEPSGRepository
    {
        IEnumerable<EPSGEntity> GetAll();
        EPSGEntity Get(int id);
        bool Add(EPSGEntity item);
        void Remove(int id);
        bool Update(EPSGEntity item);
    }
}
