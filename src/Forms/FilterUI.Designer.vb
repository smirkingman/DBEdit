<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FilterUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FilterUI))
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnFilter = New System.Windows.Forms.Button()
        Me.txtLabel = New System.Windows.Forms.TextBox()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.rtbHelp = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(151, 59)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(63, 23)
        Me.btnCancel.TabIndex = 11
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnFilter
        '
        Me.btnFilter.Location = New System.Drawing.Point(14, 59)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(63, 23)
        Me.btnFilter.TabIndex = 10
        Me.btnFilter.Text = "Filter"
        Me.btnFilter.UseVisualStyleBackColor = True
        '
        'txtLabel
        '
        Me.txtLabel.BackColor = System.Drawing.SystemColors.Control
        Me.txtLabel.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLabel.Location = New System.Drawing.Point(14, 14)
        Me.txtLabel.Name = "txtLabel"
        Me.txtLabel.Size = New System.Drawing.Size(200, 13)
        Me.txtLabel.TabIndex = 9
        Me.txtLabel.Text = "Label"
        '
        'txtValue
        '
        Me.txtValue.Location = New System.Drawing.Point(14, 33)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(200, 20)
        Me.txtValue.TabIndex = 12
        '
        'rtbHelp
        '
        Me.rtbHelp.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbHelp.Location = New System.Drawing.Point(12, 88)
        Me.rtbHelp.Name = "rtbHelp"
        Me.rtbHelp.ReadOnly = True
        Me.rtbHelp.Size = New System.Drawing.Size(202, 150)
        Me.rtbHelp.TabIndex = 13
        Me.rtbHelp.Text = ""
        '
        'FilterUI
        '
        Me.AcceptButton = Me.btnFilter
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(228, 242)
        Me.Controls.Add(Me.rtbHelp)
        Me.Controls.Add(Me.txtValue)
        Me.Controls.Add(Me.btnFilter)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.txtLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FilterUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "FilterUI"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtLabel As TextBox
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnFilter As Button
    Friend WithEvents txtValue As TextBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents rtbHelp As RichTextBox
End Class
