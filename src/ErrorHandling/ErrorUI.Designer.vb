<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ErrorUI
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose( disposing As Boolean)
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ErrorUI))
        Me.txtDetails = New System.Windows.Forms.RichTextBox()
        Me.btnContinue = New System.Windows.Forms.Button()
        Me.btnClipboard = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtDetails
        '
        Me.txtDetails.BackColor = System.Drawing.SystemColors.Control
        Me.txtDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDetails.Location = New System.Drawing.Point(11, 12)
        Me.txtDetails.Name = "txtDetails"
        Me.txtDetails.ReadOnly = True
        Me.txtDetails.Size = New System.Drawing.Size(328, 137)
        Me.txtDetails.TabIndex = 0
        Me.txtDetails.TabStop = False
        Me.txtDetails.Text = ""
        '
        'btnContinue
        '
        Me.btnContinue.Location = New System.Drawing.Point(11, 186)
        Me.btnContinue.Name = "btnContinue"
        Me.btnContinue.Size = New System.Drawing.Size(90, 35)
        Me.btnContinue.TabIndex = 1
        Me.btnContinue.Text = "Continue"
        Me.btnContinue.UseVisualStyleBackColor = True
        '
        'btnClipboard
        '
        Me.btnClipboard.Location = New System.Drawing.Point(246, 186)
        Me.btnClipboard.Name = "btnClipboard"
        Me.btnClipboard.Size = New System.Drawing.Size(90, 35)
        Me.btnClipboard.TabIndex = 2
        Me.btnClipboard.Text = "Copy to clipboard"
        Me.btnClipboard.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 161)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(331, 16)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "If you think that this needs a fix, open an issue on Github"
        '
        'ErrorUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(348, 233)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnClipboard)
        Me.Controls.Add(Me.btnContinue)
        Me.Controls.Add(Me.txtDetails)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ErrorUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DBEdit Error"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtDetails As System.Windows.Forms.RichTextBox
    Friend WithEvents btnContinue As System.Windows.Forms.Button
    Friend WithEvents btnClipboard As System.Windows.Forms.Button
    Friend WithEvents Label1 As Label
End Class
