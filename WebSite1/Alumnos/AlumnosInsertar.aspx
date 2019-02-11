﻿<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="AlumnosInsertar.aspx.vb" Inherits="Alumnos_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
   

    <asp:Label ID="lblMatricula" runat="server" Text="Matricula:"></asp:Label>
    <asp:TextBox ID="txtMatricula" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidatorMatricula" ControlToValidate="txtMatricula" runat="server" ErrorMessage="Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>

    <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNombre" ControlToValidate="txtNombre" runat="server" ErrorMessage="Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>


    <asp:Label ID="lblPaterno" runat="server" Text="Paterno"></asp:Label>
    <asp:TextBox ID="txtPaterno" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPaterno" ControlToValidate="txtPaterno" runat="server" ErrorMessage="Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>


    <asp:Label ID="lblMaterno" runat="server" Text="Materno"></asp:Label>
    <asp:TextBox ID="txtMaterno" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorMaterno" ControlToValidate="txtMaterno" runat="server" ErrorMessage="Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>


    <asp:Button ID="btnInsertar" runat="server" Text="Insertar" />


    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" />
    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" />

    <asp:DropDownList ID="ddEstado" runat="server" DataSourceID="SqlDataSourceEstados" DataTextField="estado" DataValueField="clave_estado" AutoPostBack="True"></asp:DropDownList>
    <asp:SqlDataSource runat="server" ID="SqlDataSourceEstados" ConnectionString='<%$ ConnectionStrings:UPPConnectionString %>' SelectCommand="SELECT clave_estado, estado FROM Estado ORDER BY estado"></asp:SqlDataSource>
    
    <asp:DropDownList ID="ddMunicipio" runat="server" Visible="false" DataSourceID="SqlDataSorceMunicipio" DataTextField="municipio" DataValueField="clave_municipio" AutoPostBack="True"></asp:DropDownList>

    <asp:SqlDataSource runat="server" ID="SqlDataSorceMunicipio" ConnectionString='<%$ ConnectionStrings:UPPConnectionString %>' SelectCommand="SELECT clave_municipio, municipio FROM Municipios WHERE (clave_estado = @clave_estado) ORDER BY municipio">
       <%-- <SelectParameters>
            <asp:Parameter Name="clave_estado"></asp:Parameter>
        </SelectParameters>--%>
    </asp:SqlDataSource>

    <asp:DropDownList ID="ddLocalidad" runat="server" Visible="false" DataSourceID="SqlDataSourceLocalidades" DataTextField="localidad" DataValueField="clave_localidad"></asp:DropDownList>

    <asp:SqlDataSource runat="server" ID="SqlDataSourceLocalidades" ConnectionString='<%$ ConnectionStrings:UPPConnectionString %>' SelectCommand="SELECT clave_localidad, localidad FROM Localidades WHERE (clave_entidad = @clave_entidad) AND (clave_municipio = @clave_municipio) ORDER BY localidad">
       <%-- <SelectParameters>
            <asp:Parameter Name="clave_entidad"></asp:Parameter>
            <asp:Parameter Name="clave_municipio"></asp:Parameter>
        </SelectParameters>--%>
    </asp:SqlDataSource>
    <div class="jumbotron"> 
    <div class="row">
        <div class="col-med-12">
            <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                <asp:GridView ID="gvAlumnos" runat="server" CssClass="footable" AutoGenerateColumns="False" DataKeyNames="matricula" DataSourceID="SqlDataSourceAlumnos" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
            <Columns>
                <asp:CommandField ShowSelectButton="True" ButtonType="Button"></asp:CommandField>
                <asp:BoundField DataField="matricula" HeaderText="Matricula" ReadOnly="True" SortExpression="matricula"></asp:BoundField>
                <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre"></asp:BoundField>
                <asp:BoundField DataField="paterno" HeaderText="Apellido Paterno" SortExpression="paterno"></asp:BoundField>
                <asp:BoundField DataField="materno" HeaderText="Apellido Materno" SortExpression="materno"></asp:BoundField>
                <asp:BoundField DataField="estado" HeaderText="Estado" SortExpression="estado"></asp:BoundField>
                <asp:BoundField DataField="municipio" HeaderText="Municipio" SortExpression="municipio"></asp:BoundField>
                <asp:BoundField DataField="localidad" HeaderText="Localidad" SortExpression="localidad"></asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#2461BF"></EditRowStyle>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle>
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>
            <PagerStyle HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White"></PagerStyle>
            <RowStyle BackColor="#EFF3FB"></RowStyle>
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
            <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>
            <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>
            <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>
            <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
        </asp:GridView>
            </asp:PlaceHolder> 
        </div>
    </div>
    </div> 
    <asp:SqlDataSource runat="server" ID="SqlDataSourceAlumnos" ConnectionString='<%$ ConnectionStrings:UPPConnectionString %>' SelectCommand="SELECT Alumnos.matricula, Alumnos.nombre, Alumnos.paterno, Alumnos.materno, Estado.estado, Municipios.municipio, Localidades.localidad FROM Municipios INNER JOIN Estado ON Municipios.clave_estado = Estado.clave_estado INNER JOIN Localidades ON Municipios.clave_municipio = Localidades.clave_municipio AND Estado.clave_estado = Localidades.clave_entidad INNER JOIN Alumnos ON Estado.clave_estado = Alumnos.clave_entidad AND Municipios.clave_municipio = Alumnos.clave_municipio AND Localidades.clave_localidad = Alumnos.clave_localidad"></asp:SqlDataSource>
</asp:Content>


