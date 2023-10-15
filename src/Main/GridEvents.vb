Imports DBEdit.Model
Imports System.Math
Imports MoreLinq

Partial Public Class MainUI

    Private Sub dgv_CellClick(sender As Object, e As DataGridViewCellEventArgs)

        Try
            Dim context As Context = GetContext(e.RowIndex, e.ColumnIndex)
            Dim grid As Grid = context.Grid

            If e.ColumnIndex < 0 Then ' RowHeaderClick 

                If e.RowIndex < 0 Then ' Select all 
                    grid.ModifySelection(New Rectangle(0, 0, grid.ColumnCount, grid.RowCount - 1))
                Else
                    grid.ModifySelection(New Rectangle(0, context.RowIndex, grid.ColumnCount, 1))
                End If

            End If

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub dgv_CellContextMenuStripNeeded(sender As Object, e As DataGridViewCellContextMenuStripNeededEventArgs)

        Try
            Dim context As Context = GetContext(e.RowIndex, e.ColumnIndex)
            Dim grid As Grid = context.Grid

            ' Context menus are only available when he selects a single cell that is not the top-left 'select all' cell
            If e.ColumnIndex < 0 AndAlso e.RowIndex < 0 Then

                e.ContextMenuStrip = Nothing
                SetStatus("No context menu is available here", Severity.Warning)
                Exit Sub

            End If

            If e.ColumnIndex < 0 Then ' Right-clicked a row header

                If Not grid.
                    SelectedCells.
                    OfType(Of DataGridViewCell).
                    Any(Function(q) q.RowIndex = e.RowIndex) Then

                    grid.CurrentCell = grid.Rows(context.RowIndex).Cells(0)
                End If

                e.ContextMenuStrip = cmsRows
                Exit Sub

            End If

            If e.RowIndex < 0 Then ' Right-clicked a column header

                If Not grid.
                    SelectedCells.
                    OfType(Of DataGridViewCell).
                    Any(Function(q) q.ColumnIndex = e.ColumnIndex) Then

                    grid.CurrentCell = grid.Rows(0).Cells(e.ColumnIndex)
                End If

                e.ContextMenuStrip = cmsColumns
                HideColumnsToolStripMenuItem.Enabled = grid.
                                                       Columns.
                                                       OfType(Of DataGridViewColumn).
                                                       Where(Function(q) q.Visible).
                                                       Count > 1
                Exit Sub

            End If

            ' Right-clicked a single cell

            e.ContextMenuStrip = cmsCells

            EditFieldToolStripMenuItem.Enabled = context.Column.Updateable

            grid.ClearSelection()

            grid.CurrentCell = grid.Rows(context.RowIndex).Cells(context.ColumnIndex)

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub dgv_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)

        Try
            ' For debugging --------------------------------

            'Dim v As String
            'If isEmpty(e.Value) Then
            '    v = "[Nothing]"
            'Else
            '    Dim t As Type = e.Value.GetType
            '    v = $"{e.Value.ToString} {t}"

            '    Dim msg As String = $"CellFormatting {e.RowIndex},{e.ColumnIndex} {v} : {e.DesiredType.ToString}"

            '    Debug.WriteLine(msg)
            'End If

            ' Done debugging --------------------------------

            ' Bug in the way DataGridView handles bit variables,
            ' If the column is null, it raises formaterror.
            ' Workaround: make it 'false'

            If e.DesiredType Is GetType(Boolean) Then
                If e.Value Is Nothing Then
                    e.Value = False
                    e.FormattingApplied = True
                End If
            End If

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub dgv_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs)

        If e.ColumnIndex < 0 Then
            Exit Sub
        End If

        Dim grid As Grid = Nothing

        Try
            grid = DirectCast(sender, Grid)
            e.ToolTipText = grid.Table.Columns(e.ColumnIndex).Tooltip

        Catch ex As Exception

            ' Don't croak if grid is Nothing
            Dim colname As String = "[grid is Nothing!]"
            colname = grid?.Table.Columns(e.ColumnIndex).Name
            Dim msg As String = $"Tooltip for {colname} failed {ex.Message}"
            Logger.Error(msg)
            SetStatus(msg, Severity.Error)
        End Try

    End Sub
    Private Sub dgv_CellValueNeeded(sender As Object, e As DataGridViewCellValueEventArgs)

        Dim grid As Grid = Nothing
        Dim value As Object = "[no value]"

        Try
            grid = DirectCast(sender, Grid)

            value = grid.Value(e.RowIndex, e.ColumnIndex)

            Dim result As Object = grid.Model.ToNet(grid.Table.Columns(e.ColumnIndex).Datatype, value)

            e.Value = result

        Catch ex As Exception

            ' Don't croak if grid is Nothing
            Dim colname As String = "[grid is Nothing!]"
            colname = grid?.Table.Columns(e.ColumnIndex).Name
            Dim msg As String = $"{colname} value {value} {ex.Message}"
            Logger.Error(msg)

            ' *MUST* BeginInvoke orelse dgv_CellValueNeeded will get called endlessly
            BeginInvoke(Sub() SetStatus(msg, Severity.Error))

            If grid IsNot Nothing Then
                grid.NewValue(e.ColumnIndex) = Nothing
            End If

            e.Value = Nothing

        End Try

    End Sub
    Private Sub dgv_CellValuePushed(sender As Object, e As DataGridViewCellValueEventArgs)

        Try
            Dim context As Context = GetContext(e.RowIndex, e.ColumnIndex, e.Value)

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub dgv_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs)

        Try
            If e.ColumnIndex < 0 Then
                Exit Sub
            End If

            If e.Button = MouseButtons.Left Then

                Dim context As Context = GetContext(e.RowIndex, e.ColumnIndex)
                Dim grid As Grid = context.Grid

                grid.ModifySelection(New Rectangle(e.ColumnIndex, 0, 1, grid.RowCount - 1))

            End If


        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub dgv_DataError(sender As Object, e As System.Windows.Forms.DataGridViewDataErrorEventArgs)

        Try
            Dim msg As String = $"Error at rowindex {e.RowIndex} columnindex {e.ColumnIndex} : {e.Exception.Message} {e.Context}"

            SetStatus(msg, Severity.Error)

        Catch ex As Exception
            HandleError(ex)
        End Try


    End Sub
    Private Sub dgv_KeyDown(sender As Object, e As KeyEventArgs)

        Try
            Dim grid As Grid = DirectCast(sender, Grid)

            Select Case e.KeyCode

                Case Keys.Escape

                    grid.ClearEdits()
                    grid.ClearSelection()
                    grid.Invalidate()
                    SetStatus("")

                Case Keys.Delete

                    ' If some entire rows are selected, delete the rows
                    If grid.SelectedRows.Count > 0 Then

                        DeleteSelectedRows(sender, e)

                        ' or if some cells are selected, clear them
                    ElseIf grid.SelectedCells.Count > 0 Then

                        DeleteSelectedCells(sender, e)

                    Else
                        SetStatus("Nothing selected", Severity.Warning)

                    End If

            End Select

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub dgv_RowDirtyStateNeeded(sender As Object, e As QuestionEventArgs)

        Try
            Dim grid As Grid = DirectCast(sender, Grid)

            e.Response = grid.isRowDirty

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub dgv_RowValidating(sender As Object, e As DataGridViewCellCancelEventArgs)

        Dim grid As Grid = Nothing
        Dim query As String = "[sql not prepared]"

        Try
            grid = DirectCast(sender, Grid)

            ' This event gets called frequently.
            ' Don't go through the context rigmarole if nothing was modified in this row.
            If Not grid.IsCurrentRowDirty Then
                Exit Sub
            End If

            Dim context As Context = GetContext(e.RowIndex, e.ColumnIndex)
            Dim table As Table = context.Table
            Dim model As Model = grid.Model

            ' Which columns have been modified?
            Dim varcols As IEnumerable(Of Column) =
                table.
                Columns.
                Where(Function(q) grid.NewValue(q.Index) IsNot NoValue)

            ' If none of the primary cells have values, we're up for an insert

            Dim insert As Boolean =
                table.
                PrimaryKeys.
                All(Function(q) grid.Rows(e.RowIndex).Cells(q.Index).Value Is Nothing)

            If insert Then

                ' INSERT INTO a,b,c,d...
                Dim columns As String = String.Join(",",
                    varcols.Select(Function(q) Box(q.Name)))

                ' VALUES(a,b,c,d...
                Dim values As String = String.Join(",",
                    varcols.
                    Select(Function(q)
                               Return model.ToSQL(grid.NewValue(q.Index), q.Datatype)
                           End Function))

                query = "INSERT INTO " & Box(table.Name) & "(" & columns & ") VALUES(" & values & ")"

                Dim inserted As Integer = model.ExecuteNonQuery(query)

                If inserted = 0 Then
                    SetStatus($"No rows inserted {query}", Severity.Error)
                Else
                    SetStatus($"{inserted} row inserted: {query}", Severity.Info)
                End If

            Else ' update

                ' UPDATE table SET a=x,b=y,...
                Dim columns As String = String.Join(",",
                        varcols.
                        Select(Function(q) Box(q.Name) & "=" &
                                           model.ToSQL(grid.NewValue(q.Index), q.Datatype)))

                ' WHERE pk1=x,pk2=y...
                Dim whereclause As String = String.Join(" AND ",
                        context.
                        Table.
                        PrimaryKeys.
                        Select(Function(q) Box(q.Name) & "=" &
                                           model.ToSQL(table.Value(e.RowIndex, q.Index), q.Datatype)))

                query = "UPDATE " & Box(table.Name) & " SET " & columns & " WHERE " & whereclause

                Dim updated As Integer = model.ExecuteNonQuery(query)

                If updated = 0 Then
                    SetStatus($"No rows updated: {query}", Severity.Error)
                Else
                    SetStatus($"{updated} row updated: {query}", Severity.Info)
                End If
            End If

            grid.ClearEdits()
            grid.Invalidate()

        Catch ex As Exception

            ' Get rid of useless '... has been terminated' in the standard message

            Dim msg As String = ex.
                Message.
                Replace("The statement has been terminated.", "")

            ' and ask him whether he wants to try again or give up

            If ShowBox(msg & vbCrLf & query & vbCrLf & vbCrLf &
                       "'Yes' to resume editing, 'No' to discard edits", "Row update failed",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.Yes Then

                e.Cancel = True
            Else
                ' We must clear edits first, or else invalidate will cause a recursive dgv_RowValidating
                grid.ClearEdits()

                ' and the Invalidate has to be invoked, we can't invalidate in an event handler
                BeginInvoke(Sub() grid.Invalidate())
            End If

        End Try

    End Sub
    Private Sub dgv_SelectionChanged(sender As Object, e As EventArgs)

        Try
            ' BeginInvoke, we're still on the previous cell
            BeginInvoke(Sub() ShowRowCounts())

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
End Class
