using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;

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
            string wynik = client.WypiszMacierz(TextBox1.Text,TextBox2.Text);

            TextBox3.Text = wynik;            
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
    }
}