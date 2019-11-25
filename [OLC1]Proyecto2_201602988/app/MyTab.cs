using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _OLC1_Proyecto2_201602988.app
{
    public class MyTab : TabPage
    {
        private static int contador = 0;
        public String textoOriginal;
        public String file;

        public MyTab() : base("Untitled " + ++contador + ".fi")
        {
            textoOriginal = "";
            file = null;
            this.Controls.Add(crearTextBoxEntrada());
        }

        public MyTab(String textoOriginal, String file, String titulo) : base(titulo)
        {
            this.textoOriginal = textoOriginal;
            this.file = file;
            this.Controls.Add(crearTextBoxEntrada());
            setTexto(textoOriginal);
        }

        private TextBox crearTextBoxEntrada()
        {
            TextBox area = new TextBox();
            area.Dock = DockStyle.Fill;
            area.Font = new System.Drawing.Font("Lucida Console", 16, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            area.Location = new System.Drawing.Point(4, 4);
            area.Multiline = true;
            area.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            area.Size = new System.Drawing.Size(988, 272);
            return area;
        }

        public String getTexto()
        {
            return this.Controls[0].Text;
        }
        public void setTexto(String texto)
        {
            this.Controls[0].Text = texto;
        }
    }
}
