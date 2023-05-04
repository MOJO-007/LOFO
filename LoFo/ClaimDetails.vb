Imports System.Data.SQLite
Imports System.Net
Imports System.Net.Mail
Imports System.Reflection.Metadata

Public Class ClaimDetails
    Dim claimId As New Integer
    Dim itemId As New Integer
    Dim connectionString As String = "Data Source=C:\Users\mojog\Desktop\LoFo\mydatabase.db;Version=3;"
    Public connection As New SQLiteConnection(connectionString)
    Public Sub New(ByVal cid As Integer, ByVal iid As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        claimId = cid
        itemId = iid
    End Sub

    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        MyBase.OnLoad(e)
        Dim command As New SQLiteCommand("SELECT photo_path from found_items WHERE id=@id", connection)
        command.Parameters.AddWithValue("@id", itemId)
        connection.Open()
        Dim result As Object = command.ExecuteScalar()
        Dim imagepath As String = result.ToString()
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.ImageLocation = imagepath

        Dim qry As New SQLiteCommand("SELECT item_title,date_found, contact_phone FROM found_items WHERE id=@id", connection)
        qry.Parameters.AddWithValue("@id", itemId)
        Dim reader As SQLiteDataReader = qry.ExecuteReader()
        While reader.Read()
            Dim item_title As String = reader.GetString(0)
            Dim datetime As Date = reader.GetDateTime(1)
            Dim number As String = reader.GetString(2)
            Label3.Text = item_title
            TextBox3.Text = datetime
            Label6.Text = number
        End While

        Dim qry2 As New SQLiteCommand("SELECT claimant_desc, uploader_desc, claimant_mail FROM claims WHERE id=@id", connection)
        qry2.Parameters.AddWithValue("@id", itemId)
        Dim read As SQLiteDataReader = qry2.ExecuteReader()
        While read.Read()
            Dim cdesc As String = read.GetString(0)
            Dim udesc As String = read.GetString(1)
            Dim cmail As String = read.GetString(2)
            TextBox1.Text = udesc
            TextBox2.Text = cdesc
            Label5.Text = cmail

        End While

        connection.Close()

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MessageBox.Show("Claim Accepted! and the user is notified.")
        connection.Open()
        Dim command As New SQLiteCommand("UPDATE claims SET status = @new_value WHERE id = @iid", connection)
        command.Parameters.AddWithValue("@iid", itemId)
        command.Parameters.AddWithValue("@new_value", "accepted")
        command.ExecuteNonQuery()
        Dim command2 As New SQLiteCommand("DELETE FROM found_items WHERE id = @iid", connection)
        command2.Parameters.AddWithValue("@iid", itemId)
        command2.ExecuteNonQuery()
        connection.Close()
        SendEmail()
        Me.Hide()
        Ahome.Show()
    End Sub

    Public Sub SendEmail()
        Try
            connection.Open()

            Dim email As String = Label5.Text
            ''Dim query As String = "SELECT claimant_mail FROM claims WHERE claim_id= @iid;"
            ''Dim com As New SQLiteCommand(query, connection)
            'com.Parameters.AddWithValue("@iid", claimId)
            ' Dim reader As SQLiteDataReader = com.ExecuteReader()

            'If reader.Read() Then
            ' email = reader("claimant_mail").ToString()
            'End If

            ' reader.Close()

            Dim command As New SQLiteCommand("SELECT contact_phone FROM found_items WHERE id = @iid", connection)
            command.Parameters.AddWithValue("@iid", itemId)
            command.ExecuteNonQuery()
            Dim contact As String = command.ToString()


            Dim smtpClient As New SmtpClient("smtp.gmail.com", 587)
            smtpClient.EnableSsl = True
            smtpClient.Credentials = New Net.NetworkCredential("study.time0604@gmail.com", "qiiobaukqjpwclli")

            MessageBox.Show(email)

            Dim message As New MailMessage()
            message.From = New MailAddress("study.time0604@gmail.com")
            message.To.Add(New MailAddress(email))
            message.Subject = "Mail Regarding your item Claim"
            message.Body = "Your Claim request is accepted! Please Contact " & Label6.Text & "And enquire about your item. - LOFO TEAM"
            message.IsBodyHtml = False


            'SmtpClient.UseDefaultCredentials = False


            smtpClient.Send(message)
            connection.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Sub



    Public Sub SendEmail2()
        Try
            connection.Open()

            Dim email As String = Label5.Text
            Dim contact As String = ""


            Dim smtpClient As New SmtpClient("smtp.gmail.com", 587)
            smtpClient.EnableSsl = True
            smtpClient.Credentials = New Net.NetworkCredential("study.time0604@gmail.com", "qiiobaukqjpwclli")

            MessageBox.Show(email)

            Dim message As New MailMessage()
            message.From = New MailAddress("study.time0604@gmail.com")
            message.To.Add(New MailAddress(email))
            message.Subject = "Mail Regarding your item Claim"
            message.Body = "Your Claim request is Rejected! Sorry to inform that you claim was not sactisfied. Kindly raise another detailed claim for review . - LOFO TEAM"
            message.IsBodyHtml = False

            smtpClient.Send(message)
            connection.Close()
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SendEmail2()
        MessageBox.Show("Claim Rejected! and the user is notified.")
        connection.Open()
        Dim command2 As New SQLiteCommand("DELETE FROM claims WHERE claim_id = @iid", connection)
        command2.Parameters.AddWithValue("@iid", claimId)
        command2.ExecuteNonQuery()
        connection.Close()
        Me.Hide()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub
End Class