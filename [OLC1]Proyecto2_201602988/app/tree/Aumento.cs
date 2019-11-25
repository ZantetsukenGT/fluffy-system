using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class Aumento : Instruccion
    {
        private Instruccion op;
        int fila, columna;
        public Aumento(Instruccion op, int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;
            this.op = op;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            if (op is Operacion)
            {
                Operacion op1 = (Operacion)op;
                if (op1.tipo == Operacion.Tipo.IDENTIFICADOR)
                {
                    Simbolo s = ctx.findSymbol(op1.value.ToString());
                    if (s == null)
                    {
                        stuff.error("Semántico", "'AUMENTO', la variable '" + op1.value.ToString() + "' no existe.", op1.getLinea(), op1.getColumna(), ctx);
                        return null;
                    }
                    if (s.value is BigInteger)
                    {
                        BigInteger r1 = (BigInteger)s.value;
                        s.value = Operacion.Validar(r1 + 1, ctx, stuff, op1.getLinea(), op1.getColumna());
                        return r1;
                    }
                    if (s.value is double)
                    {
                        double r1 = (double)s.value;
                        s.value = Operacion.Validar(r1 + 1.0, ctx, stuff, op1.getLinea(), op1.getColumna());
                        return r1;
                    }
                    if (s.value is char)
                    {
                        BigInteger r1 = (char)s.value;
                        s.value = Operacion.Validar(r1 + 1, ctx, stuff, op1.getLinea(), op1.getColumna());
                        return r1;
                    }
                    stuff.error("Semántico", "'AUMENTO', el tipo del operando es incompatible con esta instruccion. Encontrado: " + Operacion.getTipo(s.value) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR'.", fila, columna, ctx);
                    return null;
                }
                else
                {
                    object resOp = Operacion.Validar(op.ejecutar(ctx, stuff), ctx, stuff, fila, columna);
                    if (resOp == null)
                    {
                        return null;
                    }
                    if (resOp is BigInteger)
                    {
                        BigInteger r1 = (BigInteger)resOp + 1;
                        return r1;
                    }
                    if (resOp is double)
                    {
                        double r1 = (double)resOp + 1.0;
                        return r1;
                    }
                    if (resOp is char)
                    {
                        BigInteger r1 = ((char)resOp + 1);
                        return r1;
                    }
                    stuff.error("Semántico", "'AUMENTO', el tipo del operando es incompatible con esta instruccion. Encontrado: " + Operacion.getTipo(resOp) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR'.", fila, columna, ctx);
                    return null;
                }
            }
            else if (op is AccesoArreglo)
            {
                AccesoArreglo aa = (AccesoArreglo)op;
                object resEx = Operacion.Validar(aa.exp.ejecutar(ctx, stuff), ctx, stuff, aa.exp.getLinea(), aa.exp.getColumna());
                if (resEx == null)
                {
                    return null;
                }
                if (resEx is Arreglo)
                {
                    object resIndice = Operacion.Validar(aa.indice.ejecutar(ctx, stuff), ctx, stuff, aa.indice.getLinea(), aa.indice.getColumna());
                    if (resIndice == null)
                    {
                        return null;
                    }
                    if (resIndice is BigInteger index)
                    {
                        Arreglo arr = (Arreglo)resEx;
                        if (index < 0)
                        {
                            stuff.error("Semántico", "'ACCESO ARREGLO', el valor del indice es demasiado pequeño. Encontrado: " + index + ", Minimo: 0.", aa.indice.getLinea(), aa.indice.getColumna(), ctx);
                            return null;
                        }
                        if (index >= arr.val.Count)
                        {
                            stuff.error("Semántico", "'ACCESO ARREGLO', el valor del indice es demasiado grande. Encontrado: " + index + ", Maximo: " + (arr.val.Count - 1) + ".", aa.indice.getLinea(), aa.indice.getColumna(), ctx);
                            return null;
                        }
                        if (arr.val.ElementAt((int)index) is BigInteger)
                        {
                            BigInteger r1 = (BigInteger)arr.val.ElementAt((int)index);
                            arr.val.RemoveAt((int)index);
                            arr.val.Insert((int)index, Operacion.Validar(r1 + 1, ctx, stuff, aa.indice.getLinea(), aa.indice.getColumna()));
                            return r1;
                        }
                        if (arr.val.ElementAt((int)index) is double)
                        {
                            double r1 = (double)arr.val.ElementAt((int)index);
                            arr.val.RemoveAt((int)index);
                            arr.val.Insert((int)index, Operacion.Validar(r1 + 1.0, ctx, stuff, aa.indice.getLinea(), aa.indice.getColumna()));
                            return r1;
                        }
                        if (arr.val.ElementAt((int)index) is char)
                        {
                            BigInteger r1 = (char)arr.val.ElementAt((int)index);
                            arr.val.RemoveAt((int)index);
                            arr.val.Insert((int)index, Operacion.Validar(r1 + 1, ctx, stuff, aa.indice.getLinea(), aa.indice.getColumna()));
                            return r1;
                        }
                        stuff.error("Semántico", "'AUMENTO', el tipo del elemento indizado es incompatible con esta instruccion. Encontrado: " + Operacion.getTipo(arr.val.ElementAt((int)index)) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR'.", fila, columna, ctx);
                        return null;
                    }
                    stuff.error("Semántico", "'ACCESO ARREGLO', el tipo del indice es incompatible con esta instruccion. Encontrado: " + Operacion.getTipo(resIndice) + ", Esperado: 'ENTERO'.", aa.indice.getLinea(), aa.indice.getColumna(), ctx);
                    return null;
                }
                stuff.error("Semántico", "'ACCESO ARREGLO', el tipo del operando es incompatible con esta instruccion. Encontrado: " + Operacion.getTipo(resEx) + ", Esperado: 'ARREGLO'.", aa.exp.getLinea(), aa.exp.getColumna(), ctx);
                return null;
            }
            else if (op is AccesoPropiedad)
            {
                AccesoPropiedad ap = (AccesoPropiedad)op;

                object resExp1 = Operacion.Validar(ap.exp1.ejecutar(ctx, stuff), ctx, stuff, ap.exp1.getLinea(), ap.exp1.getColumna());
                if (resExp1 == null)
                {
                    return null;
                }
                if (resExp1 is Objeto)
                {
                    if (ap.exp2 is LlamadaMetodo)
                    {
                        LlamadaMetodo ll = (LlamadaMetodo)ap.exp2;
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
                                    if (r.expresionEvaluada is BigInteger)
                                    {
                                        BigInteger r1 = (BigInteger)r.expresionEvaluada + 1;
                                        return r1;
                                    }
                                    if (r.expresionEvaluada is double)
                                    {
                                        double r1 = (double)r.expresionEvaluada + 1.0;
                                        return r1;
                                    }
                                    if (r.expresionEvaluada is char)
                                    {
                                        BigInteger r1 = ((char)r.expresionEvaluada + 1);
                                        return r1;
                                    }
                                    stuff.error("Semántico", "'AUMENTO', el tipo del operando es incompatible con esta instruccion. Encontrado: " + Operacion.getTipo(r.expresionEvaluada) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR'.", fila, columna, ctx);
                                    return null;
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
                    else if (ap.exp2 is Operacion)
                    {
                        Operacion op = (Operacion)ap.exp2;
                        Objeto ob = (Objeto)resExp1;
                        Simbolo s = ob.atributos.findAttribute(op.value.ToString());
                        if (s == null)
                        {
                            stuff.error("Semántico", "'ACCESO PROPIEDAD', la variable '" + op.value.ToString() + "' no existe como propiedad de la clase '" + ob.nombre_clase + "'.", op.getLinea(), op.getColumna(), ctx);
                            return null;
                        }
                        if (s.value is BigInteger)
                        {
                            BigInteger r1 = (BigInteger)s.value;
                            s.value = Operacion.Validar(r1 + 1, ctx, stuff, op.getLinea(), op.getColumna());
                            return r1;
                        }
                        if (s.value is double)
                        {
                            double r1 = (double)s.value;
                            s.value = Operacion.Validar(r1 + 1.0, ctx, stuff, op.getLinea(), op.getColumna());
                            return r1;
                        }
                        if (s.value is char)
                        {
                            BigInteger r1 = (char)s.value;
                            s.value = Operacion.Validar(r1 + 1, ctx, stuff, op.getLinea(), op.getColumna());
                            return r1;
                        }
                        stuff.error("Semántico", "'AUMENTO', el tipo del operando es incompatible con esta instruccion. Encontrado: " + Operacion.getTipo(s.value) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR'.", fila, columna, ctx);
                        return null;
                    }
                }
                else
                {
                    stuff.error("Semántico", "'ACCESO PROPIEDAD', el tipo del operando es incompatible con esta instruccion. Encontrado: " + Operacion.getTipo(resExp1) + ", Esperado: 'OBJETO'.", ap.exp1.getLinea(), ap.exp1.getColumna(), ctx);
                }
                return null;
            }
            else if (op is LlamadaMetodo)
            {
                object resOp = Operacion.Validar(op.ejecutar(ctx, stuff), ctx, stuff, fila, columna);
                if (resOp == null)
                {
                    return null;
                }
                if (resOp is BigInteger)
                {
                    BigInteger r1 = (BigInteger)resOp + 1;
                    return r1;
                }
                if (resOp is double)
                {
                    double r1 = (double)resOp + 1.0;
                    return r1;
                }
                if (resOp is char)
                {
                    BigInteger r1 = ((char)resOp + 1);
                    return r1;
                }
                stuff.error("Semántico", "'AUMENTO', el tipo del operando es incompatible con esta instruccion. Encontrado: " + Operacion.getTipo(resOp) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR'.", fila, columna, ctx);
                return null;
            }
            else
            {
                stuff.error("Semántico", "'AUMENTO', el tipo del operando es incompatible con esta instruccion. Encontrado: " + Operacion.getTipo(op) + ", Esperado: 'EXPRESION', 'LLAMADA METODO', 'VARIABLE', 'PROPIEDAD' , 'INDIZADOR'.", fila, columna, ctx);
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
