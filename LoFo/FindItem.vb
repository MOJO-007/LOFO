Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Text
Imports System.IO
Imports System.Windows
Imports System.Data.SQLite
Imports System.Collections.ObjectModel
Imports System.Runtime.CompilerServices

Public Class FindItem
    Dim connectionString As String = "Data Source=C:\Users\mojog\Desktop\LoFo\mydatabase.db;Version=3;"
    Public connection As New SQLiteConnection(connectionString)
    Private Sub FindItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connection.Open()
        DataGridView1.ReadOnly = True
        Dim sql As String = "SELECT id, item_title, photo_path FROM found_items"
        Dim cmd As New SQLiteCommand(sql, connection)
        Dim adapter As New SQLiteDataAdapter(cmd)
        Dim table As New DataTable()
        adapter.Fill(table)


        DataGridView1.AutoGenerateColumns = False
        DataGridView1.RowTemplate.Height = 200
        DataGridView1.AllowUserToAddRows = False


        Dim colId As New DataGridViewTextBoxColumn()
        colId.DataPropertyName = "id"
        colId.HeaderText = "Item ID"
        colId.Name = "colId"
        DataGridView1.Columns.Add(colId)

        Dim colTitle As New DataGridViewTextBoxColumn()
        colTitle.DataPropertyName = "item_title"
        colTitle.HeaderText = "Item Title"
        colTitle.Name = "colTitle"
        DataGridView1.Columns.Add(colTitle)

        Dim colImage As New DataGridViewImageColumn()
        colImage.DataPropertyName = "photo_path"
        colImage.HeaderText = "Image"
        colImage.Name = "colImage"
        colImage.ImageLayout = DataGridViewImageCellLayout.Stretch
        DataGridView1.Columns.Add(colImage)

        For Each row As DataRow In table.Rows
            Dim id As Integer = Convert.ToInt32(row("id"))
            Dim title As String = row("item_title").ToString()
            Dim imagePath As String = row("photo_path").ToString()
            Dim image As Image = Image.FromFile(imagePath)
            DataGridView1.Rows.Add(id, title, image)
        Next

        Me.Controls.Add(DataGridView1)

        DataGridView1.Columns(0).Width = 300
        DataGridView1.Columns(1).Width = 300
        DataGridView1.Columns(2).Width = 300
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        UHome.Show()
    End Sub
End Class