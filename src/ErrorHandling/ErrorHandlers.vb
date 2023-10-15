''' <summary>
''' Error handling for all errors
''' </summary>
Public Module ErrorHandlers

    ''' <summary>
    ''' Gathers information about an error (inner exception, stack trace) and display a custom
    ''' error form, where the user can choose what to do.
    ''' </summary>
    Public Sub HandleError(e As Exception)

        Dim errorui As New ErrorUI
        Dim details As String = ""

        With errorui

            details = e.Message & " (" & e.GetType.Name & ")"

            Dim ie As Exception = e.InnerException

            Do While ie IsNot Nothing
                details = details & vbCr & "...caused by " & ie.Message
                ie = ie.InnerException
            Loop

            details = details & vbCrLf & e.StackTrace

            .txtDetails.Text = details & vbCrLf & " at " & DateTime.Now.ToLongDateString & " " & DateTime.Now.ToLongTimeString

            Dim result As DialogResult = errorui.ShowDialog

        End With

    End Sub

End Module
