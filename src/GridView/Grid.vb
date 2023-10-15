Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Math
Imports MoreLinq

Public Class Grid
    Inherits DataGridView

    Public Event Refreshed()
    Public Sub New()

        MyBase.New
        RowTemplate = New GridRow

    End Sub
    Public Sub New(model As Model, table As Table)

        _Model = model
        _Table = table

        Me.Name = table.Name

        RowTemplate = New GridRow

        table.Load()

        VirtualMode = True

        Me.Dock = DockStyle.Fill

        ' The table has one more fields than the grid, the hiddenrownumber user for paging.
        ' To allow us to iterate over NewValue easily, we add 1 extra field
        ' corresponding to hiddenrownumber, even if it will never be used

        ReDim _NewValue(table.Columns.Count) ' and not '-1' as would usually be the case

        AddHandler Application.Idle, AddressOf Application_Idle

    End Sub
    ' Couldn't find another way to autoresize columns once the grid is filled.
    ' When a new grid is created, there is no reliaable way to detect that it is completely populated.
    ' The only remaining option was to set a one-time Application_Idle handler and autosize then.
    Private Sub Application_Idle(sender As Object, e As EventArgs)

        RemoveHandler Application.Idle, AddressOf Application_Idle ' Unhook our ghastly hack immediately
        AutosizeVisible()

    End Sub
    Public Sub AutosizeVisible()

        Try
            AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells)

            ' but don't let them get ridiculously wide
            For Each column As DataGridViewColumn In
                        Columns.
                        OfType(Of DataGridViewColumn).
                        Where(Function(q) q.Width > My.Settings.MaxColWidth)

                column.Width = My.Settings.MaxColWidth
            Next

        Catch ex As Exception
            ' This shouldn't happen, but can if a cell has data of an invalid type.
            ' Example: A datetimeoffset is presented (errroneously) as a string.
            ' This will cause AutoResizeColumns to fail due to failed cell formatting.
            Debug.WriteLine($"AutosizeVisible failed {ex}")
        End Try

    End Sub
    Public Sub ClearEdits()

        ' Clear values that were updated
        For i As Integer = 0 To _NewValue.GetUpperBound(0)
            _NewValue(i) = NoValue
        Next

        ' Clear contexts
        For Each col As Column In Table.Columns
#If DEBUG Then
            Dim ctx As Context = col.Context
            If ctx IsNot Nothing Then
                Logger.Trace($"Context at {ctx.RowIndex},{ctx.ColumnIndex} cleared")
            End If
#End If
            col.Context = Nothing
        Next

    End Sub
    Protected Overrides Function CreateColumnsInstance() As DataGridViewColumnCollection

        Return New GridColCollection(Me) ' our custom collection instead of DataGridViewColumnCollection

    End Function
    Protected Overrides Function CreateRowsInstance() As DataGridViewRowCollection

        Return New GridRowCollection(Me) ' see comment above

    End Function
    Public Property PreviousRow As Integer = -1
    Public Sub ExpandSelection(add As Rectangle, Optional invert As Boolean = False)

        Dim bounds As Rectangle = SelectionBounds()

        If bounds.Width < 0 OrElse invert Then ' Empty selection
            bounds = add
        End If

        Dim range As Rectangle = Rectangle.Union(bounds, add)

        For r As Integer = range.Y To range.Y + range.Height - 1

            For c As Integer = range.X To range.X + range.Width - 1

                If invert Then
                    Me(c, r).Selected = Not Me(c, r).Selected
                Else
                    Me(c, r).Selected = True
                End If

            Next
        Next

    End Sub
    Public Overloads Sub Invalidate()

        With Table

            If .OldSort IsNot Nothing Then

                Columns(.OldSort.Index).HeaderCell.SortGlyphDirection = SortOrder.None

            End If

            If .Sort IsNot Nothing Then

                If .Ascending Then
                    Columns(.Sort.Index).HeaderCell.SortGlyphDirection = SortOrder.Ascending
                Else
                    Columns(.Sort.Index).HeaderCell.SortGlyphDirection = SortOrder.Descending
                End If

            End If

            .Refresh()

        End With

        RowCount = Table.RowCount + 1

        ClearEdits()

        MyBase.Invalidate()

        RaiseEvent Refreshed()

    End Sub
    Public ReadOnly Property isRowDirty As Boolean
        Get
            Return _NewValue.Any(Function(q) q IsNot NoValue)
        End Get
    End Property
    Friend Property Model As Model
    Public Sub ModifySelection(rect As Rectangle)

        If ModifierKeys.HasFlag(Keys.Shift) Then

            ExpandSelection(rect)

        ElseIf ModifierKeys.HasFlag(Keys.Control) Then

            ExpandSelection(rect, True)

        Else ' good old simple left-click

            ClearSelection()
            ExpandSelection(rect)

        End If

    End Sub
    ' When a user modifies cells in a row, we need to generate an update statement only for the dirty cells.
    ' One (amongst many) design faults of DataGridView is the inability to identify these dirty cells,
    ' as there is only a CurrentCellIsDirty property on the grid, instead of a (wiser) Cell.IsDirty.
    ' So we keep and array of new values, corresponding to each column, initialised to NoValue.
    ' This allows us to differentiate between 'the current cell is unmodified' (NoValue),
    ' and 'the current cell has been deleted' (Nothing), which means that we'll set the database value to Null.
    ' This is easier on users, for whom the Delete key is much more intuitive than Control+0, which
    ' sets the current cell to DBNull.Value.
    Public Property NewValue As Object()
    Protected Overrides Sub OnCellPainting(e As DataGridViewCellPaintingEventArgs)

        If IsDesignerHosted(Me) Then
            Exit Sub
        End If

        ' When a column is filtered, show the filter icon in the column header

        Try
            If e.RowIndex <> -1 OrElse e.ColumnIndex < 0 Then ' Only paint column headers
                Exit Sub
            End If

            Dim column As Column = Table.Columns(e.ColumnIndex)

            If Not isEmpty(column.Where) Then

                Using b As New SolidBrush(BackColor)

                    e.PaintBackground(e.CellBounds, False)
                    ' Force left to try and leave space for the icon
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    e.PaintContent(e.CellBounds)

                    Dim icon As Image = My.Resources.filter
                    Dim size As Single = CSng(e.CellBounds.Height / 2)
                    e.Graphics.DrawImage(icon, e.CellBounds.Left + e.CellBounds.Width - (2 * size) - 2,
                                               e.CellBounds.Top + ((e.CellBounds.Height - size) / 2),
                                               size, size)
                    e.Handled = True
                End Using
            End If

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Public Shadows Function SelectedColumns() As IEnumerable(Of DataGridViewColumn)

        ' We have to do this manually, as the grid selection mode is 'cell', 
        ' there are never SelectedRows
        Dim cols As IEnumerable(Of DataGridViewColumn) =
            MyBase.
            SelectedCells.
            OfType(Of DataGridViewCell).
            GroupBy(Function(q) q.ColumnIndex).
            Where(Function(q) q.Count = RowCount).
            Select(Function(q) q.First.OwningColumn)

        Return cols

    End Function
    Public Shadows Function SelectedRows() As IEnumerable(Of DataGridViewRow)

        ' We have to do this manually, as the grid selection mode is 'cell', 
        ' there are never SelectedRows
        Dim rows As IEnumerable(Of DataGridViewRow) =
            MyBase.
            SelectedCells.
            OfType(Of DataGridViewCell).
            GroupBy(Function(q) q.RowIndex).
            Where(Function(q) q.Count = ColumnCount).
            Select(Function(q) q.First.OwningRow)

        Dim result As List(Of DataGridViewRow) = rows.ToList
        Return result

    End Function
    Public Function SelectionBounds() As Rectangle

        If SelectedCells.Count < 1 Then
            Return New Rectangle(0, 0, -1, -1)
        End If

        Dim firstrow As Integer = SelectedCells.
                                  OfType(Of DataGridViewCell).
                                  MinBy(Function(q) q.RowIndex).
                                  Select(Function(q) q.RowIndex).
                                  First

        Dim firstcol As Integer = SelectedCells.
                                  OfType(Of DataGridViewCell).
                                  MinBy(Function(q) q.ColumnIndex).
                                  Select(Function(q) q.ColumnIndex).
                                  First

        Dim lastrow As Integer = SelectedCells.
                                 OfType(Of DataGridViewCell).
                                 MaxBy(Function(q) q.RowIndex).
                                 Select(Function(q) q.RowIndex).
                                 First

        Dim lastcol As Integer = SelectedCells.
                                 OfType(Of DataGridViewCell).
                                 MaxBy(Function(q) q.ColumnIndex).
                                 Select(Function(q) q.ColumnIndex).
                                 First

        Return New Rectangle(firstcol, firstrow, lastcol - firstcol + 1, lastrow - firstrow + 1)

    End Function
    Public ReadOnly Property Table As Table
    Public Overrides Function ToString() As String
        Return $"{Name} Rows {RowCount} Columns {ColumnCount}"
    End Function
    Public Property Value(RowIndex As Integer, ColumnIndex As Integer) As Object
        Get
            ' There are 3 values for a cell:
            '   1. The database value: Table.Value(row,col)
            '   2. The displayed value: Grid.Value(row,col)
            '   3. The new value that the user just typed: _NewValue(col),
            '      which can either be 'NoValue', to indicate that the new value 
            '      hasn't been set, of any value, including 'Nothing'.

            Dim result As Object = Table.Value(RowIndex, ColumnIndex)

            If RowIndex = CurrentRow?.Index AndAlso Not isEmpty(_NewValue(ColumnIndex)) Then

                result = _NewValue(ColumnIndex)

            End If

            result = NothingIfEmpty(result)

            Return result
        End Get

        Set(value As Object)

#If DEBUG Then ' Safety check
            ' Normally, the rowindex passed must be the same as the CurrentRow of the grid.
            ' However, when deleting several cells in different rows, we'll get multiple newvalues of 'Nothing'.
            ' This is OK.
            If Not isEmpty(value) Then
                Debug.Assert(RowIndex = CurrentRow.Index)
            End If
#End If
            value = NothingIfEmpty(value)

            ' The updating value get stored in NewValue
            _NewValue(ColumnIndex) = value

        End Set
    End Property



End Class
