using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto2_201602988.lang.engine
{
    public class Void
    {
        public string objeto;
        public string metodo;
        public int paramss;
        public Void()
        {
            this.objeto = null;
            this.metodo = null;
            this.paramss = -1;
        }
        public Void(string metodo, int paramss)
        {
            this.objeto = null;
            this.metodo = metodo;
            this.paramss = paramss;
        }
        public Void(string objeto, string metodo, int paramss)
        {
            this.objeto = objeto;
            this.metodo = metodo;
            this.paramss = paramss;
        }

        public override string ToString()
        {
            if(objeto == null && metodo == null)
            {
                return "";
            }
            else if (objeto == null && metodo != null)
            {
                return "'" + metodo + "' de " + paramss + "parametros";
            }
            else
            {
                return "'" + objeto + "." + metodo + "' de " + paramss + "parametros";
            }
        }
    }
}
