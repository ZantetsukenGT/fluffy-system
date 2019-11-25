using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _OLC1_Proyecto2_201602988.lang.engine;

namespace _OLC1_Proyecto2_201602988.app.tree
{
    public interface Instruccion
    {
        Object ejecutar(Contexto ctx, Stuff stuff);
        int getLinea();
        int getColumna();
    }
}
