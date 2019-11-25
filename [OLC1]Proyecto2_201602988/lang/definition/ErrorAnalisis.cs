using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto2_201602988.lang.definition
{
    public class ErrorAnalisis
    {
        public String tipo;
        public String descripcion;
        public int linea;
        public int columna;

        public ErrorAnalisis(String tipo, String descripcion, int linea, int columna)
        {
            this.tipo = tipo;
            this.descripcion = descripcion;
            this.linea = linea;
            this.columna = columna;
        }
    }
}
