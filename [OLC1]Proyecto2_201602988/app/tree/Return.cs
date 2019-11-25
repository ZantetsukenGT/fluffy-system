using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class Return : Instruccion
    {
        public Instruccion expresion;
        public object expresionEvaluada;

        public int fila;
        public int columna;
        public Return(Instruccion expresion, int fila, int columna)
        {
            this.expresion = expresion;
            this.fila = fila;
            this.columna = columna;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            if (ctx.retornable)
            {
                if (ctx.esVoid)
                {
                    stuff.error("Semántico", "'RETURN' no permitido en un metodo void", fila, columna, ctx);
                    expresionEvaluada = null;
                    return this;
                }
                expresionEvaluada = Operacion.Validar(expresion.ejecutar(ctx, stuff), ctx, stuff, getLinea(), getColumna());
                return this;
            }
            stuff.error("Semántico", "'RETURN', solo se permite dentro de un método o función.", fila, columna, ctx);
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