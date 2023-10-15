Imports System.Collections.ObjectModel
Imports ProtoBuf

<ProtoContract>
Public Class State

    Friend Sub New() ' Serialisation
    End Sub
    Public Sub AddRecent(server As Server, database As Database)

        Dim add As Recent = New Recent(server, database)

        _Recent = {add}.
                  Concat(_Recent.Where(Function(q) Not q.Equals(add)).
                         Select(Function(q) q)).
                  ToList

    End Sub
    Public Sub AddServer(server As Server)

        If Not _Servers.Contains(server) Then

            _Servers.Add(server)

        End If
    End Sub
    <ProtoMember(1)>
    Private _Recent As New List(Of Recent)
    Public ReadOnly Property Recent As ReadOnlyCollection(Of Recent)
        Get
            Return New ReadOnlyCollection(Of Recent)(_Recent)
        End Get
    End Property
    <ProtoMember(2)>
    Private _Servers As New List(Of Server)
    Public ReadOnly Property Servers As ReadOnlyCollection(Of Server)
        Get
            Return New ReadOnlyCollection(Of Server)(_Servers)
        End Get
    End Property
    Public Overrides Function ToString() As String

        Return $"Servers {_Servers.Count} Recent {_Recent.Count}"

    End Function
End Class
