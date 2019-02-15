Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WSCrud
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hola a todos"
    End Function

    <WebMethod()>
    Public Function Insertar(Matricula As String, Nombre As String, Paterno As String, Materno As String, Clave_entidad As Integer, Clave_municipio As Integer, Clave_localidad As Integer) As String
        Try
            Dim mycmd As New SqlCommand("SPInsertar")
            Dim reader As SqlDataReader
            Dim conexion As New SqlConnection(get_connetionString())
            conexion.Open()
            With mycmd
                .CommandType = CommandType.StoredProcedure
                .Connection = conexion
                .Parameters.AddWithValue("@matricula", Matricula)
                .Parameters.AddWithValue("@nombre", Nombre)
                .Parameters.AddWithValue("@paterno", Paterno)
                .Parameters.AddWithValue("@materno", Materno)
                .Parameters.AddWithValue("@clave_entidad", Clave_entidad)
                .Parameters.AddWithValue("@clave_municipio", Clave_municipio)
                .Parameters.AddWithValue("@clave_localidad", Clave_localidad)
            End With
            reader = mycmd.ExecuteReader

            conexion.Close()


        Catch ex As Exception
            Return ex.ToString
        End Try
        Return "Registro insertado correctamente"
    End Function
    Public Function get_connetionString() As String
        Dim SQLServer_Connexion_String As String
        SQLServer_Connexion_String = "Data Source=localhost;Initial Catalog=UPP;User ID=UPPUser2;Password=aaa"
        Return SQLServer_Connexion_String
    End Function
End Class