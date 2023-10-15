Imports DBEdit.Model
Imports MoreLinq

Partial Public Class MainUI

    Private LastWindowState As FormWindowState = FormWindowState.Minimized

    Private Sub Main_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        If WindowState <> LastWindowState Then
            LastWindowState = WindowState
            Main_ResizeEnd(sender, e)
        End If

    End Sub
    Private Sub Main_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd

        StatusStripPanel.Width = ClientRectangle.Width - ToolStrip1.Left

        If tbc.TabPages.Count > 0 Then
            Dim tp As TabPage = tbc.SelectedTab
            If tp IsNot Nothing Then
                Dim grid As Grid = tbc.SelectedTab.Controls.OfType(Of Grid).SingleOrDefault
                If grid IsNot Nothing Then
                    Cache.PAGESIZE = Math.Max(Cache.PAGESIZE, grid.DisplayedRowCount(True))
#If DEBUG Then
                    SetStatus($"PAGESIZE increased to {Cache.PAGESIZE}")
#End If
                End If
            End If
        End If

    End Sub

End Class
