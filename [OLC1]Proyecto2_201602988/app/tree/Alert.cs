using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.lang.engine;
using System.Windows.Forms;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public class Alert : Instruccion
    {
        private Instruccion op;
        int fila, columna;
        public Alert(int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;
            this.op = new Operacion("", Operacion.Tipo.CADENA, 0, 0);
        }
        public Alert(Instruccion op, int fila, int columna)
        {
            this.fila = fila;
            this.columna = columna;
            this.op = op;
        }

        public Object ejecutar(Contexto ctx, Stuff stuff)
        {
            Object body = Operacion.Validar(op.ejecutar(ctx, stuff), ctx, stuff, getLinea(), getColumna());
            if (body != null)
            {
                MessageBox.Show(body.ToString(), "Alert");
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
