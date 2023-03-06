Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports FireSharp.Config
Imports FireSharp.Response
Imports FireSharp.Interfaces
Imports FireSharp
Imports System.Text
Imports System.Drawing.Imaging
Imports System.IO

Public Class PostItem
    Shared random As New Random()
    Dim encodeType As ImageFormat = ImageFormat.Jpeg
    Dim decodingString As String = String.Empty
    Private fcon As New FirebaseConfig() With {
                .AuthSecret = "C5uzbAuqUfaFh5JFoGdTps4ka0kk0Hq3T5cUE2cU",
                .BasePath = "https://lofo-7c69f-default-rtdb.firebaseio.com/"
            }
    Private client As FirebaseClient
    Private Sub PostItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label3.Hide()
        Try
            client = New FireSharp.FirebaseClient(fcon)
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

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
        Dim encodeTypeString As String = String.Empty
        If (PictureBox1.ImageLocation.ToLower.EndsWith(".jpg")) Then
            encodeType = ImageFormat.Jpeg
            encodeTypeString = "data:image/jpeg;base64,"
        ElseIf (PictureBox1.ImageLocation.ToLower.EndsWith(".png")) Then
            encodeType = ImageFormat.Png
            encodeTypeString = "data:image/png;base64,"
        ElseIf (PictureBox1.ImageLocation.ToLower.EndsWith(".gif")) Then
            encodeType = ImageFormat.Gif
            encodeTypeString = "data:image/gif;base64,"
        End If
        decodingString = encodeTypeString
        Dim id As String
        id = CType(random.Next(0, 9999999), String)
        Dim newItem As New MyItem() With
        {
         .ImageStrings = encodeTypeString & ImageToBase64(PictureBox1.Image, encodeType),
         .ItemId = id,
         .username = Login.TextBox1.Text,
         .Itemname = TextBox1.Text,
         .ItemDescription = TextBox2.Text
        }

        Dim setter = client.Set("Items/" + id, newItem)
        PictureBox1.Image = Nothing
    End Sub


    Public Function ImageToBase64(ByVal image As Image, ByVal format As ImageFormat) As String
        Using ms As New MemoryStream()
            image.Save(ms, format)
            Dim imageBytes As Byte() = ms.ToArray()
            Dim base64String As String = Convert.ToBase64String(imageBytes)
            Return base64String
        End Using
    End Function
End Class