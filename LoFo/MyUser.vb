Public Class MyUser
    Public Property Username() As String
    Public Property Password() As String

    Public Property UserType() As String
    Public Shared K As Integer

    Private Shared TheError As String = "Username not found"

    Public Shared Sub ShowError()
        MessageBox.Show(TheError)
    End Sub

    Public Shared Function IsEqual(user1 As MyUser, user2 As MyUser)
        If (user1 Is Nothing Or user2 Is Nothing) Then
            TheError = "Username not found"
            Return False
        End If
        If (user1.Username <> user2.Username) Then
            TheError = "Username not found"
            Return False
        ElseIf (user1.Password <> user2.Password) Then
            TheError = "Username and password does not match"
            Return False
        End If
        If (user1.UserType = "admin") Then
            K = 1
        Else
            K = 2
        End If
        Return True

    End Function

End Class
