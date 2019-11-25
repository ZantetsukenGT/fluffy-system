using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _OLC1_Proyecto2_201602988.lang.engine
{
    public class Contexto
    {
        public List<Contexto> otrosArchivos;
        public string currentFile;
        public List<Simbolo> globales;
        public List<Simbolo> atributos;
        public List<Simbolo> locales_cualquier_nivel;//implementacion lista encadenada, con regla a bloque mas cercano
        public List<Simbolo> locales_mismo_nivel;//control del mismo nivel

        public List<Clase> clases;//definiciones solamente
        public List<Metodo> metodos_globales;
        public List<Metodo> metodos;
        public bool terminable;
        public bool continuable;
        public bool retornable;
        public bool esVoid;

        public Contexto()
        {
            otrosArchivos = new List<Contexto>();
            currentFile = null;
            globales = new List<Simbolo>();
            atributos = new List<Simbolo>();
            locales_cualquier_nivel = new List<Simbolo>();
            locales_mismo_nivel = new List<Simbolo>();

            clases = new List<Clase>();
            metodos_globales = new List<Metodo>();
            metodos = new List<Metodo>();
            terminable = false;
            continuable = false;
            retornable = false;
            esVoid = false;
        }

        public Simbolo findSymbol(String id)
        {
            foreach (Simbolo s in locales_cualquier_nivel)
            {
                if (id.Equals(s.id, StringComparison.InvariantCultureIgnoreCase))
                {
                    return s;
                }
            }
            foreach (Simbolo s in atributos)
            {
                if (id.Equals(s.id, StringComparison.InvariantCultureIgnoreCase))
                {
                    return s;
                }
            }
            foreach (Simbolo s in globales)
            {
                if (id.Equals(s.id, StringComparison.InvariantCultureIgnoreCase))
                {
                    return s;
                }
            }
            foreach (Contexto c in otrosArchivos)
            {
                Simbolo s = c.findSymbol(id);
                if(s != null)
                {
                    return s;
                }
            }
            return null;
        }

        public Simbolo findLocalSame(String id)
        {
            foreach (Simbolo s in locales_mismo_nivel)
            {
                if (id.Equals(s.id, StringComparison.InvariantCultureIgnoreCase))
                {
                    return s;
                }
            }
            return null;
        }

        public Simbolo findAttribute(String id)
        {
            foreach (Simbolo s in atributos)
            {
                if (id.Equals(s.id, StringComparison.InvariantCultureIgnoreCase))
                {
                    return s;
                }
            }
            return null;
        }

        public Simbolo findGlobal(String id)
        {
            foreach (Simbolo s in globales)
            {
                if (id.Equals(s.id, StringComparison.InvariantCultureIgnoreCase))
                {
                    return s;
                }
            }
            return null;
        }

        public Simbolo findGlobalFiles(String id)
        {
            foreach (Simbolo s in globales)
            {
                if (id.Equals(s.id, StringComparison.InvariantCultureIgnoreCase))
                {
                    return s;
                }
            }
            foreach (Contexto c in otrosArchivos)
            {
                Simbolo s = c.findGlobalFiles(id);
                if (s != null)
                {
                    return s;
                }
            }
            return null;
        }

        public Metodo findGlobalMethod(String id, int cantidad)
        {
            foreach (Metodo m in metodos_globales)
            {
                if (id.Equals(m.id, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (m.cantidad_params == cantidad)
                    {
                        return m;
                    }
                }
            }
            return null;
        }

        public Contexto findGlobalContextMethod(Metodo m)
        {
            foreach (Metodo m1 in metodos_globales)
            {
                if (m == m1) return this;
            }
            foreach (Contexto ctx in otrosArchivos)
            {
                foreach (Metodo m2 in ctx.metodos_globales)
                {
                    if (m == m2) return ctx;
                }
            }
            return null;
        }

        public Metodo findGlobalFilesMethod(String id, int cantidad)
        {
            foreach (Metodo m in metodos_globales)
            {
                if (id.Equals(m.id, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (m.cantidad_params == cantidad)
                    {
                        return m;
                    }
                }
            }
            foreach (Contexto c in otrosArchivos)
            {
                Metodo m = c.findGlobalFilesMethod(id, cantidad);
                if (m != null)
                {
                    return m;
                }
            }
            return null;
        }

        public Metodo findLocalMethod(String id, int cantidad)
        {
            foreach (Metodo m in metodos)
            {
                if (id.Equals(m.id, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (m.cantidad_params == cantidad)
                    {
                        return m;
                    }
                }
            }
            return null;
        }
        public Clase findClass(String id)
        {
            foreach (Clase c in clases)
            {
                if (id.Equals(c.id, StringComparison.InvariantCultureIgnoreCase))
                {
                    return c;
                }
            }
            return null;
        }

        public Contexto shallowCopy()//local mas profundo
        {
            Contexto nuevo = new Contexto();
            nuevo.otrosArchivos = otrosArchivos;
            nuevo.currentFile = currentFile;
            nuevo.globales = globales;
            nuevo.atributos = atributos;
            nuevo.metodos = metodos;
            nuevo.metodos_globales = metodos_globales;
            nuevo.clases = clases;
            foreach (Simbolo s in locales_cualquier_nivel)
            {
                nuevo.locales_cualquier_nivel.Add(s);
            }
            nuevo.terminable = terminable;
            nuevo.continuable = continuable;
            nuevo.retornable = retornable;
            nuevo.esVoid = esVoid;
            return nuevo;
        }

        public Contexto deepCopy()//nuevas instancias
        {
            Contexto nuevo = new Contexto();
            nuevo.otrosArchivos = otrosArchivos;
            nuevo.currentFile = currentFile;
            nuevo.globales = globales;
            nuevo.metodos_globales = metodos_globales;
            nuevo.clases = clases;
            nuevo.terminable = terminable;
            nuevo.continuable = continuable;
            nuevo.retornable = retornable;
            nuevo.esVoid = esVoid;
            return nuevo;
        }
    }
}
