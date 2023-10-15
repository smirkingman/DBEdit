'Imports System.Runtime.CompilerServices
'Imports System.Runtime.InteropServices

'Module Extensions_

'    <Extension()>
'    Public Function Show(row As DataGridViewRow) As String
'        Return String.Join(",", row.Cells.OfType(Of DataGridViewCell).Select(Function(q, i) i.ToString & "=" & q.Value.ToString))
'    End Function

'    <Extension()>
'    Public Function Show(cell As DataGridViewCell) As String
'        Return CStr(cell.Value)
'    End Function

'    <Extension()>
'    Public Function Show(row As DataRow) As String
'        Return String.Join(",", row.ItemArray.Select(Function(q, i) CStr(i) & "=" & CStr(q)))
'    End Function
'    <DllImport("user32.dll")>
'    Private Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Boolean, ByVal lParam As IntPtr) As Integer
'    End Function

'    Private Const WM_SETREDRAW As Integer = 11

'    ' Extension methods for Control
'    <Extension()>
'    Public Sub ResumeDrawing(ByVal Target As Control, ByVal Redraw As Boolean)
'        SendMessage(Target.Handle, WM_SETREDRAW, True, IntPtr.Zero)
'        If Redraw Then
'            Target.Refresh()
'        End If
'    End Sub

'    <Extension()>
'    Public Sub SuspendDrawing(ByVal Target As Control)
'        SendMessage(Target.Handle, WM_SETREDRAW, False, IntPtr.Zero)
'    End Sub

'    <Extension()>
'    Public Sub ResumeDrawing(ByVal Target As Control)
'        ResumeDrawing(Target, True)
'    End Sub
'End Module
