Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
Imports GoogleMaps
Imports GoogleMaps.Overlays
Imports System.Drawing

Partial Class Alumnos_Default
    Inherits System.Web.UI.Page
    Private Sub btnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        'Try
        '    'Dim sql As String
        '    Dim mycmd As New SqlCommand("SPInsertar")
        '    Dim reader As SqlDataReader
        '    Dim conexion As New SqlConnection(get_connetionString())
        '    conexion.Open()
        '    'sql = "INSERT INTO [dbo].[Alumnos]([matricula],[nombre],[paterno],[materno],[cve_estado],[cve_municipio],[cve_localidad]) VALUES (@matricula,@nombre,@paterno,@materno,@cve_estado,@cve_municipio,@cve_localidad)"
        '    'sql = "INSERT INTO [dbo].[Alumnos]([matricula],[nombre],[paterno],[materno],[clave_entidad],[clave_municipio],[clave_localidad]) VALUES (@matricula,@nombre,@paterno,@materno,@clave_entidad,@clave_municipio,@clave_localidad)"
        '    With mycmd
        '        '.CommandText = sql
        '        .CommandType = CommandType.StoredProcedure
        '        .Connection = conexion
        '        .Parameters.AddWithValue("@matricula", Crypto.Encrypt(txtMatricula.Text))
        '        .Parameters.AddWithValue("@nombre", Crypto.Encrypt(txtNombre.Text))
        '        .Parameters.AddWithValue("@paterno", Crypto.Encrypt(txtPaterno.Text))
        '        .Parameters.AddWithValue("@materno", Crypto.Encrypt(txtMaterno.Text))
        '        .Parameters.AddWithValue("@clave_entidad", ddEstado.SelectedValue.ToString)
        '        .Parameters.AddWithValue("@clave_municipio", ddMunicipio.SelectedValue.ToString)
        '        .Parameters.AddWithValue("@clave_localidad", ddLocalidad.SelectedValue.ToString)
        '        .Parameters.AddWithValue("@telefono", Crypto.Encrypt(txtTelefono.Text))
        '        .Parameters.AddWithValue("@sexo", Crypto.Encrypt(ddSexo.SelectedValue.ToString))

        '    End With
        '    reader = mycmd.ExecuteReader

        '    conexion.Close()
        '    txtMatricula.Text = ""
        '    txtNombre.Text = ""
        '    txtPaterno.Text = ""
        '    txtMaterno.Text = ""
        '    SqlDataSourceAlumnos.DataBind()
        '    gvAlumnos.DataBind()

        'Catch ex As Exception
        '    Response.Write(ex.ToString)
        'End Try

        'WEBSERVICe
        'Dim WSCrud As New ServiceReferenceCrudOlaf.WSCrudSoapClient
        Dim WSCrud As New ServiceReferenceExamen.WSCrudSoapClient
        Dim Resultado As String = WSCrud.Insertar(Crypto.Encrypt(txtMatricula.Text), Crypto.Encrypt(txtNombre.Text), Crypto.Encrypt(txtPaterno.Text), Crypto.Encrypt(txtMaterno.Text), ddEstado.SelectedValue.ToString, ddMunicipio.SelectedValue.ToString, ddLocalidad.SelectedValue.ToString, Crypto.Encrypt(txtTelefono.Text), Crypto.Encrypt(ddSexo.SelectedValue.ToString))
        txtMatricula.Text = ""
        txtNombre.Text = ""
        txtPaterno.Text = ""
        txtMaterno.Text = ""
        txtTelefono.Text = ""
        SqlDataSourceAlumnos.DataBind()
        gvAlumnos.DataBind()
    End Sub

    Public Function ActualizarMapa() As String()
        GoogleMap1.Latitude = Double.Parse(lbllatitud.Text)
        GoogleMap1.Longitude = Double.Parse(lbllongitud.Text)
        GoogleMap1.Zoom = 17
    End Function



    Private Sub ddEstado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddEstado.SelectedIndexChanged
        ddMunicipio.Visible = True
        SqlDataSorceMunicipio.SelectCommand = "SELECT clave_municipio, municipio FROM Municipios WHERE (clave_estado =" + ddEstado.SelectedValue.ToString + " ) ORDER BY municipio"
        SqlDataSorceMunicipio.DataBind()

    End Sub

    Private Sub ddMunicipio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddMunicipio.SelectedIndexChanged
        ddLocalidad.Visible = True
        SqlDataSourceLocalidades.SelectCommand = "SELECT clave_localidad, localidad FROM Localidades WHERE (clave_entidad =" + ddEstado.SelectedValue.ToString + ") AND (clave_municipio =" + ddMunicipio.SelectedValue.ToString + ") ORDER BY localidad"
        SqlDataSourceLocalidades.DataBind()
    End Sub

    Private Sub gvAlumnos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvAlumnos.SelectedIndexChanged

        txtMatricula.Text = gvAlumnos.SelectedRow.Cells(1).Text.ToString
        txtNombre.Text = gvAlumnos.SelectedRow.Cells(2).Text.ToString
        txtPaterno.Text = gvAlumnos.SelectedRow.Cells(3).Text.ToString
        txtMaterno.Text = gvAlumnos.SelectedRow.Cells(4).Text.ToString
        txtTelefono.Text = gvAlumnos.SelectedRow.Cells(8).Text.ToString
        ddSexo.SelectedItem.Text = gvAlumnos.SelectedRow.Cells(9).Text.ToString
        Dim Estado As String = HttpUtility.HtmlDecode(gvAlumnos.SelectedRow.Cells(5).Text.ToString)
        Dim Municipio As String = HttpUtility.HtmlDecode(gvAlumnos.SelectedRow.Cells(6).Text.ToString)
        Dim Localidad As String = HttpUtility.HtmlDecode(gvAlumnos.SelectedRow.Cells(7).Text.ToString)

        ddEstado.SelectedIndex = ddEstado.Items.IndexOf(ddEstado.Items.FindByText(Estado))
        ddEstado_SelectedIndexChanged(sender, e)

        ddMunicipio.DataBind()
        ddMunicipio.SelectedIndex = ddMunicipio.Items.IndexOf(ddMunicipio.Items.FindByText(Municipio))
        ddMunicipio_SelectedIndexChanged(sender, e)

        ddLocalidad.DataBind()
        ddLocalidad.SelectedIndex = ddLocalidad.Items.IndexOf(ddLocalidad.Items.FindByText(Localidad))
        Try
            Dim Latitud As Double
            Dim Longitud As Double
            Dim conexion As New SqlConnection(get_connetionString())
            conexion.Open()
            Dim sql As String = "SELECT latitud as LAT, longitud as LONG FROM Localidades WHERE (clave_entidad = " + ddEstado.SelectedValue.ToString + ") AND (clave_municipio = " + ddMunicipio.SelectedValue.ToString + ") AND (clave_localidad =" + ddLocalidad.SelectedValue.ToString + " )"
            Dim cmd As New SqlCommand(sql, conexion)
            Dim myreader As SqlDataReader = cmd.ExecuteReader()

            While myreader.Read()
                Latitud = myreader("LAT")
                Longitud = myreader("LONG")
            End While
            lbllatitud.Text = Latitud.ToString
            lbllongitud.Text = Longitud.ToString
            GoogleMap1.Longitude = Longitud
            GoogleMap1.Latitude = Latitud
            GoogleMap1.Zoom = 14
            conexion.Close()
        Catch ex As SqlException
        Finally
        End Try
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        'Try
        '    'Dim sql As String
        '    Dim mycmd As New SqlCommand("SPDelete")
        '    Dim reader As SqlDataReader
        '    Dim conexion As New SqlConnection(get_connetionString())
        '    conexion.Open()
        '    'sql = "DELETE FROM [dbo].[Alumnos] WHERE [matricula] = @matricula"
        '    With mycmd
        '        '.CommandText = sql
        '        .CommandType = CommandType.StoredProcedure
        '        .Connection = conexion
        '        .Parameters.AddWithValue("@matricula", txtMatricula.Text)
        '        '.Parameters.AddWithValue("@nombre", txtNombre.Text)'
        '        '.Parameters.AddWithValue("@paterno", txtPaterno.Text)'
        '        '.Parameters.AddWithValue("@materno", txtMaterno.Text)'

        '        '.Parameters.AddWithValue("@cve_estado", DropDownMun.SelectedValue.ToString)
        '        '.Parameters.AddWithValue("@cve_municipio", DropDownMun.SelectedItem.ToString)
        '        '.Parameters.AddWithValue("@cve_localidad", txtVisitas.Text)
        '    End With
        '    reader = mycmd.ExecuteReader
        '    conexion.Close()
        '    SqlDataSourceAlumnos.DataBind()
        '    gvAlumnos.DataBind()

        'Catch ex As Exception
        '    Response.Write(ex.ToString)
        'End Try


        'Web servive
        Dim WSCrud As New ServiceReferenceExamen2.WSCrudSoapClient
        Dim resultado As String = WSCrud.Eliminar(Crypto.Encrypt(txtMatricula.Text))
        txtMatricula.Text = ""
        txtNombre.Text = ""
        txtPaterno.Text = ""
        txtMaterno.Text = ""
        txtTelefono.Text = ""
        SqlDataSourceAlumnos.DataBind()
        gvAlumnos.DataBind()
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        'Try
        '    'Dim sql As String
        '    Dim mycmd As New SqlCommand("SPActualizar")
        '    Dim reader As SqlDataReader
        '    Dim conexion As New SqlConnection(get_connetionString())
        '    conexion.Open()
        '    'sql = "INSERT INTO [dbo].[Alumnos]([matricula],[nombre],[paterno],[materno],[cve_estado],[cve_municipio],[cve_localidad]) VALUES (@matricula,@nombre,@paterno,@materno,@cve_estado,@cve_municipio,@cve_localidad)"
        '    '           sql = "UPDATE [dbo].[Alumnos]
        '    'SET [nombre]= @nombre,
        '    '	[materno]=@materno,
        '    '	[paterno] = @paterno,
        '    '       [clave_entidad] = @clave_entidad,
        '    '       [clave_municipio] = @clave_municipio,
        '    '       [clave_localidad] = @clave_localidad

        '    ' WHERE ([matricula]=@matricula)"
        '    With mycmd
        '        '.CommandText = sql
        '        .CommandType = CommandType.StoredProcedure
        '        .Connection = conexion
        '        .Parameters.AddWithValue("@matricula", Crypto.Encrypt(txtMatricula.Text))
        '        .Parameters.AddWithValue("@nombre", Crypto.Encrypt(txtNombre.Text))
        '        .Parameters.AddWithValue("@paterno", Crypto.Encrypt(txtPaterno.Text))
        '        .Parameters.AddWithValue("@materno", Crypto.Encrypt(txtMaterno.Text))
        '        .Parameters.AddWithValue("@clave_entidad", ddEstado.SelectedValue.ToString)
        '        .Parameters.AddWithValue("@clave_municipio", ddMunicipio.SelectedValue.ToString)
        '        .Parameters.AddWithValue("@clave_localidad", ddLocalidad.SelectedValue.ToString)
        '        .Parameters.AddWithValue("@telefono", Crypto.Encrypt(txtTelefono.Text))
        '        .Parameters.AddWithValue("@sexo", Crypto.Encrypt(ddSexo.SelectedValue.ToString))
        '    End With
        '    reader = mycmd.ExecuteReader

        '    conexion.Close()
        '    SqlDataSourceAlumnos.DataBind()
        '    gvAlumnos.DataBind()

        'Catch ex As Exception
        '    Response.Write(ex.ToString)
        'End Try

        'Web service 
        Dim WSCrud As New ServiceReferenceExamen2.WSCrudSoapClient
        Dim resultado As String = WSCrud.Actualizar(Crypto.Encrypt(txtMatricula.Text), Crypto.Encrypt(txtNombre.Text), Crypto.Encrypt(txtPaterno.Text), Crypto.Encrypt(txtMaterno.Text), ddEstado.SelectedValue.ToString, ddMunicipio.SelectedValue.ToString, ddLocalidad.SelectedValue.ToString, Crypto.Encrypt(txtTelefono.Text), Crypto.Encrypt(ddSexo.SelectedValue.ToString))
        txtMatricula.Text = ""
        txtNombre.Text = ""
        txtPaterno.Text = ""
        txtMaterno.Text = ""
        txtTelefono.Text = ""
        ddMunicipio.Visible = False
        ddLocalidad.Visible = False
        SqlDataSourceAlumnos.DataBind()
        gvAlumnos.DataBind()
    End Sub

    Public Function get_connetionString() As String
        Dim SQLServer_Connexion_String As String
        SQLServer_Connexion_String = "Data Source=localhost;Initial Catalog=UPP;User ID=UPPUser2;Password=aaa"
        Return SQLServer_Connexion_String
    End Function

    Public Shared Function Encriptar(ByVal texto As String) As String
        Try
            Dim key As String = "LlaveProgramacionClienteServidor2"
            Dim keyArray As Byte()
            Dim Arreglo_a_cifrar As Byte() = UTF8Encoding.UTF8.GetBytes(texto)
            Dim HashMD5 As MD5CryptoServiceProvider = New MD5CryptoServiceProvider()
            keyArray = HashMD5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key))
            HashMD5.Clear()
            Dim TDes As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider()
            TDes.Key = keyArray
            TDes.Mode = CipherMode.ECB
            TDes.Padding = PaddingMode.PKCS7
            Dim cTrasform As ICryptoTransform = TDes.CreateEncryptor()
            Dim ArrayResultado As Byte() = cTrasform.TransformFinalBlock(Arreglo_a_cifrar, 0, Arreglo_a_cifrar.Length)
            TDes.Clear()
            texto = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length)

        Catch ex As Exception
            Return "Error en el cifrado"

        End Try
        Return texto
    End Function


    Private Sub gvAlumnos_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAlumnos.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim decryptpassword1 As String = e.Row.Cells(1).Text
            Dim decryptpassword2 As String = e.Row.Cells(2).Text
            Dim decryptpassword3 As String = e.Row.Cells(3).Text
            Dim decryptpassword4 As String = e.Row.Cells(4).Text
            Dim decryptpassword5 As String = e.Row.Cells(8).Text
            Dim decryptpassword6 As String = e.Row.Cells(9).Text


            e.Row.Cells(1).Text = Crypto.Decrypt(decryptpassword1)
            e.Row.Cells(2).Text = Crypto.Decrypt(decryptpassword2)
            e.Row.Cells(3).Text = Crypto.Decrypt(decryptpassword3)
            e.Row.Cells(4).Text = Crypto.Decrypt(decryptpassword4)
            e.Row.Cells(8).Text = Crypto.Decrypt(decryptpassword5)
            e.Row.Cells(9).Text = Crypto.Decrypt(decryptpassword6)


        End If


    End Sub



    Public Sub GoogleMap1_Click(sender As Object, e As MouseEventArgs) Handles GoogleMap1.Click
        Try
            Dim Latitud As Double
            Dim Longitud As Double
            Dim conexion As New SqlConnection(get_connetionString())
            conexion.Open()
            Dim sql As String = "SELECT latitud as LAT, longitud as LONG FROM Localidades WHERE (clave_entidad = " + ddEstado.SelectedValue.ToString + ") AND (clave_municipio = " + ddMunicipio.SelectedValue.ToString + ") AND (clave_localidad =" + ddLocalidad.SelectedValue.ToString + " )"
            Dim cmd As New SqlCommand(sql, conexion)
            Dim myreader As SqlDataReader = cmd.ExecuteReader()

            While myreader.Read()
                Latitud = myreader("LAT")
                Longitud = myreader("LONG")
            End While
            lbllatitud.Text = Latitud.ToString
            lbllongitud.Text = Longitud.ToString
            GoogleMap1.Longitude = Longitud
            GoogleMap1.Latitude = Latitud
            GoogleMap1.Zoom = 14
            conexion.Close()
        Catch ex As SqlException
        Finally
        End Try
       



    End Sub





    Public Class Crypto
        Private Shared DES As New TripleDESCryptoServiceProvider
        Private Shared MD5 As New MD5CryptoServiceProvider

        Private Shared Function MD5Hash(ByVal value As String) As Byte()
            Return MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value))
        End Function

        Public Shared Function Encrypt(ByVal stringToEncrypt As String) As String
            Dim key As String = "LlaveProgramacionClienteServidor2"

            DES.Key = Crypto.MD5Hash(key)
            DES.Mode = CipherMode.ECB
            Dim Buffer As Byte() = ASCIIEncoding.ASCII.GetBytes(stringToEncrypt)
            Return Convert.ToBase64String(DES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length))
        End Function

        Public Shared Function Decrypt(ByVal encryptedString As String) As String
            Try
                Dim key As String = "LlaveProgramacionClienteServidor2"

                DES.Key = Crypto.MD5Hash(key)
                DES.Mode = CipherMode.ECB
                Dim Buffer As Byte() = Convert.FromBase64String(encryptedString)
                Return ASCIIEncoding.ASCII.GetString(DES.CreateDecryptor().TransformFinalBlock(Buffer, 0, Buffer.Length))
            Catch ex As Exception
                Return "Error"
            End Try
        End Function
    End Class


    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    Dim polygon = New GooglePolygon() With {
    '        .ID = "Reloj Monumental de Pachuca",
    '        .Clickable = True,
    '        .TargetControlID = "GoogleMap1",
    '        .FillColor = Color.Red,
    '        .FillOpacity = 0.5F,
    '        .StrokeColor = Color.Black,
    '        .StrokeWeight = 1,
    '        .Paths = New List(Of LatLng)() From {
    '        New LatLng(20.128065, -98.731927),
    '                New LatLng(20.12783, -98.73134),
    '                New LatLng(20.12725, -98.731549),
    '                New LatLng(20.127521, -98.732136)
    '            }
    '         }
    '    GoogleMap1.Overlays.Add(polygon)
    '    GoogleMap1.Zoom = 17
    'End Sub
End Class


