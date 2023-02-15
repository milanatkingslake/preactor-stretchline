Public Class K203_SpindleAllocation
    Public Property strJobOrderNo As String
    Public Property strAvailableSpindels As String
    Public Property isOkClick As Boolean
    Public Property strRunningSpindleQty As String
    Public Property strMaxSpindles As String '' 12-11-2022

    Private Sub K203_SpindleAllocation_load(sender As Object, e As EventArgs) Handles MyBase.Load
        JobOrderNoTxt.Text = strJobOrderNo
        availableSpindelsTxt.Text = strAvailableSpindels
        '' 21-04-2022 AllocateSpindlesTxt.Text = strAvailableSpindels
        runningSpindlesTxt.Text = strRunningSpindleQty
        AllocateSpindlesTxt.Text = strRunningSpindleQty
        MaxSpindlesTxt.Text = strMaxSpindles '' 12-11-2022
    End Sub
    Private Sub K203_SpindleAllocation_FormClosed(sender As Object, e As Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If isOkClick Then
            sender = AllocateSpindlesTxt.Text
        Else
            sender = ""
        End If
    End Sub
    Private Sub OkBtn_Click(sender As Object, e As EventArgs) Handles OkBtn.Click
        '' 05-06-2022 -
        '' MsgBox("AllocateSpindlesTxt.Text " & AllocateSpindlesTxt.Text)
        '' MsgBox("runningSpindlesTxt.Text " & runningSpindlesTxt.Text)
        '' MsgBox("availableSpindelsTxt.Text " & availableSpindelsTxt.Text)

        Dim intavailableSpindels As Integer = CInt(Int(availableSpindelsTxt.Text))

        '' Dim intMaxSpindlesSpindels As Integer = CInt(Int(MaxSpindlesTxt.Text))
        Dim intAllocateSpindles As Integer = CInt(Int(AllocateSpindlesTxt.Text))
        Dim intRunningSpindles As Integer = CInt(Int(runningSpindlesTxt.Text))

        '' If intAllocateSpindles + intRunningSpindles <= intavailableSpindels Then
        If intAllocateSpindles <= intavailableSpindels + intRunningSpindles Then
            isOkClick = True
            Close()
        Else
            MsgBox("Allocate spindles should be less than or equal to " & intavailableSpindels + intRunningSpindles)
        End If

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles JobOrderNoTxt.TextChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles AvailableSpindles.Click

    End Sub

    Private Sub availableSpindelsTxt_TextChanged(sender As Object, e As EventArgs) Handles availableSpindelsTxt.TextChanged
        '' availableSpindelsTxt.Text = strAvailableSpindels
    End Sub

    Private Sub AllocateSpindlesTxt_TextChanged(sender As Object, e As EventArgs) Handles AllocateSpindlesTxt.TextChanged

    End Sub

    Private Sub AllocateSpindlesTxt_KeyPress(sender As Object, e As Windows.Forms.KeyPressEventArgs) Handles AllocateSpindlesTxt.KeyPress
        e.Handled = Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar)
    End Sub

    Private Sub runningSpindlesTxt_TextChanged(sender As Object, e As EventArgs) Handles runningSpindlesTxt.TextChanged

    End Sub

    Private Sub runningSpindles_Click(sender As Object, e As EventArgs) Handles runningSpindles.Click

    End Sub
End Class