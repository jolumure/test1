using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class ETBatchProcBLL
    {
        private string cs;

        public ETBatchProcBLL(string cs)
        {
            this.cs = cs;
        }

        public long InsertScriptBL(Stream file)
        {
            ETBatchProcDAL obj = new ETBatchProcDAL(cs);
            return obj.insertScript(file);
        }
    }
}
