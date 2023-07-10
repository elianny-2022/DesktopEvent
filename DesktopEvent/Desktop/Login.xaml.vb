Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class Login

    Private Sub btnLogin_Click(sender As Object, e As RoutedEventArgs)
        Dim correo As String = txtUser.Text
        Dim contraseña As String = txtPassword.Password

        ' Llamo la función de inicio de sesión y verifico el resultado
        Dim isLoggedIn As Boolean = Login(correo, contraseña)

        If isLoggedIn Then
            ' Me lleva a la ventana de evento
            Dim nuevaVentana As New Eventos()
            nuevaVentana.Show()
            Close()
        Else
            MessageBox.Show("Error al loguearse. Verifica tus credenciales.")
        End If

    End Sub

    Public Function Login(correo As String, contraseña As String) As Boolean
        Using client As New HttpClient()

            ' Configuro la URL
            client.BaseAddress = New Uri("http://localhost:8080/")

            ' Aqui se crea el objeto de solicitud de inicio de sesión
            Dim loginData As New Dictionary(Of String, String)()
            loginData.Add("correo", correo)
            loginData.Add("contraseña", contraseña)
            Dim content = New StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json")

            ' Aqui se realiza una peticion a mi API
            Dim response = client.PostAsync("/usuario/save", content).Result

            ' Aqui se verifica si fue exitosa la solicitud
            If response.IsSuccessStatusCode Then

                Return True
            Else

                Return False
            End If
        End Using
    End Function


    Private Sub Minimizar_Click(sender As Object, e As RoutedEventArgs) Handles btnMinimize.Click

    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)

    End Sub
End Class

