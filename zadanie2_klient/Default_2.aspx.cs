using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace zadanie2_klient
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            zad2.zadanie2SoapClient client = new zad2.zadanie2SoapClient("zadanie2Soap");
            string id = client.PrzemnozMacierz(TextBox1.Text,TextBox2.Text);

            TextBox10.Text = "Wykonano mnożenie macierzy, id macierzy wynikowej to: " + id + ".\r\n";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            zad2.zadanie2SoapClient client = new zad2.zadanie2SoapClient("zadanie2Soap");

            int width = Convert.ToInt32(TextBox4.Text);
            int height = Convert.ToInt32(TextBox5.Text);
            double Sx = Convert.ToDouble(TextBox6.Text);
            double Sy = Convert.ToDouble(TextBox7.Text);
            double Fx = Convert.ToDouble(TextBox8.Text);
            double Fy = Convert.ToDouble(TextBox9.Text);

            string bmp_b64 = client.DrawMandel(width, height, Sx, Sy, Fx, Fy);

            byte[] bitmapData = Convert.FromBase64String(bmp_b64);
            System.IO.MemoryStream streamBitmap = new System.IO.MemoryStream(bitmapData);
            Bitmap bmp = new Bitmap(Bitmap.FromStream(streamBitmap));

            string serverPath = Server.MapPath("Images/image.bmp");
            bmp.Save(serverPath, System.Drawing.Imaging.ImageFormat.Bmp);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Label1.Text = "";
            TextBox1.Text = "";
            string fileExt = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            if (fileExt == ".txt")
            {
                string id = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName);
                string serverPath = Server.MapPath("UploadedFiles/"+ id + ".txt");
                FileUpload1.PostedFile.SaveAs(serverPath);

                byte[] bytes = File.ReadAllBytes(serverPath);
                string file = Convert.ToBase64String(bytes);

                zad2.zadanie2SoapClient client = new zad2.zadanie2SoapClient("zadanie2Soap");
                string id_returned = client.ZapiszPlik(file,id);
                TextBox10.Text = "Przesłano plik, id macierzy to: " + id + ".\r\n";
            }
            else
            {
                Label1.Text = "Proszę wybrać plik *.txt";
            }
            
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string id = TextBox3.Text;
            zad2.zadanie2SoapClient client = new zad2.zadanie2SoapClient("zadanie2Soap");
            string b64 = client.PobierzMacierz(id);            

            if (b64 != "404")
            {
                byte[] data = Convert.FromBase64String(b64);
                string decodedString = Encoding.UTF8.GetString(data);

                string serverPath = Server.MapPath("UploadedFiles/" + id + ".txt");

                using (StreamWriter sw = new StreamWriter(serverPath))
                {
                    sw.WriteLine(decodedString);
                    sw.Flush();
                    sw.Close();
                }
                TextBox10.Text = "Pobrano plik.\r\n";
            }
            else
            {
                TextBox10.Text = "Error 404. Nie znaleziono pliku.\r\n";
            }
            
        }
    }
}