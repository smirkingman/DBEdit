Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Text
Imports System.Reflection

Public Module SizeOf_

    ' https://github.com/CyberSaving/MemoryUsage/blob/master/Main/Program.cs

    Public Function SizeOf(ByVal value As Object) As Integer

        If value Is Nothing Then
            Return 0
        End If

        Return SizeOfObj(value.GetType, value, Nothing)

    End Function

    Private Function SizeOfClass(ByVal thevalue As Object, ByVal gen As ObjectIDGenerator) As Integer

        Dim isfirstTime As Boolean = Nothing
        gen.GetId(thevalue, isfirstTime)
        If Not isfirstTime Then Return 0
        Dim fields As FieldInfo() = thevalue.[GetType]().GetFields(BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance)
        Dim returnval As Integer = 0

        For i As Integer = 0 To fields.Length - 1
            Dim t As Type = fields(i).FieldType
            Dim v As Object = fields(i).GetValue(thevalue)
            returnval += 4 + SizeOfObj(t, v, gen)
        Next

        Return returnval
    End Function

    Private Function SizeOfObj(ByVal T As Type, ByVal thevalue As Object, ByVal gen As ObjectIDGenerator) As Integer

        Dim type As Type = T
        Dim returnval As Integer = 0

        If type.IsValueType Then
            If type.Name = "DateTime" OrElse type.Name = "DateTimeOffset" Then
                Return 8
            End If
            Dim nulltype As Type = Nullable.GetUnderlyingType(type)
            returnval = System.Runtime.InteropServices.Marshal.SizeOf(If(nulltype, type))
        ElseIf thevalue Is Nothing Then
            Return 0
        ElseIf TypeOf thevalue Is String Then
            returnval = Encoding.[Default].GetByteCount(TryCast(thevalue, String))
        ElseIf type.IsArray AndAlso type.GetElementType().IsValueType Then
            returnval = (CType(thevalue, Array)).GetLength(0) * System.Runtime.InteropServices.Marshal.SizeOf(type.GetElementType())
        ElseIf TypeOf thevalue Is Stream Then
            Dim thestram As Stream = TryCast(thevalue, Stream)
            returnval = CInt(thestram.Length)
        ElseIf type.IsSerializable Then

            Try

                Using s As Stream = New MemoryStream()
                    Dim formatter As BinaryFormatter = New BinaryFormatter()
                    formatter.Serialize(s, thevalue)
                    returnval = CInt(s.Length)
                End Using

            Catch
            End Try
        ElseIf type.IsClass Then
            returnval += SizeOfClass(thevalue, If(gen, New ObjectIDGenerator()))
        End If

        If returnval = 0 Then

            Try
                returnval = System.Runtime.InteropServices.Marshal.SizeOf(thevalue)
            Catch
            End Try
        End If

        Return returnval
    End Function

End Module
