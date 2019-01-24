using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
    }
}