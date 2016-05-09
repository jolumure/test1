using Bentley.Interop.GFC;
using Bentley.Interop.GFC.CachedProject;
using Bentley.Interop.MicroStationDGN;
//using Bentley.Interop.Xft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using NetTopologySuite.Features;
using NetTopologySuite.Operation.Polygonize;
using GeoAPI.Geometries;

using PresentacionWFA.Entity_temp;
using PresentacionWFA.Data;
using System.Collections;
using System.Globalization;
using PresentacionWFA.Controller;

namespace PresentacionWFA
{
    class UstnController
    {
        #region Variables

        private Bentley.Interop.MicroStationDGN.ApplicationObjectConnector connectorUstn;
        private Bentley.Interop.MicroStationDGN.Application applicationUstn;

        private bool _isConnected=false;
       
        #endregion
        
        #region Constructor
        
        public UstnController() { }
        
        #endregion

        #region Methods

        internal bool setConnected()
        {
            try
            {
                //this.applicationUstn = new Bentley.Interop.MicroStationDGN.ApplicationClass();
                //Bentley.Interop.MicroStationDGN.DesignFile df = this.applicationUstn.OpenDesignFile(@"c:\temp\democonstr.dgn");
                if (System.Diagnostics.Process.GetProcessesByName("ustation").Length == 1 ||
                    System.Diagnostics.Process.GetProcessesByName("powermap").Length == 1 ||
                    System.Diagnostics.Process.GetProcessesByName("mapenterprise").Length == 1 ||
                    System.Diagnostics.Process.GetProcessesByName("mapstandalone").Length == 1)
                {
                    this.connectorUstn = (Bentley.Interop.MicroStationDGN.ApplicationObjectConnector)System.Runtime.InteropServices.Marshal.GetActiveObject("MicroStationDGN.ApplicationObjectConnector");
                    this.applicationUstn = connectorUstn.Application;
                    this.applicationUstn.Visible = true;
                  
                    this._isConnected = true;
                    return true;
                }
                else
                {
                    this._isConnected = false;
                    return false;
                }
            }
            catch
            {
                this._isConnected = false;
                return false;
            }

        }

        internal void openDgn(DgnFileEntity dgnFileEntity,bool readOnly)
        {
            this.applicationUstn.OpenDesignFile(dgnFileEntity.Ruta + dgnFileEntity.Nombre, readOnly);
        }

        internal void removeReferences()
        {
            foreach (Attachment att in this.applicationUstn.ActiveModelReference.Attachments)
            {
                this.applicationUstn.ActiveModelReference.Attachments.Remove(att);
            }
            this.applicationUstn.RedrawAllViews(MsdDrawingMode.Normal);
        }

        internal void attachReference(DgnFileEntity file)
        {
            try
            {
                //this.applicationUstn.ActiveDesignFile.DefaultModelReference.Attachments.AddCoincident(file.Ruta + file.Nombre,
                //    this.applicationUstn.ActiveModelReference.Name.ToString(),
                //    file.Nombre, file.Nombre);
                
                this.applicationUstn.ActiveModelReference.Attachments.AddCoincident(file.Ruta + file.Nombre,
                    this.applicationUstn.ActiveModelReference.Name.ToString(),
                    file.Nombre, file.Nombre);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //this.applicationUstn.MessageCenter.AddMessage(ex.Message, null, 0, true);
            }
        }

        internal void setOnOffAllLevelsRef(bool onOff)
        {
            View view;
            view = this.applicationUstn.ActiveDesignFile.Views[1];
            foreach (Attachment att in this.applicationUstn.ActiveModelReference.Attachments)
            {
                foreach (Level lv in att.Levels)
                {
                    lv.IsDisplayed = onOff;
                    lv.IsFrozen = !onOff;
                    lv.set_IsDisplayedInView(view, onOff);
                }
                this.applicationUstn.RedrawAllViews(MsdDrawingMode.Normal);
            }
        }

        internal void execKeyInDataPoint(string keyInCommand)
        {
            Point3d data;
            data.X = data.Y = data.Z = 0;
            this.applicationUstn.CadInputQueue.SendKeyin(keyInCommand);
            this.applicationUstn.CadInputQueue.SendDataPoint(ref data, this.applicationUstn.ActiveDesignFile.Views[1], 0);
        }

        internal void execKeyIn(string keyInCommand)
        {
            this.applicationUstn.CadInputQueue.SendKeyin(keyInCommand);
        }

        internal void setLayerArray(string feature, ref themes Themes)
        {
            setOnOffAllLevels(false);

            var levels = from c in Themes.theme where c.name == feature select c;
            foreach (themesTheme t in levels)
            {
                foreach (string s in t.levels)
                {
                    setLevelOnOff(s, true, true);
                }
            }
        }

        internal bool isConnected()
        {
            return this._isConnected;
        }

        internal List<string> MakeListPolygWKT(string feature,bool apostrofe)
        {
            try
            {
                PolygonLayer polygonLayer = makeTopoPolyg(feature);
                //ICollection<IGeometry> polygonLayer = makeTopoPolyg();
                if (polygonLayer != null)
                {
                    return convertTopoPolygToWKT(ref polygonLayer,apostrofe);
                    //return convertTopoPolygToWKT(ref polygonLayer, apostrofe);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        internal List<PointTextEntity> MakeListPointCveWKT(string feature)
        {
            try
            {

                ElementEnumerator enumerator = makePointEnumerator(feature);
                if (enumerator != null)
                {
                    return convertTopoPointCveToWKT(ref enumerator);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        private List<PointTextEntity> convertTopoPointCveToWKT(ref ElementEnumerator enumerator)
        {
            try
            {

                List<PointTextEntity> lstWKTGeom = new List<PointTextEntity>();
                PointTextEntity item = new PointTextEntity();
                WKTPoint origGeom = new WKTPoint();
                double rotationDegrees = 0;
                TextElement textElem;
                enumerator.Reset();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.IsTextElement())
                    {
                        textElem = enumerator.Current.AsTextElement();
                        item = new PointTextEntity();
                        origGeom = new WKTPoint();
                        origGeom.SRID = 32614;
                        origGeom.XD = textElem.get_Origin().X;
                        origGeom.YD = textElem.get_Origin().Y;
                        rotationDegrees = ((Math.Atan2(textElem.get_Rotation().RowY.X,
                                                        textElem.get_Rotation().RowX.X))
                                            / (Math.PI / 180));
                        item.originGeom = origGeom;
                        item.text = textElem.Text;
                        item.rotation = rotationDegrees;
                        lstWKTGeom.Add(item);
                    }
                }
                return lstWKTGeom;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private List<KeyValuePair<string, string>> convertTopoPointToWKT(ref ElementEnumerator enumTextElem)
        {
            try
            {
                string strWKT;
                var listGeomValue = new List<KeyValuePair<string, string>>();
                TextElement textElem;
                enumTextElem.Reset();
                while (enumTextElem.MoveNext())
                {
                    if (enumTextElem.Current.IsTextElement())
                    {
                        textElem = enumTextElem.Current.AsTextElement();
                        strWKT = string.Format("'POINT({0} {1})'", 
                            textElem.get_Origin().X.ToString(), 
                            textElem.get_Origin().Y.ToString());
                        listGeomValue.Add(new KeyValuePair<string,string>(strWKT, textElem.Text));
                    }
                }
                return listGeomValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private List<string> convertTopoPolygToWKT(ref PolygonLayer polygonLayer,bool apostrofe)
        {
            try
            {
                List<string> lstWkt = new List<string>();
                string Wkt = string.Empty;
                IArea iA;
                ICentroid iC;
                if (polygonLayer != null)
                {
                    for (int i = 1; i <= polygonLayer.Areas.Count; i++)
                    {
                        Wkt = string.Empty;
                        iA = polygonLayer.Areas[i];
                        iC = iA.Centroid;
                        if (iC != null)
                        {
                            Wkt = getEwktFromGeom(ref iA,apostrofe);
                            if (Wkt != string.Empty)
                            {
                                lstWkt.Add(Wkt);
                            }
                        }
                    }
                    return lstWkt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private List<string> convertTopoPolygToWKT(ref ICollection<IGeometry> polygonLayer, bool apostrofe)
        {
            try
            {
                List<string> lstWkt = new List<string>();
                string Wkt = string.Empty;
               
                if (polygonLayer != null)
                {
                   foreach(IGeometry pol in polygonLayer)
                    {
                        lstWkt.Add(pol.AsText());
                    }
                    return lstWkt;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        internal DgnFileEntity getCurrentDgn()
        {
            DgnFileEntity currentDgn = new DgnFileEntity();
            try
            {
                if (this.isConnected())
                {
                    currentDgn.Nombre = this.applicationUstn.ActiveDesignFile.Name;
                    currentDgn.Ruta = this.applicationUstn.ActiveDesignFile.Path + "\\";
                    return currentDgn;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Private methods

        private void setOnOffAllLevels(bool OnOff)
        {
            View view;
            view = this.applicationUstn.ActiveDesignFile.Views[1];

            foreach (Level lv in this.applicationUstn.ActiveDesignFile.Levels)
            {
                lv.IsDisplayed = OnOff;
                lv.IsFrozen = !OnOff;
                lv.set_IsDisplayedInView(view, OnOff);
            }
            this.applicationUstn.RedrawAllViews(MsdDrawingMode.Normal);
        }

        private void setLevelOnOff(string nameLevel, bool inActiveModelRef, bool OnOf)
        {
            View view;
            Level level = null;
            view = this.applicationUstn.ActiveDesignFile.Views[1];

            if (inActiveModelRef)
            {
                Levels levels = this.applicationUstn.ActiveModelReference.Levels;
                foreach (Level lvl in levels)
                {
                    if (lvl.Name.ToUpper().Trim() == nameLevel.ToUpper().Trim())
                    {
                        level = lvl;
                        break;
                    }
                }
                level = this.applicationUstn.ActiveModelReference.Levels.Find(nameLevel, null);
                if (level != null)
                {
                    level.IsDisplayed = OnOf;
                    level.IsFrozen = !OnOf;
                    level.set_IsDisplayedInView(view, OnOf);
                    this.applicationUstn.RedrawAllViews(MsdDrawingMode.Normal);
                }
            }
            else
            {
                foreach (Attachment att in this.applicationUstn.ActiveModelReference.Attachments)
                {
                    Levels levels = att.Levels;
                    foreach (Level lvl in levels)
                    {
                        if (lvl.Name.ToUpper().Trim() == nameLevel.ToUpper().Trim())
                        {
                            level = lvl;
                            break;
                        }
                    }
                    level = att.Levels.Find(nameLevel, null);
                    if (level != null)
                    {
                        level.IsDisplayed = OnOf;
                        level.IsFrozen = !OnOf;
                        level.set_IsDisplayedInView(view, OnOf);
                        this.applicationUstn.RedrawAllViews(MsdDrawingMode.Normal);
                    }
                }
            }
        }

        private void makeFenceView()
        {
            Point3d data;
            data.X = data.Y = data.Z = 0;
            this.applicationUstn.CadInputQueue.SendKeyin("fit view extended");
            this.applicationUstn.CadInputQueue.SendKeyin("place fence view");
            this.applicationUstn.CadInputQueue.SendKeyin("lock Fence overlap");
            this.applicationUstn.CadInputQueue.SendDataPoint(ref data, this.applicationUstn.ActiveDesignFile.Views[1], 0);
        }

        private PolygonLayer makeTopoPolyg(string feature)
        {
            try
            {
                this.applicationUstn.ActiveModelReference.UnselectAllElements();
                //this.applicationUstn.ActiveModelReference.UORsPerMasterUnit = 1000;
                //this.applicationUstn.ActiveModelReference.get_MasterUnit().Label;
                makeFenceView();
                PolygonLayer polLayer = this.applicationUstn.CreateObjectInMicroStation("CachedProject.PolygonLayer") as PolygonLayer;
                polLayer.Name = "polLayer";
                polLayer.ConsiderHoles = true;
                
                ElementEnumerator enumerator = this.applicationUstn.ActiveDesignFile.Fence.GetContents(false, true);
               
                enumerator.Reset();
                polLayer.AddElements(enumerator);

                
                if (polLayer.Boundaries.Count <= 0)
                {
                    return null;
                }
                polLayer.BuildAreas();
                return polLayer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private ICollection<IGeometry> makeTopoPolyg()
        {
            try
            {
                string demo = string.Empty;
                int decPositions = 11;
                //NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
                //nfi.NumberDecimalDigits = 11;
                //nfi.NumberGroupSeparator = string.Empty;
                
                ICollection<IGeometry> v_polyg;
                List<IGeometry> v_lines = new List<IGeometry>();
                PrecisionModel pm = new PrecisionModel(PrecisionModels.Floating);
                GeometryFactory geometryFactory = new GeometryFactory(pm);
                WKTReader rdr = new WKTReader(geometryFactory);

                this.applicationUstn.ActiveModelReference.UnselectAllElements();
                makeFenceView();
                ElementEnumerator enumerator = this.applicationUstn.ActiveDesignFile.Fence.GetContents(false, true);
                //int k = 0;
                while (enumerator.MoveNext())
                {
                    try
                    { 
                        if(enumerator.Current.IsLinear)
                        {
                            if (enumerator.Current.AsLineElement().VerticesCount==2)
                            {
                                v_lines.Add(rdr.Read(String.Format("LINESTRING({0} {1},{2} {3})",
                                    Math.Round(Math.Round(Double.Parse(Trunk(enumerator.Current.AsLineElement().EndPoint.X.ToString("R"), decPositions)),9),6),
                                    Math.Round(Math.Round(Double.Parse(Trunk(enumerator.Current.AsLineElement().EndPoint.Y.ToString("R"), decPositions)), 9), 6),
                                    Math.Round(Math.Round(Double.Parse(Trunk(enumerator.Current.AsLineElement().StartPoint.X.ToString("R"), decPositions)), 9), 6),
                                    Math.Round(Math.Round(Double.Parse(Trunk(enumerator.Current.AsLineElement().StartPoint.Y.ToString("R"), decPositions)), 9), 6))));

                                //demo += System.Environment.NewLine + "/*"+k+"*/" +String.Format("insert into demoedges(geom)  (select st_geomfromtext('LINESTRING({0} {1},{2} {3})'));",
                                //    Math.Round(Math.Round(Double.Parse(Trunk(enumerator.Current.AsLineElement().StartPoint.X.ToString("R"), decPositions)), 9), 6),
                                //    Math.Round(Math.Round(Double.Parse(Trunk(enumerator.Current.AsLineElement().StartPoint.Y.ToString("R"), decPositions)), 9), 6),
                                //    Math.Round(Math.Round(Double.Parse(Trunk(enumerator.Current.AsLineElement().EndPoint.X.ToString("R"), decPositions)), 9), 6),
                                //    Math.Round(Math.Round(Double.Parse(Trunk(enumerator.Current.AsLineElement().EndPoint.Y.ToString("R"), decPositions)), 9), 6));
                                
                            }
                            else 
                            {
                                string s = string.Empty;
                                for (int i = 0; i < enumerator.Current.AsLineElement().VerticesCount; i++)
                                {
                                    //v_lines.Add(rdr.Read(String.Format("LINESTRING({0} {1},{2} {3})",
                                    //Trunk(enumerator.Current.AsLineElement().GetVertices()[i].X.ToString("R"), decPositions),
                                    //Trunk(enumerator.Current.AsLineElement().GetVertices()[i].Y.ToString("R"), decPositions),
                                    //Trunk(enumerator.Current.AsLineElement().GetVertices()[i + 1].X.ToString("R"), decPositions),
                                    //Trunk(enumerator.Current.AsLineElement().GetVertices()[i + 1].Y.ToString("R"), decPositions))));
                                    if (s.Length == 0)
                                    {
                                        s += String.Format("{0} {1}",
                                            Math.Round(Math.Round(Double.Parse(Trunk(enumerator.Current.AsLineElement().GetVertices()[i].X.ToString("R"), decPositions)), 9),6),
                                            Math.Round(Math.Round(Double.Parse(Trunk(enumerator.Current.AsLineElement().GetVertices()[i].Y.ToString("R"), decPositions)), 9),6)
                                            );
                                    }
                                    else
                                    {
                                        s += String.Format(",{0} {1}",
                                            Math.Round(Math.Round(Double.Parse(Trunk(enumerator.Current.AsLineElement().GetVertices()[i].X.ToString("R"), decPositions)), 9),6),
                                            Math.Round(Math.Round(Double.Parse(Trunk(enumerator.Current.AsLineElement().GetVertices()[i].Y.ToString("R"), decPositions)), 9),6)
                                            );
                                    }
                                }
                                v_lines.Add(rdr.Read(String.Format("LINESTRING({0})", s)));
                               // demo += System.Environment.NewLine +"/*" + k + "*/" + String.Format("insert into demoedges(geom)  (select st_geomfromtext('LINESTRING({0})'));", s);
                            }
                            //k++;
                        }
                    }
                       
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        break;
                    }
                }
                if( v_lines.Count()>0)
                {
                    List<string> qrys = new List<string>();
                    foreach(IGeometry line in v_lines)
                    {
                        qrys.Add(string.Format("insert into demoedges(geom)(select st_geomfromtext('{0}'));", line.AsText()));
                    }
                    filesController controller = new filesController();
                    controller.writeLines(@"c:\temp\putosQuerys.sql", ref qrys);

                    Polygonizer polygonizer = new Polygonizer();
                    polygonizer.Add(v_lines);

                    v_polyg = polygonizer.GetPolygons();
                    if(v_polyg.Count()>0)
                    {
                        return v_polyg;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private string Trunk(string v1, int v2)
        {
            int posDec = v1.IndexOf(".");

            return v1.Substring(0, Math.Min((posDec + 1) + v2,v1.Length));
        }

        private string getEwktFromGeom(ref IArea iA, bool apostrofe)
        {
            string strEWKT = string.Empty;
            Point3d[] vertices = iA.GetVertices();
            //Límite del polígono exterior
            for (int i = 0; i < vertices.Length; i++)
            {
                if (i == 0)
                {
                    strEWKT += String.Format("({0} {1}", vertices[i].X.ToString("R"), vertices[i].Y.ToString("R"));
                }
                else
                {
                    strEWKT += String.Format(",{0} {1}", vertices[i].X.ToString("R"), vertices[i].Y.ToString("R"));
                }
            }
            if (strEWKT != string.Empty)
            {
                strEWKT += ")";
                //se buscan polígono interiores(holes)
                string strPolygonsNested = string.Empty;
                if (iA.NestedAreas != null)
                {
                    string strVertNested = string.Empty;
                    foreach (IArea hole in iA.NestedAreas)
                    {
                        strVertNested = string.Empty;
                        vertices = hole.GetVertices();
                        for (int j = 0; j < vertices.Count(); j++)
                        {
                            if (j == 0)
                            {
                                strVertNested += String.Format("({0} {1}", vertices[j].X.ToString("R"), vertices[j].Y.ToString("R"));
                            }
                            else
                            {
                                strVertNested += String.Format(",{0} {1}", vertices[j].X.ToString("R"), vertices[j].Y.ToString("R"));
                            }
                        }
                        if (strVertNested != string.Empty)
                        {
                            strVertNested += ")";
                            if (strPolygonsNested == string.Empty)
                            {
                                strPolygonsNested += strVertNested;
                            }
                            else
                            {
                                strPolygonsNested += "," + strVertNested;
                            }
                        }
                    }
                }

                if (strPolygonsNested == string.Empty)
                {
                    if (apostrofe)
                    {
                        return string.Format("'POLYGON({0})'", strEWKT);
                    }
                    else
                    {
                        return string.Format("POLYGON({0})", strEWKT);
                    }
                }
                else
                {
                    if (apostrofe)
                    {
                        return string.Format("'POLYGON({0},{1})'", strEWKT, strPolygonsNested);
                    }
                    else
                    {
                        return string.Format("POLYGON({0},{1})", strEWKT, strPolygonsNested);
                    }
                }
            }
            return string.Empty;
        }

        private Point3d[] sortCounterClockwise(Point3d[] point3d)
        {
            List<Point3d> CounterClockwise = new List<Point3d>();
            if (IsClockwise(point3d))
            {
                for (int i = (point3d.Length - 1); i >= 0; i--)
                {
                    CounterClockwise.Add(point3d[i]);
                }
                return CounterClockwise.ToArray();
            }
            else
            {
                return point3d;
            }
        }

        private Point3d[] sortCounterClockwise(Coordinate[] coordinate)
        {
            List<Point3d> CounterClockwise = new List<Point3d>();
            Point3d pto = new Point3d();
            if (IsClockwise(coordinate))
            {
                for (int i = (coordinate.Length - 1); i >= 0; i--)
                {
                    pto = new Point3d();
                    pto.X = coordinate[i].X;
                    pto.Y = coordinate[i].Y;
                    CounterClockwise.Add(pto);
                }
                return CounterClockwise.ToArray();
            }
            else
            {
                for (int i = 0; i < coordinate.Length; i++)
                {
                    pto = new Point3d();
                    pto.X = coordinate[i].X;
                    pto.Y = coordinate[i].Y;
                    CounterClockwise.Add(pto);
                }
                return CounterClockwise.ToArray();
            }
        }

        private bool IsClockwise(Coordinate[] coordinate)
        {
            //(x2-x1)(y2+y1)
            double sum = 0.0;
            for (int i = 0; i < coordinate.Length; i++)
            {
                sum += (coordinate[(i + 1) % coordinate.Length].X - coordinate[i].X) *
                    (coordinate[(i + 1) % coordinate.Length].Y + coordinate[i].Y);
            }
            return sum > 0.0;
        }

        private Point3d[] sortClockwise(Point3d[] point3d)
        {
            List<Point3d> Clockwise = new List<Point3d>();
            if (IsClockwise(point3d))
            {
                return point3d;
            }
            else
            {
                for (int i = (point3d.Length - 1); i >= 0; i--)
                {
                    Clockwise.Add(point3d[i]);
                }
                return Clockwise.ToArray();
            }
        }

        private Point3d[] sortClockwise(Coordinate[] coordinate)
        {
            List<Point3d> Clockwise = new List<Point3d>();
            Point3d pto = new Point3d();
            if (IsClockwise(coordinate))
            {
                for (int i = 0; i < coordinate.Length; i++)
                {
                    pto = new Point3d();
                    pto.X = coordinate[i].X;
                    pto.Y = coordinate[i].Y;
                    Clockwise.Add(pto);
                }
                return Clockwise.ToArray();
            }
            else
            {
                for (int i = (coordinate.Length - 1); i >= 0; i--)
                {
                    pto = new Point3d();
                    pto.X = coordinate[i].X;
                    pto.Y = coordinate[i].Y;
                    Clockwise.Add(pto);
                }
                return Clockwise.ToArray();
            }
        }

        private bool IsClockwise(Point3d[] point3d)
        {
            //(x2-x1)(y2+y1)
            double sum = 0.0;
            for (int i = 0; i < point3d.Length ; i++)
            {
                sum += (point3d[(i + 1) % point3d.Length].X - point3d[i].X) * 
                    (point3d[(i + 1) % point3d.Length].Y + point3d[i].Y);
            }
            return sum > 0.0;
        }

        internal List<LineEntity> MakeListLineWkt(string feature)
        {
            try
            {

                ElementEnumerator enumerator = makeLineEnumerator(feature);
                if (enumerator != null)
                {
                    return convertLineToWkt(ref enumerator);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        private List<LineEntity> convertLineToWkt(ref ElementEnumerator enumerator)
        {
            try
            {

                List<LineEntity> lstWktGeom = new List<LineEntity>();
                LineEntity geom = new LineEntity();
                LineElement lineElement;
                enumerator.Reset();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.IsLineElement())
                    {
                        lineElement = enumerator.Current.AsLineElement();
                        geom = new LineEntity();
                        if (lineElement.GetVertices().Count() > 0)
                        {
                            geom = getWktFromGeom(ref lineElement);
                            if (geom != null)
                            {
                                lstWktGeom.Add(geom);
                            }
                        }
                    }
                }
                return lstWktGeom;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private LineEntity getWktFromGeom(ref LineElement lineElement)
        {
            LineEntity geom = new LineEntity();
            List<Coord2dEntity> coords = new List<Coord2dEntity>();
            WKTLine wktLine = new WKTLine();
           Point3d[] vertices = lineElement.GetVertices();
            List<double> secXY = new List<double>();
            if (vertices.Length > 0)
            {
                for (int i = 0; i < vertices.Length; i++)
                {
                    coords.Add(new Coord2dEntity(vertices[i].X, vertices[i].Y));
                }
                wktLine.secXYD = coords;
                //wktLine.SRID=Constante SRID

                geom.text = string.Empty;
                geom.rotation = 0.0;
                geom.lineGeom = wktLine;
                return geom;
            }
            else
            {
                return null;
            }
        }

        private ElementEnumerator makePointEnumerator(string feature)
        {
            try
            {
                this.applicationUstn.ActiveModelReference.UnselectAllElements();
                makeFenceView();
                ElementEnumerator enumerator = this.applicationUstn.ActiveDesignFile.Fence.GetContents(false, true);
                return enumerator;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private ElementEnumerator makeLineEnumerator(string feature)
        {
            try
            {
                this.applicationUstn.ActiveModelReference.UnselectAllElements();
                makeFenceView();
                ElementEnumerator enumerator = this.applicationUstn.ActiveDesignFile.Fence.GetContents(false, true);
                return enumerator;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        
        #endregion



        private Point3d[] getGCoordTransform(Point3d[] point3d, string geomType)
        {
            int maxVertex=5000;
            Element oElement;

            Point3d[] ptsArray = null;
            long id;
            try
            {
                if (point3d.Count() > maxVertex)
                {
                    Element cElement;
                    List<ChainableElement> lCe = new List<ChainableElement>();
                    List<Point3d> listInterna = new List<Point3d>();

                    List<Point3d[]> splitted = new List<Point3d[]>();
                    int arrayLength = point3d.Length;
                    for (int i = 0; i < arrayLength; i = i + maxVertex)
                    {
                        if (arrayLength < i + maxVertex)
                        {
                            maxVertex = arrayLength - i;
                        }
                        Point3d[] val = new Point3d[maxVertex];
                        Array.Copy(point3d, i, val, 0, maxVertex);
                        splitted.Add(val);
                        cElement = this.applicationUstn.CreateLineElement1(null, ref val);
                        lCe.Add(cElement.AsChainableElement());   
                    }

                    if (lCe.Count() > 0)
                    {
                        ChainableElement[] ce;
                        ce = lCe.ToArray();
                        oElement = this.applicationUstn.CreateComplexShapeElement1(ref ce);
                        this.applicationUstn.ActiveModelReference.AddElement(oElement);
                        oElement.Redraw(MsdDrawingMode.Normal);

                        this.applicationUstn.ActiveModelReference.SelectElement(oElement, true);
                        execKeyInDataPoint("gcoord transform element");
                        this.applicationUstn.ActiveModelReference.UnselectAllElements();
                        id = oElement.AsComplexShapeElement().ID;

                        oElement = this.applicationUstn.ActiveModelReference.GetElementByID(ref id);

                        this.applicationUstn.ActiveModelReference.SelectElement(oElement, true);
                        ElementEnumerator ElemEnum = oElement.AsComplexShapeElement().GetSubElements();
                        while (ElemEnum.MoveNext())
                        {
                            if(ElemEnum.Current.IsLineElement())
                            {
                                foreach(Point3d vert in ElemEnum.Current.AsLineElement().GetVertices())
                                {
                                    listInterna.Add(vert);
                                }
                            }
                        }
                        ptsArray = listInterna.ToArray();
                        execKeyInDataPoint("delete");
                    }
                }
                else
                {
                    oElement = this.applicationUstn.CreateShapeElement1(null, ref point3d, MsdFillMode.UseActive);
                    this.applicationUstn.ActiveModelReference.AddElement(oElement);
                    oElement.Redraw(MsdDrawingMode.Normal);
                    this.applicationUstn.ActiveModelReference.SelectElement(oElement, true);
                    execKeyInDataPoint("gcoord transform element");
                    this.applicationUstn.ActiveModelReference.UnselectAllElements();

                    id = oElement.AsShapeElement().ID;
                    oElement = this.applicationUstn.ActiveModelReference.GetElementByID(ref id);

                    this.applicationUstn.ActiveModelReference.SelectElement(oElement, true);
                    ptsArray = oElement.AsShapeElement().GetVertices();
                    execKeyInDataPoint("delete");
                }
                return ptsArray;
            }
            catch
            {
                return null;
            }
        }

     
    }
}
