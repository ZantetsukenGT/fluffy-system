using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class Wrapper : Instruccion
    {
        private Instruccion exp;
        public Wrapper(Instruccion exp)
        {
            this.exp = exp;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            exp.ejecutar(ctx, stuff);
            return null;
        }

        public int getLinea()
        {
            return exp.getLinea();
        }

        public int getColumna()
        {
            return exp.getColumna();
        }
    }
}
