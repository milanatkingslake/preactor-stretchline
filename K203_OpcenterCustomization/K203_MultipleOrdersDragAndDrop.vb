Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

Public Class K203_MultipleOrdersDragAndDrop
    Public Property tblOrders As DataTable
    Public Property strOrderNo As String
    Public Property strSimilerPartNo As String
    Public Property dbQuantity As Double
    Public Property strResource As String
    Public Property dtStartTime As DateTime
    Public Property dtDueDate As DateTime

    Public Property tblOrders_filter As DataTable

    Public Property dtSelectedJob As DataTable


    Private Sub K203_MultipleOrdersDragAndDrop_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpDueDate.CustomFormat = "MM/dd/yyyy"

        txtHeaderOrderNo.Text = strOrderNo
        txtHeaderSimilerPartNo.Text = strSimilerPartNo
        txtHeaderQuantity.Text = dbQuantity.ToString
        txtHeaderResource.Text = strResource
        txtHeaderStartTime.Text = dtStartTime.ToString()
        txtHeaderdDueDate.Text = dtDueDate.ToString()

        tblOrders_filter = tblOrders
        LoadGridView()
    End Sub
    Sub LoadGridView()
        DataGridView_Orders.AllowUserToAddRows = False
        DataGridView_Orders.DataSource = tblOrders_filter
        DataGridView_Orders.AutoGenerateColumns = False

        Dim checkBoxColumn As New DataGridViewCheckBoxColumn()
        checkBoxColumn.HeaderText = "Select"
        checkBoxColumn.Width = 70
        checkBoxColumn.Name = "ColumnChecked"
        checkBoxColumn.DataPropertyName = "Checked"
        DataGridView_Orders.Columns.Insert(0, checkBoxColumn)

        DataGridView_Orders.Columns(1).HeaderText = "OrderNo"
        DataGridView_Orders.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridView_Orders.Columns(1).ReadOnly = True
        DataGridView_Orders.Columns(1).DefaultCellStyle.BackColor = Color.LightGray
        DataGridView_Orders.Columns(1).Width = 120
        DataGridView_Orders.Columns(1).Visible = False

        'DataGridView_Orders.Columns(1).HeaderText = "OrderNo"
        'DataGridView_Orders.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        'DataGridView_Orders.Columns(1).ReadOnly = True
        'DataGridView_Orders.Columns(1).DefaultCellStyle.BackColor = Color.LightGray
        'DataGridView_Orders.Columns(1).Width = 120

        'DataGridView_Orders.Columns(2).HeaderText = "OrderQuantity"
        'DataGridView_Orders.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        'DataGridView_Orders.Columns(2).ReadOnly = True
        'DataGridView_Orders.Columns(2).DefaultCellStyle.BackColor = Color.LightGray
        'DataGridView_Orders.Columns(2).Width = 100

        'DataGridView_Orders.Columns(3).HeaderText = "Quantity to be reduced"
        'DataGridView_Orders.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DataGridView_Orders.Columns(3).ReadOnly = True
        'DataGridView_Orders.Columns(3).DefaultCellStyle.BackColor = Color.LightGray
        'DataGridView_Orders.Columns(3).Width = 100

        'DataGridView_Orders.Columns(4).HeaderText = "Applied Quantity Reduction"
        'DataGridView_Orders.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DataGridView_Orders.Columns(4).ReadOnly = False
        'DataGridView_Orders.Columns(4).DefaultCellStyle.BackColor = Color.White
        'DataGridView_Orders.Columns(4).Width = 100

        DataGridView_Orders.Show()
    End Sub
    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Dim dtSelectedJob_ As DataTable = New DataTable()
        Dim c_OrderNo_s As DataColumn = New DataColumn("OrderNo", Type.[GetType]("System.String"))
        dtSelectedJob_.Columns.Add(c_OrderNo_s)
        Dim respond As DialogResult = MessageBox.Show("Do you want to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If respond = DialogResult.Yes Then


            For Each row As DataGridViewRow In DataGridView_Orders.Rows

                Dim checkBoxCell As DataGridViewCheckBoxCell = DirectCast(row.Cells("ColumnChecked"), DataGridViewCheckBoxCell)

                Dim isChecked As Boolean

                Try
                    If Not (checkBoxCell.Value Is Nothing) Then
                        If Not (checkBoxCell.Value.ToString = "") Then
                            isChecked = CBool(checkBoxCell.Value)
                            If isChecked Then
                                dtSelectedJob_.Rows.Add(row.Cells("OrderNo").Value)
                            End If
                        End If
                    End If
                Catch ex As Exception
                    ''MsgBox(ex.Message)
                End Try
            Next
            Me.Close()
            '' Application.Exit()
        End If
        dtSelectedJob = dtSelectedJob_
    End Sub

    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        'Dim filteredRows() As DataRow
        'If txtOrderNo.Text <> "" Then
        '    filteredRows = tblOrders.Select("OrderNo = '" + txtOrderNo.Text + "'")
        'End If
        ''If txtSimilerPartNo.Text <> "" Then
        ''    filteredRows = tblOrders.Select("SimilerPartNo = '" + txtSimilerPartNo.Text + "'")
        ''End If
        'tblOrders_filter.Clear()

        'For Each row As DataRow In filteredRows
        '    tblOrders_filter.ImportRow(row)
        'Next

        tblOrders_filter = tblOrders
        Dim filterValue As String
        Dim filterExpression As String
        Dim filteredDataView As DataView

        filterValue = txtOrderNo.Text
        filterExpression = String.Format("[OrderNo] LIKE '%{0}%'", filterValue)
        tblOrders_filter.DefaultView.RowFilter = filterExpression
        filteredDataView = tblOrders_filter.DefaultView
        tblOrders_filter = filteredDataView.ToTable()

        filterValue = txtSimilerPartNo.Text
        filterExpression = String.Format("[SimilerPartNo] LIKE '%{0}%'", filterValue)
        tblOrders_filter.DefaultView.RowFilter = filterExpression
        filteredDataView = tblOrders_filter.DefaultView
        tblOrders_filter = filteredDataView.ToTable()

        Dim inputString As String = dtpDueDate.Text

        If chCleardate.Checked = False Then
            filterValue = dtpDueDate.Text.ToString()
            filterExpression = String.Format("[DueDate] >= #{0}#", filterValue)
            tblOrders_filter.DefaultView.RowFilter = filterExpression
            filteredDataView = tblOrders_filter.DefaultView
            tblOrders_filter = filteredDataView.ToTable()
        End If

        LoadGridView()


    End Sub
End Class