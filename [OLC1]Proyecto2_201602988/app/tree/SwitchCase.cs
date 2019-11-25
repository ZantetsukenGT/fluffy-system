using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class SwitchCase : Instruccion
    {
        public Instruccion valor;
        private List<Instruccion> ins;

        public SwitchCase(Instruccion valor)
        {
            this.valor = valor;
            this.ins = new List<Instruccion>();
        }
        public SwitchCase(Instruccion valor, List<Instruccion> ins)
        {
            this.valor = valor;
            this.ins = ins;
        }
        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            Contexto local = ctx.shallowCopy();
            local.terminable = true;
            foreach (Instruccion i in ins)
            {
                object res = i.ejecutar(local, stuff);
                if (res is Break || res is Continue || res is Return)
                {
                    return res;
                }
            }
            return null;
        }

        public int getLinea()
        {
            return valor.getLinea();
        }

        public int getColumna()
        {
            return valor.getColumna();
        }
    }
}
