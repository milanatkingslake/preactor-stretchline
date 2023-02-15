Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Preactor
Imports Preactor.Interop.PreactorObject



Public Class K203_JobMerge
    Public Property strJobOrderNo As String
    Public Property dblJobOrderQty As Double
    Public Property tblJobMerge As DataTable
    Public Property connetionString As String
    Public Property isOkClick As Boolean
    Public Property dblTotSelectQty As Double
    Public Property dblTotAssignedSpindle As Double
    Public Property dblSelectedSpindle As Double
    Dim isSpindlesEqual As Boolean = True
    Dim isCalculate As Boolean = False

    Private Sub K203_JobMerge_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SelectedJobNoTxt.Text = strJobOrderNo
        JobQtyTxt.Text = dblJobOrderQty.ToString
        TotalJobQtyTxt.Text = dblJobOrderQty.ToString

        DataGridViewJobMerge.DataSource = tblJobMerge

        Dim jobOrderNoColumn As New DataGridViewTextBoxColumn()
        jobOrderNoColumn.HeaderText = "Job Order No11"
        jobOrderNoColumn.Width = 90
        jobOrderNoColumn.Name = "checkBoxColumn"
        jobOrderNoColumn.DataPropertyName = "Job Order No"
        jobOrderNoColumn.ReadOnly = True
        DataGridViewJobMerge.Columns.Insert(2, jobOrderNoColumn)
        DataGridViewJobMerge.Columns(1).ReadOnly = True  '' Job order No
        DataGridViewJobMerge.Columns(4).ReadOnly = True  '' No. of Spindles
        DataGridViewJobMerge.Columns(5).ReadOnly = True  '' Job Quantity

        Dim checkBoxColumn As New DataGridViewCheckBoxColumn()
        checkBoxColumn.HeaderText = "Select"
        checkBoxColumn.Width = 100
        checkBoxColumn.Name = "checkBoxColumn"
        checkBoxColumn.DataPropertyName = "Check"
        DataGridViewJobMerge.Columns(0).Visible = False
        DataGridViewJobMerge.Columns(2).Visible = False
        DataGridViewJobMerge.Columns(3).Visible = False
        DataGridViewJobMerge.Columns.Insert(3, checkBoxColumn)

        '' DataGridViewJobMerge.Columns(1).Visible = False

        DataGridViewJobMerge.Refresh()
        DataGridViewJobMerge.AutoResizeColumns()
        DataGridViewJobMerge.AllowUserToAddRows = False
    End Sub

    Private Sub K203_JobMerge_Closed(sender As Object, e As Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        '' sender = tblJobMerge

        If isOkClick Then
            '    MsgBox("Closed")
            sender = tblJobMerge
        Else
            sender = ""
        End If

    End Sub

    Private Sub DataGridView_CellClick(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewJobMerge.CellContentClick
    End Sub
    Private Sub DataGridViewJobMerge_CellContentClick(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewJobMerge.CellContentClick
        isCalculate = False
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        If isCalculate = True Then

            isSpindlesEqual = True

            For Each tbl_JobMergeRow As DataRow In tblJobMerge.Rows
                If tbl_JobMergeRow("Check").ToString() = "True" Then

                    dblSelectedSpindle = CDbl(tbl_JobMergeRow("Allocated Spindles").ToString())

                    '' MsgBox("dblTotAssignedSpindle " & dblTotAssignedSpindle & " dblSelectedSpindle " & dblSelectedSpindle)

                    If isSpindlesEqual = True Then

                        If dblTotAssignedSpindle <> dblSelectedSpindle Then
                            isSpindlesEqual = False
                        End If

                    End If
                End If
            Next

            If isSpindlesEqual = True Then
                isOkClick = True
                Close()
            Else
                MsgBox("One or more record(s) selected have not required spindles")
            End If
        Else
            MsgBox("You did not calculated the job amount")
        End If

    End Sub

    Private Sub DataGridViewJobMerge_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewJobMerge.CellClick
        'dblTotSelectQty = 0
        'For Each tbl_JobMergeRow As DataRow In tblJobMerge.Rows
        '    If tbl_JobMergeRow("Check").ToString() = "True" Then
        '        dblTotSelectQty = dblTotSelectQty + CDbl(tbl_JobMergeRow("Job Quantity").ToString())
        '    End If
        'Next

        'MsgBox("dblTotSelectQty " & dblTotSelectQty)

    End Sub

    Private Sub DataGridViewJobMerge_Leave(sender As Object, e As EventArgs) Handles DataGridViewJobMerge.Leave

    End Sub

    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        dblTotSelectQty = 0
        dblSelectedSpindle = 0
        '' Dim isSpindlesEqual As Boolean = True

        isCalculate = False
        Dim isSpindlesEqualCount As Integer = 0
        For Each tbl_JobMergeRow As DataRow In tblJobMerge.Rows
            If tbl_JobMergeRow("Check").ToString() = "True" Then
                dblTotSelectQty = dblTotSelectQty + CDbl(tbl_JobMergeRow("Job Quantity").ToString())

                dblSelectedSpindle = CDbl(tbl_JobMergeRow("Allocated Spindles").ToString())

                '' MsgBox("dblTotAssignedSpindle " & dblTotAssignedSpindle & " dblSelectedSpindle " & dblSelectedSpindle)

                ''If isSpindlesEqual = True Then

                If dblTotAssignedSpindle <> dblSelectedSpindle Then
                    isSpindlesEqual = False
                    isSpindlesEqualCount = isSpindlesEqualCount + 1
                Else
                        isSpindlesEqual = True
                    End If

                ''End If
            End If
        Next

        SelectedTotalTxt.Text = dblTotSelectQty.ToString
        TotalJobQtyTxt.Text = (dblJobOrderQty + dblTotSelectQty).ToString

        ''If isSpindlesEqual = False Then
        If isSpindlesEqualCount > 0 Then
            MsgBox("One or more record(s) selected have not required spindles")
        End If

        isCalculate = True

    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click

        dblTotSelectQty = 0
        For Each tbl_JobMergeRow As DataRow In tblJobMerge.Rows
            tbl_JobMergeRow("Check") = "True"
            dblTotSelectQty = dblTotSelectQty + CDbl(tbl_JobMergeRow("Job Quantity").ToString())
        Next

        SelectedTotalTxt.Text = dblTotSelectQty.ToString
        TotalJobQtyTxt.Text = dblJobOrderQty.ToString
        TotalJobQtyTxt.Text = (dblJobOrderQty + dblTotSelectQty).ToString

        isCalculate = True

    End Sub

    Private Sub btnClearAll_Click(sender As Object, e As EventArgs) Handles btnClearAll.Click
        For Each tbl_JobMergeRow As DataRow In tblJobMerge.Rows
            tbl_JobMergeRow("Check") = "False"
        Next

        SelectedTotalTxt.Text = "0.000"
        TotalJobQtyTxt.Text = dblJobOrderQty.ToString
        TotalJobQtyTxt.Text = dblJobOrderQty.ToString

        isCalculate = True

    End Sub

    '' Private Sub DataGridViewJobMerge_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewJobMerge.CellEndEdit
    ''    dblTotSelectQty = 0
    ''For Each tbl_JobMergeRow As DataRow In tblJobMerge.Rows
    ''If tbl_JobMergeRow("Check").ToString() = "True" Then
    ''            dblTotSelectQty = dblTotSelectQty + CDbl(tbl_JobMergeRow("Job Quantity").ToString())
    ''End If
    ''Next

    ''  MsgBox("dblTotSelectQty 11 " & dblTotSelectQty)
    ''TotalJobQtyTxt.Text = dblTotSelectQty.ToString
    ''BtnOK.Focus()

    ''End Sub
End Class