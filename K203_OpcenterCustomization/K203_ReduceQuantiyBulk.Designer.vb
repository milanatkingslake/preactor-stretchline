<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class K203_ReduceQuantiyBulk
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
        Me.DataGridViewReduceQuantity = New System.Windows.Forms.DataGridView()
        Me.btnValidateQuantityReduction = New System.Windows.Forms.Button()
        Me.txtErpOrderNo = New System.Windows.Forms.TextBox()
        Me.lblErpOrderNo = New System.Windows.Forms.Label()
        Me.lblReduceQuantity = New System.Windows.Forms.Label()
        Me.txtReduceQuantity = New System.Windows.Forms.TextBox()
        CType(Me.DataGridViewReduceQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridViewReduceQuantity
        '
        Me.DataGridViewReduceQuantity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewReduceQuantity.Location = New System.Drawing.Point(8, 55)
        Me.DataGridViewReduceQuantity.Margin = New System.Windows.Forms.Padding(2)
        Me.DataGridViewReduceQuantity.Name = "DataGridViewReduceQuantity"
        Me.DataGridViewReduceQuantity.RowHeadersWidth = 62
        Me.DataGridViewReduceQuantity.RowTemplate.Height = 28
        Me.DataGridViewReduceQuantity.Size = New System.Drawing.Size(622, 233)
        Me.DataGridViewReduceQuantity.TabIndex = 0
        '
        'btnValidateQuantityReduction
        '
        Me.btnValidateQuantityReduction.Location = New System.Drawing.Point(467, 292)
        Me.btnValidateQuantityReduction.Margin = New System.Windows.Forms.Padding(2)
        Me.btnValidateQuantityReduction.Name = "btnValidateQuantityReduction"
        Me.btnValidateQuantityReduction.Size = New System.Drawing.Size(163, 27)
        Me.btnValidateQuantityReduction.TabIndex = 1
        Me.btnValidateQuantityReduction.Text = "Validate Quantity Reduction"
        Me.btnValidateQuantityReduction.UseVisualStyleBackColor = True
        '
        'txtErpOrderNo
        '
        Me.txtErpOrderNo.Enabled = False
        Me.txtErpOrderNo.Location = New System.Drawing.Point(89, 17)
        Me.txtErpOrderNo.Margin = New System.Windows.Forms.Padding(2)
        Me.txtErpOrderNo.Name = "txtErpOrderNo"
        Me.txtErpOrderNo.Size = New System.Drawing.Size(110, 20)
        Me.txtErpOrderNo.TabIndex = 2
        '
        'lblErpOrderNo
        '
        Me.lblErpOrderNo.AutoSize = True
        Me.lblErpOrderNo.Location = New System.Drawing.Point(8, 19)
        Me.lblErpOrderNo.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblErpOrderNo.Name = "lblErpOrderNo"
        Me.lblErpOrderNo.Size = New System.Drawing.Size(75, 13)
        Me.lblErpOrderNo.TabIndex = 3
        Me.lblErpOrderNo.Text = "ERP Order No"
        '
        'lblReduceQuantity
        '
        Me.lblReduceQuantity.AutoSize = True
        Me.lblReduceQuantity.Location = New System.Drawing.Point(203, 18)
        Me.lblReduceQuantity.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblReduceQuantity.Name = "lblReduceQuantity"
        Me.lblReduceQuantity.Size = New System.Drawing.Size(87, 13)
        Me.lblReduceQuantity.TabIndex = 4
        Me.lblReduceQuantity.Text = "Reduce Quantity"
        '
        'txtReduceQuantity
        '
        Me.txtReduceQuantity.Enabled = False
        Me.txtReduceQuantity.Location = New System.Drawing.Point(295, 16)
        Me.txtReduceQuantity.Margin = New System.Windows.Forms.Padding(2)
        Me.txtReduceQuantity.Name = "txtReduceQuantity"
        Me.txtReduceQuantity.Size = New System.Drawing.Size(89, 20)
        Me.txtReduceQuantity.TabIndex = 5
        '
        'K203_ReduceQuantiyBulk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(641, 330)
        Me.Controls.Add(Me.txtReduceQuantity)
        Me.Controls.Add(Me.lblReduceQuantity)
        Me.Controls.Add(Me.lblErpOrderNo)
        Me.Controls.Add(Me.txtErpOrderNo)
        Me.Controls.Add(Me.btnValidateQuantityReduction)
        Me.Controls.Add(Me.DataGridViewReduceQuantity)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "K203_ReduceQuantiyBulk"
        Me.Text = "Reduce Quantiy Calculation"
        CType(Me.DataGridViewReduceQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridViewReduceQuantity As Windows.Forms.DataGridView
    Friend WithEvents btnValidateQuantityReduction As Windows.Forms.Button
    Friend WithEvents txtErpOrderNo As Windows.Forms.TextBox
    Friend WithEvents lblErpOrderNo As Windows.Forms.Label
    Friend WithEvents lblReduceQuantity As Windows.Forms.Label
    Friend WithEvents txtReduceQuantity As Windows.Forms.TextBox
End Class
