Public Module isEmpty_

    Public Function isEmpty(v As Object) As Boolean

        Return v Is Nothing OrElse IsDBNull(v) OrElse v Is NoValue OrElse String.IsNullOrWhiteSpace(v.ToString)

    End Function

End Module
