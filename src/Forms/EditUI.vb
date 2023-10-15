Imports System.ComponentModel
Imports System.Text

Public Class EditUI

    Public Sub New(model As Model, context As Context)

        _Model = model
        _Context = context
        Me.Location = context.Location ' must be before InitializeComponent, fuckup in form sizing code https://stackoverflow.com/a/24050123/338101

        InitializeComponent()

        Me.Text = "Edit " & context.Column.ToString

        Value = context.Value

        If Value Is Nothing Then
            ValueText = ""
        Else
            'ValueText = Value.ToString ' CStr(context.Grid.Model.ToNet(context.Column.Datatype, Value))
            ValueText = model.Converter.NetToText(context.Column.Datatype, Value)
        End If

        SetVisibility(ValueText)

        btnFormat.Visible = context.Column.Datatype = SQLType.xml

        txtError.BackColor = SystemColors.Control ' which will force the forecolor to apply. Windows bug.

    End Sub
    Private Sub EditUI_Load(sender As Object, e As EventArgs) Handles Me.Load

        EditUI_ResizeEnd(sender, e) ' Make buttons re-position
        btnCancel.Focus()
        AddHandler HexGrid.GotFocus, AddressOf HexGrid_GotFocus
        AddHandler txtValue.GotFocus, AddressOf txtValue_GotFocus
        HexGrid.Focus()

    End Sub
    Private Sub btnFormat_Click(sender As Object, e As EventArgs) Handles btnFormat.Click

        txtValue.Text = PrettyXml(txtValue.Text)

    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        Try
            Value = Model.Converter.TextToNet(Context.Column.Datatype, ValueText)
            DialogResult = DialogResult.OK
            Close()

        Catch ex As Exception
            ShowBox(ex.Message, "Error saving value",, MessageBoxIcon.Error)
            DialogResult = DialogResult.None
        End Try

    End Sub
    Private Sub bntCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        DialogResult = DialogResult.Cancel
        Close()

    End Sub
    Private ReadOnly Property Context As Context
    Private Sub EditUI_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd

        btnUpdate.Left = 3
        btnFormat.Left = (ClientRectangle.Width - btnFormat.Width) \ 2
        btnCancel.Left = ClientSize.Width - btnCancel.Width - 3

        If HexEditing Then

            HexWidth =
                HexGrid.
                Columns.
                OfType(Of DataGridViewTextBoxColumn).
                Select(Function(q) q.Width + 4).
                Sum

            SplitHorizontal.SplitterDistance = HexWidth
        End If

    End Sub
    Private Property HexEditing As Boolean
    Private Sub HandleGrid(onoff As Boolean)

        RemoveHandler HexGrid.CellFormatting, AddressOf HexGrid_CellFormatting
        RemoveHandler HexGrid.CellEnter, AddressOf HexGrid_CellEnter
        RemoveHandler HexGrid.CellValidated, AddressOf HexGrid_CellValidated
        RemoveHandler HexGrid.CellValidating, AddressOf HexGrid_CellValidating
        RemoveHandler HexGrid.KeyDown, AddressOf HexGrid_KeyDown

        If onoff Then
            AddHandler HexGrid.CellFormatting, AddressOf HexGrid_CellFormatting
            AddHandler HexGrid.CellEnter, AddressOf HexGrid_CellEnter
            AddHandler HexGrid.CellValidated, AddressOf HexGrid_CellValidated
            AddHandler HexGrid.CellValidating, AddressOf HexGrid_CellValidating
            AddHandler HexGrid.KeyDown, AddressOf HexGrid_KeyDown
        End If

    End Sub
    Private Sub HandleText(onoff As Boolean)

        RemoveHandler txtValue.Validated, AddressOf txtValue_Validated
        RemoveHandler txtValue.Validating, AddressOf txtValue_Validating

        If onoff Then
            AddHandler txtValue.Validated, AddressOf txtValue_Validated
            AddHandler txtValue.Validating, AddressOf txtValue_Validating
        End If

    End Sub
    Private Sub HexGrid_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)

        If isEmpty(e.Value) Then
            Exit Sub
        End If

        If e.RowIndex >= 0 AndAlso e.ColumnIndex > 0 AndAlso Not isLastCell(e.RowIndex, e.ColumnIndex) Then

            e.Value = CStr(e.Value).ToUpper.PadRight(8, "0"c)
            e.FormattingApplied = True

        End If

    End Sub
    Private Sub HexGrid_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles HexGrid.CellEnter

        If e.ColumnIndex = 0 Then ' Moved onto the offset column. Move to the first data column

            BeginInvoke(Sub() HexGrid.CurrentCell = HexGrid.Rows(e.RowIndex).Cells(1))

        End If

    End Sub
    Private Sub HexGrid_CellValidated(sender As Object, e As DataGridViewCellEventArgs)

        If e.ColumnIndex = 0 Then
            Exit Sub
        End If

        With HexGrid.Rows(e.RowIndex).Cells(e.ColumnIndex)
            If .Value Is .Tag Then
                Exit Sub
            End If
        End With

        Dim cellvalue As Object = Nothing

        ' Loop over all the cells, align them middle and adjust the final cell alignment to the left

        For r As Integer = 0 To HexGrid.RowCount - 1

            For c As Integer = 1 To HexGrid.ColumnCount - 1

                If isLastCell(r, c) Then

                    cellvalue = HexGrid.Rows(r).Cells(c).Value

                    If cellvalue Is Nothing Then  ' A 'nothing' cell indicates that we're done
                        GoTo DONE
                    End If

                    If CStr(cellvalue).Length < 8 Then
                        HexGrid.Rows(r).Cells(c).Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                        cellvalue = Nothing
                    End If

                    Exit For

                Else
                    HexGrid.Rows(r).Cells(c).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If
            Next
        Next
DONE:
        txtError.Text = ""
        ValueText = HexGridToString()
        UpdateBoth(True)

    End Sub
    Private Sub HexGrid_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs)

        If e.ColumnIndex = 0 OrElse Not HexGrid.IsCurrentCellDirty Then
            Exit Sub
        End If

        Dim value As Object = e.FormattedValue

        If value Is Nothing Then

            If Not isLastCell(e.RowIndex, e.ColumnIndex) Then

                txtError.Text = "Only the last cell can be empty"
                e.Cancel = True

            End If
            Exit Sub
        End If

        Dim hex As String = CStr(value).ToUpper

        If Not isLastCell(e.RowIndex, e.ColumnIndex) Then

            hex = hex.PadRight(8, "0"c)

        End If

        If Not hex.All(Function(q) "0123456789ABCDEF".Contains(q)) Then

            txtError.Text = "Invalid hexadecimal"
            e.Cancel = True
            Exit Sub

        End If


        If hex.Length Mod 2 <> 0 Then

            txtError.Text = "Must have an even number of digits"
            e.Cancel = True
            Exit Sub

        End If

        If HexGrid.EditingControl IsNot Nothing Then
            HexGrid.EditingControl.Text = hex
        End If

    End Sub
    Private Sub HexGrid_GotFocus(sender As Object, e As EventArgs)

        HandleText(False)
        HandleGrid(True)

    End Sub
    Private Sub HexGrid_KeyDown(sender As Object, e As KeyEventArgs)

        If HexGrid.SelectedCells.Count <> 1 Then
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then

            HexGrid.SelectedCells(0).Value = Nothing

        End If
    End Sub
    Private Function HexGridToString() As String

        Dim result As New StringBuilder(HexGrid.RowCount * 32)

        For Each row As DataGridViewRow In HexGrid.Rows

            For col As Integer = 1 To 4

                If row.Cells(col).Value Is Nothing Then ' All cells converted
                    Return result.ToString
                End If

                result.Append(CStr(row.Cells(col).Value))

            Next
        Next

        Return result.ToString

    End Function
    Private Property HexWidth As Integer
    Private Function isLastCell(rowoffset As Integer, coloffset As Integer) As Boolean

        Dim r As Integer = rowoffset
        Dim c As Integer = coloffset

        Do
            c += 1
            If c > 4 Then
                r += 1
                c = 1
            End If

            If r >= HexGrid.RowCount Then
                Exit Do
            End If

            If HexGrid.Rows(r).Cells(c).Value IsNot Nothing Then
                Return False
            End If

        Loop

        Return True

    End Function
    Private Sub LoadGrid()

        HexGrid.Rows.Clear()
        Dim hex As String = CStr(ValueText)
        Dim row As Integer = -1
        Dim col As Integer = 5

        Do
            If col > 4 Then
                row += 1
                col = 1
                HexGrid.Rows.Add()
                HexGrid.Rows(row).Cells(0).Value = String.Format("{0:#,##0}", row * 16) & " " &
                                                   String.Format("{0:x8}", row * 16)
            End If

            Dim nibble As New String(hex.Take(8).ToArray)

            hex = New String(hex.Skip(8).ToArray)

            With HexGrid.Rows(row).Cells(col)
                .Value = nibble
                .Tag = nibble
            End With
            col += 1

        Loop Until hex = ""

    End Sub
    Private ReadOnly Property Model As Model
    Private Sub SetVisibility(value As Object)

        txtValue.Text = CStr(value)

        If {Fundamental.binary,
            Fundamental.special}.
            Contains(FundamentalTypeOf(Context.Column.Datatype)) Then

            LoadGrid()

            HexGrid.CurrentCell = HexGrid.Rows(0).Cells(1)

            HexEditing = True

        Else
            HexEditing = False
            SplitHorizontal.Panel1Collapsed = True
        End If

    End Sub
    Private Sub txtValue_GotFocus(sender As Object, e As EventArgs) Handles txtValue.GotFocus

        HandleGrid(False)
        HandleText(True)

    End Sub
    Private Sub txtValue_Validating(sender As Object, e As CancelEventArgs)

        If Not HexEditing Then
            Exit Sub
        End If

        Dim hex As String = txtValue.Text.ToUpper

        If Not hex.All(Function(q) "0123456789ABCDEF".Contains(q)) Then

            txtError.Text = "Invalid hexadecimal"
            e.Cancel = True
            Exit Sub

        End If

        If hex.Length Mod 2 <> 0 Then

            txtError.Text = "Must have an even number of digits"
            e.Cancel = True
            Exit Sub

        End If

        ValueText = hex

    End Sub
    Private Sub txtValue_Validated(sender As Object, e As EventArgs)

        txtError.Text = ""
        ValueText = txtValue.Text
        If HexEditing Then
            UpdateBoth(False)
        End If

    End Sub
    Private Sub UpdateBoth(grid As Boolean)

        Dim row As Integer = HexGrid.CurrentCell.RowIndex
        Dim col As Integer = HexGrid.CurrentCell.ColumnIndex + 1
        If col >= HexGrid.ColumnCount Then
            col = 1
            row += 1
        End If

        ' We get called during CellValidating, so we can't reload the grid etc.
        ' Thus the Invoke jiggery-pokery
        BeginInvoke(
            Sub()
                LoadGrid()
                txtValue.Text = ValueText
                BeginInvoke(
                    Sub()
                        HandleText(Not grid)
                        HandleGrid(grid)
                        HexGrid.CurrentCell = HexGrid.Rows(row).Cells(col)
                    End Sub)

            End Sub)
    End Sub
    Public Property Value As Object
    Public Property ValueText As String = ""
End Class
