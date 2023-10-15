Imports System.ComponentModel
Imports System.Globalization
Imports System.Reflection
Partial Public Class PreferencesUI

    Public Sub New()

        InitializeComponent()

    End Sub
    Private Sub PreferencesUI_Load(sender As Object, e As EventArgs) Handles Me.Load

        cmbStartup.SelectedItem = My.Settings.Startup
        chkTooltips.Checked = My.Settings.Tooltips
        txtMaxColWidth.Text = My.Settings.MaxColWidth.ToString
        chkArgh.Checked = My.Settings.Aargh

    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click

        My.Settings.Startup = cmbStartup.SelectedItem.ToString
        My.Settings.Tooltips = chkTooltips.Checked
        My.Settings.MaxColWidth = CInt(txtMaxColWidth.Text)
        My.Settings.Aargh = chkArgh.Checked
        My.Settings.Save()

        Me.Close()

    End Sub
    Private Sub txtMaxColWidth_Validating(sender As Object, e As CancelEventArgs) Handles txtMaxColWidth.Validating

        Dim v As Integer

        If Not Integer.TryParse(txtMaxColWidth.Text, v) Then
            ShowBox("Maximum column width is not numeric")
            e.Cancel = True
            Exit Sub
        End If

        If Not Between(v, 100, 2000) Then
            ShowBox("Invalid Maximum column width")
            e.Cancel = True
            Exit Sub
        End If

    End Sub
End Class