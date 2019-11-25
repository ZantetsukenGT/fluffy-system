using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class Continue : Instruccion
    {
        public int fila;
        public int columna;
        public Continue(int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            if (ctx.continuable)
            {
                return this;
            }
            stuff.error("Semántico", "'CONTINUE', no está dentro de una estructura iterativa.", fila, columna, ctx);
            return null;
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
