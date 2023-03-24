Imports System.Collections.ObjectModel
Imports System.Data.SQLite
Imports System.Net.Mail
Imports System.Reflection.Emit
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
        'TextBox1.PlaceholderText = "Enter details : " & Environment.NewLine & " Item Description" & Environment.NewLine & " How you lost it" & Environment.NewLine & " etc."

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SendEmail()
        MessageBox.Show("Notified to the User!")
        connection.Open()
        Dim command As New SQLiteCommand("DELETE FROM logs WHERE logid = @iid", connection)
        command.Parameters.AddWithValue("@iid", logid)
        command.ExecuteNonQuery()
        connection.Close()
        Me.Hide()
    End Sub


    Public Sub SendEmail()
        Try
            connection.Open()

            Dim email As String = TextBox2.Text
            Dim contact As String = "  ADMIN OFFICE -[ADMIN BLOCK]  "

            Dim smtpClient As New SmtpClient("smtp.gmail.com", 587)
            smtpClient.EnableSsl = True
            smtpClient.Credentials = New Net.NetworkCredential("study.time0604@gmail.com", "qiiobaukqjpwclli")

            Dim message As New MailMessage()
            message.From = New MailAddress("study.time0604@gmail.com")
            message.To.Add(New MailAddress(email))
            message.Subject = "Mail Regarding your item Claim"
            message.Body = "Your Claim request is accepted! Please Contact " & contact & " And enquire about your item. - LOFO TEAM"
            message.IsBodyHtml = False
            smtpClient.Send(message)

            connection.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Sub

End Class