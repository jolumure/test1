using DotSpatial.Projections;
using GeoAPI.Geometries;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System.Collections.Generic;
using System.IO;
using Entity;
using Ionic.Zip;
using System;
using System.Linq;
using Npgsql;
using System.Collections;
using System.Text;

namespace DAL
{
    public class ETShpInfoDAL
    {
        private string cs;
        public ETShpInfoDAL() { this.cs = string.Empty; }
        public ETShpInfoDAL(string _cs) { this.cs = _cs; }
        public SHPInfo getInfoShpOnMemory(string localPath)
        {
            SHPInfo shpInfo = new SHPInfo();
            List<string> files = Decompress(localPath);
            List<string> shpAndDbf = new List<string>();
            if(files.Count>0)
            {
                var selectList = files.Where(o => o.Contains(".SHP") || o.Contains(".DBF")).ToList();
                if(selectList.Count()==2)
                {
                    foreach(string f in selectList)
                    {
                        shpAndDbf.Add(Path.Combine(Path.GetDirectoryName(localPath), f));
                    }
                    if(shpAndDbf.Count()==2)
                    {
                        shpInfo = getInfoSHP(shpAndDbf[0]);
                    } 
                }
                CleanFiles(localPath);
            }
            
            return shpInfo;
        }

        public SHPInfo setShpOnDB(string localPath, string nombreFature,string EPSGOrig, string EPSGDest)
        {
            try
            {
                SHPInfo retVal = new SHPInfo();
                List<string> sqlS = new List<string>();
                List<string> files = Decompress(localPath);
                List<string> shpAndDbf = new List<string>();
                if (files.Count > 0)
                {
                    var selectList = files.Where(o => o.Contains(".SHP") || o.Contains(".DBF")).ToList();
                    if (selectList.Count() == 2)
                    {
                        foreach (string f in selectList)
                        {
                            shpAndDbf.Add(Path.Combine(Path.GetDirectoryName(localPath), f));
                        }
                        if (shpAndDbf.Count() == 2)
                        {
                                
                            sqlS = makeSQLs(shpAndDbf[0], nombreFature, EPSGOrig, EPSGDest);
                            if(sqlS.Count>0)
                            {
                                processSQL(ref sqlS);
                                retVal = getInfoSHP(shpAndDbf[0]);
                            }
                            else
                            {
                                retVal = null;
                            }
                        }
                    }
                    CleanFiles(localPath);
                }
                return retVal; 
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void processSQL(ref List<string> sqlS)
        {
            throw new NotImplementedException();
        }

        private List<string> makeSQLs(string localPathToSHP, string nombreFature, string EPSGOrig, string EPSGDest)
        {
            SHPEntity shp = new SHPEntity();
            shp = getSHPEntity(localPathToSHP);
            if (shp != null)
            {
                return makeSQL(ref shp, nombreFature, EPSGOrig, EPSGDest);
            }
            else
            {
                return null;
            }
        }

        private SHPEntity getSHPEntity(string localPathToSHP)
        {
            try
            {
                SHPEntity shpEntity = new SHPEntity();
                ShapefileDataReader shpDataReader;
                shpDataReader = new ShapefileDataReader(localPathToSHP, new GeometryFactory());
                shpEntity.shpDataReader = shpDataReader;
                shpDataReader.Reset();
                ArrayList features = new ArrayList();
                while (shpDataReader.Read())
                {
                    Feature feature = new Feature();
                    AttributesTable attributesTable = new AttributesTable();
                    string[] keys = new string[shpDataReader.DbaseHeader.NumFields];
                    IGeometry geometry = (Geometry)shpDataReader.Geometry;
                    for (int i = 0; i < shpDataReader.DbaseHeader.NumFields; i++)
                    {
                        DbaseFieldDescriptor fldDescr = shpDataReader.DbaseHeader.Fields[i];
                        keys[i] = fldDescr.Name;
                        attributesTable.AddAttribute(fldDescr.Name, shpDataReader.GetValue(i));
                    }
                    feature.Geometry = geometry;
                    feature.Attributes = attributesTable;
                    features.Add(feature);
                }
                shpEntity.features = features;
                return shpEntity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private List<string> makeSQL(ref SHPEntity shp, string nombreFature, string ePSGOrig, string ePSGDest)
        {
            List<string> listaSQL = new List<string>();
            //StringBuilder sb = new StringBuilder();

            //sb.Append(string.Format("insert into cat_feature(id,departamento,id_tipo_feature,nombre,nombre_vista) values ({0},'{1}',{2},'{3}','{4}');", 2, dpto, 1, shp.info.nombre, "v_eg_" + shp.info.nombre));
            //listaSQL.Add(sb.ToString());
            //int j = 1890;
            //foreach (Feature feat in shp.features)
            //{
            //    sb = new StringBuilder();
            //    sb.Append(string.Format("\ninsert into eg_polyg_gral(id,geom,id_feature) values ({0},st_transform(st_geomfromtext('{1}',{2}),{3}),{4});", j, feat.Geometry.ToString(), srid_ori, srid_dest, 2));
            //    for (int i = 0; i < feat.Attributes.Count; i++)
            //    {
            //        sb.Append(string.Format("\ninsert into ea_data_gral(id,id_point,id_line,id_polyg,tipo_data,nombre_data,data_data) values ({0},{1},{2},{3},'{4}','{5}','{6}');", (j * feat.Attributes.Count) + i, "null", "null", j, "C", feat.Attributes.GetNames()[i], feat.Attributes.GetValues()[i]));

            //        // DbaseFieldDescriptor fldDescr = shpDataReader.DbaseHeader.Fields[i];
            //        Console.WriteLine(i);
            //    }
            //    j++;
            //    listaSQL.Add(sb.ToString());
            //}
            //Console.WriteLine("...");
            //writeLines(@"c:\temp\" + shp.info.nombre + ".sql", listaSQL);
            return listaSQL;
        }

        private SHPInfo getInfoSHP(string v)
        {
            try
            {
                SHPInfo retVal = new SHPInfo();
                ShapefileDataReader shpDataReader;
                shpDataReader = new ShapefileDataReader(v, new GeometryFactory());

                List<string> attrs = new List<string>();
                
                retVal.nombre = Path.GetFileNameWithoutExtension(v);
                retVal.tipo = shpDataReader.ShapeHeader.ShapeType.ToString();
                retVal.numElementos = shpDataReader.RecordCount.ToString();


                for (int i = 0; i < shpDataReader.DbaseHeader.NumFields; i++)
                {
                    DbaseFieldDescriptor fldDescr = shpDataReader.DbaseHeader.Fields[i];
                    attrs.Add(string.Format("{0}::{1}", fldDescr.Name.ToString(), fldDescr.DbaseType.ToString()));
                }
                retVal.atributos = attrs;
                shpDataReader.Close();
                shpDataReader.Dispose();
                return retVal;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private static List<string> Decompress(string targFileZipTmp)
        {
            List<string> files = new List<string>();

            using (ZipFile z = ZipFile.Read(targFileZipTmp))
            {
                foreach (ZipEntry zEntry in z)
                {
                    if(File.Exists(Path.Combine(Path.GetDirectoryName(targFileZipTmp),zEntry.FileName)))
                    {
                        File.Delete(Path.Combine(Path.GetDirectoryName(targFileZipTmp), zEntry.FileName));
                    }
                    zEntry.Extract(Path.GetDirectoryName(targFileZipTmp), ExtractExistingFileAction.OverwriteSilently);
                    files.Add(zEntry.FileName.ToUpper());
                }
            }
            return files;
        }
        private void CleanFiles(string targFileZipTmp)
        {
            using (ZipFile z = ZipFile.Read(targFileZipTmp))
            {
                foreach (ZipEntry zEntry in z)
                {
                    if (File.Exists(Path.Combine(Path.GetDirectoryName(targFileZipTmp), zEntry.FileName)))
                    {
                        File.Delete(Path.Combine(Path.GetDirectoryName(targFileZipTmp), zEntry.FileName));
                    }
                }
            }
            File.Delete(targFileZipTmp);
        }
      
    }
}
