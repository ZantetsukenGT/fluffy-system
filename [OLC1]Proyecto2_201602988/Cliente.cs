using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using _OLC1_Proyecto2_201602988.lang.engine;
using _OLC1_Proyecto2_201602988.lang.definition;
using _OLC1_Proyecto2_201602988.app;
using _OLC1_Proyecto2_201602988.app.tree;
using Irony.Parsing;

namespace _OLC1_Proyecto2_201602988
{
    public partial class Cliente : Form
    {
        public Cliente()
        {
            InitializeComponent();
            Text = "ZantetIDE v3.1 (No Multiproject)";
        }

        private void compilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabsEntradas.TabCount > 0)
            {
                String entrada = obtenerEntrada().Replace("”", "\"").Replace("“", "\"").Replace("\r", "").Replace("\f", "").Trim();
                if (entrada.Length > 0)
                {
                    Importar.Reset();
                    Gramatica gramatica = new Gramatica();
                    LanguageData lenguaje = new LanguageData(gramatica);
                    Parser parser = new Parser(lenguaje);
                    ParseTree arbol = parser.Parse(entrada);
                    ParseTreeNode AST = arbol.Root;
                    
                    List<Instruccion> myAST = new List<Instruccion>();
                    if (AST != null)
                    {
                        myAST = new GeneradorAST().Analizar(AST);
                    }
                    bool debug = true;
                    if(!debug)
                    {
                        if(AST != null)
                        {
                            Graficador g = new Graficador();
                            string s = g.GenerarGrafo(AST);

                            StreamWriter archivo = new StreamWriter("debug.dot", false);
                            archivo.Write(s);
                            archivo.Close();

                            Process p = new Process();
                            p.StartInfo.FileName = @"C:\Program Files (x86)\Graphviz2.38\bin\dot.exe";
                            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            p.StartInfo.Arguments = "-Tpng debug.dot -o debug.png";
                            p.EnableRaisingEvents = true;
                            p.Exited += (sender1, e1) =>
                            {
                                Thread.Sleep(100);
                                Process.Start("debug.png");
                            };
                            p.Start();
                        }
                    }

                    Stuff stuff = new Stuff(textBoxConsola);
                    foreach (Irony.LogMessage error in parser.Context.CurrentParseTree.ParserMessages)
                    {
                        stuff.erroresLexicosSintacticos.Add(new Irony.LogMessage(error.Level, error.Location, ((((MyTab)tabsEntradas.SelectedTab).file != null) ? Path.GetFileName(((MyTab)tabsEntradas.SelectedTab).file) + ": " : "No File: ") + error.Message, error.ParserState));
                    }
                    ejecutar(myAST, stuff);
                }
                else
                {
                    MessageBox.Show("Entrada vacía", "Advertencia");
                }
            }
            else
            {
                MessageBox.Show("No hay ni una tab para compilar", "Advertencia");
            }
        }

        #region Funciones del IDE
        private String obtenerEntrada()
        {
            return ((MyTab)tabsEntradas.SelectedTab).getTexto();
        }

        private void limpiarConsolaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxConsola.ResetText();
        }

        private void limpiarVariablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tablaErrores.Rows.Clear();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyTab nuevo = new MyTab();
            nuevo.Controls[0].Click += new EventHandler(anyTextBox_LineCalculator);
            nuevo.Controls[0].TextChanged += new EventHandler(anyTextBox_LineCalculator);
            tabsEntradas.TabPages.Add(nuevo);

            tabsEntradas.SelectedIndex = tabsEntradas.TabCount - 1;
            lLinea.Text = "Linea: 0";
            lColumna.Text = "Columna: 0";
        }

        private void eliminarPestañaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyTab selectedTab = (MyTab)tabsEntradas.SelectedTab;
            if(selectedTab != null)
            {
                if (!(selectedTab.textoOriginal.Equals(selectedTab.getTexto())))
                {
                    DialogResult reply = MessageBox.Show("Desea guardar los cambios?\r\nSe perderán si no los guarda.", "Advertencia", MessageBoxButtons.YesNoCancel);
                    if (reply == DialogResult.Yes)
                    {
                        guardarToolStripMenuItem_Click(sender, e);
                        if (selectedTab.file != null)
                        {
                            tabsEntradas.TabPages.RemoveAt(tabsEntradas.SelectedIndex);
                            if(tabsEntradas.TabCount == 0)
                            {
                                lLinea.Text = "Linea: ";
                                lColumna.Text = "Columna: ";
                            }
                        }
                    }
                    else if (reply == DialogResult.No)
                    {
                        tabsEntradas.TabPages.RemoveAt(tabsEntradas.SelectedIndex);
                        if (tabsEntradas.TabCount == 0)
                        {
                            lLinea.Text = "Linea: ";
                            lColumna.Text = "Columna: ";
                        }
                    }
                }
                else
                {
                    tabsEntradas.TabPages.RemoveAt(tabsEntradas.SelectedIndex);
                    if (tabsEntradas.TabCount == 0)
                    {
                        lLinea.Text = "Linea: ";
                        lColumna.Text = "Columna: ";
                    }
                }
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Abrir...",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "pyusac",
                Filter = "PyUsac Language Files (*.pyusac)|*.pyusac"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                nuevoToolStripMenuItem_Click(sender, e);
                MyTab tab = (MyTab) tabsEntradas.SelectedTab;
                tab.Text = Path.GetFileName(openFileDialog.FileName);
                tab.file = Path.GetFullPath(openFileDialog.FileName);

                StringBuilder sb = new StringBuilder();

                foreach (string line in File.ReadLines(tab.file))
                {
                    sb.AppendLine(line);
                }

                String text = sb.ToString();
                tab.textoOriginal = text;
                tab.Controls[0].Text = text;
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyTab tab = (MyTab)tabsEntradas.SelectedTab;
            if (tab.file == null)
            {
                guardarComoToolStripMenuItem_Click(sender, e);
            }
            else
            {
                String text = tab.getTexto();

                StreamWriter archivo = new StreamWriter(tab.file,false);
                archivo.Write(text);
                archivo.Close();
                tab.textoOriginal = text;
                tab.Text = Path.GetFileName(tab.file);
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Guardar como...",

                DefaultExt = "pyusac",
                Filter = "PyUsac Language Files (*.pyusac)|*.pyusac"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                MyTab tab = (MyTab) tabsEntradas.SelectedTab;
                tab.file = Path.GetFullPath(saveFileDialog.FileName.Replace(".pyusac", "") + ".pyusac");
                guardarToolStripMenuItem_Click(sender, e);
            }
        }

        private void manualDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(File.Exists(Path.GetFullPath("ManualUsuario.pdf")))
            {
                Process.Start("ManualUsuario.pdf");
            }
            else
            {
                MessageBox.Show("No hay manual de usuario", "Advertencia");
            }
        }

        private void manualTécnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path.GetFullPath("ManualTecnico.pdf")))
            {
                Process.Start("ManualTecnico.pdf");
            }
            else
            {
                MessageBox.Show("No hay manual técnico", "Advertencia");
            }
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult reply = MessageBox.Show("Dev: Ozmar René Escobar Avila\r\nCarnet: 201602988", "Acerca de...");
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = tabsEntradas.SelectedIndex;
            foreach (MyTab tab in tabsEntradas.TabPages)
            {
                tabsEntradas.SelectedTab = tab;
                if (!(tab.textoOriginal.Equals(tab.getTexto())))
                {
                    DialogResult reply = MessageBox.Show("Desea guardar los cambios?\r\nSe perderán si no los guarda.", "Advertencia", MessageBoxButtons.YesNoCancel);
                    if (reply == DialogResult.Yes)
                    {
                        guardarToolStripMenuItem_Click(sender, e);
                        if (tab.file == null)
                        {
                            tabsEntradas.SelectedIndex = selectedIndex;
                            return;
                        }
                    }
                    else if (reply == DialogResult.Cancel)
                    {
                        tabsEntradas.SelectedIndex = selectedIndex;
                        return;
                    }
                }
            }
            System.Windows.Forms.Application.Exit();
        }
        private void anyTextBox_LineCalculator(object sender, EventArgs e)
        {
            int line = ((TextBox)sender).GetLineFromCharIndex(((TextBox)sender).SelectionStart);
            int column = ((TextBox)sender).SelectionStart - ((TextBox)sender).GetFirstCharIndexFromLine(line);
            lLinea.Text = "Linea: " + (line + 1);
            lColumna.Text = "Columna: " + (column + 1);
        }
        #endregion

        #region Ejecutar
        private void ejecutar(List<Instruccion> myAST, Stuff stuff)
        {
            stuff.consola.ResetText();
            tablaErrores.Rows.Clear();
            if (stuff.erroresLexicosSintacticos.Count > 0)
            {
                crearReporteErrores(stuff);
                MessageBox.Show("Hay errores léxicos o sintácticos.", "Advertencia");
                return;
            }
            else
            {
                Contexto global = new Contexto();
                global.currentFile = ((MyTab)tabsEntradas.SelectedTab).file;
                if(global.currentFile != null)
                {
                    Importar.instanciasArchivos.Add(global);
                }
                Main m = null;
                foreach (Instruccion i in myAST)
                {
                    if (i is Importar)
                    {
                        i.ejecutar(global, stuff);
                    }
                }
                foreach (Instruccion i in myAST)
                {
                    if (i is DeclaracionMetodo || i is DeclaracionClase)
                    {
                        i.ejecutar(global, stuff);
                    }
                }
                foreach (Instruccion i in myAST)
                {
                    if (i is DeclaracionMetodo || i is DeclaracionClase || i is Main || i is Importar)
                    {
                    }
                    else
                    {
                        i.ejecutar(global, stuff);
                    }
                }
                foreach (Instruccion i in myAST)
                {
                    if (i is Main)
                    {
                        if (m == null)
                        {
                            m = (Main)i;
                        }
                        else
                        {
                            stuff.error("Semántico", "'MAIN' está declarado más de una vez, se ejecutará el primero encontrado.", i.getLinea(), i.getColumna(), global);
                        }
                    }
                }
                if (m != null)
                {
                    m.ejecutar(global, stuff);
                }
                
                if (stuff.erroresSemanticos.Count == 0)
                {
                    MessageBox.Show("Análisis completados correctamente.", "Mensaje");
                }
                else
                {
                    MessageBox.Show("Hay errores semánticos.", "Advertencia");
                }
                crearReporteErrores(stuff);
            }
        }

        #endregion

        #region Errores
        private void crearReporteErrores(Stuff stuff)
        {
            foreach (var log in stuff.erroresLexicosSintacticos)
            {
                string tipo = (log.Message.Contains("Invalid character:")) ? "Léxico" : "Sintáctico";
                tablaErrores.Rows.Add(tipo, log.Message, log.Location.Line + 1, log.Location.Column);
            }
            foreach (ErrorAnalisis e in stuff.erroresSemanticos)
            {
                tablaErrores.Rows.Add(e.tipo, e.descripcion, e.linea + 1, e.columna);
            }
        }
        #endregion

    }
}
