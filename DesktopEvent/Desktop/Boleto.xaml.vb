Imports Newtonsoft.Json
Imports System.Net.Http
Imports System.Text

Public Class Boleto
    Public Property Evento As Evento

    ' Método para guardar los boletos
    Private Async Sub GuardarDetalles_Click(sender As Object, e As RoutedEventArgs)
        ' Obtener el nombre del evento desde la ventana principal

        If Evento Is Nothing Then
            MessageBox.Show("No se ha asignado ningún evento.")
            Return
        End If

        Dim eventoId As Long = Evento.eventoId
        Dim nombreEvento As String = txtNombreEventoTB.Text
        Dim cantidadBoletos As Double
        Dim precio As Double
        ' Crear una instancia de Boletos con los datos ingresados en la interfaz
        Dim boletos As New Boletos()
        boletos.nombreEvento = txtNombreEventoTB.Text
        boletos.precio = If(Double.TryParse(txtPrecioTB.Text, precio), precio, 0.0)
        boletos.cantidadBoletos = If(Double.TryParse(txtCantidadBoletosTB.Text, cantidadBoletos), cantidadBoletos, 0.0)
        boletos.asiento = CType(cmbTipoAsientoTB.SelectedItem, ComboBoxItem).Content.ToString()


        Dim boleto As New Boletos()
        boletos.eventoId = eventoId

        ' Serializar el objeto boletos a formato JSON
        Dim jsonBoletos As String = JsonConvert.SerializeObject(boletos)

        ' Crear el contenido de la solicitud HTTP
        Dim content As New StringContent(jsonBoletos, Encoding.UTF8, "application/json")

        Try
            ' Enviar la solicitud POST para guardar los boletos
            Using httpClient As New HttpClient()
                Dim response As HttpResponseMessage = Await httpClient.PostAsync($"http://localhost:8080/boletos/boleto/save", content)

                ' Verificar si la solicitud fue exitosa
                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Boletos guardados exitosamente")
                    Close()
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

    Public Property eventoId As Long
    Public Property nombreEvento As String
    Public Property cantidadBoletos As Double
    Public Property precio As Double
    Public Property asiento As String
End Class