Public Module Compact_

    Public Function Compact(s As String) As String

        Dim result As String = s.Replace(vbCr, " ").Replace(vbLf, " ").Replace(vbTab, " ")

        Do While result.IndexOf("  ") >= 0
            result = result.Replace("  ", " ")
        Loop

        Return result.Trim

    End Function

End Module
