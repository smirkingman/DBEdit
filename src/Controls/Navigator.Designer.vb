<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Navigator
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.txtPosition = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRecords = New System.Windows.Forms.TextBox()
        Me.btnFirst = New System.Windows.Forms.Button()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnLast = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'txtPosition
        '
        Me.txtPosition.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPosition.Location = New System.Drawing.Point(48, 6)
        Me.txtPosition.Name = "txtPosition"
        Me.txtPosition.Size = New System.Drawing.Size(50, 13)
        Me.txtPosition.TabIndex = 2
        Me.txtPosition.Text = "0"
        Me.txtPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtPosition, "Enter the record number to go to")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(104, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "of"
        '
        'txtRecords
        '
        Me.txtRecords.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRecords.Location = New System.Drawing.Point(126, 6)
        Me.txtRecords.Name = "txtRecords"
        Me.txtRecords.ReadOnly = True
        Me.txtRecords.Size = New System.Drawing.Size(44, 13)
        Me.txtRecords.TabIndex = 4
        Me.txtRecords.TabStop = False
        Me.txtRecords.Text = "9999999"
        Me.ToolTip1.SetToolTip(Me.txtRecords, "The number of records in the entire table")
        '
        'btnFirst
        '
        Me.btnFirst.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnFirst.FlatAppearance.BorderSize = 0
        Me.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFirst.Image = Global.DBEdit.My.Resources.Resources.resultset_first
        Me.btnFirst.Location = New System.Drawing.Point(4, 4)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(16, 16)
        Me.btnFirst.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.btnFirst, "Move to the first record")
        Me.btnFirst.UseVisualStyleBackColor = True
        '
        'btnPrevious
        '
        Me.btnPrevious.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnPrevious.FlatAppearance.BorderSize = 0
        Me.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrevious.Image = Global.DBEdit.My.Resources.Resources.resultset_previous
        Me.btnPrevious.Location = New System.Drawing.Point(26, 4)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(16, 16)
        Me.btnPrevious.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.btnPrevious, "Move to the previous record")
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnNext.FlatAppearance.BorderSize = 0
        Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNext.Image = Global.DBEdit.My.Resources.Resources.resultset_next
        Me.btnNext.Location = New System.Drawing.Point(176, 4)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(16, 16)
        Me.btnNext.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.btnNext, "Move to the next record")
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnLast
        '
        Me.btnLast.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnLast.FlatAppearance.BorderSize = 0
        Me.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLast.Image = Global.DBEdit.My.Resources.Resources.resultset_last
        Me.btnLast.Location = New System.Drawing.Point(198, 4)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(16, 16)
        Me.btnLast.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.btnLast, "Move to the last record")
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnAdd.FlatAppearance.BorderSize = 0
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Image = Global.DBEdit.My.Resources.Resources.add
        Me.btnAdd.Location = New System.Drawing.Point(220, 4)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(16, 16)
        Me.btnAdd.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.btnAdd, "Move to the 'new' record, to add a new row to the table")
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'Navigator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnLast)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrevious)
        Me.Controls.Add(Me.btnFirst)
        Me.Controls.Add(Me.txtRecords)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPosition)
        Me.DoubleBuffered = True
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "Navigator"
        Me.Size = New System.Drawing.Size(242, 25)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtPosition As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtRecords As TextBox
    Friend WithEvents btnFirst As Button
    Friend WithEvents btnPrevious As Button
    Friend WithEvents btnNext As Button
    Friend WithEvents btnLast As Button
    Friend WithEvents btnAdd As Button
    Friend WithEvents ToolTip1 As ToolTip
End Class
