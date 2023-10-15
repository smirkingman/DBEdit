Imports DBEdit.Model

Imports MoreLinq
Partial Public Class MainUI

    Private Sub HideColumnsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideColumnsToolStripMenuItem.Click

        Dim context As Context = GetContext()
        context.ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        Dim grid As Grid = context.Grid

        If grid.Columns.OfType(Of DataGridViewColumn).Where(Function(q) q.Visible).Count < 2 Then
            ' This should never happen, we disabled hide if only one remaining column is visible
            ' (in dgv_CellContextMenuStripNeeded)
            SetStatus("Can't hide single remaining column", Severity.Error)
            Exit Sub
        End If

        grid.Columns(context.ColumnIndex).Visible = False

        ' Find the first visible column on the right of the one we're hiding
        Dim showcolumn As DataGridViewColumn = grid.
                                               Columns.
                                               OfType(Of DataGridViewColumn).
                                               Where(Function(q) q.Visible AndAlso
                                                                 q.DisplayIndex > grid.Columns(context.ColumnIndex).DisplayIndex).
                                               LastOrDefault

        ' Ah, hiding right-most column, move to the one on the left then
        If showcolumn Is Nothing Then
            showcolumn = grid.
                         Columns.
                         OfType(Of DataGridViewColumn).
                         Where(Function(q) q.Visible).
                         Last

        End If

        grid.CurrentCell = grid(showcolumn.DisplayIndex, context.RowIndex)

        ShowRowCounts()

    End Sub
    Private Sub Sorting(sender As Object, e As EventArgs) Handles _
        AscendingToolStripMenuItem.Click, DescendingToolStripMenuItem.Click, UnsortedToolStripMenuItem.Click

        Dim tsmi As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        Dim context As Context = GetContext()
        context.Verb = tsmi.Text.Replace("&", "")
        SortFromContext(context)

    End Sub
    Private Sub UnhideColumnsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnhideColumnsToolStripMenuItem.Click

        Dim context As Context = GetContext()
        context.ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        Dim grid As Grid = context.Grid

        For Each col As DataGridViewColumn In grid.Columns
            col.Visible = True
        Next

        ShowRowCounts()

    End Sub
    Private Sub SortFromContext(context As Context)

        Try
            context.Table.SortOn(context.Column, context.Verb)

            context.Grid.Invalidate()

            SetStatus($"Sorted on {context.Table.Sort.ToString} {context.Table.SortOrder}")

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub

End Class
