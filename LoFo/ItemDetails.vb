Imports System.Collections.ObjectModel
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports System.Data.SQLite

Public Class ItemDetails
    Private itemTitle As String
    Private itemId As Integer
    Private itemImage As Image
    Dim connectionString As String = "Data Source=C:\Users\mojog\Desktop\LoFo\mydatabase.db;Version=3;"
    Public connection As New SQLiteConnection(connectionString)

    Public Sub New(ByVal id As Integer, ByVal title As String)
        InitializeComponent()
        itemTitle = title
        itemId = id
    End Sub

    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        MyBase.OnLoad(e)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        connection.Open()
        Dim command As New SQLiteCommand("SELECT photo_path from found_items WHERE id=@id", connection)
        command.Parameters.AddWithValue("@id", itemId)
        Dim result As Object = command.ExecuteScalar()
        Dim imagepath As String = result.ToString()

        Label1.Text = itemTitle
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.ImageLocation = imagepath


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
#Region "Condition"
        If (String.IsNullOrWhiteSpace(TextBox1.Text) AndAlso String.IsNullOrWhiteSpace(TextBox2.Text)) Then
            MessageBox.Show("Please fill all the fields")
            Return
        End If
#End Region
        Dim claimant_desc As String = TextBox2.Text
        Dim claimant_mail As String = TextBox1.Text
        Dim command As New SQLiteCommand("SELECT item_description from found_items WHERE id=@id", connection)
        command.Parameters.AddWithValue("@id", itemId)
        Dim result As Object = command.ExecuteScalar()
        Dim uploader_desc As String = result.ToString()

        Dim qry As New SQLiteCommand("INSERT INTO claims(id,claimant_desc, claimant_mail, uploader_desc) VALUES (@id, @cdesc, @mail, @udesc)", connection)
        qry.Parameters.AddWithValue("@id", itemId)
        qry.Parameters.AddWithValue("@cdesc", claimant_desc)
        qry.Parameters.AddWithValue("@mail", claimant_mail)
        qry.Parameters.AddWithValue("@udesc", uploader_desc)
        Dim rc As Integer = qry.ExecuteNonQuery()
        If (rc > 0) Then
            MessageBox.Show("Claim request sent, Thank you for your efforts!")
            Me.Close()
            connection.Close()
        Else
            MessageBox.Show("Oops, Item is not Uploaded.")
            Me.Refresh()
        End If
    End Sub

    Private Sub ItemDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class