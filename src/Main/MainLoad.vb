Imports DBEdit.Model

Imports MoreLinq
Partial Public Class MainUI

    Private Sub Main_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Try
            If My.Settings.StartupWidth > 0 Then
                Me.Bounds = New Rectangle(My.Settings.StartupX, My.Settings.StartupY, My.Settings.StartupWidth, My.Settings.StartupHeight)
            End If

            _World = New World

            AddHandler _World.Notify, AddressOf SetStatus

            World.Initialise()

            AddHandler My.Application.Shutdown,
                Sub()
                    World.Save()
                    My.Settings.StartupX = Me.Left
                    My.Settings.StartupY = Me.Top
                    My.Settings.StartupWidth = Me.Width
                    My.Settings.StartupHeight = Me.Height
                    My.Settings.Save()
                End Sub

            Show()


            BeginInvoke(
                Sub()
                    Try
                        Main_ResizeEnd(sender, e)

                        Select Case My.Settings.Startup.Substring(0, 1)

                            Case "D" ' Do nothing

                            Case "O" ' Open previous database
                                Dim recent As Recent = World.MostRecent
                                If recent IsNot Nothing Then
                                    Open(recent.Server, recent.Database)
                                End If

                            Case "S" ' Show open dialog
                                OpenToolStripMenuItem_Click(sender, e)

                        End Select

                    Catch ex As Exception
                        HandleError(ex)
                    End Try
                End Sub)

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub

End Class
