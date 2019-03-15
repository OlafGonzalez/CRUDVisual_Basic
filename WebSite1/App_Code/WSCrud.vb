Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class WSCrud
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function HelloWorld() As String
        Return "Hola a todos"
    End Function

    <WebMethod>
    Public Function Actualizar(Matricula As String, Nombre As String, Paterno As String, Materno As String, Clave_entidad As Integer, Clave_municipio As Integer, Clave_localidad As Integer, Telefono As String, Sexo As String) As String
        Try
            'Dim sql As String
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
                .Parameters.AddWithValue("@matricula", Matricula)
                .Parameters.AddWithValue("@nombre", Nombre)
                .Parameters.AddWithValue("@paterno", Paterno)
                .Parameters.AddWithValue("@materno", Materno)
                .Parameters.AddWithValue("@clave_entidad", Clave_entidad)
                .Parameters.AddWithValue("@clave_municipio", Clave_municipio)
                .Parameters.AddWithValue("@clave_localidad", Clave_localidad)
                .Parameters.AddWithValue("@telefono", Telefono)
                .Parameters.AddWithValue("@sexo", Sexo)
            End With
            reader = mycmd.ExecuteReader

            conexion.Close()


        Catch ex As Exception
            ex.ToString()
        End Try
        Return "Actualizado"
    End Function

    <WebMethod()>
    Public Function Eliminar(Matriculas As String) As String
        Try
            'Dim sql As String
            Dim mycmd As New SqlCommand("SPDelete")
            Dim reader As SqlDataReader
            Dim conexion As New SqlConnection(get_connetionString())
            conexion.Open()
            'sql = "DELETE FROM [dbo].[Alumnos] WHERE [matricula] = @matricula"
            With mycmd
                '.CommandText = sql
                .CommandType = CommandType.StoredProcedure
                .Connection = conexion
                .Parameters.AddWithValue("@matricula", Matriculas)
                '.Parameters.AddWithValue("@nombre", txtNombre.Text)'
                '.Parameters.AddWithValue("@paterno", txtPaterno.Text)'
                '.Parameters.AddWithValue("@materno", txtMaterno.Text)'
                '.Parameters.AddWithValue("@cve_estado", DropDownMun.SelectedValue.ToString)
                '.Parameters.AddWithValue("@cve_municipio", DropDownMun.SelectedItem.ToString)
                '.Parameters.AddWithValue("@cve_localidad", txtVisitas.Text)
            End With
            reader = mycmd.ExecuteReader
            conexion.Close()


        Catch ex As Exception
            ex.ToString()
        End Try
        Return "Eliminado"
    End Function


    <WebMethod()>
    Public Function Insertar(Matricula As String, Nombre As String, Paterno As String, Materno As String, Clave_entidad As Integer, Clave_municipio As Integer, Clave_localidad As Integer, Telefono As String, Sexo As String) As String
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
                .Parameters.AddWithValue("@telefono", Telefono)
                .Parameters.AddWithValue("@sexo", Sexo)
            End With
            reader = mycmd.ExecuteReader

            conexion.Close()


        Catch ex As Exception
            Return ex.ToString
        End Try
        Return "Registro insertado correctamente"
    End Function


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


    Public Function get_connetionString() As String
        Dim SQLServer_Connexion_String As String
        SQLServer_Connexion_String = "Data Source=localhost;Initial Catalog=UPP;User ID=UPPUser2;Password=aaa"
        Return SQLServer_Connexion_String
    End Function
End Class

