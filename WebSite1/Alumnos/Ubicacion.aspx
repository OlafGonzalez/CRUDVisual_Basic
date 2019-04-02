<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Ubicacion.aspx.vb" Inherits="Alumnos_Ubicacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">


        <asp:Label ID="lbllatitud" runat="server" Text=lat></asp:Label>
        <asp:Label ID="lbllongitud" runat="server" Text=lon></asp:Label><br />

      <a href="javascript:get_loc();">Mostrar</a>

          <map:GoogleMap ID="GoogleMap2" runat="server" ApiKey="AIzaSyALHHBkPilu3dbCdrRYHekRZVacdzHDhdo" MapType="Hybrid" Zoom="4" Latitude="19.906909" Longitude="-99.301216" CssClass="map" DefaultAddress="Sofia" Height="600" Width="80%"></map:GoogleMap>


</asp:Content>

