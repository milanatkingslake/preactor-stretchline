Imports System.Drawing
Imports System.Windows.Forms

Public Class K203_ReduceQuantiyBulk
    Public Property strConnectionCon As String
    Public Property strERPJobNum As String
    Public Property strJobOrderNo As String
    Public Property decReduceQuantiyMain As Double

    Public Property tblReduceQuantiy As DataTable
    Public Property tblReduceQuantiy_Calculated As DataTable

    Private Sub K203_ReduceQuantiyBulk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtErpOrderNo.Text = strERPJobNum
        txtReduceQuantity.Text = decReduceQuantiyMain.ToString

        DataGridViewReduceQuantity.DataSource = tblReduceQuantiy
        'DataGridViewReduceQuantity.Refresh()
        'DataGridViewReduceQuantity.AutoResizeColumns()
        'Me.DataGridViewReduceQuantity.Columns("ID").Visible = False
        'Me.DataGridViewReduceQuantity.Columns("OrderNo").ReadOnly = True
        'Me.DataGridViewReduceQuantity.Columns("OrderQuantity").ReadOnly = True
        'Me.DataGridViewReduceQuantity.Columns("ReducedMainQuantity").ReadOnly = True
        'Me.DataGridViewReduceQuantity.Columns("FinalQuantity").ReadOnly = True

        DataGridViewReduceQuantity.AutoGenerateColumns = False

        DataGridViewReduceQuantity.Columns(0).HeaderText = "ID"
        DataGridViewReduceQuantity.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewReduceQuantity.Columns(0).ReadOnly = True
        DataGridViewReduceQuantity.Columns(0).DefaultCellStyle.BackColor = Color.LightGray
        DataGridViewReduceQuantity.Columns(0).Width = 100
        DataGridViewReduceQuantity.Columns(0).Visible = False


        DataGridViewReduceQuantity.Columns(1).HeaderText = "OrderNo"
        DataGridViewReduceQuantity.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewReduceQuantity.Columns(1).ReadOnly = True
        DataGridViewReduceQuantity.Columns(1).DefaultCellStyle.BackColor = Color.LightGray
        DataGridViewReduceQuantity.Columns(1).Width = 120


        DataGridViewReduceQuantity.Columns(2).HeaderText = "OrderQuantity"
        DataGridViewReduceQuantity.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewReduceQuantity.Columns(2).ReadOnly = True
        DataGridViewReduceQuantity.Columns(2).DefaultCellStyle.BackColor = Color.LightGray
        DataGridViewReduceQuantity.Columns(2).Width = 100


        DataGridViewReduceQuantity.Columns(3).HeaderText = "Quantity to be reduced"
        DataGridViewReduceQuantity.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewReduceQuantity.Columns(3).ReadOnly = True
        DataGridViewReduceQuantity.Columns(3).DefaultCellStyle.BackColor = Color.LightGray
        DataGridViewReduceQuantity.Columns(3).Width = 100


        DataGridViewReduceQuantity.Columns(4).HeaderText = "Applied Quantity Reduction"
        DataGridViewReduceQuantity.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewReduceQuantity.Columns(4).ReadOnly = False
        DataGridViewReduceQuantity.Columns(4).DefaultCellStyle.BackColor = Color.White
        DataGridViewReduceQuantity.Columns(4).Width = 100


        Dim gTextColumn As New DataGridViewTextBoxColumn()
        gTextColumn.HeaderText = "Applied Quantity Reduction"
        gTextColumn.Name = "ReducedQuantity"
        DataGridViewReduceQuantity.Columns.Remove("ReducedQuantity")
        DataGridViewReduceQuantity.Columns.Insert(4, gTextColumn)




        DataGridViewReduceQuantity.Columns(5).HeaderText = "Quantity After Applying Reduction"
        DataGridViewReduceQuantity.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridViewReduceQuantity.Columns(5).ReadOnly = True
        DataGridViewReduceQuantity.Columns(5).DefaultCellStyle.BackColor = Color.LightGray
        DataGridViewReduceQuantity.Columns(5).Width = 100

        DataGridViewReduceQuantity.Show()
    End Sub


    Private Sub DataGridViewReduceQuantity_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewReduceQuantity.CellValueChanged
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = DataGridViewReduceQuantity.Columns("ReducedQuantity").Index Then


            Dim orderQuantity_t As DataGridViewTextBoxCell = DirectCast(DataGridViewReduceQuantity.Rows(e.RowIndex).Cells("OrderQuantity"), DataGridViewTextBoxCell)
            Dim reducedQuantity_t As DataGridViewTextBoxCell = DirectCast(DataGridViewReduceQuantity.Rows(e.RowIndex).Cells("ReducedQuantity"), DataGridViewTextBoxCell)
            Dim finalQuantity_t As DataGridViewTextBoxCell = DirectCast(DataGridViewReduceQuantity.Rows(e.RowIndex).Cells("FinalQuantity"), DataGridViewTextBoxCell)

            If Not IsNumeric(reducedQuantity_t.Value) Then
                DataGridViewReduceQuantity.Rows(e.RowIndex).Cells("ReducedQuantity").Value = 0
            Else
                Dim orderQuantity_t_val As Decimal = CDec(orderQuantity_t.Value)
                Dim reducedQuantity_t_val As Decimal = CDec(reducedQuantity_t.Value)
                If orderQuantity_t_val >= reducedQuantity_t_val Then
                    If (validateReduction()) Then
                        DataGridViewReduceQuantity.Rows(e.RowIndex).Cells("FinalQuantity").Value = (orderQuantity_t_val - reducedQuantity_t_val).ToString
                    Else
                        DataGridViewReduceQuantity.Rows(e.RowIndex).Cells("ReducedQuantity").Value = 0
                    End If
                Else
                    MsgBox("Reduction quantity can't be grater than Order Quantity",, "Error")
                    DataGridViewReduceQuantity.Rows(e.RowIndex).Cells("ReducedQuantity").Value = orderQuantity_t
                End If
            End If
        End If
    End Sub
    Function validateReduction() As Boolean
        Dim mainReductionQuantity = decReduceQuantiyMain.ToString
        Dim iRowCnt As Integer = 0
        Dim dr As Data.DataRow
        Dim reductionQty As Decimal = 0

        For Each GridViewRow In DataGridViewReduceQuantity.Rows
            If DataGridViewReduceQuantity.Rows.Count - 1 > iRowCnt Then
                reductionQty = reductionQty + CDec(DataGridViewReduceQuantity.Rows(iRowCnt).Cells(4).Value)
            End If
            iRowCnt += 1
        Next
        If Not (CDec(txtReduceQuantity.Text) >= reductionQty) Then
            MsgBox("Reduction quantity can't be grater than Total reduction Quantity",, "Error")
            Return False
        End If
        Return True
    End Function
    Private Sub btnValidateQuantityReduction_Click(sender As Object, e As EventArgs) Handles btnValidateQuantityReduction.Click
        Dim calculated As Boolean = True
        Try

            Dim tblReduceQuantiy_Cal As DataTable = New DataTable()
            Dim rowId As DataColumn = New DataColumn("ID", Type.[GetType]("System.Double"))
            tblReduceQuantiy_Cal.Columns.Add(rowId)
            Dim sOrderNo As DataColumn = New DataColumn("OrderNo", Type.[GetType]("System.String"))
            tblReduceQuantiy_Cal.Columns.Add(sOrderNo)
            Dim sReducedQuantity As DataColumn = New DataColumn("ReducedQuantity", Type.[GetType]("System.Double"))
            tblReduceQuantiy_Cal.Columns.Add(sReducedQuantity)
            Dim iRowCnt As Integer = 0
            Dim dr As Data.DataRow
            For Each GridViewRow In DataGridViewReduceQuantity.Rows
                If DataGridViewReduceQuantity.Rows.Count - 1 > iRowCnt Then
                    If Not ((DataGridViewReduceQuantity.Rows(iRowCnt).Cells(4).Value Is DBNull.Value) Or (DataGridViewReduceQuantity.Rows(iRowCnt).Cells(4).Value Is Nothing)) Then
                        dr = tblReduceQuantiy_Cal.NewRow
                        dr(0) = DataGridViewReduceQuantity.Rows(iRowCnt).Cells(0).Value
                        dr(1) = DataGridViewReduceQuantity.Rows(iRowCnt).Cells(1).Value
                        dr(2) = DataGridViewReduceQuantity.Rows(iRowCnt).Cells(4).Value

                        tblReduceQuantiy_Cal.Rows.Add(dr)
                    End If
                End If
                iRowCnt += 1
            Next
            tblReduceQuantiy_Calculated = tblReduceQuantiy_Cal
        Catch ex As Exception
            MsgBox("Error in hiding the rows" & vbCrLf & ex.Message)
        End Try
        If (calculated) Then
            MsgBox("Reduction Quantity Calculated", , "Infor")
        End If

    End Sub

    Private Sub K203_ReduceQuantiyBulk_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Dim result As DialogResult = MessageBox.Show("do you want to Quantity Reduction Confirm and update...", "Infor", MessageBoxButtons.YesNo)
        If result = DialogResult.No Then
            sender = Nothing
        ElseIf result = DialogResult.Yes Then
            sender = tblReduceQuantiy_Calculated
        End If
    End Sub


End Class