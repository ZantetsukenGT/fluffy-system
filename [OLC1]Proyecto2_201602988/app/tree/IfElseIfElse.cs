using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class IfElseIfElse : Instruccion
    {
        private Instruccion principal;
        private List<Instruccion> listaElseif;
        private Instruccion ultimo;
        public IfElseIfElse(Instruccion principal)
        {
            this.principal = principal;
            this.listaElseif = new List<Instruccion>();
            this.ultimo = null;
        }
        public IfElseIfElse(Instruccion principal, List<Instruccion> listaElseif)
        {
            this.principal = principal;
            this.listaElseif = listaElseif;
            this.ultimo = null;
        }
        public IfElseIfElse(Instruccion principal, Instruccion ultimo)
        {
            this.principal = principal;
            this.listaElseif = new List<Instruccion>();
            this.ultimo = ultimo;
        }
        public IfElseIfElse(Instruccion principal, List<Instruccion> listaElseif, Instruccion ultimo)
        {
            this.principal = principal;
            this.listaElseif = listaElseif;
            this.ultimo = ultimo;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            If principalTemp = (If)principal;
            Object resOp = Operacion.Validar(principalTemp.op.ejecutar(ctx, stuff), ctx, stuff, getLinea(), getColumna());
            //principal
            if (resOp == null)
            {
                return null;
            }
            if (resOp is bool)
            {
                if ((bool)resOp)
                {
                    return principal.ejecutar(ctx, stuff);
                }
            }
            else
            {
                stuff.error("Semántico", "'IF', la condición es del tipo incorrecto. Esperado: 'BOOLEANO'. Encontrado: " + (Operacion.getTipo(resOp)), ((Operacion)principalTemp.op).fila2, ((Operacion)principalTemp.op).columna2, ctx);
                return null;
            }

            //else ifs
            foreach (Instruccion elseIf in listaElseif)
            {
                If elseIfTemp = (If)elseIf;
                resOp = Operacion.Validar(elseIfTemp.op.ejecutar(ctx, stuff), ctx, stuff, getLinea(), getColumna());
                if (resOp == null)
                {
                    return null;
                }
                if (resOp is bool)
                {
                    if ((bool)resOp)
                    {
                        return elseIf.ejecutar(ctx, stuff);
                    }
                }
                else
                {
                    stuff.error("Semántico", "'ELSE IF', la condición es del tipo incorrecto. Esperado: 'BOOLEANO'. Encontrado: " + (Operacion.getTipo(resOp)), ((Operacion)principalTemp.op).fila2, ((Operacion)principalTemp.op).columna2, ctx);
                    return null;
                }
            }
            //else
            if (ultimo != null)
            {
                return ultimo.ejecutar(ctx, stuff);
            }
            return null;
        }

        public int getLinea()
        {
            return principal.getLinea();
        }

        public int getColumna()
        {
            return principal.getColumna();
        }
    }
}
