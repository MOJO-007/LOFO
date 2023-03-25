
Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports System.Security.Cryptography
Imports System.Text

Public Class AddUsers
    Dim connectionString As String = "Data Source=C:\Users\mojog\Desktop\LoFo\mydatabase.db;Version=3;"
    Public connection As New SQLiteConnection(connectionString)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim username As String = TextBox1.Text
        Dim password As String = TextBox2.Text
        Dim usertype As String = ComboBox1.Text

        Dim sha256 As SHA256 = SHA256.Create()
        Dim hash As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
        Dim hashedPassword As String = Convert.ToBase64String(hash)
        connection.Open()
        'Dim queryString As String = 
        Dim command As New SQLiteCommand("INSERT INTO users (username, password, usertype) VALUES (@Username, @Password, @UserType);", connection)
        command.Parameters.AddWithValue("@Username", username)
        command.Parameters.AddWithValue("@Password", hashedPassword)
        command.Parameters.AddWithValue("@UserType", usertype)
        'command.ExecuteNonQuery()
        Dim rc As Integer = command.ExecuteNonQuery()
        If (rc > 0) Then
            MessageBox.Show("User is added, Thank you for your efforts!")
            Me.Close()
            Ahome.Show()
            connection.Close()
        Else
            MessageBox.Show("Oops, Item is not Uploaded.")
            TextBox1.Text = ""
            TextBox2.Text = ""
        End If
    End Sub
End Class