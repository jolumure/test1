using FL;
using PresentacionWFA.Controller;
using PresentacionWFA.Data;
using PresentacionWFA.Entity_temp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PresentacionWFA
{
    public partial class Extractor : Form
    {
        #region Variables        
        private EPSGController epsgController;//Jeciel
        private UstnController UstnController;
        private DgnFileEntity dgnOriginal;
        //private DgnFileEntity dgnSeed1; //No se usan 
        //private DgnFileEntity dgnSeed2;//No se usan 
        //private DgnFileEntity dgnSeed1Work;//No se usan 
        //private DgnFileEntity dgnSeed2Work;//No se usan 
        private List<DgnFileEntity> listFiles;
        private themes Themes;
        /// <summary>
        /// TODO: Agregar SQLColofon a app.config
        /// </summary>
        private string SQLColofon = string.Empty;

        #endregion

        public Extractor()
        {
            InitializeComponent();
            if (InitializeDGN())
            {
                this.btnExtract.Enabled = true;
                // InitializeLevelNames();
                InitializeLevelCategories(); //<------------ Jeciel
                this.SQLColofon = "SELECT pkg__consolidation_cadastral('{0}');";
            }
            else
            {
                logError("Sin instancia de Bentley Map ejecutandose");
                this.btnExtract.Enabled = false;
            }
        }

        private bool InitializeDGN()
        {
            try
            {
                this.UstnController = new UstnController();
                if (this.UstnController.setConnected())
                {
                    this.dgnOriginal = this.UstnController.getCurrentDgn();
                    if (this.dgnOriginal == null)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                logError("");
                return false;
            }
        }

        private void logError(string p)
        {
            this.tBoxLog.Text += System.Environment.NewLine + p;
        }

        private void InitializeLevelNames()
        {
            if (this.UstnController.isConnected())
            {
                levelsController controllerLevels = new levelsController();
                this.Themes = new themes();
                this.Themes = controllerLevels.readLevelNames();
                if (Themes != null)
                {
                    for (int i = 0; i < Themes.theme.Count; i++)
                    {
                        this.chkListLevels.Items.Add(Themes.theme[i].name, true);
                    }
                }
            }
        }

        #region JECIEL
        /**************************************************************/
        /*                      Jeciel                                */
        /*              aquí cambié Names <----->Categories           */
        private void InitializeLevelCategories()
        {
            if (this.UstnController.isConnected())
            {
                CategoriesController controllerCategory = new CategoriesController();
                this.Themes = new themes();
                this.Themes = controllerCategory.readLevelCategories();

                List<string> categoriesNames = controllerCategory.getCategories(this.Themes);

                if (categoriesNames != null)
                {
                    for (int i = 0; i < categoriesNames.Count; i++)
                    {
                        this.chkListLevels.Items.Add(categoriesNames[i], true);
                    }

                    /*RATION BUTTON*/
                    /*List<RadioButton> categoriesList = new List<RadioButton>(Themes.theme.Count);

                    categoriesList.Add(new RadioButton());
                    categoriesList[0].Text = Themes.theme[0].name;
                    this.panelListCategories.Controls.Add(categoriesList[0]);

                    for (int i = 1; i < Themes.theme.Count; i++)
                    {
                        categoriesList.Add(new RadioButton());
                        categoriesList[i].Text = Themes.theme[i].name;
                        this.panelListCategories.Controls.Add(categoriesList[i]);
                        categoriesList[i].Location = new System.Drawing.Point(categoriesList[i - 1].Location.X, categoriesList[i - 1].Location.Y + 20);
                    }*/
                }
            }
        }

        /************************FIN JECIEL****************************/
        #endregion





        #region Events

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            //TODO : Agregár a variables del programa: carpeta de inicio y filtros de extensión 
            openFileDialog1.InitialDirectory = @"c:\";
            //openFileDialog1.DefaultExt = "*.PCF";
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileNames.Count() > 0)
                {
                    this.listFiles = new List<DgnFileEntity>();
                    DgnFileEntity dgn;
                    foreach (string file in openFileDialog1.FileNames)
                    {
                        dgn = new DgnFileEntity();
                        if (setWorkFile(file, ref dgn))
                        {
                            this.listFiles.Add(dgn);
                            this.lBoxFiles.Items.Add(dgn.Nombre, true);
                        }
                    }
                }
            }
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkListLevels.Items.Count; i++)
            {
                chkListLevels.SetItemChecked(i, true);
            }
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkListLevels.Items.Count; i++)
            {
                chkListLevels.SetItemChecked(i, false);
            }
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            if (this.UstnController.isConnected())
            {
                foreach (var v in this.lBoxFiles.CheckedItems)
                {
                    var CurrentDgn = from c in this.listFiles where c.Nombre == v.ToString() select c;
                    DateTime inicio, fin;
                    inicio = System.DateTime.Now;
                    foreach (DgnFileEntity file in CurrentDgn)
                    {
                        this.UstnController.openDgn(file, false);
                        processExtractor(file.Nombre);
                        if (this.SQLColofon != string.Empty)
                        {
                            string colofon = string.Format(this.SQLColofon, file.Nombre.Split('.')[0]);
                            filesController fileController = new filesController();
                            fileController.writeLines(PresentacionWFA.Properties.Resources.pathTemp + file.Nombre.Split('.')[0] + ".SQL", ref colofon);
                        }
                    }
                    filesController controllerZip = new filesController();
                    string zipFile = controllerZip.makeZIP(Properties.Resources.pathTemp + v.ToString().Split('.')[0] + ".SQL");

                    if (zipFile != string.Empty)
                    {
                        controllerZip.removeFile(Properties.Resources.pathTemp + v.ToString().Split('.')[0] + ".SQL");
                        byte[] zipArray = controllerZip.getArrayBytes(zipFile);

                        if (zipArray != null)
                        {
                            ETBatchProcFL fl = new ETBatchProcFL();
                             //long retVal = fl.insertScript(zipArray);
                            fl.insertScript(v.ToString().Split('.')[0], Properties.Resources.pathTemp, zipArray, ConfigurationManager.AppSettings["host"], ConfigurationManager.AppSettings["puerto"]);// Jeciel
                        }
                        else
                        {
                            //Alert function
                        }

                    }

                    fin = System.DateTime.Now;
                    System.TimeSpan duration = fin - inicio;
                    //this.tBoxLog.Text += Environment.NewLine + v.ToString() + " Fin ::: " + fin.ToString() + " :::";
                    this.tBoxLog.Text += Environment.NewLine + v.ToString() + " Duración ::: " + duration.TotalMinutes.ToString() + " :::";
                }
            }
        }

        //Jeciel
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<EPSGEntity> EPSGEList = new List<EPSGEntity>();
            epsgController = new EPSGController();

            if (cB_opcion.SelectedItem != null && tB_opcion.Text != "")
            {
                cBox_resultados.Items.Clear();
                int max = 0;
                EPSGEList = epsgController.getEPSGEList(cB_opcion.SelectedItem.ToString(), tB_opcion.Text);
                if (EPSGEList.Count != 0)
                {
                    max = 0;
                    foreach (EPSGEntity epsg in EPSGEList)
                    {
                        if (epsg.ToString().Length > max)
                            max = epsg.ToString().Length;
                        cBox_resultados.Items.Add(epsg);
                    }
                    cBox_resultados.DropDownWidth = max * 2;
                }
            }
        }
        #endregion

        private void processExtractor(string file)
        {
            /* Original*/
            /* foreach (string feature in this.chkListLevels.CheckedItems)
             {
                 ProcessFeature(feature, file);
             }*/

            /* Jeciel */
            foreach (string categoria in this.chkListLevels.CheckedItems)
            {
                foreach (string feature in new CategoriesController().getLevelNames(this.Themes, categoria))
                    ProcessFeature(feature, file);
            }
        }

        private void ProcessFeature(string feature, string file)
        {
            if (this.UstnController.isConnected())
            {
                this.UstnController.setLayerArray(feature, ref Themes);
                levelsController levelsController = new levelsController();
                switch (levelsController.getTypeFeature(feature, this.Themes))
                {
                    case "polygon":
                        //WKT
                        List<string> polygonsWkt = new List<string>();
                        polygonsWkt = makeWKTPolygons(feature, false);
                        if (polygonsWkt != null)
                        {
                            makeInsertPolygon(feature, ref polygonsWkt, file);
                        }
                        break;
                    case "point":
                        List<PointTextEntity> pointsCve = new List<PointTextEntity>();
                        pointsCve = makeWKTPoints(feature);
                        if (pointsCve != null)
                        {
                            makeInsertPoint(feature, ref pointsCve, file);
                        }
                        break;
                    case "line":
                        List<LineEntity> LinesWkt = new List<LineEntity>();
                        LinesWkt = makeWktLines(feature);
                        if (LinesWkt != null)
                        {
                            makeInsertLine(feature, ref LinesWkt, file);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void makeInsertLine(string feature, ref List<LineEntity> linesWkt, string file)
        {
            switch (feature)
            {
                case "Calle":
                    if (!chkBoxSQLOutput.Checked)
                    {

                    }
                    else
                    {
                        string fileSQL = makeSQLLine(ref linesWkt, feature, file);
                        if (fileSQL != string.Empty)
                        {

                        }
                        else
                        {
                            //Alert, arror al escribir archivo
                        }
                    }
                    break;
                default: break;
            }
        }

        private void makeInsertPoint(string feature, ref List<PointTextEntity> pointsCve, string file)
        {
            switch (feature)
            {
                case "Manzana Clave":
                    if (!chkBoxSQLOutput.Checked)
                    {
                    }
                    else
                    {
                        string fileSQL = makeSQLPointCve(ref pointsCve, feature, file);
                        if (fileSQL != string.Empty)
                        {
                            //filesController controller = new filesController();
                            //string fileZip = controller.makeZIP(fileSQL);
                            //Alert, archivo creado correctamente
                        }
                        else
                        {
                            //Alert, arror al escribir archivo
                        }
                    }
                    break;
                case "Predio Clave":
                    if (!chkBoxSQLOutput.Checked)
                    {
                        //EGPredioClaveFL _obj = new EGPredioClaveFL();
                        //foreach (KeyValuePair<string, SdoGeometry> geo in pointsCve)
                        //    _obj.insertPredClave(geo.Value, geo.Key);
                    }
                    else
                    {
                        string fileSQL = makeSQLPointCve(ref pointsCve, feature, file);
                        if (fileSQL != string.Empty)
                        {
                            //Alert, archivo creado correctamente
                        }
                        else
                        {
                            //Alert, arror al escribir archivo
                        }
                    }
                    break;
                case "Construcción Clave":
                    if (!chkBoxSQLOutput.Checked)
                    {
                        //EGConstruccionClaveFL __obj = new EGConstruccionClaveFL();
                        //foreach (KeyValuePair<string, SdoGeometry> geo in pointsCve)
                        //    __obj.insertConstrClave(geo.Value, geo.Key);
                    }
                    else
                    {
                        string fileSQL = makeSQLPointCve(ref pointsCve, feature, file);
                        if (fileSQL != string.Empty)
                        {
                            //Alert, archivo creado correctamente
                        }
                        else
                        {
                            //Alert, arror al escribir archivo
                        }
                    }
                    break;
                case "Número Oficial":
                    if (!chkBoxSQLOutput.Checked)
                    {
                        //EGConstruccionClaveFL __obj = new EGConstruccionClaveFL();
                        //foreach (KeyValuePair<string, SdoGeometry> geo in pointsCve)
                        //    __obj.insertConstrClave(geo.Value, geo.Key);
                    }
                    else
                    {
                        string fileSQL = makeSQLPointCve(ref pointsCve, feature, file);
                        if (fileSQL != string.Empty)
                        {
                            //Alert, archivo creado correctamente
                        }
                        else
                        {
                            //Alert, arror al escribir archivo
                        }
                    }
                    break;
                //Delegación Clave
                case "Delegación Clave":
                    if (!chkBoxSQLOutput.Checked)
                    {
                        //EGConstruccionClaveFL __obj = new EGConstruccionClaveFL();
                        //foreach (KeyValuePair<string, SdoGeometry> geo in pointsCve)
                        //    __obj.insertConstrClave(geo.Value, geo.Key);
                    }
                    else
                    {
                        string fileSQL = makeSQLPointCve(ref pointsCve, feature, file);
                        if (fileSQL != string.Empty)
                        {
                            //Alert, archivo creado correctamente
                        }
                        else
                        {
                            //Alert, arror al escribir archivo
                        }
                    }
                    break;
                case "Colonia Clave":
                    if (!chkBoxSQLOutput.Checked)
                    {
                        //EGConstruccionClaveFL __obj = new EGConstruccionClaveFL();
                        //foreach (KeyValuePair<string, SdoGeometry> geo in pointsCve)
                        //    __obj.insertConstrClave(geo.Value, geo.Key);
                    }
                    else
                    {
                        string fileSQL = makeSQLPointCve(ref pointsCve, feature, file);
                        if (fileSQL != string.Empty)
                        {
                            //Alert, archivo creado correctamente
                        }
                        else
                        {
                            //Alert, arror al escribir archivo
                        }
                    }
                    break;
                case "Calle Clave":
                    if (!chkBoxSQLOutput.Checked)
                    {
                        //EGConstruccionClaveFL __obj = new EGConstruccionClaveFL();
                        //foreach (KeyValuePair<string, SdoGeometry> geo in pointsCve)
                        //    __obj.insertConstrClave(geo.Value, geo.Key);
                    }
                    else
                    {
                        string fileSQL = makeSQLPointCve(ref pointsCve, feature, file);
                        if (fileSQL != string.Empty)
                        {
                            //Alert, archivo creado correctamente
                        }
                        else
                        {
                            //Alert, arror al escribir archivo
                        }
                    }
                    break;
                case "Índice cartográfico Clave":
                    if (!chkBoxSQLOutput.Checked)
                    {
                    }
                    else
                    {
                        string fileSQL = makeSQLPointCve(ref pointsCve, feature, file);
                        if (fileSQL != string.Empty)
                        {
                            //Alert, archivo creado correctamente
                        }
                        else
                        {
                            //Alert, arror al escribir archivo
                        }
                    }
                    break;
                case "Default":
                    if (!chkBoxSQLOutput.Checked)
                    {
                    }
                    else
                    {
                        string fileSQL = makeSQLPointCve(ref pointsCve, feature, file);
                        if (fileSQL != string.Empty)
                        {
                            //Alert, archivo creado correctamente
                        }
                        else
                        {
                            //Alert, arror al escribir archivo
                        }
                    }
                    break;



                default: break;
            }
        }

        private string makeSQLPointCve(ref List<PointTextEntity> pointsCve, string feature, string file)
        {
            SQLFormatController controllerSQL = new SQLFormatController();
            filesController controllerFiles = new filesController();
            try
            {
                List<string> listSQLs = controllerSQL.makeInsertPoint(feature, ref pointsCve, 32613);
                string[] split = file.Split('.');
                if (split.Count() > 0)
                {
                    string filename = PresentacionWFA.Properties.Resources.pathTemp + split[0] + ".SQL";
                    controllerFiles.writeLines(filename, ref listSQLs);
                    return filename;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        private string makeSQLPolygon(ref List<string> polygonsWkt, string feature, string file)
        {
            SQLFormatController controllerSQL = new SQLFormatController();
            filesController controllerFiles = new filesController();
            try
            {
                List<string> listSQLs = controllerSQL.makeInsertPolygon(feature, ref polygonsWkt, 0.0005, 32613);
                string[] split = file.Split('.');
                if (split.Count() > 0)
                {
                    string filename = PresentacionWFA.Properties.Resources.pathTemp + split[0] + ".SQL";
                    controllerFiles.writeLines(filename, ref listSQLs);
                    return filename;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        private string makeSQLLine(ref List<LineEntity> linesWkt, string feature, string file)
        {
            SQLFormatController controllerSQL = new SQLFormatController();
            filesController controllerFiles = new filesController();
            try
            {
                List<string> listSQLs = controllerSQL.makeInsertLine(feature, ref linesWkt);
                string[] split = file.Split('.');
                if (split.Count() > 0)
                {
                    string filename = PresentacionWFA.Properties.Resources.pathTemp + split[0] + ".SQL";
                    controllerFiles.writeLines(filename, ref listSQLs);
                    return filename;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        private void makeInsertPolygon(string feature, ref List<string> polygonsWkt, string file)
        {
            switch (feature)
            {
                case "Manzana":
                    if (!chkBoxSQLOutput.Checked)
                    {
                        //EGManzanaFL obj = new EGManzanaFL();
                        //foreach (SdoGeometry geo in polygonsSdo)
                        //    obj.insertManzPolig(geo);
                    }
                    else
                    {
                        string fileSQL = makeSQLPolygon(ref polygonsWkt, feature, file);
                        if (fileSQL != string.Empty)
                        {
                            //Alert, archivo creado correctamente
                        }
                        else
                        {
                            //Alert, arror al escribir archivo
                        }
                    }
                    break;
                case "Predio":
                    if (!chkBoxSQLOutput.Checked)
                    {
                        //EGPredioFL _obj = new EGPredioFL();
                        //foreach (SdoGeometry geo in polygonsSdo)
                        //    _obj.insertPredPolig(geo);
                    }
                    else
                    {
                        string fileSQL = makeSQLPolygon(ref polygonsWkt, feature, file);
                        if (fileSQL != string.Empty)
                        {
                            //Alert, archivo creado correctamente
                        }
                        else
                        {
                            //Alert, error al escribir archivo
                        }
                    }
                    break;
                case "Construcción":
                    if (!chkBoxSQLOutput.Checked)
                    {
                        //EGConstruccionFL __obj = new EGConstruccionFL();
                        //foreach (SdoGeometry geo in polygonsSdo)
                        //    __obj.insertConstrPolig(geo);
                    }
                    else
                    {
                        string fileSQL = makeSQLPolygon(ref polygonsWkt, feature, file);
                        if (fileSQL != string.Empty)
                        {
                            //Alert, archivo creado correctamente
                        }
                        else
                        {
                            //Alert, arror al escribir archivo
                        }
                    }
                    break;
                case "Delegación":
                    if (!chkBoxSQLOutput.Checked)
                    {
                        //EGConstruccionFL __obj = new EGConstruccionFL();
                        //foreach (SdoGeometry geo in polygonsSdo)
                        //    __obj.insertConstrPolig(geo);
                    }
                    else
                    {
                        string fileSQL = makeSQLPolygon(ref polygonsWkt, feature, file);
                        if (fileSQL != string.Empty)
                        {
                            //Alert, archivo creado correctamente
                        }
                        else
                        {
                            //Alert, arror al escribir archivo
                        }
                    }
                    break;
                case "Colonia":
                    if (!chkBoxSQLOutput.Checked)
                    {
                        //EGConstruccionFL __obj = new EGConstruccionFL();
                        //foreach (SdoGeometry geo in polygonsSdo)
                        //    __obj.insertConstrPolig(geo);
                    }
                    else
                    {
                        string fileSQL = makeSQLPolygon(ref polygonsWkt, feature, file);
                        if (fileSQL != string.Empty)
                        {
                            //Alert, archivo creado correctamente
                        }
                        else
                        {
                            //Alert, arror al escribir archivo
                        }
                    }
                    break;
                case "Índice cartográfico":
                    if (!chkBoxSQLOutput.Checked)
                    {
                    }
                    else
                    {
                        string fileSQL = makeSQLPolygon(ref polygonsWkt, feature, file);
                        if (fileSQL != string.Empty)
                        {
                            //Alert, archivo creado correctamente
                        }
                        else
                        {
                            //Alert, arror al escribir archivo
                        }
                    }
                    break;
                case "Default":
                    break;
                default: break;
            }
        }

        private List<string> makeWKTPolygons(string feature, bool apostrofe)
        {
            try
            {
                List<string> pols = new List<string>();

                if (this.UstnController.isConnected())
                {
                    return (this.UstnController.MakeListPolygWKT(feature, apostrofe));
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

        private List<PointTextEntity> makeWKTPoints(string feature)
        {
            try
            {
                if (this.UstnController.isConnected())
                {
                    return (this.UstnController.MakeListPointCveWKT(feature));
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

        private List<LineEntity> makeWktLines(string feature)
        {
            try
            {
                if (this.UstnController.isConnected())
                {
                    return (this.UstnController.MakeListLineWkt(feature));
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

        private bool setWorkFile(string file, ref DgnFileEntity dgn)
        {
            filesController controller = new filesController();
            try
            {
                return controller.setFile(file, ref dgn);
            }
            catch
            {
                logError("error");
                return false;
            }
        }

        //Jeciel
        private void cBox_resultados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_resultados.Items.Count > 0)
            {
                int selectedIndex = cBox_resultados.SelectedIndex;
                //EPSGOrigen = (EPSGEntity)cBox_resultados.SelectedItem;
                lOrigenEPSG.Text = ((EPSGEntity)cBox_resultados.SelectedItem).epsg.ToString();
            }
        }
    }
}
