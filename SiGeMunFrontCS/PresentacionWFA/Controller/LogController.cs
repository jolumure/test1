using Entity_Temp;
using FL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentacionWFA.Controller
{
    public class LogController
    {
        public IEnumerable<LogEntity> getLog()
        {
            return new Log().getLog();
        }
    }
}
