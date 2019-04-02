
Imports GoogleMaps

Partial Class Alumnos_Ubicacion
    Inherits System.Web.UI.Page

    Private Sub GoogleMap2_Click(sender As Object, e As MouseEventArgs) Handles GoogleMap2.Click
        Dim LON As Double = Double.Parse(lbllongitud.ToString)
        Dim LAT As Double = lbllatitud.ToString

        GoogleMap2.Latitude = LAT
        GoogleMap2.Longitude = LON
        GoogleMap2.Zoom = 17
    End Sub
End Class
