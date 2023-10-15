
' ================================================================================================
' Microsoft.SqlServer.Types *MUST* be the old 2008 version 10.50.1600.1
' All the newer ones fail
' <package id="Microsoft.SqlServer.Types" version="10.50.1600.1" targetFramework="net472" />
' ================================================================================================

Imports System.ComponentModel

Imports Microsoft.SqlServer.Types ' *MUST* be the old 2008 version 10.50.1600.1
Public Class Converter

    Delegate Function NetToTextDelegate(sqltype As SQLType, value As Object) As String
    Delegate Function NetToSQLDelegate(sqltype As SQLType, value As Object) As String
    Delegate Function SQLToNetDelegate(sqltype As SQLType, value As Object) As Object
    Delegate Function TextToNetDelegate(sqltype As SQLType, value As String) As Object

    Private ReadOnly Property Conversions As New Dictionary(Of SQLType, Conversion)

    Private Class Conversion

        Public ReadOnly Property SQLType As SQLType
        Public ReadOnly Property NetType As Type
        Private ReadOnly Property NetToTextDelegate As NetToTextDelegate
        Private ReadOnly Property NetToSQLDelegate As NetToSQLDelegate
        Private ReadOnly Property SQLToNetDelegate As SQLToNetDelegate
        Private ReadOnly Property TextToNetDelegate As TextToNetDelegate
        Sub New(sqltype As SQLType, netType As Type, nettotext As NetToTextDelegate, nettosql As NetToSQLDelegate, sqltonet As SQLToNetDelegate, texttonet As TextToNetDelegate)

            Me.SQLType = sqltype
            Me.NetType = netType
            Me.NetToTextDelegate = nettotext
            Me.NetToSQLDelegate = nettosql
            Me.SQLToNetDelegate = sqltonet
            Me.TextToNetDelegate = texttonet

        End Sub
        Public Function NetToText(value As Object) As String

            Return NetToTextDelegate(SQLType, value)

        End Function
        Public Function NetToSQL(value As Object) As String

            Return NetToSQLDelegate(SQLType, value)

        End Function
        Public Function SQLToNet(value As Object) As Object

            Return SQLToNetDelegate(SQLType, value)

        End Function
        Public Function TextToNet(value As String) As Object

            Return TextToNetDelegate(SQLType, value)

        End Function
        Public Overrides Function ToString() As String
            Return $"{SQLType} {NetType}"
        End Function
    End Class

    Public Sub New()
        Setup()
    End Sub
    Private Sub AddConverter(sqltype As SQLType, netType As Type,
                             nettotext As NetToTextDelegate,
                             nettosql As NetToSQLDelegate,
                             sqltonet As SQLToNetDelegate,
                             texttonet As TextToNetDelegate)

        Conversions.Add(sqltype, New Conversion(sqltype, netType, nettotext, nettosql, sqltonet, texttonet))

    End Sub
    Public Function NetToSQL(sqltype As SQLType, netvalue As Object) As String

        Dim result As String = Conversions(sqltype).NetToSQL(netvalue)

        If FundamentalTypeOf(sqltype) = Fundamental.string Then
            result = SingleQuote(result)
        End If

        Return result

    End Function
    Public Function SQLToNet(sqltype As SQLType, sqlvalue As Object) As Object

        Return Conversions(sqltype).SQLToNet(sqlvalue)

    End Function
    Public Function NetToText(sqltype As SQLType, netvalue As Object) As String

        Return Conversions(sqltype).NetToText(netvalue)

    End Function
    Public Function TextToNet(sqltype As SQLType, netvalue As String) As Object

        Return Conversions(sqltype).TextToNet(netvalue)

    End Function
    Private Sub Setup()


        ' The more-or-less standard fields, which we can edit
        AddConverter(SQLType.bigint, GetType(Long), AddressOf NetToTextConverter, AddressOf NetToSQLConverter, AddressOf SQLToNetConverter, AddressOf TextToNetConverter)
        AddConverter(SQLType.binary, GetType(Byte()), AddressOf BinaryToText, AddressOf BinaryToSQL, AddressOf BinaryToNet, AddressOf TextToBinary)
        AddConverter(SQLType.bit, GetType(Boolean), AddressOf BitToText, AddressOf BitToSql, AddressOf BitToNet, AddressOf TextToBit)
        AddConverter(SQLType.char, GetType(String), AddressOf NetToTextConverter, AddressOf NetToSQLConverter, AddressOf SQLToNetConverter, AddressOf TextToNetConverter)
        AddConverter(SQLType.date, GetType(DateTime), AddressOf NetToTextConverter, AddressOf DateToSQL, AddressOf DateToNET, AddressOf TextToNetConverter)
        AddConverter(SQLType.datetime, GetType(DateTime), AddressOf NetToTextConverter, AddressOf DateToSQL, AddressOf DateToNET, AddressOf TextToNetConverter)
        AddConverter(SQLType.datetime2, GetType(DateTime), AddressOf NetToTextConverter, AddressOf DateToSQL, AddressOf DateToNET, AddressOf TextToNetConverter)
        AddConverter(SQLType.datetimeoffset, GetType(DateTimeOffset), AddressOf NetToTextConverter, AddressOf OffsetToSQL, AddressOf OffsetToNet, AddressOf TextToOffset)
        AddConverter(SQLType.decimal, GetType(Decimal), AddressOf NetToTextConverter, AddressOf NetToSQLConverter, AddressOf SQLToNetConverter, AddressOf TextToNetConverter)
        AddConverter(SQLType.float, GetType(Double), AddressOf NetToTextConverter, AddressOf NetToSQLConverter, AddressOf SQLToNetConverter, AddressOf TextToNetConverter)
        AddConverter(SQLType.int, GetType(Integer), AddressOf NetToTextConverter, AddressOf NetToSQLConverter, AddressOf SQLToNetConverter, AddressOf TextToNetConverter)
        AddConverter(SQLType.money, GetType(Decimal), AddressOf NetToTextConverter, AddressOf NetToSQLConverter, AddressOf SQLToNetConverter, AddressOf TextToNetConverter)
        AddConverter(SQLType.nchar, GetType(String), AddressOf NetToTextConverter, AddressOf NetToSQLConverter, AddressOf SQLToNetConverter, AddressOf TextToNetConverter)
        AddConverter(SQLType.ntext, GetType(String), AddressOf NetToTextConverter, AddressOf NetToSQLConverter, AddressOf SQLToNetConverter, AddressOf TextToNetConverter)
        AddConverter(SQLType.numeric, GetType(Decimal), AddressOf NetToTextConverter, AddressOf NetToSQLConverter, AddressOf SQLToNetConverter, AddressOf TextToNetConverter)
        AddConverter(SQLType.nvarchar, GetType(String), AddressOf NetToTextConverter, AddressOf NetToSQLConverter, AddressOf SQLToNetConverter, AddressOf TextToNetConverter)
        AddConverter(SQLType.real, GetType(Double), AddressOf NetToTextConverter, AddressOf NetToSQLConverter, AddressOf SQLToNetConverter, AddressOf TextToNetConverter)
        AddConverter(SQLType.smalldatetime, GetType(DateTime), AddressOf NetToTextConverter, AddressOf DateToSQL, AddressOf DateToNET, AddressOf TextToNetConverter)
        AddConverter(SQLType.smallint, GetType(Integer), AddressOf NetToTextConverter, AddressOf NetToSQLConverter, AddressOf SQLToNetConverter, AddressOf TextToNetConverter)
        AddConverter(SQLType.smallmoney, GetType(Decimal), AddressOf NetToTextConverter, AddressOf NetToSQLConverter, AddressOf SQLToNetConverter, AddressOf TextToNetConverter)
        AddConverter(SQLType.text, GetType(String), AddressOf NetToTextConverter, AddressOf NetToSQLConverter, AddressOf SQLToNetConverter, AddressOf TextToNetConverter)
        AddConverter(SQLType.time, GetType(DateTime), AddressOf NetToTextConverter, AddressOf TimeToSQL, AddressOf TimeToNet, AddressOf TextToNetConverter)
        AddConverter(SQLType.tinyint, GetType(Integer), AddressOf NetToTextConverter, AddressOf NetToSQLConverter, AddressOf SQLToNetConverter, AddressOf TextToNetConverter)
        AddConverter(SQLType.varbinary, GetType(Byte()), AddressOf BinaryToText, AddressOf BinaryToSQL, AddressOf BinaryToNet, AddressOf TextToBinary)
        AddConverter(SQLType.varchar, GetType(String), AddressOf NetToTextConverter, AddressOf NetToSQLConverter, AddressOf SQLToNetConverter, AddressOf TextToNetConverter)
        AddConverter(SQLType.xml, GetType(String), AddressOf NetToTextConverter, AddressOf NetToSQLConverter, AddressOf SQLToNetConverter, AddressOf TextToNetConverter)

        ' The weirdos that we can't edit
        AddConverter(SQLType.geography, Nothing, AddressOf NetToTextConverter, Nothing, AddressOf NetToTextConverter, Nothing)
        AddConverter(SQLType.geometry, Nothing, AddressOf NetToTextConverter, Nothing, AddressOf NetToTextConverter, Nothing)
        AddConverter(SQLType.hierarchyid, Nothing, AddressOf NetToTextConverter, Nothing, AddressOf NetToTextConverter, Nothing)
        AddConverter(SQLType.image, GetType(Byte()), AddressOf BinaryToText, Nothing, AddressOf BinaryToNet, Nothing)
        AddConverter(SQLType.sql_variant, Nothing, AddressOf NetToTextConverter, Nothing, AddressOf NetToTextConverter, Nothing)
        AddConverter(SQLType.timestamp, Nothing, AddressOf NetToTextConverter, AddressOf BinaryToSQL, AddressOf BinaryToNet, Nothing)
        AddConverter(SQLType.uniqueidentifier, Nothing, AddressOf NetToTextConverter, Nothing, AddressOf NetToTextConverter, Nothing)

        ' Rowversion is an alias for timestamp https://dba.stackexchange.com/a/86531/76518
        ' So we would write this, if ever someone passed an SQLType of rowversion
        ' AddConverter("rowversion", "special")

    End Sub
    Private Function NetToSQLConverter(sqltype As SQLType, value As Object) As String

        Return value.ToString

    End Function
    Private Function NetToTextConverter(sqltype As SQLType, value As Object) As String

        Return value.ToString

    End Function
    Private Function SQLToNetConverter(sqltype As SQLType, value As Object) As Object

        Return value

    End Function
    Private Function TextToNetConverter(sqltype As SQLType, value As String) As Object

        Dim conversion As Conversion = Conversions(sqltype)

        Dim result As Object = TypeDescriptor.GetConverter(conversion.NetType).ConvertFromString(value)

        Return result

    End Function
    Private Function BinaryToSQL(sqltype As SQLType, value As Object) As String

        Return "0X" & DirectCast(value, String)

    End Function
    Private Function BinaryToNet(sqltype As SQLType, value As Object) As Object

        ' DataGridView doesn't handle binary, so we show it as hex and
        ' then UPDATE .. SET field = 0Xabcdef...
        ' So the first time we get a binary value it is Byte(), 
        ' but once edited it'll be a hex string
        If TypeOf (value) Is String Then
            Return value
        End If

        Return ByteArrayToString(DirectCast(value, Byte()))

    End Function
    Private Function BinaryToText(sqltype As SQLType, value As Object) As String

        Return ByteArrayToString(DirectCast(value, Byte()))

    End Function
    Private Function TextToBinary(sqltype As SQLType, value As String) As Object

        Return value

    End Function
    Private Function BitToSql(sqltype As SQLType, value As Object) As String

        Return If(CBool(value), "1", "0")

    End Function
    Private Function BitToNet(sqltype As SQLType, value As Object) As Object

        Return CBool(value)

    End Function
    Private Function BitToText(sqltype As SQLType, value As Object) As String

        Return value.ToString

    End Function
    Private Function TextToBit(sqltype As SQLType, value As Object) As String

        Return If(CBool(value), "1", "0")

    End Function

    Private Function DateToSQL(sqltype As SQLType, value As Object) As String

        Return "convert(datetime2," & SingleQuote(String.Format("{0:yyyy-MM-dd hh:mm:ss.fff}", value)) & ",121)"

    End Function
    Private Function DateToNET(sqltype As SQLType, value As Object) As Object

        Return CDate(value)

    End Function
    Private Function OffsetToSQL(sqltype As SQLType, value As Object) As String

        Dim dto As DateTimeOffset = DateTimeOffset.Parse(value.ToString)
        Dim literal As String = dto.ToString("yyyy-MM-ddTHH:mm:ss.fffzzzz")
        Return SingleQuote(literal)

    End Function
    Private Function OffsetToNet(sqltype As SQLType, value As Object) As Object

        Return value

    End Function
    Private Function TextToOffset(sqltype As SQLType, value As String) As Object

        Dim result As DateTimeOffset = DateTimeOffset.Parse(CStr(value))
        Return result

    End Function
    Private Function TimeToSQL(sqltype As SQLType, value As Object) As String

        Return SingleQuote(String.Format("{0:hh:mm:ss.fff}", value))

    End Function
    Private Function TimeToNet(sqltype As SQLType, value As Object) As Object

        Return String.Format("{0:hh\:mm\:ss\.fff}", value)

    End Function
End Class
