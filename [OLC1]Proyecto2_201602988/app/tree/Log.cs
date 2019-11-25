using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class Log : Instruccion
    {
        private Instruccion op;
        public int fila, columna;
        public Log(int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;
            this.op = new Operacion("", Operacion.Tipo.CADENA, 0, 0);
        }
        public Log(Instruccion op, int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;
            this.op = op;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            Object txt = Operacion.Validar(op.ejecutar(ctx, stuff), ctx, stuff, fila, columna);
            if (txt != null)
            {
                stuff.escribir(txt.ToString());
            }
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
