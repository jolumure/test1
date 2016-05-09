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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lOrigenEPSG = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lDestinoEPSG = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cBox_resultados = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tB_opcion = new System.Windows.Forms.TextBox();
            this.cB_opcion = new System.Windows.Forms.ComboBox();
            this.chkBoxTransformGeo = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkListLevels = new System.Windows.Forms.CheckedListBox();
            this.chkBoxSQLOutput = new System.Windows.Forms.CheckBox();
            this.btnExtract = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnUncheckAll = new System.Windows.Forms.Button();
            this.tBoxLog = new System.Windows.Forms.TextBox();
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.btnCleanSelection = new System.Windows.Forms.Button();
            this.lBoxFiles = new System.Windows.Forms.CheckedListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(9, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(575, 458);
            this.tabControl1.TabIndex = 45;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.chkBoxTransformGeo);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.chkListLevels);
            this.tabPage1.Controls.Add(this.chkBoxSQLOutput);
            this.tabPage1.Controls.Add(this.btnExtract);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnClose);
            this.tabPage1.Controls.Add(this.btnUncheckAll);
            this.tabPage1.Controls.Add(this.tBoxLog);
            this.tabPage1.Controls.Add(this.btnCheckAll);
            this.tabPage1.Controls.Add(this.btnAddFiles);
            this.tabPage1.Controls.Add(this.btnCleanSelection);
            this.tabPage1.Controls.Add(this.lBoxFiles);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(567, 432);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "DGN\'s Catastro";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 17);
            this.label4.TabIndex = 45;
            this.label4.Text = "Operación sobre multiples archivos";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lOrigenEPSG);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.lDestinoEPSG);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cBox_resultados);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tB_opcion);
            this.groupBox1.Controls.Add(this.cB_opcion);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 155);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transformación de sistema de referencia";
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
            this.btnBuscar.Location = new System.Drawing.Point(99, 78);
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
            this.cBox_resultados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBox_resultados.FormattingEnabled = true;
            this.cBox_resultados.Location = new System.Drawing.Point(3, 107);
            this.cBox_resultados.MaxDropDownItems = 50;
            this.cBox_resultados.MaxLength = 100;
            this.cBox_resultados.Name = "cBox_resultados";
            this.cBox_resultados.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cBox_resultados.Size = new System.Drawing.Size(183, 21);
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
            // tB_opcion
            // 
            this.tB_opcion.Location = new System.Drawing.Point(3, 81);
            this.tB_opcion.MaxLength = 20;
            this.tB_opcion.Name = "tB_opcion";
            this.tB_opcion.Size = new System.Drawing.Size(90, 20);
            this.tB_opcion.TabIndex = 1;
            // 
            // cB_opcion
            // 
            this.cB_opcion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_opcion.FormattingEnabled = true;
            this.cB_opcion.Items.AddRange(new object[] {
            "SRID",
            "UTM Zone",
            "Ciudad"});
            this.cB_opcion.Location = new System.Drawing.Point(3, 53);
            this.cB_opcion.Name = "cB_opcion";
            this.cB_opcion.Size = new System.Drawing.Size(90, 21);
            this.cB_opcion.TabIndex = 0;
            // 
            // chkBoxTransformGeo
            // 
            this.chkBoxTransformGeo.AutoSize = true;
            this.chkBoxTransformGeo.Checked = true;
            this.chkBoxTransformGeo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxTransformGeo.Location = new System.Drawing.Point(407, 356);
            this.chkBoxTransformGeo.Name = "chkBoxTransformGeo";
            this.chkBoxTransformGeo.Size = new System.Drawing.Size(141, 30);
            this.chkBoxTransformGeo.TabIndex = 43;
            this.chkBoxTransformGeo.Text = "Realizar transformación \r\nde sistema de referencia";
            this.chkBoxTransformGeo.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(289, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Capas de información :";
            // 
            // chkListLevels
            // 
            this.chkListLevels.CheckOnClick = true;
            this.chkListLevels.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkListLevels.FormattingEnabled = true;
            this.chkListLevels.Location = new System.Drawing.Point(218, 45);
            this.chkListLevels.Name = "chkListLevels";
            this.chkListLevels.ScrollAlwaysVisible = true;
            this.chkListLevels.Size = new System.Drawing.Size(240, 208);
            this.chkListLevels.TabIndex = 23;
            // 
            // chkBoxSQLOutput
            // 
            this.chkBoxSQLOutput.AutoSize = true;
            this.chkBoxSQLOutput.Checked = true;
            this.chkBoxSQLOutput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxSQLOutput.Enabled = false;
            this.chkBoxSQLOutput.Location = new System.Drawing.Point(321, 363);
            this.chkBoxSQLOutput.Name = "chkBoxSQLOutput";
            this.chkBoxSQLOutput.Size = new System.Drawing.Size(71, 17);
            this.chkBoxSQLOutput.TabIndex = 41;
            this.chkBoxSQLOutput.Text = "SQL Files";
            this.chkBoxSQLOutput.UseVisualStyleBackColor = true;
            // 
            // btnExtract
            // 
            this.btnExtract.Location = new System.Drawing.Point(451, 396);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(95, 23);
            this.btnExtract.TabIndex = 24;
            this.btnExtract.Text = "Actualizar";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(225, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Notificaciones:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(338, 396);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 23);
            this.btnClose.TabIndex = 25;
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnUncheckAll
            // 
            this.btnUncheckAll.Location = new System.Drawing.Point(243, 22);
            this.btnUncheckAll.Name = "btnUncheckAll";
            this.btnUncheckAll.Size = new System.Drawing.Size(19, 19);
            this.btnUncheckAll.TabIndex = 39;
            this.btnUncheckAll.Text = "-";
            this.btnUncheckAll.UseVisualStyleBackColor = true;
            this.btnUncheckAll.Click += new System.EventHandler(this.btnUncheckAll_Click);
            // 
            // tBoxLog
            // 
            this.tBoxLog.Location = new System.Drawing.Point(218, 276);
            this.tBoxLog.Multiline = true;
            this.tBoxLog.Name = "tBoxLog";
            this.tBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tBoxLog.Size = new System.Drawing.Size(329, 72);
            this.tBoxLog.TabIndex = 26;
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Location = new System.Drawing.Point(218, 22);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(19, 19);
            this.btnCheckAll.TabIndex = 38;
            this.btnCheckAll.Text = "+";
            this.btnCheckAll.UseVisualStyleBackColor = true;
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // btnAddFiles
            // 
            this.btnAddFiles.Location = new System.Drawing.Point(144, 394);
            this.btnAddFiles.Name = "btnAddFiles";
            this.btnAddFiles.Size = new System.Drawing.Size(54, 25);
            this.btnAddFiles.TabIndex = 28;
            this.btnAddFiles.Text = "Agregar archivos";
            this.btnAddFiles.UseVisualStyleBackColor = true;
            this.btnAddFiles.Click += new System.EventHandler(this.btnAddFiles_Click);
            // 
            // btnCleanSelection
            // 
            this.btnCleanSelection.Location = new System.Drawing.Point(84, 394);
            this.btnCleanSelection.Name = "btnCleanSelection";
            this.btnCleanSelection.Size = new System.Drawing.Size(54, 25);
            this.btnCleanSelection.TabIndex = 31;
            this.btnCleanSelection.Text = "Limpiar";
            this.btnCleanSelection.UseVisualStyleBackColor = true;
            // 
            // lBoxFiles
            // 
            this.lBoxFiles.CheckOnClick = true;
            this.lBoxFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lBoxFiles.FormattingEnabled = true;
            this.lBoxFiles.Location = new System.Drawing.Point(6, 186);
            this.lBoxFiles.Name = "lBoxFiles";
            this.lBoxFiles.ScrollAlwaysVisible = true;
            this.lBoxFiles.Size = new System.Drawing.Size(192, 191);
            this.lBoxFiles.TabIndex = 30;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Extractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 470);
            this.Controls.Add(this.tabControl1);
            this.Name = "Extractor";
            this.Text = "Extractor";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox chkBoxTransformGeo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox chkListLevels;
        private System.Windows.Forms.CheckBox chkBoxSQLOutput;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnUncheckAll;
        private System.Windows.Forms.TextBox tBoxLog;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.Button btnAddFiles;
        private System.Windows.Forms.Button btnCleanSelection;
        private System.Windows.Forms.CheckedListBox lBoxFiles;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lOrigenEPSG;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lDestinoEPSG;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cBox_resultados;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tB_opcion;
        private System.Windows.Forms.ComboBox cB_opcion;
    }
}