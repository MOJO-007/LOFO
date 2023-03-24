Public Class Ahome
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        ResolveClaims.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Login.Show()
    End Sub

    Private Sub Ahome_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class