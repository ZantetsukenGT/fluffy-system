using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using _OLC1_Proyecto2_201602988.lang.engine;
using _OLC1_Proyecto2_201602988.lang.definition;
using Irony.Parsing;
using System.Linq;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class Importar : Instruccion
    {
        public static List<Contexto> instanciasArchivos;
        public Instruccion op;
        public int fila, columna;
        public Importar(Instruccion op, int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;
            this.op = op;
        }
        public static void Reset()
        {
            instanciasArchivos = new List<Contexto>();
        }
        public object ejecutar(Contexto ctx, Stuff stuff)
        {
            object resOp = Operacion.Validar(op.ejecutar(ctx, stuff), ctx, stuff, fila, columna);
            if (resOp == null)
            {
                return null;
            }
            if (!(resOp is string))
            {
                stuff.error("Semántico", "'IMPORTAR', el parámetro 1 es del tipo incorrecto. Encontrado: " + Operacion.getTipo(resOp) + ", Esperado: 'CADENA'.", fila, columna, ctx);
                return null;
            }
            foreach (Contexto instancia in instanciasArchivos)
            {
                string filee;
                if (ctx.currentFile == null)
                {
                    filee = Path.GetFullPath(Path.Combine(resOp.ToString()));
                }
                else
                {
                    filee = Path.GetFullPath(Path.Combine(ctx.currentFile, @"..\", resOp.ToString()));
                }
                if (instancia.currentFile.Equals(filee))
                {
                    if(ctx != instancia)
                    {
                        ctx.otrosArchivos.Add(instancia);
                    }
                    return null;
                }
            }
            string file = "";
            try
            {
                StringBuilder sb = new StringBuilder();
                if(ctx.currentFile == null)
                {
                    file = Path.GetFullPath(Path.Combine(resOp.ToString()));
                }
                else
                {
                    file = Path.GetFullPath(Path.Combine(ctx.currentFile, @"..\", resOp.ToString()));
                }
                foreach (string line in File.ReadLines(file))
                {
                    sb.AppendLine(line);
                }
                
                String entrada = sb.Replace("”", "\"").Replace("“", "\"").Replace("\r", "").Replace("\f", "").ToString();
                if (entrada.Length > 0)
                {
                    Gramatica gramatica = new Gramatica();
                    LanguageData lenguaje = new LanguageData(gramatica);
                    Parser parser = new Parser(lenguaje);
                    ParseTree arbol = parser.Parse(entrada);
                    ParseTreeNode AST = arbol.Root;

                    List<Instruccion> myAST = new List<Instruccion>();
                    if (AST != null)
                    {
                        myAST = new GeneradorAST().Analizar(AST);
                    }
                    foreach (Irony.LogMessage error in parser.Context.CurrentParseTree.ParserMessages)
                    {
                        stuff.erroresLexicosSintacticos.Add(new Irony.LogMessage(error.Level, error.Location, ((file != null) ? Path.GetFileName(file) + ": " : "No File: ") + error.Message, error.ParserState));
                    }

                    if (stuff.erroresLexicosSintacticos.Count == 0)
                    {
                        Contexto otroGlobal = new Contexto();
                        otroGlobal.currentFile = file;
                        foreach (Instruccion i in myAST)
                        {
                            if (i is Importar)
                            {
                                i.ejecutar(otroGlobal, stuff);
                            }
                        }
                        foreach (Instruccion i in myAST)
                        {
                            if (i is DeclaracionMetodo || i is DeclaracionClase)
                            {
                                i.ejecutar(otroGlobal, stuff);
                            }
                        }
                        foreach (Instruccion i in myAST)
                        {
                            if (i is DeclaracionMetodo || i is DeclaracionClase || i is Main || i is Importar)
                            {
                            }
                            else
                            {
                                i.ejecutar(otroGlobal, stuff);
                            }
                        }
                        foreach (Instruccion i in myAST)
                        {
                            if (i is Main)
                            {
                                stuff.error("Semántico", "'MAIN' está declarado en otro archivo que no es el principal.", i.getLinea(), i.getColumna(), otroGlobal);
                            }
                        }
                        foreach (Clase c in otroGlobal.clases)
                        {
                            ctx.clases.Add(c);
                        }
                        ctx.otrosArchivos.Add(otroGlobal);
                        instanciasArchivos.Add(otroGlobal);
                        return null;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                stuff.error("Semántico", "'IMPORTAR', el archivo '" + file + "' no existe o no se puede leer.", fila, columna, ctx);
            }
            catch (IOException e)
            {
                stuff.error("Semántico", e.Message, fila, columna, ctx);
            }
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
