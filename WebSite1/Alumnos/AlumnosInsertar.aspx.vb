Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
Partial Class Alumnos_Default
    Inherits System.Web.UI.Page
    Private Sub btnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        Try
            'Dim sql As String
            Dim mycmd As New SqlCommand("SPInsertar")
            Dim reader As SqlDataReader
            Dim conexion As New SqlConnection(get_connetionString())
            conexion.Open()
            'sql = "INSERT INTO [dbo].[Alumnos]([matricula],[nombre],[paterno],[materno],[cve_estado],[cve_municipio],[cve_localidad]) VALUES (@matricula,@nombre,@paterno,@materno,@cve_estado,@cve_municipio,@cve_localidad)"
            'sql = "INSERT INTO [dbo].[Alumnos]([matricula],[nombre],[paterno],[materno],[clave_entidad],[clave_municipio],[clave_localidad]) VALUES (@matricula,@nombre,@paterno,@materno,@clave_entidad,@clave_municipio,@clave_localidad)"
            With mycmd
                '.CommandText = sql
                .CommandType = CommandType.StoredProcedure
                .Connection = conexion
                .Parameters.AddWithValue("@matricula", Crypto.Encrypt(txtMatricula.Text))
                .Parameters.AddWithValue("@nombre", Crypto.Encrypt(txtNombre.Text))
                .Parameters.AddWithValue("@paterno", Crypto.Encrypt(txtPaterno.Text))
                .Parameters.AddWithValue("@materno", Crypto.Encrypt(txtMaterno.Text))
                .Parameters.AddWithValue("@clave_entidad", ddEstado.SelectedValue.ToString)
                .Parameters.AddWithValue("@clave_municipio", ddMunicipio.SelectedValue.ToString)
                .Parameters.AddWithValue("@clave_localidad", ddLocalidad.SelectedValue.ToString)

            End With
            reader = mycmd.ExecuteReader

            conexion.Close()
            txtMatricula.Text = ""
            txtNombre.Text = ""
            txtPaterno.Text = ""
            txtMaterno.Text = ""
            SqlDataSourceAlumnos.DataBind()
            gvAlumnos.DataBind()

        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

        ''WEBSERVICe
        'Dim WSCrud As New ServiceReferenceCrudOlaf.WSCrudSoapClient
        'Dim Resultado As String = WSCrud.Insertar(txtMatricula.Text, txtNombre.Text, txtPaterno.Text, txtMaterno.Text, ddEstado.SelectedValue.ToString, ddMunicipio.SelectedValue.ToString, ddLocalidad.SelectedValue.ToString)
        'txtMatricula.Text = ""
        'txtNombre.Text = ""
        'txtPaterno.Text = ""
        'txtMaterno.Text = ""
        'SqlDataSourceAlumnos.DataBind()
        'gvAlumnos.DataBind()
    End Sub

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
        'ddEstado.SelectedItem = gvAlumnos.SelectedRow.Cells(5).Text.ToString
        'ddEstado.SelectedItem.Enabled = SqlDataSourceEstados.SelectCommand = "
        'SELECT  estado
        'FROM     dbo.Estado
        'WHERE (estado = '" + gvAlumnos.SelectedRow.Cells(5).Text.ToString + "')"


        For Each item As ListItem In ddEstado.Items
            item.Selected = False
            If (item.Text = gvAlumnos.SelectedRow.Cells(5).Text.ToString) Then
                item.Selected = True
                ddMunicipio.Visible = True
                SqlDataSorceMunicipio.SelectCommand = "SELECT clave_municipio, municipio FROM Municipios WHERE (clave_estado =" + ddEstado.SelectedValue.ToString + " ) ORDER BY municipio"
                SqlDataSorceMunicipio.DataBind()

            End If

        Next

        'For Each item2 As ListItem In ddMunicipio.Items
        '    item2.Selected = False
        '    If (item2.Text = gvAlumnos.SelectedRow.Cells(6).Text.ToString) Then
        '        item2.Selected = True
        '        ddLocalidad.Visible = True
        '        SqlDataSourceLocalidades.SelectCommand = "SELECT clave_localidad, localidad FROM Localidades WHERE (clave_entidad =" + ddEstado.SelectedValue.ToString + ") AND (clave_municipio =" + ddMunicipio.SelectedValue.ToString + ") ORDER BY localidad"
        '        SqlDataSourceLocalidades.DataBind()
        '        MsgBox("Selecionado:" + item2.Text)
        '    End If
        'Next


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
        Dim WSCrud As New ServiceReferenceCrudOlaf1.WSCrudSoapClient
        Dim resultado As String = WSCrud.Eliminar(txtMatricula.Text)
        txtMatricula.Text = ""
        txtNombre.Text = ""
        txtPaterno.Text = ""
        txtMaterno.Text = ""
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
        '        .Parameters.AddWithValue("@matricula", txtMatricula.Text)
        '        .Parameters.AddWithValue("@nombre", txtNombre.Text)
        '        .Parameters.AddWithValue("@paterno", txtPaterno.Text)
        '        .Parameters.AddWithValue("@materno", txtMaterno.Text)
        '        .Parameters.AddWithValue("@clave_entidad", ddEstado.SelectedValue.ToString)
        '        .Parameters.AddWithValue("@clave_municipio", ddMunicipio.SelectedValue.ToString)
        '        .Parameters.AddWithValue("@clave_localidad", ddLocalidad.SelectedValue.ToString)
        '    End With
        '    reader = mycmd.ExecuteReader

        '    conexion.Close()
        '    SqlDataSourceAlumnos.DataBind()
        '    gvAlumnos.DataBind()

        'Catch ex As Exception
        '    Response.Write(ex.ToString)
        'End Try
        Dim WSCrud As New ServiceReferenceCrudOlaf1.WSCrudSoapClient
        Dim resultado As String = WSCrud.Actualizar(txtMatricula.Text, txtNombre.Text, txtPaterno.Text, txtMaterno.Text, ddEstado.SelectedValue.ToString, ddMunicipio.SelectedValue.ToString, ddLocalidad.SelectedValue.ToString)
        txtMatricula.Text = ""
        txtNombre.Text = ""
        txtPaterno.Text = ""
        txtMaterno.Text = ""
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

    Public Shared Function Desencriptar(ByVal cifrado As String) As String

    End Function

    Private Sub gvAlumnos_Load(sender As Object, e As EventArgs) Handles gvAlumnos.Load

    End Sub



    Private Sub gvAlumnos_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAlumnos.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim decryptpassword1 As String = e.Row.Cells(1).Text
            Dim decryptpassword2 As String = e.Row.Cells(2).Text
            Dim decryptpassword3 As String = e.Row.Cells(3).Text
            Dim decryptpassword4 As String = e.Row.Cells(4).Text

            e.Row.Cells(1).Text = Crypto.Decrypt(decryptpassword1)
            e.Row.Cells(2).Text = Crypto.Decrypt(decryptpassword2)
            e.Row.Cells(3).Text = Crypto.Decrypt(decryptpassword3)
            e.Row.Cells(4).Text = Crypto.Decrypt(decryptpassword4)

        End If


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

End Class
