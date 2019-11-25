using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;

namespace _OLC1_Proyecto2_201602988.app
{
    public class Graficador
    {
        private int contador;
        private String texto;

        public Graficador()
        {
            this.contador = 0;
            this.texto = "";
        }

        public String GenerarGrafo(ParseTreeNode raiz)
        {
            texto = "digraph G {";
            texto += "AST[label = \"" + Escapar(raiz.ToString()) + "\"];\n";
            contador = 1;
            RecorrerAST("AST", raiz);
            texto += "}";
            return texto;
        }

        private void RecorrerAST(String nombrePadre, ParseTreeNode NodoPadre)
        {
            foreach (ParseTreeNode hijo in NodoPadre.ChildNodes)
            {
                String nombreHijo = "nodo" + contador++;
                texto += nombreHijo + "[label = \"" + Escapar(hijo.ToString()) + "\"];\n";
                texto += nombrePadre + "->" + nombreHijo + ";\n";
                RecorrerAST(nombreHijo, hijo);
            }
        }

        private String Escapar(String cadena)
        {
            cadena = cadena.Replace("\\", "\\\\");
            cadena = cadena.Replace("\"", "\\\"");
            return cadena;
        }
    }
}
