Public Module TableToClipboard_

    Public Sub TableToClipboard(table As DataTable, title As String)

        Dim html As String = TableToHTML(table, title)

        Dim da As New DataObject

        da.SetData(DataFormats.Html, ConvertHtmlToClipboardData(html))

        da.SetData(DataFormats.Text, TableToTabbed(table))

        Clipboard.SetDataObject(da, True)

    End Sub

    Const HEADER_ As String = "Version:0.9" & vbCrLf & "StartHTML:{0:0000000000}" & vbCrLf & "EndHTML:{1:0000000000}" & vbCrLf & "StartFragment:{2:0000000000}" & vbCrLf & "EndFragment:{3:0000000000}" & vbCrLf
    Const HTML_START_ As String = "<html>" & vbCrLf & "<body>" & vbCrLf & "<!--StartFragment-->"
    Const HTML_END_ As String = "<!--EndFragment-->" ' & vbCrLf & "</body>" & vbCrLf & "</html>"

    Private Function ConvertHtmlToClipboardData(html As String) As String

        Dim result As String = ""
        Try
            ' https://stackoverflow.com/a/68439416/338101

            Dim encoding As New System.Text.UTF8Encoding(encoderShouldEmitUTF8Identifier:=False)
            Dim data As Byte() = Array.Empty(Of Byte)()
            Dim header As Byte() = encoding.GetBytes(String.Format(HEADER_, 0, 1, 2, 3))
            data = data.Concat(header).ToArray()
            Dim startHtml As Integer = data.Length
            data = data.Concat(encoding.GetBytes(HTML_START_)).ToArray()
            Dim startFragment As Integer = data.Length
            data = data.Concat(encoding.GetBytes(html)).ToArray()
            Dim endFragment As Integer = data.Length
            data = data.Concat(encoding.GetBytes(HTML_END_)).ToArray()
            Dim endHtml As Integer = data.Length
            Dim newHeader As Byte() = encoding.GetBytes(String.Format(HEADER_, startHtml, endHtml, startFragment, endFragment))

            If newHeader.Length <> startHtml Then
                Throw New InvalidOperationException(NameOf(ConvertHtmlToClipboardData))
            End If

            Array.Copy(newHeader, data, length:=startHtml)
            result = encoding.GetString(data)

        Catch ex As Exception
            HandleError(ex)
        End Try

        Return result

    End Function

End Module
