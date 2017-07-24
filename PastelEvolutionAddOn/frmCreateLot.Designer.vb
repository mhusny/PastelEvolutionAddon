<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateLot
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbLot = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpExpDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.txtLot = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraButton1 = New Infragistics.Win.Misc.UltraButton()
        Me.UltraButton2 = New Infragistics.Win.Misc.UltraButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbUE = New System.Windows.Forms.RadioButton()
        Me.rbCN = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbLot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpExpDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbLot)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtpExpDate)
        Me.GroupBox1.Controls.Add(Me.txtLot)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(3, 53)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(241, 87)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Lot Number"
        '
        'cmbLot
        '
        Me.cmbLot.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbLot.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn
        Appearance1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cmbLot.DisplayLayout.Override.CellAppearance = Appearance1
        Appearance2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cmbLot.DisplayLayout.Override.RowAppearance = Appearance2
        Me.cmbLot.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
        Me.cmbLot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLot.Location = New System.Drawing.Point(84, 12)
        Me.cmbLot.Name = "cmbLot"
        Me.cmbLot.Size = New System.Drawing.Size(142, 22)
        Me.cmbLot.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Expiry Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(22, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Lot"
        '
        'dtpExpDate
        '
        Me.dtpExpDate.AutoSize = False
        Me.dtpExpDate.DateTime = New Date(2009, 11, 20, 0, 0, 0, 0)
        Me.dtpExpDate.Location = New System.Drawing.Point(84, 46)
        Me.dtpExpDate.Name = "dtpExpDate"
        Me.dtpExpDate.Size = New System.Drawing.Size(142, 22)
        Me.dtpExpDate.TabIndex = 9
        Me.dtpExpDate.Value = New Date(2009, 11, 20, 0, 0, 0, 0)
        '
        'txtLot
        '
        Me.txtLot.AutoSize = False
        Me.txtLot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLot.Location = New System.Drawing.Point(84, 13)
        Me.txtLot.Name = "txtLot"
        Me.txtLot.Size = New System.Drawing.Size(142, 21)
        Me.txtLot.TabIndex = 3
        '
        'UltraButton1
        '
        Me.UltraButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraButton1.Location = New System.Drawing.Point(106, 146)
        Me.UltraButton1.Name = "UltraButton1"
        Me.UltraButton1.Size = New System.Drawing.Size(66, 26)
        Me.UltraButton1.TabIndex = 1
        Me.UltraButton1.Text = "OK"
        '
        'UltraButton2
        '
        Me.UltraButton2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraButton2.Location = New System.Drawing.Point(178, 146)
        Me.UltraButton2.Name = "UltraButton2"
        Me.UltraButton2.Size = New System.Drawing.Size(66, 26)
        Me.UltraButton2.TabIndex = 11
        Me.UltraButton2.TabStop = False
        Me.UltraButton2.Text = "Cancel"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbUE)
        Me.GroupBox2.Controls.Add(Me.rbCN)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(241, 44)
        Me.GroupBox2.TabIndex = 12
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "New/Existing"
        '
        'rbUE
        '
        Me.rbUE.AutoSize = True
        Me.rbUE.Location = New System.Drawing.Point(143, 19)
        Me.rbUE.Name = "rbUE"
        Me.rbUE.Size = New System.Drawing.Size(83, 17)
        Me.rbUE.TabIndex = 1
        Me.rbUE.Text = "Use Existing"
        Me.rbUE.UseVisualStyleBackColor = True
        '
        'rbCN
        '
        Me.rbCN.AutoSize = True
        Me.rbCN.Checked = True
        Me.rbCN.Location = New System.Drawing.Point(9, 19)
        Me.rbCN.Name = "rbCN"
        Me.rbCN.Size = New System.Drawing.Size(81, 17)
        Me.rbCN.TabIndex = 0
        Me.rbCN.TabStop = True
        Me.rbCN.Text = "Create New"
        Me.rbCN.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 152)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(22, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Lot"
        Me.Label3.Visible = False
        '
        'frmCreateLot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(248, 182)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.UltraButton2)
        Me.Controls.Add(Me.UltraButton1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmCreateLot"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Create Lot"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbLot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpExpDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents UltraButton2 As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraButton1 As Infragistics.Win.Misc.UltraButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtLot As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents dtpExpDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbLot As Infragistics.Win.UltraWinGrid.UltraCombo
    Public WithEvents rbUE As System.Windows.Forms.RadioButton
    Public WithEvents rbCN As System.Windows.Forms.RadioButton
End Class
