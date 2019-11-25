using System;
using System.Collections.Generic;
using Irony.Parsing;
using _OLC1_Proyecto2_201602988.app.tree;
using System.Numerics;

namespace _OLC1_Proyecto2_201602988.lang.definition
{
    public class GeneradorAST
    {
        public GeneradorAST()
        {
        }
        public List<Instruccion> Analizar(ParseTreeNode raiz)
        {
            return (List<Instruccion>) analizarNodos(raiz);
        }
        
        private object analizarNodos(ParseTreeNode actual)
        {
            if (EsteNodoEs(actual, "instrucciones_global"))
            {
                List<Instruccion> ins = new List<Instruccion>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    ins.Add((Instruccion)analizarNodos(hijo));
                }
                return ins;
            }
            else if (EsteNodoEs(actual, "instruccion_global"))
            {
                if (actual.ChildNodes.Count == 1)
                {
                    if (EsteNodoEs(actual.ChildNodes[0], "RBREAK"))
                    {
                        return new Break(getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "RCONTINUE"))
                    {
                        return new Continue(getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "expresion_full"))
                    {
                        return new Wrapper((Instruccion)analizarNodos(actual.ChildNodes[0]));
                    }
                    else
                    {
                        return analizarNodos(actual.ChildNodes[0]);
                    }
                }
                else if (actual.ChildNodes.Count == 2)
                {
                    if (EsteNodoEs(actual.ChildNodes[0], "RRETURN"))
                    {
                        return new Return((Instruccion)analizarNodos(actual.ChildNodes[1]), getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "definicion_metodo_global"))
                    {
                        DeclaracionMetodo dm = (DeclaracionMetodo)analizarNodos(actual.ChildNodes[0]);
                        dm.ins = (List<Instruccion>)analizarNodos(actual.ChildNodes[1]);
                        return dm;
                    }
                }
                else if (actual.ChildNodes.Count == 3)
                {
                    if (EsteNodoEs(actual.ChildNodes[0], "RLOG"))
                    {
                        return new Log(getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "RALERT"))
                    {
                        return new Alert(getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "IDENTIFICADOR"))
                    {
                        return new Main(getLexema(actual, 0), getLinea(actual, 0), getColumna(actual, 0));
                    }
                }
                else if (actual.ChildNodes.Count == 4)
                {
                    if (EsteNodoEs(actual.ChildNodes[0], "RIMPORTAR"))
                    {
                        return new Importar((Instruccion)analizarNodos(actual.ChildNodes[2]), getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "RLOG"))
                    {
                        return new Log((Instruccion)analizarNodos(actual.ChildNodes[2]), getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "RALERT"))
                    {
                        return new Alert((Instruccion)analizarNodos(actual.ChildNodes[2]), getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "IDENTIFICADOR"))
                    {
                        return new Main(getLexema(actual, 0), (List<Instruccion>)analizarNodos(actual.ChildNodes[3]), getLinea(actual, 0), getColumna(actual, 0));
                    }
                }
                else if (actual.ChildNodes.Count == 5)
                {
                    return new Graph((Instruccion)analizarNodos(actual.ChildNodes[2]), (Instruccion)analizarNodos(actual.ChildNodes[3]), getLinea(actual, 0), getColumna(actual, 0));
                }
            }
            else if (EsteNodoEs(actual, "declaracion_clase"))
            {
                if (actual.ChildNodes.Count == 1)
                {
                    return new DeclaracionClase(getLexema(actual, 0), getLinea(actual, 0), getColumna(actual, 0));
                }
                else if (actual.ChildNodes.Count == 2)
                {
                    return new DeclaracionClase(getLexema(actual, 0), (List<Instruccion>)analizarNodos(actual.ChildNodes[1]), getLinea(actual, 0), getColumna(actual, 0));
                }
            }
            else if (EsteNodoEs(actual, "miembros_clase"))
            {
                List<Instruccion> miembros = new List<Instruccion>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    miembros.Add((Instruccion)analizarNodos(hijo));
                }
                return miembros;
            }
            else if (EsteNodoEs(actual, "miembro_clase"))
            {
                if (actual.ChildNodes.Count == 1)
                {
                    if(EsteNodoEs(actual.ChildNodes[0], "declaracion_asignacion"))
                    {
                        object decl = analizarNodos(actual.ChildNodes[0]);
                        if (decl is Declaracion)
                        {
                            Declaracion d = (Declaracion)decl;
                            d.tipo = Declaracion.Tipo.ATRIBUTO;
                        }
                        else if (decl is DeclaracionArreglo)
                        {
                            DeclaracionArreglo d = (DeclaracionArreglo)decl;
                            d.tipo = Declaracion.Tipo.ATRIBUTO;
                        }
                        else if (decl is Declaraciones)
                        {
                            foreach(Instruccion i in ((Declaraciones)decl).declaraciones)
                            {

                                if (i is Declaracion)
                                {
                                    Declaracion d = (Declaracion)i;
                                    d.tipo = Declaracion.Tipo.ATRIBUTO;
                                }
                                else if (i is DeclaracionArreglo)
                                {
                                    DeclaracionArreglo d = (DeclaracionArreglo)i;
                                    d.tipo = Declaracion.Tipo.ATRIBUTO;
                                }
                            }
                        }
                        return decl;
                    }
                    else
                    {
                        return analizarNodos(actual.ChildNodes[0]);
                    }
                }
                else if (actual.ChildNodes.Count == 2)
                {
                    DeclaracionMetodo dm = (DeclaracionMetodo)analizarNodos(actual.ChildNodes[0]);
                    dm.ins = (List<Instruccion>)analizarNodos(actual.ChildNodes[1]);
                    return dm;
                }
            }
            else if (EsteNodoEs(actual, "definicion_metodo"))
            {
                if (actual.ChildNodes.Count == 3)
                {
                    return new DeclaracionMetodo(false, false, getLexema(actual, 0), getLinea(actual, 0), getColumna(actual, 0));
                }
                else if (actual.ChildNodes.Count == 4)
                {
                    if (EsteNodoEs(actual.ChildNodes[0], "RVOID"))
                    {
                        return new DeclaracionMetodo(false, true, getLexema(actual, 1), getLinea(actual, 1), getColumna(actual, 1));
                    }
                    return new DeclaracionMetodo(false, false, getLexema(actual, 0), (Instruccion)analizarNodos(actual.ChildNodes[2]), getLinea(actual, 0), getColumna(actual, 0));
                }
                else if (actual.ChildNodes.Count == 5)
                {
                    return new DeclaracionMetodo(false, true, getLexema(actual, 1), (Instruccion)analizarNodos(actual.ChildNodes[3]), getLinea(actual, 1), getColumna(actual, 1));
                }
            }
            else if (EsteNodoEs(actual, "definicion_metodo_global"))
            {
                if (actual.ChildNodes.Count == 3)
                {
                    return new DeclaracionMetodo(true, false, getLexema(actual, 0), getLinea(actual, 0), getColumna(actual, 0));
                }
                else if (actual.ChildNodes.Count == 4)
                {
                    if (EsteNodoEs(actual.ChildNodes[0], "RVOID"))
                    {
                        return new DeclaracionMetodo(true, true, getLexema(actual, 1), getLinea(actual, 1), getColumna(actual, 1));
                    }
                    return new DeclaracionMetodo(true, false, getLexema(actual, 0), (Instruccion)analizarNodos(actual.ChildNodes[2]), getLinea(actual, 0), getColumna(actual, 0));
                }
                else if (actual.ChildNodes.Count == 5)
                {
                    return new DeclaracionMetodo(true, true, getLexema(actual, 1), (Instruccion)analizarNodos(actual.ChildNodes[3]), getLinea(actual, 1), getColumna(actual, 1));
                }
            }
            else if (EsteNodoEs(actual, "decl_lista_parametros"))
            {
                List<string> ids = new List<string>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    ids.Add(getLexema(hijo));
                }
                return new Declaracion(Declaracion.Tipo.LOCAL, ids, getLinea(actual, 0), getColumna(actual, 0));
            }
            else if (EsteNodoEs(actual, "instrucciones"))
            {
                List<Instruccion> ins = new List<Instruccion>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    ins.Add((Instruccion)analizarNodos(hijo));
                }
                return ins;
            }
            else if (EsteNodoEs(actual, "instruccion"))
            {
                if (actual.ChildNodes.Count == 1)
                {
                    if (EsteNodoEs(actual.ChildNodes[0], "RBREAK"))
                    {
                        return new Break(getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "RCONTINUE"))
                    {
                        return new Continue(getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "expresion_full"))
                    {
                        return new Wrapper((Instruccion)analizarNodos(actual.ChildNodes[0]));
                    }
                    else
                    {
                        return analizarNodos(actual.ChildNodes[0]);
                    }
                }
                else if (actual.ChildNodes.Count == 2)
                {
                    return new Return((Instruccion)analizarNodos(actual.ChildNodes[1]), getLinea(actual, 0), getColumna(actual, 0));
                }
                else if (actual.ChildNodes.Count == 3)
                {
                    if (EsteNodoEs(actual.ChildNodes[0], "RLOG"))
                    {
                        return new Log(getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "RALERT"))
                    {
                        return new Alert(getLinea(actual, 0), getColumna(actual, 0));
                    }
                }
                else if (actual.ChildNodes.Count == 4)
                {
                    if (EsteNodoEs(actual.ChildNodes[0], "RLOG"))
                    {
                        return new Log((Instruccion)analizarNodos(actual.ChildNodes[2]), getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "RALERT"))
                    {
                        return new Alert((Instruccion)analizarNodos(actual.ChildNodes[2]), getLinea(actual, 0), getColumna(actual, 0));
                    }
                }
                else if (actual.ChildNodes.Count == 5)
                {
                    return new Graph((Instruccion)analizarNodos(actual.ChildNodes[2]), (Instruccion)analizarNodos(actual.ChildNodes[3]), getLinea(actual, 0), getColumna(actual, 0));
                }
            }
            else if (EsteNodoEs(actual, "instruccion_for"))
            {
                if (actual.ChildNodes.Count == 5)
                {
                    return new For((Instruccion)analizarNodos(actual.ChildNodes[1]), (Instruccion)analizarNodos(actual.ChildNodes[2]), (Instruccion)analizarNodos(actual.ChildNodes[3]));
                }
                else if (actual.ChildNodes.Count == 6)
                {
                    return new For((Instruccion)analizarNodos(actual.ChildNodes[1]), (Instruccion)analizarNodos(actual.ChildNodes[2]), (Instruccion)analizarNodos(actual.ChildNodes[3]), (List<Instruccion>)analizarNodos(actual.ChildNodes[5]));
                }
            }
            else if (EsteNodoEs(actual, "instruccion_while"))
            {
                if (actual.ChildNodes.Count == 3)
                {
                    return new While((Instruccion) analizarNodos(actual.ChildNodes[1]));
                }
                else if (actual.ChildNodes.Count == 4)
                {
                    return new While((Instruccion)analizarNodos(actual.ChildNodes[1]), (List<Instruccion>)analizarNodos(actual.ChildNodes[3]));
                }
            }
            else if (EsteNodoEs(actual, "instruccion_dowhile"))
            {
                if (actual.ChildNodes.Count == 3)
                {
                    return new DoWhile((Instruccion)analizarNodos(actual.ChildNodes[1]));
                }
                else if (actual.ChildNodes.Count == 4)
                {
                    return new DoWhile((Instruccion)analizarNodos(actual.ChildNodes[2]), (List<Instruccion>)analizarNodos(actual.ChildNodes[0]));
                }
            }
            else if (EsteNodoEs(actual, "instruccion_switch"))
            {
                if (actual.ChildNodes.Count == 3)
                {
                    return new Switch((Instruccion)analizarNodos(actual.ChildNodes[1]));
                }
                else if (actual.ChildNodes.Count == 4)
                {
                    if (EsteNodoEs(actual.ChildNodes[3], "caso_default"))
                    {
                        return new Switch((Instruccion)analizarNodos(actual.ChildNodes[1]), (Instruccion)analizarNodos(actual.ChildNodes[3]));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[3], "lista_casos"))
                    {
                        return new Switch((Instruccion)analizarNodos(actual.ChildNodes[1]), (List<Instruccion>)analizarNodos(actual.ChildNodes[3]));
                    }
                }
                else if (actual.ChildNodes.Count == 5)
                {
                    return new Switch((Instruccion)analizarNodos(actual.ChildNodes[1]), (List<Instruccion>)analizarNodos(actual.ChildNodes[3]), (Instruccion)analizarNodos(actual.ChildNodes[4]));
                }
            }
            else if (EsteNodoEs(actual, "lista_casos"))
            {
                List<Instruccion> casos = new List<Instruccion>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    casos.Add((Instruccion)analizarNodos(hijo));
                }
                return casos;
            }
            else if (EsteNodoEs(actual, "caso"))
            {
                if (actual.ChildNodes.Count == 1)
                {
                    return new SwitchCase((Instruccion)analizarNodos(actual.ChildNodes[0]));
                }
                else if (actual.ChildNodes.Count == 2)
                {
                    return new SwitchCase((Instruccion)analizarNodos(actual.ChildNodes[0]), (List<Instruccion>)analizarNodos(actual.ChildNodes[1]));
                }
            }
            else if (EsteNodoEs(actual, "caso_default"))
            {
                if (actual.ChildNodes.Count == 0)
                {
                    return new SwitchDefault();
                }
                else if (actual.ChildNodes.Count == 1)
                {
                    return new SwitchDefault((List<Instruccion>)analizarNodos(actual.ChildNodes[0]));
                }
            }
            else if (EsteNodoEs(actual, "identificadores"))
            {
                List<string> ids = new List<string>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    ids.Add(getLexema(hijo));
                }
                return ids;
            }
            else if (EsteNodoEs(actual, "declaracion_asignacion"))
            {
                return analizarNodos(actual.ChildNodes[0]);
            }
            else if (EsteNodoEs(actual, "asignacion"))
            {
                return new Asignacion((Instruccion)analizarNodos(actual.ChildNodes[0]), (Instruccion)analizarNodos(actual.ChildNodes[1]));
            }
            else if (EsteNodoEs(actual, "asig_acceso_celda"))
            {
                return analizarNodos(actual.ChildNodes[0]);
            }
            else if (EsteNodoEs(actual, "declaracion"))
            {
                if (actual.ChildNodes.Count == 1)
                {
                    if (EsteNodoEs(actual.ChildNodes[0], "identificadores"))
                    {
                        return new Declaracion(Declaracion.Tipo.LOCAL, (List<string>)analizarNodos(actual.ChildNodes[0]), getLinea(actual.ChildNodes[0], 0), getColumna(actual.ChildNodes[0], 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "declaraciones_con_valor"))
                    {
                        return new Declaraciones((List<Instruccion>)analizarNodos(actual.ChildNodes[0]));
                    }
                }
                else if (actual.ChildNodes.Count == 2)
                {
                    if (EsteNodoEs(actual.ChildNodes[0], "identificadores"))
                    {
                        return new DeclaracionArreglo(Declaracion.Tipo.LOCAL, (List<string>)analizarNodos(actual.ChildNodes[0]), (List<Instruccion>)analizarNodos(actual.ChildNodes[1]), getLinea(actual.ChildNodes[0], 0), getColumna(actual.ChildNodes[0], 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "declaraciones_con_valor"))
                    {
                        List<Instruccion> declaraciones = (List<Instruccion>)analizarNodos(actual.ChildNodes[0]);
                        declaraciones.Add(new Declaracion(Declaracion.Tipo.LOCAL, (List<string>)analizarNodos(actual.ChildNodes[1]), getLinea(actual.ChildNodes[1], 0), getColumna(actual.ChildNodes[1], 0)));
                        return new Declaraciones(declaraciones);
                    }
                }
                else if (actual.ChildNodes.Count == 3)
                {
                    List<Instruccion> declaraciones = (List<Instruccion>)analizarNodos(actual.ChildNodes[0]);
                    declaraciones.Add(new DeclaracionArreglo(Declaracion.Tipo.LOCAL, (List<string>)analizarNodos(actual.ChildNodes[1]), (List<Instruccion>)analizarNodos(actual.ChildNodes[2]), getLinea(actual.ChildNodes[1], 0), getColumna(actual.ChildNodes[1], 0)));
                    return new Declaraciones(declaraciones);
                }
            }
            else if (EsteNodoEs(actual, "declaraciones_con_valor"))
            {
                List<Instruccion> declaraciones = new List<Instruccion>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    declaraciones.Add((Instruccion)analizarNodos(hijo));
                }
                return declaraciones;
            }
            else if (EsteNodoEs(actual, "declaracion_con_valor"))
            {
                if (actual.ChildNodes.Count == 2)
                {
                    return new Declaracion(Declaracion.Tipo.LOCAL, (List<string>)analizarNodos(actual.ChildNodes[0]), (Instruccion)analizarNodos(actual.ChildNodes[1]), getLinea(actual.ChildNodes[0], 0), getColumna(actual.ChildNodes[0], 0));
                }
                else if (actual.ChildNodes.Count == 3)
                {
                    return new DeclaracionArreglo(Declaracion.Tipo.LOCAL, (List<string>)analizarNodos(actual.ChildNodes[0]), (List<Instruccion>)analizarNodos(actual.ChildNodes[1]), (Instruccion)analizarNodos(actual.ChildNodes[2]), getLinea(actual.ChildNodes[0], 0), getColumna(actual.ChildNodes[0], 0));
                }
            }
            else if (EsteNodoEs(actual, "declaracion_global"))
            {
                if (actual.ChildNodes.Count == 1)
                {
                    if (EsteNodoEs(actual.ChildNodes[0], "identificadores"))
                    {
                        return new Declaracion(Declaracion.Tipo.GLOBAL, (List<string>)analizarNodos(actual.ChildNodes[0]), getLinea(actual.ChildNodes[0], 0), getColumna(actual.ChildNodes[0], 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "declaraciones_con_valor_global"))
                    {
                        return new Declaraciones((List<Instruccion>)analizarNodos(actual.ChildNodes[0]));
                    }
                }
                else if (actual.ChildNodes.Count == 2)
                {
                    if (EsteNodoEs(actual.ChildNodes[0], "identificadores"))
                    {
                        return new DeclaracionArreglo(Declaracion.Tipo.GLOBAL, (List<string>)analizarNodos(actual.ChildNodes[0]), (List<Instruccion>)analizarNodos(actual.ChildNodes[1]), getLinea(actual.ChildNodes[0], 0), getColumna(actual.ChildNodes[0], 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "declaraciones_con_valor_global"))
                    {
                        List<Instruccion> declaraciones = (List<Instruccion>)analizarNodos(actual.ChildNodes[0]);
                        declaraciones.Add(new Declaracion(Declaracion.Tipo.GLOBAL, (List<string>)analizarNodos(actual.ChildNodes[1]), getLinea(actual.ChildNodes[1], 0), getColumna(actual.ChildNodes[1], 0)));
                        return new Declaraciones(declaraciones);
                    }
                }
                else if (actual.ChildNodes.Count == 3)
                {
                    List<Instruccion> declaraciones = (List<Instruccion>)analizarNodos(actual.ChildNodes[0]);
                    declaraciones.Add(new DeclaracionArreglo(Declaracion.Tipo.GLOBAL, (List<string>)analizarNodos(actual.ChildNodes[1]), (List<Instruccion>)analizarNodos(actual.ChildNodes[2]), getLinea(actual.ChildNodes[1], 0), getColumna(actual.ChildNodes[1], 0)));
                    return new Declaraciones(declaraciones);
                }
            }
            else if (EsteNodoEs(actual, "declaraciones_con_valor_global"))
            {
                List<Instruccion> declaraciones = new List<Instruccion>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    declaraciones.Add((Instruccion)analizarNodos(hijo));
                }
                return declaraciones;
            }
            else if (EsteNodoEs(actual, "declaracion_con_valor_global"))
            {
                if (actual.ChildNodes.Count == 2)
                {
                    return new Declaracion(Declaracion.Tipo.GLOBAL, (List<string>)analizarNodos(actual.ChildNodes[0]), (Instruccion)analizarNodos(actual.ChildNodes[1]), getLinea(actual.ChildNodes[0], 0), getColumna(actual.ChildNodes[0], 0));
                }
                else if (actual.ChildNodes.Count == 3)
                {
                    return new DeclaracionArreglo(Declaracion.Tipo.GLOBAL, (List<string>)analizarNodos(actual.ChildNodes[0]), (List<Instruccion>)analizarNodos(actual.ChildNodes[1]), (Instruccion)analizarNodos(actual.ChildNodes[2]), getLinea(actual.ChildNodes[0], 0), getColumna(actual.ChildNodes[0], 0));
                }
            }
            else if (EsteNodoEs(actual, "decl_celdas"))
            {
                List<Instruccion> indices = new List<Instruccion>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    indices.Add((Instruccion)analizarNodos(hijo));
                }
                return indices;
            }
            else if (EsteNodoEs(actual, "arr_una_dimension"))
            {
                List<Object> expresiones = new List<Object>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    expresiones.Add(analizarNodos(hijo));
                }
                return expresiones;
            }
            else if (EsteNodoEs(actual, "arr_dos_dimension"))
            {
                List<Object> expresiones = new List<Object>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    expresiones.Add(analizarNodos(hijo));
                }
                return expresiones;
            }
            else if (EsteNodoEs(actual, "arr_tres_dimension"))
            {
                List<Object> expresiones = new List<Object>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    expresiones.Add(analizarNodos(hijo));
                }
                return expresiones;
            }
            else if (EsteNodoEs(actual, "bloque_if_elseif_else"))
            {
                if (actual.ChildNodes.Count == 1)
                {
                    Instruccion principal = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                    return new IfElseIfElse(principal);
                }
                else if (actual.ChildNodes.Count == 2)
                {
                    if (EsteNodoEs(actual.ChildNodes[1], "instrucciones_elseif"))
                    {
                        Instruccion principal = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                        List<Instruccion> listaElseIf = (List<Instruccion>)analizarNodos(actual.ChildNodes[1]);
                        return new IfElseIfElse(principal, listaElseIf);
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "instruccion_else"))
                    {
                        Instruccion principal = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                        Instruccion ultimo = (Instruccion)analizarNodos(actual.ChildNodes[1]);
                        return new IfElseIfElse(principal, ultimo);
                    }
                }
                else if (actual.ChildNodes.Count == 3)
                {
                    Instruccion principal = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                    List<Instruccion> listaElseIf = (List<Instruccion>)analizarNodos(actual.ChildNodes[1]);
                    Instruccion ultimo = (Instruccion)analizarNodos(actual.ChildNodes[2]);
                    return new IfElseIfElse(principal, listaElseIf, ultimo);
                }
            }
            else if (EsteNodoEs(actual, "instruccion_if"))
            {
                if (actual.ChildNodes.Count == 3)
                {
                    Instruccion cond = (Instruccion)analizarNodos(actual.ChildNodes[1]);
                    return new If(cond);
                }
                else if (actual.ChildNodes.Count == 4)
                {
                    Instruccion cond = (Instruccion)analizarNodos(actual.ChildNodes[1]);
                    List<Instruccion> ins = (List<Instruccion>)analizarNodos(actual.ChildNodes[3]);
                    return new If(cond, ins);
                }
            }
            else if (EsteNodoEs(actual, "instrucciones_elseif"))
            {
                List<Instruccion> listaElseIf = new List<Instruccion>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    listaElseIf.Add((Instruccion)analizarNodos(hijo));
                }
                return listaElseIf;
            }
            else if (EsteNodoEs(actual, "instruccion_elseif"))
            {
                if (actual.ChildNodes.Count == 3)
                {
                    Instruccion cond = (Instruccion)analizarNodos(actual.ChildNodes[1]);
                    return new If(cond);
                }
                else if (actual.ChildNodes.Count == 4)
                {
                    Instruccion cond = (Instruccion)analizarNodos(actual.ChildNodes[1]);
                    List<Instruccion> ins = (List<Instruccion>)analizarNodos(actual.ChildNodes[3]);
                    return new If(cond, ins);
                }
            }
            else if (EsteNodoEs(actual, "instruccion_else"))
            {
                if (actual.ChildNodes.Count == 0)
                {
                    return null;
                }
                else if (actual.ChildNodes.Count == 1)
                {
                    List<Instruccion> ins = (List<Instruccion>)analizarNodos(actual.ChildNodes[0]);
                    return new If(ins);
                }
            }
            else if (EsteNodoEs(actual, "expresion_full"))
            {
                return analizarNodos(actual.ChildNodes[0]);
            }
            else if (EsteNodoEs(actual, "expresion"))
            {
                if (actual.ChildNodes.Count == 1)
                {
                    if (EsteNodoEs(actual.ChildNodes[0], "id"))
                    {
                        return analizarNodos(actual.ChildNodes[0]);
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "CADENA"))
                    {
                        string str = getLexema(actual, 0);
                        str = str.Substring(1, str.Length - 2);
                        str = str.Replace("\\n", "\n");
                        str = str.Replace("\\t", "\t");
                        str = str.Replace("\\r", "\r");
                        return new Operacion(str, Operacion.Tipo.CADENA, getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "NUMERO"))
                    {
                        double r1 = Convert.ToDouble(getLexema(actual, 0));
                        try
                        {
                            BigInteger r2 = BigInteger.Parse(getLexema(actual, 0));
                            return new Operacion(r2, Operacion.Tipo.ENTERO, getLinea(actual, 0), getColumna(actual, 0));
                        }
                        catch (Exception)
                        {
                            return new Operacion(r1, Operacion.Tipo.DOUBLE, getLinea(actual, 0), getColumna(actual, 0));
                        }
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "CHAR"))
                    {
                        string r1 = getLexema(actual, 0).Replace("\'", "");
                        return new Operacion(r1[0], Operacion.Tipo.CHAR, getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "RTRUE"))
                    {
                        return new Operacion(true, Operacion.Tipo.BOOLEANO, getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "RFALSE"))
                    {
                        return new Operacion(false, Operacion.Tipo.BOOLEANO, getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "RNULL"))
                    {
                        return new Operacion(engine.Simbolo.NULL, Operacion.Tipo.NULL, getLinea(actual, 0), getColumna(actual, 0));
                    }
                }
                else if (actual.ChildNodes.Count == 2)
                {
                    if (EsteNodoEs(actual.ChildNodes[1], "AUMENTO"))
                    {
                        return new Aumento((Instruccion)analizarNodos(actual.ChildNodes[0]), getLinea(actual, 1), getColumna(actual, 1));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "DECREMENTO"))
                    {
                        return new Decremento((Instruccion)analizarNodos(actual.ChildNodes[0]), getLinea(actual, 1), getColumna(actual, 1));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "NOT"))
                    {
                        return new Operacion((Instruccion)analizarNodos(actual.ChildNodes[1]), Operacion.Tipo.NOT, getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[0], "SIGNOMENOS"))
                    {
                        return new Operacion((Instruccion)analizarNodos(actual.ChildNodes[1]), Operacion.Tipo.NEGATIVO, getLinea(actual, 0), getColumna(actual, 0));
                    }
                }
                else if (actual.ChildNodes.Count == 3)
                {
                    if (EsteNodoEs(actual.ChildNodes[1], "SIGNOMAS"))
                    {
                        Instruccion op1 = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                        Instruccion op2 = (Instruccion)analizarNodos(actual.ChildNodes[2]);
                        return new Operacion(op1, op2, Operacion.Tipo.SUMA);
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "SIGNOMENOS"))
                    {
                        Instruccion op1 = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                        Instruccion op2 = (Instruccion)analizarNodos(actual.ChildNodes[2]);
                        return new Operacion(op1, op2, Operacion.Tipo.RESTA);
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "SIGNODIVIDIDO"))
                    {
                        Instruccion op1 = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                        Instruccion op2 = (Instruccion)analizarNodos(actual.ChildNodes[2]);
                        return new Operacion(op1, op2, Operacion.Tipo.DIVISION);
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "SIGNOPOR"))
                    {
                        Instruccion op1 = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                        Instruccion op2 = (Instruccion)analizarNodos(actual.ChildNodes[2]);
                        return new Operacion(op1, op2, Operacion.Tipo.MULTIPLICACION);
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "SIGNOPOTENCIA"))
                    {
                        Instruccion op1 = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                        Instruccion op2 = (Instruccion)analizarNodos(actual.ChildNodes[2]);
                        return new Operacion(op1, op2, Operacion.Tipo.POTENCIA);
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "MAYORQUE"))
                    {
                        Instruccion op1 = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                        Instruccion op2 = (Instruccion)analizarNodos(actual.ChildNodes[2]);
                        return new Operacion(op1, op2, Operacion.Tipo.MAYORQUE);
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "MENORQUE"))
                    {
                        Instruccion op1 = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                        Instruccion op2 = (Instruccion)analizarNodos(actual.ChildNodes[2]);
                        return new Operacion(op1, op2, Operacion.Tipo.MENORQUE);
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "MAYORIGUALQUE"))
                    {
                        Instruccion op1 = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                        Instruccion op2 = (Instruccion)analizarNodos(actual.ChildNodes[2]);
                        return new Operacion(op1, op2, Operacion.Tipo.MAYORIGUALQUE);
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "MENORIGUALQUE"))
                    {
                        Instruccion op1 = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                        Instruccion op2 = (Instruccion)analizarNodos(actual.ChildNodes[2]);
                        return new Operacion(op1, op2, Operacion.Tipo.MENORIGUALQUE);
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "IGUALQUE"))
                    {
                        Instruccion op1 = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                        Instruccion op2 = (Instruccion)analizarNodos(actual.ChildNodes[2]);
                        return new Operacion(op1, op2, Operacion.Tipo.IGUALQUE);
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "DISTINTOQUE"))
                    {
                        Instruccion op1 = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                        Instruccion op2 = (Instruccion)analizarNodos(actual.ChildNodes[2]);
                        return new Operacion(op1, op2, Operacion.Tipo.DISTINTOQUE);
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "OR"))
                    {
                        Instruccion op1 = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                        Instruccion op2 = (Instruccion)analizarNodos(actual.ChildNodes[2]);
                        return new Operacion(op1, op2, Operacion.Tipo.OR);
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "AND"))
                    {
                        Instruccion op1 = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                        Instruccion op2 = (Instruccion)analizarNodos(actual.ChildNodes[2]);
                        return new Operacion(op1, op2, Operacion.Tipo.AND);
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "XOR"))
                    {
                        Instruccion op1 = (Instruccion)analizarNodos(actual.ChildNodes[0]);
                        Instruccion op2 = (Instruccion)analizarNodos(actual.ChildNodes[2]);
                        return new Operacion(op1, op2, Operacion.Tipo.XOR);
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "expresion"))
                    {
                        return analizarNodos(actual.ChildNodes[1]);
                    }
                }
                else if (actual.ChildNodes.Count == 4)
                {
                    return new NewObjeto(getLexema(actual, 1), getLinea(actual, 1), getColumna(actual, 1));
                }
                else if (actual.ChildNodes.Count == 5)
                {
                    NewObjeto no = new NewObjeto(getLexema(actual, 1), getLinea(actual, 1), getColumna(actual, 1));
                    return new AccesoPropiedad(no, (Instruccion)analizarNodos(actual.ChildNodes[4]));
                }
            }
            else if (EsteNodoEs(actual, "id"))
            {
                if (actual.ChildNodes.Count == 1)
                {
                    return new Operacion(getLexema(actual, 0), Operacion.Tipo.IDENTIFICADOR, getLinea(actual, 0), getColumna(actual, 0));
                }
                else if (actual.ChildNodes.Count == 2)
                {
                    if (EsteNodoEs(actual.ChildNodes[1], "llamada_metodo"))
                    {
                        return new LlamadaMetodo(getLexema(actual, 0), (List<Instruccion>)analizarNodos(actual.ChildNodes[1]), getLinea(actual, 0), getColumna(actual, 0));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "asig_acceso_celda"))
                    {
                        return new AccesoArreglo((Instruccion)analizarNodos(actual.ChildNodes[0]),(Instruccion)analizarNodos(actual.ChildNodes[1]));
                    }
                    else if (EsteNodoEs(actual.ChildNodes[1], "id"))
                    {
                        return new AccesoPropiedad((Instruccion)analizarNodos(actual.ChildNodes[0]), (Instruccion)analizarNodos(actual.ChildNodes[1]));
                    }
                }
            }
            else if (EsteNodoEs(actual, "llamada_metodo"))
            {
                if (actual.ChildNodes.Count == 2)
                {
                    return new List<Instruccion>();
                }
                else if (actual.ChildNodes.Count == 3)
                {
                    return analizarNodos(actual.ChildNodes[1]);
                }
            }
            else if (EsteNodoEs(actual, "lista_expresiones"))
            {
                List<Instruccion> ins = new List<Instruccion>();
                foreach (ParseTreeNode hijo in actual.ChildNodes)
                {
                    ins.Add((Instruccion)analizarNodos(hijo));
                }
                return ins;
            }
            else if (EsteNodoEs(actual, "expresion_array"))
            {
                if (EsteNodoEs(actual.ChildNodes[0],"arr_una_dimension"))
                {
                    return new Operacion(analizarNodos(actual.ChildNodes[0]), Operacion.Tipo.EXPRESIONARRAY1, 0, 0);
                }
                else if (EsteNodoEs(actual.ChildNodes[0], "arr_dos_dimension"))
                {
                    return new Operacion(analizarNodos(actual.ChildNodes[0]), Operacion.Tipo.EXPRESIONARRAY2, 0, 0);
                }
                else if (EsteNodoEs(actual.ChildNodes[0], "arr_tres_dimension"))
                {
                    return new Operacion(analizarNodos(actual.ChildNodes[0]), Operacion.Tipo.EXPRESIONARRAY3, 0, 0);
                }
            }
            return null;
        }

        private bool EsteNodoEs(ParseTreeNode nodo, string nombre)
        {
            return nodo.Term.Name.Equals(nombre, System.StringComparison.InvariantCultureIgnoreCase);
        }

        private string getLexema(ParseTreeNode nodo)
        {
            return nodo.Token.Text;
        }

        private string getLexema(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Text;
        }

        private int getLinea(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Location.Line;
        }

        private int getColumna(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Location.Column;
        }
    }
}