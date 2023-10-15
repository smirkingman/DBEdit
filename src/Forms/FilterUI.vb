
Public Class FilterUI

    Sub New(filter As Filter)

        InitializeComponent()

        _Filter = filter

    End Sub
    Private ReadOnly Property Context As Context
        Get
            Return Filter.Context
        End Get
    End Property

    Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)

        Dim g As Graphics = e.Graphics
        Dim topLeftColor As Color = Color.FromArgb(&HFF909090)
        Dim bottomRightColor As Color = Color.FromArgb(&HFF303030)
        Dim frameSize As Integer = 2

        g.Clear(Me.BackColor)

        ' Draw top frame
        Using topBrush As New SolidBrush(topLeftColor)
            g.FillRectangle(topBrush, 0, 0, Me.Width, frameSize)
        End Using

        ' Draw left frame
        Using leftBrush As New SolidBrush(topLeftColor)
            g.FillRectangle(leftBrush, 0, 0, frameSize, Me.Height)
        End Using

        ' Draw bottom frame
        Using bottomBrush As New SolidBrush(bottomRightColor)
            g.FillRectangle(bottomBrush, 0, Me.Height - frameSize, Me.Width, frameSize)
        End Using

        ' Draw right frame
        Using rightBrush As New SolidBrush(bottomRightColor)
            g.FillRectangle(rightBrush, Me.Width - frameSize, 0, frameSize, Me.Height)
        End Using

    End Sub
    Private Sub FilterUI_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            txtLabel.Text = $"Filter on { Context.Table.Name}.{ Context.Column.Name} {Context.Verb} :"

            ' Size the form and controls, the text can be long
            Dim sizef As New SizeF()
            Using g As Graphics = txtLabel.CreateGraphics
                sizef = g.MeasureString(txtLabel.Text, txtLabel.Font)
            End Using
            Dim needed As Integer = CInt(sizef.Width + 10)

            txtLabel.Width = needed
            txtValue.Width = txtLabel.Width
            btnCancel.Left = txtValue.Left + txtValue.Width - btnCancel.Width

            rtbHelp.LoadFile("Resources\filter" & Context.Verb.Replace(" ", "") & ".rtf")
            rtbHelp.SelectAll()
            rtbHelp.SelectionFont = rtbHelp.Font
            rtbHelp.Width = btnCancel.Left + btnCancel.Width - btnFilter.Left

            Me.ClientSize = New Size(txtValue.Width + (2 * txtValue.Left), Me.ClientSize.Height)

            Dim value As Object = Context.Value

            If Context.Column.Lookup IsNot Nothing Then
                value = Context.Grid.Rows(Context.RowIndex).Cells(Context.ColumnIndex).FormattedValue
            End If

            txtValue.Text = IfEmpty(value, "")

            txtValue_KeyUp(txtValue, New KeyEventArgs(Keys.Delete))

            Me.Show() ' So that we can set focii https://stackoverflow.com/a/6288028/338101

            txtValue.Focus()

            btnFilter.Enabled = Not String.IsNullOrWhiteSpace(txtValue.Text)

            Refresh()

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnFilter.Click

        Try
            Context.Column.Filter.Apply(Context.Verb, txtValue.Text)
            DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        DialogResult = DialogResult.Cancel
        Me.Close()

    End Sub
    Public ReadOnly Property Filter As Filter
    Private Sub txt_GotFocus(sender As Object, e As EventArgs) Handles txtLabel.GotFocus

        Try
            DirectCast(sender, TextBox).SelectAll()

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub txtValue_KeyUp(sender As Object, e As KeyEventArgs) Handles txtValue.KeyUp

        Try
            If My.Settings.Tooltips Then

                If isEmpty(txtValue.Text) Then

                    btnFilter.Enabled = False
                    ToolTip1.SetToolTip(txtValue, "")

                Else
                    btnFilter.Enabled = True
                    ToolTip1.SetToolTip(txtValue, Context.Column.Filter.Where)
                End If

            End If

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
End Class
