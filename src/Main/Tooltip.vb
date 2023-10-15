Imports DBEdit.Model

Imports MoreLinq
Partial Public Class MainUI

    Private Sub ToolTip1_Draw(sender As Object, e As DrawToolTipEventArgs) Handles ToolTip1.Draw

        e.DrawBackground()
        e.DrawBorder()
        ' Convert backslashes to carriage-return+linefeed
        If e.ToolTipText.Contains("\") Then
            ToolTip1.SetToolTip(e.AssociatedControl, e.ToolTipText.Replace("\", vbCrLf))
        End If
        e.DrawText()

    End Sub
End Class
