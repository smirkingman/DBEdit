Public Module GetDefault_

    Public Function GetDefault(ByVal type As Type) As Object

        ' https://stackoverflow.com/a/353073/338101

        If type.IsValueType Then
            Return Activator.CreateInstance(type)
        End If

        Return Nothing

    End Function

End Module
