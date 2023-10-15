Imports System.Reflection
Public Module Caller_
    ''' <summary>
    ''' Get the name of method calling us (outside of the runtime)
    ''' </summary>
    ''' <returns></returns>
    Public Function Caller(skip As Integer) As String

        Dim st As New StackTrace
        Dim sf As StackFrame = st.GetFrames.Skip(2 + skip).FirstOrDefault
        Dim meth As MethodBase = sf.GetMethod
        Dim name As String = meth.DeclaringType.Name & "." & meth.Name
        Return name
        'For Each sf As StackFrame In st.GetFramess.Skip(1)

        '    Dim meth As MethodBase = sf.GetMethod

        '    If meth.DeclaringType.Namespace.StartsWith("System") Then
        '        Return prev.GetMethod.DeclaringType.FriendlyName
        '    ElseIf meth.DeclaringType.Namespace <> "Runtime" Then
        '        Return meth.DeclaringType.FriendlyName
        '    End If
        '    prev = sf
        'Next
        'Return "[Unknown]"

    End Function

End Module
