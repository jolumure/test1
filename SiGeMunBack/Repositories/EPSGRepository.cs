using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;

namespace WASiGeMun.Repositories
{
    public class EPSGRepository
    {
        private List<EPSGEntity> epsgs = new List<EPSGEntity>();

        public IEnumerable<EPSGEntity> GetAll()
        {
            return epsgs;
        }

        public EPSGEntity Get(int id)
        {
            return epsgs.Find(p => p.epsg == id);
        }

        public bool Add(EPSGEntity item)
        {
            if (item == null)
            {
                return false;
            }

            epsgs.Add(item);
            return true;
        }

        public void Remove(int id)
        {
            epsgs.RemoveAll(p => p.epsg == id);
        }

        public bool Update(EPSGEntity item)
        {
            if (item == null)
            {
                return false;
            }

            int index = epsgs.FindIndex(p => p.epsg == item.epsg);

            if (index == -1)
            {
                return false;
            }
            epsgs.RemoveAt(index);
            epsgs.Add(item);
            return true;
        }
    }
}
