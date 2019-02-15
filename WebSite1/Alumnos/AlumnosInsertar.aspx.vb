Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography

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
        '        .Parameters.AddWithValue("@matricula", txtMatricula.Text)
        '        .Parameters.AddWithValue("@nombre", Encriptar(txtNombre.Text))
        '        .Parameters.AddWithValue("@paterno", txtPaterno.Text)
        '        .Parameters.AddWithValue("@materno", txtMaterno.Text)
        '        .Parameters.AddWithValue("@clave_entidad", ddEstado.SelectedValue.ToString)
        '        .Parameters.AddWithValue("@clave_municipio", ddMunicipio.SelectedValue.ToString)
        '        .Parameters.AddWithValue("@clave_localidad", ddLocalidad.SelectedValue.ToString)

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
        Dim WSCrud As New ServiceReferenceCrudOlaf.WSCrudSoapClient
        Dim Resultado As String = WSCrud.Insertar(txtMatricula.Text, txtNombre.Text, txtPaterno.Text, txtMaterno.Text, ddEstado.SelectedValue.ToString, ddMunicipio.SelectedValue.ToString, ddLocalidad.SelectedValue.ToString)
        txtMatricula.Text = ""
        txtNombre.Text = ""
        txtPaterno.Text = ""
        txtMaterno.Text = ""
        SqlDataSourceAlumnos.DataBind()
        gvAlumnos.DataBind()
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

        For Each item As ListItem In ddEstado.Items
            item.Selected = False
            If (item.Text = gvAlumnos.SelectedRow.Cells(5).Text.ToString) Then
                item.Selected = True
                ddMunicipio.Visible = True
                SqlDataSorceMunicipio.SelectCommand = "SELECT clave_municipio, municipio FROM Municipios WHERE (clave_estado =" + ddEstado.SelectedValue.ToString + " ) ORDER BY municipio"
                SqlDataSorceMunicipio.DataBind()

            End If

        Next

        For Each item2 As ListItem In ddMunicipio.Items
            item2.Selected = False
            If (item2.Text = gvAlumnos.SelectedRow.Cells(6).Text.ToString) Then
                item2.Selected = True
                ddLocalidad.Visible = True
                SqlDataSourceLocalidades.SelectCommand = "SELECT clave_localidad, localidad FROM Localidades WHERE (clave_entidad =" + ddEstado.SelectedValue.ToString + ") AND (clave_municipio =" + ddMunicipio.SelectedValue.ToString + ") ORDER BY localidad"
                SqlDataSourceLocalidades.DataBind()
                MsgBox("Selecionado:" + item2.Text)
            End If
        Next


    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Try
            Dim sql As String
            Dim mycmd As New SqlCommand("SPDelete")
            Dim reader As SqlDataReader
            Dim conexion As New SqlConnection(get_connetionString())
            conexion.Open()
            'sql = "DELETE FROM [dbo].[Alumnos] WHERE [matricula] = @matricula"
            With mycmd
                '.CommandText = sql
                .CommandType = CommandType.StoredProcedure
                .Connection = conexion
                .Parameters.AddWithValue("@matricula", txtMatricula.Text)
                '.Parameters.AddWithValue("@nombre", txtNombre.Text)'
                '.Parameters.AddWithValue("@paterno", txtPaterno.Text)'
                '.Parameters.AddWithValue("@materno", txtMaterno.Text)'
                '.Parameters.AddWithValue("@cve_estado", DropDownMun.SelectedValue.ToString)
                '.Parameters.AddWithValue("@cve_municipio", DropDownMun.SelectedItem.ToString)
                '.Parameters.AddWithValue("@cve_localidad", txtVisitas.Text)
            End With
            reader = mycmd.ExecuteReader
            conexion.Close()
            SqlDataSourceAlumnos.DataBind()
            gvAlumnos.DataBind()

        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Try
            Dim sql As String
            Dim mycmd As New SqlCommand("SPActualizar")
            Dim reader As SqlDataReader
            Dim conexion As New SqlConnection(get_connetionString())
            conexion.Open()
            'sql = "INSERT INTO [dbo].[Alumnos]([matricula],[nombre],[paterno],[materno],[cve_estado],[cve_municipio],[cve_localidad]) VALUES (@matricula,@nombre,@paterno,@materno,@cve_estado,@cve_municipio,@cve_localidad)"
            '           sql = "UPDATE [dbo].[Alumnos]
            'SET [nombre]= @nombre,
            '	[materno]=@materno,
            '	[paterno] = @paterno,
            '       [clave_entidad] = @clave_entidad,
            '       [clave_municipio] = @clave_municipio,
            '       [clave_localidad] = @clave_localidad

            ' WHERE ([matricula]=@matricula)"
            With mycmd
                '.CommandText = sql
                .CommandType = CommandType.StoredProcedure
                .Connection = conexion
                .Parameters.AddWithValue("@matricula", txtMatricula.Text)
                .Parameters.AddWithValue("@nombre", txtNombre.Text)
                .Parameters.AddWithValue("@paterno", txtPaterno.Text)
                .Parameters.AddWithValue("@materno", txtMaterno.Text)
                .Parameters.AddWithValue("@clave_entidad", ddEstado.SelectedValue.ToString)
                .Parameters.AddWithValue("@clave_municipio", ddMunicipio.SelectedValue.ToString)
                .Parameters.AddWithValue("@clave_localidad", ddLocalidad.SelectedValue.ToString)
            End With
            reader = mycmd.ExecuteReader

            conexion.Close()
            SqlDataSourceAlumnos.DataBind()
            gvAlumnos.DataBind()

        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
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




    Public Function DecryptData(ByVal encryptedtext As String) As String

        ' Convert the encrypted text string to a byte array.
        Dim encryptedBytes() As Byte = Convert.FromBase64String(encryptedtext)

        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the decoder to write to the stream.
        Dim decStream As New CryptoStream(ms, TripleDES.Create, System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
        decStream.FlushFinalBlock()

        ' Convert the plaintext stream to a string.
        Return System.Text.Encoding.Unicode.GetString(ms.ToArray)
    End Function

End Class
