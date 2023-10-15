Public Class GridButton
    Inherits ToolStripButton

    Public Event ItemClick(sender As GridButton, e As EventArgs)

    Public Sub New(name As String)

        MyBase.New(name)

    End Sub
    Public Property Context As Context
    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
        RaiseEvent ItemClick(Me, New EventArgs)
    End Sub

End Class
