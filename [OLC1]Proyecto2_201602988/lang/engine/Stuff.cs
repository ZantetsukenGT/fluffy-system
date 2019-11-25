using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Irony.Parsing;
using _OLC1_Proyecto2_201602988.lang.definition;
using System.IO;

namespace _OLC1_Proyecto2_201602988.lang.engine
{
    public class Stuff
    {
        public TextBox consola;
        public List<ErrorAnalisis> erroresSemanticos;
        public Irony.LogMessageList erroresLexicosSintacticos;

        public Stuff(TextBox consola)
        {
            this.consola = consola;
            erroresSemanticos = new List<ErrorAnalisis>();
            erroresLexicosSintacticos = new Irony.LogMessageList();
        }

        public void escribir(String text)
        {
            consola.Text += text + "\r\n";
        }

        public void error(String tipo, String error, int fila, int columna, Contexto ctx)
        {
            erroresSemanticos.Add(new ErrorAnalisis(tipo, ((ctx.currentFile != null)?Path.GetFileName(ctx.currentFile) + ": ":"No File: ") + error, fila, columna));
        }
    }
}
