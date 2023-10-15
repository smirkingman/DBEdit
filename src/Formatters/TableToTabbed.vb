Imports System.Text

Public Module TableToTabbed_

    Public Function TableToTabbed(table As DataTable) As String

        Dim line As New StringBuilder(table.Rows.Count * 100)

        For Each col As DataColumn In table.
                                      Columns.
                                      OfType(Of DataColumn).
                                      Where(Function(q) q.ColumnName <> Cache.ROWNUMBER)

            line.Append(col.ColumnName)

            If col.Ordinal < table.Columns.Count - 1 Then
                line.Append(vbTab)
            End If

        Next
        line.Append(vbCr)

        For Each row As DataRow In table.Rows

            For Each col As DataColumn In table.
                                          Columns.
                                          OfType(Of DataColumn).
                                          Where(Function(q) q.ColumnName <> Cache.ROWNUMBER)

                Dim value As Object = row(col)

                If Not isEmpty(value) Then

                    Dim os As Integer = SizeOf(value)

                    If os > 32767 Then
                        line.Append($"Value has length {os:#,##0}, too long!")
                    Else
                        line.Append(value.ToString.Replace(vbTab, " "))
                    End If
                End If

                If col.Ordinal < table.Columns.Count - 1 Then
                    line.Append(vbTab)
                End If

            Next
            line.Append(vbCr)

        Next

        Return line.ToString

    End Function

End Module
