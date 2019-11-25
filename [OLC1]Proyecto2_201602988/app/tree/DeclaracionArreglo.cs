using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class DeclaracionArreglo : Instruccion
    {
        public Declaracion.Tipo tipo;
        public List<string> ids;
        public List<Instruccion> indices;
        public Instruccion arreglo;

        public int fila, columna;

        public DeclaracionArreglo(Declaracion.Tipo tipo, List<string> ids, List<Instruccion> indices, int fila, int columna)
        {
            this.tipo = tipo;
            this.ids = ids;
            this.indices = indices;
            this.arreglo = null;
            this.fila = fila;
            this.columna = columna;
        }

        public DeclaracionArreglo(Declaracion.Tipo tipo, List<string> ids, List<Instruccion> indices, Instruccion arreglo, int fila, int columna)
        {
            this.tipo = tipo;
            this.ids = ids;
            this.indices = indices;
            this.arreglo = arreglo;
            this.fila = fila;
            this.columna = columna;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            if (tipo == Declaracion.Tipo.GLOBAL)
            {
                if (arreglo == null)
                {
                    bool evaluado = false;
                    object resOp = null;
                    foreach (string id in ids)
                    {
                        Simbolo s = ctx.findGlobal(id);
                        if (s != null)
                        {
                            stuff.error("Semántico", "'DECLARACION ARREGLO GLOBAL', la variable '" + id + "' ya existe.", fila, columna, ctx);
                        }
                        else
                        {
                            if (!evaluado)
                            {
                                evaluado = true;
                                List<object> indicesEvaluados = new List<object>();
                                bool error = false;
                                foreach (Instruccion i in indices)
                                {
                                    object e = Operacion.Validar(i.ejecutar(ctx, stuff), ctx, stuff, i.getLinea(), i.getColumna());
                                    if (e == null)
                                    {
                                        error = true;
                                        break;
                                    }
                                    if (e is BigInteger)
                                    {
                                        indicesEvaluados.Add(e);
                                    }
                                    else
                                    {
                                        error = true;
                                        stuff.error("Semántico", "'DECLARACION ARREGLO GLOBAL', una de las expresiones de la declaración de índices es del tipo incorrecto. Encontrado: " + Operacion.getTipo(e) + ", Esperado: 'ENTERO'.", fila, columna, ctx);
                                        break;
                                    }
                                }
                                if (error)
                                {
                                    resOp = Simbolo.NULL;
                                }
                                else
                                {
                                    if (indicesEvaluados.Count == 1)
                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                    else if (indicesEvaluados.Count == 2)
                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                    else
                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                }
                            }
                            s = new Simbolo(id, resOp);
                            ctx.globales.Add(s);
                        }
                    }
                    return null;
                }
                else
                {
                    bool evaluado = false;
                    object resOp = null;
                    foreach (string id in ids)
                    {
                        Simbolo s = ctx.findGlobal(id);
                        if (s != null)
                        {
                            stuff.error("Semántico", "'DECLARACION ARREGLO GLOBAL', la variable '" + id + "' ya existe.", fila, columna, ctx);
                        }
                        else
                        {
                            if (!evaluado)
                            {
                                evaluado = true;
                                List<object> indicesEvaluados = new List<object>();
                                bool error = false;
                                foreach (Instruccion i in indices)
                                {
                                    object e = Operacion.Validar(i.ejecutar(ctx, stuff), ctx, stuff, i.getLinea(), i.getColumna());
                                    if (e == null)
                                    {
                                        error = true;
                                        break;
                                    }
                                    if (e is BigInteger)
                                    {
                                        indicesEvaluados.Add(e);
                                    }
                                    else
                                    {
                                        error = true;
                                        stuff.error("Semántico", "'DECLARACION ARREGLO GLOBAL', una de las expresiones de la declaración de índices es del tipo incorrecto. Encontrado: " + Operacion.getTipo(e) + ", Esperado: 'ENTERO'.", fila, columna, ctx);
                                        break;
                                    }
                                }
                                if (error)
                                {
                                    resOp = Simbolo.NULL;
                                }
                                else
                                {
                                    object arr = Operacion.Validar(arreglo.ejecutar(ctx, stuff), ctx, stuff, arreglo.getLinea(), arreglo.getColumna());
                                    if (arr == null)
                                    {
                                        if (indicesEvaluados.Count == 1)
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                        else if (indicesEvaluados.Count == 2)
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                        else
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                    }
                                    else if(arr is Arreglo)
                                    {
                                        if (indicesEvaluados.Count == ((Arreglo)arr).dims)
                                        {
                                            if (indicesEvaluados.Count == 1)
                                            {
                                                if((int)(BigInteger)indicesEvaluados.ElementAt(0) == (int)(BigInteger)((Arreglo)arr).val.Count)
                                                    resOp = arr;
                                                else
                                                {
                                                    stuff.error("Semántico", "'DECLARACION ARREGLO GLOBAL', la expresión de arreglo no tiene el tamaño adecuado dictado por su declaración. Encontrado: '[" + (int)(BigInteger)((Arreglo)arr).val.Count + "]', Esperado: '[" + (int)(BigInteger)indicesEvaluados.ElementAt(0) + "]'.", fila, columna, ctx);
                                                    if (indicesEvaluados.Count == 1)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                                    else if (indicesEvaluados.Count == 2)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                                    else
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                                }
                                            }
                                            else if (indicesEvaluados.Count == 2)
                                            {
                                                if ((int)(BigInteger)indicesEvaluados.ElementAt(0) == (int)(BigInteger)((Arreglo)arr).val.Count
                                                    && (int)(BigInteger)indicesEvaluados.ElementAt(1) == (int)(BigInteger)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.Count)
                                                    resOp = arr;
                                                else
                                                {
                                                    stuff.error("Semántico", "'DECLARACION ARREGLO GLOBAL', la expresión de arreglo no tiene el tamaño adecuado dictado por su declaración. Encontrado: '[" + (int)(BigInteger)((Arreglo)arr).val.Count + "][" + (int)(BigInteger)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.Count + "]', Esperado: '[" + (int)(BigInteger)indicesEvaluados.ElementAt(0) + "][" + (int)(BigInteger)indicesEvaluados.ElementAt(1) + "]'.", fila, columna, ctx);
                                                    if (indicesEvaluados.Count == 1)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                                    else if (indicesEvaluados.Count == 2)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                                    else
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                                }
                                            }
                                            else
                                            {
                                                if ((int)(BigInteger)indicesEvaluados.ElementAt(0) == (int)(BigInteger)((Arreglo)arr).val.Count
                                                    && (int)(BigInteger)indicesEvaluados.ElementAt(1) == (int)(BigInteger)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.Count
                                                    && (int)(BigInteger)indicesEvaluados.ElementAt(2) == (int)(BigInteger)((Arreglo)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.ElementAt(0)).val.Count)
                                                    resOp = arr;
                                                else
                                                {
                                                    stuff.error("Semántico", "'DECLARACION ARREGLO GLOBAL', la expresión de arreglo no tiene el tamaño adecuado dictado por su declaración. Encontrado: '[" + (int)(BigInteger)((Arreglo)arr).val.Count + "][" + (int)(BigInteger)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.Count + "][" + (int)(BigInteger)((Arreglo)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.ElementAt(0)).val.Count + "]', Esperado: '[" + (int)(BigInteger)indicesEvaluados.ElementAt(0) + "][" + (int)(BigInteger)indicesEvaluados.ElementAt(1) + "][" + (int)(BigInteger)indicesEvaluados.ElementAt(2) + "]'.", fila, columna, ctx);
                                                    if (indicesEvaluados.Count == 1)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                                    else if (indicesEvaluados.Count == 2)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                                    else
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            stuff.error("Semántico", "'DECLARACION ARREGLO GLOBAL', la expresión de arreglo no es de la misma cantidad de dimensiones que la declaración. Encontrado: " + ((Arreglo)arr).dims + ", Esperado: '" + indicesEvaluados.Count + "'.", fila, columna, ctx);
                                            if (indicesEvaluados.Count == 1)
                                                resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                            else if (indicesEvaluados.Count == 2)
                                                resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                            else
                                                resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                        }
                                    }
                                    else
                                    {
                                        stuff.error("Semántico", "'DECLARACION ARREGLO GLOBAL', la expresión de declaración es del tipo incorrecto. Encontrado: " + Operacion.getTipo(arr) + ", Esperado: 'ARREGLO'.", fila, columna, ctx);
                                        if (indicesEvaluados.Count == 1)
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                        else if (indicesEvaluados.Count == 2)
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                        else
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                    }
                                }
                            }
                            s = new Simbolo(id, resOp);
                            ctx.globales.Add(s);
                        }
                    }
                    return null;
                }
            }
            if (tipo == Declaracion.Tipo.ATRIBUTO)
            {
                if (arreglo == null)
                {
                    bool evaluado = false;
                    object resOp = null;
                    foreach (string id in ids)
                    {
                        Simbolo s = ctx.findAttribute(id);
                        if (s != null)
                        {
                            stuff.error("Semántico", "'DECLARACION ARREGLO ATRIBUTO', la variable '" + id + "' ya existe.", fila, columna, ctx);
                        }
                        else
                        {
                            if (!evaluado)
                            {
                                evaluado = true;
                                List<object> indicesEvaluados = new List<object>();
                                bool error = false;
                                foreach (Instruccion i in indices)
                                {
                                    object e = Operacion.Validar(i.ejecutar(ctx, stuff), ctx, stuff, i.getLinea(), i.getColumna());
                                    if (e == null)
                                    {
                                        error = true;
                                        break;
                                    }
                                    if (e is BigInteger)
                                    {
                                        indicesEvaluados.Add(e);
                                    }
                                    else
                                    {
                                        error = true;
                                        stuff.error("Semántico", "'DECLARACION ARREGLO ATRIBUTO', una de las expresiones de la declaración de índices es del tipo incorrecto. Encontrado: " + Operacion.getTipo(e) + ", Esperado: 'ENTERO'.", fila, columna, ctx);
                                        break;
                                    }
                                }
                                if (error)
                                {
                                    resOp = Simbolo.NULL;
                                }
                                else
                                {
                                    if (indicesEvaluados.Count == 1)
                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                    else if (indicesEvaluados.Count == 2)
                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                    else
                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
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
                    bool evaluado = false;
                    object resOp = null;
                    foreach (string id in ids)
                    {
                        Simbolo s = ctx.findAttribute(id);
                        if (s != null)
                        {
                            stuff.error("Semántico", "'DECLARACION ARREGLO ATRIBUTO', la variable '" + id + "' ya existe.", fila, columna, ctx);
                        }
                        else
                        {
                            if (!evaluado)
                            {
                                evaluado = true;
                                List<object> indicesEvaluados = new List<object>();
                                bool error = false;
                                foreach (Instruccion i in indices)
                                {
                                    object e = Operacion.Validar(i.ejecutar(ctx, stuff), ctx, stuff, i.getLinea(), i.getColumna());
                                    if (e == null)
                                    {
                                        error = true;
                                        break;
                                    }
                                    if (e is BigInteger)
                                    {
                                        indicesEvaluados.Add(e);
                                    }
                                    else
                                    {
                                        error = true;
                                        stuff.error("Semántico", "'DECLARACION ARREGLO ATRIBUTO', una de las expresiones de la declaración de índices es del tipo incorrecto. Encontrado: " + Operacion.getTipo(e) + ", Esperado: 'ENTERO'.", fila, columna, ctx);
                                        break;
                                    }
                                }
                                if (error)
                                {
                                    resOp = Simbolo.NULL;
                                }
                                else
                                {
                                    object arr = Operacion.Validar(arreglo.ejecutar(ctx, stuff), ctx, stuff, arreglo.getLinea(), arreglo.getColumna());
                                    if (arr == null)
                                    {
                                        if (indicesEvaluados.Count == 1)
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                        else if (indicesEvaluados.Count == 2)
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                        else
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                    }
                                    else if(arr is Arreglo)
                                    {
                                        if (indicesEvaluados.Count == ((Arreglo)arr).dims)
                                        {
                                            if (indicesEvaluados.Count == 1)
                                            {
                                                if ((int)(BigInteger)indicesEvaluados.ElementAt(0) == (int)(BigInteger)((Arreglo)arr).val.Count)
                                                    resOp = arr;
                                                else
                                                {
                                                    stuff.error("Semántico", "'DECLARACION ARREGLO ATRIBUTO', la expresión de arreglo no tiene el tamaño adecuado dictado por su declaración. Encontrado: '[" + (int)(BigInteger)((Arreglo)arr).val.Count + "]', Esperado: '[" + (int)(BigInteger)indicesEvaluados.ElementAt(0) + "]'.", fila, columna, ctx);
                                                    if (indicesEvaluados.Count == 1)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                                    else if (indicesEvaluados.Count == 2)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                                    else
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                                }
                                            }
                                            else if (indicesEvaluados.Count == 2)
                                            {
                                                if ((int)(BigInteger)indicesEvaluados.ElementAt(0) == (int)(BigInteger)((Arreglo)arr).val.Count
                                                    && (int)(BigInteger)indicesEvaluados.ElementAt(1) == (int)(BigInteger)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.Count)
                                                    resOp = arr;
                                                else
                                                {
                                                    stuff.error("Semántico", "'DECLARACION ARREGLO ATRIBUTO', la expresión de arreglo no tiene el tamaño adecuado dictado por su declaración. Encontrado: '[" + (int)(BigInteger)((Arreglo)arr).val.Count + "][" + (int)(BigInteger)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.Count + "]', Esperado: '[" + (int)(BigInteger)indicesEvaluados.ElementAt(0) + "][" + (int)(BigInteger)indicesEvaluados.ElementAt(1) + "]'.", fila, columna, ctx);
                                                    if (indicesEvaluados.Count == 1)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                                    else if (indicesEvaluados.Count == 2)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                                    else
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                                }
                                            }
                                            else
                                            {
                                                if ((int)(BigInteger)indicesEvaluados.ElementAt(0) == (int)(BigInteger)((Arreglo)arr).val.Count
                                                    && (int)(BigInteger)indicesEvaluados.ElementAt(1) == (int)(BigInteger)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.Count
                                                    && (int)(BigInteger)indicesEvaluados.ElementAt(2) == (int)(BigInteger)((Arreglo)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.ElementAt(0)).val.Count)
                                                    resOp = arr;
                                                else
                                                {
                                                    stuff.error("Semántico", "'DECLARACION ARREGLO ATRIBUTO', la expresión de arreglo no tiene el tamaño adecuado dictado por su declaración. Encontrado: '[" + (int)(BigInteger)((Arreglo)arr).val.Count + "][" + (int)(BigInteger)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.Count + "][" + (int)(BigInteger)((Arreglo)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.ElementAt(0)).val.Count + "]', Esperado: '[" + (int)(BigInteger)indicesEvaluados.ElementAt(0) + "][" + (int)(BigInteger)indicesEvaluados.ElementAt(1) + "][" + (int)(BigInteger)indicesEvaluados.ElementAt(2) + "]'.", fila, columna, ctx);
                                                    if (indicesEvaluados.Count == 1)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                                    else if (indicesEvaluados.Count == 2)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                                    else
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            stuff.error("Semántico", "'DECLARACION ARREGLO ATRIBUTO', la expresión de arreglo no es de la misma cantidad de dimensiones que la declaración. Encontrado: " + ((Arreglo)arr).dims + ", Esperado: '" + indicesEvaluados.Count + "'.", fila, columna, ctx);
                                            if (indicesEvaluados.Count == 1)
                                                resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                            else if (indicesEvaluados.Count == 2)
                                                resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                            else
                                                resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                        }
                                    }
                                    else
                                    {
                                        stuff.error("Semántico", "'DECLARACION ARREGLO ATRIBUTO', la expresión de declaración es del tipo incorrecto. Encontrado: " + Operacion.getTipo(arr) + ", Esperado: 'ARREGLO'.", fila, columna, ctx);
                                        if (indicesEvaluados.Count == 1)
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                        else if (indicesEvaluados.Count == 2)
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                        else
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                    }
                                }
                            }
                            s = new Simbolo(id, resOp);
                            ctx.atributos.Add(s);
                        }
                    }
                    return null;
                }
            }
            else
            {
                if (arreglo == null)
                {
                    bool evaluado = false;
                    object resOp = null;
                    foreach (string id in ids)
                    {
                        Simbolo s = ctx.findLocalSame(id);
                        if (s != null)
                        {
                            stuff.error("Semántico", "'DECLARACION ARREGLO', la variable '" + id + "' ya existe.", fila, columna, ctx);
                        }
                        else
                        {
                            if (!evaluado)
                            {
                                evaluado = true;
                                List<object> indicesEvaluados = new List<object>();
                                bool error = false;
                                foreach (Instruccion i in indices)
                                {
                                    object e = Operacion.Validar(i.ejecutar(ctx, stuff), ctx, stuff, i.getLinea(), i.getColumna());
                                    if (e == null)
                                    {
                                        error = true;
                                        break;
                                    }
                                    if (e is BigInteger)
                                    {
                                        indicesEvaluados.Add(e);
                                    }
                                    else
                                    {
                                        error = true;
                                        stuff.error("Semántico", "'DECLARACION ARREGLO', una de las expresiones de la declaración de índices es del tipo incorrecto. Encontrado: " + Operacion.getTipo(e) + ", Esperado: 'ENTERO'.", fila, columna, ctx);
                                        break;
                                    }
                                }
                                if (error)
                                {
                                    resOp = Simbolo.NULL;
                                }
                                else
                                {
                                    if (indicesEvaluados.Count == 1)
                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                    else if (indicesEvaluados.Count == 2)
                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                    else
                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                }
                            }
                            s = new Simbolo(id, resOp);
                            ctx.locales_mismo_nivel.Add(s);
                            ctx.locales_cualquier_nivel.Insert(0, s);
                        }
                    }
                    return null;
                }
                else
                {
                    bool evaluado = false;
                    object resOp = null;
                    foreach (string id in ids)
                    {
                        Simbolo s = ctx.findLocalSame(id);
                        if (s != null)
                        {
                            stuff.error("Semántico", "'DECLARACION ARREGLO', la variable '" + id + "' ya existe.", fila, columna, ctx);
                        }
                        else
                        {
                            if (!evaluado)
                            {
                                evaluado = true;
                                List<object> indicesEvaluados = new List<object>();
                                bool error = false;
                                foreach (Instruccion i in indices)
                                {
                                    object e = Operacion.Validar(i.ejecutar(ctx, stuff), ctx, stuff, i.getLinea(), i.getColumna());
                                    if (e == null)
                                    {
                                        error = true;
                                        break;
                                    }
                                    if (e is BigInteger)
                                    {
                                        indicesEvaluados.Add(e);
                                    }
                                    else
                                    {
                                        error = true;
                                        stuff.error("Semántico", "'DECLARACION ARREGLO', una de las expresiones de la declaración de índices es del tipo incorrecto. Encontrado: " + Operacion.getTipo(e) + ", Esperado: 'ENTERO'.", fila, columna, ctx);
                                        break;
                                    }
                                }
                                if (error)
                                {
                                    resOp = Simbolo.NULL;
                                }
                                else
                                {
                                    object arr = Operacion.Validar(arreglo.ejecutar(ctx, stuff), ctx, stuff, arreglo.getLinea(), arreglo.getColumna());
                                    if (arr == null)
                                    {
                                        if (indicesEvaluados.Count == 1)
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                        else if (indicesEvaluados.Count == 2)
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                        else
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                    }
                                    else if(arr is Arreglo)
                                    {
                                        if (indicesEvaluados.Count == ((Arreglo)arr).dims)
                                        {
                                            if (indicesEvaluados.Count == 1)
                                            {
                                                if ((int)(BigInteger)indicesEvaluados.ElementAt(0) == (int)(BigInteger)((Arreglo)arr).val.Count)
                                                    resOp = arr;
                                                else
                                                {
                                                    stuff.error("Semántico", "'DECLARACION ARREGLO', la expresión de arreglo no tiene el tamaño adecuado dictado por su declaración. Encontrado: '[" + (int)(BigInteger)((Arreglo)arr).val.Count + "]', Esperado: '[" + (int)(BigInteger)indicesEvaluados.ElementAt(0) + "]'.", fila, columna, ctx);
                                                    if (indicesEvaluados.Count == 1)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                                    else if (indicesEvaluados.Count == 2)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                                    else
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                                }
                                            }
                                            else if (indicesEvaluados.Count == 2)
                                            {
                                                if ((int)(BigInteger)indicesEvaluados.ElementAt(0) == (int)(BigInteger)((Arreglo)arr).val.Count
                                                    && (int)(BigInteger)indicesEvaluados.ElementAt(1) == (int)(BigInteger)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.Count)
                                                    resOp = arr;
                                                else
                                                {
                                                    stuff.error("Semántico", "'DECLARACION ARREGLO', la expresión de arreglo no tiene el tamaño adecuado dictado por su declaración. Encontrado: '[" + (int)(BigInteger)((Arreglo)arr).val.Count + "][" + (int)(BigInteger)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.Count + "]', Esperado: '[" + (int)(BigInteger)indicesEvaluados.ElementAt(0) + "][" + (int)(BigInteger)indicesEvaluados.ElementAt(1) + "]'.", fila, columna, ctx);
                                                    if (indicesEvaluados.Count == 1)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                                    else if (indicesEvaluados.Count == 2)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                                    else
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                                }
                                            }
                                            else
                                            {
                                                if ((int)(BigInteger)indicesEvaluados.ElementAt(0) == (int)(BigInteger)((Arreglo)arr).val.Count
                                                    && (int)(BigInteger)indicesEvaluados.ElementAt(1) == (int)(BigInteger)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.Count
                                                    && (int)(BigInteger)indicesEvaluados.ElementAt(2) == (int)(BigInteger)((Arreglo)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.ElementAt(0)).val.Count)
                                                    resOp = arr;
                                                else
                                                {
                                                    stuff.error("Semántico", "'DECLARACION ARREGLO', la expresión de arreglo no tiene el tamaño adecuado dictado por su declaración. Encontrado: '[" + (int)(BigInteger)((Arreglo)arr).val.Count + "][" + (int)(BigInteger)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.Count + "][" + (int)(BigInteger)((Arreglo)((Arreglo)((Arreglo)arr).val.ElementAt(0)).val.ElementAt(0)).val.Count + "]', Esperado: '[" + (int)(BigInteger)indicesEvaluados.ElementAt(0) + "][" + (int)(BigInteger)indicesEvaluados.ElementAt(1) + "][" + (int)(BigInteger)indicesEvaluados.ElementAt(2) + "]'.", fila, columna, ctx);
                                                    if (indicesEvaluados.Count == 1)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                                    else if (indicesEvaluados.Count == 2)
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                                    else
                                                        resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            stuff.error("Semántico", "'DECLARACION ARREGLO', la expresión de arreglo no es de la misma cantidad de dimensiones que la declaración. Encontrado: " + ((Arreglo)arr).dims + ", Esperado: '" + indicesEvaluados.Count + "'.", fila, columna, ctx);
                                            if (indicesEvaluados.Count == 1)
                                                resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                            else if (indicesEvaluados.Count == 2)
                                                resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                            else
                                                resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                        }
                                    }
                                    else
                                    {
                                        stuff.error("Semántico", "'DECLARACION ARREGLO', la expresión de declaración es del tipo incorrecto. Encontrado: " + Operacion.getTipo(arr) + ", Esperado: 'ARREGLO'.", fila, columna, ctx);
                                        if (indicesEvaluados.Count == 1)
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0));
                                        else if (indicesEvaluados.Count == 2)
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1));
                                        else
                                            resOp = new Arreglo((int)(BigInteger)indicesEvaluados.ElementAt(0), (int)(BigInteger)indicesEvaluados.ElementAt(1), (int)(BigInteger)indicesEvaluados.ElementAt(2));
                                    }
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
