using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class Declaraciones : Instruccion
    {
        public List<Instruccion> declaraciones;

        public Declaraciones(List<Instruccion> declaraciones)
        {
            this.declaraciones = declaraciones;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            foreach(Instruccion i in declaraciones)
            {
                i.ejecutar(ctx, stuff);
            }
            return null;
        }

        public int getColumna()
        {
            return 0;
        }

        public int getLinea()
        {
            return 0;
        }
    }
}
