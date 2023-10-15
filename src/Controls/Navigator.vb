Imports System.ComponentModel

Public Class Navigator

    Public Event MoveFirst()
    Public Event MovePrevious()
    Public Event MoveNext()
    Public Event MoveLast()
    Public Event MoveAdd()
    Public Event MoveTo(index As Integer)
    Public Event Message(msg As String)

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        RaiseEvent MoveAdd()

    End Sub

    Private Sub btnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click

        RaiseEvent MoveFirst()

    End Sub

    Private Sub btnLast_Click(sender As Object, e As EventArgs) Handles btnLast.Click

        RaiseEvent MoveLast()

    End Sub
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

        RaiseEvent MoveNext()

    End Sub
    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click

        RaiseEvent MovePrevious()

    End Sub
    Private Sub txtPosition_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPosition.KeyDown

        If e.KeyCode = Keys.Enter Then
            txtPosition_Validating(sender, New CancelEventArgs)
        End If

    End Sub
    Private Sub txtPosition_Validating(sender As Object, e As CancelEventArgs) Handles txtPosition.Validating

        Dim value As Integer
        ' We force number formatting to use quote as thousands separator
        If Not Integer.TryParse(txtPosition.Text.Replace("'", ""), value) Then
            RaiseEvent Message($"Invalid number {txtPosition.Text}")
            e.Cancel = True
            Exit Sub
        End If

        RaiseEvent MoveTo(value)

    End Sub

End Class
