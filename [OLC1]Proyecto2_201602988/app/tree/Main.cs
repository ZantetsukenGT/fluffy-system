using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class Main : Instruccion
    {
        public string id;
        public List<Instruccion> ins;
        public int fila, columna;
        public Main(string id, int fila, int columna)
        {
            this.id = id;
            this.ins = new List<Instruccion>();
            this.fila = fila;
            this.columna = columna;
        }
        public Main(string id, List<Instruccion> ins, int fila, int columna)
        {
            this.id = id;
            this.ins = ins;
            this.fila = fila;
            this.columna = columna;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            if (id.Equals("main", StringComparison.InvariantCultureIgnoreCase))
            {
                Contexto local = ctx.shallowCopy();

                foreach (Instruccion i in ins)
                {
                    i.ejecutar(local, stuff);
                }
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
