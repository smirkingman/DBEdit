Public Module ShowBox_
    ''' <summary>
    ''' Show a message box centered on the main form
    ''' </summary>
    ''' <param name="prompt">Text of message to display</param>
    ''' <param name="title">Title text</param>
    ''' <param name="buttons">Buttons to display</param>
    Public Function ShowBox(prompt As String,
                               Optional title As String = "",
                               Optional buttons As MessageBoxButtons = MessageBoxButtons.OK,
                               Optional icon As MessageBoxIcon = MessageBoxIcon.Information) As DialogResult
        If title = "" Then
            title = "DBEdit"
        End If
        Dim mainform As Form = My.Application.ApplicationContext.MainForm
        Dim result As DialogResult = MessageBox.Show(mainform, prompt, title, buttons, icon)

        Return result

    End Function

End Module