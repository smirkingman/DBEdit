Module SingleQuote_

    Function SingleQuote(s As String) As String

        Return "'" & s.Replace("'", "''") & "'"

    End Function

End Module
