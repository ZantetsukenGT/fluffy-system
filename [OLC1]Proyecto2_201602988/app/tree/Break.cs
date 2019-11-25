using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class Break : Instruccion
    {
        public int fila;
        public int columna;
        public Break(int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            if(ctx.terminable)
            {
                return this;
            }
            stuff.error("Semántico", "'BREAK', no está dentro de una estructura iterativa o sentencia switch.", fila, columna, ctx);
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
