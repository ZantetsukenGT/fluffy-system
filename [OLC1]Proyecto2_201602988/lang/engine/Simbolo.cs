using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.app.tree;

namespace _OLC1_Proyecto2_201602988.lang.engine
{
    public class Simbolo
    {
        public static Null NULL = new Null();

        public String id;
        public Object value;

        public Simbolo(String id, Object value)
        {
            this.id = id;
            this.value = value;
        }

        public Simbolo deepCopy()
        {
            if (value is Arreglo)
            {
                Arreglo r1 = (Arreglo)value;
                return new Simbolo(this.id, r1.deepCopy());
            }
            if (value is Objeto)
            {
                Objeto r1 = (Objeto)value;
                return new Simbolo(this.id, r1.deepCopy());
            }
            return new Simbolo(this.id, value);
        }
    }
}
