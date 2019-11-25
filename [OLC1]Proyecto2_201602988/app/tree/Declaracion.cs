using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class Declaracion : Instruccion
    {
        public Tipo tipo;
        public List<string> ids;
        public Instruccion op;
        public int fila, columna;

        public enum Tipo
        {
            GLOBAL,
            ATRIBUTO,
            LOCAL
        }

        public Declaracion(Tipo tipo, List<string> ids, int fila, int columna)
        {
            this.tipo = tipo;
            this.ids = ids;
            this.op = null;
            this.fila = fila;
            this.columna = columna;
        }
        public Declaracion(Tipo tipo, List<string> ids, Instruccion op, int fila, int columna)
        {
            this.tipo = tipo;
            this.ids = ids;
            this.op = op;
            this.fila = fila;
            this.columna = columna;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            if (tipo == Tipo.GLOBAL)
            {
                if (op == null)
                {
                    foreach (string id in ids)
                    {
                        Simbolo s = ctx.findGlobal(id);
                        if (s != null)
                        {
                            stuff.error("Semántico", "'DECLARACION GLOBAL', la variable '" + id + "' ya existe.", fila, columna, ctx);
                        }
                        else
                        {
                            s = new Simbolo(id, Simbolo.NULL);
                            ctx.globales.Add(s);
                        }
                    }
                    return null;
                }
                bool evaluado = false;
                object resOp = null;
                foreach (string id in ids)
                {
                    Simbolo s = ctx.findGlobal(id);
                    if (s != null)
                    {
                        stuff.error("Semántico", "'DECLARACION GLOBAL', la variable '" + id + "' ya existe.", fila, columna, ctx);
                    }
                    else
                    {
                        if (!evaluado)
                        {
                            evaluado = true;
                            resOp = Operacion.Validar(op.ejecutar(ctx, stuff), ctx, stuff, fila, columna);
                            if (resOp == null)
                            {
                                resOp = Simbolo.NULL;
                            }
                        }
                        s = new Simbolo(id, resOp);
                        ctx.globales.Add(s);
                    }
                }
                return null;
            }
            else if (tipo == Tipo.ATRIBUTO)
            {
                if (op == null)
                {
                    foreach (string id in ids)
                    {
                        Simbolo s = ctx.findAttribute(id);
                        if (s != null)
                        {
                            stuff.error("Semántico", "'DECLARACION ATRIBUTO', la variable '" + id + "' ya existe.", fila, columna, ctx);
                        }
                        else
                        {
                            s = new Simbolo(id, Simbolo.NULL);
                            ctx.atributos.Add(s);
                        }
                    }
                    return null;
                }
                bool evaluado = false;
                object resOp = null;
                foreach (string id in ids)
                {
                    Simbolo s = ctx.findAttribute(id);
                    if (s != null)
                    {
                        stuff.error("Semántico", "'DECLARACION ATRIBUTO', la variable '" + id + "' ya existe.", fila, columna, ctx);
                    }
                    else
                    {
                        if (!evaluado)
                        {
                            evaluado = true;
                            resOp = Operacion.Validar(op.ejecutar(ctx, stuff), ctx, stuff, fila, columna);
                            if (resOp == null)
                            {
                                resOp = Simbolo.NULL;
                            }
                        }
                        s = new Simbolo(id, resOp);
                        ctx.atributos.Add(s);
                    }
                }
                return null;
            }
            else
            {
                if (op == null)
                {
                    foreach (string id in ids)
                    {
                        Simbolo s = ctx.findLocalSame(id);
                        if (s != null)
                        {
                            stuff.error("Semántico", "'DECLARACION', la variable '" + id + "' ya existe.", fila, columna, ctx);
                        }
                        else
                        {
                            s = new Simbolo(id, Simbolo.NULL);
                            ctx.locales_mismo_nivel.Add(s);
                            ctx.locales_cualquier_nivel.Insert(0, s);
                        }
                    }
                    return null;
                }
                bool evaluado = false;
                object resOp = null;
                foreach (string id in ids)
                {
                    Simbolo s = ctx.findLocalSame(id);
                    if (s != null)
                    {
                        stuff.error("Semántico", "'DECLARACION', la variable '" + id + "' ya existe.", fila, columna, ctx);
                    }
                    else
                    {
                        if (!evaluado)
                        {
                            evaluado = true;
                            resOp = Operacion.Validar(op.ejecutar(ctx, stuff), ctx, stuff, fila, columna);
                            if (resOp == null)
                            {
                                resOp = Simbolo.NULL;
                            }
                        }
                        s = new Simbolo(id, resOp);
                        ctx.locales_mismo_nivel.Add(s);
                        ctx.locales_cualquier_nivel.Insert(0, s);
                    }
                }
                return null;
            }
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
