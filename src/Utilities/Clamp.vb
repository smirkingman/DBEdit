Public Module Clamp_

    Public Function Clamp(value As Integer, min As Integer, max As Integer) As Integer

        If value < min Then
            Return min
        ElseIf value > max Then
            Return max
        Else
            Return value
        End If
    End Function

End Module
