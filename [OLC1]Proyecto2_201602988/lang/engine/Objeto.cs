using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto2_201602988.lang.engine
{
    public class Objeto
    {
        public string nombre_clase;
        public Contexto atributos;

        public Objeto(string nombre_clase, Contexto atributos)
        {
            this.nombre_clase = nombre_clase;
            this.atributos = atributos;
        }

        public Objeto deepCopy()
        {
            return new Objeto(nombre_clase, atributos.deepCopy());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[Objeto: '" + nombre_clase + "']");
            return sb.ToString();
        }
    }
}
