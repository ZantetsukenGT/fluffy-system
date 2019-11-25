using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class For : Instruccion
    {
        private Instruccion init;
        private Instruccion cond;
        private Instruccion aumento;
        private List<Instruccion> ins;
        public For(Instruccion init, Instruccion cond, Instruccion aumento)
        {
            this.init = init;
            this.cond = cond;
            this.aumento = aumento;
            this.ins = new List<Instruccion>();
        }
        public For(Instruccion init, Instruccion cond, Instruccion aumento, List<Instruccion> ins)
        {
            this.init = init;
            this.cond = cond;
            this.aumento = aumento;
            this.ins = ins;
        }

        public Object ejecutar(Contexto ctx, Stuff stuff)
        {
            Contexto localPermanente = ctx.shallowCopy();
            localPermanente.terminable = true;
            localPermanente.continuable = true;
            init.ejecutar(localPermanente, stuff);
            Object resCond = Operacion.Validar(cond.ejecutar(localPermanente, stuff), ctx, stuff, getLinea(), getColumna());
            if (resCond == null)
            {
                return null;
            }

            if (resCond is bool)
            {
                while((bool)resCond)
                {
                    Contexto localVolatil = localPermanente.shallowCopy();
                    foreach (Instruccion i in ins)
                    {
                        object resIns = i.ejecutar(localVolatil, stuff);
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
                    aumento.ejecutar(localPermanente, stuff);
                    resCond = Operacion.Validar(cond.ejecutar(localPermanente, stuff), ctx, stuff, getLinea(), getColumna());
                    if (resCond == null)
                    {
                        stuff.error("Semántico", "'FOR', la condición tuvo un error durante la ejecucion.", ((Operacion)cond).fila2, ((Operacion)cond).columna2, ctx);
                        return null;
                    }
                    if (!(resCond is bool))
                    {
                        stuff.error("Semántico", "'FOR', la condición YA NO es del tipo correcto. Esperado: 'BOOLEANO'. Encontrado: " + (Operacion.getTipo(resCond)), ((Operacion)cond).fila2, ((Operacion)cond).columna2, ctx);
                        return null;
                    }
                }
            }
            else
            {
                stuff.error("Semántico", "'FOR', la condición NO es del tipo correcto. Esperado: 'BOOLEANO'. Encontrado: " + (Operacion.getTipo(resCond)), ((Operacion)cond).fila2, ((Operacion)cond).columna2, ctx);
            }
            return null;
        }
        public int getLinea()
        {
            return init.getLinea();
        }

        public int getColumna()
        {
            return init.getColumna();
        }
    }
}
