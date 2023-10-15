Imports System.Globalization
Imports System.Threading
Imports NLog
Imports NLog.Config
Imports DBEdit.Model
Imports NLog.Targets
Imports NLog.Layouts
Imports System.Media
Imports System.IO
Imports System.Resources

Public Class MainUI

    ' Turn on double buffering at the form level because TabControl doesn't support it.
    ' Reduces flicker when we set the ToolTips on the tabs.
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property
    Sub New()

        InitializeComponent()

        Dim config As LoggingConfiguration = LogManager.Configuration

        ' Adjust logging configuration
        ' For debugging, log everything to the debug window
        ' For release, no deb logging
        ' In both, file logging for error and worse

        Dim debugrule As LoggingRule = config.FindRuleByName("debug")
        Dim debugtarget As DebuggerTarget = config.FindTargetByName(Of DebuggerTarget)("debug")
#If DEBUG Then
        debugrule.SetLoggingLevels(LogLevel.Trace, LogLevel.Fatal)
        debugtarget.Layout = New SimpleLayout("${date:format=HH\:mm\:ss.fff} ${message} | " &
            "${level: uppercase =true} ${callsite:className=true:methodName=true} ${exception:format=tostring}")
#Else
        debugrule.DisableLoggingForLevels(LogLevel.trace, LogLevel.Fatal)
        debugtarget.Layout = New SimpleLayout("${date:format=HH\:mm\:ss.fff} ${message} ${exception:format=tostring}")
#End If
        LogManager.Configuration = config
        LogManager.GetCurrentClassLogger()
        Logger.Trace("Starting")

        Application.EnableVisualStyles() ' So that ProgressBar marquee moves

        pbProgress.Visible = False
        slSelection.Text = ""
        slStatus.Text = ""
        ToolStrip1.BackColor = ToolStrip1.BackColor ' https://stackoverflow.com/a/56596351/338101

        Dim newCultureDefinition As CultureInfo = CType(Thread.CurrentThread.CurrentCulture.Clone(), CultureInfo)
        newCultureDefinition.NumberFormat.PercentPositivePattern = 1 ' 123% and not 123 %
        ' comma and dot have poor readability, e.g. when displaying statistics
        newCultureDefinition.NumberFormat.CurrencyGroupSeparator = "'"
        newCultureDefinition.NumberFormat.PercentGroupSeparator = "'"
        newCultureDefinition.NumberFormat.NumberGroupSeparator = "'"
        Thread.CurrentThread.CurrentCulture = newCultureDefinition

        ToolTip1.OwnerDraw = True

    End Sub
    Private Sub DeleteSelectedCells(sender As Object, e As EventArgs)

        Try
            Dim grid As Grid = DirectCast(sender, Grid)

            Dim msg As String = "this cell"

            If grid.SelectedCells.Count > 1 Then
                msg = "these " & grid.SelectedCells.Count & " cells"
            End If

            If ShowBox("Are you sure you want to clear " & msg & "?" & vbCrLf & "This cannot be undone!",
                 "Confirm delete", MessageBoxButtons.YesNo) <> MsgBoxResult.Yes Then
                Exit Sub
            End If

            For Each cell As DataGridViewCell In grid.SelectedCells

                Dim context As Context = GetContext(cell.RowIndex, cell.ColumnIndex)

                If context.RowIndex >= context.Table.RowCount Then ' Trying to delete cells in the new column. Idiot.
                    SetStatus("There is nothing to delete in the 'new' row", Severity.Warning)
                    Exit Sub
                End If

                context.Value = Nothing
                Dim ea As New DataGridViewCellCancelEventArgs(cell.ColumnIndex, cell.RowIndex)
                dgv_RowValidating(sender, ea)

            Next

            SetStatus($"{Grid.SelectedCells.Count} cells cleared", Severity.Info)

        Catch ex As Exception
            ShowBox(ex.Message, "Delete failed",, MessageBoxIcon.Error)
        End Try


    End Sub
    Private Sub DeleteSelectedRows(sender As Object, e As EventArgs)

        Try
            Dim context As Context = GetContext()
            Dim grid As Grid = context.Grid

            Dim msg As String = "this record"

            If grid.SelectedRows.Count > 1 Then
                msg = "these " & grid.SelectedRows.Count & " records"
            End If

            If ShowBox("Are you sure you want to delete " & msg & "?" & vbCrLf & "This cannot be undone!",
                 "Confirm delete", MessageBoxButtons.YesNo) <> MsgBoxResult.Yes Then
                Exit Sub
            End If

            Dim deleted As Integer = 0

            For Each row As DataGridViewRow In grid.SelectedRows.OfType(Of DataGridViewRow).ToList

                Dim where As String = String.Join(" and ",
                    grid.
                    Table.
                    PrimaryKeys.
                    Select(Function(q) Box(q.Name) & "=" & CStr(Model.ToSQL(row.Cells(q.Name).Value, q.Datatype))))

                Dim query As String = "delete from " & Box(grid.Table.Name) & " where " & where

                deleted += Model.ExecuteNonQuery(query)
            Next

            If deleted = 0 Then
                SetStatus("No rows deleted", Severity.Error)
            Else
                SetStatus($"{deleted} rows deleted", Severity.Info)
            End If

            grid.Invalidate()

        Catch ex As Exception
            ShowBox(ex.Message, "Delete failed",, MessageBoxIcon.Error)
        End Try

    End Sub
    Public Function GetContext() As Context

        Dim tp As TabPage = tbc.SelectedTab
        Dim grid As Grid = tp.Controls.OfType(Of Grid).Single
        Dim row As Integer = -1
        Dim col As Integer = -1

        Dim cell As DataGridViewCell = grid.CurrentCell

        If cell IsNot Nothing Then
            row = cell.RowIndex
            col = cell.ColumnIndex
        End If

        Return GetContext(row, col)

    End Function
    Public Function GetContext(row As Integer, col As Integer, Optional newvalue As Object = Nothing) As Context

        Dim tp As TabPage = tbc.SelectedTab
        Dim grid As Grid = tp.Controls.OfType(Of Grid).Single
        Dim result As Context = Nothing

#If DEBUG Then
        Dim fresh As String = "reuse "
#End If


        If row >= 0 AndAlso col >= 0 Then

            result = grid.Table.Columns(col).Context

            If result IsNot Nothing Then
                If result.RowIndex <> row OrElse result.ColumnIndex <> col Then
                    result = Nothing
                End If
            End If
        End If

        If result Is Nothing Then
            result = New Context(grid, row, col)
#If DEBUG Then
            fresh = "new "
#End If
        End If

        If newvalue IsNot Nothing Then
            result.Value = newvalue
        End If

        If col >= 0 Then
            result.Column.Context = result
        End If

#If DEBUG Then
        result.Trace(fresh)
#End If

        Return result

    End Function
    Public Logger As Logger = LogManager.GetCurrentClassLogger
    Public ReadOnly Property Model As Model
    Sub Open(server As Server, database As Database)

        Try
            Application.UseWaitCursor = True
            tbc.Visible = False

            ProgressStart(0, "Opening...")

            _Model = New Model(server, database)

            Using semaphore As New AutoResetEvent(False)

                Task.Run(
                    Sub()
                        Try
                            AddHandler Model.Progress, AddressOf ProgressUpdate

                            Model.Load()

                            RemoveHandler Model.Progress, AddressOf ProgressUpdate

                        Catch ex As Exception
                            HandleError(ex)

                        Finally
                            semaphore.Set()
                        End Try
                    End Sub)

                Do
                    Application.DoEvents()
                Loop Until semaphore.WaitOne(100)

            End Using

            SetupTabControl(tbc)

            AddTabs(tbc)

            AddTablesMenu()

            AddHandler tbc.SelectedIndexChanged, AddressOf tbc_SelectedIndexChanged

            SwitchToTabPage(Model, tbc.TabPages(0))

            Me.Text = $"DBedit : {database.Name} on {server.Name}"

            ProgressFinish("Ready")

        Finally
            tbc.Visible = True
            Application.UseWaitCursor = False

        End Try
    End Sub
    Private Sub PopulateRecent()

        For Each old As ToolStripMenuItem In RecentToolStripMenuItem.DropDownItems
            RemoveHandler old.Click, AddressOf Recent_Click
        Next

        RecentToolStripMenuItem.DropDownItems.Clear()

        RecentToolStripMenuItem.
            DropDownItems.
            AddRange(World.
                     Recent.
                     Select(Function(q)
                                Dim result As New ToolStripMenuItem(q.Name)
                                result.Tag = q
                                AddHandler result.Click, AddressOf Recent_Click
                                Return result
                            End Function).
                     ToArray)

    End Sub
    Private Sub ProgressFinish(text As String)

        Invoke(
            Sub()
                pbProgress.Visible = False
                slStatus.Text = text
                ToolStrip1.Invalidate()
            End Sub)

    End Sub
    Private Sub ProgressMade(value As Integer)

        Invoke(
            Sub()
                pbProgress.Value = value
                ToolStrip1.Invalidate()
            End Sub)

    End Sub
    Private Sub ProgressStart(maximum As Integer, text As String)

        Invoke(
            Sub()
                If maximum = 0 Then
                    pbProgress.Style = ProgressBarStyle.Marquee
                    pbProgress.MarqueeAnimationSpeed = 50
                Else
                    pbProgress.Style = ProgressBarStyle.Continuous
                    pbProgress.Maximum = maximum
                    pbProgress.Value = 0
                End If
                pbProgress.Visible = True
                slStatus.Text = text
                ToolStrip1.Invalidate()
            End Sub)

    End Sub
    Private Sub ProgressUpdate(sender As Object, text As String)

        Invoke(
            Sub()
                slStatus.Text = text
                ToolStrip1.Invalidate()
            End Sub)

    End Sub
    Sub SetStatus(msg As String, Optional severity As Severity = Severity.Info)

        Invoke(Sub()
                   Dim colour As Color
                   Select Case severity
                       Case Severity.Info
                           colour = Color.Black
                       Case Severity.Warning
                           colour = Color.Blue
                       Case Severity.Error
                           colour = Color.Red
                           Aargh()
                   End Select
                   slStatus.ForeColor = colour
                   slStatus.Text = msg
                   ToolStrip1.Invalidate()
               End Sub)

    End Sub
    Sub SetStatus(e As MessageEventArgs)

        SetStatus(e.Message, e.Severity)

    End Sub
    Sub SetStatus(sender As Object, e As MessageEventArgs)

        SetStatus(e.Message, e.Severity)

    End Sub
    Sub ShowRowCounts()

        Try
            Dim tp As TabPage = tbc.SelectedTab
            If tp Is Nothing Then
                Exit Sub
            End If
            Dim grid As Grid = tp.Controls.OfType(Of Grid).Single
            Dim table As Table = grid.Table

            Dim rowindex As Integer = -1
            Dim colindex As Integer = -1

            If grid.CurrentCell IsNot Nothing Then
                rowindex = grid.CurrentCell.RowIndex
                colindex = grid.CurrentCell.ColumnIndex
            End If

            If rowindex >= 0 AndAlso colindex >= 0 Then

                Dim selected As String

                If rowindex >= table.RowCount Then
                    selected = "New row"
                Else
                    selected = $"{grid.SelectedCells.Count:#,##0} cells selected"

                    If grid.SelectedRows.Count > 0 Then
                        selected = $"{grid.SelectedRows.Count:#,##0} rows selected"
                    End If
                End If

                Dim filters As String = String.Join(" AND ",
                                            table.
                                            Columns.
                                            Where(Function(q) Not isEmpty(q.Where)).
                                            Select(Function(q) q.Where).
                                            OrderBy(Function(q) q))

                Dim filtered As String = If(filters = "", "", $", filtered on {filters}")

                Dim hiddencount As Integer = grid.Columns.OfType(Of DataGridViewColumn).Where(Function(q) Not q.Visible).Count
                Dim hidden As String = If(hiddencount = 0, "", $", {hiddencount} hidden columns")

                Dim msg As String = $"{selected}{filtered}{hidden}"

                Navigator1.txtPosition.Text = $"{rowindex + 1:#,##0}"  ' +1 = number, not offset
                Navigator1.txtRecords.Text = $"{table.RowCount:#,##0}"

                slSelection.Text = msg
                ToolStrip1.Invalidate()

            Else
                slSelection.Text = "Nothing selected"
            End If

            ToolStrip1.Invalidate()

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Public Sub SortTabs()

        ' Already sorted?
        If tbc.
           TabPages.
           OfType(Of TabPage).
           OrderBy(Function(q) q.Name).
           SequenceEqual(tbc.TabPages.OfType(Of TabPage)) Then

            Exit Sub
        End If

        ' Add tab pages in one fell swoop; awful flickering otherwise
        Dim tps As TabPage() = tbc.TabPages.OfType(Of TabPage).OrderBy(Function(q) q.Text).ToArray
        tbc.TabPages.Clear()
        tbc.TabPages.AddRange(tps)

    End Sub
    WithEvents bs As BindingSource
    Sub SwitchToTabPage(model As Model, tp As TabPage)

        Try
            If Not tbc.TabPages.Contains(tp) Then ' this tab page is collapsed, reinstate it
                tbc.TabPages.Add(tp)
                SortTabs()
            End If

            Dim table As Table = model.Tables.Where(Function(q) q.Name = tp.Name).Single

            Dim grid As Grid = tp.Controls.OfType(Of Grid).SingleOrDefault

            If grid Is Nothing Then ' First time on this tab, create the grid

                grid = New Grid(model, table)
                DGVDefaults(grid)
                SetupColumns(model, grid, table)
                DGVHandlers(grid)

                tp.Controls.Add(grid)

                grid.Invalidate()

                SortTabs()

                tbc.SelectedTab = tp ' Switch only once tabs are sorted

            Else
                tbc.SelectedTab = tp
                ShowRowCounts() ' We don't refresh when switching to a populated tab

            End If

            If table.RowCount = 0 Then
                SetStatus("Table is empty", Severity.Warning)
            Else
                SetStatus($"")
            End If

        Catch ex As Exception
            HandleError(ex)
        End Try

    End Sub
    Public Property TabPages As List(Of TabPage)
    Private Sub _World_Notify(sender As Object, e As MessageEventArgs) Handles _World.Notify

        SetStatus(e)

    End Sub
    Private WithEvents _World As World
    Public ReadOnly Property World As World
        Get
            Return _World
        End Get
    End Property

End Class
