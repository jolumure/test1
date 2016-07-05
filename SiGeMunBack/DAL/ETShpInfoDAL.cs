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
using System.Web.Script.Serialization;

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
                        shpInfo = getInfoSHP(shpAndDbf[0]);
                    }
                }
                CleanFiles(localPath);
            }

            return shpInfo;
        }

        public SHPResultInsert setShpOnDB(string localPath, string departamento, string nombreFeature, string EPSGOrig, string EPSGDest)
        {
            SHPResultInsert result = new SHPResultInsert();
            try
            {
                List<string> sqlS = new List<string>();
                List<string> files = Decompress(localPath);
                List<string> shpAndDbf = new List<string>();
                if (files.Count > 0)
                {
                    var selectList = files.Where(o => o.ToUpper().Contains(".SHP") || o.ToUpper().Contains(".DBF") || o.ToUpper().Contains(".JSON")).ToList();
                    if (selectList.Count() == 3)
                    {
                        foreach (string f in selectList)
                        {
                            shpAndDbf.Add(Path.Combine(Path.GetDirectoryName(localPath), f));
                        }
                        if (shpAndDbf.Count() == 3)
                        {

                            result=makeSQLs(shpAndDbf.Where(o => o.ToUpper().Contains(".SHP")).First().ToString(), departamento, nombreFeature, EPSGOrig, EPSGDest);
                        }
                        else
                        {
                            result.observaciones.Add("No se cuenta con los archivos necesarios, shp, dbf y json...");
                            result.status = "Error";
                        }
                    }
                    else
                    {
                        result.observaciones.Add("No se cuenta con los archivos necesarios, shp, dbf y json...");
                        result.status = "Error";
                    }
                    CleanFiles(localPath);
                }
                else
                {
                    result.observaciones.Add("zip con problemas...");
                    result.status = "Error";
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        private SHPResultInsert makeSQLs(string localPathToSHP, string departamento, string nombreFeature, string EPSGOrig, string EPSGDest)
        {
            SHPResultInsert result = new SHPResultInsert();
            SHPEntity shp = new SHPEntity();
            shp = getSHPEntity(localPathToSHP);
            string json = getJSONFeatures(localPathToSHP);
            if (shp != null)
            {
                result = makeSQL(ref shp, departamento, nombreFeature, EPSGOrig, EPSGDest, json);
            }
            else
            {
                result.observaciones.Add("Problemas al crear entidad...");
                result.status = "Error";
            }
            return result;
        }

        private string getJSONFeatures(string localPathToSHP)
        {
            try
            {
                return File.ReadAllText(localPathToSHP.ToUpper().Replace(".SHP", ".JSON"));
            }
            catch(Exception ex)
            {
                return string.Empty;
            }
        }

        private SHPEntity getSHPEntity(string localPathToSHP)
        {
            try
            {
                SHPEntity shpEntity = new SHPEntity();
                SHPInfo shpInfo = new SHPInfo();
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

                shpInfo.nombre = Path.GetFileNameWithoutExtension(localPathToSHP);
                shpInfo.tipo = shpDataReader.ShapeHeader.ShapeType.ToString();
                shpInfo.numElementos = shpDataReader.RecordCount.ToString();

                shpEntity.info = shpInfo;

                shpDataReader.Close();
                shpDataReader.Dispose();
                return shpEntity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private SHPResultInsert makeSQL(ref SHPEntity shp, string departamento, string nombreFeature, string ePSGOrig, string ePSGDest, string json)
        {
            SHPResultInsert resultJSON = new SHPResultInsert();
            try
            {
                int nGeoms = 0;
                int j = 0;
                var serializer = new JavaScriptSerializer();
                var result = serializer.Deserialize<dynamic>(json);
                List<string> attrs = new List<string>();
                foreach(string o in result["atributos"])
                {
                    attrs.Add(o.Split(new string[] { "::" }, StringSplitOptions.None)[0]);
                }
                if (this.cs != string.Empty)
                {
                    int idFeature = ProcessSQLReturnID(string.Format("select pkg__insertcat_feature( '{0}','{1}','{2}','{3}')",
                                    departamento, nombreFeature, "v_" + nombreFeature, shp.info.tipo));
                    if (idFeature > 0)
                    {

                        resultJSON.nombreVista = "v_" + nombreFeature;

                        foreach (Feature feat in shp.features)
                        {
                            int idGeometry = ProcessSQLReturnID(string.Format("select pkg__inserteg_geom_gral('{0}',{1},{2},{3},'{4}')",
                            feat.Geometry.ToString(), idFeature, ePSGOrig, ePSGDest, GeomTableName(ref shp)));
                            if (idGeometry > 0)
                            {
                                nGeoms++;
                                List<string> sqls = new List<string>();
                                for (int i = 0; i < feat.Attributes.Count; i++)
                                {
                                    if (attrs.Contains(feat.Attributes.GetNames()[i]))
                                    {
                                        sqls.Add(string.Format("select pkg__insertea_data_gral(" + arrayIDs(ref shp) + ",'{1}','{2}')", idGeometry, feat.Attributes.GetValues()[i], feat.Attributes.GetNames()[i]));
                                    }
                                }
                                if (sqls.Count() > 0)
                                {
                                    j = ProcessSQLReturnNothing(sqls);
                                    resultJSON.status = "Ok";
                                }
                                else
                                {
                                    resultJSON.observaciones.Add("No se pudo ingresar geometría cerca de :: " + feat.Geometry.Centroid.X.ToString() + "," + feat.Geometry.Centroid.Y.ToString());
                                }
                            }
                            else
                            {
                                resultJSON.observaciones.Add("No se pudo ingresar geometría cerca de :: " + feat.Geometry.Centroid.X.ToString() + "," + feat.Geometry.Centroid.Y.ToString());
                            }
                        }
                        resultJSON.observaciones.Add("Número de geometrías ingresadas :: " + nGeoms.ToString());
                        //Si es correcto todo, se crea la vista v_id_feature numeric, v_geom_type text,str_view text
                        int boolVista = ProcessSQLReturnID(string.Format("select pkg__createview_feature({0},'{1}','{2}')", idFeature, shp.info.tipo, "v_" + nombreFeature));
                        if(boolVista==0)
                        {
                            resultJSON.observaciones.Add("No se pudo crear la vista correctamente");
                            resultJSON.nombreVista = string.Empty;
                        }
                    }
                }
                return resultJSON;
            }
            catch(Exception ex)
            {
                resultJSON.observaciones.Add("Excepción ::: " + ex.Message);
                return resultJSON;
            }
        }

        private int ProcessSQLReturnID( string sql)
        {
            try
            {
                int retVal=-1;
                bool Ok = false;
                using (var conn = new Npgsql.NpgsqlConnection(this.cs))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {

                        using (var comm = conn.CreateCommand())
                        {
                            comm.CommandText = sql;
                            NpgsqlDataReader drdr = comm.ExecuteReader();
                            while (drdr.Read())
                            {
                                if ((int.TryParse(drdr[0].ToString(), out retVal)))
                                {
                                    Ok = true;
                                }
                            }
                            drdr.Close();
                        }
                        tran.Commit();
                    }
                    conn.Close();
                }
                if (Ok)
                {
                    return retVal;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        private int ProcessSQLReturnNothing(List<string> sqls)
        {
            try
            {
                int retVal = -1;
                bool Ok = false;
                using (var conn = new Npgsql.NpgsqlConnection(this.cs))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
                        foreach(string s in sqls)
                        {
                            using (var comm = conn.CreateCommand())
                            {
                                comm.CommandText = s;
                                comm.ExecuteNonQuery();
                            }
                        }
                        tran.Commit();
                    }
                    conn.Close();
                }
                if (Ok)
                {
                    return retVal;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        private string arrayIDs(ref SHPEntity shp)
        {
            switch (shp.info.tipo)
            {
               
                case "Point":
                    return "{0},null,null";
                case "LineString":
                    return "null,{0},null";
                case "Polygon":
                    return "null,null,{0}";
                default:
                    return string.Empty;
            }
        }

        private string GeomTableName(ref SHPEntity shp)
        {
            switch (shp.info.tipo)
            {
                case "Polygon":
                    return "eg_polyg_gral";
                case "Point":
                    return "eg_point_gral";
                case "LineString":
                    return "eg_line_gral";
                default:
                    return string.Empty;
            }
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
                    if (File.Exists(Path.Combine(Path.GetDirectoryName(targFileZipTmp), zEntry.FileName)))
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
