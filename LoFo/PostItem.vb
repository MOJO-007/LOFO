Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Text
Imports System.IO
Imports System.Windows
Imports System.Data.SQLite
Imports System.Collections.ObjectModel
Imports System.Diagnostics.Tracing

Public Class PostItem
    Dim connectionString As String = "Data Source=C:\Users\mojog\Desktop\LoFo\mydatabase.db;Version=3;Journal Mode=WAL"
    Public connection As New SQLiteConnection(connectionString)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim img1 As OpenFileDialog = New OpenFileDialog()
        img1.Filter = "choose image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif"
        If img1.ShowDialog() = DialogResult.OK Then
            Label3.Show()
            Label3.Text = img1.FileName
            PictureBox1.ImageLocation = Label3.Text
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim command As New SQLiteCommand("INSERT INTO found_items(item_type, item_description, location_found, date_found, contact_phone, photo_path, username, item_title) VALUES (@item_type, @item_description, @location_found, @date_found, @contact_phone, @photo_path, @username, @item_title)", connection)
        'Dim commandtext As String = "INSERT INTO found_items(item_type, item_description, location_found, date_found, contact_phone, photo_path, username) VALUES (@item_type, @item_description, @location_found, @date_found, @contact_phone, @photo_path, @username)"
        'Using command As New SQLiteCommand(commandtext, connection)


        command.Parameters.AddWithValue("@item_title", TextBox1.Text)
        command.Parameters.AddWithValue("@item_type", ComboBox1.Text)
        command.Parameters.AddWithValue("@item_description", TextBox2.Text)
        command.Parameters.AddWithValue("@location_found", TextBox3.Text)
        command.Parameters.AddWithValue("@date_found", DateTimePicker1.Value.ToString("yyyy-MM-dd"))
        command.Parameters.AddWithValue("@contact_phone", TextBox4.Text)
        command.Parameters.AddWithValue("@photo_path", Label3.Text)
        command.Parameters.AddWithValue("@username", Login.Label3.Text)
        connection.Open()
        Dim rc As Integer = command.ExecuteNonQuery()
        If (rc > 0) Then
            MessageBox.Show("Item is Uploaded, Thank you for your efforts!")
            Me.Close()
            UHome.Show()
            connection.Close()
        Else
            MessageBox.Show("Oops, Item is not Uploaded.")
            Me.Refresh()
        End If
        'End Using
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        UHome.Show()
    End Sub

    Private Sub PostItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class