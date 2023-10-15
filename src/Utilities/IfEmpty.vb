Public Module IfEmpty_

    Public Function IfEmpty(expression As Object, whenempty As String) As String

        If isEmpty(expression) Then
            Return whenempty
        Else
            Return expression.ToString
        End If

    End Function

End Module
