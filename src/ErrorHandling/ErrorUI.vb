''' <summary>
''' Custom form to display the details of an error
''' </summary>
''' <remarks></remarks>
Public Class ErrorUI

    Private Sub btnIgnore_Click(sender As System.Object, e As System.EventArgs) Handles btnContinue.Click

        Me.DialogResult = Windows.Forms.DialogResult.Ignore
        Me.Close()

    End Sub
    Private Sub btnClipboard_Click(sender As System.Object, e As System.EventArgs) Handles btnClipboard.Click

        Dim da As New DataObject
        da.SetData(DataFormats.UnicodeText, txtDetails.Text)
        Clipboard.SetDataObject(da, True)

    End Sub
    Private Sub ErrorUI_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Me.BringToFront()
        Aargh()

    End Sub

End Class