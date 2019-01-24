using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace zadanie1
{
    public partial class _Default : Page
    {
        private Random rnd = new Random();

        private Bitmap DrawMandel(int width, int height, double Sx, double Sy, double Fx, double Fy)
        {
            // Holds all of the possible colors
            Color[] cs = new Color[256];
            for (int i = 0; i < 256; i++)
            {
                cs[i] = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)); ;
            }
            // Fills cs with the colors from the current ColorMap file
            //cs = GetColors(ColMaps[CurColMap]);
            // Creates the Bitmap we draw to
            Bitmap b = new Bitmap(width, height);
            // From here on out is just converted from the c++ version.
            double x, y, x1, y1, xx, xmin, xmax, ymin, ymax = 0.0;

            //int looper, z = 0;
            double intigralX, intigralY = 0.0;
            xmin = Sx; // Start x value, normally -2.1
            ymin = Sy; // Start y value, normally -1.3
            xmax = Fx; // Finish x value, normally 1
            ymax = Fy; // Finish y value, normally 1.3
            intigralX = (xmax - xmin) / width; // Make it fill the whole window
            intigralY = (ymax - ymin) / height;
            x = xmin;

            int[,] colorTable = new int[width,height];

            for (int s = 0; s < width; s++)
            //Parallel.For(0, width, s =>
              {
                  y = ymin;

                  for (int z = 0; z < height; z++)
                  {
                      x1 = 0;
                      y1 = 0;
                      int looper = 0;

                      while (looper < 100 && Math.Sqrt((x1 * x1) + (y1 * y1)) < 2)
                      {
                          looper++;
                          xx = (x1 * x1) - (y1 * y1) + x;
                          y1 = 2 * x1 * y1 + y;
                          x1 = xx;
                      }

                    //Parallel.For (0,100,l =>
                    //{
                    //    looper=l+1;
                    //    xx = (x1 * x1) - (y1 * y1) + x;
                    //    y1 = 2 * x1 * y1 + y;
                    //    x1 = xx;
                    //});

                    // Get the percent of where the looper stopped
                    double perc = looper / (100.0);
                    // Get that part of a 255 scale
                    int val = ((int)(perc * 255));
                    // Use that number to set the color
                    //b.SetPixel(s, z, cs[val]);
                    colorTable[s, z] = val;
                      y += intigralY;
                  }
                  x += intigralX;
              }//);

            for (int i=1; i<width; i++)
            {
                for (int j=1; j<height; j++)
                {
                    b.SetPixel(i, j, cs[colorTable[i - 1, j - 1]]);
                }
            }

            return b;
        }

        private double[,] WczytajMacierz(string matrixPath)
        {
            string serverPath = Server.MapPath(matrixPath);
            string macierz_input = File.ReadAllText(serverPath);
            string[] macierzWiersze = macierz_input.Split(';');
            string[] macierzKolumny = macierzWiersze[0].Split(',');
            int wiersze = 0, kolumny = 0;
            double[,] macierz;
            foreach (string wiersz in macierzWiersze)
            {
                wiersze += 1;
            }
            foreach (string kolumna in macierzKolumny)
            {
                kolumny += 1;
            }

            macierz = new double[wiersze, kolumny];
            for (int i = 0; i < wiersze; i++)
            {
                string[] elementyWiersza = macierzWiersze[i].Split(',');
                for (int j = 0; j < kolumny; j++)
                {
                    macierz[i, j] = Convert.ToDouble(elementyWiersza[j]);
                }
            }

            //for (int i = 0; i < wiersze; i++)
            //{
            //    for (int j = 0; j < kolumny; j++)
            //    {
            //        Response.Write(macierz[i, j] + "/");
            //    }
            //    Response.Write("//");
            //}

            return macierz;
        }

        private void WypiszMacierz(double[,] m, long ms, long c)
        {
            string macierz = "[";

            int wiersze = m.GetLength(0);
            int kolumny = m.GetLength(1);

            for (int i = 0; i < wiersze; i++)
            {
                for (int j = 0; j < kolumny; j++)
                {
                    macierz += m[i, j];

                    if (j == kolumny - 1)
                        break;
                    else
                        macierz += ",";
                }

                if (i<wiersze-1)
                    macierz += "\r\n";
            }

            macierz += "]";

            TextBox1.Text = macierz += "\r\n\r\nOperacja trwała:\r\n" + ms + " (milisekundy)\r\n" + c +" (cykle zegara)";

        }

        private double[,] MnozenieMacierzy(double[,] m1, double[,] m2)
        {           
            int m1_wiersze = m1.GetLength(0);
            int m1_kolumny = m1.GetLength(1);
            int m2_wiersze = m2.GetLength(0);
            int m2_kolumny = m2.GetLength(1);

            double[,] result;

            if ((m1_kolumny != m2_wiersze) && (m2_kolumny != m1_wiersze))
            {
                result = new double[1, 1];
                Label1.Text = "Nieodpowiednie wymiary macierzy! Ilość kolumn pierwszej macierzy musi odpowiadać ilości wierszy drugiej macierzy.";              
            }
            else if ((m1_kolumny != m2_wiersze) && (m2_kolumny == m1_wiersze))
            {
                double[,] m1_new = new double[m2_wiersze, m2_kolumny];
                double[,] m2_new = new double[m1_wiersze, m1_kolumny];

                for (int i = 0; i < m1_wiersze; i++)
                {
                    for (int j = 0; j < m1_kolumny; j++)
                    {
                        m2_new[i, j] = m1[i, j];
                    }
                }

                for (int i = 0; i < m2_wiersze; i++)
                {
                    for (int j = 0; j < m2_kolumny; j++)
                    {
                        m1_new[i, j] = m2[i, j];
                    }
                }

                result = new double[m2_wiersze, m1_kolumny];
                
                Parallel.For(0, m2_wiersze, (i) =>
                {
                    for (int j = 0; j < m1_kolumny; j++)
                    {
                        for (int k = 0; k < m2_kolumny; k++)
                        {
                            result[i, j] += m1_new[i, k] * m2_new[k, j];
                        }
                    }
                });

                //for (int i = 0; i < m2_wiersze; i++)
                //{
                //    for (int j = 0; j < m1_kolumny; j++)
                //    {
                //        Response.Write(result[i, j] + "/");
                //    }
                //    Response.Write("//");
                //}
            }
            else
            {
                result = new double[m1_wiersze, m2_kolumny];
                
                Parallel.For (0, m1_wiersze, (i) =>
                {
                    for (int j = 0; j < m2_kolumny; j++)
                    {
                        for (int k = 0; k < m1_kolumny; k++)
                        {
                            result[i, j] += m1[i, k] * m2[k, j];
                        }
                    }
                });

                //for (int i = 0; i < m1_wiersze; i++)
                //{
                //    for (int j = 0; j < m2_kolumny; j++)
                //    {
                //        Response.Write(result[i, j] + "/");
                //    }
                //    Response.Write("//");
                //}                
            }

            return result;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            TextBox1.Text = "";
            double[,] macierz1 = WczytajMacierz("UploadedFiles/macierz1.txt");
            double[,] macierz2 = WczytajMacierz("UploadedFiles/macierz2.txt");

            Stopwatch s = new Stopwatch();
            s.Start();

            double[,] result;
            result = MnozenieMacierzy(macierz1,macierz2);

            s.Stop();

            long milisekundy = s.ElapsedMilliseconds;
            long cykle = s.ElapsedTicks;

            WypiszMacierz(result, milisekundy, cykle);
        }



        protected void Button3_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            TextBox1.Text = "";
            string fileExt1 = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            if (fileExt1 == ".txt")
            {
                string serverPath1 = Server.MapPath("UploadedFiles/macierz1.txt");
                FileUpload1.PostedFile.SaveAs(serverPath1);                
            }
            else
            {
                Label1.Text = "Proszę wybrać plik *.txt";
            }

            string fileExt2 = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName);
            if (fileExt2 == ".txt")
            {
                string serverPath2 = Server.MapPath("UploadedFiles/macierz2.txt");
                FileUpload2.PostedFile.SaveAs(serverPath2);
            }
            else
            {
                Label1.Text = "Proszę wybrać plik *.txt";
            }
            
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            double Sx = Convert.ToDouble(TextBox2.Text);
            double Sy = Convert.ToDouble(TextBox3.Text);
            double Fx = Convert.ToDouble(TextBox5.Text);
            double Fy = Convert.ToDouble(TextBox6.Text);

            Stopwatch s = new Stopwatch();
            s.Start();

            Bitmap bmp = DrawMandel(400, 400, Sx, Sy, Fx, Fy);

            s.Stop();

            string serverPath = Server.MapPath("Images/image.bmp");
            bmp.Save(serverPath, System.Drawing.Imaging.ImageFormat.Bmp);

            long ms = s.ElapsedMilliseconds;
            long c = s.ElapsedTicks;

            Image1.ImageUrl = "~/Images/image.bmp" + "?" + DateTime.Now.Ticks;

            TextBox4.Text = "Operacja trwała:\r\n" + ms + " (milisekundy)\r\n" + c + " (cykle zegara)";
        }
    }
}