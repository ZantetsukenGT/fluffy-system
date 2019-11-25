using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class Switch : Instruccion
    {
        private Instruccion expresion;
        private List<Instruccion> casos;
        private Instruccion casoDefault;

        public Switch(Instruccion expresion)
        {
            this.expresion = expresion;
            this.casos = new List<Instruccion>();
            this.casoDefault = null;
        }
        public Switch(Instruccion expresion, Instruccion casoDefault)
        {
            this.expresion = expresion;
            this.casos = new List<Instruccion>();
            this.casoDefault = casoDefault;
        }
        public Switch(Instruccion expresion, List<Instruccion> casos)
        {
            this.expresion = expresion;
            this.casos = casos;
            this.casoDefault = null;
        }
        public Switch(Instruccion expresion, List<Instruccion> casos, Instruccion casoDefault)
        {
            this.expresion = expresion;
            this.casos = casos;
            this.casoDefault = casoDefault;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            bool cascada = false;
            object resEx = Operacion.Validar(expresion.ejecutar(ctx, stuff), ctx, stuff, getLinea(), getColumna());
            if(resEx == null)
            {
                return null;
            }
            foreach (SwitchCase caso in casos)
            {
                if(cascada)
                {
                    object res = caso.ejecutar(ctx, stuff);
                    if (res is Break)
                    {
                        return null;
                    }
                    else if (res is Continue || res is Return)
                    {
                        return res;
                    }
                }
                else
                {
                    object resOp = new Operacion(resEx, caso.valor, Operacion.Tipo.SWITCHIGUALQUE, expresion.getLinea(), expresion.getColumna(), caso.getLinea(), caso.getColumna()).ejecutar(ctx, stuff);
                    cascada = (bool)((resOp == null) ?false:resOp);
                    if(cascada)
                    {
                        object res = caso.ejecutar(ctx, stuff);
                        if (res is Break)
                        {
                            return null;
                        }
                        else if (res is Continue || res is Return)
                        {
                            return res;
                        }
                    }
                }
            }
            if(casoDefault != null)
            {
                object res = casoDefault.ejecutar(ctx, stuff);
                if (res is Break)
                {
                    return null;
                }
                else if (res is Continue || res is Return)
                {
                    return res;
                }
            }
            return null;
        }

        public int getLinea()
        {
            return expresion.getLinea();
        }

        public int getColumna()
        {
            return expresion.getColumna();
        }
    }
}
