using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class AccesoArreglo : Instruccion
    {
        public Instruccion exp;
        public Instruccion indice;

        public AccesoArreglo(Instruccion exp, Instruccion indice)
        {
            this.exp = exp;
            this.indice = indice;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            object resEx = Operacion.Validar(exp.ejecutar(ctx, stuff), ctx, stuff, exp.getLinea(), exp.getColumna());
            if(resEx == null)
            {
                return null;
            }
            if(resEx is Arreglo)
            {
                object resIndice = Operacion.Validar(indice.ejecutar(ctx, stuff), ctx, stuff, indice.getLinea(), indice.getColumna());
                if(resIndice == null)
                {
                    return null;
                }
                if(resIndice is BigInteger index)
                {
                    Arreglo arr = (Arreglo)resEx;
                    if (index < 0)
                    {
                        stuff.error("Semántico", "'ACCESO ARREGLO', el valor del indice es demasiado pequeño. Encontrado: " + index + ", Minimo: 0.", indice.getLinea(), indice.getColumna(), ctx);
                        return null;
                    }
                    if (index >= arr.val.Count)
                    {
                        stuff.error("Semántico", "'ACCESO ARREGLO', el valor del indice es demasiado grande. Encontrado: " + index + ", Maximo: " + (arr.val.Count - 1) + ".", indice.getLinea(), indice.getColumna(), ctx);
                        return null;
                    }
                    return arr.val.ElementAt((int)index);
                }
                stuff.error("Semántico", "'ACCESO ARREGLO', el tipo del indice es incompatible con esta instruccion. Encontrado: " + Operacion.getTipo(resIndice) + ", Esperado: 'ENTERO'.", indice.getLinea(), indice.getColumna(), ctx);
                return null;
            }
            stuff.error("Semántico", "'ACCESO ARREGLO', el tipo del operando es incompatible con esta instruccion. Encontrado: " + Operacion.getTipo(resEx) + ", Esperado: 'ARREGLO'.", exp.getLinea(), exp.getColumna(), ctx);
            return null;
        }

        public int getLinea()
        {
            return indice.getLinea();
        }

        public int getColumna()
        {
            return indice.getColumna();
        }
    }
}
