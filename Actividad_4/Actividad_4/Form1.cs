using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

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

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)  /// Saca las coordenada de los puntos 
        {                                                                     /// que se requieren para los puntos
                                                                              /// del elipse
            if (Band == 0)
            {
                xi = e.X;
                yi = e.Y;

                bmp.SetPixel(xi, yi, Color.Red);

                Band ++;
            }
            else 
            {
                xr = Math.Abs(xi - e.X);
                yr = Math.Abs(yi - e.Y);
                bmp.SetPixel(e.X, e.Y, Color.Red);
                Band =0;
                AlgoritmoDDA(xi, yi, xr, yr);
                AlgoritmoBresenham(xi, yi, xr, yr);
            }
     

            pictureBox1.Image = bmp;
        }

        private double SacarRadio(int x1, int y1, int x2, int y2)  /// FUNCION PARA SACAR LA DISTANCIA ENTRE EL PUNTO A a B y A a C
        {
            double resultado;
            resultado = 0;
            int Dx2 = Math.Abs(x2 - x1);
            int Dy2 = Math.Abs(y2 - y1);
            resultado = (Math.Sqrt(Math.Pow(Dx2, 2) + Math.Pow(Dy2, 2)));
            return resultado;
        }

        private void AlgoritmoDDA(int x1,int y1, double xr,double yr)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            double x, y;
            x = 0;
            y = 0;
           
            double Xr2 = Math.Pow(xr, 2);
            double Yr2 = Math.Pow(yr, 2);
            double Rx2Yr2 = Xr2 * Yr2;
            
            for(x=0; x<xr;x++)
            {
                y = Math.Sqrt((Rx2Yr2 - (Math.Pow(x, 2) * Yr2)) / (Xr2));
                drawCuadrantes(x, Math.Round(y), x1, y1, Color.Black);
            }
            for (y = 0; y < yr; y++)
            {
                x = Math.Sqrt((Rx2Yr2 - (Math.Pow(y, 2) * Xr2)) / (Yr2));
                drawCuadrantes(Math.Round(x), y, x1, y1, Color.Black);
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            timeDDA.Text = String.Format("{0}", ts.TotalMilliseconds);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(600, 500);
            pictureBox1.Image = bmp;
        }

        private void AlgoritmoBresenham(int x1, int y1, double rx, double ry)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double x, y, rx2, ry2, pk, pk2;
            x = 0;
            y = ry;
            rx2 = Math.Pow(rx, 2);
            ry2 = Math.Pow(ry, 2);
            pk = ry2 - (rx2 * ry) + (0.25 * rx2);   ///Inicializacion PK para primera iteracion por cada x iterar en y
            while ((ry2 * x) < (rx2 * y))   ///Cuando radio cuadrado de y sea mayor al radio cuadrado de x iterando con el opuesto para esta iteracion
            {
                if (pk < 0)
                {
                    x++;
                    pk = pk + (2 * ry2 * x) + ry2;
                }
                else
                {
                    x++; y--;
                    pk = pk + (2 * ry2 * x) - (2 * rx2 * y) + ry2;
                }
                drawCuadrantes(x, y, x1, y1, Color.Red);        
            }  ///termina iteraciones en  Pk1 para cambiar a iterar por cada y una x
            pk2 = (ry2) * Math.Pow((x + 0.5), 2) + (rx2) * Math.Pow((y - 1), 2) - (rx2 * ry2);
            while (y > 0)
            {
                if (pk2 > 0)
                {
                    y--;
                    pk2 = pk2 - (2 * rx2 * y) + rx2;
                }
                else
                {
                    x++; y--;
                    pk2 = pk2 + (2 * ry2 * x) - (2 * rx2 * y) + rx2;
                }
                drawCuadrantes(x, y, x1, y1, Color.Red);
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            timeBresenham.Text = String.Format("{0}", ts.TotalMilliseconds);


        }


        void drawCuadrantes(double x, double y, int xc, int yc, Color col)
        {

            if (x + xc > 0 && x + xc < 600 && y + yc > 0 && y + yc < 500)  /// Cuadrante 2
                bmp.SetPixel((int)x + xc, (int)y + yc, col);
            if (-x + xc > 0 && -x + xc < 600 && -y + yc > 0 && -y + yc < 500)  ///CUADRANTE 6
                bmp.SetPixel((int)-x + xc, (int)-y + yc, col);
            if (-x + xc > 0 && -x + xc < 600 && y + yc > 0 && y + yc < 500)  ///Cuadrante 3
                bmp.SetPixel((int)-x + xc, (int)y + yc, col);
            if (x + xc > 0 && x + xc < 600 && -y + yc > 0 && -y + yc < 500)  ///Cuadrante 7
                bmp.SetPixel((int)x + xc, (int)-y + yc, col);
            
            

        }


    }
}
