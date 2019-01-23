<%@ Page Title="zadanie 1" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="zadanie1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>zadanie 1a</h2>
        <p>Wybierz plik *.txt z pierwszą macierzą</p>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        </br>
        <p>Wybierz plik *.txt z drugą macierzą</p>
        <asp:FileUpload ID="FileUpload2" runat="server" /></br>
        <asp:Button ID="Button3" runat="server" Text="Wyślij" OnClick="Button3_Click" />
        </br></br>
        <asp:Button ID="Button1" runat="server" Text="Przemnóż" OnClick="Button1_Click" />
        </br></br>
        <asp:Label ID="Label1" runat="server"></asp:Label>
        </br>
        <asp:TextBox ID="TextBox1" runat="server" Height="122px" ReadOnly="True" TextMode="MultiLine" Width="433px"></asp:TextBox>
        <h2>zadanie 1b</h2>
        <p>Generowanie fraktalu Mandelbrota</p>
        <asp:TextBox ID="TextBox2" runat="server" Width="67px">-2,1</asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="Sx"></asp:Label></br>
        <asp:TextBox ID="TextBox3" runat="server" Width="67px">-1,3</asp:TextBox>
        <asp:Label ID="Label3" runat="server" Text="Sy"></asp:Label></br>
        <asp:TextBox ID="TextBox5" runat="server" Width="67px">1</asp:TextBox>
        <asp:Label ID="Label4" runat="server" Text="Fx"></asp:Label></br>
        <asp:TextBox ID="TextBox6" runat="server" Width="67px">1,3</asp:TextBox>
        <asp:Label ID="Label5" runat="server" Text="Fy"></asp:Label>
        </br></br>
        <asp:Button ID="Button4" runat="server" Text="Generuj" OnClick="Button4_Click" />
        </br></br>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/image.bmp" Width="436px" />        
        </br></br>
        <asp:TextBox ID="TextBox4" runat="server" Height="89px" ReadOnly="True" TextMode="MultiLine" Width="428px"></asp:TextBox>
        
    </div>

    </asp:Content>
