Public Enum Severity
    Info
    Warning
    [Error]
End Enum
Public Class MessageEventArgs
    Inherits EventArgs

    Public Sub New(message As String, severity As Severity)

        _Message = message
        _Severity = severity

    End Sub
    Public ReadOnly Property Message As String
    Public ReadOnly Property Severity As Severity
End Class
