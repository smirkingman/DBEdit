<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainUI
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainUI))
        Me.cmsTabs = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.HideTabToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsMainMenu = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RecentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.QuitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyToClipboardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PreferencesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.TableToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PreviousToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NextToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CollapseTabsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.tbc = New System.Windows.Forms.TabControl()
        Me.StatusStripPanel = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.pbProgress = New System.Windows.Forms.ToolStripProgressBar()
        Me.slSelection = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.slStatus = New System.Windows.Forms.ToolStripLabel()
        Me.Navigator1 = New DBEdit.Navigator()
        Me.cmsRows = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteRowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsColumns = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SortToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AscendingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DescendingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnsortedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HideColumnsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnhideColumnsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsCells = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditFieldToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FilterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EqualsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NotEqualsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LessThanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GreaterThanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContainsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LikeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NullToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NotNullToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartsWithToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearallToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatisticsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmsTabs.SuspendLayout()
        Me.cmsMainMenu.SuspendLayout()
        Me.FileContextMenuStrip.SuspendLayout()
        Me.EditContextMenuStrip.SuspendLayout()
        Me.ViewContextMenuStrip.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.StatusStripPanel.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.cmsRows.SuspendLayout()
        Me.cmsColumns.SuspendLayout()
        Me.cmsCells.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmsTabs
        '
        Me.cmsTabs.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HideTabToolStripMenuItem, Me.ShowAllToolStripMenuItem})
        Me.cmsTabs.Name = "cmsTBC"
        Me.cmsTabs.Size = New System.Drawing.Size(120, 48)
        '
        'HideTabToolStripMenuItem
        '
        Me.HideTabToolStripMenuItem.Name = "HideTabToolStripMenuItem"
        Me.HideTabToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.HideTabToolStripMenuItem.Text = "&Hide tab"
        '
        'ShowAllToolStripMenuItem
        '
        Me.ShowAllToolStripMenuItem.Name = "ShowAllToolStripMenuItem"
        Me.ShowAllToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.ShowAllToolStripMenuItem.Text = "&Show all"
        '
        'cmsMainMenu
        '
        Me.cmsMainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ViewToolStripMenuItem})
        Me.cmsMainMenu.Location = New System.Drawing.Point(0, 0)
        Me.cmsMainMenu.Name = "cmsMainMenu"
        Me.cmsMainMenu.ShowItemToolTips = True
        Me.cmsMainMenu.Size = New System.Drawing.Size(967, 24)
        Me.cmsMainMenu.TabIndex = 0
        Me.cmsMainMenu.TabStop = True
        Me.cmsMainMenu.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDown = Me.FileContextMenuStrip
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'FileContextMenuStrip
        '
        Me.FileContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem, Me.RecentToolStripMenuItem, Me.QuitToolStripMenuItem})
        Me.FileContextMenuStrip.Name = "FileContextMenuStrip"
        Me.FileContextMenuStrip.OwnerItem = Me.FileToolStripMenuItem
        Me.FileContextMenuStrip.Size = New System.Drawing.Size(155, 70)
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.OpenToolStripMenuItem.Text = "&Open ..."
        Me.OpenToolStripMenuItem.ToolTipText = "Open a database"
        '
        'RecentToolStripMenuItem
        '
        Me.RecentToolStripMenuItem.Name = "RecentToolStripMenuItem"
        Me.RecentToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.RecentToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.RecentToolStripMenuItem.Text = "&Recent"
        '
        'QuitToolStripMenuItem
        '
        Me.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem"
        Me.QuitToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.QuitToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.QuitToolStripMenuItem.Text = "&Quit"
        Me.QuitToolStripMenuItem.ToolTipText = "End the program"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDown = Me.EditContextMenuStrip
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "&Edit"
        Me.EditToolStripMenuItem.ToolTipText = "edit tooltip"
        '
        'EditContextMenuStrip
        '
        Me.EditContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyToClipboardToolStripMenuItem, Me.CopyToExcelToolStripMenuItem, Me.PreferencesToolStripMenuItem})
        Me.EditContextMenuStrip.Name = "EditContextMenuStrip"
        Me.EditContextMenuStrip.OwnerItem = Me.EditToolStripMenuItem
        Me.EditContextMenuStrip.Size = New System.Drawing.Size(212, 70)
        '
        'CopyToClipboardToolStripMenuItem
        '
        Me.CopyToClipboardToolStripMenuItem.Name = "CopyToClipboardToolStripMenuItem"
        Me.CopyToClipboardToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyToClipboardToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.CopyToClipboardToolStripMenuItem.Text = "&Copy to clipboard"
        Me.CopyToClipboardToolStripMenuItem.ToolTipText = "To copy all cells, select a single cell. To copy a subset, click the top-left cel" &
    "l and Control-click to additionally select the bottom right cell."
        '
        'CopyToExcelToolStripMenuItem
        '
        Me.CopyToExcelToolStripMenuItem.Name = "CopyToExcelToolStripMenuItem"
        Me.CopyToExcelToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.CopyToExcelToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.CopyToExcelToolStripMenuItem.Text = "Copy to &Excel"
        Me.CopyToExcelToolStripMenuItem.ToolTipText = "To copy all cells, select a single cell. To copy a subset, click the top-left cel" &
    "l and Control-click to additionally select the bottom right cell."
        '
        'PreferencesToolStripMenuItem
        '
        Me.PreferencesToolStripMenuItem.Name = "PreferencesToolStripMenuItem"
        Me.PreferencesToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PreferencesToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.PreferencesToolStripMenuItem.Text = "&Preferences"
        Me.PreferencesToolStripMenuItem.ToolTipText = "Change your preferences"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDown = Me.ViewContextMenuStrip
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.ViewToolStripMenuItem.Text = "&View"
        '
        'ViewContextMenuStrip
        '
        Me.ViewContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TableToolStripMenuItem, Me.PreviousToolStripMenuItem, Me.NextToolStripMenuItem, Me.RefreshToolStripMenuItem, Me.CollapseTabsToolStripMenuItem})
        Me.ViewContextMenuStrip.Name = "ViewContextMenuStrip"
        Me.ViewContextMenuStrip.OwnerItem = Me.ViewToolStripMenuItem
        Me.ViewContextMenuStrip.Size = New System.Drawing.Size(164, 114)
        '
        'TableToolStripMenuItem
        '
        Me.TableToolStripMenuItem.Name = "TableToolStripMenuItem"
        Me.TableToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.TableToolStripMenuItem.Text = "&Table"
        Me.TableToolStripMenuItem.ToolTipText = "Switch to a tab from the sub-list"
        '
        'PreviousToolStripMenuItem
        '
        Me.PreviousToolStripMenuItem.Name = "PreviousToolStripMenuItem"
        Me.PreviousToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3
        Me.PreviousToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.PreviousToolStripMenuItem.Text = "&Previous"
        Me.PreviousToolStripMenuItem.ToolTipText = "Switch to the previous tab"
        '
        'NextToolStripMenuItem
        '
        Me.NextToolStripMenuItem.Name = "NextToolStripMenuItem"
        Me.NextToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4
        Me.NextToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.NextToolStripMenuItem.Text = "&Next"
        Me.NextToolStripMenuItem.ToolTipText = "Switch to the next tab"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.RefreshToolStripMenuItem.Text = "&Refresh"
        Me.RefreshToolStripMenuItem.ToolTipText = "Get fresh data from the database"
        '
        'CollapseTabsToolStripMenuItem
        '
        Me.CollapseTabsToolStripMenuItem.Name = "CollapseTabsToolStripMenuItem"
        Me.CollapseTabsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6
        Me.CollapseTabsToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.CollapseTabsToolStripMenuItem.Text = "&Collapse tabs"
        Me.CollapseTabsToolStripMenuItem.ToolTipText = "Collapse/Restore all tabs"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 24)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.tbc)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.StatusStripPanel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Navigator1)
        Me.SplitContainer1.Panel2MinSize = 24
        Me.SplitContainer1.Size = New System.Drawing.Size(967, 526)
        Me.SplitContainer1.SplitterDistance = 500
        Me.SplitContainer1.SplitterWidth = 1
        Me.SplitContainer1.TabIndex = 4
        Me.SplitContainer1.TabStop = False
        '
        'tbc
        '
        Me.tbc.ContextMenuStrip = Me.cmsTabs
        Me.tbc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbc.HotTrack = True
        Me.tbc.Location = New System.Drawing.Point(0, 0)
        Me.tbc.Margin = New System.Windows.Forms.Padding(0)
        Me.tbc.Multiline = True
        Me.tbc.Name = "tbc"
        Me.tbc.Padding = New System.Drawing.Point(0, 0)
        Me.tbc.SelectedIndex = 0
        Me.tbc.Size = New System.Drawing.Size(967, 500)
        Me.tbc.TabIndex = 1
        '
        'StatusStripPanel
        '
        Me.StatusStripPanel.Controls.Add(Me.ToolStrip1)
        Me.StatusStripPanel.Location = New System.Drawing.Point(245, 3)
        Me.StatusStripPanel.Name = "StatusStripPanel"
        Me.StatusStripPanel.Size = New System.Drawing.Size(589, 20)
        Me.StatusStripPanel.TabIndex = 5
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator2, Me.pbProgress, Me.slSelection, Me.ToolStripSeparator1, Me.slStatus})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(589, 20)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 20)
        '
        'pbProgress
        '
        Me.pbProgress.Name = "pbProgress"
        Me.pbProgress.Size = New System.Drawing.Size(100, 17)
        '
        'slSelection
        '
        Me.slSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.slSelection.Name = "slSelection"
        Me.slSelection.Size = New System.Drawing.Size(63, 17)
        Me.slSelection.Text = "slSelection"
        Me.slSelection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.slSelection.ToolTipText = "Displays the current cell selection"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 20)
        '
        'slStatus
        '
        Me.slStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.slStatus.Name = "slStatus"
        Me.slStatus.Size = New System.Drawing.Size(47, 17)
        Me.slStatus.Text = "slStatus"
        Me.slStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.slStatus.ToolTipText = "Displays messages"
        '
        'Navigator1
        '
        Me.Navigator1.Location = New System.Drawing.Point(0, 0)
        Me.Navigator1.Margin = New System.Windows.Forms.Padding(0)
        Me.Navigator1.Name = "Navigator1"
        Me.Navigator1.Size = New System.Drawing.Size(242, 25)
        Me.Navigator1.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.Navigator1, "Navigate by entering a record number or clicking the butttons")
        '
        'cmsRows
        '
        Me.cmsRows.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteRowToolStripMenuItem})
        Me.cmsRows.Name = "cmsRows"
        Me.cmsRows.Size = New System.Drawing.Size(108, 26)
        '
        'DeleteRowToolStripMenuItem
        '
        Me.DeleteRowToolStripMenuItem.Name = "DeleteRowToolStripMenuItem"
        Me.DeleteRowToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.DeleteRowToolStripMenuItem.Text = "&Delete"
        '
        'cmsColumns
        '
        Me.cmsColumns.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SortToolStripMenuItem, Me.HideColumnsToolStripMenuItem, Me.UnhideColumnsToolStripMenuItem})
        Me.cmsColumns.Name = "cmsColumns"
        Me.cmsColumns.Size = New System.Drawing.Size(181, 92)
        '
        'SortToolStripMenuItem
        '
        Me.SortToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AscendingToolStripMenuItem, Me.DescendingToolStripMenuItem, Me.UnsortedToolStripMenuItem})
        Me.SortToolStripMenuItem.Name = "SortToolStripMenuItem"
        Me.SortToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SortToolStripMenuItem.Text = "&Sort"
        '
        'AscendingToolStripMenuItem
        '
        Me.AscendingToolStripMenuItem.Name = "AscendingToolStripMenuItem"
        Me.AscendingToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.AscendingToolStripMenuItem.Text = "&Ascending"
        '
        'DescendingToolStripMenuItem
        '
        Me.DescendingToolStripMenuItem.Name = "DescendingToolStripMenuItem"
        Me.DescendingToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.DescendingToolStripMenuItem.Text = "&Descending"
        '
        'UnsortedToolStripMenuItem
        '
        Me.UnsortedToolStripMenuItem.Name = "UnsortedToolStripMenuItem"
        Me.UnsortedToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.UnsortedToolStripMenuItem.Text = "&Unsorted"
        '
        'HideColumnsToolStripMenuItem
        '
        Me.HideColumnsToolStripMenuItem.Name = "HideColumnsToolStripMenuItem"
        Me.HideColumnsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.HideColumnsToolStripMenuItem.Text = "&Hide"
        '
        'UnhideColumnsToolStripMenuItem
        '
        Me.UnhideColumnsToolStripMenuItem.Name = "UnhideColumnsToolStripMenuItem"
        Me.UnhideColumnsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.UnhideColumnsToolStripMenuItem.Text = "&Unhide"
        '
        'cmsCells
        '
        Me.cmsCells.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditFieldToolStripMenuItem, Me.FilterToolStripMenuItem, Me.StatisticsToolStripMenuItem})
        Me.cmsCells.Name = "cmsCells"
        Me.cmsCells.Size = New System.Drawing.Size(121, 70)
        '
        'EditFieldToolStripMenuItem
        '
        Me.EditFieldToolStripMenuItem.Name = "EditFieldToolStripMenuItem"
        Me.EditFieldToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.EditFieldToolStripMenuItem.Text = "&Edit"
        '
        'FilterToolStripMenuItem
        '
        Me.FilterToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EqualsToolStripMenuItem, Me.NotEqualsToolStripMenuItem, Me.LessThanToolStripMenuItem, Me.GreaterThanToolStripMenuItem, Me.ContainsToolStripMenuItem, Me.InToolStripMenuItem, Me.LikeToolStripMenuItem, Me.NullToolStripMenuItem, Me.NotNullToolStripMenuItem, Me.StartsWithToolStripMenuItem, Me.ClearToolStripMenuItem, Me.ClearallToolStripMenuItem})
        Me.FilterToolStripMenuItem.Name = "FilterToolStripMenuItem"
        Me.FilterToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.FilterToolStripMenuItem.Text = "&Filter"
        '
        'EqualsToolStripMenuItem
        '
        Me.EqualsToolStripMenuItem.Name = "EqualsToolStripMenuItem"
        Me.EqualsToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.EqualsToolStripMenuItem.Text = "&Equals"
        '
        'NotEqualsToolStripMenuItem
        '
        Me.NotEqualsToolStripMenuItem.Name = "NotEqualsToolStripMenuItem"
        Me.NotEqualsToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.NotEqualsToolStripMenuItem.Text = "N&ot equals"
        '
        'LessThanToolStripMenuItem
        '
        Me.LessThanToolStripMenuItem.Name = "LessThanToolStripMenuItem"
        Me.LessThanToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.LessThanToolStripMenuItem.Text = "&Less than"
        '
        'GreaterThanToolStripMenuItem
        '
        Me.GreaterThanToolStripMenuItem.Name = "GreaterThanToolStripMenuItem"
        Me.GreaterThanToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.GreaterThanToolStripMenuItem.Text = "&Greater than"
        '
        'ContainsToolStripMenuItem
        '
        Me.ContainsToolStripMenuItem.Name = "ContainsToolStripMenuItem"
        Me.ContainsToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.ContainsToolStripMenuItem.Text = "Con&tains"
        '
        'InToolStripMenuItem
        '
        Me.InToolStripMenuItem.Name = "InToolStripMenuItem"
        Me.InToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.InToolStripMenuItem.Text = "&In"
        '
        'LikeToolStripMenuItem
        '
        Me.LikeToolStripMenuItem.Name = "LikeToolStripMenuItem"
        Me.LikeToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.LikeToolStripMenuItem.Text = "Li&ke"
        '
        'NullToolStripMenuItem
        '
        Me.NullToolStripMenuItem.Name = "NullToolStripMenuItem"
        Me.NullToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.NullToolStripMenuItem.Text = "&Null"
        '
        'NotNullToolStripMenuItem
        '
        Me.NotNullToolStripMenuItem.Name = "NotNullToolStripMenuItem"
        Me.NotNullToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.NotNullToolStripMenuItem.Text = "Not n&ull"
        '
        'StartsWithToolStripMenuItem
        '
        Me.StartsWithToolStripMenuItem.Name = "StartsWithToolStripMenuItem"
        Me.StartsWithToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.StartsWithToolStripMenuItem.Text = "&Starts with"
        '
        'ClearToolStripMenuItem
        '
        Me.ClearToolStripMenuItem.Name = "ClearToolStripMenuItem"
        Me.ClearToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.ClearToolStripMenuItem.Text = "&Clear"
        '
        'ClearallToolStripMenuItem
        '
        Me.ClearallToolStripMenuItem.Name = "ClearallToolStripMenuItem"
        Me.ClearallToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.ClearallToolStripMenuItem.Text = "Clear &all"
        '
        'StatisticsToolStripMenuItem
        '
        Me.StatisticsToolStripMenuItem.Name = "StatisticsToolStripMenuItem"
        Me.StatisticsToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.StatisticsToolStripMenuItem.Text = "S&tatistics"
        '
        'MainUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(967, 550)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.cmsMainMenu)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.cmsMainMenu
        Me.Name = "MainUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "DBEdit"
        Me.cmsTabs.ResumeLayout(False)
        Me.cmsMainMenu.ResumeLayout(False)
        Me.cmsMainMenu.PerformLayout()
        Me.FileContextMenuStrip.ResumeLayout(False)
        Me.EditContextMenuStrip.ResumeLayout(False)
        Me.ViewContextMenuStrip.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.StatusStripPanel.ResumeLayout(False)
        Me.StatusStripPanel.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.cmsRows.ResumeLayout(False)
        Me.cmsColumns.ResumeLayout(False)
        Me.cmsCells.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmsMainMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsTabs As ContextMenuStrip
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents tbc As TabControl
    Friend WithEvents Navigator1 As Navigator
    Friend WithEvents StatusStripPanel As Panel
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents pbProgress As ToolStripProgressBar
    Friend WithEvents slSelection As ToolStripLabel
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents slStatus As ToolStripLabel
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents cmsRows As ContextMenuStrip
    Friend WithEvents DeleteRowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents cmsColumns As ContextMenuStrip
    Friend WithEvents cmsCells As ContextMenuStrip
    Friend WithEvents SortToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AscendingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DescendingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UnsortedToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HideColumnsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UnhideColumnsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditFieldToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FilterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EqualsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NotEqualsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LessThanToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GreaterThanToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContainsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LikeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NullToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NotNullToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StartsWithToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClearToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClearallToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StatisticsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HideTabToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents EditContextMenuStrip As ContextMenuStrip
    Friend WithEvents CopyToClipboardToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopyToExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PreferencesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FileContextMenuStrip As ContextMenuStrip
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RecentToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents QuitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewContextMenuStrip As ContextMenuStrip
    Friend WithEvents TableToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PreviousToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NextToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CollapseTabsToolStripMenuItem As ToolStripMenuItem
End Class
