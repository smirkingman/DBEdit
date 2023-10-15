Public Module Fundamental_
    Public ReadOnly Property FundamentalTypeOf As New Dictionary(Of SQLType, Fundamental) From
        {
            {SQLType.bigint, Fundamental.integer},
            {SQLType.binary, Fundamental.binary},
            {SQLType.bit, Fundamental.bit},
            {SQLType.[char], Fundamental.string},
            {SQLType.[date], Fundamental.date},
            {SQLType.datetime, Fundamental.date},
            {SQLType.datetime2, Fundamental.date},
            {SQLType.datetimeoffset, Fundamental.offset},
            {SQLType.[decimal], Fundamental.decimal},
            {SQLType.float, Fundamental.double},
            {SQLType.geography, Fundamental.special},
            {SQLType.geometry, Fundamental.special},
            {SQLType.hierarchyid, Fundamental.special},
            {SQLType.image, Fundamental.binary},
            {SQLType.int, Fundamental.integer},
            {SQLType.money, Fundamental.decimal},
            {SQLType.nchar, Fundamental.string},
            {SQLType.ntext, Fundamental.string},
            {SQLType.numeric, Fundamental.decimal},
            {SQLType.nvarchar, Fundamental.string},
            {SQLType.real, Fundamental.double},
            {SQLType.smalldatetime, Fundamental.date},
            {SQLType.smallint, Fundamental.integer},
            {SQLType.smallmoney, Fundamental.decimal},
            {SQLType.sql_variant, Fundamental.special},
            {SQLType.text, Fundamental.string},
            {SQLType.time, Fundamental.time},
            {SQLType.timestamp, Fundamental.special},
            {SQLType.tinyint, Fundamental.integer},
            {SQLType.uniqueidentifier, Fundamental.special},
            {SQLType.varbinary, Fundamental.binary},
            {SQLType.varchar, Fundamental.string},
            {SQLType.xml, Fundamental.string}
        }

    Public Enum Fundamental
        binary
        bit
        [date]
        [decimal]
        [double]
        [integer]
        [long]
        offset
        special
        [string]
        time
    End Enum

End Module
