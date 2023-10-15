Imports System.Collections.ObjectModel
Imports System.Data.SqlClient
Imports ProtoBuf

<ProtoContract>
Public Class Server
    Implements IEquatable(Of Server)

    Friend Sub New() ' Serialisation
    End Sub
    Public Sub New(servername As String, instancename As String, isclustered As Boolean, version As String)

        _ServerName = servername.ToUpper
        _InstanceName = instancename
        _isClustered = isclustered
        _Version = version

        Task.Run(Sub() GetDatabases())

    End Sub
    Public Sub New(name As String)

        Dim parts() As String = name.Split("\"c)
        _ServerName = parts(0).ToUpper
        _InstanceName = If(parts.Count = 1, "", parts(1))
        _isClustered = False
        _Version = ""

        Task.Run(Sub() GetDatabases())

    End Sub
    Public Function Database(name As String) As Database

        Dim result As Database = Databases.Where(Function(q) q.Name = name).SingleOrDefault

        If result Is Nothing Then
            result = New Database(Me.Name, name)
            _Databases.Add(result)
        End If

        Return result

    End Function
    <ProtoMember(1)>
    Private _Databases As New List(Of Database)
    Public ReadOnly Property Databases As ReadOnlyCollection(Of Database)
        Get
            Return _Databases.AsReadOnly
        End Get
    End Property
    Public Sub GetDatabases()

        Try
            Using conn As SqlConnection = SQL.Connection(Me, Nothing)

                conn.Open()

                Dim dbs As New List(Of Database)

                Dim query As String = "SELECT name FROM sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb') ORDER BY name"

                With New SqlReader(query, conn)

                    Do While .Read

                        Dim dbname As String = .Item("name")
                        Dim db As New Database(Me.Name, dbname)
                        dbs.Add(db)

                    Loop

                End With

                conn.Close()

                _Databases = dbs

            End Using

        Catch

        End Try

    End Sub
    <ProtoMember(2)>
    Private _InstanceName As String
    Public ReadOnly Property InstanceName As String
        Get
            Return _InstanceName
        End Get
    End Property
    <ProtoMember(3)>
    Private _isClustered As Boolean
    Public ReadOnly Property isClustered As Boolean
        Get
            Return _isClustered
        End Get
    End Property
    Public ReadOnly Property Name As String
        Get
            If InstanceName = "" Then
                Return SimpleName
            Else
                Return $"{SimpleName}\{InstanceName}"
            End If
        End Get
    End Property
    <ProtoMember(4)>
    Private _Password As String
    Public Property Password As String
        Get
            Return _Password
        End Get
        Set(value As String)
            _Password = value
        End Set
    End Property
    <ProtoMember(5)>
    Private _ServerName As String
    Public ReadOnly Property SimpleName As String
        Get
            Return _ServerName
        End Get
    End Property
    Public Overrides Function ToString() As String

        Return Name

    End Function
    Public Function TryAuthenticate(db As Database, ByRef failure As String) As Boolean

        Try
            Using conn As SqlConnection = SQL.Connection(Me, db)
                conn.Open()
                conn.Close()
            End Using
            Return True

        Catch ex As Exception
            failure = ex.Message

        End Try

        Return False

    End Function
    <ProtoMember(6)>
    Private _User As String
    Public Property User As String
        Get
            Return _User
        End Get
        Set(value As String)
            _User = value
        End Set
    End Property
    <ProtoMember(7)>
    Private _Version As String
    Public ReadOnly Property Version As String
        Get
            Return _Version
        End Get
    End Property
    Public Overloads Function Equals(other As Server) As Boolean Implements IEquatable(Of Server).Equals

        Return Name = other.Name

    End Function
    Public Overrides Function GetHashCode() As Int32

        Return Name.GetHashCode()

    End Function
End Class
