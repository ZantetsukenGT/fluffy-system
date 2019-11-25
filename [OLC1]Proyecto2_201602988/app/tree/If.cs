using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class If : Instruccion
    {
        public Instruccion op;
        public List<Instruccion> ins;

        public If(List<Instruccion> ins)
        {
            this.op = null;
            this.ins = ins;
        }
        public If(Instruccion op)
        {
            this.op = op;
            this.ins = new List<Instruccion>();
        }
        public If(Instruccion op, List<Instruccion> ins)
        {
            this.op = op;
            this.ins = ins;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            Contexto local = ctx.shallowCopy();
            foreach (Instruccion i in ins)
            {
                object resOp = i.ejecutar(local, stuff);
                if (resOp is Break || resOp is Continue || resOp is Return)
                {
                    return resOp;
                }
            }
            return null;
        }

        public int getLinea()
        {
            return (op == null) ? 0 : op.getLinea();
        }

        public int getColumna()
        {
            return (op == null) ? 0 : op.getColumna();
        }
    }
}
