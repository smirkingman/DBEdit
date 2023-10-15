Public Class LoginUI
    Private ReadOnly Property World As World
    Sub New(world As World)

        InitializeComponent()

        _World = world

        Status.Text = ""

        Dim recent As Recent = world.MostRecent

        If recent Is Nothing Then

            cmbServer.Text = ""
            cmbInstance.Text = ""
            cmbDatabase.Text = ""
            txtUser.Text = ""
            txtPassword.Text = ""
            rbWindows.Checked = True

        Else
            _Server = recent.Server
            _Database = recent.Database

            cmbServer.Text = server.SimpleName
            cmbInstance.Text = server.InstanceName
            cmbDatabase.Text = database.Name
            txtUser.Text = server.User
            txtPassword.Text = server.Password
            rbWindows.Checked = String.IsNullOrWhiteSpace(txtUser.Text)

        End If

        rbSQL.Checked = Not rbWindows.Checked

    End Sub
    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click

        If Server Is Nothing Then
            Fail("Server is mandatory")
            cmbServer.Focus()
            Exit Sub
        End If

        If Database Is Nothing Then
            Fail("Database is mandatory")
            cmbDatabase.Focus()
            Exit Sub
        End If

        Try
            btnOpen.Enabled = False
            Cursor = Cursors.WaitCursor

            If rbWindows.Checked Then
                Server.User = ""
                Server.Password = ""
            Else
                If Not isEmpty(txtUser.Text) Then
                    Server.User = txtUser.Text
                    Server.Password = txtPassword.Text
                End If
            End If

            Dim message As String = ""

            If Server.TryAuthenticate(Database, message) Then

                World.State.AddRecent(Server, Database)
                DialogResult = DialogResult.OK
                Me.Close()

            Else
                Fail($"Login failed {message}")
                DialogResult = DialogResult.None
            End If

        Finally
            Cursor = Cursors.Default
            btnOpen.Enabled = True

        End Try

    End Sub
    Private Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click

        DialogResult = DialogResult.Cancel
        Me.Close()

    End Sub
    Private Sub cmb_GotFocus(sender As Object, e As EventArgs) Handles _
            cmbServer.GotFocus, cmbInstance.GotFocus, cmbDatabase.GotFocus

        DirectCast(sender, ComboBox).SelectAll()

    End Sub
    Private Sub cmb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles _
            cmbServer.KeyPress, cmbInstance.KeyPress, cmbDatabase.KeyPress

        If Char.IsLower(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If

    End Sub
    Private Sub cmbDatabase_DropDown(sender As Object, e As EventArgs) Handles cmbDatabase.DropDown

        Try
            Dim databases As String() =
                Server.
                Databases.
                Select(Function(q) q.Name).
                ToArray

            If databases.Any Then
                cmbDatabase.Items.Clear()
                cmbDatabase.Items.AddRange(databases)
            End If

        Catch

        End Try

    End Sub
    Private Sub cmbDatabase_Validated(sender As Object, e As EventArgs) Handles cmbDatabase.Validated

        _Database = Server.Database(cmbDatabase.Text)

    End Sub
    Private Sub cmbServer_DropDown(sender As Object, e As EventArgs) Handles cmbServer.DropDown

        Dim servers As String() =
            World.
            Servers.
            Select(Function(q) q.SimpleName).
            Distinct.
            OrderBy(Function(q) q).
            ToArray

        If servers.Any Then
            cmbServer.Items.Clear()
            cmbServer.Items.AddRange(servers)
        End If

    End Sub
    Private Sub cmbServer_Validated(sender As Object, e As EventArgs) Handles cmbServer.Validated

        _Server = World.Server(cmbServer.Text, "")

        Dim instances As String() =
            World.
            Servers.
            Where(Function(q) q.SimpleName = cmbServer.Text).
            Select(Function(q) q.InstanceName).
            Distinct.
            OrderBy(Function(q) q).
            ToArray

        cmbInstance.Items.Clear()

        If instances.Any Then
            If instances.Count < 2 Then
                cmbInstance.Enabled = False
                cmbInstance.TabStop = False
                cmbInstance.Text = If(instances.Count = 0, "", instances.First)
                cmbDatabase.Focus()
            Else
                cmbInstance.Items.AddRange(instances)
                cmbInstance.Enabled = True
                cmbInstance.TabStop = True
                cmbInstance.Focus()
            End If
        End If

    End Sub
    Public ReadOnly Property Database As Database
    Private Sub Fail(msg As String)

        Status.ForeColor = Color.Red
        Status.Text = msg
        StatusStrip1.Invalidate()

    End Sub
    Public ReadOnly Property Server As Server
    Private Sub txt_GotFocus(sender As Object, e As EventArgs) Handles _
             txtUser.GotFocus, txtPassword.GotFocus

        DirectCast(sender, TextBox).SelectAll()

    End Sub

    Private Sub txtUser_Validated(sender As Object, e As EventArgs) Handles _
        txtUser.Validated, txtPassword.Validated

        rbSQL.Checked = Not isEmpty(txtUser.Text) OrElse Not isEmpty(txtPassword.Text)

    End Sub
End Class
