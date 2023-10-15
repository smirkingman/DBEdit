Imports DBEdit.Model

Partial Public Class MainUI

    Private Sub ClearAllFilters(sender As Object, e As EventArgs) Handles ClearallToolStripMenuItem.Click

        Try
            Dim context As Context = GetContext()

            Dim cleared As Integer = 0

            For Each column As Column In context.Table.Columns

                If column.isFiltered Then
                    cleared += 1
                End If

                column.ClearFilter()

            Next

            context.Grid.Invalidate()

            SetStatus($"{cleared} filters cleared")

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub ClearFilter(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem.Click

        Try
            Dim context As Context = GetContext()

            context.Column.ClearFilter()

            context.Grid.Invalidate()

            SetStatus($"Filter on {context.Column.Name} cleared")

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub EditField(sender As Object, e As EventArgs) Handles EditFieldToolStripMenuItem.Click

        Try
            Dim context As Context = GetContext()
            context.ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)

            Dim os As Integer = SizeOf(context.Value)
            If os > 32767 Then
                SetStatus($"Value at row {context.RowIndex + 1} column {context.Column.Name} " &
                          $" has length {os:#,##0}, too long to edit", Severity.Error)
                Exit Sub
            End If

            Dim edit As New EditUI(Model, context)
            Dim result As DialogResult = edit.ShowDialog(Me)

            If result = DialogResult.OK Then

                Dim grid As Grid = context.Grid

                grid.Value(context.RowIndex, context.ColumnIndex) = edit.Value
                ' and the 'item' to make the grid show the 'row-modified' glyph
                grid.Item(context.ColumnIndex, context.RowIndex).Value = edit.Value

                SetStatus($"{context.Column.Name} = {edit.ValueText}")
            Else
                SetStatus($"{context.Column.Name} unchanged")
            End If

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub FilterDialog(sender As Object, e As EventArgs) Handles _
            EqualsToolStripMenuItem.Click, NotEqualsToolStripMenuItem.Click, LessThanToolStripMenuItem.Click,
            GreaterThanToolStripMenuItem.Click, ContainsToolStripMenuItem.Click, InToolStripMenuItem.Click,
            LikeToolStripMenuItem.Click, StartsWithToolStripMenuItem.Click

        Try
            Dim context As Context = GetContext()
            Dim tsmi As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
            context.ToolStripMenuItem = tsmi
            context.Verb = tsmi.Text.Replace("&", "")

            Dim filter As New Filter(context)
            context.Column.Filter = filter

            Dim filterui As New FilterUI(filter) With {
                .Location = context.Location
            }

            Dim result As DialogResult = filterui.ShowDialog()

            If result = DialogResult.OK Then

                context.Grid.Invalidate()

            End If

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub FilterNotNull(sender As Object, e As EventArgs) Handles NotNullToolStripMenuItem.Click

        FilterNulls(sender, "NOT ")

    End Sub
    Private Sub FilterNull(sender As Object, e As EventArgs) Handles NullToolStripMenuItem.Click

        FilterNulls(sender, "")

    End Sub
    Private Sub FilterNulls(sender As Object, inverse As String)

        Try
            Dim context As Context = GetContext()

            Dim filter As New Filter(context)
            context.Column.Filter = filter

            context.Column.Filter.Apply($"{inverse}Null", "")

            context.Grid.Invalidate()

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub StatisticsDialog(sender As Object, e As EventArgs) Handles StatisticsToolStripMenuItem.Click

        Try
            Dim context As Context = GetContext()
            context.ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)

            Dim stats As New StatisticsUI(context)

            stats.ShowDialog(Me)

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub

End Class
