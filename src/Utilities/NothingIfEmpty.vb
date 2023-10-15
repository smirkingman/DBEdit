Public Module NothingIfEmpty_

    Public Function NothingIfEmpty(o As Object) As Object

        If isEmpty(o) Then
            Return Nothing
        Else
            Return o
        End If

    End Function

End Module
