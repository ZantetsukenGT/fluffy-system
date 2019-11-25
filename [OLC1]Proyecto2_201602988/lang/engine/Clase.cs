using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto2_201602988.lang.engine
{
    public class Clase
    {
        public string id;
        public List<app.tree.Instruccion> miembros;
        public Contexto signature;

        public Clase(string id)
        {
            this.id = id;
            this.signature = new Contexto();
        }
    }
}
