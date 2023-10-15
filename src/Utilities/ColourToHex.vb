''' <summary>
''' Convert a system.color into it's hex representation
''' </summary>
''' <remarks></remarks>
Public Module ColourToHex_

    ''' <summary>
    ''' Convert a system.color into it's hex representation
    ''' </summary>
    ''' <param name="c">a <see cref="system.drawing.color" /></param>
    ''' <returns>6-character hexadecimal string RRGGBB</returns>
    ''' <remarks></remarks>
    Public Function ColourToHex(c As Color) As String

        Return String.Format("{0:x2}{1:x2}{2:x2}", c.R, c.G, c.B)

    End Function

End Module
