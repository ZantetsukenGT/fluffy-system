using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _OLC1_Proyecto2_201602988.app.tree;

namespace _OLC1_Proyecto2_201602988.lang.engine
{
    public class Metodo
    {
        public bool esVoid;
        public string id;
        public Instruccion decl_params;
        public int cantidad_params;
        public List<Instruccion> listaInstrucciones;

        public Metodo(bool esVoid, string id, Instruccion decl_params, int cantidad_params, List<Instruccion> listaInstrucciones)
        {
            this.esVoid = esVoid;
            this.id = id;
            this.decl_params = decl_params;
            this.cantidad_params = cantidad_params;
            this.listaInstrucciones = listaInstrucciones;
        }
    }
}
