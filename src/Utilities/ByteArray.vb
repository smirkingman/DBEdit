Imports System.Text

Public Module ByteArray_

    Public Function ByteArrayToString(ByVal ba As Byte()) As String

        Dim hex As New StringBuilder(ba.Length * 2)

        For Each b As Byte In ba
            hex.AppendFormat("{0:X2}", b)
        Next

        Return hex.ToString

    End Function
    Public Function StringToByteArray(ByVal hex As String) As Byte()

        Dim NumberChars As Integer = hex.Length
        Dim bytes As Byte() = New Byte((NumberChars \ 2) - 1) {}

        For i As Integer = 0 To NumberChars - 1 Step 2
            bytes(i \ 2) = Convert.ToByte(hex.Substring(i, 2), 16)
        Next

        Return bytes

    End Function

End Module
