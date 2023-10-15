''' <summary>
''' A datagridview column that contains <see cref="TimeCell" />s
''' http://msdn.microsoft.com/en-us/library/7tas5c80(v=vs.100).aspx
''' </summary>
''' <remarks></remarks>
Public Class TimeColumn
    Inherits GridCol

    Public Sub New(cell As TimeCell)
        MyBase.New(cell)
    End Sub

    Public Overrides Property CellTemplate() As DataGridViewCell
        Get
            Return MyBase.CellTemplate
        End Get
        Set(value As DataGridViewCell)

            ' Ensure that the cell used for the template is a TimeCell. 
            If (value IsNot Nothing) AndAlso _
                Not value.GetType().IsAssignableFrom(GetType(TimeCell)) _
                Then
                Throw New InvalidCastException("Must be a TimeCell")
            End If
            MyBase.CellTemplate = value

        End Set
    End Property

End Class
''' <summary>
''' A datagridview cell that contains a date and provides a date-picker
''' </summary>
''' <remarks></remarks>
Public Class TimeCell
    Inherits DataGridViewTextBoxCell

    Public Sub New()
        Me.Style.Format = Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.LongTimePattern
    End Sub

    Public Overrides Sub InitializeEditingControl(rowIndex As Integer, _
         initialToFormatted As Object, _
         dataGridViewCellStyle As DataGridViewCellStyle)

        ' Set the value of the editing control to the current cell value. 
        MyBase.InitializeEditingControl(rowIndex, initialToFormatted, _
            dataGridViewCellStyle)

        Dim ctl As TimeEditingControl = CType(DataGridView.EditingControl, TimeEditingControl)

        ctl.ShowUpDown = True

        ' Use the default row value when Value property is null. 
        If IsDBNull(Me.Value) Then
            ctl.Value = CType(Me.DefaultNewRowValue, DateTime)
        Else
            Try
                ctl.Value = CType(Me.Value, DateTime)
            Catch ex As Exception
                ctl.Value = Windows.Forms.DateTimePicker.MinimumDateTime
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Describes what a calendar cell is edited with
    ''' </summary>
    ''' <returns>GetType(TimeEditingControl)</returns>
    ''' <remarks></remarks>
    Public Overrides ReadOnly Property EditType() As Type
        Get
            ' Return the type of the editing control that TimeCell uses. 
            Return GetType(TimeEditingControl)
        End Get
    End Property
    ''' <summary>
    ''' Describes what sort of value type a calendar cell is
    ''' </summary>
    ''' <returns>GetType(DateTime)</returns>
    ''' <remarks></remarks>
    Public Overrides ReadOnly Property ValueType() As Type
        Get
            ' Return the type of the value that TimeCell contains. 
            Return GetType(TimeSpan)
        End Get
    End Property
    ''' <summary>
    ''' The default value for a calendar cell is now
    ''' </summary>
    ''' <returns>DateTime.Now</returns>
    ''' <remarks></remarks>
    Public Overrides ReadOnly Property DefaultNewRowValue() As Object
        Get
            ' Use the current date and time as the default value. 
            Return Nothing
        End Get
    End Property

End Class

''' <summary>
''' A calendar dropdown control
''' </summary>
''' <remarks></remarks>
Class TimeEditingControl
    Inherits Windows.Forms.DateTimePicker
    Implements IDataGridViewEditingControl

    Private dataGridViewControl As DataGridView
    Private valueIsChanged As Boolean = False
    Private rowIndexNum As Integer

    Public Sub New()
        Format = DateTimePickerFormat.Custom
        CustomFormat = Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.LongTimePattern
        ShowUpDown = True
    End Sub

    Public Property EditingControlToFormatted() As Object _
        Implements IDataGridViewEditingControl.EditingControlFormattedValue

        Get
            Return String.Format(CustomFormat, Me.Value)
        End Get

        Set(value As Object)
            Try
                ' This will throw an exception of the string is  
                ' null, empty, or not in the format of a date. 
                Me.Value = DateTime.Parse(CStr(value))
            Catch
                ' In the case of an exception, just use the default 
                ' value so we're not left with a null value. 
                Me.Value = DateTime.Now
            End Try
        End Set

    End Property

    Public Function GetEditingControlToFormatted(context _
        As DataGridViewDataErrorContexts) As Object _
        Implements IDataGridViewEditingControl.GetEditingControlFormattedValue

        Return String.Format(CustomFormat, Me.Value)

    End Function

    Public Sub ApplyCellStyleToEditingControl(dataGridViewCellStyle As _
        DataGridViewCellStyle) _
        Implements IDataGridViewEditingControl.ApplyCellStyleToEditingControl

        Me.Font = dataGridViewCellStyle.Font
        Me.CalendarForeColor = dataGridViewCellStyle.ForeColor
        Me.CalendarMonthBackground = dataGridViewCellStyle.BackColor

    End Sub

    Public Property EditingControlRowIndex() As Integer _
        Implements IDataGridViewEditingControl.EditingControlRowIndex

        Get
            Return rowIndexNum
        End Get
        Set(value As Integer)
            rowIndexNum = value
        End Set

    End Property

    Public Function EditingControlWantsInputKey(key As Keys, _
         dataGridViewWantsInputKey As Boolean) As Boolean _
        Implements IDataGridViewEditingControl.EditingControlWantsInputKey

        ' Let the DateTimePicker handle the keys listed. 
        Select Case key And Keys.KeyCode
            Case Keys.Left, Keys.Up, Keys.Down, Keys.Right, _
                Keys.Home, Keys.End, Keys.PageDown, Keys.PageUp

                Return True

            Case Else
                Return Not dataGridViewWantsInputKey
        End Select

    End Function

    Public Sub PrepareEditingControlForEdit(selectAll As Boolean) _
        Implements IDataGridViewEditingControl.PrepareEditingControlForEdit

        ' No preparation needs to be done. 

    End Sub

    Public ReadOnly Property RepositionEditingControlOnValueChange() _
        As Boolean Implements _
        IDataGridViewEditingControl.RepositionEditingControlOnValueChange

        Get
            Return False
        End Get

    End Property

    Public Property EditingControlDataGridView() As DataGridView _
        Implements IDataGridViewEditingControl.EditingControlDataGridView

        Get
            Return dataGridViewControl
        End Get
        Set(value As DataGridView)
            dataGridViewControl = value
        End Set

    End Property

    Public Property EditingControlValueChanged() As Boolean _
        Implements IDataGridViewEditingControl.EditingControlValueChanged

        Get
            Return valueIsChanged
        End Get
        Set(value As Boolean)
            valueIsChanged = value
        End Set

    End Property

    Public ReadOnly Property EditingControlCursor() As Cursor _
        Implements IDataGridViewEditingControl.EditingPanelCursor

        Get
            Return MyBase.Cursor
        End Get

    End Property

    Protected Overrides Sub OnValueChanged(eventargs As EventArgs)

        ' Notify the GolfGridView that the contents of the cell have changed.
        valueIsChanged = True
        Me.EditingControlDataGridView.NotifyCurrentCellDirty(True)
        MyBase.OnValueChanged(eventargs)

    End Sub

End Class
