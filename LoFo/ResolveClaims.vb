Imports System.Data.SQLite

Public Class ResolveClaims
    Dim connectionString As String = "Data Source=C:\Users\mojog\Desktop\LoFo\mydatabase.db;Version=3;"
    Public connection As New SQLiteConnection(connectionString)
    Private Sub ResolveClaims_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim adapter As New SQLiteDataAdapter("SELECT claims.claim_id, found_items.id, claims.status FROM claims INNER JOIN found_items ON claims.id = found_items.id", connection)
        Dim table As New DataTable()
        adapter.Fill(table)
        DataGridView1.DataSource = table
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle

        DataGridView1.Columns(0).Width = 135
        DataGridView1.Columns(1).Width = 135
        DataGridView1.Columns(2).Width = 139

        Dim font As New Font("Unispace", 14, FontStyle.Bold)
        DataGridView1.DefaultCellStyle.Font = font
        DataGridView1.Rows(0).Selected = False

    End Sub



    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 2 AndAlso e.RowIndex >= 0 Then
            ' Retrieve the information for the selected item from the database
            Dim claimID As Integer = CInt(DataGridView1.Rows(e.RowIndex).Cells(0).Value)
            Dim itemId As Integer = CStr(DataGridView1.Rows(e.RowIndex).Cells(1).Value)
            'Dim itemImage As Image = Image.FromFile(CStr(DataGridView1.Rows(e.RowIndex).Cells(2).Value))

            ' Create a new instance of the ItemDetailsForm form with the item information as arguments
            Dim claimDetailsform As New ClaimDetails(claimID, itemId)

            ' Show the new form as a dialog box
            claimDetailsform.ShowDialog()
            Me.Hide()
            Ahome.Show()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        Ahome.Show()
    End Sub
End Class