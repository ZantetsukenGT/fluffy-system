using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class AccesoPropiedad : Instruccion
    {
        public Instruccion exp1;
        public Instruccion exp2;

        public AccesoPropiedad(Instruccion exp1, Instruccion exp2)
        {
            this.exp1 = exp1;
            this.exp2 = exp2;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            object resExp1 = Operacion.Validar(exp1.ejecutar(ctx, stuff), ctx, stuff, exp1.getLinea(), exp1.getColumna());
            if (resExp1 == null)
            {
                return null;
            }
            if(resExp1 is Objeto)
            {
                if (exp2 is LlamadaMetodo)
                {
                    LlamadaMetodo ll = (LlamadaMetodo)exp2;
                    Objeto ob = (Objeto)resExp1;
                    Metodo m = ob.atributos.findLocalMethod(ll.id, ll.expresiones_params.Count);
                    if (m == null)
                    {
                        stuff.error("Semántico", "'LLAMADA METODO/FUNCION',  el método o función '" + ll.id + "' no está definido dentro de la clase '" + ob.nombre_clase + "', o no tiene '" + ll.expresiones_params.Count + "' parametros.", ll.getLinea(), ll.getColumna(), ctx);
                        return null;
                    }
                    else
                    {
                        Contexto ctx_metodo = new Contexto();
                        ctx_metodo.otrosArchivos = ob.atributos.otrosArchivos;
                        ctx_metodo.currentFile = ob.atributos.currentFile;
                        ctx_metodo.globales = ob.atributos.globales;
                        ctx_metodo.atributos = ob.atributos.atributos;
                        ctx_metodo.clases = ob.atributos.clases;
                        ctx_metodo.metodos = ob.atributos.metodos;
                        ctx_metodo.metodos_globales = ob.atributos.metodos_globales;
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
                            Instruccion i = ll.expresiones_params.ElementAt(j);
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
                            stuff.error("Semántico", "'LLAMADA METODO/FUNCION',  la función '" + ob.nombre_clase + "." + ll.id + "' de " + m.cantidad_params + " parametros carece de una instrucción 'RETURN' con expresion.", ll.getLinea(), ll.getColumna(), ctx);
                            return null;
                        }
                        return new lang.engine.Void(ob.nombre_clase, ll.id, m.cantidad_params);
                    }
                }
                else if (exp2 is Operacion)
                {
                    Operacion op = (Operacion)exp2;
                    Objeto ob = (Objeto)resExp1;
                    Simbolo s = ob.atributos.findAttribute(op.value.ToString());
                    if (s == null)
                    {
                        stuff.error("Semántico", "'ACCESO PROPIEDAD', la variable '" + op.value.ToString() + "' no existe como propiedad de la clase '" + ob.nombre_clase + "'.", op.getLinea(), op.getColumna(), ctx);
                        return null;
                    }
                    return s.value;
                }
            }
            else
            {
                stuff.error("Semántico", "'ACCESO PROPIEDAD', el tipo del operando es incompatible con esta instruccion. Encontrado: " + Operacion.getTipo(resExp1) + ", Esperado: 'OBJETO'.", exp1.getLinea(), exp1.getColumna(), ctx);
            }
            return null;
        }

        public int getLinea()
        {
            return exp2.getLinea();
        }

        public int getColumna()
        {
            return exp2.getColumna();
        }
    }
}
