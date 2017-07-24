<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectSerialNo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSelectSerialNo))
        Me.ultraGroupBox2 = New Infragistics.Win.Misc.UltraGroupBox
        Me.lbSelected = New System.Windows.Forms.ListBox
        Me.UltraButton3 = New Infragistics.Win.Misc.UltraButton
        Me.UltraButton4 = New Infragistics.Win.Misc.UltraButton
        Me.UltraButton2 = New Infragistics.Win.Misc.UltraButton
        Me.UltraButton1 = New Infragistics.Win.Misc.UltraButton
        Me.lbAdded = New System.Windows.Forms.ListBox
        Me.btnOK = New Infragistics.Win.Misc.UltraButton
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton
        CType(Me.ultraGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ultraGroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ultraGroupBox2
        '
        Me.ultraGroupBox2.Controls.Add(Me.lbSelected)
        Me.ultraGroupBox2.Controls.Add(Me.UltraButton3)
        Me.ultraGroupBox2.Controls.Add(Me.UltraButton4)
        Me.ultraGroupBox2.Controls.Add(Me.UltraButton2)
        Me.ultraGroupBox2.Controls.Add(Me.UltraButton1)
        Me.ultraGroupBox2.Controls.Add(Me.lbAdded)
        Me.ultraGroupBox2.Location = New System.Drawing.Point(4, 4)
        Me.ultraGroupBox2.Name = "ultraGroupBox2"
        Me.ultraGroupBox2.Size = New System.Drawing.Size(518, 426)
        Me.ultraGroupBox2.TabIndex = 2
        '
        'lbSelected
        '
        Me.lbSelected.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbSelected.FormattingEnabled = True
        Me.lbSelected.ItemHeight = 16
        Me.lbSelected.Location = New System.Drawing.Point(6, 8)
        Me.lbSelected.Name = "lbSelected"
        Me.lbSelected.Size = New System.Drawing.Size(233, 404)
        Me.lbSelected.TabIndex = 9
        '
        'UltraButton3
        '
        Me.UltraButton3.Location = New System.Drawing.Point(240, 142)
        Me.UltraButton3.Name = "UltraButton3"
        Me.UltraButton3.Size = New System.Drawing.Size(34, 28)
        Me.UltraButton3.TabIndex = 8
        Me.UltraButton3.Text = "<<"
        Me.UltraButton3.Visible = False
        '
        'UltraButton4
        '
        Me.UltraButton4.Location = New System.Drawing.Point(240, 71)
        Me.UltraButton4.Name = "UltraButton4"
        Me.UltraButton4.Size = New System.Drawing.Size(34, 28)
        Me.UltraButton4.TabIndex = 7
        Me.UltraButton4.Text = "<"
        '
        'UltraButton2
        '
        Me.UltraButton2.Location = New System.Drawing.Point(240, 176)
        Me.UltraButton2.Name = "UltraButton2"
        Me.UltraButton2.Size = New System.Drawing.Size(34, 28)
        Me.UltraButton2.TabIndex = 6
        Me.UltraButton2.Text = ">>"
        Me.UltraButton2.Visible = False
        '
        'UltraButton1
        '
        Me.UltraButton1.Location = New System.Drawing.Point(240, 42)
        Me.UltraButton1.Name = "UltraButton1"
        Me.UltraButton1.Size = New System.Drawing.Size(34, 28)
        Me.UltraButton1.TabIndex = 5
        Me.UltraButton1.Text = ">"
        '
        'lbAdded
        '
        Me.lbAdded.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbAdded.FormattingEnabled = True
        Me.lbAdded.ItemHeight = 16
        Me.lbAdded.Location = New System.Drawing.Point(276, 11)
        Me.lbAdded.Name = "lbAdded"
        Me.lbAdded.Size = New System.Drawing.Size(233, 404)
        Me.lbAdded.TabIndex = 2
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(358, 436)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 28)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(439, 436)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 28)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        '
        'frmSelectSerialNo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(526, 473)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.ultraGroupBox2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSelectSerialNo"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Serial No"
        CType(Me.ultraGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ultraGroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ultraGroupBox2 As Infragistics.Win.Misc.UltraGroupBox
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraButton3 As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraButton4 As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraButton2 As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraButton1 As Infragistics.Win.Misc.UltraButton
    Public WithEvents lbAdded As System.Windows.Forms.ListBox
    Friend WithEvents lbSelected As System.Windows.Forms.ListBox
End Class
