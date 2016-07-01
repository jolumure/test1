namespace PresentacionWFA
{
    partial class Extractor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabLogg = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.progressBarDgns = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tBoxLog = new System.Windows.Forms.TextBox();
            this.groupBox_Archivos = new System.Windows.Forms.GroupBox();
            this.lBoxFiles = new System.Windows.Forms.CheckedListBox();
            this.btnCleanSelection = new System.Windows.Forms.Button();
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.groupBox_Capas = new System.Windows.Forms.GroupBox();
            this.chkListLevels = new System.Windows.Forms.CheckedListBox();
            this.btnUncheckAll = new System.Windows.Forms.Button();
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.groupBox_Transformacion = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cBox_numeros = new System.Windows.Forms.ComboBox();
            this.lOrigenEPSG = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lDestinoEPSG = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cBox_resultados = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tBox_opcion = new System.Windows.Forms.TextBox();
            this.cBox_opcion = new System.Windows.Forms.ComboBox();
            this.chkBoxTransformGeo = new System.Windows.Forms.CheckBox();
            this.chkBoxSQLOutput = new System.Windows.Forms.CheckBox();
            this.btnExtract = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.btnLimpiarLog = new System.Windows.Forms.Button();
            this.btnIniciarLog = new System.Windows.Forms.Button();
            this.btnDetenerLog = new System.Windows.Forms.Button();
            this.logGridView = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabLogg.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox_Archivos.SuspendLayout();
            this.groupBox_Capas.SuspendLayout();
            this.groupBox_Transformacion.SuspendLayout();
            this.tabLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabLogg
            // 
            this.tabLogg.Controls.Add(this.tabPage1);
            this.tabLogg.Controls.Add(this.tabLog);
            this.tabLogg.Location = new System.Drawing.Point(9, 6);
            this.tabLogg.Name = "tabLogg";
            this.tabLogg.SelectedIndex = 0;
            this.tabLogg.Size = new System.Drawing.Size(575, 504);
            this.tabLogg.TabIndex = 45;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.progressBarDgns);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox_Archivos);
            this.tabPage1.Controls.Add(this.groupBox_Capas);
            this.tabPage1.Controls.Add(this.groupBox_Transformacion);
            this.tabPage1.Controls.Add(this.chkBoxTransformGeo);
            this.tabPage1.Controls.Add(this.chkBoxSQLOutput);
            this.tabPage1.Controls.Add(this.btnExtract);
            this.tabPage1.Controls.Add(this.btnClose);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(567, 478);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "DGN\'s Catastro";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // progressBarDgns
            // 
            this.progressBarDgns.Location = new System.Drawing.Point(6, 403);
            this.progressBarDgns.Name = "progressBarDgns";
            this.progressBarDgns.Size = new System.Drawing.Size(270, 23);
            this.progressBarDgns.TabIndex = 49;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tBoxLog);
            this.groupBox2.Location = new System.Drawing.Point(282, 235);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(264, 154);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Notificaciones";
            // 
            // tBoxLog
            // 
            this.tBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tBoxLog.Location = new System.Drawing.Point(7, 12);
            this.tBoxLog.Multiline = true;
            this.tBoxLog.Name = "tBoxLog";
            this.tBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tBoxLog.Size = new System.Drawing.Size(251, 136);
            this.tBoxLog.TabIndex = 27;
            // 
            // groupBox_Archivos
            // 
            this.groupBox_Archivos.Controls.Add(this.lBoxFiles);
            this.groupBox_Archivos.Controls.Add(this.btnCleanSelection);
            this.groupBox_Archivos.Controls.Add(this.btnAddFiles);
            this.groupBox_Archivos.Location = new System.Drawing.Point(6, 170);
            this.groupBox_Archivos.Name = "groupBox_Archivos";
            this.groupBox_Archivos.Size = new System.Drawing.Size(270, 219);
            this.groupBox_Archivos.TabIndex = 47;
            this.groupBox_Archivos.TabStop = false;
            this.groupBox_Archivos.Text = "Operación sobre Múltiples Archivos";
            // 
            // lBoxFiles
            // 
            this.lBoxFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lBoxFiles.CheckOnClick = true;
            this.lBoxFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lBoxFiles.FormattingEnabled = true;
            this.lBoxFiles.Location = new System.Drawing.Point(9, 16);
            this.lBoxFiles.Name = "lBoxFiles";
            this.lBoxFiles.ScrollAlwaysVisible = true;
            this.lBoxFiles.Size = new System.Drawing.Size(255, 153);
            this.lBoxFiles.TabIndex = 31;
            // 
            // btnCleanSelection
            // 
            this.btnCleanSelection.Location = new System.Drawing.Point(128, 188);
            this.btnCleanSelection.Name = "btnCleanSelection";
            this.btnCleanSelection.Size = new System.Drawing.Size(54, 25);
            this.btnCleanSelection.TabIndex = 31;
            this.btnCleanSelection.Text = "Limpiar";
            this.btnCleanSelection.UseVisualStyleBackColor = true;
            this.btnCleanSelection.Click += new System.EventHandler(this.btnCleanSelection_Click);
            // 
            // btnAddFiles
            // 
            this.btnAddFiles.Location = new System.Drawing.Point(188, 188);
            this.btnAddFiles.Name = "btnAddFiles";
            this.btnAddFiles.Size = new System.Drawing.Size(54, 25);
            this.btnAddFiles.TabIndex = 28;
            this.btnAddFiles.Text = "Agregar archivos";
            this.btnAddFiles.UseVisualStyleBackColor = true;
            this.btnAddFiles.Click += new System.EventHandler(this.btnAddFiles_Click);
            // 
            // groupBox_Capas
            // 
            this.groupBox_Capas.Controls.Add(this.chkListLevels);
            this.groupBox_Capas.Controls.Add(this.btnUncheckAll);
            this.groupBox_Capas.Controls.Add(this.btnCheckAll);
            this.groupBox_Capas.Location = new System.Drawing.Point(282, 6);
            this.groupBox_Capas.Name = "groupBox_Capas";
            this.groupBox_Capas.Size = new System.Drawing.Size(264, 224);
            this.groupBox_Capas.TabIndex = 46;
            this.groupBox_Capas.TabStop = false;
            this.groupBox_Capas.Text = "Capas de Información";
            // 
            // chkListLevels
            // 
            this.chkListLevels.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkListLevels.CheckOnClick = true;
            this.chkListLevels.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkListLevels.FormattingEnabled = true;
            this.chkListLevels.Location = new System.Drawing.Point(7, 48);
            this.chkListLevels.Name = "chkListLevels";
            this.chkListLevels.ScrollAlwaysVisible = true;
            this.chkListLevels.Size = new System.Drawing.Size(251, 170);
            this.chkListLevels.TabIndex = 40;
            // 
            // btnUncheckAll
            // 
            this.btnUncheckAll.Location = new System.Drawing.Point(32, 21);
            this.btnUncheckAll.Name = "btnUncheckAll";
            this.btnUncheckAll.Size = new System.Drawing.Size(19, 19);
            this.btnUncheckAll.TabIndex = 42;
            this.btnUncheckAll.Text = "-";
            this.btnUncheckAll.UseVisualStyleBackColor = true;
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Location = new System.Drawing.Point(7, 21);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(19, 19);
            this.btnCheckAll.TabIndex = 41;
            this.btnCheckAll.Text = "+";
            this.btnCheckAll.UseVisualStyleBackColor = true;
            // 
            // groupBox_Transformacion
            // 
            this.groupBox_Transformacion.Controls.Add(this.label8);
            this.groupBox_Transformacion.Controls.Add(this.cBox_numeros);
            this.groupBox_Transformacion.Controls.Add(this.lOrigenEPSG);
            this.groupBox_Transformacion.Controls.Add(this.label7);
            this.groupBox_Transformacion.Controls.Add(this.btnBuscar);
            this.groupBox_Transformacion.Controls.Add(this.lDestinoEPSG);
            this.groupBox_Transformacion.Controls.Add(this.label5);
            this.groupBox_Transformacion.Controls.Add(this.cBox_resultados);
            this.groupBox_Transformacion.Controls.Add(this.label3);
            this.groupBox_Transformacion.Controls.Add(this.tBox_opcion);
            this.groupBox_Transformacion.Controls.Add(this.cBox_opcion);
            this.groupBox_Transformacion.Location = new System.Drawing.Point(6, 6);
            this.groupBox_Transformacion.Name = "groupBox_Transformacion";
            this.groupBox_Transformacion.Size = new System.Drawing.Size(270, 157);
            this.groupBox_Transformacion.TabIndex = 44;
            this.groupBox_Transformacion.TabStop = false;
            this.groupBox_Transformacion.Text = "Transformación de sistema de referencia";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(55, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "EPSG:";
            // 
            // cBox_numeros
            // 
            this.cBox_numeros.DropDownHeight = 70;
            this.cBox_numeros.FormattingEnabled = true;
            this.cBox_numeros.IntegralHeight = false;
            this.cBox_numeros.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60"});
            this.cBox_numeros.Location = new System.Drawing.Point(128, 54);
            this.cBox_numeros.MaxLength = 2;
            this.cBox_numeros.Name = "cBox_numeros";
            this.cBox_numeros.Size = new System.Drawing.Size(78, 21);
            this.cBox_numeros.TabIndex = 10;
            this.cBox_numeros.Visible = false;
            this.cBox_numeros.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cBox_numeros_KeyPress);
            // 
            // lOrigenEPSG
            // 
            this.lOrigenEPSG.AutoSize = true;
            this.lOrigenEPSG.Location = new System.Drawing.Point(100, 32);
            this.lOrigenEPSG.Name = "lOrigenEPSG";
            this.lOrigenEPSG.Size = new System.Drawing.Size(37, 13);
            this.lOrigenEPSG.TabIndex = 8;
            this.lOrigenEPSG.Text = "32613";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(55, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "EPSG:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(9, 80);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(87, 23);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lDestinoEPSG
            // 
            this.lDestinoEPSG.AutoSize = true;
            this.lDestinoEPSG.Location = new System.Drawing.Point(100, 133);
            this.lDestinoEPSG.Name = "lDestinoEPSG";
            this.lDestinoEPSG.Size = new System.Drawing.Size(37, 13);
            this.lDestinoEPSG.TabIndex = 5;
            this.lDestinoEPSG.Text = "32613";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Destino: ";
            // 
            // cBox_resultados
            // 
            this.cBox_resultados.DropDownHeight = 70;
            this.cBox_resultados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBox_resultados.FormattingEnabled = true;
            this.cBox_resultados.IntegralHeight = false;
            this.cBox_resultados.Location = new System.Drawing.Point(9, 107);
            this.cBox_resultados.MaxDropDownItems = 50;
            this.cBox_resultados.MaxLength = 100;
            this.cBox_resultados.Name = "cBox_resultados";
            this.cBox_resultados.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cBox_resultados.Size = new System.Drawing.Size(255, 21);
            this.cBox_resultados.Sorted = true;
            this.cBox_resultados.TabIndex = 3;
            this.cBox_resultados.SelectedIndexChanged += new System.EventHandler(this.cBox_resultados_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Origen";
            // 
            // tBox_opcion
            // 
            this.tBox_opcion.Location = new System.Drawing.Point(128, 54);
            this.tBox_opcion.MaxLength = 20;
            this.tBox_opcion.Name = "tBox_opcion";
            this.tBox_opcion.Size = new System.Drawing.Size(136, 20);
            this.tBox_opcion.TabIndex = 1;
            this.tBox_opcion.Visible = false;
            this.tBox_opcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tBox_opcion_KeyPress);
            // 
            // cBox_opcion
            // 
            this.cBox_opcion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBox_opcion.FormattingEnabled = true;
            this.cBox_opcion.Items.AddRange(new object[] {
            "Tipo de Búsqueda",
            "SRID",
            "UTM Zone",
            "Ciudad"});
            this.cBox_opcion.Location = new System.Drawing.Point(9, 53);
            this.cBox_opcion.Name = "cBox_opcion";
            this.cBox_opcion.Size = new System.Drawing.Size(113, 21);
            this.cBox_opcion.TabIndex = 0;
            this.cBox_opcion.SelectedIndexChanged += new System.EventHandler(this.cBox_opcion_SelectedIndexChanged);
            // 
            // chkBoxTransformGeo
            // 
            this.chkBoxTransformGeo.AutoSize = true;
            this.chkBoxTransformGeo.Checked = true;
            this.chkBoxTransformGeo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxTransformGeo.Location = new System.Drawing.Point(405, 395);
            this.chkBoxTransformGeo.Name = "chkBoxTransformGeo";
            this.chkBoxTransformGeo.Size = new System.Drawing.Size(141, 30);
            this.chkBoxTransformGeo.TabIndex = 43;
            this.chkBoxTransformGeo.Text = "Realizar transformación \r\nde sistema de referencia";
            this.chkBoxTransformGeo.UseVisualStyleBackColor = true;
            // 
            // chkBoxSQLOutput
            // 
            this.chkBoxSQLOutput.AutoSize = true;
            this.chkBoxSQLOutput.Checked = true;
            this.chkBoxSQLOutput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxSQLOutput.Enabled = false;
            this.chkBoxSQLOutput.Location = new System.Drawing.Point(329, 403);
            this.chkBoxSQLOutput.Name = "chkBoxSQLOutput";
            this.chkBoxSQLOutput.Size = new System.Drawing.Size(71, 17);
            this.chkBoxSQLOutput.TabIndex = 41;
            this.chkBoxSQLOutput.Text = "SQL Files";
            this.chkBoxSQLOutput.UseVisualStyleBackColor = true;
            // 
            // btnExtract
            // 
            this.btnExtract.Location = new System.Drawing.Point(451, 443);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(95, 23);
            this.btnExtract.TabIndex = 24;
            this.btnExtract.Text = "Actualizar";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(350, 443);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 23);
            this.btnClose.TabIndex = 25;
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.btnLimpiarLog);
            this.tabLog.Controls.Add(this.btnIniciarLog);
            this.tabLog.Controls.Add(this.btnDetenerLog);
            this.tabLog.Controls.Add(this.logGridView);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(567, 478);
            this.tabLog.TabIndex = 1;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // btnLimpiarLog
            // 
            this.btnLimpiarLog.Enabled = false;
            this.btnLimpiarLog.Location = new System.Drawing.Point(486, 449);
            this.btnLimpiarLog.Name = "btnLimpiarLog";
            this.btnLimpiarLog.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiarLog.TabIndex = 3;
            this.btnLimpiarLog.Text = "Limpiar";
            this.btnLimpiarLog.UseVisualStyleBackColor = true;
            // 
            // btnIniciarLog
            // 
            this.btnIniciarLog.Enabled = false;
            this.btnIniciarLog.Location = new System.Drawing.Point(324, 449);
            this.btnIniciarLog.Name = "btnIniciarLog";
            this.btnIniciarLog.Size = new System.Drawing.Size(75, 23);
            this.btnIniciarLog.TabIndex = 2;
            this.btnIniciarLog.Text = "Iniciar";
            this.btnIniciarLog.UseVisualStyleBackColor = true;
            this.btnIniciarLog.Click += new System.EventHandler(this.btnIniciarLog_Click);
            // 
            // btnDetenerLog
            // 
            this.btnDetenerLog.Enabled = false;
            this.btnDetenerLog.Location = new System.Drawing.Point(405, 449);
            this.btnDetenerLog.Name = "btnDetenerLog";
            this.btnDetenerLog.Size = new System.Drawing.Size(75, 23);
            this.btnDetenerLog.TabIndex = 1;
            this.btnDetenerLog.Text = "Dentener";
            this.btnDetenerLog.UseVisualStyleBackColor = true;
            this.btnDetenerLog.Click += new System.EventHandler(this.btnDetenerLog_Click);
            // 
            // logGridView
            // 
            this.logGridView.AllowUserToAddRows = false;
            this.logGridView.AllowUserToDeleteRows = false;
            this.logGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.logGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.logGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.logGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.logGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.logGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.logGridView.Location = new System.Drawing.Point(6, 6);
            this.logGridView.Name = "logGridView";
            this.logGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.logGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.logGridView.Size = new System.Drawing.Size(555, 437);
            this.logGridView.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Extractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 512);
            this.Controls.Add(this.tabLogg);
            this.Name = "Extractor";
            this.Text = "Extractor";
            this.Load += new System.EventHandler(this.Extractor_Load);
            this.tabLogg.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox_Archivos.ResumeLayout(false);
            this.groupBox_Capas.ResumeLayout(false);
            this.groupBox_Transformacion.ResumeLayout(false);
            this.groupBox_Transformacion.PerformLayout();
            this.tabLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabLogg;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox chkBoxTransformGeo;
        private System.Windows.Forms.CheckBox chkBoxSQLOutput;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAddFiles;
        private System.Windows.Forms.Button btnCleanSelection;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox_Transformacion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lOrigenEPSG;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lDestinoEPSG;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cBox_resultados;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tBox_opcion;
        private System.Windows.Forms.ComboBox cBox_opcion;
        private System.Windows.Forms.ComboBox cBox_numeros;
        private System.Windows.Forms.GroupBox groupBox_Capas;
        private System.Windows.Forms.GroupBox groupBox_Archivos;
        private System.Windows.Forms.CheckedListBox lBoxFiles;
        private System.Windows.Forms.CheckedListBox chkListLevels;
        private System.Windows.Forms.Button btnUncheckAll;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tBoxLog;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.DataGridView logGridView;
        private System.Windows.Forms.Button btnDetenerLog;
        private System.Windows.Forms.Button btnIniciarLog;
        private System.Windows.Forms.Button btnLimpiarLog;
        private System.Windows.Forms.ProgressBar progressBarDgns;
    }
}