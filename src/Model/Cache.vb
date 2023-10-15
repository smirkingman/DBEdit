
Imports System.Data.SqlClient

Imports DBEdit.Model

Public Class Cache
    ' https://stackoverflow.com/a/16373108/338101

    Public Shared PAGESIZE As Integer = 25
    Public Const ROWNUMBER As String = "HiddenRowNumber"

    Public Sub New(model As Model, table As Table)

        _Model = model
        _Table = table

        ' https://stackoverflow.com/a/4267952/338101
        ' When a bit column can be null, we have to coalesce if to False for the DataGridView

        ColumnNames = String.Join(",", table.Columns.Select(Function(q)
                                                                If q.Datatype = SQLType.bit Then
                                                                    Return $"ISNULL({Box(q.Name)}, 0) AS {Box(q.Name)}"
                                                                Else
                                                                    Return Box(q.Name)
                                                                End If
                                                            End Function))
        LastOffset = -1

    End Sub
    Public Function BuildDataTable() As DataTable

        Dim result As DataTable

        Using conn As SqlConnection = Model.Connection

            ' Get current rowcount of table

            Dim wherecolumns As IEnumerable(Of Column) = Table.Columns.Where(Function(q) Not isEmpty(q.Where))

            Dim getcount As String = $"SELECT COUNT(*) FROM {Box(Table.Name)}"
            Dim countwhere As String = ""

            If wherecolumns.Any Then

                countwhere = " WHERE " & String.Join(" AND ",
                                    wherecolumns.
                                    Select(Function(q) q.Where))
                getcount &= countwhere
            End If

            _RowCount = CInt(Model.ExecuteScalar(getcount))

            _FirstOffset = Clamp(_FirstOffset, 0, RowCount)

            _LastOffset = _FirstOffset + Math.Min(PAGESIZE - 1, RowCount - _FirstOffset - 1)

            ' Build where clause for main select

            Dim where As String = $"{ROWNUMBER} BETWEEN {_FirstOffset + 1} AND {_LastOffset + 1}"

            If wherecolumns.Any Then

                where &= " AND " & String.Join(" AND ",
                        wherecolumns.
                        Select(Function(q) q.Where))

            End If

            Dim sortorder As String = If(Table.Ascending, "ASC", "DESC")

            Dim over As String = $"ROW_NUMBER() OVER (ORDER BY {Box(Table.Sort.Name)} {sortorder}) AS {ROWNUMBER}"

            Dim query As String = $"SELECT {ColumnNames},{ROWNUMBER} AS {ROWNUMBER} FROM " &
                                  $"(SELECT {ColumnNames},{over} FROM {Box(Table.Name)} {countwhere}) AS derived WHERE {where}"

            conn.Open()
            Dim adapter As New SqlDataAdapter(query, conn)
            Dim dataset As New DataSet
            dataset.DataSetName = "table"
            adapter.Fill(dataset)

            result = dataset.Tables(0)
            result.PrimaryKey = New DataColumn() {result.Columns(ROWNUMBER)}

            conn.Close()

        End Using

        Logger.Trace($"Fetched {FirstOffset + 1}..{LastOffset + 1}={LastOffset - FirstOffset + 1} rows of {RowCount}")

        Return result

    End Function
    Public Function CompleteDataTable(firstrow As Integer, firstcol As Integer, lastrow As Integer, lastcol As Integer) As DataTable

        Dim result As DataTable

        Using conn As SqlConnection = Model.Connection

            ' Get current rowcount of table

            Dim wherecolumns As IEnumerable(Of Column) = Table.Columns.Where(Function(q) Not isEmpty(q.Where))

            Dim getcount As String = $"SELECT COUNT(*) FROM {Box(Table.Name)}"
            Dim countwhere As String = ""

            If wherecolumns.Any Then

                countwhere = " WHERE " & String.Join(" AND ",
                                    wherecolumns.
                                    Select(Function(q) q.Where))
                getcount &= countwhere
            End If

            ' Build where clause for main select

            Dim where As String = $"{ROWNUMBER} BETWEEN {firstrow} AND {lastrow}"

            If wherecolumns.Any Then

                where &= " AND " & String.Join(" AND ",
                         wherecolumns.
                         Select(Function(q) q.Where))

            End If

            Dim columnnames As String = String.Join(",",
                Table.
                Columns.
                Where(Function(q) Between(q.Index + 1, firstcol, lastcol)).
                Select(Function(q, i) Box(q.Name)))

            Dim sortorder As String = If(Table.Ascending, "ASC", "DESC")

            Dim over As String = $"ROW_NUMBER() OVER (ORDER BY {Box(Table.Sort.Name)} {sortorder}) AS {ROWNUMBER}"

            Dim query As String = $"SELECT {columnnames},{ROWNUMBER} AS {ROWNUMBER} FROM " &
                                  $"(SELECT {columnnames},{over} FROM {Box(Table.Name)} {countwhere}) AS derived WHERE {where}"

            conn.Open()
            Dim adapter As New SqlDataAdapter(query, conn)
            Dim dataset As New DataSet
            dataset.DataSetName = "table"
            adapter.Fill(dataset)

            result = dataset.Tables(0)
            result.PrimaryKey = New DataColumn() {result.Columns(ROWNUMBER)}

            conn.Close()

        End Using

        Logger.Trace($"Fetched {FirstOffset + 1}..{LastOffset + 1}={LastOffset - FirstOffset + 1} rows of {RowCount}")

        Return result

    End Function
    Public ReadOnly Property ColumnNames As String
    Public Property DataTable As DataTable
    Public Property FirstOffset As Integer
    Public Property LastOffset As Integer = -1
    Private ReadOnly Property Model As Model
    Public Sub MoveTo(startoffset As Integer)

        Try
            If Between(startoffset, FirstOffset, LastOffset) Then ' we're in the current window

                Exit Sub

            End If

            _FirstOffset = startoffset

            DataTable = BuildDataTable()

            LastOffset = FirstOffset + DataTable.Rows.Count - 1

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Public Sub Refresh()

        LastOffset = -1 ' Force a refresh from the database

        MoveTo(FirstOffset) ' at the current offset

    End Sub
    Public ReadOnly Property RowCount As Integer
    Private ReadOnly Property Table As Table
    Public Overrides Function ToString() As String
        Return $"{Table.Name} {FirstOffset}..{LastOffset}"
    End Function
    Public Function Value(rowoffset As Integer, coloffset As Integer) As Object

        Dim result As Object = Nothing
        'Try
        ' If the table is empty, or we're asking for the 'new' row, return nothing
        If rowoffset >= Table.RowCount Then
            Return Nothing
        End If

        MoveTo(rowoffset)

        Dim row As DataRow = DataTable.Rows.Find(rowoffset + 1)

        result = row?(coloffset)

        'Catch ex As Exception
        '    HandleError(ex)
        'End Try

        Return result

    End Function
End Class
