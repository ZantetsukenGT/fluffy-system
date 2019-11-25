namespace _OLC1_Proyecto2_201602988
{
    partial class Cliente
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.bMenuArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.bMenuNuevo = new System.Windows.Forms.ToolStripMenuItem();
            this.bMenuAbrir = new System.Windows.Forms.ToolStripMenuItem();
            this.bMenuGuardar = new System.Windows.Forms.ToolStripMenuItem();
            this.bMenuGuardarComo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.bMenuLimpiarConsola = new System.Windows.Forms.ToolStripMenuItem();
            this.bMenuLimpiarVariables = new System.Windows.Forms.ToolStripMenuItem();
            this.bMenuEliminarPestanya = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.bMenuSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.bMenuAnalisis = new System.Windows.Forms.ToolStripMenuItem();
            this.bMenuCompilar = new System.Windows.Forms.ToolStripMenuItem();
            this.bMenuAyuda = new System.Windows.Forms.ToolStripMenuItem();
            this.bMenuManualUsuario = new System.Windows.Forms.ToolStripMenuItem();
            this.bMenuManualTecnico = new System.Windows.Forms.ToolStripMenuItem();
            this.bMenuAcercaDe = new System.Windows.Forms.ToolStripMenuItem();
            this.tabsEntradas = new System.Windows.Forms.TabControl();
            this.tabsSalidas = new System.Windows.Forms.TabControl();
            this.tabConsola = new System.Windows.Forms.TabPage();
            this.textBoxConsola = new System.Windows.Forms.TextBox();
            this.tabErrores = new System.Windows.Forms.TabPage();
            this.tablaErrores = new System.Windows.Forms.DataGridView();
            this.columnaTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnaValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnaLinea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnaColumna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lLinea = new System.Windows.Forms.Label();
            this.lColumna = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.tabsSalidas.SuspendLayout();
            this.tabConsola.SuspendLayout();
            this.tabErrores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaErrores)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bMenuArchivo,
            this.bMenuAnalisis,
            this.bMenuAyuda});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1030, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // bMenuArchivo
            // 
            this.bMenuArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bMenuNuevo,
            this.bMenuAbrir,
            this.bMenuGuardar,
            this.bMenuGuardarComo,
            this.toolStripMenuItem1,
            this.bMenuLimpiarConsola,
            this.bMenuLimpiarVariables,
            this.bMenuEliminarPestanya,
            this.toolStripMenuItem2,
            this.bMenuSalir});
            this.bMenuArchivo.Name = "bMenuArchivo";
            this.bMenuArchivo.Size = new System.Drawing.Size(71, 24);
            this.bMenuArchivo.Text = "Archivo";
            // 
            // bMenuNuevo
            // 
            this.bMenuNuevo.Name = "bMenuNuevo";
            this.bMenuNuevo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.bMenuNuevo.Size = new System.Drawing.Size(321, 26);
            this.bMenuNuevo.Text = "Nuevo";
            this.bMenuNuevo.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // bMenuAbrir
            // 
            this.bMenuAbrir.Name = "bMenuAbrir";
            this.bMenuAbrir.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.bMenuAbrir.Size = new System.Drawing.Size(321, 26);
            this.bMenuAbrir.Text = "Abrir";
            this.bMenuAbrir.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // bMenuGuardar
            // 
            this.bMenuGuardar.Name = "bMenuGuardar";
            this.bMenuGuardar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.bMenuGuardar.Size = new System.Drawing.Size(321, 26);
            this.bMenuGuardar.Text = "Guardar";
            this.bMenuGuardar.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click);
            // 
            // bMenuGuardarComo
            // 
            this.bMenuGuardarComo.Name = "bMenuGuardarComo";
            this.bMenuGuardarComo.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.bMenuGuardarComo.Size = new System.Drawing.Size(321, 26);
            this.bMenuGuardarComo.Text = "Guardar como...";
            this.bMenuGuardarComo.Click += new System.EventHandler(this.guardarComoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(318, 6);
            // 
            // bMenuLimpiarConsola
            // 
            this.bMenuLimpiarConsola.Name = "bMenuLimpiarConsola";
            this.bMenuLimpiarConsola.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.bMenuLimpiarConsola.Size = new System.Drawing.Size(321, 26);
            this.bMenuLimpiarConsola.Text = "Limpiar consola";
            this.bMenuLimpiarConsola.Click += new System.EventHandler(this.limpiarConsolaToolStripMenuItem_Click);
            // 
            // bMenuLimpiarVariables
            // 
            this.bMenuLimpiarVariables.Name = "bMenuLimpiarVariables";
            this.bMenuLimpiarVariables.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
            this.bMenuLimpiarVariables.Size = new System.Drawing.Size(321, 26);
            this.bMenuLimpiarVariables.Text = "Limpiar errores";
            this.bMenuLimpiarVariables.Click += new System.EventHandler(this.limpiarVariablesToolStripMenuItem_Click);
            // 
            // bMenuEliminarPestanya
            // 
            this.bMenuEliminarPestanya.Name = "bMenuEliminarPestanya";
            this.bMenuEliminarPestanya.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Delete)));
            this.bMenuEliminarPestanya.Size = new System.Drawing.Size(321, 26);
            this.bMenuEliminarPestanya.Text = "Eliminar pestaña";
            this.bMenuEliminarPestanya.Click += new System.EventHandler(this.eliminarPestañaToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(318, 6);
            // 
            // bMenuSalir
            // 
            this.bMenuSalir.Name = "bMenuSalir";
            this.bMenuSalir.Size = new System.Drawing.Size(321, 26);
            this.bMenuSalir.Text = "Salir";
            this.bMenuSalir.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // bMenuAnalisis
            // 
            this.bMenuAnalisis.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bMenuCompilar});
            this.bMenuAnalisis.Name = "bMenuAnalisis";
            this.bMenuAnalisis.Size = new System.Drawing.Size(71, 24);
            this.bMenuAnalisis.Text = "Análisis";
            // 
            // bMenuCompilar
            // 
            this.bMenuCompilar.Name = "bMenuCompilar";
            this.bMenuCompilar.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.bMenuCompilar.Size = new System.Drawing.Size(251, 26);
            this.bMenuCompilar.Text = "Compilar";
            this.bMenuCompilar.Click += new System.EventHandler(this.compilarToolStripMenuItem_Click);
            // 
            // bMenuAyuda
            // 
            this.bMenuAyuda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bMenuManualUsuario,
            this.bMenuManualTecnico,
            this.bMenuAcercaDe});
            this.bMenuAyuda.Name = "bMenuAyuda";
            this.bMenuAyuda.Size = new System.Drawing.Size(63, 24);
            this.bMenuAyuda.Text = "Ayuda";
            // 
            // bMenuManualUsuario
            // 
            this.bMenuManualUsuario.Name = "bMenuManualUsuario";
            this.bMenuManualUsuario.Size = new System.Drawing.Size(206, 26);
            this.bMenuManualUsuario.Text = "Manual de usuario";
            this.bMenuManualUsuario.Click += new System.EventHandler(this.manualDeUsuarioToolStripMenuItem_Click);
            // 
            // bMenuManualTecnico
            // 
            this.bMenuManualTecnico.Name = "bMenuManualTecnico";
            this.bMenuManualTecnico.Size = new System.Drawing.Size(206, 26);
            this.bMenuManualTecnico.Text = "Manual técnico";
            this.bMenuManualTecnico.Click += new System.EventHandler(this.manualTécnicoToolStripMenuItem_Click);
            // 
            // bMenuAcercaDe
            // 
            this.bMenuAcercaDe.Name = "bMenuAcercaDe";
            this.bMenuAcercaDe.Size = new System.Drawing.Size(206, 26);
            this.bMenuAcercaDe.Text = "Acerca de...";
            this.bMenuAcercaDe.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // tabsEntradas
            // 
            this.tabsEntradas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabsEntradas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabsEntradas.Location = new System.Drawing.Point(3, 3);
            this.tabsEntradas.Name = "tabsEntradas";
            this.tabsEntradas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabsEntradas.SelectedIndex = 0;
            this.tabsEntradas.Size = new System.Drawing.Size(999, 237);
            this.tabsEntradas.TabIndex = 1;
            // 
            // tabsSalidas
            // 
            this.tabsSalidas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabsSalidas.Controls.Add(this.tabConsola);
            this.tabsSalidas.Controls.Add(this.tabErrores);
            this.tabsSalidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabsSalidas.Location = new System.Drawing.Point(3, 246);
            this.tabsSalidas.Name = "tabsSalidas";
            this.tabsSalidas.SelectedIndex = 0;
            this.tabsSalidas.Size = new System.Drawing.Size(999, 237);
            this.tabsSalidas.TabIndex = 2;
            // 
            // tabConsola
            // 
            this.tabConsola.Controls.Add(this.textBoxConsola);
            this.tabConsola.Location = new System.Drawing.Point(4, 27);
            this.tabConsola.Name = "tabConsola";
            this.tabConsola.Padding = new System.Windows.Forms.Padding(3);
            this.tabConsola.Size = new System.Drawing.Size(991, 206);
            this.tabConsola.TabIndex = 0;
            this.tabConsola.Text = "Consola";
            this.tabConsola.UseVisualStyleBackColor = true;
            // 
            // textBoxConsola
            // 
            this.textBoxConsola.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxConsola.Font = new System.Drawing.Font("Lucida Console", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxConsola.Location = new System.Drawing.Point(3, 3);
            this.textBoxConsola.Multiline = true;
            this.textBoxConsola.Name = "textBoxConsola";
            this.textBoxConsola.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxConsola.Size = new System.Drawing.Size(985, 200);
            this.textBoxConsola.TabIndex = 0;
            this.textBoxConsola.Click += new System.EventHandler(this.anyTextBox_LineCalculator);
            this.textBoxConsola.TextChanged += new System.EventHandler(this.anyTextBox_LineCalculator);
            // 
            // tabErrores
            // 
            this.tabErrores.Controls.Add(this.tablaErrores);
            this.tabErrores.Location = new System.Drawing.Point(4, 27);
            this.tabErrores.Name = "tabErrores";
            this.tabErrores.Padding = new System.Windows.Forms.Padding(3);
            this.tabErrores.Size = new System.Drawing.Size(991, 206);
            this.tabErrores.TabIndex = 1;
            this.tabErrores.Text = "Errores";
            this.tabErrores.UseVisualStyleBackColor = true;
            // 
            // tablaErrores
            // 
            this.tablaErrores.AllowUserToAddRows = false;
            this.tablaErrores.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tablaErrores.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tablaErrores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.tablaErrores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaErrores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnaTipo,
            this.columnaValor,
            this.columnaLinea,
            this.columnaColumna});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tablaErrores.DefaultCellStyle = dataGridViewCellStyle3;
            this.tablaErrores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaErrores.Location = new System.Drawing.Point(3, 3);
            this.tablaErrores.Name = "tablaErrores";
            this.tablaErrores.ReadOnly = true;
            this.tablaErrores.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tablaErrores.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tablaErrores.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.tablaErrores.RowTemplate.Height = 24;
            this.tablaErrores.Size = new System.Drawing.Size(985, 200);
            this.tablaErrores.TabIndex = 0;
            // 
            // columnaTipo
            // 
            this.columnaTipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnaTipo.FillWeight = 15F;
            this.columnaTipo.HeaderText = "Tipo";
            this.columnaTipo.Name = "columnaTipo";
            this.columnaTipo.ReadOnly = true;
            this.columnaTipo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnaValor
            // 
            this.columnaValor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnaValor.FillWeight = 70F;
            this.columnaValor.HeaderText = "Descripcion";
            this.columnaValor.Name = "columnaValor";
            this.columnaValor.ReadOnly = true;
            this.columnaValor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnaLinea
            // 
            this.columnaLinea.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnaLinea.FillWeight = 5F;
            this.columnaLinea.HeaderText = "Linea";
            this.columnaLinea.Name = "columnaLinea";
            this.columnaLinea.ReadOnly = true;
            this.columnaLinea.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnaColumna
            // 
            this.columnaColumna.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnaColumna.FillWeight = 8F;
            this.columnaColumna.HeaderText = "Columna";
            this.columnaColumna.Name = "columnaColumna";
            this.columnaColumna.ReadOnly = true;
            this.columnaColumna.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lLinea
            // 
            this.lLinea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lLinea.AutoSize = true;
            this.lLinea.Location = new System.Drawing.Point(702, 9);
            this.lLinea.Name = "lLinea";
            this.lLinea.Size = new System.Drawing.Size(51, 17);
            this.lLinea.TabIndex = 1;
            this.lLinea.Text = "Linea: ";
            // 
            // lColumna
            // 
            this.lColumna.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lColumna.AutoSize = true;
            this.lColumna.Location = new System.Drawing.Point(798, 9);
            this.lColumna.Name = "lColumna";
            this.lColumna.Size = new System.Drawing.Size(71, 17);
            this.lColumna.TabIndex = 3;
            this.lColumna.Text = "Columna: ";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tabsEntradas, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabsSalidas, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 32);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1005, 486);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // Cliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 530);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lColumna);
            this.Controls.Add(this.lLinea);
            this.Controls.Add(this.menuStrip1);
            this.MinimumSize = new System.Drawing.Size(1048, 577);
            this.Name = "Cliente";
            this.Text = "Proyecto2, 201602988";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabsSalidas.ResumeLayout(false);
            this.tabConsola.ResumeLayout(false);
            this.tabConsola.PerformLayout();
            this.tabErrores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablaErrores)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem bMenuArchivo;
        private System.Windows.Forms.ToolStripMenuItem bMenuNuevo;
        private System.Windows.Forms.ToolStripMenuItem bMenuAbrir;
        private System.Windows.Forms.ToolStripMenuItem bMenuGuardar;
        private System.Windows.Forms.ToolStripMenuItem bMenuAnalisis;
        private System.Windows.Forms.ToolStripMenuItem bMenuAyuda;
        private System.Windows.Forms.ToolStripMenuItem bMenuGuardarComo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bMenuLimpiarConsola;
        private System.Windows.Forms.ToolStripMenuItem bMenuLimpiarVariables;
        private System.Windows.Forms.ToolStripMenuItem bMenuEliminarPestanya;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem bMenuSalir;
        private System.Windows.Forms.ToolStripMenuItem bMenuCompilar;
        private System.Windows.Forms.ToolStripMenuItem bMenuManualUsuario;
        private System.Windows.Forms.ToolStripMenuItem bMenuManualTecnico;
        private System.Windows.Forms.ToolStripMenuItem bMenuAcercaDe;
        private System.Windows.Forms.TabControl tabsSalidas;
        private System.Windows.Forms.TabPage tabConsola;
        private System.Windows.Forms.TabPage tabErrores;
        private System.Windows.Forms.TextBox textBoxConsola;
        private System.Windows.Forms.DataGridView tablaErrores;
        private System.Windows.Forms.Label lLinea;
        private System.Windows.Forms.Label lColumna;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnaTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnaValor;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnaLinea;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnaColumna;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.TabControl tabsEntradas;
    }
}

