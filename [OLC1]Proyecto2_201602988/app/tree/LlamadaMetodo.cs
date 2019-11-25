using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class LlamadaMetodo : Instruccion
    {
        public string id;
        public List<Instruccion> expresiones_params;
        public int fila, columna;

        public LlamadaMetodo(string id, List<Instruccion> expresiones_params, int fila, int columna)
        {
            this.id = id;
            this.expresiones_params = expresiones_params;
            this.fila = fila;
            this.columna = columna;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            Metodo m = ctx.findLocalMethod(id, expresiones_params.Count);
            if (m == null)
            {
                m = ctx.findGlobalFilesMethod(id, expresiones_params.Count);
                if (m == null)
                {
                    stuff.error("Semántico", "'LLAMADA METODO/FUNCION',  el método o función '" + id + "' no está definido, o no tiene '" + expresiones_params.Count + "' parametros.", fila, columna, ctx);
                    return null;
                }
                Contexto otroGlobal = ctx.findGlobalContextMethod(m);
                Contexto ctx_metodo = new Contexto();
                ctx_metodo.otrosArchivos = otroGlobal.otrosArchivos;
                ctx_metodo.currentFile = otroGlobal.currentFile;
                ctx_metodo.globales = otroGlobal.globales;
                ctx_metodo.clases = otroGlobal.clases;
                ctx_metodo.metodos_globales = otroGlobal.metodos_globales;
                ctx_metodo.terminable = false;
                ctx_metodo.continuable = false;
                ctx_metodo.retornable = true;
                ctx_metodo.esVoid = m.esVoid;

                if(m.decl_params != null)
                {
                    m.decl_params.ejecutar(ctx_metodo, stuff);
                }
                for (int j = 0; j < m.cantidad_params; j++)
                {
                    Simbolo s = ctx_metodo.locales_mismo_nivel.ElementAt(j);
                    Instruccion i = expresiones_params.ElementAt(j);
                    object resIns = Operacion.Validar(i.ejecutar(ctx, stuff), ctx, stuff, i.getLinea(), i.getColumna());
                    if(resIns == null)
                    {
                        resIns = Simbolo.NULL;
                    }
                    s.value = resIns;
                }
                foreach(Instruccion i in m.listaInstrucciones)
                {
                    object res = i.ejecutar(ctx_metodo, stuff);
                    if(res is Return r)
                    {
                        return r.expresionEvaluada;
                    }
                }
                if(!m.esVoid)
                {
                    stuff.error("Semántico", "'LLAMADA METODO/FUNCION',  la función '" + id + "' de " + m.cantidad_params + " parametros carece de una instrucción 'RETURN' con expresion.", fila, columna, ctx);
                    return null;
                }
                return new lang.engine.Void(id, m.cantidad_params);
            }
            else
            {
                Contexto ctx_metodo = new Contexto();
                ctx_metodo.otrosArchivos = ctx.otrosArchivos;
                ctx_metodo.currentFile = ctx.currentFile;
                ctx_metodo.globales = ctx.globales;
                ctx_metodo.atributos = ctx.atributos;
                ctx_metodo.clases = ctx.clases;
                ctx_metodo.metodos = ctx.metodos;
                ctx_metodo.metodos_globales = ctx.metodos_globales;
                ctx_metodo.terminable = false;
                ctx_metodo.continuable = false;
                ctx_metodo.retornable = true;
                ctx_metodo.esVoid = m.esVoid;

                if (m.decl_params != null)
                {
                    m.decl_params.ejecutar(ctx_metodo, stuff);
                }
                for (int j = 0; j < m.cantidad_params; j++)
                {
                    Simbolo s = ctx_metodo.locales_mismo_nivel.ElementAt(j);
                    Instruccion i = expresiones_params.ElementAt(j);
                    object resIns = Operacion.Validar(i.ejecutar(ctx, stuff), ctx, stuff, i.getLinea(), i.getColumna());
                    if (resIns == null)
                    {
                        resIns = Simbolo.NULL;
                    }
                    s.value = resIns;
                }
                foreach (Instruccion i in m.listaInstrucciones)
                {
                    object res = i.ejecutar(ctx_metodo, stuff);
                    if (res is Return r)
                    {
                        return r.expresionEvaluada;
                    }
                }
                if (!m.esVoid)
                {
                    stuff.error("Semántico", "'LLAMADA METODO/FUNCION',  la función '" + id + "' de " + m.cantidad_params + " parametros carece de una instrucción 'RETURN' con expresion.", fila, columna, ctx);
                    return null;
                }
                return new lang.engine.Void(id, m.cantidad_params);
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
