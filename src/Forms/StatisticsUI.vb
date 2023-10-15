Imports DBEdit.Model

Public Class StatisticsUI

    Public Sub New(context As Context)

        _Context = context
        Me.Location = context.Location ' must be before InitializeComponent, fuckup in form sizing code https://stackoverflow.com/a/24050123/338101

        InitializeComponent()

        txtError.Text = ""

        Me.Text = context.Column.ToString

    End Sub
    Private ReadOnly Property Context As Context
    Private Sub StatisticsUI_Load(sender As Object, e As EventArgs) Handles Me.Load

        For Each tb As TextBox In Me.Controls.OfType(Of TextBox)
            tb.Text = ""
        Next

        Invalidate()

        ' Populate values on a worker thread in case SQL Server is slow
        Task.Run(Sub() Work())

    End Sub
    Private Sub Work()

        Dim table As Table = Context.Table
        Dim col As Column = Context.Column
        Dim colname As String = Box(col.Name)
        Dim tablename As String = Box(table.Name)

        Populate(txtCount, Formatted($"SELECT COUNT({colname}) FROM {tablename}", 0))

        Dim pkcolumns As String = colname
        If table.PrimaryKeys.Count > 1 Then
            ' e.g. : select count(distinct concat(businessentityid,departmentid,shiftid,startdate)) from HumanResources.EmployeeDepartmentHistory
            pkcolumns = "CONCAT(" & String.Join(",", table.PrimaryKeys.Select(Function(q) Box(q.Name))) & ")"
        End If

        Populate(txtCountDistinct, Formatted($"SELECT COUNT(DISTINCT {pkcolumns}) FROM {tablename}", 0))

        Populate(txtNulls, Formatted($"SELECT COUNT(*) FROM {tablename} WHERE {colname} IS NULL", 0))

        Populate(txtMinimum, Formatted($"SELECT MIN({colname}) FROM {tablename} WHERE {colname} IS NOT NULL", 0))

        Populate(txtMaximum, Formatted($"SELECT MAX({colname}) FROM {tablename} WHERE {colname} IS NOT NULL", 0))

        If col.isNumeric Then

            Populate(txtAverage, Formatted($"SELECT AVG(CAST({colname} AS FLOAT(53))) FROM {tablename} WHERE {colname} IS NOT NULL", 3))

            Populate(txtSum, Formatted($"SELECT SUM(CAST({colname} AS FLOAT(53))) FROM {tablename} WHERE {colname} IS NOT NULL",
                                       If(col.isInteger, 0, 3)))

            Populate(txtStdDev, Formatted($"SELECT STDEV(CAST({colname} AS FLOAT(53))) FROM {tablename} WHERE {colname} IS NOT NULL", 3))

            Populate(txtVariance, Formatted($"SELECT VAR(CAST({colname} AS FLOAT(53))) FROM {tablename} WHERE {colname} IS NOT NULL", 3))
        Else
            Populate(txtAverage, "N/A")
            Populate(txtSum, "N/A")
            Populate(txtStdDev, "N/A")
            Populate(txtVariance, "N/A")
        End If

    End Sub
    Private Sub Populate(control As TextBox, value As String)

        BeginInvoke(Sub()
                        control.Text = value
                        control.Invalidate()
                    End Sub)

    End Sub
    Private Function Formatted(query As String, decimals As Integer) As String

        Dim model As Model = Context.Grid.Model
        Try
            If Context.Column.isNumeric Then

                Dim result As Double = CDbl(ExecuteScalar(model.Server, model.Database, query))

                If decimals = 0 Then
                    Return CLng(result).ToString("#,##0")
                Else
                    Return result.ToString("#,##0." & StrDup(decimals, "0"))
                End If
            Else
                Dim result As String = ExecuteScalar(model.Server, model.Database, query).ToString
                Return result
            End If

        Catch ex As Exception
            Invoke(Sub()
                       If txtError.Text = "" Then
                           txtError.Text = ex.Message
                       End If
                   End Sub)
        End Try

        Return "error"

    End Function

End Class
