Imports System.Text

Public Module TableToHTML_

    Public Function TableToHTML(table As DataTable,
                                title As String,
                                Optional gridlines As Boolean = False) As String

        Dim line As New StringBuilder(table.Rows.Count * 100)
        Dim fontfamily As String = "Arial"

        line.AppendLine("<html>")
        line.AppendLine("<head>")
        line.AppendLine("<title>" & title & "</title>")

        If gridlines Then
            line.AppendLine("<style>")
            line.AppendLine("table {border: 1px solid #444444; border-collapse:collapse}")
            line.AppendLine("thead,tr,td {border: 1px solid #222222}")
            line.AppendLine("</style>")
        End If

        line.AppendLine("</head>")
        line.AppendLine("<body>")

        line.Append("<table style='color:#000000;vertical-align:middle;text-align:right;font-size:8.25pt;")
        line.Append("font-family:""" & fontfamily & """;'>")

        line.AppendLine(vbTab & "<thead style='display: table-header-group'><tr>")

        Dim align As New Dictionary(Of DataColumn, String)

        For Each col As DataColumn In table.
                                      Columns.
                                      OfType(Of DataColumn).
                                      Where(Function(q) q.ColumnName <> Cache.ROWNUMBER)

            line.Append("<th style='text-align:center'>" & col.ColumnName & "</th>")

            Select Case col.DataType

                Case GetType(Boolean)
                    align(col) = "vertical-align:middle;text-align:center;"
                Case GetType(Byte)
                    align(col) = "vertical-align:middle;text-align:center;"
                Case GetType(Byte())
                    align(col) = "vertical-align:middle;text-align:center;"
                Case GetType(Char)
                    align(col) = "vertical-align:middle;text-align:center;"
                Case GetType(Date), GetType(DateTime), GetType(DateTimeOffset), GetType(TimeSpan)
                    align(col) = "vertical-align:middle;text-align:center;"
                Case GetType(Decimal)
                    align(col) = "vertical-align:middle;text-align:right;"
                Case GetType(Double)
                    align(col) = "vertical-align:middle;text-align:right;"
                Case GetType(Guid)
                    align(col) = "vertical-align:middle;text-align:center;"
                Case GetType(Integer)
                    align(col) = "vertical-align:middle;text-align:right;"
                Case GetType(Long)
                    align(col) = "vertical-align:middle;text-align:right;"
                Case GetType(Object)
                    align(col) = "vertical-align:middle;text-align:center;"
                Case GetType(SByte)
                    align(col) = "vertical-align:middle;text-align:center;"
                Case GetType(Short)
                    align(col) = "vertical-align:middle;text-align:right;"
                Case GetType(Single)
                    align(col) = "vertical-align:middle;text-align:right;"
                Case GetType(String)
                    align(col) = "vertical-align:middle;text-align:left;"
                Case GetType(UInteger)
                    align(col) = "vertical-align:middle;text-align:right;"
                Case GetType(ULong)
                    align(col) = "vertical-align:middle;text-align:right;"
                Case GetType(UShort)
                    align(col) = "vertical-align:middle;text-align:right;"
                Case Else
                    align(col) = "vertical-align:middle;text-align:left;"

            End Select
        Next
        line.AppendLine(vbTab & "</tr></thead>")

        line.AppendLine(vbTab & "<tbody style='display: table-row-group'>")

        For Each row As DataRow In table.Rows

            line.AppendLine(vbTab & "<tr>")

            For Each col As DataColumn In table.
                                          Columns.
                                          OfType(Of DataColumn).
                                          Where(Function(q) q.ColumnName <> Cache.ROWNUMBER)

                Dim value As Object = row(col)

                line.Append("<td style='" & align(col) & "'>")

                If Not isEmpty(value) Then

                    Dim os As Integer = SizeOf(value)
                    If os > 32767 Then

                        line.Append($"Value has length {os:#,##0}, too long for Excel!")

                    ElseIf col.DataType = GetType(Byte()) Then

                        line.Append("0x" & ByteArrayToString(DirectCast(value, Byte())))
                    Else

                        line.Append(value.ToString.TrimEnd)

                    End If
                End If

                line.AppendLine("</td>")

            Next

            line.AppendLine(vbTab & "</tr>")

        Next

        line.AppendLine(vbTab & "</tbody>")
        line.AppendLine("</table>")
        line.AppendLine("</body>")
        line.AppendLine("</html>")

        Dim result As String = line.ToString

        Return result

    End Function

End Module
