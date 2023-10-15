Public Module Box_
    Public Function Box(s As String) As String
        Dim parts() As String = s.Split("."c)
        Return String.Join(".", parts.Select(Function(q) "[" & q & "]"))
    End Function
End Module
