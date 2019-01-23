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

namespace zadanie1
{
    public partial class _Default : Page
    {
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
    }
}