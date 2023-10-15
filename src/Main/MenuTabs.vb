Imports DBEdit.Model

Imports MoreLinq
Partial Public Class MainUI

    Private Property CurrentTab As TabPage
    Private TabsCollapsed As Boolean

    Private Sub ChangeTabpage_MenuItem_Click(sender As Object, e As EventArgs)  ' Handler on/off at runtime in builders.vb

        Dim tsmi As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        Dim tp As TabPage = DirectCast(tsmi.Tag, TabPage)
        SwitchToTabPage(Model, tp)

    End Sub
    Private Sub CollapseTabs_Click(sender As Object, e As EventArgs) Handles CollapseTabsToolStripMenuItem.Click

        tbc.Visible = False ' hide tabcontrol whilst we're fiddling with tabs to reeduce flicker

        Try
            If TabsCollapsed Then

                Dim current As TabPage = tbc.SelectedTab
                tbc.TabPages.Clear()
                tbc.TabPages.AddRange(TabPages.ToArray)
                tbc.SelectedTab = current
                CollapseTabsToolStripMenuItem.Text = "&Collapse tabs"
                SetStatus($"All {tbc.TabPages.Count} tabs displayed", Severity.Info)

            Else

                Dim current As TabPage = tbc.SelectedTab
                tbc.TabPages.Clear()
                tbc.TabPages.Add(current)
                SwitchToTabPage(Model, current)
                CollapseTabsToolStripMenuItem.Text = "&Restore tabs"
                SetStatus("Single tab displayed", Severity.Info)

            End If

            TabsCollapsed = Not TabsCollapsed

        Finally
            tbc.Visible = True

        End Try


    End Sub
    Private Sub HideTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideTabToolStripMenuItem.Click

        If CurrentTab Is Nothing Then
            SetStatus("Mouse isn't over a tab", Severity.Error)
            Exit Sub
        End If

        If tbc.TabPages.Count < 2 Then
            SetStatus("Can't hide sole tab", Severity.Error)
            Exit Sub
        End If

        tbc.TabPages.Remove(CurrentTab)

        SetStatus($"{CurrentTab.Name} hidden", Severity.Info)

        CurrentTab = Nothing

        tbc.Invalidate()

    End Sub
    Private Sub NextTab_Click(sender As Object, e As EventArgs) Handles NextToolStripMenuItem.Click

        Dim thisindex As Integer = TabPages.IndexOf(tbc.SelectedTab)
        Dim nextpage As TabPage = tbc.
                                  TabPages.
                                  OfType(Of TabPage).
                                  Where(Function(q) TabPages.IndexOf(q) > thisindex).
                                  FirstOrDefault
        If nextpage Is Nothing Then
            SetStatus("There is no next tab")
            Exit Sub
        End If
        SwitchToTabPage(Model, nextpage)

    End Sub
    Private Sub PreviousTab_Click(sender As Object, e As EventArgs) Handles PreviousToolStripMenuItem.Click

        Dim thisindex As Integer = TabPages.IndexOf(tbc.SelectedTab)
        Dim nextpage As TabPage = tbc.
                                  TabPages.
                                  OfType(Of TabPage).
                                  Reverse.
                                  Where(Function(q) TabPages.IndexOf(q) < thisindex).
                                  FirstOrDefault
        If nextpage Is Nothing Then
            SetStatus("There is no previous tab")
            Exit Sub
        End If
        SwitchToTabPage(Model, nextpage)

    End Sub
    Private Sub ShowAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowAllToolStripMenuItem.Click

        TabsCollapsed = True
        CollapseTabs_Click(sender, e)

    End Sub
    Private Sub tbc_DrawItem(sender As Object, e As DrawItemEventArgs) Handles tbc.DrawItem

        Try
            Dim page As TabPage = tbc.TabPages(e.Index)
            Dim forecolour As Color = tbc.ForeColor
            Dim backcolour As Color = tbc.BackColor

            If e.State = DrawItemState.Selected Then
                forecolour = Color.White
                backcolour = Color.FromArgb(96, 96, 96)
            End If

            e.Graphics.FillRectangle(New SolidBrush(backcolour), e.Bounds)
            Dim paddedBounds As Rectangle = e.Bounds
            Dim yOffset As Integer = If(e.State = DrawItemState.Selected, -2, 1)
            paddedBounds.Offset(1, yOffset)

            TextRenderer.DrawText(e.Graphics, page.Text, e.Font, paddedBounds, forecolour)

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Private Sub tbc_MouseMove(sender As Object, e As MouseEventArgs) Handles tbc.MouseMove

        CurrentTab = Nothing

        For i As Integer = 0 To tbc.TabPages.Count - 1

            If tbc.GetTabRect(i).Contains(e.Location) Then

                CurrentTab = tbc.TabPages(i)

                ' Start a task to set the tooltip with rowcount.
                ' Note that we're doing this by setting a non-null tag
                If CurrentTab.Tag Is Nothing Then

                    CurrentTab.Tag = ""

                    Task.Run(
                        Sub()
                            Dim rows As Integer = CInt(Model.ExecuteScalar($"SELECT COUNT(*) FROM {Box(CurrentTab.Name)}"))

                            BeginInvoke(
                                Sub()
                                    CurrentTab.ToolTipText = CurrentTab.Name & " has " & rows.ToString("#,##0") & " rows"
                                End Sub)
                        End Sub)

                End If
            End If
        Next

    End Sub
    Private Sub tbc_SelectedIndexChanged(sender As Object, e As System.EventArgs) ' Handler on/off at runtime in builders.vb

        Try
            Dim tp As TabPage = tbc.SelectedTab

            If tp Is Nothing Then ' we're in Main_Load, not ready yet
                Exit Sub
            End If

            SwitchToTabPage(Model, tp)

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub

End Class
