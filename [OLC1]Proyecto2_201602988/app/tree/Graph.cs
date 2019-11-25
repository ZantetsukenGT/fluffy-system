using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class Graph : Instruccion
    {
        public Instruccion op1, op2;
        public int fila, columna;

        public Graph(Instruccion op1, Instruccion op2, int fila, int columna)
        {
            this.op1 = op1;
            this.op2 = op2;
            this.fila = fila;
            this.columna = columna;
        }

        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            object resOp1 = Operacion.Validar(op1.ejecutar(ctx, stuff), ctx, stuff, fila, columna);
            if(resOp1 == null)
            {
                return null;
            }
            if(!(resOp1 is string))
            {
                stuff.error("Semántico", "'GRAPH', el parámetro 1 es del tipo incorrecto. Encontrado: " + Operacion.getTipo(resOp1) + ", Esperado: 'CADENA'.", fila, columna, ctx);
                return null;
            }

            object resOp2 = Operacion.Validar(op2.ejecutar(ctx, stuff), ctx, stuff, fila, columna);
            if (resOp2 == null)
            {
                return null;
            }
            if (!(resOp2 is string))
            {
                stuff.error("Semántico", "'GRAPH', el parámetro 2 es del tipo incorrecto. Encontrado: " + Operacion.getTipo(resOp2) + ", Esperado: 'CADENA'.", fila, columna, ctx);
                return null;
            }

            StreamWriter archivo = new StreamWriter(resOp1.ToString() + ".nothing", false);
            archivo.Write(resOp2.ToString().Replace("\\\"","\""));
            archivo.Close();

            Process p = new Process();
            p.StartInfo.FileName = @"C:\Program Files (x86)\Graphviz2.38\bin\dot.exe";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.Arguments = "-Tpng " + resOp1.ToString() + ".nothing " + "-o " + resOp1.ToString();
            p.EnableRaisingEvents = true;
            p.Exited += (sender1, e1) =>
            {
                Thread.Sleep(100);
                if(File.Exists(resOp1.ToString()))
                {
                    Process.Start(resOp1.ToString());
                }
                else
                {
                    stuff.error("Advertencia", "'GRAPH', Revise el archivo '" + resOp1.ToString() + ".nothing', puede contener errores para GraphViz.", fila, columna, ctx);
                }
            };
            p.Start();
            return null;
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
