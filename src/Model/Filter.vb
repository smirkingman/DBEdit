Imports DBEdit.Model

Public Class Filter

    Public Sub New(context As Context)

        _Context = context

    End Sub
    Public Sub Apply(comparator As String, compareto As String)

        _Where = WhereClause(comparator, compareto)

    End Sub
    Public Sub Clear()

        _Where = ""

    End Sub
    Public ReadOnly Property Context As Context
    Public Property Value As String

    Private Shared ReadOnly _Operator As New Dictionary(Of String, String)(StringComparer.CurrentCultureIgnoreCase) From {
        {"equals", "="},
        {"less than", "<"},
        {"greater than", ">"},
        {"not equals", "<>"}
    }
    Public ReadOnly Property Where As String
    Private Function WhereClause(comparator As String, compare As String) As String

        Dim column As Column = Context.Column

        If comparator = "null" OrElse comparator = "not null" Then

            Return $"{Box(column.Name)} IS {comparator}"

        End If

        Dim query As String = ""
        Dim model As Model = Context.Grid.Model
        Dim comparefield As Column = column
        Dim lookuptable As Table = Nothing
        Dim lookupkey As Column = Nothing

        If column.isLookup Then

            lookupkey = column.Lookup
            lookuptable = lookupkey.Table
            comparefield = lookuptable.Selector

        End If

        Select Case comparator

            Case = "equals", "less than", "greater than", "not equals"

                Dim op As String = _Operator(comparator)
                query = $"{Box(comparefield.Name)}{op}{model.ToSQL(compare, comparefield.Datatype)}"

            Case "starts with"

                compare &= "%"
                query = $"{Box(comparefield.Name)} LIKE {model.ToSQL(compare, SQLType.nvarchar)}" ' only for numbers!

            Case "contains"

                compare = "%" & compare & "%"
                query = $"{Box(comparefield.Name)} LIKE {model.ToSQL(compare, SQLType.nvarchar)}"

            Case "like"

                query = $"{Box(comparefield.Name)} LIKE {model.ToSQL(compare, SQLType.nvarchar)}"

            Case "in"

                For Each sep As Char In {";"c, ","c, " "c}

                    If compare.Contains(sep) Then

                        Dim values() As String = compare.Split(sep)

                        query = $"{Box(comparefield.Name)} IN (" &
                                String.Join(",", values.Select(Function(q) model.ToSQL(q, comparefield.Datatype))) &
                            ")"
                        Exit For
                    End If
                Next sep

            Case Else
                Throw New Exception($"Invalid comparator {comparator}")

        End Select

        If column.isLookup Then

            query = $"{column.Name} IN " &
                     $"(SELECT {Box(lookupkey.Name)} " &
                     $"FROM {Box(lookuptable.Name)} WHERE {query})"
        End If

        Return query

    End Function

    Public Overrides Function ToString() As String

        Return $"{Where}"

    End Function

End Class
