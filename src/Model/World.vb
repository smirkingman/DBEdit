Imports System.Collections.ObjectModel
Imports System.Data.Sql
Imports System.IO
Imports System.ServiceProcess

Imports ProtoBuf

Public Class World

    Public Event Notify(sender As Object, e As MessageEventArgs)
    Friend Sub New()
    End Sub
    Private Sub EnumerateServers()

        Try
            Dim serviceController As New ServiceController

            If ServiceController.
               GetServices().
               Any(Function(q) q.ServiceName.Equals("SQL Server Browser")) Then

                If serviceController.Status = ServiceControllerStatus.Running Then

                    Dim enumerator As SqlDataSourceEnumerator = SqlDataSourceEnumerator.Instance
                    Dim dataTable As DataTable = enumerator.GetDataSources()
                    Dim discovered As New List(Of Server)

                    For Each row As DataRow In dataTable.Rows

                        Dim servername As String = CStr(row("ServerName"))
                        Try
                            Dim instancename As String = IfEmpty(row("InstanceName"), "")

                            Dim isclustered As Boolean = CStr(row("IsClustered")).ToUpper.StartsWith("Y")

                            Dim version As String = IfEmpty(row("Version"), "")

                            Dim instance As New Server(servername, instancename, isclustered, version)

                            If Not State.Servers.Contains(instance) Then
                                State.AddServer(instance)
                            End If

                        Catch ex As Exception
                            RaiseEvent Notify(Me, New MessageEventArgs($"Error discovering {servername}: {ex.Message}", Severity.Error))

                        End Try

                    Next

                End If
            End If

        Catch ex As Exception
            RaiseEvent Notify(Me, New MessageEventArgs($"Unable to discover servers: {ex.Message}", Severity.Warning))
        End Try

    End Sub
    Public Sub Initialise()

        Load()

        Task.Run(Sub() EnumerateServers())

    End Sub
    Private Sub Load()

        If My.Settings.State = "" Then

            _State = New State

        Else
            Dim values As Byte() = System.Convert.FromBase64String(My.Settings.State)

            Using ms As New MemoryStream(values)
                _State = Serializer.Deserialize(Of State)(ms)
            End Using

        End If

    End Sub
    Public ReadOnly Property MostRecent As Recent
        Get
            Return State.Recent.FirstOrDefault
        End Get
    End Property
    Public ReadOnly Property Recent As ReadOnlyCollection(Of Recent)
        Get
            Return State.Recent
        End Get
    End Property
    Public Sub Save()

        Using ms As New MemoryStream()

            Serializer.Serialize(ms, State)
            My.Settings.State = Convert.ToBase64String(ms.GetBuffer, 0, CInt(ms.Length))

        End Using

    End Sub
    Public Function Server(name As String) As Server

        Dim result As Server = Servers.Where(Function(q) q.Name = name).SingleOrDefault

        If result Is Nothing Then
            result = New Server(name)
            State.AddServer(result)
        End If

        Return result
    End Function
    Public Function Server(servername As String, instancename As String) As Server

        Dim name As String = servername

        If instancename <> "" Then

            name &= "\" & instancename

        End If

        Return Server(name)

    End Function
    Public ReadOnly Property Servers As ReadOnlyCollection(Of Server)
        Get
            Return State.Servers
        End Get
    End Property
    Friend ReadOnly Property State As State

End Class
