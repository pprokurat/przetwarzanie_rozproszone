﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace zadanie2
{
    /// <summary>
    /// Opis podsumowujący dla zadanie2
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Aby zezwalać na wywoływanie tej usługi sieci Web ze skryptu za pomocą kodu ASP.NET AJAX, usuń znaczniki komentarza z następującego wiersza. 
    // [System.Web.Script.Services.ScriptService]
    public class zadanie2 : System.Web.Services.WebService
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

        [WebMethod]
        public string WypiszMacierz(string id1, string id2)
        {
            double[,] m1 = WczytajMacierz("UploadedFiles/" + id1 + ".txt");
            double[,] m2 = WczytajMacierz("UploadedFiles/" + id2 + ".txt");

            double[,] m = MnozenieMacierzy(m1, m2);

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

                if (i < wiersze - 1)
                    macierz += "\r\n";
            }

            macierz += "]";

            //macierz += "\r\n\r\nOperacja trwała:\r\n" + ms + " (milisekundy)\r\n" + c + " (cykle zegara)";

            return macierz;
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
                //Label1.Text = "Nieodpowiednie wymiary macierzy! Ilość kolumn pierwszej macierzy musi odpowiadać ilości wierszy drugiej macierzy.";
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

                Parallel.For(0, m1_wiersze, (i) =>
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
    }
}