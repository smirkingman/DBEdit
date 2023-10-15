Imports ProtoBuf

<ProtoContract>
Public Class Recent
    Implements IEquatable(Of Recent)

    Friend Sub New() ' Serialisation
    End Sub
    Public Sub New(server As Server, database As Database)

        _Server = server
        _Database = database

    End Sub
    <ProtoMember(1)>
    Private _Database As Database
    Public ReadOnly Property Database As Database
        Get
            Return _Database
        End Get
    End Property
    Public ReadOnly Property Name As String
        Get
            Return $"{Database.Name} on {Server.Name}"
        End Get
    End Property
    <ProtoMember(2)>
    Private _Server As Server
    Public ReadOnly Property Server As Server
        Get
            Return _Server
        End Get
    End Property
    Public Overloads Function Equals(other As Recent) As Boolean Implements IEquatable(Of Recent).Equals

        Return Server.Name = other.Server.Name AndAlso Database.Name = other.Database.Name

    End Function
    Public Overrides Function GetHashCode() As Int32

        Dim hash As Integer = 179
        hash = (hash * 27) + Server.GetHashCode
        hash = (hash * 27) + Database.GetHashCode
        Return hash

    End Function
    Public Overrides Function ToString() As String

        Return Name

    End Function
End Class


