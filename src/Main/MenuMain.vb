Imports DBEdit.Model

Imports MoreLinq
Partial Public Class MainUI

    Private Sub CopyTo(action As Action(Of DataTable, String), target As String)

        Try
            Cursor = Cursors.WaitCursor
            Cursor.Position = Cursor.Position ' https://stackoverflow.com/a/4609917/338101

            Dim context As Context = GetContext()
            Dim table As Table = context.Table
            Dim firstrow As Integer = 1
            Dim lastrow As Integer = table.RowCount
            Dim firstcol As Integer = 1
            Dim lastcol As Integer = table.Columns.Count - 1 ' -1 = HiddenRowNumber

            Dim sc As List(Of DataGridViewCell) = context.Grid.SelectedCells.OfType(Of DataGridViewCell).ToList

            If sc.Count > 1 Then ' Specific cell selection, not the whole table

                ' Get the row and column offsets of the lowest and highest row and columns in the cells he selected
                firstrow = sc.MinBy(Function(q) q.RowIndex).First.RowIndex + 1
                firstcol = sc.MinBy(Function(q) q.ColumnIndex).First.ColumnIndex + 1
                lastrow = sc.MaxBy(Function(q) q.RowIndex).Last.RowIndex + 1
                lastcol = sc.MaxBy(Function(q) q.ColumnIndex).Last.ColumnIndex + 1

            End If

            ' If he selected the 'new' row as well, ignore it
            lastrow = Math.Min(lastrow, table.RowCount)

            Dim dt As DataTable = table.Cache.CompleteDataTable(firstrow, firstcol, lastrow, lastcol)

            action(dt, table.Name)

            SetStatus($"{(lastcol - firstcol + 1) * (lastrow - firstrow + 1):#,##0} cells copied to {target}")

        Catch ex As Exception
            HandleError(ex)

        Finally
            Cursor = Cursors.Default
            Cursor.Position = Cursor.Position

        End Try

    End Sub
    Private Sub CopyToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CopyToClipboardToolStripMenuItem.Click

        CopyTo(AddressOf TableToClipboard, "Clipboard")

    End Sub
    Private Sub CopyToExcelToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CopyToExcelToolStripMenuItem.Click

        CopyTo(AddressOf TableToExcel, "Excel")

    End Sub
    Private Sub FileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileToolStripMenuItem.Click

        PopulateRecent()

    End Sub
    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click

        Try
            Dim loginui As New LoginUI(World)

            Dim result As DialogResult = loginui.ShowDialog(Me)

            If result = DialogResult.OK Then

                Open(loginui.Server, loginui.Database)

            End If

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub PreferencesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PreferencesToolStripMenuItem.Click

        Dim preferui As New PreferencesUI

        Dim result As DialogResult = preferui.ShowDialog(Me)

    End Sub
    Private Sub Recent_Click(sender As Object, e As EventArgs)

        Dim tsmi As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        Dim recent As Recent = DirectCast(tsmi.Tag, Recent)

        Open(recent.Server, recent.Database)

    End Sub
    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click

        Try
            Dim tp As TabPage = tbc.SelectedTab
            Dim grid As Grid = tp.Controls.OfType(Of Grid).Single
            Dim table As Table = grid.Table

            grid.Invalidate()

            SetStatus("Refreshed")

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub Quit_Click(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem.Click
        Me.Close()
    End Sub

End Class
