Imports System.Text
Public Class Column
    Implements IEquatable(Of Column)

    Sub New(table As Table,
            name As String,
            index As Integer,
            nullable As Boolean,
            datatype As SQLType,
            identity As Boolean,
            length As Integer)

        _Table = table
        _Name = name
        _Index = index
        _Nullable = nullable
        _Datatype = datatype
        _isIdentity = identity
        _Length = length

    End Sub
    Friend Sub AddPrimary(primarycolumn As Column, primaryordinal As Integer, foreignordinal As Integer)

        _Primary = primarycolumn
        _PrimaryOrdinal = primaryordinal
        _ForeignOrdinal = foreignordinal

    End Sub
    Public Sub ClearFilter()

        _Filter = Nothing

    End Sub
    Public Property Context As Context
    Friend Sub Finalise(model As Model)

        ' Easier to understand in the affirmative
        Dim locked As Boolean =
            Table.PrimaryKeys.Count = 0 OrElse
            Table.PrimaryKeys.Contains(Me) OrElse
            isIdentity OrElse
            isReadOnly(Datatype) OrElse
            isComputed

        _Updateable = Not locked

    End Sub
    Public ReadOnly Property Datatype As SQLType
    Public Property Filter As Filter
    Friend Property ForeignOrdinal As Integer
    Public ReadOnly Property Index As Integer
    Public Property isComputed As Boolean
    Public ReadOnly Property isFiltered As Boolean
        Get
            Return Filter IsNot Nothing
        End Get
    End Property
    Public Property isIdentity As Boolean
    Public ReadOnly Property isInteger As Boolean
        Get
            Return FundamentalTypeOf(Datatype) = Fundamental.integer
        End Get
    End Property
    Public ReadOnly Property isNumeric As Boolean
        Get
            Return {Fundamental.decimal, Fundamental.double, Fundamental.integer}.
                   Contains(FundamentalTypeOf(Datatype))
        End Get
    End Property
    Public ReadOnly Property isLookup As Boolean
        Get
            ' If this column has a primary key (constraint), it's a lookup on that primary table.
            ' Said table will have a 'selector', hopefully some kind of string.
            '
            ' However, if this column is (part of) a primary key of its own table, then this table's
            ' primary key has a constraint on some other table's column.
            ' In this case, it obviously makes no sense to lookup.
            '
            ' This is legal in SQL Server, but terribly confusing.
            ' (Frankly it's ghastly, and if to boot it's a partial key, it's revolting)

            ' Additionally, there's no point in doing a lookup if the target selector isn't a string
            ' (it would just be confusing to lookup one integer on another)

            Return Lookup IsNot Nothing AndAlso
                   Not Table.PrimaryKeys.Contains(Me) AndAlso
                   FundamentalTypeOf(Lookup.Table.Selector.Datatype) = Fundamental.string

        End Get
    End Property
    Public Function isReadOnly(sqltype As SQLType) As Boolean

        Return FundamentalTypeOf(sqltype) = Fundamental.special

    End Function
    Public ReadOnly Property Length As Integer
    Public ReadOnly Property Lookup As Column
        Get
            ' A column can have a foreign constraint on a column which in turn also has a foreign constraint.
            ' Walk the chain until we find the final, true constraint
            Dim foreign As Column = Primary

            Do While foreign IsNot Nothing

                If foreign.Primary Is Nothing Then
                    Exit Do
                End If

                foreign = foreign.Primary
            Loop

            Return foreign

        End Get
    End Property
    Friend ReadOnly Property Model As Model
        Get
            Return Table.Model
        End Get
    End Property
    Public ReadOnly Property Name As String
    Public ReadOnly Property Nullable() As Boolean
    Public ReadOnly Property Primary As Column
    Friend Property PrimaryOrdinal As Integer
    Public ReadOnly Property Table As Table
    Public ReadOnly Property Tooltip As String
        Get
            Dim result As String = Name
            Try
                Dim sb As New StringBuilder(200)
                Dim nulls As String = If(Nullable, "Null", "Not Null")
                Dim lengths As String = If(Length > 0, $"({Length})", "")

                sb.Append($"Column {Index + 1} {Table.Name}.{Name} {Datatype}{lengths} {nulls}")

                If Not Updateable Then

                    sb.Append(vbCrLf)
                    sb.Append("Readonly: ")

                    If isIdentity Then
                        sb.Append(" Identity.")
                    End If

                    If Table.PrimaryKeys.Count = 0 Then
                        sb.Append(" Table has no primary keys.")

                    ElseIf Table.PrimaryKeys.Contains(Me) Then
                        sb.Append($" Primary key({PrimaryOrdinal}/{Table.PrimaryKeys.Count}).")
                    End If

                    If isReadOnly(Datatype) Then
                        sb.Append($" {Datatype} cannot be edited.")
                    End If

                    If isComputed Then
                        sb.Append($" Computed column.")
                    End If

                End If

                If Primary IsNot Nothing Then

                    sb.Append(vbCrLf)
                    sb.Append($"Foreign constraint")

                    Dim foreign As Column = Primary

                    Do While foreign IsNot Nothing

                        sb.Append(vbCrLf)
                        sb.Append($" on {foreign.Table.Name}.{foreign.Name}({foreign.ForeignOrdinal})")

                        foreign = foreign.Primary
                    Loop
                End If

                If Not isEmpty(Where) Then
                    sb.Append(vbCrLf)
                    sb.Append($"Filter {Where}")
                End If

                result = sb.ToString

            Catch ex As Exception
                result &= ex.Message

            End Try

            Return result

        End Get
    End Property
    Public ReadOnly Property Updateable As Boolean
    Public ReadOnly Property Where As String
        Get
            If _Filter Is Nothing Then
                Return ""
            End If
            Return Filter.Where
        End Get
    End Property
    Public Overrides Function ToString() As String

        Return Tooltip

    End Function
    Public Overloads Function Equals(other As Column) As Boolean Implements IEquatable(Of Column).Equals

        Return Table.Name = other.Table.Name And Name = other.Name

    End Function
    Public Overrides Function GetHashCode() As Int32
        Dim hash As Int32 = 179 ' or something intelligent

        hash = (hash * 27) + Table.Name.GetHashCode()
        hash = (hash * 27) + Name.GetHashCode()

        Return hash
    End Function
End Class
