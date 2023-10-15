Imports System.Data.SqlClient

Imports NLog

''' <summary>
''' Describes the tables, columns, keys and relations of a database
''' </summary>
''' <remarks></remarks>
Public Class Model

    Public Event Progress(sender As Object, e As String)

    Public ReadOnly Property Converter As Converter

    Private ReadOnly Property RowSet As New Dictionary(Of String, Table)(StringComparer.CurrentCultureIgnoreCase)
    Private ReadOnly Property SqlTypes As New Dictionary(Of String, SQLType)(StringComparer.InvariantCultureIgnoreCase)
    Private Property Stopwatch As Stopwatch

    Public Sub New(server As Server, database As Database)

        _Server = server
        _Database = database

    End Sub
    Public Function Connection() As SqlConnection

        Return SQL.Connection(Server, Database)

    End Function
    Private Sub CreateSQLTypes()

        _SqlTypes.Add("bigint", SQLType.bigint)
        _SqlTypes.Add("binary", SQLType.binary)
        _SqlTypes.Add("bit", SQLType.bit)
        _SqlTypes.Add("char", SQLType.[char])
        _SqlTypes.Add("cursor", SQLType.cursor)
        _SqlTypes.Add("date", SQLType.[date])
        _SqlTypes.Add("datetime", SQLType.datetime)
        _SqlTypes.Add("datetime2", SQLType.datetime2)
        _SqlTypes.Add("datetimeoffset", SQLType.datetimeoffset)
        _SqlTypes.Add("decimal", SQLType.[decimal])
        _SqlTypes.Add("float", SQLType.float)
        _SqlTypes.Add("geography", SQLType.geography)
        _SqlTypes.Add("geometry", SQLType.geometry)
        _SqlTypes.Add("hierarchyid", SQLType.hierarchyid)
        _SqlTypes.Add("image", SQLType.image)
        _SqlTypes.Add("int", SQLType.int)
        _SqlTypes.Add("money", SQLType.money)
        _SqlTypes.Add("nchar", SQLType.nchar)
        _SqlTypes.Add("ntext", SQLType.ntext)
        _SqlTypes.Add("numeric", SQLType.numeric)
        _SqlTypes.Add("nvarchar", SQLType.nvarchar)
        _SqlTypes.Add("real", SQLType.real)
        ' In theory https://dba.stackexchange.com/a/86531/76518
        ' _SqlTypes.Add("rowversion", SQLType.rowversion)
        _SqlTypes.Add("smalldatetime", SQLType.smalldatetime)
        _SqlTypes.Add("smallint", SQLType.smallint)
        _SqlTypes.Add("smallmoney", SQLType.smallmoney)
        _SqlTypes.Add("sql_variant", SQLType.sql_variant)
        _SqlTypes.Add("table", SQLType.table)
        _SqlTypes.Add("text", SQLType.text)
        _SqlTypes.Add("time", SQLType.time)
        _SqlTypes.Add("timestamp", SQLType.timestamp)
        _SqlTypes.Add("tinyint", SQLType.tinyint)
        _SqlTypes.Add("uniqueidentifier", SQLType.uniqueidentifier)
        _SqlTypes.Add("varbinary", SQLType.varbinary)
        _SqlTypes.Add("varchar", SQLType.varchar)
        _SqlTypes.Add("xml", SQLType.xml)

    End Sub
    Public ReadOnly Property Database As Database
    Public Function ExecuteNonQuery(query As String) As Integer

        Return SQL.ExecuteNonQuery(Server, Database, query)

    End Function
    Public Function ExecuteScalar(query As String) As Object

        Return SQL.ExecuteScalar(Server, Database, query)

    End Function
    Private Sub FinaliseModel()

        For Each table As Table In RowSet.Values

            Status($"Finalising {table.Name}")

            For Each column As Column In table.Columns
                column.Finalise(Me)
            Next

            table.Finalise()
        Next

    End Sub
    Public Function FindTable(schemaname As String, tablename As String) As Table

        Dim fullname As String = tablename
        If SchemaNames Then
            fullname = $"{schemaname}.{tablename}"
        End If
        Return Tables.Where(Function(q) q.Name = fullname).Single

    End Function
    Sub Load()

        Stopwatch = New Stopwatch
        Stopwatch.Start()
        Status("Loading model...")

        _Database = Database

        CreateSQLTypes()

        _Converter = New Converter()

        Using conn As SqlConnection = Connection()

            conn.Open()

            PopulateTables(conn)

            PopulateColumns(conn)

            PopulatePrimaryKeys(conn)

            PopulateForeignKeys(conn)

            PopulateComputedColumns(conn)

            PopulateIdentityColumns(conn)

            FinaliseModel()

            conn.Close()

        End Using


        Stopwatch.Stop()
        Stopwatch.Reset()

        Status("Model loaded")

    End Sub
    Public Logger As Logger = LogManager.GetCurrentClassLogger
    Private Sub PopulateColumns(conn As SqlConnection)

        Try
            Using rdr As New SqlReader(SQLColumns, conn)

                With rdr

                    Do While .Read

                        Dim tablename As String = .Item("TableName")
                        If _SchemaNames Then
                            tablename = .Item("TableSchema") & "." & tablename
                        End If

                        Dim columnname As String = .Item("ColumnName")
                        Dim nullable As Boolean = False
                        If .Item("IS_NULLABLE") = "YES" Then
                            nullable = True
                        End If

                        Dim length As Integer = 0
                        If .Item("Length") IsNot Nothing Then
                            length = CInt(.Item("Length"))
                        End If

                        Dim datatype As String = .Item("DataType")

                        Dim identity As Boolean

                        Try ' SQL Server only
                            Dim cmd As New SqlCommand("SELECT is_identity FROM sys.columns WHERE name=" & SingleQuote(columnname) &
                                                     " AND object_id = OBJECT_ID('" & tablename & "')", conn)

                            identity = CBool(cmd.ExecuteScalar)

                        Catch NotSQLServer As Exception
                        End Try

                        Dim sqltype As SQLType = SqlTypeOf(conn, datatype)

                        RowSet(tablename).AddColumn(columnname, nullable, sqltype, identity, length)

                        Status("Column " & .Item("TableName") & "." & .Item("ColumnName"))
                    Loop

                End With
            End Using

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub PopulateComputedColumns(conn As SqlConnection)

        Try

            Using rdr As New SqlReader(SQLComputedColumns, conn)

                With rdr

                    Do While .Read

                        Dim table As Table = FindTable(.Item("SchemaName"), .Item("TableName"))
                        Dim column As Column = table.Column(.Item("ColumnName"))

                        column.isComputed = True
                        Status($"{table.Name}.{column.Name} is computed")
                    Loop

                End With
            End Using

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub PopulateForeignKeys(conn As SqlConnection)

        Try
            Using rdr As New SqlReader(SQLForeignKeys, conn)
                With rdr

                    Do While .Read

                        Dim primarytablename As String = .Item("ParentTable")
                        If SchemaNames Then
                            primarytablename = .Item("ParentSchema") & "." & primarytablename
                        End If
                        Dim primarytable As Table = RowSet(primarytablename)
                        Dim primarycolumn As Column = primarytable.Column(.Item("ParentColumn"))

                        Dim foreigntablename As String = .Item("ChildTable")
                        If SchemaNames Then
                            foreigntablename = .Item("ChildSchema") & "." & foreigntablename
                        End If
                        Dim foreigntable As Table = RowSet(foreigntablename)
                        Dim foreigncolumn As Column = foreigntable.Column(.Item("ChildColumn"))

                        foreigncolumn.AddPrimary(primarycolumn, .ItemInt("ParentOrdinal"), .ItemInt("ChildOrdinal"))

                        Status("FK " & .Item("ParentTable") & "." & .Item("ParentColumn") & "->" &
                                       .Item("ChildTable") & "." & .Item("ChildColumn"))
                    Loop

                End With
            End Using

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub PopulateIdentityColumns(conn As SqlConnection)

        Try

            Using rdr As New SqlReader(SQLIdentityColumns, conn)

                With rdr

                    Do While .Read

                        Dim table As Table = FindTable(.Item("SchemaName"), .Item("TableName"))
                        Dim column As Column = table.Column(.Item("ColumnName"))

                        column.isIdentity = True
                        Status($"{table.Name}.{column.Name} is identity")
                    Loop

                End With
            End Using

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub PopulatePrimaryKeys(conn As SqlConnection)

        Try
            Using rdr As New SqlReader(SQLPrimaryKeys, conn)
                With rdr

                    Do While .Read

                        Dim tablename As String = .Item("TableName")
                        If _SchemaNames Then
                            tablename = .Item("TableSchema") & "." & tablename
                        End If

                        RowSet(tablename).AddPrimary(RowSet(tablename).Column(.Item("ColumnName")), .ItemInt("OrdinalPosition"))

                        Status("PK " & .Item("ConstraintName"))

                    Loop

                End With
            End Using

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub PopulateTables(conn As SqlConnection)

        Try
            Using rdr As New SqlReader(SQLSchemas, conn)
                With rdr

                    .Read()
                    Dim schemas As Integer = .ItemInt("schemas")
                    _SchemaNames = schemas > 1

                End With
            End Using

            Using rdr As New SqlReader(SQLTables, conn)
                With rdr
                    Do While .Read

                        Dim tablename As String = .Item("TableName")
                        Dim schemaname As String = .Item("SchemaName")
                        Dim table As New Table(Me, schemaname, tablename)

                        RowSet.Add(table.Name, table)

                        Status("Table " & table.Name)

                    Loop
                End With
            End Using

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Public ReadOnly Property SchemaNames As Boolean
    Public ReadOnly Property Server As Server
    Public Function SQLTypeName(typename As SQLType) As String
        Return _SqlTypes.Where(Function(q) q.Value = typename).Single.Key
    End Function
    Public Function SqlTypeOf(conn As SqlConnection, sqltypename As String) As SQLType

        Dim result As SQLType = Nothing
        Try

            If _SqlTypes.TryGetValue(sqltypename, result) Then
                Return result
            End If

            ' Ah-ha, a user-defined type. Find and setup the underlying type

            Dim underlying As String = sqltypename
            Dim sanity As Integer = 0
            Do
                Dim query As String = "
                SELECT 
	                u.name AS userdefined,
	                s.name AS underlying,
	                u.precision,
	                u.scale,
	                u.max_length
                FROM sys.types u
                INNER JOIN sys.types s
	                ON u.system_type_id = s.user_type_id
                WHERE u.is_user_defined = 1" &
                $"  AND u.name = '{underlying}'"


                With New SqlReader(SQLColumns, conn)
                    .Read()
                    underlying = .Item("underlying")
                End With

                sanity += 1

            Loop Until _SqlTypes.ContainsKey(underlying) OrElse sanity > 10

            If Not _SqlTypes.ContainsKey(underlying) Then ' OMFG, what on earth was the database designer thinking of?

                ShowBox($"User-defined type '{sqltypename}' has an unknown base type '{underlying}' (another user-defined type?).",
                         "Heretical database design",, MessageBoxIcon.Exclamation)

                Return SQLType.nvarchar
            End If

            Dim underlyingtype As SQLType = _SqlTypes(underlying)

            _SqlTypes.Add(sqltypename, underlyingtype)

            result = underlyingtype

        Catch ex As Exception
            HandleError(ex)
        End Try

        Return result

    End Function
    Private Sub Status(text As String)

        Logger.Trace(text)

        ' Don't update status more than 4 times a second
        If Stopwatch.ElapsedMilliseconds < 250 Then
            Exit Sub
        End If

        Stopwatch.Restart()

        RaiseEvent Progress(Me, text)

    End Sub
    Public ReadOnly Property Tables As IEnumerable(Of Table)
        Get
            Return RowSet.Values.OrderBy(Function(q) q.Name)
        End Get
    End Property
    ''' <summary>
    ''' Convert a .Net value to an SQL Literal
    ''' </summary>
    Public Function ToSQL(netvalue As Object, sqltype As SQLType) As String

        If isEmpty(netvalue) Then
            Return "null"
        End If

        Return Converter.NetToSQL(sqltype, netvalue)

    End Function
    ''' <summary>
    ''' Convert AN SQL Literal to a .Net value
    ''' </summary>
    Public Function ToNet(sqltype As SQLType, sqlvalue As Object) As Object

        If isEmpty(sqlvalue) Then
            Return Nothing
        End If

        Return Converter.SQLToNet(sqltype, sqlvalue)

    End Function
    Public Overrides Function ToString() As String

        Return $"Server {Server.Name} Tables {Tables.Count}"

    End Function
End Class
