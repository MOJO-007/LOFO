Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports FireSharp.Config
Imports FireSharp.Response
Imports FireSharp.Interfaces
Imports FireSharp
Imports System.Text
Imports System.IO
Imports System.Drawing.Imaging
Imports Newtonsoft.Json
Imports System.Reflection.Emit

Public Class FindItem
    Private fcon As New FirebaseConfig() With {
                .AuthSecret = "C5uzbAuqUfaFh5JFoGdTps4ka0kk0Hq3T5cUE2cU",
                .BasePath = "https://lofo-7c69f-default-rtdb.firebaseio.com/"
            }
    Private client As FirebaseClient
    Private Sub FindItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim base64String As String
        DataGridView1.ReadOnly = True
        Try
            client = New FireSharp.FirebaseClient(fcon)
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

        Dim res As FirebaseResponse = client.Get("Items")
        If res IsNot Nothing AndAlso res.Body IsNot Nothing Then
            Dim data = res.ResultAs(Of Dictionary(Of String, MyItem))()

            Dim table As New DataTable()
            table.Columns.Add("Name")
            table.Columns.Add("Id", GetType(Integer))
            table.Columns.Add("ImageColumn", GetType(Byte()))

            For Each item In data
                Dim row = table.NewRow()
                row("Name") = item.Value.Itemname
                row("Id") = item.Value.ItemId
                base64String = item.Value.ImageStrings
                Dim imageData As Byte() = Convert.FromBase64String(base64String)
                row("ImageColumn") = imageData
                table.Rows.Add(row)
            Next

            DataGridView1.DataSource = table

            Dim imageColumn As DataGridViewImageColumn = DataGridView1.Columns("ImageColumn")
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom
        End If
        ' Set the AutoSizeRowsMode property to None
        DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None

        ' Set the row height to the desired value
        For Each row As DataGridViewRow In DataGridView1.Rows
            row.Height = 200
        Next

        ' Set the width of all columns to 100 pixels
        For Each column As DataGridViewColumn In DataGridView1.Columns
            column.Width = 270
        Next

    End Sub






    Public Function Base64ToImage(ByVal base64code As String) As Image
        Dim imageBytes As Byte() = Convert.FromBase64String(base64code)
        Dim ms As New MemoryStream(imageBytes, 0, imageBytes.Length)
        Dim tmpimage As Image = Image.FromStream(ms, True)
        Return tmpimage
    End Function

End Class