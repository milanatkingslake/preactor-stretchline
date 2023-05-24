<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class K203_JobMerge
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.DataGridViewJobMerge = New System.Windows.Forms.DataGridView()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.SelectedJobNoTxt = New System.Windows.Forms.TextBox()
        Me.SelectedJobOrderNo = New System.Windows.Forms.Label()
        Me.JobQtyTxt = New System.Windows.Forms.TextBox()
        Me.Quantity = New System.Windows.Forms.Label()
        Me.TotalJobQtyTxt = New System.Windows.Forms.TextBox()
        Me.TotalQty = New System.Windows.Forms.Label()
        Me.btnCalculate = New System.Windows.Forms.Button()
        Me.SelectedTotalTxt = New System.Windows.Forms.TextBox()
        Me.SelectedQty = New System.Windows.Forms.Label()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.btnClearAll = New System.Windows.Forms.Button()
        CType(Me.DataGridViewJobMerge, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridViewJobMerge
        '
        Me.DataGridViewJobMerge.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewJobMerge.Location = New System.Drawing.Point(2, 32)
        Me.DataGridViewJobMerge.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DataGridViewJobMerge.Name = "DataGridViewJobMerge"
        Me.DataGridViewJobMerge.RowHeadersWidth = 62
        Me.DataGridViewJobMerge.RowTemplate.Height = 28
        Me.DataGridViewJobMerge.Size = New System.Drawing.Size(440, 183)
        Me.DataGridViewJobMerge.TabIndex = 0
        '
        'BtnOK
        '
        Me.BtnOK.Location = New System.Drawing.Point(168, 259)
        Me.BtnOK.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(50, 18)
        Me.BtnOK.TabIndex = 1
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'SelectedJobNoTxt
        '
        Me.SelectedJobNoTxt.Location = New System.Drawing.Point(122, 8)
        Me.SelectedJobNoTxt.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SelectedJobNoTxt.Name = "SelectedJobNoTxt"
        Me.SelectedJobNoTxt.ReadOnly = True
        Me.SelectedJobNoTxt.Size = New System.Drawing.Size(107, 20)
        Me.SelectedJobNoTxt.TabIndex = 2
        '
        'SelectedJobOrderNo
        '
        Me.SelectedJobOrderNo.AutoSize = True
        Me.SelectedJobOrderNo.Location = New System.Drawing.Point(5, 11)
        Me.SelectedJobOrderNo.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SelectedJobOrderNo.Name = "SelectedJobOrderNo"
        Me.SelectedJobOrderNo.Size = New System.Drawing.Size(115, 13)
        Me.SelectedJobOrderNo.TabIndex = 3
        Me.SelectedJobOrderNo.Text = "Selected Job Order No"
        '
        'JobQtyTxt
        '
        Me.JobQtyTxt.Location = New System.Drawing.Point(303, 8)
        Me.JobQtyTxt.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.JobQtyTxt.Name = "JobQtyTxt"
        Me.JobQtyTxt.ReadOnly = True
        Me.JobQtyTxt.Size = New System.Drawing.Size(119, 20)
        Me.JobQtyTxt.TabIndex = 4
        '
        'Quantity
        '
        Me.Quantity.AutoSize = True
        Me.Quantity.Location = New System.Drawing.Point(253, 11)
        Me.Quantity.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Quantity.Name = "Quantity"
        Me.Quantity.Size = New System.Drawing.Size(46, 13)
        Me.Quantity.TabIndex = 5
        Me.Quantity.Text = "Quantity"
        '
        'TotalJobQtyTxt
        '
        Me.TotalJobQtyTxt.Location = New System.Drawing.Point(327, 250)
        Me.TotalJobQtyTxt.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TotalJobQtyTxt.Name = "TotalJobQtyTxt"
        Me.TotalJobQtyTxt.ReadOnly = True
        Me.TotalJobQtyTxt.Size = New System.Drawing.Size(113, 20)
        Me.TotalJobQtyTxt.TabIndex = 6
        '
        'TotalQty
        '
        Me.TotalQty.AutoSize = True
        Me.TotalQty.Location = New System.Drawing.Point(231, 252)
        Me.TotalQty.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.TotalQty.Name = "TotalQty"
        Me.TotalQty.Size = New System.Drawing.Size(94, 13)
        Me.TotalQty.TabIndex = 7
        Me.TotalQty.Text = "Total Quantity (kg)"
        '
        'btnCalculate
        '
        Me.btnCalculate.Location = New System.Drawing.Point(165, 227)
        Me.btnCalculate.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnCalculate.Name = "btnCalculate"
        Me.btnCalculate.Size = New System.Drawing.Size(57, 18)
        Me.btnCalculate.TabIndex = 8
        Me.btnCalculate.Text = "Calculate"
        Me.btnCalculate.UseVisualStyleBackColor = True
        '
        'SelectedTotalTxt
        '
        Me.SelectedTotalTxt.Location = New System.Drawing.Point(327, 228)
        Me.SelectedTotalTxt.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SelectedTotalTxt.Name = "SelectedTotalTxt"
        Me.SelectedTotalTxt.ReadOnly = True
        Me.SelectedTotalTxt.Size = New System.Drawing.Size(113, 20)
        Me.SelectedTotalTxt.TabIndex = 9
        '
        'SelectedQty
        '
        Me.SelectedQty.AutoSize = True
        Me.SelectedQty.Location = New System.Drawing.Point(233, 230)
        Me.SelectedQty.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SelectedQty.Name = "SelectedQty"
        Me.SelectedQty.Size = New System.Drawing.Size(91, 13)
        Me.SelectedQty.TabIndex = 10
        Me.SelectedQty.Text = "Selected Quantity"
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Location = New System.Drawing.Point(5, 226)
        Me.btnSelectAll.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(57, 19)
        Me.btnSelectAll.TabIndex = 12
        Me.btnSelectAll.Text = "Select All"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'btnClearAll
        '
        Me.btnClearAll.Location = New System.Drawing.Point(71, 227)
        Me.btnClearAll.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btnClearAll.Name = "btnClearAll"
        Me.btnClearAll.Size = New System.Drawing.Size(55, 18)
        Me.btnClearAll.TabIndex = 13
        Me.btnClearAll.Text = "Clear All"
        Me.btnClearAll.UseVisualStyleBackColor = True
        '
        'K203_JobMerge
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(445, 292)
        Me.Controls.Add(Me.btnClearAll)
        Me.Controls.Add(Me.btnSelectAll)
        Me.Controls.Add(Me.SelectedQty)
        Me.Controls.Add(Me.SelectedTotalTxt)
        Me.Controls.Add(Me.btnCalculate)
        Me.Controls.Add(Me.TotalQty)
        Me.Controls.Add(Me.TotalJobQtyTxt)
        Me.Controls.Add(Me.Quantity)
        Me.Controls.Add(Me.JobQtyTxt)
        Me.Controls.Add(Me.SelectedJobOrderNo)
        Me.Controls.Add(Me.SelectedJobNoTxt)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.DataGridViewJobMerge)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "K203_JobMerge"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "K203_Job Merge"
        CType(Me.DataGridViewJobMerge, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridViewJobMerge As Windows.Forms.DataGridView
    Friend WithEvents BtnOK As Windows.Forms.Button
    Friend WithEvents SelectedJobNoTxt As Windows.Forms.TextBox
    Friend WithEvents SelectedJobOrderNo As Windows.Forms.Label
    Friend WithEvents JobQtyTxt As Windows.Forms.TextBox
    Friend WithEvents Quantity As Windows.Forms.Label
    Friend WithEvents TotalJobQtyTxt As Windows.Forms.TextBox
    Friend WithEvents TotalQty As Windows.Forms.Label
    Friend WithEvents btnCalculate As Windows.Forms.Button
    Friend WithEvents SelectedTotalTxt As Windows.Forms.TextBox
    Friend WithEvents SelectedQty As Windows.Forms.Label
    Friend WithEvents btnSelectAll As Windows.Forms.Button
    Friend WithEvents btnClearAll As Windows.Forms.Button
End Class
