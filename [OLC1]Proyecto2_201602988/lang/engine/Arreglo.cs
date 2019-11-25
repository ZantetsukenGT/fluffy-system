using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto2_201602988.lang.engine
{
    public class Arreglo
    {
        public List<object> val;
        public int dims;

        public Arreglo(List<object> val)
        {
            this.val = val;
        }

        public Arreglo(int dim1)
        {
            this.val = new List<object>();
            for (int i = 0; i < dim1; i++)
            {
                object o = Simbolo.NULL;
                val.Add(o);
            }
            this.dims = 1;
        }
        public Arreglo(int dim1, int dim2)
        {
            this.val = new List<object>();
            for (int i = 0; i < dim1; i++)
            {
                List<object> l2 = new List<object>();
                for (int j = 0; j < dim2; j++)
                {
                    object o = Simbolo.NULL;
                    l2.Add(o);
                }
                Arreglo arr2 = new Arreglo(l2);
                arr2.dims = 1;
                this.val.Add(arr2);
            }
            this.dims = 2;
        }

        public Arreglo(int dim1, int dim2, int dim3)
        {
            this.val = new List<object>();
            for (int i = 0; i < dim1; i++)
            {
                List<object> l2 = new List<object>();
                for (int j = 0; j < dim2; j++)
                {
                    List<object> l3 = new List<object>();
                    for (int k = 0; k < dim3; k++)
                    {
                        object o = Simbolo.NULL;
                        l3.Add(o);
                    }
                    Arreglo arr3 = new Arreglo(l3);
                    arr3.dims = 1;
                    l2.Add(arr3);
                }
                Arreglo arr2 = new Arreglo(l2);
                arr2.dims = 2;
                this.val.Add(arr2);
            }
            this.dims = 3;
        }

        override public String ToString()
        {
            StringBuilder result = new StringBuilder("[");
            for (int i = 0; i < val.Count; i++)
            {
                object o = val.ElementAt(i);
                if (i == val.Count - 1)
                {
                    if (o is Null)
                    {
                        result.Append("NULL");
                    }
                    else
                    {
                        result.Append(o.ToString());
                    }
                }
                else
                {
                    if (o is Null)
                    {
                        result.Append("NULL,");
                    }
                    else
                    {
                        result.Append(o.ToString());
                        result.Append(",");
                    }
                }
            }
            result.Append("]");
            return result.ToString();
        }
    
        public object deepCopy()//para usar en instancias de la misma clase
        {
            List<object> nuevos = new List<object>();
            foreach (object o in val)
            {
                if (o is Null)
                {
                    nuevos.Add(o);
                }
                if (o is BigInteger)
                {
                    nuevos.Add(o);
                }
                else if (o is double)
                {
                    nuevos.Add(o);
                }
                else if (o is char)
                {
                    nuevos.Add(o);
                }
                else if (o is bool)
                {
                    nuevos.Add(o);
                }
                else if (o is string)
                {
                    nuevos.Add(o);
                }
                else if (o is Arreglo a)
                {
                    nuevos.Add(a.deepCopy());
                }
                else if (o is Objeto ob)
                {
                    nuevos.Add(ob.deepCopy());
                }
            }
            Arreglo nuevo = new Arreglo(nuevos);
            nuevo.dims = this.dims;
            return nuevo;
        }
    }
}
