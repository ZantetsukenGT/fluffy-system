using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class Operacion : Instruccion
    {
        public enum Tipo
        {
            //primitivos valores_literales
            BOOLEANO,//
            CADENA,//
            CHAR,//
            DOUBLE,//
            ENTERO,//
            IDENTIFICADOR,//
            NULL,//
            EXPRESIONARRAY1,//
            EXPRESIONARRAY2,//
            EXPRESIONARRAY3,//
            //operaciones unarias
            NEGATIVO,//
            NOT,//
            //operaciones binarias
            SUMA,//
            RESTA,//
            MULTIPLICACION,//
            DIVISION,//
            POTENCIA,//
            IGUALQUE,//
            SWITCHIGUALQUE,//
            DISTINTOQUE,//
            MAYORQUE,//
            MAYORIGUALQUE,//
            MENORQUE,//
            MENORIGUALQUE,//
            AND,//short-circuit
            OR,//short-circuit
            XOR//
        };

        public Object value;
        public Tipo tipo;

        private Instruccion op1;
        private Instruccion op2;
        public int fila1, fila2, columna1, columna2;

        /// <summary>
        /// solo utilizado para construir las hojas del arbol, tipos de dato minimo
        /// </summary>
        public Operacion(Object value, Tipo tipo, int fila1, int columna1)
        {
            this.value = value;
            this.tipo = tipo;
            this.op1 = null;
            this.op2 = null;

            this.fila1 = fila1;
            this.columna1 = columna1;
            this.fila2 = fila1;
            this.columna2 = columna1;
        }

        /// <summary>
        /// utilizado para construir las ramas del arbol con operaciones unarias
        /// </summary>
        public Operacion(Instruccion op1, Tipo tipo, int fila1, int columna1)
        {
            this.value = null;
            this.tipo = tipo;
            this.op1 = op1;
            this.op2 = null;

            this.fila1 = fila1;
            this.columna1 = columna1;
            this.fila2 = fila1;
            this.columna2 = columna1;
        }

        /// <summary>
        /// utilizado para construir las ramas del arbol con operaciones binarias
        /// </summary>
        public Operacion(Instruccion op1, Instruccion op2, Tipo tipo)
        {
            this.value = null;
            this.tipo = tipo;
            this.op1 = op1;
            this.op2 = op2;

            this.fila1 = op1.getLinea();
            this.columna1 = op1.getColumna();
            this.fila2 = op2.getLinea();
            this.columna2 = op2.getColumna();
        }

        /// <summary>
        /// utilizado para switch
        /// </summary>
        public Operacion(object value, Instruccion op1, Tipo tipo, int fila1, int columna1, int fila2, int columna2)
        {
            this.value = value;
            this.tipo = tipo;
            this.op1 = op1;
            this.op2 = null;

            this.fila1 = fila1;
            this.columna1 = columna1;
            this.fila2 = fila2;
            this.columna2 = columna2;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            Object resOp1;
            Object resOp2;
            Simbolo s;
            switch (tipo)
            {
                case Tipo.BOOLEANO:
                    {
                        return value;
                    }
                case Tipo.CADENA:
                    {
                        return value;
                    }
                case Tipo.CHAR:
                    {
                        return value;
                    }
                case Tipo.DOUBLE:
                    {
                        return value;
                    }
                case Tipo.ENTERO:
                    {
                        return value;
                    }
                case Tipo.IDENTIFICADOR:
                    {
                        s = ctx.findSymbol(value.ToString());
                        if (s == null)
                        {
                            stuff.error("Semántico", "'EXPRESION', la variable '" + value.ToString() + "' no existe.", fila2, columna2, ctx);
                            return null;
                        }
                        return s.value;
                    }
                case Tipo.NULL:
                    {
                        return value;
                    }
                case Tipo.EXPRESIONARRAY1:
                    {
                        List<object> dim1 = (List<object>)value;
                        List<object> dim1_evaluada = new List<object>();
                        for (int i = 0; i < dim1.Count; i++)
                        {
                            object resOp = Validar(((Instruccion)dim1.ElementAt(i)).ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                            if(resOp == null)
                            {
                                resOp = Simbolo.NULL;
                            }
                            dim1_evaluada.Add(resOp);
                        }
                        Arreglo arr1 = new Arreglo(dim1_evaluada);
                        arr1.dims = 1;
                        return arr1;
                    }
                case Tipo.EXPRESIONARRAY2:
                    {
                        List<object> dim2 = (List<object>)value;
                        List<object> dim2_evaluada = new List<object>();
                        for (int i = 0; i < dim2.Count; i++)
                        {
                            List<object> dim1 = (List<object>)dim2.ElementAt(i);
                            List<object> dim1_evaluada = new List<object>();
                            for (int j = 0; j < dim1.Count; j++)
                            {
                                object resOp = Validar(((Instruccion)dim1.ElementAt(j)).ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                                if (resOp == null)
                                {
                                    resOp = Simbolo.NULL;
                                }
                                dim1_evaluada.Add(resOp);
                            }
                            Arreglo arr1 = new Arreglo(dim1_evaluada);
                            arr1.dims = 1;
                            dim2_evaluada.Add(arr1);
                        }
                        Arreglo arr2 = new Arreglo(dim2_evaluada);
                        arr2.dims = 2;
                        for (int i = 0,j = ((Arreglo)arr2.val.ElementAt(0)).val.Count; i < arr2.val.Count; i++)
                        {
                            if(((Arreglo)arr2.val.ElementAt(i)).val.Count != j)
                            {
                                stuff.error("Semántico", "'EXPRESION ARREGLO 2 DIM', las dimensiones no tienen la misma cantidad de elementos", fila2, columna2, ctx);
                                return Simbolo.NULL;
                            }
                        }
                        return arr2;
                    }
                case Tipo.EXPRESIONARRAY3:
                    {
                        List<object> dim3 = (List<object>)value;
                        List<object> dim3_evaluada = new List<object>();
                        for (int i = 0; i < dim3.Count; i++)
                        {
                            List<object> dim2 = (List<object>)dim3.ElementAt(i);
                            List<object> dim2_evaluada = new List<object>();
                            for (int j = 0; j < dim2.Count; j++)
                            {
                                List<object> dim1 = (List<object>)dim2.ElementAt(j);
                                List<object> dim1_evaluada = new List<object>();
                                for (int k = 0; k < dim1.Count; k++)
                                {
                                    object resOp = Validar(((Instruccion)dim1.ElementAt(k)).ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                                    if (resOp == null)
                                    {
                                        resOp = Simbolo.NULL;
                                    }
                                    dim1_evaluada.Add(resOp);
                                }
                                Arreglo arr1 = new Arreglo(dim1_evaluada);
                                arr1.dims = 1;
                                dim2_evaluada.Add(arr1);
                            }
                            Arreglo arr2 = new Arreglo(dim2_evaluada);
                            arr2.dims = 2;
                            dim3_evaluada.Add(arr2);
                        }
                        Arreglo arr3 = new Arreglo(dim3_evaluada);
                        arr3.dims = 3;
                        for (int i = 0, j = ((Arreglo)arr3.val.ElementAt(0)).val.Count, l = ((Arreglo)((Arreglo)arr3.val.ElementAt(0)).val.ElementAt(0)).val.Count; i < arr3.val.Count; i++)
                        {
                            if(((Arreglo)arr3.val.ElementAt(i)).val.Count != j)
                            {
                                stuff.error("Semántico", "'EXPRESION ARREGLO 3 DIM', las dimensiones no tienen la misma cantidad de elementos", fila2, columna2, ctx);
                                return Simbolo.NULL;
                            }
                            for (int k = 0; k < ((Arreglo)arr3.val.ElementAt(i)).val.Count; k++)
                            {
                                if (((Arreglo)((Arreglo)arr3.val.ElementAt(i)).val.ElementAt(k)).val.Count != l)
                                {
                                    stuff.error("Semántico", "'EXPRESION ARREGLO 3 DIM', las dimensiones no tienen la misma cantidad de elementos", fila2, columna2, ctx);
                                    return Simbolo.NULL;
                                }
                            }
                        }
                        return arr3;
                    }
                case Tipo.NEGATIVO:
                    {
                        resOp1 = ValidarMinimo(op1.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp1 == null)
                        {
                            return null;
                        }

                        if (resOp1 is BigInteger)
                        {
                            return -((BigInteger)resOp1);
                        }
                        if (resOp1 is double)
                        {
                            return -((double)resOp1);
                        }
                        if (resOp1 is char)
                        {
                            return -((BigInteger)(char)resOp1);
                        }
                        stuff.error("Semántico", "'EXPRESION' @ 'NEGATIVO', el operando es del tipo incorrecto. Encontrado: " + getTipo(resOp1) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR'.", fila2, columna2, ctx);
                        return null;
                    }
                case Tipo.NOT:
                    {
                        resOp1 = ValidarMinimo(op1.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp1 == null)
                        {
                            return null;
                        }

                        if (resOp1 is bool)
                        {
                            return !((bool)resOp1);
                        }
                        stuff.error("Semántico", "'EXPRESION' @ 'NOT', el operando es del tipo incorrecto. Encontrado: " + getTipo(resOp1) + ", Esperado: 'BOOLEANO'", fila2, columna2, ctx);
                        return null;
                    }
                case Tipo.SUMA:
                    {
                        resOp1 = ValidarMinimo(op1.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp1 == null)
                        {
                            return null;
                        }
                        resOp2 = ValidarMinimo(op2.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp2 == null)
                        {
                            return null;
                        }

                        if (resOp1 is string && resOp2 is bool)
                        {
                            return (string)resOp1 + (bool)resOp2;
                        }
                        if (resOp1 is string && resOp2 is char)
                        {
                            return (string)resOp1 + (char)resOp2;
                        }
                        if (resOp1 is string && resOp2 is double)
                        {
                            return (string)resOp1 + (double)resOp2;
                        }
                        if (resOp1 is string && resOp2 is BigInteger)
                        {
                            return (string)resOp1 + (BigInteger)resOp2;
                        }
                        if (resOp1 is string && resOp2 is string)
                        {
                            return (string)resOp1 + (string)resOp2;
                        }
                        if (resOp1 is string && resOp2 is Null)
                        {
                            return (string)resOp1 + "NULL";
                        }

                        if (resOp1 is bool && resOp2 is string)
                        {
                            return (bool)resOp1 + (string)resOp2;
                        }
                        if (resOp1 is char && resOp2 is string)
                        {
                            return (char)resOp1 + (string)resOp2;
                        }
                        if (resOp1 is double && resOp2 is string)
                        {
                            return (double)resOp1 + (string)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is string)
                        {
                            return (BigInteger)resOp1 + (string)resOp2;
                        }
                        if (resOp1 is Null && resOp2 is string)
                        {
                            return "NULL" + (string)resOp2;
                        }

                        if (resOp1 is double && resOp2 is double)
                        {
                            return (double)resOp1 + (double)resOp2;
                        }
                        if (resOp1 is double && resOp2 is BigInteger)
                        {
                            return (double)resOp1 + (double)(BigInteger)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is double)
                        {
                            return (double)(BigInteger)resOp1 + (double)resOp2;
                        }
                        if (resOp1 is double && resOp2 is char)
                        {
                            return (double)resOp1 + (char)resOp2;
                        }
                        if (resOp1 is char && resOp2 is double)
                        {
                            return (char)resOp1 + (double)resOp2;
                        }

                        if (resOp1 is BigInteger && resOp2 is BigInteger)
                        {
                            return (BigInteger)resOp1 + (BigInteger)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is char)
                        {
                            return (BigInteger)resOp1 + (char)resOp2;
                        }
                        if (resOp1 is char && resOp2 is BigInteger)
                        {
                            return (char)resOp1 + (BigInteger)resOp2;
                        }
                        if (resOp1 is char && resOp2 is char)
                        {
                            return (BigInteger)((char)resOp1 + (char)resOp2);
                        }
                        stuff.error("Semántico", "'EXPRESION' @ 'SUMA', uno de los operandos es incompatible con el otro. Encontrado: " + getTipo(resOp1) + " + " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                        return null;
                    }
                case Tipo.RESTA:
                    {
                        resOp1 = ValidarMinimo(op1.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp1 == null)
                        {
                            return null;
                        }
                        resOp2 = ValidarMinimo(op2.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp2 == null)
                        {
                            return null;
                        }

                        if (resOp1 is double && resOp2 is double)
                        {
                            return (double)resOp1 - (double)resOp2;
                        }
                        if (resOp1 is double && resOp2 is BigInteger)
                        {
                            return (double)resOp1 - (double)(BigInteger)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is double)
                        {
                            return (double)(BigInteger)resOp1 - (double)resOp2;
                        }
                        if (resOp1 is double && resOp2 is char)
                        {
                            return (double)resOp1 - (char)resOp2;
                        }
                        if (resOp1 is char && resOp2 is double)
                        {
                            return (char)resOp1 - (double)resOp2;
                        }

                        if (resOp1 is BigInteger && resOp2 is BigInteger)
                        {
                            return (BigInteger)resOp1 - (BigInteger)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is char)
                        {
                            return (BigInteger)resOp1 - (char)resOp2;
                        }
                        if (resOp1 is char && resOp2 is BigInteger)
                        {
                            return (char)resOp1 - (BigInteger)resOp2;
                        }
                        if (resOp1 is char && resOp2 is char)
                        {
                            return (BigInteger)((char)resOp1 - (char)resOp2);
                        }
                        stuff.error("Semántico", "'EXPRESION' @ 'RESTA', uno de los operandos es incompatible con el otro. Encontrado: " + getTipo(resOp1) + " - " + getTipo(resOp2) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR'.", fila2, columna2, ctx);
                        return null;
                    }
                case Tipo.MULTIPLICACION:
                    {
                        resOp1 = ValidarMinimo(op1.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp1 == null)
                        {
                            return null;
                        }
                        resOp2 = ValidarMinimo(op2.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp2 == null)
                        {
                            return null;
                        }

                        if (resOp1 is double && resOp2 is double)
                        {
                            return (double)resOp1 * (double)resOp2;
                        }
                        if (resOp1 is double && resOp2 is BigInteger)
                        {
                            return (double)resOp1 * (double)(BigInteger)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is double)
                        {
                            return (double)(BigInteger)resOp1 * (double)resOp2;
                        }
                        if (resOp1 is double && resOp2 is char)
                        {
                            return (double)resOp1 * (char)resOp2;
                        }
                        if (resOp1 is char && resOp2 is double)
                        {
                            return (char)resOp1 * (double)resOp2;
                        }

                        if (resOp1 is BigInteger && resOp2 is BigInteger)
                        {
                            return (BigInteger)resOp1 * (BigInteger)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is char)
                        {
                            return (BigInteger)resOp1 * (char)resOp2;
                        }
                        if (resOp1 is char && resOp2 is BigInteger)
                        {
                            return (char)resOp1 * (BigInteger)resOp2;
                        }
                        if (resOp1 is char && resOp2 is char)
                        {
                            return (BigInteger)((char)resOp1 * (char)resOp2);
                        }
                        stuff.error("Semántico", "'EXPRESION' @ 'MULTIPLICACION', uno de los operandos es incompatible con el otro. Encontrado: " + getTipo(resOp1) + " * " + getTipo(resOp2) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR'.", fila2, columna2, ctx);
                        return null;
                    }
                case Tipo.DIVISION:
                    {
                        resOp1 = ValidarMinimo(op1.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp1 == null)
                        {
                            return null;
                        }
                        resOp2 = ValidarMinimo(op2.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp2 == null)
                        {
                            return null;
                        }

                        if (resOp1 is double && resOp2 is double)
                        {
                            if (Math.Abs((double)resOp2) < 0.00001)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'DIVISION', segundo parámetro es 0. Encontrado: " + getTipo(resOp1) + " / " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return (double)resOp1 / (double)resOp2;
                        }
                        if (resOp1 is double && resOp2 is BigInteger)
                        {
                            if ((BigInteger)resOp2 == 0)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'DIVISION', segundo parámetro es 0. Encontrado: " + getTipo(resOp1) + " / " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return (double)resOp1 / (double)(BigInteger)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is double)
                        {
                            if (Math.Abs((double)resOp2) < 0.00001)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'DIVISION', segundo parámetro es 0. Encontrado: " + getTipo(resOp1) + " / " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return (double)(BigInteger)resOp1 / (double)resOp2;
                        }
                        if (resOp1 is double && resOp2 is char)
                        {
                            if ((char)resOp2 == 0)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'DIVISION', segundo parámetro es 0. Encontrado: " + getTipo(resOp1) + " / " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return (double)resOp1 / (char)resOp2;
                        }
                        if (resOp1 is char && resOp2 is double)
                        {
                            if (Math.Abs((double)resOp2) < 0.00001)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'DIVISION', segundo parámetro es 0. Encontrado: " + getTipo(resOp1) + " / " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return (char)resOp1 / (double)resOp2;
                        }

                        if (resOp1 is BigInteger && resOp2 is BigInteger)
                        {
                            if ((BigInteger)resOp2 == 0)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'DIVISION', segundo parámetro es 0. Encontrado: " + getTipo(resOp1) + " / " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return (BigInteger)resOp1 / (BigInteger)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is char)
                        {
                            if ((char)resOp2 == 0)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'DIVISION', segundo parámetro es 0. Encontrado: " + getTipo(resOp1) + " / " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return (BigInteger)resOp1 / (char)resOp2;
                        }
                        if (resOp1 is char && resOp2 is BigInteger)
                        {
                            if ((BigInteger)resOp2 == 0)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'DIVISION', segundo parámetro es 0. Encontrado: " + getTipo(resOp1) + " / " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return (char)resOp1 / (BigInteger)resOp2;
                        }
                        if (resOp1 is char && resOp2 is char)
                        {
                            if ((char)resOp2 == 0)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'DIVISION', segundo parámetro es 0. Encontrado: " + getTipo(resOp1) + " / " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return (BigInteger)((char)resOp1 / (char)resOp2);
                        }
                        stuff.error("Semántico", "'EXPRESION' @ 'DIVISION', uno de los operandos es incompatible con el otro. Encontrado: " + getTipo(resOp1) + " / " + getTipo(resOp2) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR'.", fila2, columna2, ctx);
                        return null;
                    }
                case Tipo.POTENCIA:
                    {
                        resOp1 = ValidarMinimo(op1.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp1 == null)
                        {
                            return null;
                        }
                        resOp2 = ValidarMinimo(op2.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp2 == null)
                        {
                            return null;
                        }

                        if (resOp1 is double && resOp2 is double)
                        {
                            if (Math.Abs((double)resOp1) < 0.00001 && Math.Abs((double)resOp2) < 0.00001)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'POTENCIA', ambos parametros son 0. Encontrado: " + getTipo(resOp1) + " pow " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return Math.Pow((double)resOp1, (double)resOp2);
                        }
                        if (resOp1 is double && resOp2 is BigInteger)
                        {
                            if (Math.Abs((double)resOp1) < 0.00001 && (BigInteger)resOp2 == 0)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'POTENCIA', ambos parametros son 0. Encontrado: " + getTipo(resOp1) + " pow " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return Math.Pow((double)resOp1, (double)(BigInteger)resOp2);
                        }
                        if (resOp1 is BigInteger && resOp2 is double)
                        {
                            if ((BigInteger)resOp1 == 0 && Math.Abs((double)resOp2) < 0.00001)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'POTENCIA', ambos parametros son 0. Encontrado: " + getTipo(resOp1) + " pow " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return Math.Pow((double)(BigInteger)resOp1, (double)resOp2);
                        }
                        if (resOp1 is double && resOp2 is char)
                        {
                            if (Math.Abs((double)resOp1) < 0.00001 && (char)resOp2 == 0)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'POTENCIA', ambos parametros son 0. Encontrado: " + getTipo(resOp1) + " pow " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return Math.Pow((double)resOp1, (char)resOp2);
                        }
                        if (resOp1 is char && resOp2 is double)
                        {
                            if ((char)resOp1 == 0 && Math.Abs((double)resOp2) < 0.00001)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'POTENCIA', ambos parametros son 0. Encontrado: " + getTipo(resOp1) + " pow " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return Math.Pow((char)resOp1, (double)resOp2);
                        }

                        if (resOp1 is BigInteger && resOp2 is BigInteger)
                        {
                            if ((BigInteger)resOp1 == 0 && (BigInteger)resOp2 == 0)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'POTENCIA', ambos parametros son 0. Encontrado: " + getTipo(resOp1) + " pow " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return Math.Pow((double)(BigInteger)resOp1, (double)(BigInteger)resOp2);
                        }
                        if (resOp1 is BigInteger && resOp2 is char)
                        {
                            if ((BigInteger)resOp1 == 0 && (char)resOp2 == 0)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'POTENCIA', ambos parametros son 0. Encontrado: " + getTipo(resOp1) + " pow " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return Math.Pow((double)(BigInteger)resOp1, (char)resOp2);
                        }
                        if (resOp1 is char && resOp2 is BigInteger)
                        {
                            if ((char)resOp1 == 0 && (BigInteger)resOp2 == 0)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'POTENCIA', ambos parametros son 0. Encontrado: " + getTipo(resOp1) + " pow " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return Math.Pow((char)resOp1, (double)(BigInteger)resOp2);
                        }
                        if (resOp1 is char && resOp2 is char)
                        {
                            if ((char)resOp1 == 0 && (char)resOp2 == 0)
                            {
                                stuff.error("Semántico", "'EXPRESION' @ 'POTENCIA', ambos parametros son 0. Encontrado: " + getTipo(resOp1) + " pow " + getTipo(resOp2) + ".", fila2, columna2, ctx);
                                return null;
                            }
                            return Math.Pow((char)resOp1, (char)resOp2);
                        }
                        stuff.error("Semántico", "'EXPRESION' @ 'POTENCIA', uno de los operandos es incompatible con el otro. Encontrado: " + getTipo(resOp1) + " pow " + getTipo(resOp2) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR'.", fila2, columna2, ctx);
                        return null;
                    }
                case Tipo.IGUALQUE:
                    {
                        resOp1 = Validar(op1.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp1 == null)
                        {
                            return null;
                        }
                        resOp2 = Validar(op2.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp2 == null)
                        {
                            return null;
                        }

                        if (resOp1 is double && resOp2 is double)
                        {
                            return Math.Abs((double)resOp1 - (double)resOp2) < 0.00001;
                        }
                        if (resOp1 is double && resOp2 is BigInteger)
                        {
                            return Math.Abs((double)resOp1 - (double)(BigInteger)resOp2) < 0.00001;
                        }
                        if (resOp1 is BigInteger && resOp2 is double)
                        {
                            return Math.Abs((double)(BigInteger)resOp1 - (double)resOp2) < 0.00001;
                        }
                        if (resOp1 is double && resOp2 is char)
                        {
                            return Math.Abs((double)resOp1 - (char)resOp2) < 0.00001;
                        }
                        if (resOp1 is char && resOp2 is double)
                        {
                            return Math.Abs((char)resOp1 - (double)resOp2) < 0.00001;
                        }

                        if (resOp1 is BigInteger && resOp2 is BigInteger)
                        {
                            return (BigInteger)resOp1 == (BigInteger)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is char)
                        {
                            return (BigInteger)resOp1 == (char)resOp2;
                        }
                        if (resOp1 is char && resOp2 is BigInteger)
                        {
                            return (char)resOp1 == (BigInteger)resOp2;
                        }
                        if (resOp1 is char && resOp2 is char)
                        {
                            return (char)resOp1 == (char)resOp2;
                        }

                        if (resOp1 is string && resOp2 is string)
                        {
                            return ((string)resOp1).Equals((string)resOp2, StringComparison.InvariantCultureIgnoreCase);
                        }
                        if (resOp1 is bool && resOp2 is bool)
                        {
                            return (bool)resOp1 == (bool)resOp2;
                        }
                        if (resOp1 is Objeto && resOp2 is Objeto)
                        {
                            return (Objeto)resOp1 == (Objeto)resOp2;
                        }
                        if (resOp1 is Arreglo && resOp2 is Arreglo)
                        {
                            return (Arreglo)resOp1 == (Arreglo)resOp2;
                        }
                        if (resOp1 is Null || resOp2 is Null)
                        {
                            if (resOp1 is Null && resOp2 is Null)
                            {
                                return true;
                            }
                            return false;
                        }
                        stuff.error("Semántico", "'EXPRESION' @ 'IGUALQUE', uno de los operandos es incompatible con el otro. Encontrado: " + getTipo(resOp1) + " == " + getTipo(resOp2) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR', 'CADENA'.", fila2, columna2, ctx);
                        return null;
                    }
                case Tipo.SWITCHIGUALQUE:
                    {
                        resOp1 = Validar(value, ctx, stuff, fila2, columna2);
                        if (resOp1 == null)
                        {
                            return null;
                        }
                        resOp2 = Validar(op1.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp2 == null)
                        {
                            return null;
                        }

                        if (resOp1 is double && resOp2 is double)
                        {
                            return Math.Abs((double)resOp1 - (double)resOp2) < 0.00001;
                        }
                        if (resOp1 is double && resOp2 is BigInteger)
                        {
                            return Math.Abs((double)resOp1 - (double)(BigInteger)resOp2) < 0.00001;
                        }
                        if (resOp1 is BigInteger && resOp2 is double)
                        {
                            return Math.Abs((double)(BigInteger)resOp1 - (double)resOp2) < 0.00001;
                        }
                        if (resOp1 is double && resOp2 is char)
                        {
                            return Math.Abs((double)resOp1 - (char)resOp2) < 0.00001;
                        }
                        if (resOp1 is char && resOp2 is double)
                        {
                            return Math.Abs((char)resOp1 - (double)resOp2) < 0.00001;
                        }

                        if (resOp1 is BigInteger && resOp2 is BigInteger)
                        {
                            return (BigInteger)resOp1 == (BigInteger)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is char)
                        {
                            return (BigInteger)resOp1 == (char)resOp2;
                        }
                        if (resOp1 is char && resOp2 is BigInteger)
                        {
                            return (char)resOp1 == (BigInteger)resOp2;
                        }
                        if (resOp1 is char && resOp2 is char)
                        {
                            return (char)resOp1 == (char)resOp2;
                        }

                        if (resOp1 is string && resOp2 is string)
                        {
                            return ((string)resOp1).Equals((string)resOp2, StringComparison.InvariantCultureIgnoreCase);
                        }
                        if (resOp1 is bool && resOp2 is bool)
                        {
                            return (bool)resOp1 == (bool)resOp2;
                        }
                        if (resOp1 is Objeto && resOp2 is Objeto)
                        {
                            return (Objeto)resOp1 == (Objeto)resOp2;
                        }
                        if (resOp1 is Arreglo && resOp2 is Arreglo)
                        {
                            return (Arreglo)resOp1 == (Arreglo)resOp2;
                        }
                        if (resOp1 is Null || resOp2 is Null)
                        {
                            if(resOp1 is Null && resOp2 is Null)
                            {
                                return true;
                            }
                            return false;
                        }
                        stuff.error("Semántico", "'SWITCH CASE', un operando de la comprobacion de tipos es incompatible con el otro. Encontrado: " + getTipo(resOp1) + " == " + getTipo(resOp2) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR', 'CADENA'.", fila2, columna2, ctx);
                        return null;
                    }
                case Tipo.DISTINTOQUE:
                    {
                        resOp1 = Validar(op1.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp1 == null)
                        {
                            return null;
                        }
                        resOp2 = Validar(op2.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp2 == null)
                        {
                            return null;
                        }

                        if (resOp1 is double && resOp2 is double)
                        {
                            return Math.Abs((double)resOp1 - (double)resOp2) >= 0.00001;
                        }
                        if (resOp1 is double && resOp2 is BigInteger)
                        {
                            return Math.Abs((double)resOp1 - (double)(BigInteger)resOp2) >= 0.00001;
                        }
                        if (resOp1 is BigInteger && resOp2 is double)
                        {
                            return Math.Abs((double)(BigInteger)resOp1 - (double)resOp2) >= 0.00001;
                        }
                        if (resOp1 is double && resOp2 is char)
                        {
                            return Math.Abs((double)resOp1 - (char)resOp2) >= 0.00001;
                        }
                        if (resOp1 is char && resOp2 is double)
                        {
                            return Math.Abs((char)resOp1 - (double)resOp2) >= 0.00001;
                        }

                        if (resOp1 is BigInteger && resOp2 is BigInteger)
                        {
                            return (BigInteger)resOp1 != (BigInteger)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is char)
                        {
                            return (BigInteger)resOp1 != (char)resOp2;
                        }
                        if (resOp1 is char && resOp2 is BigInteger)
                        {
                            return (char)resOp1 != (BigInteger)resOp2;
                        }
                        if (resOp1 is char && resOp2 is char)
                        {
                            return (char)resOp1 != (char)resOp2;
                        }

                        if (resOp1 is string && resOp2 is string)
                        {
                            return ((string)resOp1).Equals((string)resOp2, StringComparison.InvariantCultureIgnoreCase);
                        }
                        if (resOp1 is bool && resOp2 is bool)
                        {
                            return (bool)resOp1 != (bool)resOp2;
                        }
                        if (resOp1 is Objeto && resOp2 is Objeto)
                        {
                            return (Objeto)resOp1 != (Objeto)resOp2;
                        }
                        if (resOp1 is Arreglo && resOp2 is Arreglo)
                        {
                            return (Arreglo)resOp1 != (Arreglo)resOp2;
                        }
                        if (resOp1 is Null || resOp2 is Null)
                        {
                            if (resOp1 is Null && resOp2 is Null)
                            {
                                return false;
                            }
                            return true;
                        }
                        stuff.error("Semántico", "'EXPRESION' @ 'DISTINTOQUE', uno de los operandos es incompatible con el otro. Encontrado: " + getTipo(resOp1) + " != " + getTipo(resOp2) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR', 'CADENA'.", fila2, columna2, ctx);
                        return null;
                    }
                case Tipo.MAYORQUE:
                    {
                        resOp1 = Validar(op1.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp1 == null)
                        {
                            return null;
                        }
                        resOp2 = Validar(op2.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp2 == null)
                        {
                            return null;
                        }

                        if (resOp1 is double && resOp2 is double)
                        {
                            return (double)resOp1 > (double)resOp2;
                        }
                        if (resOp1 is double && resOp2 is BigInteger)
                        {
                            return (double)resOp1 > (double)(BigInteger)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is double)
                        {
                            return (double)(BigInteger)resOp1 > (double)resOp2;
                        }
                        if (resOp1 is double && resOp2 is char)
                        {
                            return (double)resOp1 > (char)resOp2;
                        }
                        if (resOp1 is char && resOp2 is double)
                        {
                            return (char)resOp1 > (double)resOp2;
                        }

                        if (resOp1 is BigInteger && resOp2 is BigInteger)
                        {
                            return (BigInteger)resOp1 > (BigInteger)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is char)
                        {
                            return (BigInteger)resOp1 > (char)resOp2;
                        }
                        if (resOp1 is char && resOp2 is BigInteger)
                        {
                            return (char)resOp1 > (BigInteger)resOp2;
                        }
                        if (resOp1 is char && resOp2 is char)
                        {
                            return (char)resOp1 > (char)resOp2;
                        }
                        stuff.error("Semántico", "'EXPRESION' @ 'MAYORQUE', uno de los operandos es incompatible con el otro. Encontrado: " + getTipo(resOp1) + " > " + getTipo(resOp2) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR'.", fila2, columna2, ctx);
                        return null;
                    }
                case Tipo.MAYORIGUALQUE:
                    {
                        resOp1 = Validar(op1.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp1 == null)
                        {
                            return null;
                        }
                        resOp2 = Validar(op2.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp2 == null)
                        {
                            return null;
                        }

                        if (resOp1 is double && resOp2 is double)
                        {
                            return (double)resOp1 > (double)resOp2 || Math.Abs((double)resOp1 - (double)resOp2) < 0.00001;
                        }
                        if (resOp1 is double && resOp2 is BigInteger)
                        {
                            return (double)resOp1 > (double)(BigInteger)resOp2 || Math.Abs((double)resOp1 - (double)(BigInteger)resOp2) < 0.00001;
                        }
                        if (resOp1 is BigInteger && resOp2 is double)
                        {
                            return (double)(BigInteger)resOp1 > (double)resOp2 || Math.Abs((double)(BigInteger)resOp1 - (double)resOp2) < 0.00001;
                        }
                        if (resOp1 is double && resOp2 is char)
                        {
                            return (double)resOp1 > (char)resOp2 || Math.Abs((double)resOp1 - (char)resOp2) < 0.00001;
                        }
                        if (resOp1 is char && resOp2 is double)
                        {
                            return (char)resOp1 > (double)resOp2 || Math.Abs((char)resOp1 - (double)resOp2) < 0.00001;
                        }

                        if (resOp1 is BigInteger && resOp2 is BigInteger)
                        {
                            return (BigInteger)resOp1 >= (BigInteger)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is char)
                        {
                            return (BigInteger)resOp1 >= (char)resOp2;
                        }
                        if (resOp1 is char && resOp2 is BigInteger)
                        {
                            return (char)resOp1 >= (BigInteger)resOp2;
                        }
                        if (resOp1 is char && resOp2 is char)
                        {
                            return (char)resOp1 >= (char)resOp2;
                        }
                        stuff.error("Semántico", "'EXPRESION' @ 'MAYORIGUALQUE', uno de los operandos es incompatible con el otro. Encontrado: " + getTipo(resOp1) + " >= " + getTipo(resOp2) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR'.", fila2, columna2, ctx);
                        return null;
                    }
                case Tipo.MENORQUE:
                    {
                        resOp1 = Validar(op1.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp1 == null)
                        {
                            return null;
                        }
                        resOp2 = Validar(op2.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp2 == null)
                        {
                            return null;
                        }

                        if (resOp1 is double && resOp2 is double)
                        {
                            return (double)resOp1 < (double)resOp2;
                        }
                        if (resOp1 is double && resOp2 is BigInteger)
                        {
                            return (double)resOp1 < (double)(BigInteger)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is double)
                        {
                            return (double)(BigInteger)resOp1 < (double)resOp2;
                        }
                        if (resOp1 is double && resOp2 is char)
                        {
                            return (double)resOp1 < (char)resOp2;
                        }
                        if (resOp1 is char && resOp2 is double)
                        {
                            return (char)resOp1 < (double)resOp2;
                        }

                        if (resOp1 is BigInteger && resOp2 is BigInteger)
                        {
                            return (BigInteger)resOp1 < (BigInteger)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is char)
                        {
                            return (BigInteger)resOp1 < (char)resOp2;
                        }
                        if (resOp1 is char && resOp2 is BigInteger)
                        {
                            return (char)resOp1 < (BigInteger)resOp2;
                        }
                        if (resOp1 is char && resOp2 is char)
                        {
                            return (char)resOp1 < (char)resOp2;
                        }
                        stuff.error("Semántico", "'EXPRESION' @ 'MENORQUE', uno de los operandos es incompatible con el otro. Encontrado: " + getTipo(resOp1) + " < " + getTipo(resOp2) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR'.", fila2, columna2, ctx);
                        return null;
                    }
                case Tipo.MENORIGUALQUE:
                    {
                        resOp1 = Validar(op1.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp1 == null)
                        {
                            return null;
                        }
                        resOp2 = Validar(op2.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp2 == null)
                        {
                            return null;
                        }

                        if (resOp1 is double && resOp2 is double)
                        {
                            return (double)resOp1 < (double)resOp2 || Math.Abs((double)resOp1 - (double)resOp2) < 0.00001;
                        }
                        if (resOp1 is double && resOp2 is BigInteger)
                        {
                            return (double)resOp1 < (double)(BigInteger)resOp2 || Math.Abs((double)resOp1 - (double)(BigInteger)resOp2) < 0.00001;
                        }
                        if (resOp1 is BigInteger && resOp2 is double)
                        {
                            return (double)(BigInteger)resOp1 < (double)resOp2 || Math.Abs((double)(BigInteger)resOp1 - (double)resOp2) < 0.00001;
                        }
                        if (resOp1 is double && resOp2 is char)
                        {
                            return (double)resOp1 < (char)resOp2 || Math.Abs((double)resOp1 - (char)resOp2) < 0.00001;
                        }
                        if (resOp1 is char && resOp2 is double)
                        {
                            return (char)resOp1 < (double)resOp2 || Math.Abs((char)resOp1 - (double)resOp2) < 0.00001;
                        }

                        if (resOp1 is BigInteger && resOp2 is BigInteger)
                        {
                            return (BigInteger)resOp1 <= (BigInteger)resOp2;
                        }
                        if (resOp1 is BigInteger && resOp2 is char)
                        {
                            return (BigInteger)resOp1 <= (char)resOp2;
                        }
                        if (resOp1 is char && resOp2 is BigInteger)
                        {
                            return (char)resOp1 <= (BigInteger)resOp2;
                        }
                        if (resOp1 is char && resOp2 is char)
                        {
                            return (char)resOp1 <= (char)resOp2;
                        }
                        stuff.error("Semántico", "'EXPRESION' @ 'MENORIGUALQUE', uno de los operandos es incompatible con el otro. Encontrado: " + getTipo(resOp1) + " <= " + getTipo(resOp2) + ", Esperado: 'ENTERO', 'DOUBLE', 'CHAR'.", fila2, columna2, ctx);
                        return null;
                    }
                case Tipo.AND:
                    {
                        resOp1 = ValidarMinimo(op1.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp1 == null)
                        {
                            return null;
                        }
                        if (resOp1 is bool)
                        {
                            if (!(bool)resOp1)
                            {
                                return false;
                            }
                            resOp2 = ValidarMinimo(op2.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                            if (resOp2 == null)
                            {
                                return null;
                            }
                            if (resOp2 is bool)
                            {
                                return resOp2;
                            }
                            stuff.error("Semántico", "'EXPRESION' @ 'AND', el operando 2 es del tipo incorrecto. Encontrado: " + getTipo(resOp2) + ", Esperado: 'BOOLEANO'", fila2, columna2, ctx);
                            return null;
                        }
                        stuff.error("Semántico", "'EXPRESION' @ 'AND', el operando 1 es del tipo incorrecto. Encontrado: " + getTipo(resOp1) + ", Esperado: 'BOOLEANO'", fila1, columna1, ctx);
                        return null;
                    }
                case Tipo.OR:
                    {
                        resOp1 = ValidarMinimo(op1.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp1 == null)
                        {
                            return null;
                        }
                        if (resOp1 is bool)
                        {
                            if ((bool)resOp1)
                            {
                                return true;
                            }
                            resOp2 = ValidarMinimo(op2.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                            if (resOp2 == null)
                            {
                                return null;
                            }
                            if (resOp2 is bool)
                            {
                                return resOp2;
                            }
                            stuff.error("Semántico", "'EXPRESION' @ 'OR', el operando 2 es del tipo incorrecto. Encontrado: " + getTipo(resOp2) + ", Esperado: 'BOOLEANO'", fila2, columna2, ctx);
                            return null;
                        }
                        stuff.error("Semántico", "'EXPRESION' @ 'OR', el operando 1 es del tipo incorrecto. Encontrado: " + getTipo(resOp1) + ", Esperado: 'BOOLEANO'", fila1, columna1, ctx);
                        return null;
                    }
                case Tipo.XOR:
                    {
                        resOp1 = ValidarMinimo(op1.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp1 == null)
                        {
                            return null;
                        }
                        resOp2 = ValidarMinimo(op2.ejecutar(ctx, stuff), ctx, stuff, fila2, columna2);
                        if (resOp2 == null)
                        {
                            return null;
                        }

                        if (resOp1 is bool && resOp2 is bool)
                        {
                            return (bool)resOp1 ^ (bool)resOp2;
                        }
                        stuff.error("Semántico", "'EXPRESION' @ 'XOR', uno de los operandos es incompatible con el otro. Encontrado: " + getTipo(resOp1) + " ^ " + getTipo(resOp2) + ", Esperado: 'BOOLEANO'.", fila2, columna2, ctx);
                        return null;
                    }
            }
            return null;
        }

        public static string getTipo(Object o)
        {
            if (o == null)
            {
                return "'error raro asd'";
            }
            if (o is Null)
            {
                return "'NULL'";
            }
            if (o is BigInteger)
            {
                return "'ENTERO'";
            }
            if (o is string)
            {
                return "'CADENA'";
            }
            if (o is bool)
            {
                return "'BOOLEANO'";
            }
            if (o is char)
            {
                return "'CHAR'";
            }
            if (o is double)
            {
                return "'DOUBLE'";
            }
            if (o is Arreglo)
            {
                return "'ARREGLO'";
            }
            if (o is Objeto)
            {
                return "'OBJETO:" + ((Objeto)o).nombre_clase + "'";
            }
            return "'" + o.GetType().ToString().ToUpper() + "'";
        }

        public static object Validar(Object o, Contexto ctx, Stuff stuff, int fila, int columna)
        {
            if (o == null)
            {
                return null;
            }
            if (o is lang.engine.Void)
            {
                stuff.error("Semántico", "'LLAMADA METODO/FUNCION', la funcion " + o.ToString() + " es de tipo void.", fila, columna, ctx);
                return null;
            }
            if (o is BigInteger)
            {
                if ((BigInteger)o > int.MaxValue)
                {
                    stuff.error("Semántico", "'ENTERO', el valor entero es mayor que lo permitido. Encontrado: " + ((BigInteger)o).ToString() + ", Maximo: " + int.MaxValue + ".", fila, columna, ctx);
                    return null;
                }
                if ((BigInteger)o < int.MinValue)
                {
                    stuff.error("Semántico", "'ENTERO', el valor entero es menor que lo permitido. Encontrado: " + ((BigInteger)o).ToString() + ", Minimo: " + int.MinValue + ".", fila, columna, ctx);
                    return null;
                }
                return o;
            }
            if (o is double)
            {
                return Truncate(o);
            }
            return o;
        }

        public static object ValidarMinimo(Object o, Contexto ctx, Stuff stuff, int fila, int columna)
        {
            if (o == null)
            {
                return null;
            }
            if (o is lang.engine.Void)
            {
                stuff.error("Semántico", "'LLAMADA METODO/FUNCION', la funcion " + o.ToString() + " es de tipo void.", fila, columna, ctx);
                return null;
            }
            return o;
        }

        public static object Truncate(object o)
        {
            if (o == null)
            {
                return null;
            }
            double a = Math.Truncate(((double)o) * 100000) / 100000;
            return a;
        }

        public int getLinea()
        {
            return fila2;
        }

        public int getColumna()
        {
            return columna2;
        }
    }
}
