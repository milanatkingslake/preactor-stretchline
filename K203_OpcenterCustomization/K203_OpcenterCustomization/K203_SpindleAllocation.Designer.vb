<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class K203_SpindleAllocation
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
        Me.components = New System.ComponentModel.Container()
        Me.JobOrderNoTxt = New System.Windows.Forms.TextBox()
        Me.OrderNo = New System.Windows.Forms.Label()
        Me.availableSpindelsTxt = New System.Windows.Forms.TextBox()
        Me.AvailableSpindles = New System.Windows.Forms.Label()
        Me.AllocateSpindlesTxt = New System.Windows.Forms.TextBox()
        Me.AllocateSpindlesLbl = New System.Windows.Forms.Label()
        Me.OkBtn = New System.Windows.Forms.Button()
        Me.runningSpindlesTxt = New System.Windows.Forms.TextBox()
        Me.runningSpindles = New System.Windows.Forms.Label()
        Me.MaxSpindlesTxt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'JobOrderNoTxt
        '
        Me.JobOrderNoTxt.Location = New System.Drawing.Point(307, 19)
        Me.JobOrderNoTxt.Name = "JobOrderNoTxt"
        Me.JobOrderNoTxt.ReadOnly = True
        Me.JobOrderNoTxt.Size = New System.Drawing.Size(190, 26)
        Me.JobOrderNoTxt.TabIndex = 0
        '
        'OrderNo
        '
        Me.OrderNo.AutoSize = True
        Me.OrderNo.Location = New System.Drawing.Point(226, 25)
        Me.OrderNo.Name = "OrderNo"
        Me.OrderNo.Size = New System.Drawing.Size(73, 20)
        Me.OrderNo.TabIndex = 1
        Me.OrderNo.Text = "Order No"
        '
        'availableSpindelsTxt
        '
        Me.availableSpindelsTxt.Location = New System.Drawing.Point(307, 83)
        Me.availableSpindelsTxt.Name = "availableSpindelsTxt"
        Me.availableSpindelsTxt.ReadOnly = True
        Me.availableSpindelsTxt.Size = New System.Drawing.Size(190, 26)
        Me.availableSpindelsTxt.TabIndex = 2
        '
        'AvailableSpindles
        '
        Me.AvailableSpindles.AutoSize = True
        Me.AvailableSpindles.Location = New System.Drawing.Point(165, 84)
        Me.AvailableSpindles.Name = "AvailableSpindles"
        Me.AvailableSpindles.Size = New System.Drawing.Size(137, 20)
        Me.AvailableSpindles.TabIndex = 3
        Me.AvailableSpindles.Text = "Available Spindles"
        '
        'AllocateSpindlesTxt
        '
        Me.AllocateSpindlesTxt.Location = New System.Drawing.Point(307, 149)
        Me.AllocateSpindlesTxt.Name = "AllocateSpindlesTxt"
        Me.AllocateSpindlesTxt.Size = New System.Drawing.Size(190, 26)
        Me.AllocateSpindlesTxt.TabIndex = 4
        '
        'AllocateSpindlesLbl
        '
        Me.AllocateSpindlesLbl.AutoSize = True
        Me.AllocateSpindlesLbl.Location = New System.Drawing.Point(169, 153)
        Me.AllocateSpindlesLbl.Name = "AllocateSpindlesLbl"
        Me.AllocateSpindlesLbl.Size = New System.Drawing.Size(131, 20)
        Me.AllocateSpindlesLbl.TabIndex = 5
        Me.AllocateSpindlesLbl.Text = "Allocate Spindles"
        '
        'OkBtn
        '
        Me.OkBtn.Location = New System.Drawing.Point(307, 181)
        Me.OkBtn.Name = "OkBtn"
        Me.OkBtn.Size = New System.Drawing.Size(75, 30)
        Me.OkBtn.TabIndex = 6
        Me.OkBtn.Text = "OK"
        Me.OkBtn.UseVisualStyleBackColor = True
        '
        'runningSpindlesTxt
        '
        Me.runningSpindlesTxt.Location = New System.Drawing.Point(307, 117)
        Me.runningSpindlesTxt.Name = "runningSpindlesTxt"
        Me.runningSpindlesTxt.ReadOnly = True
        Me.runningSpindlesTxt.Size = New System.Drawing.Size(190, 26)
        Me.runningSpindlesTxt.TabIndex = 7
        '
        'runningSpindles
        '
        Me.runningSpindles.AutoSize = True
        Me.runningSpindles.Location = New System.Drawing.Point(132, 120)
        Me.runningSpindles.Name = "runningSpindles"
        Me.runningSpindles.Size = New System.Drawing.Size(170, 20)
        Me.runningSpindles.TabIndex = 8
        Me.runningSpindles.Text = "Allocated Job Spindles"
        '
        'MaxSpindlesTxt
        '
        Me.MaxSpindlesTxt.Location = New System.Drawing.Point(307, 51)
        Me.MaxSpindlesTxt.Name = "MaxSpindlesTxt"
        Me.MaxSpindlesTxt.ReadOnly = True
        Me.MaxSpindlesTxt.Size = New System.Drawing.Size(190, 26)
        Me.MaxSpindlesTxt.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(200, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 20)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Max Spindles"
        '
        'K203_SpindleAllocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(645, 225)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MaxSpindlesTxt)
        Me.Controls.Add(Me.runningSpindles)
        Me.Controls.Add(Me.runningSpindlesTxt)
        Me.Controls.Add(Me.OkBtn)
        Me.Controls.Add(Me.AllocateSpindlesLbl)
        Me.Controls.Add(Me.AllocateSpindlesTxt)
        Me.Controls.Add(Me.AvailableSpindles)
        Me.Controls.Add(Me.availableSpindelsTxt)
        Me.Controls.Add(Me.OrderNo)
        Me.Controls.Add(Me.JobOrderNoTxt)
        Me.Name = "K203_SpindleAllocation"
        Me.Text = "K203_SpindleAllocation"
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents JobOrderNoTxt As Windows.Forms.TextBox
    Friend WithEvents OrderNo As Windows.Forms.Label
    Friend WithEvents availableSpindelsTxt As Windows.Forms.TextBox
    Friend WithEvents AvailableSpindles As Windows.Forms.Label
    Friend WithEvents AllocateSpindlesTxt As Windows.Forms.TextBox
    Friend WithEvents AllocateSpindlesLbl As Windows.Forms.Label
    Friend WithEvents OkBtn As Windows.Forms.Button
    Friend WithEvents runningSpindlesTxt As Windows.Forms.TextBox
    Friend WithEvents runningSpindles As Windows.Forms.Label
    Friend WithEvents MaxSpindlesTxt As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents BindingSource1 As Windows.Forms.BindingSource
End Class
