<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StatisticsUI
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StatisticsUI))
        Me.bntClose = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCount = New System.Windows.Forms.TextBox()
        Me.txtNulls = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCountDistinct = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMinimum = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtMaximum = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAverage = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtSum = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtStdDev = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtVariance = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtError = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'bntClose
        '
        Me.bntClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.bntClose.Location = New System.Drawing.Point(89, 230)
        Me.bntClose.Name = "bntClose"
        Me.bntClose.Size = New System.Drawing.Size(61, 23)
        Me.bntClose.TabIndex = 0
        Me.bntClose.Text = "&Close"
        Me.bntClose.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(38, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Count(*)"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCount
        '
        Me.txtCount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCount.Location = New System.Drawing.Point(89, 11)
        Me.txtCount.Name = "txtCount"
        Me.txtCount.ReadOnly = True
        Me.txtCount.Size = New System.Drawing.Size(140, 13)
        Me.txtCount.TabIndex = 5
        Me.txtCount.TabStop = False
        Me.txtCount.Text = "txtCount"
        '
        'txtNulls
        '
        Me.txtNulls.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNulls.Location = New System.Drawing.Point(89, 49)
        Me.txtNulls.Name = "txtNulls"
        Me.txtNulls.ReadOnly = True
        Me.txtNulls.Size = New System.Drawing.Size(140, 13)
        Me.txtNulls.TabIndex = 7
        Me.txtNulls.TabStop = False
        Me.txtNulls.Text = "txtNulls"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(53, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Nulls"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCountDistinct
        '
        Me.txtCountDistinct.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCountDistinct.Location = New System.Drawing.Point(89, 30)
        Me.txtCountDistinct.Name = "txtCountDistinct"
        Me.txtCountDistinct.ReadOnly = True
        Me.txtCountDistinct.Size = New System.Drawing.Size(140, 13)
        Me.txtCountDistinct.TabIndex = 9
        Me.txtCountDistinct.TabStop = False
        Me.txtCountDistinct.Text = "txtCountDistinct"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Count Distinct"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMinimum
        '
        Me.txtMinimum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMinimum.Location = New System.Drawing.Point(89, 68)
        Me.txtMinimum.Name = "txtMinimum"
        Me.txtMinimum.ReadOnly = True
        Me.txtMinimum.Size = New System.Drawing.Size(140, 13)
        Me.txtMinimum.TabIndex = 11
        Me.txtMinimum.TabStop = False
        Me.txtMinimum.Text = "txtMinimum"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(35, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Minimum"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMaximum
        '
        Me.txtMaximum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMaximum.Location = New System.Drawing.Point(89, 87)
        Me.txtMaximum.Name = "txtMaximum"
        Me.txtMaximum.ReadOnly = True
        Me.txtMaximum.Size = New System.Drawing.Size(140, 13)
        Me.txtMaximum.TabIndex = 13
        Me.txtMaximum.TabStop = False
        Me.txtMaximum.Text = "txtMaximum"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(32, 87)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Maximum"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAverage
        '
        Me.txtAverage.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAverage.Location = New System.Drawing.Point(89, 106)
        Me.txtAverage.Name = "txtAverage"
        Me.txtAverage.ReadOnly = True
        Me.txtAverage.Size = New System.Drawing.Size(140, 13)
        Me.txtAverage.TabIndex = 15
        Me.txtAverage.TabStop = False
        Me.txtAverage.Text = "txtAverage"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(36, 106)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Average"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSum
        '
        Me.txtSum.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSum.Location = New System.Drawing.Point(89, 125)
        Me.txtSum.Name = "txtSum"
        Me.txtSum.ReadOnly = True
        Me.txtSum.Size = New System.Drawing.Size(140, 13)
        Me.txtSum.TabIndex = 17
        Me.txtSum.TabStop = False
        Me.txtSum.Text = "txtSum"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(55, 125)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(28, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Sum"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtStdDev
        '
        Me.txtStdDev.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStdDev.Location = New System.Drawing.Point(89, 144)
        Me.txtStdDev.Name = "txtStdDev"
        Me.txtStdDev.ReadOnly = True
        Me.txtStdDev.Size = New System.Drawing.Size(140, 13)
        Me.txtStdDev.TabIndex = 19
        Me.txtStdDev.TabStop = False
        Me.txtStdDev.Text = "txtStdDev"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(40, 144)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 13)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "StdDev"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVariance
        '
        Me.txtVariance.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtVariance.Location = New System.Drawing.Point(89, 163)
        Me.txtVariance.Name = "txtVariance"
        Me.txtVariance.ReadOnly = True
        Me.txtVariance.Size = New System.Drawing.Size(140, 13)
        Me.txtVariance.TabIndex = 21
        Me.txtVariance.TabStop = False
        Me.txtVariance.Text = "txtVariance"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(34, 163)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(49, 13)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "Variance"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtError
        '
        Me.txtError.BackColor = System.Drawing.SystemColors.Control
        Me.txtError.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtError.ForeColor = System.Drawing.Color.Red
        Me.txtError.Location = New System.Drawing.Point(13, 180)
        Me.txtError.Multiline = True
        Me.txtError.Name = "txtError"
        Me.txtError.Size = New System.Drawing.Size(215, 44)
        Me.txtError.TabIndex = 22
        Me.txtError.Text = "txtError"
        '
        'StatisticsUI
        '
        Me.AcceptButton = Me.bntClose
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.bntClose
        Me.ClientSize = New System.Drawing.Size(246, 263)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtError)
        Me.Controls.Add(Me.txtVariance)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtStdDev)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtSum)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtAverage)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtMaximum)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtMinimum)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtCountDistinct)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtNulls)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCount)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.bntClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "StatisticsUI"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Statistics (Unfiltered)"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bntClose As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtCount As TextBox
    Friend WithEvents txtNulls As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtCountDistinct As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtMinimum As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtMaximum As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtAverage As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtSum As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtStdDev As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtVariance As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents txtError As TextBox
End Class
