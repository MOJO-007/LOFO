Imports System.Collections.ObjectModel
Imports System.Data.SQLite
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class LogDetails
    Dim logid As Integer
    Dim log_desc As String
    Dim log_mail As String
    Dim connectionString As String = "Data Source=C:\Users\mojog\Desktop\LoFo\mydatabase.db;Version=3;"
    Public connection As New SQLiteConnection(connectionString)
    Public Sub New(ByVal id As Integer, ByVal desc As String, ByVal mail As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        logid = id
        log_desc = desc
        log_mail = mail
    End Sub

    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        MyBase.OnLoad(e)
        Label1.Text = logid
        TextBox1.Text = log_desc
        TextBox2.Text = log_mail
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MessageBox.Show("Notified to the User!")
        connection.Open()
        Dim command As New SQLiteCommand("DELETE FROM logs WHERE logid = @iid", connection)
        command.Parameters.AddWithValue("@iid", logid)
        command.ExecuteNonQuery()
        connection.Close()
        Me.Hide()
    End Sub
End Class