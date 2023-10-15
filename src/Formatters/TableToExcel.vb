Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop.Excel

Public Module TableToExcel_

    Public Sub TableToExcel(table As System.Data.DataTable, title As String)

        TableToClipboard(table, title)

        Task.Run(Sub()
                     StartExcelAsync(table.Rows.Count, table.Columns.Count)
                 End Sub)
    End Sub
    Private Sub StartExcelAsync(rows As Integer, cols As Integer)

        ' NB: Excel will not terminate when debugging https://stackoverflow.com/a/25135685/338101
        ' It *will* in release mode

        Try
            System.Windows.Forms.Application.UseWaitCursor = True ' as opposed to Excel.Application

            Dim xcl As New Microsoft.Office.Interop.Excel.Application

            With xcl
                Dim wb As Workbook = .Workbooks.Add()
                Dim sheet As Worksheet = DirectCast(wb.Sheets(1), Worksheet)
                sheet.Paste()
                Dim selection As Range = sheet.UsedRange
                selection.ColumnWidth = 255
                selection.EntireRow.AutoFit()
                selection.EntireColumn.AutoFit()
                .Range("A1").Select()
                .WindowState = XlWindowState.xlMaximized
                .Visible = True
                .ActiveWindow.Activate() ' bring to front
                ' Let go of all the unmanaged objects
                selection = Nothing
                wb = Nothing
                sheet = Nothing
            End With
            xcl = Nothing

        Finally
            ' https://stackoverflow.com/a/38170605/338101
            Dim gcs As Integer = 0
            Do
                GC.Collect()
                GC.WaitForPendingFinalizers()
                gcs += 1
            Loop Until Not Marshal.AreComObjectsAvailableForCleanup
            System.Windows.Forms.Application.UseWaitCursor = False

        End Try
    End Sub

End Module
