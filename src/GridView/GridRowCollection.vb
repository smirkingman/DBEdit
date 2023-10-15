Public Class GridRowCollection
    Inherits DataGridViewRowCollection

    Sub New(grid as Grid)

        MyBase.New(grid)

    End Sub
    Public Overrides Function Add() As Integer

        Return MyBase.Add(New GridRow)

    End Function
End Class
