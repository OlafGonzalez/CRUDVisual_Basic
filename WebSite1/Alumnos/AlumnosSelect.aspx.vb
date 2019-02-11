
Imports System.Data.SqlClient

Partial Class Alumnos_AlumnosSelect
    Inherits System.Web.UI.Page

    Private Sub btnSelecionar_Click(sender As Object, e As EventArgs) Handles btnSelecionar.Click

        Try
            Dim sql As String
            Dim mycmd As New SqlCommand
            Dim reader As SqlDataReader
            Dim conexion As New SqlConnection(get_connetionString())
            conexion.Open()
            'sql = "INSERT INTO [dbo].[Alumnos]([matricula],[nombre],[paterno],[materno],[cve_estado],[cve_municipio],[cve_localidad]) VALUES (@matricula,@nombre,@paterno,@materno,@cve_estado,@cve_municipio,@cve_localidad)"
            sql = "SELECT [matricula]
      ,[nombre]
      ,[paterno]
      ,[materno]
  FROM [UPP].[dbo].[Alumnos]"
      

            With mycmd
                .CommandText = sql
                .Connection = conexion
                ' .Parameters.AddWithValue("@matricula", txtMatricula.Text)
                '.Parameters.AddWithValue("@nombre", txtNombre.Text)
                '.Parameters.AddWithValue("@paterno", txtPaterno.Text)
                '.Parameters.AddWithValue("@materno", txtMaterno.Text)
                '.Parameters.AddWithValue("@cve_estado", DropDownMun.SelectedValue.ToString)
                '.Parameters.AddWithValue("@cve_municipio", DropDownMun.SelectedItem.ToString)
                '.Parameters.AddWithValue("@cve_localidad", txtVisitas.Text)
            End With
            reader = mycmd.ExecuteReader



            conexion.Close()

        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub


    Public Function get_connetionString() As String
        Dim SQLServer_Connexion_String As String
        SQLServer_Connexion_String = "Data Source=localhost;Initial Catalog=UPP;User ID=UPPUser2;Password=aaa"
        Return SQLServer_Connexion_String
    End Function
End Class
