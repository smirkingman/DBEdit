Imports System.Data.SqlClient

Public Class SqlReader
    Implements IDisposable

    Private ReadOnly _Command As SqlCommand
    Private ReadOnly _Reader As SqlDataReader
    Private disposedValue As Boolean

    Public Sub New(query As String, conn As SqlConnection)

        _Command = New SqlCommand(query, conn)
        _Reader = _Command.ExecuteReader

    End Sub
    Public Sub Close()
        _Reader.Close()
    End Sub
    ReadOnly Property ItemInt(name As String) As Integer
        Get
            If isEmpty(_Reader(name)) Then
                Return Nothing
            End If
            Return CInt(_Reader(name))
        End Get
    End Property
    Default ReadOnly Property Item(name As String) As String
        Get
            If isEmpty(_Reader(name)) Then
                Return Nothing
            End If
            Return CStr(_Reader(name))
        End Get
    End Property
    Public Function Read() As Boolean
        Return _Reader.Read()
    End Function

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _Reader.Close()
            End If
            disposedValue = True
        End If
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
