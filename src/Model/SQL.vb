Imports System.Data.SqlClient

Imports NLog

Public Module SQL

    Public Function Connection(server As Server, database As Database) As SqlConnection

        Return New SqlConnection(ConnectionString(server, database))

    End Function
    Public Function ConnectionString(server As Server, Database As Database) As String

        If isEmpty(server.Name) Then
            Return Nothing
        End If

        Dim result As String = "Server=" & server.Name & ";MultipleActiveResultSets=True;"

        If Database IsNot Nothing Then
            result &= "Initial Catalog=" & Database.Name & ";"
        End If

        If server.User = "" Then
            result &= "Trusted_Connection=True;"
        Else
            result &= "User Id=" & server.User & ";Password=" & server.Password & ";"
        End If

        Return result

    End Function
    Public Function ExecuteNonQuery(server As Server, database As Database, query As String) As Integer

        Logger.Debug($"{Compact(query)}")

        Dim modified As Integer = 0

        Using conn As SqlConnection = Connection(server, database)
            conn.Open()
            Using cmd As New SqlCommand(query, conn)
                modified = cmd.ExecuteNonQuery()
            End Using
            conn.Close()
        End Using

        Return modified

    End Function
    Public Function ExecuteScalar(server As Server, database As Database, query As String) As Object

        Logger.Debug($"{Compact(query)}")

        Dim result As Object = Nothing
        Using conn As SqlConnection = Connection(server, database)
            conn.Open()
            Using cmd As New SqlCommand(query, conn)
                result = cmd.ExecuteScalar
            End Using
            conn.Close()
        End Using
        Return result

    End Function
    Public Logger As Logger = LogManager.GetCurrentClassLogger
End Module
