<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class K203_JobSplitDetails
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.OkBtn = New System.Windows.Forms.Button()
        Me.JobOrderNoTxt = New System.Windows.Forms.TextBox()
        Me.OrderNo = New System.Windows.Forms.Label()
        Me.JobQtyTxt = New System.Windows.Forms.TextBox()
        Me.Quanty = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'OkBtn
        '
        Me.OkBtn.Location = New System.Drawing.Point(200, 116)
        Me.OkBtn.Name = "OkBtn"
        Me.OkBtn.Size = New System.Drawing.Size(88, 35)
        Me.OkBtn.TabIndex = 0
        Me.OkBtn.Text = "OK"
        Me.OkBtn.UseVisualStyleBackColor = True
        '
        'JobOrderNoTxt
        '
        Me.JobOrderNoTxt.Location = New System.Drawing.Point(200, 28)
        Me.JobOrderNoTxt.Name = "JobOrderNoTxt"
        Me.JobOrderNoTxt.ReadOnly = True
        Me.JobOrderNoTxt.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.JobOrderNoTxt.Size = New System.Drawing.Size(248, 26)
        Me.JobOrderNoTxt.TabIndex = 2
        '
        'OrderNo
        '
        Me.OrderNo.AutoSize = True
        Me.OrderNo.Location = New System.Drawing.Point(121, 32)
        Me.OrderNo.Name = "OrderNo"
        Me.OrderNo.Size = New System.Drawing.Size(73, 20)
        Me.OrderNo.TabIndex = 3
        Me.OrderNo.Text = "Order No"
        '
        'JobQtyTxt
        '
        Me.JobQtyTxt.AcceptsTab = True
        Me.JobQtyTxt.Location = New System.Drawing.Point(200, 61)
        Me.JobQtyTxt.Name = "JobQtyTxt"
        Me.JobQtyTxt.Size = New System.Drawing.Size(148, 26)
        Me.JobQtyTxt.TabIndex = 4
        '
        'Quanty
        '
        Me.Quanty.AutoSize = True
        Me.Quanty.Location = New System.Drawing.Point(134, 64)
        Me.Quanty.Name = "Quanty"
        Me.Quanty.Size = New System.Drawing.Size(60, 20)
        Me.Quanty.TabIndex = 5
        Me.Quanty.Text = "Quanty"
        '
        'K203_JobSplitDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(567, 185)
        Me.Controls.Add(Me.Quanty)
        Me.Controls.Add(Me.JobQtyTxt)
        Me.Controls.Add(Me.OrderNo)
        Me.Controls.Add(Me.JobOrderNoTxt)
        Me.Controls.Add(Me.OkBtn)
        Me.Name = "K203_JobSplitDetails"
        Me.Text = "K203_JobSplitDetails"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents OkBtn As Windows.Forms.Button
    Friend WithEvents JobOrderNoTxt As Windows.Forms.TextBox
    Friend WithEvents OrderNo As Windows.Forms.Label
    Friend WithEvents JobQtyTxt As Windows.Forms.TextBox
    Friend WithEvents Quanty As Windows.Forms.Label
End Class
