﻿using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class ETShpInfoBLL
    {
        public SHPInfo getShpInfo(string localPath)
        {
            ETShpInfoDAL obj = new ETShpInfoDAL();
            return obj.getInfoShpOnMemory(localPath);
        }
        public SHPResultInsert setShp(string cs,string localPath, string departamento,string nombreFeature, string EPSGOrig, string EPSGDest)
        {
            ETShpInfoDAL obj = new ETShpInfoDAL(cs);
            return obj.setShpOnDB(localPath,departamento, nombreFeature, EPSGOrig, EPSGDest);
        }
    }
}
