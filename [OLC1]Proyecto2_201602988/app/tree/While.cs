using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class While : Instruccion
    {
        public Instruccion op;
        public List<Instruccion> ins;

        public While(Instruccion op)
        {
            this.op = op;
            this.ins = new List<Instruccion>();
        }
        public While(Instruccion op, List<Instruccion> ins)
        {
            this.op = op;
            this.ins = ins;
        }

        public Object ejecutar(Contexto ctx, Stuff stuff)
        {
            Object resOp = Operacion.Validar(op.ejecutar(ctx, stuff), ctx, stuff, getLinea(), getColumna());
            if (resOp == null)
            {
                return null;
            }

            if (resOp is bool)
            {
                while ((bool)resOp)
                {
                    Contexto local = ctx.shallowCopy();
                    local.terminable = true;
                    local.continuable = true;
                    foreach (Instruccion i in ins)
                    {
                        Object resIns = i.ejecutar(local, stuff);
                        if (resIns is Break)
                        {
                            return null;
                        }
                        if (resIns is Continue)
                        {
                            break;
                        }
                        if (resIns is Return)
                        {
                            return resIns;
                        }
                    }
                    resOp = Operacion.Validar(op.ejecutar(ctx, stuff), ctx, stuff, getLinea(), getColumna());
                    if (resOp == null)
                    {
                        stuff.error("Semántico", "'WHILE', la condición tuvo un error durante la ejecucion.", ((Operacion)op).fila2, ((Operacion)op).columna2, ctx);
                        return null;
                    }
                    if (!(resOp is bool))
                    {
                        stuff.error("Semántico", "'WHILE', la condición YA NO es del tipo correcto. Esperado: 'BOOLEANO'. Encontrado: " + (Operacion.getTipo(resOp)), ((Operacion)op).fila2, ((Operacion)op).columna2, ctx);
                        return null;
                    }
                }
            }
            else
            {
                stuff.error("Semántico", "'WHILE', la condición NO es del tipo correcto. Esperado: 'BOOLEANO'. Encontrado: " + (Operacion.getTipo(resOp)), ((Operacion)op).fila2, ((Operacion)op).columna2, ctx);
            }
            return null;
        }

        public int getLinea()
        {
            return op.getLinea();
        }

        public int getColumna()
        {
            return op.getColumna();
        }
    }
}
