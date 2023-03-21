Imports System.Windows
Imports System.Data.SQLite
Imports System.Collections.ObjectModel


Public Class Login

    Dim connectionString As String = "Data Source=C:\Users\mojog\Desktop\LoFo\mydatabase.db;Version=3;"
    Public connection As New SQLiteConnection(connectionString)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connection.Open()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


#Region "Condition"
        If (String.IsNullOrWhiteSpace(TextBox1.Text) AndAlso String.IsNullOrWhiteSpace(TextBox2.Text)) Then
            MessageBox.Show("Please fill all the fields")
            Return
        End If
#End Region
        Dim query As String = "SELECT usertype FROM users WHERE username = @username AND password = @password"
        Dim command As New SQLiteCommand(query, connection)
        command.Parameters.AddWithValue("@username", TextBox1.Text)
        command.Parameters.AddWithValue("@password", TextBox2.Text)

        Dim reader As SQLiteDataReader = command.ExecuteReader()
        If reader.Read() Then
            Dim userType As String = reader.GetString(0)
            If userType = "admin" Then
                MessageBox.Show("Welcome, admin!")
                Me.Hide()
                Ahome.Show()
                TextBox1.Clear()
                TextBox2.Clear()
                'connection.Close()
            Else
                MessageBox.Show("Welcome, " + TextBox1.Text)
                Me.Hide()
                UHome.Show()
                ' connection.Close()
                TextBox1.Clear()
                TextBox2.Clear()

            End If

        Else
            MessageBox.Show("Incorrect username or password.")
            TextBox1.Clear()
            TextBox2.Clear()
            'connection.Close()
        End If
    End Sub
End Class
