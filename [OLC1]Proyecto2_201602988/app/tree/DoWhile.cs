using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class DoWhile : Instruccion
    {
        private Instruccion cond;
        private List<Instruccion> ins;
        public DoWhile(Instruccion cond)
        {
            this.cond = cond;
            this.ins = new List<Instruccion>();
        }
        public DoWhile(Instruccion cond, List<Instruccion> ins)
        {
            this.cond = cond;
            this.ins = ins;
        }

        public Object ejecutar(Contexto ctx, Stuff stuff)
        {
            object resCond = null;
            do
            {
                Contexto local = ctx.shallowCopy();
                local.terminable = true;
                local.continuable = true;
                foreach (Instruccion i in ins)
                {
                    object resIns = i.ejecutar(local, stuff);
                    if (resIns is Break)
                    {
                        return null;
                    }
                    else if (resIns is Continue)
                    {
                        break;
                    }
                    else if (resIns is Return)
                    {
                        return resIns;
                    }
                }
                resCond = Operacion.Validar(cond.ejecutar(ctx, stuff), ctx, stuff, getLinea(), getColumna());
                if (resCond == null)
                {
                    stuff.error("Semántico", "'DO WHILE', la condición tuvo un error durante la ejecucion.", ((Operacion)cond).fila2, ((Operacion)cond).columna2, ctx);
                    return null;
                }
                if (!(resCond is bool))
                {
                    stuff.error("Semántico", "'DO WHILE', la condición YA NO es del tipo correcto. Esperado: 'BOOLEANO'. Encontrado: " + (Operacion.getTipo(resCond)), ((Operacion)cond).fila2, ((Operacion)cond).columna2, ctx);
                    return null;
                }
            } while ((bool)resCond);
            return null;
        }
        public int getLinea()
        {
            return cond.getLinea();
        }

        public int getColumna()
        {
            return cond.getColumna();
        }
    }
}
