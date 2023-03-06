Imports FireSharp.Config
Imports FireSharp.Response
Imports FireSharp.Interfaces
Imports FireSharp
Imports System.Windows

Public Class Login
    Private fcon As New FirebaseConfig() With {
                .AuthSecret = "C5uzbAuqUfaFh5JFoGdTps4ka0kk0Hq3T5cUE2cU",
                .BasePath = "https://lofo-7c69f-default-rtdb.firebaseio.com/"
            }
    Private client As FirebaseClient
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            client = New FireSharp.FirebaseClient(fcon)
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
#Region "Condition"
        If (String.IsNullOrWhiteSpace(TextBox1.Text) AndAlso String.IsNullOrWhiteSpace(TextBox2.Text)) Then
            MessageBox.Show("Please fill all the fields")
            Return
        End If
#End Region
        Dim res = client.Get("Users/" + TextBox1.Text)
        Dim resUser = res.ResultAs(Of MyUser)

        Dim curUser As New MyUser With {
        .Username = TextBox1.Text,
        .Password = TextBox2.Text
        }

        If (MyUser.IsEqual(resUser, curUser)) Then
            MessageBox.Show("Successfull Login")
            If (resUser.UserType.Equals("admin")) Then
                Me.Hide()
                Ahome.Show()

            Else
                Me.Hide()
                UHome.Show()
            End If

        Else
                MyUser.ShowError()
            End If

    End Sub
End Class
