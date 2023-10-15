Imports System.ComponentModel

Partial Public Class MainUI
    Private Sub Navigator1_Message(msg As String) Handles Navigator1.Message

        SetStatus(msg, Severity.Error)

    End Sub
    Private Sub Navigator1_MoveAdd() Handles Navigator1.MoveAdd

        Try
            SetStatus("")
            Dim grid As Grid = tbc.SelectedTab.Controls.OfType(Of Grid).Single
            grid.CurrentCell = grid.Rows(grid.Table.RowCount).Cells(0)

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub Navigator1_MoveFirst() Handles Navigator1.MoveFirst

        Try
            SetStatus("")
            Dim grid As Grid = tbc.SelectedTab.Controls.OfType(Of Grid).Single
            grid.CurrentCell = grid.Rows(0).Cells(0)

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub Navigator1_MoveLast() Handles Navigator1.MoveLast

        Try
            SetStatus("")
            Dim grid As Grid = tbc.SelectedTab.Controls.OfType(Of Grid).Single
            grid.CurrentCell = grid.Rows(grid.RowCount - 2).Cells(0)

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub Navigator1_MoveNext() Handles Navigator1.MoveNext

        Try
            SetStatus("")
            Dim grid As Grid = tbc.SelectedTab.Controls.OfType(Of Grid).Single
            Dim cell As DataGridViewCell = grid.CurrentCell

            If cell Is Nothing Then
                SetStatus("No current cell", Severity.Error)
                Exit Sub
            End If

            If cell.RowIndex + 1 >= grid.Table.RowCount Then
                SetStatus("Already at end", Severity.Error)
                Exit Sub
            End If

            grid.CurrentCell = grid.Rows(cell.RowIndex + 1).Cells(0)

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub Navigator1_MovePrevious() Handles Navigator1.MovePrevious

        Try
            SetStatus("")
            Dim grid As Grid = tbc.SelectedTab.Controls.OfType(Of Grid).Single
            Dim cell As DataGridViewCell = grid.CurrentCell

            If cell Is Nothing Then
                SetStatus("No current cell", Severity.Warning)
                Exit Sub
            End If

            If cell.RowIndex = 0 Then
                SetStatus("Already at start", Severity.Warning)
                Exit Sub
            End If

            grid.CurrentCell = grid.Rows(cell.RowIndex - 1).Cells(0)
            grid.Invalidate()

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub Navigator1_MoveTo(index As Integer) Handles Navigator1.MoveTo

        Try
            SetStatus("")
            Dim grid As Grid = tbc.SelectedTab.Controls.OfType(Of Grid).Single
            index = Clamp(index, 1, grid.RowCount)
            grid.CurrentCell = grid.Rows(index - 1).Cells(0)

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
End Class
