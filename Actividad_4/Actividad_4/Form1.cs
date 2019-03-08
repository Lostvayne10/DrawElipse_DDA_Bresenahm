using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Actividad_4
{
    public partial class Form1 : Form
    {
        Bitmap bmp = new Bitmap(600, 500);
        int Band;
        int  xi, yi; 
        double xr, yr;
        public Form1()
        {
            InitializeComponent();
            Band = 0;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(Band == 0)
            {
                xi = e.X;
                yi = e.Y;

                bmp.SetPixel(xi, yi, Color.Red);

                Band ++;
            }
            else if(Band == 1)
            {
                xr = SacarRadio(xi,yi,e.X,e.Y);
                bmp.SetPixel(e.X, e.Y, Color.Red);
                Band ++;

                MessageBox.Show("Resultado xr: " + xr);
            }
            else
            {
                yr = SacarRadio(xi, yi, e.X, e.Y);
                bmp.SetPixel(e.X, e.Y, Color.Red);
                Band = 0;

                MessageBox.Show("Resultado yr: " + yr);
            }

            pictureBox1.Image = bmp;
        }

        private double SacarRadio(int x1, int y1, int x2, int y2)
        {
            double resultado;
            resultado = 0;
            int Dx2 = Math.Abs(x2 - x1);
            int Dy2 = Math.Abs(y2 - y1);
            resultado = (Math.Sqrt(Math.Pow(Dx2, 2) + Math.Pow(Dy2, 2)));
            return resultado;
        }

    }
}
