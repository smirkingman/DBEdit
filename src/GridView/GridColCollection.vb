Public Class GridColCollection
    Inherits DataGridViewColumnCollection

    Sub New(grid as Grid)

        MyBase.New(grid)

    End Sub
    Public Overrides Function Add(dataGridViewColumn As DataGridViewColumn) As Integer
        Return MyBase.Add(dataGridViewColumn)
    End Function

End Class
