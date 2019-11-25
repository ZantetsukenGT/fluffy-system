using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class DeclaracionClase : Instruccion
    {
        public string id;
        public List<Instruccion> miembros;
        public int fila, columna;

        public DeclaracionClase(string id, int fila, int columna)
        {
            this.id = id;
            this.miembros = new List<Instruccion>();
            this.fila = fila;
            this.columna = columna;
        }

        public DeclaracionClase(string id, List<Instruccion> miembros, int fila, int columna)
        {
            this.id = id;
            this.miembros = miembros;
            this.fila = fila;
            this.columna = columna;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            Clase c = ctx.findClass(id);
            if (c != null)
            {
                stuff.error("Semántico", "'DECLARACION CLASE', la clase '" + id + "' ya existe.", fila, columna, ctx);
                return null;
            }
            else
            {
                c = new Clase(id);
                c.signature.otrosArchivos = ctx.otrosArchivos;
                c.signature.currentFile = ctx.currentFile;
                c.signature.globales = ctx.globales;
                c.signature.clases = ctx.clases;
                c.signature.metodos_globales = ctx.metodos_globales;
                c.signature.terminable = false;
                c.signature.continuable = false;
                c.signature.retornable = false;
                c.signature.esVoid = false;

                c.miembros = miembros;
                ctx.clases.Add(c);
                return null;
            }
        }

        public int getLinea()
        {
            return fila;
        }

        public int getColumna()
        {
            return columna;
        }
    }
}
