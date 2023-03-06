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
            Dim column1 As New DataColumn("ImageColumn", GetType(Byte()))
            table.Columns.Add(column1)

            For Each item In data
                Dim row = table.NewRow()
                row("Name") = item.Value.Itemname
                row("Id") = item.Value.ItemId
                'row("ImageColumn") = imageData
                table.Rows.Add(row)
            Next

            DataGridView1.DataSource = table
        End If
    End Sub
End Class