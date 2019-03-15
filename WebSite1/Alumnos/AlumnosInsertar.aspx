<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="AlumnosInsertar.aspx.vb" Inherits="Alumnos_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
   <h1>Formulario</h1>
    <div class="container">
        <div class="from-group col-md-2">
            <div class="from-group">
    <asp:Label ID="lblMatricula" runat="server" Text="Matricula:"></asp:Label>
    <asp:TextBox ID="txtMatricula" placeholder="Ingresa matricula.." runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidatorMatricula" ControlToValidate="txtMatricula" runat="server" ErrorMessage="Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="from-group">
    <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
    <asp:TextBox ID="txtNombre" placeholder="Aqui pon tu nombre..." runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNombre" ControlToValidate="txtNombre" runat="server" ErrorMessage="Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="from-group">
    <asp:Label ID="lblPaterno" runat="server" Text="Paterno"></asp:Label>
    <asp:TextBox ID="txtPaterno" placeholder="Escribe tu apelido paterno.." runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPaterno" ControlToValidate="txtPaterno" runat="server" ErrorMessage="Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="from-group">
    <asp:Label ID="lblMaterno" runat="server" Text="Materno"></asp:Label>
    <asp:TextBox ID="txtMaterno" placeholder="Escribe tu apelido materno.." runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorMaterno" ControlToValidate="txtMaterno" runat="server" ErrorMessage="Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>

             <div class="from-group">
               <asp:Label ID="LblTelefono" runat="server" Text="Telefono"></asp:Label>
                <asp:TextBox ID="txtTelefono" placeholder="Escribe tu telefono.." runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTelefono" ControlToValidate="txtTelefono" runat="server" ErrorMessage="Campo requerido" ForeColor="Red"></asp:RequiredFieldValidator>

                </div>

         <div class="from-group">
             <asp:Label ID="Label1" runat="server" Text="Sexo"></asp:Label>

             <asp:DropDownList  CssClass="btn btn-info dropdown-toggle" ID="ddSexo" runat="server">
                 <asp:ListItem>Sexo</asp:ListItem>
                 <asp:ListItem>Masculino</asp:ListItem>
                 <asp:ListItem>Femenino</asp:ListItem>
             </asp:DropDownList>
         </div>
           

    <div class="from-group">
        <asp:Label ID="Label2" runat="server" Text="Estado:"></asp:Label>
    <asp:DropDownList CssClass="btn btn-info dropdown-toggle" ID="ddEstado" runat="server" DataSourceID="SqlDataSourceEstados" DataTextField="estado" DataValueField="clave_estado" AutoPostBack="True"></asp:DropDownList>
    <asp:SqlDataSource runat="server" ID="SqlDataSourceEstados" ConnectionString='<%$ ConnectionStrings:UPPConnectionString %>' SelectCommand="SELECT clave_estado, estado FROM Estado ORDER BY estado"></asp:SqlDataSource>
        
    </div>
    <div class="from-group">
            <asp:DropDownList CssClass="btn btn-info dropdown-toggle" ID="ddMunicipio" runat="server" Visible="false" DataSourceID="SqlDataSorceMunicipio" DataTextField="municipio" DataValueField="clave_municipio" AutoPostBack="True"></asp:DropDownList>

                <asp:SqlDataSource runat="server" ID="SqlDataSorceMunicipio" ConnectionString='<%$ ConnectionStrings:UPPConnectionString %>' SelectCommand="SELECT clave_municipio, municipio FROM Municipios WHERE (clave_estado = @clave_estado) ORDER BY municipio">
       <%-- <SelectParameters>
            <asp:Parameter Name="clave_estado"></asp:Parameter>
        </SelectParameters>--%>
    </asp:SqlDataSource> 
            </div>
    <div class="from-group">
                

    <asp:DropDownList CssClass="btn btn-info dropdown-toggle" ID="ddLocalidad" runat="server" Visible="false" DataSourceID="SqlDataSourceLocalidades" DataTextField="localidad" DataValueField="clave_localidad"></asp:DropDownList>

        <asp:SqlDataSource runat="server" ID="SqlDataSourceLocalidades" ConnectionString='<%$ ConnectionStrings:UPPConnectionString %>' SelectCommand="SELECT clave_localidad, localidad, latitud, longitud FROM Localidades WHERE (clave_entidad = @clave_entidad) AND (clave_municipio = @clave_municipio) ORDER BY localidad">

            <%-- <SelectParameters>
            <asp:Parameter Name="clave_entidad"></asp:Parameter>
            <asp:Parameter Name="clave_municipio"></asp:Parameter>
        </SelectParameters>--%>
            <%--<SelectParameters>
                <asp:Parameter Name="clave_entidad"></asp:Parameter>
                <asp:Parameter Name="clave_municipio"></asp:Parameter>
            </SelectParameters>--%>
        </asp:SqlDataSource>
            </div><br/>
            <br /></div>
</div>
    <div>
       
        <asp:Label ID="lbllatitud" runat="server" Text=lat></asp:Label>

        <asp:Label ID="lbllongitud" runat="server" Text=lon></asp:Label><br />

        <%--<input type="hidden" id="lonID" value="" />
        <input type="hidden" id="latID" value="" />--%>

      <a href="javascript:get_loc();">Mostrar localización actual</a>

    </div>
    <div class="container">

      <map:GoogleMap ID="GoogleMap1" runat="server" ApiKey="AIzaSyALHHBkPilu3dbCdrRYHekRZVacdzHDhdo" MapType="Hybrid" Zoom="4" Latitude="19.906909" Longitude="-99.301216" CssClass="map" DefaultAddress="Sofia" Height="300" Width="50%"></map:GoogleMap>

    </div>
    <asp:Button CssClass="btn btn-success" ID="btnInsertar" runat="server" Text="Insertar" />


    <asp:Button CssClass="btn btn-danger" ID="btnEliminar" runat="server" Text="Eliminar" />
    <asp:Button CssClass="btn btn-warning" ID="btnActualizar" runat="server" Text="Actualizar" />

    <div class="jumbotron"> 
    <div class="row">
        <div class="col-md-12">
            <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                <asp:GridView ID="gvAlumnos" runat="server" CssClass="table table-striped table-dark" AutoGenerateColumns="False" DataKeyNames="matricula" DataSourceID="SqlDataSourceAlumnos" CellPadding="4"  GridLines="None">
<%--            <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>--%>
            <Columns>
                <asp:CommandField ControlStyle-CssClass="btn btn-primary" ShowSelectButton="True"></asp:CommandField>
                <asp:BoundField DataField="matricula" HeaderText="matricula" ReadOnly="True" SortExpression="matricula"></asp:BoundField>
                <asp:BoundField DataField="nombre" HeaderText="nombre" SortExpression="nombre"></asp:BoundField>

                <asp:BoundField DataField="paterno" HeaderText="paterno" SortExpression="paterno"></asp:BoundField>
                <asp:BoundField DataField="materno" HeaderText="materno" SortExpression="materno"></asp:BoundField>
                <asp:BoundField DataField="estado" HeaderText="estado" SortExpression="estado"></asp:BoundField>
                <asp:BoundField DataField="municipio" HeaderText="municipio" SortExpression="municipio"></asp:BoundField>
                <asp:BoundField DataField="localidad" HeaderText="localidad" SortExpression="localidad"></asp:BoundField>
                <asp:BoundField DataField="telefono" HeaderText="telefono" SortExpression="telefono"></asp:BoundField>
                <asp:BoundField DataField="sexo" HeaderText="sexo" SortExpression="sexo"></asp:BoundField>
            </Columns>
        </asp:GridView>
            </asp:PlaceHolder> 
        </div>
    </div>
    </div> 

    <asp:SqlDataSource runat="server" ID="SqlDataSourceAlumnos" ConnectionString='<%$ ConnectionStrings:UPPConnectionString %>' SelectCommand="SELECT Alumnos.matricula, Alumnos.nombre, Alumnos.paterno, Alumnos.materno, Estado.estado, Municipios.municipio, Localidades.localidad, Alumnos.telefono, Alumnos.sexo FROM Municipios INNER JOIN Estado ON Municipios.clave_estado = Estado.clave_estado INNER JOIN Localidades ON Municipios.clave_municipio = Localidades.clave_municipio AND Estado.clave_estado = Localidades.clave_entidad INNER JOIN Alumnos ON Estado.clave_estado = Alumnos.clave_entidad AND Municipios.clave_municipio = Alumnos.clave_municipio AND Localidades.clave_localidad = Alumnos.clave_localidad"></asp:SqlDataSource>
</asp:Content>


