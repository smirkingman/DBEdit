<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EditUI
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditUI))
        Me.SplitVertical = New System.Windows.Forms.SplitContainer()
        Me.SplitHorizontal = New System.Windows.Forms.SplitContainer()
        Me.HexGrid = New System.Windows.Forms.DataGridView()
        Me.Offset = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Offset00 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Offset04 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Offset08 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Offset12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.txtError = New System.Windows.Forms.TextBox()
        Me.btnFormat = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        CType(Me.SplitVertical, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitVertical.Panel1.SuspendLayout()
        Me.SplitVertical.Panel2.SuspendLayout()
        Me.SplitVertical.SuspendLayout()
        CType(Me.SplitHorizontal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitHorizontal.Panel1.SuspendLayout()
        Me.SplitHorizontal.Panel2.SuspendLayout()
        Me.SplitHorizontal.SuspendLayout()
        CType(Me.HexGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitVertical
        '
        Me.SplitVertical.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitVertical.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitVertical.Location = New System.Drawing.Point(0, 0)
        Me.SplitVertical.Name = "SplitVertical"
        Me.SplitVertical.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitVertical.Panel1
        '
        Me.SplitVertical.Panel1.Controls.Add(Me.SplitHorizontal)
        '
        'SplitVertical.Panel2
        '
        Me.SplitVertical.Panel2.Controls.Add(Me.txtError)
        Me.SplitVertical.Panel2.Controls.Add(Me.btnFormat)
        Me.SplitVertical.Panel2.Controls.Add(Me.btnCancel)
        Me.SplitVertical.Panel2.Controls.Add(Me.btnUpdate)
        Me.SplitVertical.Panel2MinSize = 55
        Me.SplitVertical.Size = New System.Drawing.Size(561, 199)
        Me.SplitVertical.SplitterDistance = 143
        Me.SplitVertical.SplitterWidth = 1
        Me.SplitVertical.TabIndex = 4
        '
        'SplitHorizontal
        '
        Me.SplitHorizontal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitHorizontal.Location = New System.Drawing.Point(0, 0)
        Me.SplitHorizontal.Name = "SplitHorizontal"
        '
        'SplitHorizontal.Panel1
        '
        Me.SplitHorizontal.Panel1.Controls.Add(Me.HexGrid)
        '
        'SplitHorizontal.Panel2
        '
        Me.SplitHorizontal.Panel2.Controls.Add(Me.txtValue)
        Me.SplitHorizontal.Size = New System.Drawing.Size(561, 143)
        Me.SplitHorizontal.SplitterDistance = 318
        Me.SplitHorizontal.TabIndex = 0
        '
        'HexGrid
        '
        Me.HexGrid.AllowUserToResizeColumns = False
        Me.HexGrid.AllowUserToResizeRows = False
        Me.HexGrid.BackgroundColor = System.Drawing.SystemColors.Control
        Me.HexGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.HexGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.HexGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.HexGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.HexGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Offset, Me.Offset00, Me.Offset04, Me.Offset08, Me.Offset12})
        Me.HexGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HexGrid.EnableHeadersVisualStyles = False
        Me.HexGrid.Location = New System.Drawing.Point(0, 0)
        Me.HexGrid.MultiSelect = False
        Me.HexGrid.Name = "HexGrid"
        Me.HexGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Blue
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.HexGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.HexGrid.RowHeadersVisible = False
        Me.HexGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.HexGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.HexGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.HexGrid.Size = New System.Drawing.Size(318, 143)
        Me.HexGrid.TabIndex = 0
        '
        'Offset
        '
        Me.Offset.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Offset.DefaultCellStyle = DataGridViewCellStyle2
        Me.Offset.HeaderText = "    Offset"
        Me.Offset.MinimumWidth = 80
        Me.Offset.Name = "Offset"
        Me.Offset.ReadOnly = True
        Me.Offset.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Offset.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Offset.Width = 80
        '
        'Offset00
        '
        Me.Offset00.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Offset00.DefaultCellStyle = DataGridViewCellStyle3
        Me.Offset00.HeaderText = "0 00"
        Me.Offset00.MaxInputLength = 8
        Me.Offset00.MinimumWidth = 70
        Me.Offset00.Name = "Offset00"
        Me.Offset00.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Offset00.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Offset00.Width = 70
        '
        'Offset04
        '
        Me.Offset04.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Offset04.DefaultCellStyle = DataGridViewCellStyle4
        Me.Offset04.HeaderText = "4 04"
        Me.Offset04.MaxInputLength = 8
        Me.Offset04.MinimumWidth = 70
        Me.Offset04.Name = "Offset04"
        Me.Offset04.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Offset04.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Offset04.Width = 70
        '
        'Offset08
        '
        Me.Offset08.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Offset08.DefaultCellStyle = DataGridViewCellStyle5
        Me.Offset08.HeaderText = "8 08"
        Me.Offset08.MaxInputLength = 8
        Me.Offset08.MinimumWidth = 70
        Me.Offset08.Name = "Offset08"
        Me.Offset08.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Offset08.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Offset08.Width = 70
        '
        'Offset12
        '
        Me.Offset12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Offset12.DefaultCellStyle = DataGridViewCellStyle6
        Me.Offset12.HeaderText = "12 0C"
        Me.Offset12.MaxInputLength = 8
        Me.Offset12.MinimumWidth = 70
        Me.Offset12.Name = "Offset12"
        Me.Offset12.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Offset12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Offset12.Width = 70
        '
        'txtValue
        '
        Me.txtValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtValue.Location = New System.Drawing.Point(0, 0)
        Me.txtValue.Multiline = True
        Me.txtValue.Name = "txtValue"
        Me.txtValue.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtValue.Size = New System.Drawing.Size(239, 143)
        Me.txtValue.TabIndex = 0
        '
        'txtError
        '
        Me.txtError.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtError.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtError.ForeColor = System.Drawing.Color.Red
        Me.txtError.Location = New System.Drawing.Point(0, 5)
        Me.txtError.Name = "txtError"
        Me.txtError.ReadOnly = True
        Me.txtError.Size = New System.Drawing.Size(561, 13)
        Me.txtError.TabIndex = 4
        Me.txtError.TabStop = False
        Me.txtError.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnFormat
        '
        Me.btnFormat.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnFormat.Location = New System.Drawing.Point(235, 30)
        Me.btnFormat.Name = "btnFormat"
        Me.btnFormat.Size = New System.Drawing.Size(61, 23)
        Me.btnFormat.TabIndex = 1
        Me.btnFormat.Text = "&Format"
        Me.btnFormat.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(494, 30)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(61, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnUpdate.Location = New System.Drawing.Point(8, 30)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(61, 23)
        Me.btnUpdate.TabIndex = 0
        Me.btnUpdate.Text = "&Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'EditUI
        '
        Me.AcceptButton = Me.btnUpdate
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(561, 199)
        Me.Controls.Add(Me.SplitVertical)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EditUI"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Edit"
        Me.SplitVertical.Panel1.ResumeLayout(False)
        Me.SplitVertical.Panel2.ResumeLayout(False)
        Me.SplitVertical.Panel2.PerformLayout()
        CType(Me.SplitVertical, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitVertical.ResumeLayout(False)
        Me.SplitHorizontal.Panel1.ResumeLayout(False)
        Me.SplitHorizontal.Panel2.ResumeLayout(False)
        Me.SplitHorizontal.Panel2.PerformLayout()
        CType(Me.SplitHorizontal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitHorizontal.ResumeLayout(False)
        CType(Me.HexGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitVertical As SplitContainer
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnFormat As Button
    Friend WithEvents SplitHorizontal As SplitContainer
    Friend WithEvents HexGrid As DataGridView
    Friend WithEvents txtValue As TextBox
    Friend WithEvents txtError As TextBox
    Friend WithEvents Offset As DataGridViewTextBoxColumn
    Friend WithEvents Offset00 As DataGridViewTextBoxColumn
    Friend WithEvents Offset04 As DataGridViewTextBoxColumn
    Friend WithEvents Offset08 As DataGridViewTextBoxColumn
    Friend WithEvents Offset12 As DataGridViewTextBoxColumn
End Class
