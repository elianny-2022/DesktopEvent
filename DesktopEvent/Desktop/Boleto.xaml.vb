Imports Newtonsoft.Json
Imports System.Net.Http
Imports System.Runtime.CompilerServices
Imports System.Text

Public Class Boleto
    Public Property Evento As Evento

    Public Sub New(evento As Evento)
        Me.Evento = evento

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    ' este es el metodo para guardar boletos
    Private Async Sub GuardarDetalles_Click(sender As Object, e As RoutedEventArgs)
        'aqui se obtiene el nombre del evento

        If Evento Is Nothing Then
            MessageBox.Show("No se ha asignado ningún evento.")
            Return
        End If

        Dim eventoId As Long = Evento.eventoId
        Dim nombreEvento As String = txtNombreEventoTB.Text
        Dim cantidadBoletos As Double
        Dim precio As Double

        ' aqui se crea una instancia de boletos con los valores ingresados
        Dim boletos As New Boletos()
        boletos.nombreEvento = txtNombreEventoTB.Text
        boletos.evento = Evento
        boletos.precio = If(Double.TryParse(txtPrecioTB.Text, precio), precio, 0.0)
        boletos.cantidadBoletos = If(Double.TryParse(txtCantidadBoletosTB.Text, cantidadBoletos), cantidadBoletos, 0.0)
        boletos.asiento = CType(cmbTipoAsientoTB.SelectedItem, ComboBoxItem).Content.ToString()


        Dim jsonBoletos As String = JsonConvert.SerializeObject(boletos)


        Dim content As New StringContent(jsonBoletos, Encoding.UTF8, "application/json")

        Try

            Using httpClient As New HttpClient()
                Dim response As HttpResponseMessage = Await httpClient.PostAsync($"http://localhost:8080/boletos/boleto/save", content)

                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Boletos guardados exitosamente")
                    Close()
                    Dim nuevaVentana As New Eventos()
                    nuevaVentana.Show()
                Else
                    MessageBox.Show("Error al guardar los boletos")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error en la solicitud HTTP: " & ex.Message)
        End Try
    End Sub
End Class


Public Class Boletos

    Public Property evento As Evento
    Public Property nombreEvento As String
    Public Property cantidadBoletos As Double
    Public Property precio As Double
    Public Property asiento As String
End Class