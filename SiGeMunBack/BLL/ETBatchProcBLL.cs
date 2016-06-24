using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ETBatchProcBLL
    {
        private string cs;

        public ETBatchProcBLL(string cs)
        {
            this.cs = cs;
        }

        public bool InsertScriptBL(Stream file)
        {
            ETBatchProcDAL obj = new ETBatchProcDAL(cs);
            return obj.insertScript(file);
        }
    }
}
