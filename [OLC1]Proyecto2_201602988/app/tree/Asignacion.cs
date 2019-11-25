using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class Asignacion : Instruccion
    {
        public Instruccion exp1;
        public Instruccion exp2;

        public Asignacion(Instruccion exp1, Instruccion exp2)
        {
            this.exp1 = exp1;
            this.exp2 = exp2;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            if (exp1 is Operacion)
            {
                Operacion op1 = (Operacion)exp1;
                if (op1.tipo == Operacion.Tipo.IDENTIFICADOR)
                {
                    Simbolo s = ctx.findSymbol(op1.value.ToString());
                    if (s == null)
                    {
                        stuff.error("Semántico", "'ASIGNACION', la variable '" + op1.value.ToString() + "' no existe.", exp1.getLinea(), exp1.getColumna(), ctx);
                        return null;
                    }
                    object resOp = Operacion.Validar(exp2.ejecutar(ctx, stuff), ctx, stuff, exp2.getLinea(), exp2.getColumna());
                    if(resOp == null)
                    {
                        resOp = Simbolo.NULL;
                    }
                    s.value = resOp;
                    return null;
                }
                else
                {
                    stuff.error("Semántico", "'ASIGNACION', El lado izquierdo de una asignación debe ser una variable, propiedad o indizador.", exp1.getLinea(), exp1.getColumna(), ctx);
                    return null;
                }
            }
            else if (exp1 is AccesoArreglo)
            {
                AccesoArreglo aa = (AccesoArreglo)exp1;
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
                        object resOp = Operacion.Validar(exp2.ejecutar(ctx, stuff), ctx, stuff, exp2.getLinea(), exp2.getColumna());
                        if (resOp == null)
                        {
                            resOp = Simbolo.NULL;
                        }
                        arr.val.RemoveAt((int)index);
                        arr.val.Insert((int)index, resOp);
                        return null;
                    }
                    stuff.error("Semántico", "'ACCESO ARREGLO', el tipo del indice es incompatible con esta instruccion. Encontrado: " + Operacion.getTipo(resIndice) + ", Esperado: 'ENTERO'.", aa.indice.getLinea(), aa.indice.getColumna(), ctx);
                    return null;
                }
                stuff.error("Semántico", "'ACCESO ARREGLO', el tipo del operando es incompatible con esta instruccion. Encontrado: " + Operacion.getTipo(resEx) + ", Esperado: 'ARREGLO'.", aa.exp.getLinea(), aa.exp.getColumna(), ctx);
                return null;
            }
            else if (exp1 is AccesoPropiedad)
            {
                AccesoPropiedad ap = (AccesoPropiedad)exp1;

                object resExp1 = Operacion.Validar(ap.exp1.ejecutar(ctx, stuff), ctx, stuff, ap.exp1.getLinea(), ap.exp1.getColumna());
                if (resExp1 == null)
                {
                    return null;
                }
                if (resExp1 is Objeto)
                {
                    if (ap.exp2 is LlamadaMetodo)
                    {
                        stuff.error("Semántico", "'ASIGNACION', El lado izquierdo de una asignación debe ser una variable, propiedad o indizador.", exp1.getLinea(), exp1.getColumna(), ctx);
                        return null;
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
                        object resOp = Operacion.Validar(exp2.ejecutar(ctx, stuff), ctx, stuff, exp2.getLinea(), exp2.getColumna());
                        if (resOp == null)
                        {
                            resOp = Simbolo.NULL;
                        }
                        s.value = resOp;
                        return null;
                    }
                }
                else
                {
                    stuff.error("Semántico", "'ACCESO PROPIEDAD', el tipo del operando es incompatible con esta instruccion. Encontrado: " + Operacion.getTipo(resExp1) + ", Esperado: 'OBJETO'.", ap.exp1.getLinea(), ap.exp1.getColumna(), ctx);
                }
                return null;
            }
            else if (exp1 is LlamadaMetodo)
            {
                stuff.error("Semántico", "'ASIGNACION', El lado izquierdo de una asignación debe ser una variable, propiedad o indizador.", exp1.getLinea(), exp1.getColumna(), ctx);
                return null;
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
