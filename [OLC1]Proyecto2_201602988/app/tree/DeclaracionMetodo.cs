using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class DeclaracionMetodo : Instruccion
    {
        public bool global;
        public bool esVoid;
        public string id;
        public Instruccion decl_parametros;
        public int cantidad_params;
        public List<Instruccion> ins;

        public int fila, columna;

        public DeclaracionMetodo(bool global, bool esVoid, string id, int fila, int columna)
        {
            this.global = global;
            this.esVoid = esVoid;
            this.id = id;
            this.decl_parametros = null;
            this.cantidad_params = 0;
            this.ins = new List<Instruccion>();
            this.fila = fila;
            this.columna = columna;
        }

        public DeclaracionMetodo(bool global, bool esVoid, string id, Instruccion decl_parametros, int fila, int columna)
        {
            this.global = global;
            this.esVoid = esVoid;
            this.id = id;
            this.decl_parametros = decl_parametros;
            this.cantidad_params = ((Declaracion)decl_parametros).ids.Count;
            this.ins = new List<Instruccion>();
            this.fila = fila;
            this.columna = columna;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            String tipo = (esVoid) ?"el metodo":"la función";
            if (global)
            {
                Metodo m = ctx.findGlobalMethod(id, cantidad_params);
                if (m != null)
                {
                    stuff.error("Semántico", "'DECLARACION METODO GLOBAL', " + tipo + "'" + id + "' ya está definido, utilice una cantidad distinta de parametros.", fila, columna, ctx);
                }
                else
                {
                    m = new Metodo(esVoid, id, decl_parametros, cantidad_params, ins);
                    ctx.metodos_globales.Add(m);
                }
                return null;
            }
            else
            {
                Metodo m = ctx.findLocalMethod(id, cantidad_params);
                if (m != null)
                {
                    stuff.error("Semántico", "'DECLARACION METODO', " + tipo + "'" + id + "' ya está definido, utilice una cantidad distinta de parametros.", fila, columna, ctx);
                }
                else
                {
                    m = new Metodo(esVoid, id, decl_parametros, cantidad_params, ins);
                    ctx.metodos.Add(m);
                }
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
