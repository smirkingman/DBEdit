Imports ProtoBuf

<ProtoContract>
Public Class Database
    Implements IEquatable(Of Database)

    Friend Sub New() ' Serialisation
    End Sub
    Public Sub New(servername As String, name As String)

        _ServerName = servername
        _Name = name.ToUpper

    End Sub
    Public ReadOnly Property FullName As String
        Get
            Return $"{Name} on {ServerName}"
        End Get
    End Property
    <ProtoMember(1)>
    Private _Name As String
    Public ReadOnly Property Name As String
        Get
            Return _Name
        End Get
    End Property
    <ProtoMember(2)>
    Private _ServerName As String
    Public ReadOnly Property ServerName As String
        Get
            Return _ServerName
        End Get
    End Property
    Public Overloads Function Equals(other As Database) As Boolean Implements IEquatable(Of Database).Equals

        Return Name = other.Name

    End Function
    Public Overrides Function GetHashCode() As Int32

        Return Name.GetHashCode()

    End Function
    Public Overrides Function ToString() As String

        Return FullName

    End Function
End Class

