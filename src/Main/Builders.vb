Imports System.Data.SqlClient

Imports DBEdit.Model

Partial Public Class MainUI

    Private Sub AddTabs(tbc As TabControl)

        tbc.TabPages.AddRange(Model.
                              Tables.
                              Select(Function(q)
                                         Dim result As New TabPage(q.Name) With {
                                             .Name = q.Name
                                         }
                                         Return result
                                     End Function).
                              ToArray)

        TabPages = tbc.TabPages.OfType(Of TabPage).ToList

    End Sub
    Private Sub AddTablesMenu()

        For Each tsm1 As ToolStripMenuItem In TableToolStripMenuItem.DropDownItems
            RemoveHandler tsm1.Click, AddressOf ChangeTabpage_MenuItem_Click
            For Each tsm2 As ToolStripMenuItem In tsm1.DropDownItems
                RemoveHandler tsm2.Click, AddressOf ChangeTabpage_MenuItem_Click
            Next
            tsm1.DropDownItems.Clear()
        Next
        TableToolStripMenuItem.DropDownItems.Clear()

        Const FANOUT As Integer = 10

        Dim names As List(Of String) = Model.
                                       Tables.
                                       Select(Function(q) q.Name).
                                       OrderBy(Function(q) q).
                                       ToList

        Dim namecount As Integer = names.Count

        If namecount < 2 * FANOUT Then ' Simple list

            Dim items As New List(Of ToolStripMenuItem)

            For Each name As String In names

                Dim item As New ToolStripMenuItem(name)
                item.Tag = tbc.TabPages(name)
                items.Add(item)
                AddHandler item.Click, AddressOf ChangeTabpage_MenuItem_Click

            Next

            TableToolStripMenuItem.DropDownItems.AddRange(items.ToArray)

        Else ' 2-level list

            Dim chunk As Integer = namecount \ FANOUT
            Dim level1 As New List(Of ToolStripMenuItem)

            Do
                Dim subset As List(Of String) = names.Take(chunk).ToList

                names = names.Skip(chunk).ToList

                Dim label As String = $"{subset.First} {RIGHTARROW} {subset.Last}"

                Dim item1 As New ToolStripMenuItem(label)
                level1.Add(item1)

                Dim level2 As New List(Of ToolStripMenuItem)

                For Each name As String In subset

                    Dim item2 As New ToolStripMenuItem(name)
                    item2.Tag = tbc.TabPages(name)
                    level2.Add(item2)

                    AddHandler item2.Click, AddressOf ChangeTabpage_MenuItem_Click

                    item1.DropDownItems.AddRange(level2.ToArray)

                Next

            Loop Until Not names.Any

            TableToolStripMenuItem.DropDownItems.AddRange(level1.ToArray)
        End If

    End Sub
    Sub DGVDefaults(grid As Grid)

        grid.DataSource = Nothing
        grid.Rows.Clear()
        grid.Columns.Clear()
        grid.AutoGenerateColumns = False
        grid.AllowUserToAddRows = True
        grid.AllowUserToDeleteRows = False
        grid.AllowUserToOrderColumns = False
        grid.AllowUserToResizeColumns = True
        grid.AllowUserToResizeRows = True
        grid.BorderStyle = BorderStyle.None
        ' grid.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
        grid.SelectionMode = DataGridViewSelectionMode.CellSelect
        grid.MultiSelect = True
        grid.EnableHeadersVisualStyles = False ' We'll be doing our own styling, thank you

    End Sub
    Sub DGVHandlers(grid As Grid)

        AddHandler grid.CellFormatting, AddressOf dgv_CellFormatting
        AddHandler grid.CellToolTipTextNeeded, AddressOf dgv_CellToolTipTextNeeded
        AddHandler grid.CellValueNeeded, AddressOf dgv_CellValueNeeded
        AddHandler grid.CellValuePushed, AddressOf dgv_CellValuePushed
        AddHandler grid.ColumnHeaderMouseClick, AddressOf dgv_ColumnHeaderMouseClick
        AddHandler grid.CellClick, AddressOf dgv_CellClick
        AddHandler grid.DataError, AddressOf dgv_DataError
        AddHandler grid.KeyDown, AddressOf dgv_KeyDown
        AddHandler grid.CellContextMenuStripNeeded, AddressOf dgv_CellContextMenuStripNeeded
        AddHandler grid.Refreshed, AddressOf ShowRowCounts
        AddHandler grid.RowDirtyStateNeeded, AddressOf dgv_RowDirtyStateNeeded
        AddHandler grid.RowValidating, AddressOf dgv_RowValidating
        AddHandler grid.SelectionChanged, AddressOf dgv_SelectionChanged

    End Sub
    Sub MakeDropdown(model As Model, column As Column, ByRef col As GridCol, ByRef combo As DataGridViewComboBoxCell)

        Try
            combo = New DataGridViewComboBoxCell

            combo.Style.BackColor = Color.White
            combo.Style.ForeColor = Color.Black

            col = New GridCol With {
                            .MinimumWidth = 30,
                            .Width = 30,
                            .DataPropertyName = column.Name
                        }

            Dim primarykey As Column = column.Lookup
            Dim primary As Table = primarykey.Table

            Dim selector As String = primary.Selector.Name

            Dim query As String = $"SELECT {Box(primarykey.Name)}, {Box(selector)} AS text " &
                                  $"FROM {Box(primary.Name)} ORDER BY 2"

            Using conn As SqlConnection = model.Connection

                Dim adapter As New SqlDataAdapter(query, conn)
                Dim dataset As New DataSet

                adapter.Fill(dataset, column.Name)

                combo.DataSource = dataset.Tables(column.Name)
                combo.DisplayMember = "text"
                combo.ValueMember = column.Primary.Name
                combo.MaxDropDownItems = 100 ' the maximum

                Dim longest As Integer = 5 ' characters

                For Each row As DataRow In dataset.
                                           Tables(column.Name).
                                           Rows.
                                           OfType(Of DataRow).
                                           Take(50) ' Could be millions!

                    Dim val As String = CStr(row("text"))
                    If val.Length > longest Then
                        longest = val.Length
                    End If
                Next

                ' Find widest item in combo
                Dim g As Graphics = Me.CreateGraphics
                g.PageUnit = GraphicsUnit.Pixel
                Dim biggest As SizeF = g.MeasureString(New String("X"c, longest), Me.Font)
                If biggest.Width > col.Width Then
                    col.MinimumWidth = CInt(biggest.Width)


                End If
                combo.DropDownWidth = CInt(biggest.Width)
                combo.FlatStyle = FlatStyle.Flat ' https://stackoverflow.com/a/31953509/338101

            End Using

            col.CellTemplate = combo

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Sub SetupColumns(model As Model, grid As Grid, table As Table)

        Try
            Using conn As SqlConnection = model.Connection

                conn.Open()

                For Each column As Column In table.Columns

                    If column.Name = Cache.ROWNUMBER Then
                        Continue For
                    End If

                    Dim col As GridCol = Nothing
                    Dim template As DataGridViewCell = Nothing
                    Dim align As DataGridViewContentAlignment = DataGridViewContentAlignment.MiddleCenter
                    Dim minwidth As Integer = 20

                    If column.isLookup Then

                        Dim combocol As GridCol = Nothing
                        Dim combocell As DataGridViewComboBoxCell = Nothing

                        MakeDropdown(model, column, combocol, combocell)

                        col = combocol
                        template = combocell

                        align = DataGridViewContentAlignment.MiddleLeft

                    Else
                        Select Case FundamentalTypeOf(column.Datatype)

                            Case Fundamental.date

                                template = New CalendarCell
                                align = DataGridViewContentAlignment.MiddleCenter
                                col = New CalendarColumn()
                                col.DefaultCellStyle.Format = Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern
                                minwidth = 70

                            Case Fundamental.time

                                template = New DataGridViewTextBoxCell
                                align = DataGridViewContentAlignment.MiddleCenter
                                col = New GridCol
                                col.DefaultCellStyle.Format = Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.LongTimePattern

                                '' https://stackoverflow.com/a/3629130/338101
                                'Dim tc As New TimeCell
                                'col = New TimeColumn(tc)
                                'template = tc
                                minwidth = 70

                            Case Fundamental.bit

                                template = New DataGridViewCheckBoxCell
                                align = DataGridViewContentAlignment.MiddleCenter
                                col = New GridCol

                            Case Fundamental.string, Fundamental.binary

                                template = New DataGridViewTextBoxCell
                                align = DataGridViewContentAlignment.MiddleLeft
                                col = New GridCol

                            Case Fundamental.double, Fundamental.integer, Fundamental.decimal, Fundamental.long

                                template = New DataGridViewTextBoxCell
                                align = DataGridViewContentAlignment.MiddleRight
                                col = New GridCol

                            Case Fundamental.offset

                                template = New DataGridViewTextBoxCell
                                align = DataGridViewContentAlignment.MiddleLeft
                                col = New GridCol

                            Case Fundamental.special

                                template = New DataGridViewTextBoxCell

                                ' In theory. In practice it is impossible to create a 'rowversion' column,
                                ' it becomes a timestamp and there's nothing you can do about it.
                                ' https://dba.stackexchange.com/a/86531/76518
                                If column.Datatype = SQLType.timestamp Then
                                    align = DataGridViewContentAlignment.MiddleRight
                                Else
                                    align = DataGridViewContentAlignment.MiddleLeft
                                End If

                                col = New GridCol

                            Case Else
                                Throw New Exception($"Column {column.Name} unknwown underlying {FundamentalTypeOf(column.Datatype)} for {column.Datatype}")

                        End Select
                    End If

                    template.Style.Alignment = align
                    col.MinimumWidth = minwidth
                    col.HeaderCell.Style.Alignment = align
                    col.Name = column.Name
                    col.DataPropertyName = column.Name
                    col.CellTemplate = template
                    col.SortMode = DataGridViewColumnSortMode.Programmatic

                    If Not column.Updateable Then
                        col.ReadOnly = True
                        col.HeaderCell.Style.BackColor = Color.FromArgb(245, 245, 245)
                        template.Style.BackColor = col.HeaderCell.Style.BackColor
                    End If

                    grid.Columns.Add(col)

                Next

                conn.Close()

            End Using

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Sub SetupTabControl(tbc As TabControl)

        tbc.DrawMode = TabDrawMode.OwnerDrawFixed

        tbc.ShowToolTips = True

        RemoveHandler tbc.SelectedIndexChanged, AddressOf tbc_SelectedIndexChanged ' otherwise the 'clear' will trigger a change

        tbc.Controls.Clear()

    End Sub

End Class

