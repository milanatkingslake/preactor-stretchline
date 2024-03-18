<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class K203_MultipleOrdersDragAndDrop
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
        Me.DataGridView_Orders = New System.Windows.Forms.DataGridView()
        Me.btnProcess = New System.Windows.Forms.Button()
        Me.txtOrderNo = New System.Windows.Forms.TextBox()
        Me.lblOrderNo = New System.Windows.Forms.Label()
        Me.lblSimilerPartNo = New System.Windows.Forms.Label()
        Me.txtSimilerPartNo = New System.Windows.Forms.TextBox()
        Me.lblDueDate = New System.Windows.Forms.Label()
        Me.btnFilter = New System.Windows.Forms.Button()
        Me.dtpDueDate = New System.Windows.Forms.DateTimePicker()
        Me.gbFilter = New System.Windows.Forms.GroupBox()
        Me.chCleardate = New System.Windows.Forms.CheckBox()
        Me.txtHeaderOrderNo = New System.Windows.Forms.TextBox()
        Me.lblHeaderOrderNo = New System.Windows.Forms.Label()
        Me.txtHeaderSimilerPartNo = New System.Windows.Forms.TextBox()
        Me.lblHeaderSimilerPartNo = New System.Windows.Forms.Label()
        Me.txtHeaderQuantity = New System.Windows.Forms.TextBox()
        Me.lblHeaderQuantity = New System.Windows.Forms.Label()
        Me.txtHeaderResource = New System.Windows.Forms.TextBox()
        Me.lblHeaderResource = New System.Windows.Forms.Label()
        Me.txtHeaderStartTime = New System.Windows.Forms.TextBox()
        Me.txtHeadersStartTime = New System.Windows.Forms.Label()
        Me.txtHeaderdDueDate = New System.Windows.Forms.TextBox()
        Me.lblHeaderDueDate = New System.Windows.Forms.Label()
        CType(Me.DataGridView_Orders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbFilter.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView_Orders
        '
        Me.DataGridView_Orders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView_Orders.Location = New System.Drawing.Point(12, 132)
        Me.DataGridView_Orders.Name = "DataGridView_Orders"
        Me.DataGridView_Orders.Size = New System.Drawing.Size(776, 260)
        Me.DataGridView_Orders.TabIndex = 0
        '
        'btnProcess
        '
        Me.btnProcess.Location = New System.Drawing.Point(649, 398)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(139, 29)
        Me.btnProcess.TabIndex = 1
        Me.btnProcess.Text = "Process"
        Me.btnProcess.UseVisualStyleBackColor = True
        '
        'txtOrderNo
        '
        Me.txtOrderNo.Location = New System.Drawing.Point(74, 22)
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.Size = New System.Drawing.Size(99, 20)
        Me.txtOrderNo.TabIndex = 2
        '
        'lblOrderNo
        '
        Me.lblOrderNo.AutoSize = True
        Me.lblOrderNo.Location = New System.Drawing.Point(21, 26)
        Me.lblOrderNo.Name = "lblOrderNo"
        Me.lblOrderNo.Size = New System.Drawing.Size(47, 13)
        Me.lblOrderNo.TabIndex = 3
        Me.lblOrderNo.Text = "OrderNo"
        '
        'lblSimilerPartNo
        '
        Me.lblSimilerPartNo.AutoSize = True
        Me.lblSimilerPartNo.Location = New System.Drawing.Point(179, 26)
        Me.lblSimilerPartNo.Name = "lblSimilerPartNo"
        Me.lblSimilerPartNo.Size = New System.Drawing.Size(76, 13)
        Me.lblSimilerPartNo.TabIndex = 5
        Me.lblSimilerPartNo.Text = "Similer Part No"
        '
        'txtSimilerPartNo
        '
        Me.txtSimilerPartNo.Location = New System.Drawing.Point(258, 22)
        Me.txtSimilerPartNo.Name = "txtSimilerPartNo"
        Me.txtSimilerPartNo.Size = New System.Drawing.Size(99, 20)
        Me.txtSimilerPartNo.TabIndex = 4
        '
        'lblDueDate
        '
        Me.lblDueDate.AutoSize = True
        Me.lblDueDate.Location = New System.Drawing.Point(366, 26)
        Me.lblDueDate.Name = "lblDueDate"
        Me.lblDueDate.Size = New System.Drawing.Size(53, 13)
        Me.lblDueDate.TabIndex = 7
        Me.lblDueDate.Text = "Due Date"
        '
        'btnFilter
        '
        Me.btnFilter.Location = New System.Drawing.Point(630, 18)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(118, 29)
        Me.btnFilter.TabIndex = 8
        Me.btnFilter.Text = "Filter"
        Me.btnFilter.UseVisualStyleBackColor = True
        '
        'dtpDueDate
        '
        Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDueDate.Location = New System.Drawing.Point(425, 22)
        Me.dtpDueDate.Name = "dtpDueDate"
        Me.dtpDueDate.Size = New System.Drawing.Size(115, 20)
        Me.dtpDueDate.TabIndex = 9
        '
        'gbFilter
        '
        Me.gbFilter.Controls.Add(Me.chCleardate)
        Me.gbFilter.Controls.Add(Me.dtpDueDate)
        Me.gbFilter.Controls.Add(Me.txtOrderNo)
        Me.gbFilter.Controls.Add(Me.btnFilter)
        Me.gbFilter.Controls.Add(Me.lblOrderNo)
        Me.gbFilter.Controls.Add(Me.lblDueDate)
        Me.gbFilter.Controls.Add(Me.txtSimilerPartNo)
        Me.gbFilter.Controls.Add(Me.lblSimilerPartNo)
        Me.gbFilter.Location = New System.Drawing.Point(13, 66)
        Me.gbFilter.Name = "gbFilter"
        Me.gbFilter.Size = New System.Drawing.Size(775, 60)
        Me.gbFilter.TabIndex = 10
        Me.gbFilter.TabStop = False
        Me.gbFilter.Text = "Filter"
        '
        'chCleardate
        '
        Me.chCleardate.AutoSize = True
        Me.chCleardate.Location = New System.Drawing.Point(548, 24)
        Me.chCleardate.Name = "chCleardate"
        Me.chCleardate.Size = New System.Drawing.Size(76, 17)
        Me.chCleardate.TabIndex = 10
        Me.chCleardate.Text = "Clear Date"
        Me.chCleardate.UseVisualStyleBackColor = True
        '
        'txtHeaderOrderNo
        '
        Me.txtHeaderOrderNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtHeaderOrderNo.Location = New System.Drawing.Point(167, 4)
        Me.txtHeaderOrderNo.Name = "txtHeaderOrderNo"
        Me.txtHeaderOrderNo.Size = New System.Drawing.Size(99, 20)
        Me.txtHeaderOrderNo.TabIndex = 11
        '
        'lblHeaderOrderNo
        '
        Me.lblHeaderOrderNo.AutoSize = True
        Me.lblHeaderOrderNo.Location = New System.Drawing.Point(115, 4)
        Me.lblHeaderOrderNo.Name = "lblHeaderOrderNo"
        Me.lblHeaderOrderNo.Size = New System.Drawing.Size(47, 13)
        Me.lblHeaderOrderNo.TabIndex = 12
        Me.lblHeaderOrderNo.Text = "OrderNo"
        '
        'txtHeaderSimilerPartNo
        '
        Me.txtHeaderSimilerPartNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtHeaderSimilerPartNo.Location = New System.Drawing.Point(382, 4)
        Me.txtHeaderSimilerPartNo.Name = "txtHeaderSimilerPartNo"
        Me.txtHeaderSimilerPartNo.Size = New System.Drawing.Size(99, 20)
        Me.txtHeaderSimilerPartNo.TabIndex = 13
        '
        'lblHeaderSimilerPartNo
        '
        Me.lblHeaderSimilerPartNo.AutoSize = True
        Me.lblHeaderSimilerPartNo.Location = New System.Drawing.Point(303, 8)
        Me.lblHeaderSimilerPartNo.Name = "lblHeaderSimilerPartNo"
        Me.lblHeaderSimilerPartNo.Size = New System.Drawing.Size(76, 13)
        Me.lblHeaderSimilerPartNo.TabIndex = 14
        Me.lblHeaderSimilerPartNo.Text = "Similer Part No"
        '
        'txtHeaderQuantity
        '
        Me.txtHeaderQuantity.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtHeaderQuantity.Location = New System.Drawing.Point(581, 6)
        Me.txtHeaderQuantity.Name = "txtHeaderQuantity"
        Me.txtHeaderQuantity.Size = New System.Drawing.Size(99, 20)
        Me.txtHeaderQuantity.TabIndex = 15
        '
        'lblHeaderQuantity
        '
        Me.lblHeaderQuantity.AutoSize = True
        Me.lblHeaderQuantity.Location = New System.Drawing.Point(528, 11)
        Me.lblHeaderQuantity.Name = "lblHeaderQuantity"
        Me.lblHeaderQuantity.Size = New System.Drawing.Size(46, 13)
        Me.lblHeaderQuantity.TabIndex = 16
        Me.lblHeaderQuantity.Text = "Quantity"
        '
        'txtHeaderResource
        '
        Me.txtHeaderResource.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtHeaderResource.Location = New System.Drawing.Point(580, 39)
        Me.txtHeaderResource.Name = "txtHeaderResource"
        Me.txtHeaderResource.Size = New System.Drawing.Size(99, 20)
        Me.txtHeaderResource.TabIndex = 17
        '
        'lblHeaderResource
        '
        Me.lblHeaderResource.AutoSize = True
        Me.lblHeaderResource.Location = New System.Drawing.Point(521, 42)
        Me.lblHeaderResource.Name = "lblHeaderResource"
        Me.lblHeaderResource.Size = New System.Drawing.Size(53, 13)
        Me.lblHeaderResource.TabIndex = 18
        Me.lblHeaderResource.Text = "Resource"
        '
        'txtHeaderStartTime
        '
        Me.txtHeaderStartTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtHeaderStartTime.Location = New System.Drawing.Point(168, 36)
        Me.txtHeaderStartTime.Name = "txtHeaderStartTime"
        Me.txtHeaderStartTime.Size = New System.Drawing.Size(99, 20)
        Me.txtHeaderStartTime.TabIndex = 19
        '
        'txtHeadersStartTime
        '
        Me.txtHeadersStartTime.AutoSize = True
        Me.txtHeadersStartTime.Location = New System.Drawing.Point(107, 41)
        Me.txtHeadersStartTime.Name = "txtHeadersStartTime"
        Me.txtHeadersStartTime.Size = New System.Drawing.Size(55, 13)
        Me.txtHeadersStartTime.TabIndex = 20
        Me.txtHeadersStartTime.Text = "Start Time"
        '
        'txtHeaderdDueDate
        '
        Me.txtHeaderdDueDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtHeaderdDueDate.Location = New System.Drawing.Point(382, 38)
        Me.txtHeaderdDueDate.Name = "txtHeaderdDueDate"
        Me.txtHeaderdDueDate.Size = New System.Drawing.Size(99, 20)
        Me.txtHeaderdDueDate.TabIndex = 21
        '
        'lblHeaderDueDate
        '
        Me.lblHeaderDueDate.AutoSize = True
        Me.lblHeaderDueDate.Location = New System.Drawing.Point(326, 41)
        Me.lblHeaderDueDate.Name = "lblHeaderDueDate"
        Me.lblHeaderDueDate.Size = New System.Drawing.Size(53, 13)
        Me.lblHeaderDueDate.TabIndex = 22
        Me.lblHeaderDueDate.Text = "Due Date"
        '
        'K203_MultipleOrdersDragAndDrop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 442)
        Me.Controls.Add(Me.txtHeaderdDueDate)
        Me.Controls.Add(Me.lblHeaderDueDate)
        Me.Controls.Add(Me.txtHeaderStartTime)
        Me.Controls.Add(Me.txtHeadersStartTime)
        Me.Controls.Add(Me.txtHeaderQuantity)
        Me.Controls.Add(Me.lblHeaderQuantity)
        Me.Controls.Add(Me.txtHeaderResource)
        Me.Controls.Add(Me.lblHeaderResource)
        Me.Controls.Add(Me.txtHeaderOrderNo)
        Me.Controls.Add(Me.lblHeaderOrderNo)
        Me.Controls.Add(Me.txtHeaderSimilerPartNo)
        Me.Controls.Add(Me.lblHeaderSimilerPartNo)
        Me.Controls.Add(Me.gbFilter)
        Me.Controls.Add(Me.btnProcess)
        Me.Controls.Add(Me.DataGridView_Orders)
        Me.Name = "K203_MultipleOrdersDragAndDrop"
        Me.Text = "Multiple Orders Selection"
        CType(Me.DataGridView_Orders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbFilter.ResumeLayout(False)
        Me.gbFilter.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView_Orders As Windows.Forms.DataGridView
    Friend WithEvents btnProcess As Windows.Forms.Button
    Friend WithEvents txtOrderNo As Windows.Forms.TextBox
    Friend WithEvents lblOrderNo As Windows.Forms.Label
    Friend WithEvents lblSimilerPartNo As Windows.Forms.Label
    Friend WithEvents txtSimilerPartNo As Windows.Forms.TextBox
    Friend WithEvents lblDueDate As Windows.Forms.Label
    Friend WithEvents btnFilter As Windows.Forms.Button
    Friend WithEvents dtpDueDate As Windows.Forms.DateTimePicker
    Friend WithEvents gbFilter As Windows.Forms.GroupBox
    Friend WithEvents txtHeaderOrderNo As Windows.Forms.TextBox
    Friend WithEvents lblHeaderOrderNo As Windows.Forms.Label
    Friend WithEvents txtHeaderSimilerPartNo As Windows.Forms.TextBox
    Friend WithEvents lblHeaderSimilerPartNo As Windows.Forms.Label
    Friend WithEvents txtHeaderQuantity As Windows.Forms.TextBox
    Friend WithEvents lblHeaderQuantity As Windows.Forms.Label
    Friend WithEvents txtHeaderResource As Windows.Forms.TextBox
    Friend WithEvents lblHeaderResource As Windows.Forms.Label
    Friend WithEvents txtHeaderStartTime As Windows.Forms.TextBox
    Friend WithEvents txtHeadersStartTime As Windows.Forms.Label
    Friend WithEvents txtHeaderdDueDate As Windows.Forms.TextBox
    Friend WithEvents lblHeaderDueDate As Windows.Forms.Label
    Friend WithEvents chCleardate As Windows.Forms.CheckBox
End Class
