using Entity_Temp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentacionWFA
{
    class SQLFormatController
    {
        internal List<string> makeInsertPolygon(string feature, ref List<string> polygonsWkt, double tolerance, int SRID)
        {
            List<string> sqls = new List<string>();
            string sql = string.Empty;
            switch (feature)
            {
                case "Manzana":
                    foreach (string geo in polygonsWkt)
                    {
                        sqls.Add(string.Format("SELECT pkg__insertmanzana_polig ('{0}',{1},{2});", geo.ToString(), tolerance, SRID));
                    }
                    break;
                case "Predio":
                    foreach (string geo in polygonsWkt)
                    {
                        sqls.Add(string.Format("SELECT pkg__insertpredio_polig('{0}',{1});", geo.ToString(), SRID));
                    }
                    break;
                case "Construcción":
                    foreach (string geo in polygonsWkt)
                    {
                        sqls.Add(string.Format("SELECT pkg__insertconstr_polig('{0}',{1});", geo.ToString(), SRID));
                    }
                    break;
                case "Delegación":
                    foreach (string geo in polygonsWkt)
                    {
                        sqls.Add(string.Format("MGII.INSERT_GEOM_PKG.PINSERTEGDELEGACIONPOLIG ({0},pCOD);", geo.ToString()));
                    }
                    break;
                case "Colonia":
                    foreach (string geo in polygonsWkt)
                    {
                        sqls.Add(string.Format("SELECT pkg__insertcolonia_polig('{0}',{1});", geo.ToString(), SRID));
                    }
                    break;

                case "Codigo Postal":
                    foreach (string geo in polygonsWkt)
                    {
                        sqls.Add(string.Format("SELECT pkg_insertcodigopostal_polig('{0}', {1});", geo.ToString(), SRID));
                    }
                    break;
                case "Centro Historico":
                    foreach (string geo in polygonsWkt)
                    {
                        sqls.Add(string.Format("SELECT pkg_insertcentrohistorico_polig('{0}', {1});", geo.ToString(), SRID));
                    }
                    break;
                case "Zonas Economicas":
                    foreach (string geo in polygonsWkt)
                    {
                        sqls.Add(string.Format("SELECT pkg_insertzonaseconomicas_polig('{0}', {1});", geo.ToString(), SRID));
                    }
                    break;
                case "Municipio":
                    foreach (string geo in polygonsWkt)
                    {
                        sqls.Add(string.Format("SELECT pkg_insertmunicipio_polig('{0}', {1});", geo.ToString(), SRID));
                    }
                    break;
                case "Seccion":
                    foreach (string geo in polygonsWkt)
                    {
                        sqls.Add(string.Format("SELECT pkg_insertseccion_polig('{0}', {1});", geo.ToString(), SRID));
                    }
                    break;
                case "Localidad":
                    foreach (string geo in polygonsWkt)
                    {
                        sqls.Add(string.Format("SELECT pkg_insertlocalidad_polig('{0}', {1});", geo.ToString(), SRID));
                    }
                    break;
                case "Estado":
                    foreach (string geo in polygonsWkt)
                    {
                        sqls.Add(string.Format("SELECT pkg_insertestado_polig('{0}', {1});", geo.ToString(), SRID));
                    }
                    break;
                case "Índice cartográfico":
                    foreach (string geo in polygonsWkt)
                    {
                        sqls.Add(string.Format("MGII.INSERT_GEOM_PKG.PINSERTEGINDICEPOLIG ({0},pCOD);", geo.ToString(), SRID));
                    }
                    break;
                case "Default":
                    foreach (string geo in polygonsWkt)
                    {
                        sqls.Add(string.Format("insert into eg_zonificacionseduvi (geom) values ({0});", geo.ToString(), SRID));
                    }
                    break;
                default: break;
            }
            return sqls;
        }

        internal List<string> makeInsertPoint(string feature, ref List<PointTextEntity> pointsCve, int SRID)
        {
            List<string> sqls = new List<string>();
            string sql = string.Empty;
            switch (feature)
            {
                case "Manzana Clave":
                    foreach (PointTextEntity geo in pointsCve)
                    {
                        sqls.Add(string.Format("SELECT pkg__insertmanzana_cve ('{0}','{1}',{2});", geo.originGeom.AsText, geo.text, SRID));
                    }
                    break;
                case "Predio Clave":
                    foreach (PointTextEntity geo in pointsCve)
                    {
                        sqls.Add(string.Format("SELECT pkg__insertpredio_cve('{0}','{1}',{2});", geo.originGeom.AsText, geo.text, SRID));
                    }
                    break;
                case "Construcción Clave":
                    foreach (PointTextEntity geo in pointsCve)
                    {
                        sqls.Add(string.Format("SELECT pkg__insertconstr_cve('{0}','{1}',{2});", geo.originGeom.AsText, geo.text, SRID));
                    }
                    break;
                case "Número Oficial":
                    foreach (PointTextEntity geo in pointsCve)
                    {
                        sqls.Add(string.Format("SELECT pkg__insertpredio_numof('{0}','{1}', {2}, {3});", geo.originGeom.AsText, geo.text, SRID, geo.rotation.ToString()));
                    }
                    break;
                case "Delegación Clave":
                    foreach (PointTextEntity geo in pointsCve)
                    {
                        sqls.Add(string.Format("MGII.INSERT_GEOM_PKG.PINSERTEGDELEGACIONCVE ({0}, '{1}' ,pCOD);", geo.originGeom.AsText, geo.text, SRID));
                    }
                    break;
                case "Colonia Clave":
                    foreach (PointTextEntity geo in pointsCve)
                    {
                        sqls.Add(string.Format("SELECT pkg__insertcolonia_cve('{0}', '{1}', {2});", geo.originGeom.AsText, geo.text, SRID));
                    }
                    break;
                case "Calle Clave":
                    foreach (PointTextEntity geo in pointsCve)
                    {
                        sqls.Add(string.Format("SELECT pkg__insertcalle_cve('{0}', '{1}', {2});", geo.originGeom.AsText, geo.text, SRID));
                    }
                    break;
                case "Índice cartográfico Clave":
                    foreach (PointTextEntity geo in pointsCve)
                    {
                        sqls.Add(string.Format("MGII.INSERT_GEOM_PKG.PINSERTEGINDICECLAVE ({0}, '{1}' ,pCOD);", geo.originGeom.AsText, geo.text, SRID));
                    }
                    break;
                case "Estado Clave":
                    foreach (PointTextEntity geo in pointsCve)
                    {
                        sqls.Add(string.Format("SELECT pkg_insertestado_cve('{0}', '{1}', {2}); ", geo.originGeom.AsText, geo.text, SRID));
                    }
                    break;
                case "Codigo Postal Clave":
                    foreach (PointTextEntity geo in pointsCve)
                    {
                        sqls.Add(string.Format("SELECT pkg_insertcodigopostal_cve('{0}', '{1}', {2}); ", geo.originGeom.AsText, geo.text, SRID));
                    }
                    break;
                case "Centro Historico Clave":
                    foreach (PointTextEntity geo in pointsCve)
                    {
                        sqls.Add(string.Format("SELECT pkg_insertcentrohistorico_cve('{0}', '{1}', {2}); ", geo.originGeom.AsText, geo.text, SRID));
                    }
                    break;
                case "Localidad Clave":
                    foreach (PointTextEntity geo in pointsCve)
                    {
                        sqls.Add(string.Format("SELECT pkg_insertlocalidad_cve('{0}', '{1}', {2});", geo.originGeom.AsText, geo.text, SRID));
                    }
                    break;
                case "Zonas Economicas Clave":
                    foreach (PointTextEntity geo in pointsCve)
                    {
                        sqls.Add(string.Format("SELECT pkg_insertzonaseconomicas_cve('{0}', '{1}', {2}); ", geo.originGeom.AsText, geo.text, SRID));
                    }
                    break;
                case "Municipio Clave":
                    foreach (PointTextEntity geo in pointsCve)
                    {
                        sqls.Add(string.Format("SELECT pkg_insertmunicipio_cve('{0}', '{1}', {2}); ", geo.originGeom.AsText, geo.text, SRID));
                    }
                    break;
                case "Seccion Clave":
                    foreach (PointTextEntity geo in pointsCve)
                    {
                        sqls.Add(string.Format("SELECT pkg_insertseccion_cve('{0}', '{1}', {2});", geo.originGeom.AsText, geo.text, SRID));
                    }
                    break;
                case "Default":
                    foreach (PointTextEntity geo in pointsCve)
                    {
                        string[] strSplit = geo.text.Split('@');
                        if (strSplit.Length >= 5)
                        {
                            sqls.Add(string.Format("insert into eg_zonificacionseduviclave (geom,zon,uso,niveles,a_libre,densidad)  values ({0},'{1}','{2}','{3}','{4}','{5}');", geo.originGeom.AsText, strSplit[0], strSplit[1], strSplit[2], strSplit[3], strSplit[4]));
                        }
                    }
                    break;
                default: break;
            }
            return sqls;
        }

        internal List<string> makeInsertLine(string feature, ref List<LineEntity> linesWkt, int SRID)
        {
            List<string> sqls = new List<string>();
            string sql = string.Empty;
            switch (feature)
            {
                case "Calle":
                    foreach (LineEntity geo in linesWkt)
                    {

                        sqls.Add(string.Format("SELECT pkg__insertcalle_line('{0}',{1});", geo.lineGeom.AsText, SRID));
                    }
                    break;
                default: break;
            }
            return sqls;
        }
    }
}
