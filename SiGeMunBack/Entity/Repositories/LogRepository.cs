using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class LogRepository
    {
        private List<LogEntity> logs = new List<LogEntity>();

        public IEnumerable<LogEntity> GetAll()
        {
            return logs;
        }

        public LogEntity Get(string id)
        {
            return logs.Find(p => p.carta == id);
        }

        public bool Add(LogEntity item)
        {
            if (item == null)
            {
                return false;
            }

            logs.Add(item);
            return true;
        }

        public void Remove(string id)
        {
            logs.RemoveAll(p => p.carta == id);
        }

        public bool Update(LogEntity item)
        {
            if (item == null)
            {
                return false;
            }

            int index = logs.FindIndex(p => p.carta == item.carta);

            if (index == -1)
            {
                return false;
            }
            logs.RemoveAt(index);
            logs.Add(item);
            return true;
        }

        public override string ToString()
        {
            return logs.ToString();
        }
    }
}
