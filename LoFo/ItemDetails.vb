Imports System.Data.Entity.Infrastructure
Imports System.Data.SQLite

Public Class ItemDetails
    Private itemTitle As String
    Private itemId As Integer
    Private itemImage As Image

    Public Sub New(ByVal id As Integer, ByVal title As String)

        ' This call is required by the designer.
        InitializeComponent()
        itemTitle = title
        itemId = id


        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        MyBase.OnLoad(e)
        Dim connectionString As String = "Data Source=C:\Users\mojog\Desktop\LoFo\mydatabase.db;Version=3;"
        Dim connection As New SQLiteConnection(connectionString)
        connection.Open()
        Dim command As New SQLiteCommand("SELECT photo_path from found_items WHERE id=@id", connection)
        command.Parameters.AddWithValue("@id", itemId)
        Dim result As Object = command.ExecuteScalar()
        Dim imagepath As String = result.ToString()
        connection.Close()
        Label1.Text = itemTitle
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.ImageLocation = imagepath
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class