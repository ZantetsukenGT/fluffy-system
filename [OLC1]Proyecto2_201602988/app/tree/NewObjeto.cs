using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class NewObjeto : Instruccion
    {
        public string id;
        public int fila, columna;

        public NewObjeto(string id, int fila, int columna)
        {
            this.id = id;
            this.fila = fila;
            this.columna = columna;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            Clase c = ctx.findClass(id);
            if(c == null)
            {
                stuff.error("Semántico", "'NEW OBJETO', la clase '" + id + "' no existe.", fila, columna, ctx);
                return null;
            }
            Objeto ob = new Objeto(id, c.signature.deepCopy());
            foreach (Instruccion i in c.miembros)
            {
                if (i is DeclaracionMetodo)
                {
                    i.ejecutar(ob.atributos, stuff);
                }
            }
            foreach (Instruccion i in c.miembros)
            {
                if (!(i is DeclaracionMetodo))
                {
                    i.ejecutar(ob.atributos, stuff);
                }
            }
            return ob;
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
