Public Module Aargh_

    Public Sub Aargh()

        If My.Settings.Aargh Then
            My.Computer.Audio.Play(My.Resources.injuryscream, AudioPlayMode.Background)
        End If

    End Sub

End Module
