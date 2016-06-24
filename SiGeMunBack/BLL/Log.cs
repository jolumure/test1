using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Log
    {
        private string cs;

        public Log(string cs)
        {
            this.cs = cs;
        }

        public IEnumerable<LogEntity> getLog()
        {
            LogDAL obj = new LogDAL(cs);
            return obj.getLog();
        }
    }
}
