Imports System.Data.SQLite

Public Class ResolveLogs
    Dim connectionString As String = "Data Source=C:\Users\mojog\Desktop\LoFo\mydatabase.db;Version=3;"
    Public connection As New SQLiteConnection(connectionString)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Ahome.Show()
    End Sub

    Private Sub ResolveLogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim adapter As New SQLiteDataAdapter("SELECT * from logs", connection)
        Dim table As New DataTable()
        adapter.Fill(table)
        DataGridView1.DataSource = table
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 Then

            Dim logID As Integer = CInt(DataGridView1.Rows(e.RowIndex).Cells(0).Value)
            Dim logdesc As String = CStr(DataGridView1.Rows(e.RowIndex).Cells(1).Value)
            Dim logmail As String = CStr(DataGridView1.Rows(e.RowIndex).Cells(2).Value)

            Dim logDetailsform As New LogDetails(logID, logdesc, logmail)

            logDetailsform.ShowDialog()
            Me.Hide()
            Ahome.Show()
        End If
    End Sub
End Class