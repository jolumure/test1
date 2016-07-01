using FL;
using PresentacionWFA.Controller;
using PresentacionWFA.Data;
using Entity_Temp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Media;
using System.Reflection;
using System.Resources;
using System.Collections;
using System.IO;
using System.ComponentModel;
using System.Threading;

namespace PresentacionWFA
{
    public partial class Extractor : Form
    {
        #region Variables        
        private EPSGController epsgController;//Jeciel
        private LogController logController; //Jeciel
        private bool numeros = false; //Jeciel

        //private HashSet<LogEntity> listaLog; //Jeciel
        IEnumerable<LogEntity> listaLog;
        private UstnController UstnController;
        private DgnFileEntity dgnOriginal;
        private List<DgnFileEntity> listFiles;
        private themes Themes;
        private BackgroundWorker extraer;
        delegate void SetTextCallback(string text);
        delegate void setLogCallback(IEnumerable<LogEntity> lista);
        BackgroundWorker logWorker;

        public STATUS status;
        //private DgnFileEntity dgnSeed1; //No se usan 
        //private DgnFileEntity dgnSeed2;//No se usan 
        //private DgnFileEntity dgnSeed1Work;//No se usan 
        //private DgnFileEntity dgnSeed2Work;//No se usan /// <summary>
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

                this.logWorker = new BackgroundWorker();
                this.logWorker.WorkerReportsProgress = true;
                try
                {
                    this.logWorker.DoWork += (obj, e) => this.getLog();
                    this.logWorker.RunWorkerAsync();
                }
                catch (Exception error)
                {
                    logError("Error de hilos" + error.Message);
                }
            }
            else
            {
                logError("ERROR: Sin instancia de Bentley Map ejecutandose");
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
                logError("ERROR: al Inicializar el DGN");
                return false;
            }
        }

        private void logError(string p)
        {
            this.tBoxLog.Text += System.Environment.NewLine + p;
            SystemSounds.Exclamation.Play();
        }

        private void processExtractor(string file)
        {
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

        //Jeciel
        private void InitializeLevelCategories()
        {
            if (this.UstnController.isConnected())
            {
                CategoriesController controllerCategory = new CategoriesController();
                this.Themes = new themes();
                List<string> categoriesNames = null;

                try
                {
                    this.Themes = controllerCategory.readLevelCategories();
                    categoriesNames = controllerCategory.getCategories(this.Themes);
                }
                catch (ArgumentNullException e)
                {
                    logError("ERROR: Está mal la dirección del archivo \n " + e.Message);
                    categoriesNames = null;
                }
                catch (FileLoadException e)
                {
                    logError("ERROR: El archivo no se puede leer\n " + e.Message);
                    categoriesNames = null;
                }
                catch (FileNotFoundException e)
                {
                    logError("ERROR: El archivo no se puede ecnontrar\n " + e.Message);
                    categoriesNames = null;
                }
                catch (Exception e)
                {
                    logError("ERROR: No se pudo leer el archivo XML\n " + e.Message);
                    categoriesNames = null;
                }

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
            else
                logError("ERROR: No se pudo conectar");
        }

        #region Events

        //Jeciel
        private void btnDetenerLog_Click(object sender, EventArgs e)
        {
            /* if (this.logWorker != null && this.logWorker.IsBusy)
             {
                 this.logWorker.CancelAsync();
             }*/

        }//No usado

        //Jeciel
        private void cBox_resultados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBox_resultados.Items.Count > 0)
            {
                int selectedIndex = cBox_resultados.SelectedIndex;
                lOrigenEPSG.Text = ((EPSGEntity)cBox_resultados.SelectedItem).epsg.ToString();
            }
        }

        //Jeciel
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception e2)
            {
                this.tBoxLog.Text += Environment.NewLine + e2.Message;
            }

        }

        //Jeciel
        private void btnCleanSelection_Click(object sender, EventArgs e)
        {
            this.listFiles.Clear();
            this.lBoxFiles.Items.Clear();
        }

        //Jeciel
        private void cBox_opcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tBox_opcion.Text = "";
            switch (cBox_opcion.Text)
            {

                case "UTM Zone":
                    this.tBox_opcion.Visible = false;
                    this.cBox_numeros.Visible = true;
                    numeros = false;
                    break;
                case "SRID":
                    this.cBox_numeros.Visible = false;
                    this.tBox_opcion.Visible = true;
                    numeros = true;
                    break;
                case "Ciudad":
                    this.cBox_numeros.Visible = false;
                    this.tBox_opcion.Visible = true;
                    numeros = false;
                    break;
                default:
                    this.tBox_opcion.Visible = false;
                    this.cBox_numeros.Visible = false;
                    numeros = false;
                    break;
            }
        }

        //Jeciel
        private void tBox_opcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (numeros && !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Jeciel
        private void cBox_numeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Jeciel
        private void Extractor_Load(object sender, EventArgs e)
        {
            this.cBox_opcion.SelectedIndex = 0;
            this.cBox_numeros.SelectedIndex = 0;
            List<LogEntity> lista_temp = new List<LogEntity>();
            lista_temp.Add(new LogEntity { carta = "", estatus = STATUS.EJECUTANDO, tiempo = "" });
            /*var bindingList = new BindingList<LogEntity>(lista_temp);
            var source = new BindingSource(bindingList, null);
            this.logGridView.DataSource = source;*/
        }

        //Jeciel
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (this.cBox_opcion.SelectedIndex != 0)
            {
                int max = 0;
                IEnumerable<EPSGEntity> EPSGEList = null;
                epsgController = new EPSGController();

                if (cBox_opcion.Text == "UTM Zone")
                    EPSGEList = epsgController.getEPSGEList(this.cBox_opcion.SelectedItem.ToString(), this.cBox_numeros.Text);
                else
                {
                    if (tBox_opcion.Text.Trim(' ') != "")
                        EPSGEList = epsgController.getEPSGEList(this.cBox_opcion.SelectedItem.ToString(), this.tBox_opcion.Text);
                }

                if (EPSGEList != null)
                {
                    this.cBox_resultados.Items.Clear();
                    max = 0;
                    foreach (EPSGEntity epsg in EPSGEList)
                    {
                        if (epsg.ToString().Length > max)
                            max = epsg.ToString().Length;
                        this.cBox_resultados.Items.Add(epsg);
                    }
                    this.cBox_resultados.DropDownWidth = max * 2;
                }
            }
        }

        //Jeciel
        private void getLog()
        {
            logController = new LogController();

            while (true)
            {
                this.listaLog = logController.getLog();
                if (this.listaLog != null)
                {
                    this.setLog(this.listaLog);
                }
                Thread.Sleep(5000);
            }
        }

        //Jeciel
        private void btnIniciarLog_Click(object sender, EventArgs e)
        {
            //this.getLog();
        }

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
            this.btnExtract.Enabled = false;
            this.groupBox_Transformacion.Enabled = false;
            this.groupBox_Capas.Enabled = false;
            this.groupBox_Archivos.Enabled = false;


            this.extraer = new BackgroundWorker();
            this.extraer.WorkerReportsProgress = true;
            try
            {
                this.extraer.DoWork += new DoWorkEventHandler(extraerDgns);
                this.extraer.ProgressChanged += new ProgressChangedEventHandler(extraer_ProgressChanged);
                this.extraer.RunWorkerCompleted += extraerDgns_Terminado;
                this.extraer.RunWorkerAsync();
            }
            catch (Exception error)
            {
                logError("Error de hilos" + error.Message);

            }
        }

        #endregion

        #region hilos

        //Jeciel
        private void extraerDgns(object sender, DoWorkEventArgs e)
        {
            if (this.UstnController.isConnected())
            {
                Double progreso = 0;
                Double total = this.lBoxFiles.CheckedItems.Count;
                this.extraer.ReportProgress(0);
                foreach (var v in this.lBoxFiles.CheckedItems)
                {
                    bool retVal = false;
                    var CurrentDgn = from c in this.listFiles where c.Nombre == v.ToString() select c;
                    DateTime inicio = System.DateTime.Now, fin;
                    string zipFile = string.Empty;

                    foreach (DgnFileEntity file in CurrentDgn)
                    {
                        try
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
                        catch (InvalidCastException error)
                        {
                            SetLogError("ERROR: Problemas con la instancia de Bentley." + Environment.NewLine + error.Message);
                            break;
                        }
                    }
                    filesController controllerZip = new filesController();
                    zipFile = controllerZip.makeZIP(Properties.Resources.pathTemp + v.ToString().Split('.')[0] + ".SQL");

                    if (zipFile != string.Empty)
                    {
                        controllerZip.removeFile(Properties.Resources.pathTemp + v.ToString().Split('.')[0] + ".SQL");
                        byte[] zipArray = controllerZip.getArrayBytes(zipFile);

                        if (zipArray != null)
                        {
                            ETBatchProcFL fl = new ETBatchProcFL();
                            retVal = fl.insertScript(zipArray);// Jeciel
                        }
                        else
                        {
                            retVal = false;
                        }

                    }

                    if (retVal == false)
                    {
                        SetLogError("ERROR: Hubo un error al cargar o enviar el archivo");
                    }

                    fin = System.DateTime.Now;
                    System.TimeSpan duration = (fin - inicio);
                    this.SetText(Environment.NewLine + "Archivo " + v.ToString() + Environment.NewLine + "\tDuración ::: " + string.Format("{0:mm\\:ss} segundos", duration) + " :::");

                    Double procentaje = (((++progreso / total))) * 100;
                    this.extraer.ReportProgress((Int16)procentaje);
                }
                //Se terminaron de enviar los archivos 
                SystemSounds.Hand.Play();
            }
        }

        //Jeciel
        void extraer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // The progress percentage is a property of e
            progressBarDgns.Value = e.ProgressPercentage;
        }

        //Jeciel
        private void extraerDgns_Terminado(object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnExtract.Enabled = true;
            this.groupBox_Transformacion.Enabled = true;
            this.groupBox_Capas.Enabled = true;
            this.groupBox_Archivos.Enabled = true;
        }

        //Jeciel
        private void setLog(IEnumerable<LogEntity> lista)
        {
            if (this.logGridView.InvokeRequired)
            {
                setLogCallback d = new setLogCallback(setLog);
                this.Invoke(d, new object[] { lista });
            }
            else
            {
                var bindingList = new BindingList<LogEntity>(this.listaLog.ToList<LogEntity>());
                var source = new BindingSource(bindingList, null);
                this.logGridView.DataSource = source;
            }

        }

        //Jeciel
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.tBoxLog.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.tBoxLog.AppendText(text);
            }
        }

        //Jeciel
        private void SetLogError(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.tBoxLog.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.tBoxLog.AppendText(Environment.NewLine + text);
                SystemSounds.Exclamation.Play();
            }
        }

        //Jeciel
        private void changeLog()
        {
            /*logController = new LogController();
            IEnumerable<LogEntity> listaLog;
            string resultado;

            while(!logWorker.CancellationPending)
            {
                // resultado = logController.getLog();
                SetText(logController.getLog());
                Thread.Sleep(5000);
            }*/
        }//No usado

        //Jeciel
        private void logThread()//No usado
        {
            /* logWorker = new BackgroundWorker();
             logWorker.DoWork += (o, e) => changeLog();
             logWorker.RunWorkerAsync();*/
        }

        #endregion

        #region Makes

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
                case "Estado":
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
                case "Codigo Postal":
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
                case "Centro Historico":
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
                case "Zonas Economicas":
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
                case "Municipio":
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
                case "Seccion":
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
                case "Localidad":
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
                case "Índice Cartográfico Clave":
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
                //Jeciel
                case "Estado Clave":
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
                case "Codigo Postal Clave":
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
                case "Centro Historico Clave":
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
                case "Zonas Economicas Clave":
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
                case "Municipio Clave":
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
                case "Seccion Clave":
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
                case "Localidad Clave":
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
            catch(Exception e)
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
                List<string> listSQLs = controllerSQL.makeInsertLine(feature, ref linesWkt, 32613);
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
        #endregion
    }
}
