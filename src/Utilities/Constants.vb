Public Module Constants

    Public Const RIGHTARROW As String = ChrW(8594)

    ' When we store new values that the user has typed, those values can be Nothing,
    ' when he deletes the cell. So we need a way to differentiate 'was not modified' 
    ' from 'was modified to nothing'.
    ' Thus, instead of writing
    '   if newvalue isnot nothing
    ' which would be wrong if the new value is nothing we write
    '   if newvalue isnot novalue
    Public ReadOnly Property NoValue As Object = "WTF!?" ' If this value ever appears anywhere we have a SNAFU

End Module
