
Imports NLog

Public Class Context

    Public Sub New(grid As Grid, row As Integer, col As Integer)

        _Grid = grid
        _RowIndex = row
        _ColumnIndex = col

    End Sub
    Public ReadOnly Property Column As Column
        Get
            Return Table.Columns(ColumnIndex)
        End Get
    End Property
    Public ReadOnly Property ColumnIndex As Integer
    Public ReadOnly Property Grid As Grid
    Private _ToolStripMenuItem As ToolStripMenuItem
    Public Property ToolStripMenuItem As ToolStripMenuItem
        Get
#If DEBUG Then
            If _ToolStripMenuItem Is Nothing Then
                Debug.WriteLine("Context.ToolStripMenuItem is nothing")
                Stop
            End If
#End If
            Return _ToolStripMenuItem
        End Get
        Set(value As ToolStripMenuItem)
            _ToolStripMenuItem = value
        End Set
    End Property
    Public ReadOnly Property Location As Point
        Get
            ' Originally Parent.Location.PointToScreen(ToolStripMenuItem.Bounds.Location),
            ' the location of the item he just clicked.
            ' It's much nicer to align to the parent menu item, where he chose this sub-menu.
            Return Parent.Location
        End Get
    End Property
    Public Logger As Logger = LogManager.GetCurrentClassLogger
    Public ReadOnly Property Parent As ToolStripDropDownMenu
        Get
            Return DirectCast(ToolStripMenuItem.GetCurrentParent, ToolStripDropDownMenu)
        End Get
    End Property
    Public ReadOnly Property RowIndex As Integer
    Public ReadOnly Property Table As Table
        Get
            Return Grid.Table
        End Get
    End Property
    Public Overrides Function ToString() As String

        Dim tbl As String = IfEmpty(Table?.Name, "[no table]")
        Dim vlu As String = IfEmpty(Value, "[Nothing]")

        Return $"Table {tbl} Row {RowIndex} Column {ColumnIndex} Value {vlu}"

    End Function
#If DEBUG Then
    Public Sub Trace(isnew As String)

        Dim tn As String = "[no table]"
        If Table IsNot Nothing Then
            tn = Table.Name
        End If

        Dim cn As String = "[no column]"
        If ColumnIndex >= 0 Then
            cn = Column.Name
        End If

        Dim vl As String = "[index out-of-range]"
        If RowIndex >= 0 AndAlso _ColumnIndex >= 0 Then
            vl = IfEmpty(Grid.Value(RowIndex, ColumnIndex), "[no value]")
        End If

        Logger.Trace($"{isnew}context from {Caller(3)} for {tn}.{cn} Row {_RowIndex} Col {_ColumnIndex} Value {vl}")

    End Sub
#End If

    Public Property Value As Object
        Get
            Return Grid.Value(RowIndex, ColumnIndex)
        End Get
        Set(value As Object)
            Grid.Value(RowIndex, ColumnIndex) = value
        End Set
    End Property
    Public Property Verb As String = ""

End Class
