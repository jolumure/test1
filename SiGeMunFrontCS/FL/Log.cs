using Entity_Temp;
using HTTPService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FL
{
    public class Log
    {
        public IEnumerable<LogEntity> getLog()
        {
            try
            {
                return Requests.getLog().Result;
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }
}
