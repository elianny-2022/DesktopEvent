Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class Login

    Private Sub Minimizar_Click(sender As Object, e As RoutedEventArgs) Handles btnMinimize.Click

    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As RoutedEventArgs)
        Dim correo As String = txtUser.Text
        Dim contraseña As String = txtPassword.Password

        ' Llamar a la función de inicio de sesión y verificar el resultado
        Dim isLoggedIn As Boolean = Login(correo, contraseña)

        If isLoggedIn Then
            ' Abrir la nueva ventana después de iniciar sesión exitosamente
            Dim nuevaVentana As New Eventos()
            nuevaVentana.Show()
            Close() ' Cerrar la ventana actual si es necesario
        Else
            MessageBox.Show("Inicio de sesión fallido. Verifica tus credenciales.")
        End If

    End Sub

    Public Function Login(correo As String, contraseña As String) As Boolean
        Using client As New HttpClient()
            ' Configurar la URL base de tu API
            client.BaseAddress = New Uri("http://localhost:8080/")

            ' Crear el objeto de solicitud de inicio de sesión
            Dim loginData As New Dictionary(Of String, String)()
            loginData.Add("correo", correo)
            loginData.Add("contraseña", contraseña)
            Dim content = New StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json")

            ' Realizar la solicitud POST al endpoint de inicio de sesión
            Dim response = client.PostAsync("/usuario/save", content).Result

            ' Verificar si la solicitud fue exitosa y el código de respuesta es 200 (OK)
            If response.IsSuccessStatusCode Then
                ' Aquí puedes realizar las acciones necesarias después de iniciar sesión exitosamente
                Return True
            Else
                ' Aquí puedes manejar el caso en el que el inicio de sesión falló
                Return False
            End If
        End Using
    End Function

End Class
