<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="AlumnosModificar.aspx.vb" Inherits="Alumnos_AlumnosModificar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">


    <asp:Label ID="lblMatricula" runat="server" Text="Matricula a modificar:"></asp:Label>
    <asp:TextBox ID="txtMatricula" runat="server"></asp:TextBox>



    <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>


    <asp:Label ID="lblPaterno" runat="server" Text="Paterno"></asp:Label>
    <asp:TextBox ID="txtPaterno" runat="server"></asp:TextBox>


    <asp:Label ID="lblMaterno" runat="server" Text="Materno"></asp:Label>
    <asp:TextBox ID="txtMaterno" runat="server"></asp:TextBox>


   <asp:Button ID="btnModificar" runat="server" Text="Modificar" />


    
</asp:Content>

