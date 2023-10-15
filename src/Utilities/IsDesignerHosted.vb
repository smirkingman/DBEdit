Imports System.ComponentModel

Public Module IsDesignerHosted_
    ''' <summary>
    ''' The DesignMode property does not correctly tell you if
    ''' you are in design mode.  IsDesignerHosted is a corrected
    ''' version of that property.
    ''' (see https://connect.microsoft.com/VisualStudio/feedback/details/553305
    ''' and http://stackoverflow.com/a/2693338/238419 )
    ''' </summary>
    Public Function IsDesignerHosted(him As Control) As Boolean

        If LicenseManager.UsageMode = LicenseUsageMode.Designtime Then
            Return True
        End If

        Dim ctrl As Control = him
        While ctrl IsNot Nothing
            If ctrl.Site IsNot Nothing AndAlso ctrl.Site.DesignMode Then
                Return True
            End If
            ctrl = ctrl.Parent
        End While
        Return False
    End Function
End Module
