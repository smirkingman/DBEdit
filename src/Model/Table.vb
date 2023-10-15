Public Class Table

    Public Event Refreshed(sender As Table, e As EventArgs)

    Private ReadOnly _ColumnsNamed As Dictionary(Of String, Column)
    Private ReadOnly _TableName As String
    Private _Ascending As Boolean = True

    Sub New(model As Model, schema As String, name As String)

        _Model = model
        _Schema = schema
        _TableName = name
        _Columns = New List(Of Column)
        _PrimaryKeys = New List(Of Column)
        _ColumnsNamed = New Dictionary(Of String, Column)(StringComparer.CurrentCultureIgnoreCase)

    End Sub

    Friend Sub AddColumn(name As String,
                  nullable As Boolean,
                  sqltype As SQLType,
                  identity As Boolean,
                  length As Integer)

        Dim added As New Column(Me, name, _Columns.Count, nullable, sqltype, identity, length)

        _Columns.Add(added)
        _ColumnsNamed.Add(name, added)

    End Sub
    Sub AddPrimary(column As Column, ordinal As Integer)
        column.PrimaryOrdinal = ordinal
        _PrimaryKeys.Add(column)
    End Sub
    Public Property Ascending As Boolean
        Get
            Return _Ascending
        End Get
        Set(value As Boolean)
            _Ascending = value
        End Set
    End Property
    Public ReadOnly Property Cache As Cache
    Public Function Column(name As String) As Column

        Return _ColumnsNamed(name)

    End Function
    Public ReadOnly Property Columns As List(Of Column)
    Private Function DefaultSort() As Column

        If _PrimaryKeys.Count = 0 Then
            Return Columns.First
        Else
            Return _PrimaryKeys.First
        End If

    End Function
    Friend Sub Finalise()

        _Selector = Columns.Where(Function(q) FundamentalTypeOf(q.Datatype) = Fundamental.string).FirstOrDefault

        If _Selector Is Nothing Then
            _Selector = Columns.First
        End If

        _Sort = DefaultSort()

        ' Renumber key ordinals, which are column indexes, to start from 1
        Dim k As Integer = 0
        For Each pcol As Column In PrimaryKeys.OrderBy(Function(q) q.PrimaryOrdinal).ToList
            k += 1
            pcol.PrimaryOrdinal = k
        Next

        k = 0
        For Each pcol As Column In PrimaryKeys.OrderBy(Function(q) q.ForeignOrdinal).ToList
            k += 1
            pcol.ForeignOrdinal = k
        Next

    End Sub
    Public Sub Load()

        _Cache = New Cache(Model, Me)

        AddColumn(Cache.ROWNUMBER, False, SQLType.int, False, 0)

    End Sub
    Friend ReadOnly Property Model As Model
    Public ReadOnly Property Name As String
        Get
            If Model.SchemaNames Then
                Return $"{Schema}.{_TableName}"
            Else
                Return _TableName
            End If
        End Get
    End Property
    Public Property OldSort As Column
    Public ReadOnly Property PrimaryKeys As List(Of Column)
    Public Sub Refresh()

        Cache.Refresh()

        OldSort = Nothing

    End Sub
    Public ReadOnly Property RowCount As Integer
        Get
            Return Cache.RowCount
        End Get
    End Property
    Public ReadOnly Property Schema As String
    Public ReadOnly Property Selector As Column
    Friend ReadOnly Property Sort As Column
    Public Sub SortOn(column As Column, direction As String)

        Dim newdirection As String = Left(direction, 1).ToLower
        OldSort = Sort
        Dim sortcolumn As Column = column


        Select Case newdirection

            Case "" ' Switch sort direction on current column

                If column IsNot OldSort Then
                    Ascending = True
                Else
                    Ascending = Not Ascending
                End If

            Case "a", "d"

                Ascending = newdirection = "a"

            Case "u"
                sortcolumn = DefaultSort()
                Ascending = True

        End Select

        _Sort = sortcolumn

    End Sub
    Public ReadOnly Property SortOrder As String
        Get
            Return If(Ascending, "ASC", "DESC")
        End Get
    End Property
    Public Overrides Function ToString() As String

        Return Name

    End Function
    Public Function Value(rowoffset As Integer, coloffset As Integer) As Object

        Return Cache.Value(rowoffset, coloffset)

    End Function
End Class
