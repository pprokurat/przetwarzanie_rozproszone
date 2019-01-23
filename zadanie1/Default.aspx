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
        <asp:TextBox ID="TextBox1" runat="server" Height="122px" ReadOnly="True" TextMode="MultiLine" Width="444px"></asp:TextBox>
    </div>

    </asp:Content>
