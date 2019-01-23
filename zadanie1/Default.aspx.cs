using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

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

            for (int i = 0; i < wiersze; i++)
            {
                for (int j = 0; j < kolumny; j++)
                {
                    Response.Write(macierz[i, j] + "/");
                }
                Response.Write("//");
            }

            return macierz;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {            
            double[,] macierz1 = WczytajMacierz("UploadedFiles/macierz1.txt");
            double[,] macierz2 = WczytajMacierz("UploadedFiles/macierz2.txt");

        }



        protected void Button3_Click(object sender, EventArgs e)
        {            
            string fileExt1 = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            if (fileExt1 == ".txt")
            {
                string serverPath1 = Server.MapPath("UploadedFiles/macierz1.txt");
                FileUpload1.PostedFile.SaveAs(serverPath1);                
            }
            else
            {
                Response.Write("Proszę wybrać plik *.txt");
            }

            string fileExt2 = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName);
            if (fileExt2 == ".txt")
            {
                string serverPath2 = Server.MapPath("UploadedFiles/macierz2.txt");
                FileUpload2.PostedFile.SaveAs(serverPath2);
            }
            else
            {
                Response.Write("Proszę wybrać plik *.txt");
            }
            
        }
    }
}