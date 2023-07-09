Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Class Eventos
    Private txtNombreEvento As Object
    Private txtTipoEvento As Object
    Private dpFecha As Object
    Private txtUbicacion As Object
    Private txtDescripcion As Object

    Private Async Sub GuardarEvento_Click(sender As Object, e As RoutedEventArgs)
        Dim evento As New Evento()

        evento.nombreEvento = txtNombreEventoTB.Text
        evento.tipoEvento = txtTipoEventoTB.Text
        evento.fecha = dpFechaTB.SelectedDate
        evento.ubicacion = txtUbicacionTB.Text
        evento.descripcion = txtDescripcionTB.Text

        Using client As New HttpClient()
            ' Configurar la URL base de tu API
            client.BaseAddress = New Uri("http://localhost:8080/")

            ' Serializar el objeto de evento a JSON
            Dim jsonEvento As String = JsonConvert.SerializeObject(evento)
            Dim content = New StringContent(jsonEvento, Encoding.UTF8, "application/json")

            ' Realizar la solicitud POST al endpoint de la API para guardar el evento
            Dim response = Await client.PostAsync("/eventos/evento/save", content)

            If response.IsSuccessStatusCode Then
                MessageBox.Show("El evento ha sido guardado correctamente.")
                Dim boletosWindow As New Boleto()
                boletosWindow.txtNombreEventoTB.Text = txtNombreEventoTB.Text
                boletosWindow.Evento = evento
                boletosWindow.Show()
                Close()
            Else
                MessageBox.Show("No se pudo guardar el evento. Inténtalo nuevamente.")
            End If
        End Using
    End Sub


    Private Sub LimpiarCampos()
        txtNombreEvento.Text = ""
        txtTipoEvento.Text = ""
        dpFecha.SelectedDate = Date.Now
        txtUbicacion.Text = ""
        txtDescripcion.Text = ""
    End Sub
End Class


Public Class Evento
    Public Property eventoId As Long
    Public Property nombreEvento As String
    Public Property tipoEvento As String
    Public Property fecha As Date?
    Public Property ubicacion As String
    Public Property descripcion As String
End Class

