<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default_2.aspx.cs" Inherits="zadanie2_klient._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>zadanie 2a</h2>
        <p>Wybierz plik *.txt z macierzą        
        </p>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        </br>
        <asp:Button ID="Button3" runat="server" Text="Prześlij" OnClick="Button3_Click" />
        </br></br>
        <p>Wykonaj mnożenie macierzy</p>
          <asp:Label ID="Label2" runat="server" Text="Podaj id macierzy do przemnożenia"></asp:Label></br>
         <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Przemnóż" />
        </br></br>
        <asp:TextBox ID="TextBox10" runat="server" Height="86px" ReadOnly="True" TextMode="MultiLine" Width="380px"></asp:TextBox> 
        </br></br>
        <p>Pobierz plik z macierzą wynikową</p>
          <asp:Label ID="Label3" runat="server" Text="Podaj id macierzy do pobrania"></asp:Label></br>
         <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Pobierz" />
        
        <p>
           
            </br></br>
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
            <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
            <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
            <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
        </p>
    </div>

</asp:Content>
