Imports System.Windows
Imports System.Data.SQLite
Imports System.Collections.ObjectModel
Imports System.Security.Cryptography
Imports System.Text
Public Class Login

    Dim connectionString As String = "Data Source=C:\Users\mojog\Desktop\LoFo\mydatabase.db;Version=3;"
    Public connection As New SQLiteConnection(connectionString)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connection.Open()
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim username As String = TextBox1.Text
        Dim password As String = TextBox2.Text
#Region "Condition"
        If (String.IsNullOrWhiteSpace(TextBox1.Text) AndAlso String.IsNullOrWhiteSpace(TextBox2.Text)) Then
            MessageBox.Show("Please fill all the fields")
            Return
        End If

#End Region
        Dim sha256 As SHA256 = SHA256.Create()
        Dim hash As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
        Dim hashedPassword As String = Convert.ToBase64String(hash)

        Dim query As String = "SELECT usertype FROM users WHERE username = @username AND password = @password"
        Dim command As New SQLiteCommand(query, connection)
        command.Parameters.AddWithValue("@username", username)
        command.Parameters.AddWithValue("@password", hashedPassword)

        Dim reader As SQLiteDataReader = command.ExecuteReader()
        If reader.Read() Then
            Dim userType As String = reader.GetString(0)
            If userType = "admin" Then
                MessageBox.Show("Welcome, admin!")
                Me.Hide()
                Ahome.Show()
                Label3.Text = TextBox1.Text
                TextBox1.Clear()
                TextBox2.Clear()
            Else
                MessageBox.Show("Welcome, " + TextBox1.Text)
                Me.Hide()
                UHome.Show()
                TextBox1.Clear()
                TextBox2.Clear()

            End If

        Else
            MessageBox.Show("Incorrect username or password.")
            TextBox1.Clear()
            TextBox2.Clear()
        End If
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            ' The "Enter" key was pressed
            e.Handled = True ' Prevents the "ding" sound
            Button1.PerformClick() ' Programmatically click the button
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            ' The "Enter" key was pressed
            e.Handled = True ' Prevents the "ding" sound
            Button1.PerformClick() ' Programmatically click the button
        End If
    End Sub
End Class
