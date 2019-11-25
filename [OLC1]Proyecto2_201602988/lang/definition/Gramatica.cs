using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;

namespace _OLC1_Proyecto2_201602988.lang.definition
{
    public class Gramatica : Grammar
    {
        public Gramatica() : base(caseSensitive: false)
        {
            #region TERMINALES


            #region Reservadas
            KeyTerm
                RVAR = ToTerm("var", "RVAR"),
                RNULL = ToTerm("null", "RNULL"),
                RLOG = ToTerm("log", "RLOG"),
                RALERT = ToTerm("alert", "RALERT"),
                RGRAPH = ToTerm("graph", "RGRAPH"),
                RIF = ToTerm("if", "RIF"),
                RELSE = ToTerm("else", "RELSE"),
                RDO = ToTerm("do", "RDO"),
                RWHILE = ToTerm("while", "RWHILE"),
                RFOR = ToTerm("for", "RFOR"),
                RSWITCH = ToTerm("switch", "RSWITCH"),
                RCASE = ToTerm("case", "RCASE"),
                RDEFAULT = ToTerm("default", "RDEFAULT"),
                RBREAK = ToTerm("break", "RBREAK"),
                RCONTINUE = ToTerm("continue", "RCONTINUE"),
                RRETURN = ToTerm("return", "RRETURN"),
                RIMPORTAR = ToTerm("importar", "RIMPORTAR"),
                RTRUE = ToTerm("true", "RTRUE"),
                RFALSE = ToTerm("false", "RFALSE"),
                RCLASS = ToTerm("class", "RCLASS"),
                RNEW = ToTerm("new", "RNEW"),
                RFUNCTION = ToTerm("function", "RFUNCTION"),
                RVOID = ToTerm("void", "RVOID");

            MarkReservedWords("var","null","log","alert","graph","if","else","do","while","for","switch",
                              "case","default","break","continue","return","importar","true","false","class",
                              "new","function","void");
            #endregion

            #region Operadores
            Terminal 
                PARENTA = ToTerm("(", "PARENTA"),
                PARENTC = ToTerm(")", "PARENTC"),
                CORCHETEA = ToTerm("[", "CORCHETEA"),
                CORCHETEC = ToTerm("]", "CORCHETEC"),
                LLAVEA = ToTerm("{", "LLAVEA"),
                LLAVEC = ToTerm("}", "LLAVEC"),
                SIGNOIGUAL = ToTerm("=", "SIGNOIGUAL"),
                IGUALQUE = ToTerm("==", "IGUALQUE"),
                DISTINTOQUE = ToTerm("<>", "DISTINTOQUE"),
                MENORQUE = ToTerm("<", "MENORQUE"),
                MENORIGUALQUE = ToTerm("<=", "MENORIGUALQUE"),
                MAYORQUE = ToTerm(">", "MAYORQUE"),
                MAYORIGUALQUE = ToTerm(">=", "MAYORIGUALQUE"),
                AND = ToTerm("&&", "AND"),
                OR = ToTerm("||", "OR"),
                XOR = ToTerm("^", "XOR"),
                NOT = ToTerm("!", "NOT"),
                SIGNOMAS = ToTerm("+", "SIGNOMAS"),
                AUMENTO = ToTerm("++", "AUMENTO"),
                SIGNOMENOS = ToTerm("-", "SIGNOMENOS"),
                DECREMENTO = ToTerm("--", "DECREMENTO"),
                SIGNOPOR = ToTerm("*", "SIGNOPOR"),
                SIGNODIVIDIDO = ToTerm("/", "SIGNODIVIDIDO"),
                SIGNOPOTENCIA = ToTerm("pow", "SIGNOPOTENCIA"),
                PUNTO = ToTerm(".", "PUNTO"),
                COMA = ToTerm(",", "COMA"),
                PYCOMA = ToTerm(";", "PYCOMA"),
                DOSPUNTOS = ToTerm(":", "DOSPUNTOS");
            #endregion

            #region ER

            NumberLiteral NUMERO = new NumberLiteral("NUMERO");
            IdentifierTerminal IDENTIFICADOR = new IdentifierTerminal("IDENTIFICADOR");
            StringLiteral CADENA = new StringLiteral("CADENA", "\"", StringOptions.AllowsAllEscapes);
            StringLiteral CHAR = new StringLiteral("CHAR", "\'", StringOptions.IsChar);
            #endregion

            #region Comentarios
            CommentTerminal comentarioMultilinea = new CommentTerminal("comentarioMultiLinea", "/*", "*/");
            CommentTerminal comentario = new CommentTerminal("comentarioUniLinea", "//", "\n", "\r\n");
            Terminal espacio = ToTerm(" ");
            Terminal tab = ToTerm("\t");

            base.NonGrammarTerminals.Add(comentarioMultilinea);
            base.NonGrammarTerminals.Add(comentario);
            base.NonGrammarTerminals.Add(espacio);
            base.NonGrammarTerminals.Add(tab);
            #endregion

            #endregion

            #region NoTerminales
            NonTerminal inicio = new NonTerminal("inicio");
            NonTerminal instrucciones_global = new NonTerminal("instrucciones_global");
            NonTerminal instruccion_global = new NonTerminal("instruccion_global");
            NonTerminal declaracion_clase = new NonTerminal("declaracion_clase");
            NonTerminal miembros_clase = new NonTerminal("miembros_clase");
            NonTerminal miembro_clase = new NonTerminal("miembro_clase");
            NonTerminal definicion_metodo = new NonTerminal("definicion_metodo");
            NonTerminal definicion_metodo_global = new NonTerminal("definicion_metodo_global");
            NonTerminal decl_lista_parametros = new NonTerminal("decl_lista_parametros");
            NonTerminal decl_parametro = new NonTerminal("decl_parametro");
            NonTerminal instrucciones = new NonTerminal("instrucciones");
            NonTerminal instruccion = new NonTerminal("instruccion");
            NonTerminal instruccion_for = new NonTerminal("instruccion_for");
            NonTerminal instruccion_while = new NonTerminal("instruccion_while");
            NonTerminal instruccion_dowhile = new NonTerminal("instruccion_dowhile");
            NonTerminal instruccion_switch = new NonTerminal("instruccion_switch");
            NonTerminal lista_casos = new NonTerminal("lista_casos");
            NonTerminal caso = new NonTerminal("caso");
            NonTerminal caso_default = new NonTerminal("caso_default");
            NonTerminal identificadores = new NonTerminal("identificadores");
            NonTerminal declaracion_asignacion = new NonTerminal("declaracion_asignacion");
            NonTerminal asignacion = new NonTerminal("asignacion");
            NonTerminal asig_acceso_celda = new NonTerminal("asig_acceso_celda");
            NonTerminal declaracion = new NonTerminal("declaracion");
            NonTerminal declaraciones_con_valor = new NonTerminal("declaraciones_con_valor");
            NonTerminal declaracion_con_valor = new NonTerminal("declaracion_con_valor");
            NonTerminal declaracion_global = new NonTerminal("declaracion_global");
            NonTerminal declaraciones_con_valor_global = new NonTerminal("declaraciones_con_valor_global");
            NonTerminal declaracion_con_valor_global = new NonTerminal("declaracion_con_valor_global");
            NonTerminal decl_celdas = new NonTerminal("decl_celdas");
            NonTerminal arr_una_dimension = new NonTerminal("arr_una_dimension");
            NonTerminal arr_dos_dimension = new NonTerminal("arr_dos_dimension");
            NonTerminal arr_dos_dimension_aux = new NonTerminal("arr_dos_dimension_aux");
            NonTerminal arr_tres_dimension = new NonTerminal("arr_tres_dimension");
            NonTerminal arr_tres_dimension_aux = new NonTerminal("arr_tres_dimension_aux");
            NonTerminal bloque_if_elseif_else = new NonTerminal("bloque_if_elseif_else");
            NonTerminal instruccion_if = new NonTerminal("instruccion_if");
            NonTerminal instrucciones_elseif = new NonTerminal("instrucciones_elseif");
            NonTerminal instruccion_elseif = new NonTerminal("instruccion_elseif");
            NonTerminal instruccion_else = new NonTerminal("instruccion_else");
            NonTerminal expresion_full = new NonTerminal("expresion_full");
            NonTerminal expresion = new NonTerminal("expresion");
            NonTerminal id = new NonTerminal("id");
            NonTerminal llamada_metodo = new NonTerminal("llamada_metodo");
            NonTerminal lista_expresiones = new NonTerminal("lista_expresiones");
            NonTerminal expresion_array = new NonTerminal("expresion_array");
            #endregion

            #region Gramatica

            inicio.Rule =//skip
               /*1*/    instrucciones_global;//
            
            instrucciones_global.Rule =//cv
               /*N*/ MakePlusRule(instrucciones_global, instruccion_global);//
            
            instruccion_global.Rule =//c
               /*1*/    declaracion_clase//
               /*1*/ | declaracion_global + PYCOMA//
               /*1*/ | asignacion + PYCOMA//
               /*4*/ | RIMPORTAR + PARENTA + expresion + PARENTC + PYCOMA//
               /*3*/ | RLOG + PARENTA + PARENTC + PYCOMA//
               /*4*/ | RLOG + PARENTA + expresion_full + PARENTC + PYCOMA//
               /*3*/ | RALERT + PARENTA + PARENTC + PYCOMA//
               /*4*/ | RALERT + PARENTA + expresion_full + PARENTC + PYCOMA//
               /*5*/ | RGRAPH + PARENTA + expresion + COMA + expresion + PARENTC + PYCOMA//
               /*1*/ | bloque_if_elseif_else//
               /*1*/ | instruccion_for//
               /*1*/ | instruccion_while//
               /*1*/ | instruccion_dowhile//
               /*1*/ | instruccion_switch//
               /*1*/ | RBREAK + PYCOMA//
               /*1*/ | RCONTINUE + PYCOMA//
               /*2*/ | RRETURN + expresion_full + PYCOMA//
               /*1*/ | expresion_full + PYCOMA//
               /*3*/ | IDENTIFICADOR + PARENTA + PARENTC + LLAVEA + LLAVEC//
               /*4*/ | IDENTIFICADOR + PARENTA + PARENTC + LLAVEA + instrucciones + LLAVEC//
               /*1*/ | definicion_metodo_global + LLAVEA + LLAVEC//
               /*2*/ | definicion_metodo_global + LLAVEA + instrucciones + LLAVEC;//

            declaracion_clase.Rule =//cv
               /*1*/    RCLASS + IDENTIFICADOR + LLAVEA + LLAVEC//
               /*2*/ | RCLASS + IDENTIFICADOR + LLAVEA + miembros_clase + LLAVEC;//

            miembros_clase.Rule =//cv
               /*N*/ MakePlusRule(miembros_clase, miembro_clase);
               
            miembro_clase.Rule =//cv
               /*1*/    declaracion_asignacion + PYCOMA//
               /*1*/ | definicion_metodo + LLAVEA + LLAVEC//
               /*2*/ | definicion_metodo + LLAVEA + instrucciones + LLAVEC;//

            definicion_metodo.Rule =//cv
               /*3*/    RFUNCTION + IDENTIFICADOR + PARENTA + PARENTC//
               /*4*/ | RFUNCTION + IDENTIFICADOR + PARENTA + decl_lista_parametros + PARENTC//
               /*4*/ | RFUNCTION + RVOID + IDENTIFICADOR + PARENTA + PARENTC//
               /*5*/ | RFUNCTION + RVOID + IDENTIFICADOR + PARENTA + decl_lista_parametros + PARENTC;//
               
            definicion_metodo_global.Rule =//cv
               /*3*/    RFUNCTION + IDENTIFICADOR + PARENTA + PARENTC//
               /*4*/ | RFUNCTION + IDENTIFICADOR + PARENTA + decl_lista_parametros + PARENTC//
               /*4*/ | RFUNCTION + RVOID + IDENTIFICADOR + PARENTA + PARENTC//
               /*5*/ | RFUNCTION + RVOID + IDENTIFICADOR + PARENTA + decl_lista_parametros + PARENTC;//
               
            decl_lista_parametros.Rule =//cv
               /*N*/ MakePlusRule(decl_lista_parametros, COMA, decl_parametro);//

            decl_parametro.Rule =//skip
               /*1*/ RVAR + IDENTIFICADOR;//

            instrucciones.Rule =//cv
               /*N*/ MakePlusRule(instrucciones, instruccion);//

            instruccion.Rule =//cv
               /*1*/    declaracion_asignacion + PYCOMA//
               /*3*/ | RLOG + PARENTA + PARENTC + PYCOMA//
               /*4*/ | RLOG + PARENTA + expresion_full + PARENTC + PYCOMA
               /*3*/ | RALERT + PARENTA + PARENTC + PYCOMA//
               /*4*/ | RALERT + PARENTA + expresion_full + PARENTC + PYCOMA
               /*5*/ | RGRAPH + PARENTA + expresion + COMA + expresion + PARENTC + PYCOMA//
               /*1*/ | bloque_if_elseif_else//
               /*1*/ | instruccion_for//
               /*1*/ | instruccion_while//
               /*1*/ | instruccion_dowhile//
               /*1*/ | instruccion_switch//
               /*1*/ | RBREAK + PYCOMA//
               /*1*/ | RCONTINUE + PYCOMA//
               /*2*/ | RRETURN + expresion_full + PYCOMA
               /*1*/ | expresion_full + PYCOMA;//

            instruccion_for.Rule =//cv
               /*5*/    RFOR + PARENTA + declaracion_asignacion + PYCOMA + expresion + PYCOMA + expresion + PARENTC + LLAVEA + LLAVEC//
               /*6*/ | RFOR + PARENTA + declaracion_asignacion + PYCOMA + expresion + PYCOMA + expresion + PARENTC + LLAVEA + instrucciones + LLAVEC//
               /*5*/ | RFOR + PARENTA + declaracion_asignacion + PYCOMA + expresion + PYCOMA + asignacion + PARENTC + LLAVEA + LLAVEC//
               /*6*/ | RFOR + PARENTA + declaracion_asignacion + PYCOMA + expresion + PYCOMA + asignacion + PARENTC + LLAVEA + instrucciones + LLAVEC;//

            instruccion_while.Rule =//cv
               /*3*/    RWHILE + PARENTA + expresion + PARENTC + LLAVEA + LLAVEC//
               /*4*/ | RWHILE + PARENTA + expresion + PARENTC + LLAVEA + instrucciones + LLAVEC;//

            instruccion_dowhile.Rule =//cv
               /*3*/    RDO + LLAVEA + LLAVEC + RWHILE + PARENTA + expresion + PARENTC + PYCOMA//
               /*4*/ | RDO + LLAVEA + instrucciones + LLAVEC + RWHILE + PARENTA + expresion + PARENTC + PYCOMA;//

            instruccion_switch.Rule =//cv
               /*3*/    RSWITCH + PARENTA + expresion + PARENTC + LLAVEA + LLAVEC//
               /*4*/ | RSWITCH + PARENTA + expresion + PARENTC + LLAVEA + caso_default + LLAVEC//
               /*4*/ | RSWITCH + PARENTA + expresion + PARENTC + LLAVEA + lista_casos + LLAVEC//
               /*5*/ | RSWITCH + PARENTA + expresion + PARENTC + LLAVEA + lista_casos + caso_default + LLAVEC;//

            lista_casos.Rule =//cv
               /*N*/    MakePlusRule(lista_casos, caso);//
            
            caso.Rule =//cv
               /*1*/    RCASE + expresion + DOSPUNTOS//
               /*2*/ | RCASE + expresion + DOSPUNTOS + instrucciones;//

            caso_default.Rule =//cv
               /*0*/    RDEFAULT + DOSPUNTOS//
               /*1*/ | RDEFAULT + DOSPUNTOS + instrucciones;//
            
            identificadores.Rule =//cv
               /*N*/    MakePlusRule(identificadores, COMA, IDENTIFICADOR);//

            declaracion_asignacion.Rule =//cv
               /*1*/    declaracion//
               /*1*/ | asignacion;//

            asignacion.Rule =//cv
               /*1*/    id + SIGNOIGUAL + expresion;
            
            asig_acceso_celda.Rule =//cv
               /*1*/    CORCHETEA + expresion + CORCHETEC;//

            declaracion.Rule =//cv
               /*1*/    RVAR + identificadores//
               /*2*/ | RVAR + identificadores + decl_celdas//
               /*1*/ | RVAR + declaraciones_con_valor//
               /*2*/ | RVAR + declaraciones_con_valor + COMA + identificadores//
               /*3*/ | RVAR + declaraciones_con_valor + COMA + identificadores + decl_celdas;//

            declaraciones_con_valor.Rule =//cv
               /*N*/    MakePlusRule(declaraciones_con_valor, COMA, declaracion_con_valor);//

            declaracion_con_valor.Rule =//cv
               /*2*/    identificadores + SIGNOIGUAL + expresion_full//
               /*3*/ | identificadores + decl_celdas + SIGNOIGUAL + expresion_full;//
               
            declaracion_global.Rule =//cv
               /*1*/    RVAR + identificadores//
               /*2*/ | RVAR + identificadores + decl_celdas//
               /*1*/ | RVAR + declaraciones_con_valor_global//
               /*2*/ | RVAR + declaraciones_con_valor_global + COMA + identificadores//
               /*3*/ | RVAR + declaraciones_con_valor_global + COMA + identificadores + decl_celdas;//

            declaraciones_con_valor_global.Rule =//cv
               /*N*/    MakePlusRule(declaraciones_con_valor_global, COMA, declaracion_con_valor_global);//

            declaracion_con_valor_global.Rule =//cv
               /*2*/    identificadores + SIGNOIGUAL + expresion_full//
               /*3*/ | identificadores + decl_celdas + SIGNOIGUAL + expresion_full;//

            decl_celdas.Rule =//cv
               /*1*/    CORCHETEA + expresion + CORCHETEC//
               /*2*/ | CORCHETEA + expresion + CORCHETEC + CORCHETEA + expresion + CORCHETEC
               /*3*/ | CORCHETEA + expresion + CORCHETEC + CORCHETEA + expresion + CORCHETEC + CORCHETEA + expresion + CORCHETEC;

            arr_una_dimension.Rule =//cv
               /*N*/ MakePlusRule(arr_una_dimension, COMA, expresion);

            arr_dos_dimension.Rule =//cv
               /*N*/ MakePlusRule(arr_dos_dimension, COMA, arr_dos_dimension_aux);
            arr_dos_dimension_aux.Rule =//skip
               /*skip*/ LLAVEA + arr_una_dimension + LLAVEC;

            arr_tres_dimension.Rule =//cv
               /*N*/ MakePlusRule(arr_tres_dimension, COMA, arr_tres_dimension_aux);
            arr_tres_dimension_aux.Rule =//skip
               /*skip*/ LLAVEA + arr_dos_dimension + LLAVEC;

            bloque_if_elseif_else.Rule =//cv
               /*1*/     instruccion_if//
               /*2*/ | instruccion_if + instrucciones_elseif//
               /*2*/ | instruccion_if + instruccion_else//
               /*3*/ | instruccion_if + instrucciones_elseif + instruccion_else;//

            instruccion_if.Rule =//cv
               /*3*/     RIF + PARENTA + expresion + PARENTC + LLAVEA + LLAVEC//
               /*4*/ | RIF + PARENTA + expresion + PARENTC + LLAVEA + instrucciones + LLAVEC;//

            instrucciones_elseif.Rule =//cv
               /*N*/ MakePlusRule(instrucciones_elseif, instruccion_elseif);//

            instruccion_elseif.Rule =//cv
               /*3*/     RELSE + RIF + PARENTA + expresion + PARENTC + LLAVEA + LLAVEC//
               /*4*/ | RELSE + RIF + PARENTA + expresion + PARENTC + LLAVEA + instrucciones + LLAVEC;//

            instruccion_else.Rule =//cv
               /*0*/     RELSE + LLAVEA + LLAVEC//
               /*1*/ | RELSE + LLAVEA + instrucciones + LLAVEC;//

            expresion_full.Rule =//cv
               /*1*/    expresion//
               /*1*/ | expresion_array;//
               
            expresion.Rule =//cv
               /*3*/    expresion + SIGNOMAS + expresion//
               /*3*/ | expresion + SIGNOMENOS + expresion//
               /*3*/ | expresion + SIGNODIVIDIDO + expresion//
               /*3*/ | expresion + SIGNOPOR + expresion//
               /*3*/ | expresion + SIGNOPOTENCIA + expresion//
               /*3*/ | expresion + MAYORQUE + expresion//
               /*3*/ | expresion + MENORQUE + expresion//
               /*3*/ | expresion + MAYORIGUALQUE + expresion//
               /*3*/ | expresion + MENORIGUALQUE + expresion//
               /*3*/ | expresion + IGUALQUE + expresion//
               /*3*/ | expresion + DISTINTOQUE + expresion//
               /*3*/ | expresion + OR + expresion//
               /*3*/ | expresion + AND + expresion//
               /*3*/ | expresion + XOR + expresion//
               /*2*/ | expresion + AUMENTO//
               /*2*/ | expresion + DECREMENTO//
               /*2*/ | NOT + expresion//
               /*2*/ | SIGNOMENOS + expresion//
               /*3*/ | PARENTA + expresion + PARENTC//
               /*1*/ | id//
               /*1*/ | CADENA//
               /*1*/ | NUMERO//
               /*1*/ | CHAR//
               /*1*/ | RTRUE//
               /*1*/ | RFALSE//
               /*1*/ | RNULL//
               /*4*/ | RNEW + IDENTIFICADOR + PARENTA + PARENTC//
               /*5*/ | RNEW + IDENTIFICADOR + PARENTA + PARENTC + PUNTO + id;//
               
            id.Rule =//cv
               /*2*/    id + PUNTO + id//
               /*2*/ | id + asig_acceso_celda//
               /*1*/ | IDENTIFICADOR//
               /*2*/ | IDENTIFICADOR + llamada_metodo;//

            llamada_metodo.Rule =//cv
               /*2*/    PARENTA + PARENTC//
               /*3*/ | PARENTA + lista_expresiones + PARENTC;//

            lista_expresiones.Rule =//cv
               /*N*/    MakePlusRule(lista_expresiones, COMA, expresion_full);//

            expresion_array.Rule =//cv
               /*1*/     LLAVEA + arr_una_dimension + LLAVEC//
               /*1*/ |  LLAVEA + arr_dos_dimension + LLAVEC//
               /*1*/ |  LLAVEA + arr_tres_dimension + LLAVEC;//

            #endregion

            #region Precedencia
            RegisterOperators(1, Associativity.Left, OR);
            RegisterOperators(2, Associativity.Left, AND);
            RegisterOperators(3, Associativity.Left, XOR);
            RegisterOperators(4, Associativity.Left, DISTINTOQUE, IGUALQUE);
            RegisterOperators(5, Associativity.Neutral, MENORQUE, MAYORQUE, MENORIGUALQUE, MAYORIGUALQUE);
            RegisterOperators(6, Associativity.Left, SIGNOMAS, SIGNOMENOS);
            RegisterOperators(7, Associativity.Left, SIGNOPOR, SIGNODIVIDIDO);
            RegisterOperators(8, Associativity.Right, SIGNOPOTENCIA);
            RegisterOperators(9, Associativity.Right, NOT);
            RegisterOperators(10, Associativity.Neutral, AUMENTO, DECREMENTO);
            RegisterOperators(11, Associativity.Left, PUNTO, CORCHETEA, CORCHETEC, PARENTA, PARENTC);
            #endregion

            MarkPunctuation(RVAR, PYCOMA, COMA, DOSPUNTOS, PUNTO, LLAVEA, LLAVEC, CORCHETEA, CORCHETEC, SIGNOIGUAL, RCLASS, RFUNCTION, RIF, RELSE, RFOR, RDO, RWHILE, RSWITCH, RCASE, RDEFAULT);
            MarkTransient(arr_dos_dimension_aux, arr_tres_dimension_aux, decl_parametro, inicio);
            this.Root = inicio;
        }
    }

}
