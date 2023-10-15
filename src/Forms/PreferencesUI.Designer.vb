<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PreferencesUI
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PreferencesUI))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbStartup = New System.Windows.Forms.ComboBox()
        Me.chkTooltips = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtMaxColWidth = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkArgh = New System.Windows.Forms.CheckBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(72, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "At startup"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbStartup
        '
        Me.cmbStartup.FormattingEnabled = True
        Me.cmbStartup.Items.AddRange(New Object() {"Do nothing", "Open previous database", "Show open dialog"})
        Me.cmbStartup.Location = New System.Drawing.Point(130, 12)
        Me.cmbStartup.Name = "cmbStartup"
        Me.cmbStartup.Size = New System.Drawing.Size(168, 21)
        Me.cmbStartup.TabIndex = 7
        '
        'chkTooltips
        '
        Me.chkTooltips.AutoSize = True
        Me.chkTooltips.Location = New System.Drawing.Point(130, 39)
        Me.chkTooltips.Name = "chkTooltips"
        Me.chkTooltips.Size = New System.Drawing.Size(15, 14)
        Me.chkTooltips.TabIndex = 10
        Me.chkTooltips.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(54, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Show tooltips"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(116, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Maximum column width"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMaxColWidth
        '
        Me.txtMaxColWidth.Location = New System.Drawing.Point(130, 59)
        Me.txtMaxColWidth.Name = "txtMaxColWidth"
        Me.txtMaxColWidth.Size = New System.Drawing.Size(61, 20)
        Me.txtMaxColWidth.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.txtMaxColWidth, "Column autosize maximum width. 100..2000 Default 300")
        '
        'chkArgh
        '
        Me.chkArgh.AutoSize = True
        Me.chkArgh.Location = New System.Drawing.Point(130, 88)
        Me.chkArgh.Name = "chkArgh"
        Me.chkArgh.Size = New System.Drawing.Size(15, 14)
        Me.chkArgh.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.chkArgh, "When an error occurs, play the Aargh! sound")
        Me.chkArgh.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(116, 117)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(64, 23)
        Me.btnClose.TabIndex = 14
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(83, 89)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Aargh!!"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(151, 89)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(115, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "when there's a SNAFU"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PreferencesUI
        '
        Me.AcceptButton = Me.btnClose
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(310, 147)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.chkArgh)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.txtMaxColWidth)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.chkTooltips)
        Me.Controls.Add(Me.cmbStartup)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PreferencesUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Preferences"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbStartup As ComboBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents chkTooltips As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtMaxColWidth As TextBox
    Friend WithEvents btnClose As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents chkArgh As CheckBox
    Friend WithEvents Label6 As Label
End Class
