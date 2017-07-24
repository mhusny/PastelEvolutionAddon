<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSerialNo
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSerialNo))
        Me.ultraGroupBox1 = New Infragistics.Win.Misc.UltraGroupBox
        Me.lbl2 = New Infragistics.Win.Misc.UltraLabel
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel
        Me.lbl1 = New Infragistics.Win.Misc.UltraLabel
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel
        Me.txtPad = New System.Windows.Forms.NumericUpDown
        Me.txtNumber = New Infragistics.Win.UltraWinEditors.UltraNumericEditor
        Me.btnClear = New Infragistics.Win.Misc.UltraButton
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel
        Me.txtGenerate = New Infragistics.Win.UltraWinEditors.UltraNumericEditor
        Me.btnAdd = New Infragistics.Win.Misc.UltraButton
        Me.btnGenerate = New Infragistics.Win.Misc.UltraButton
        Me.txtString = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.ultraLabel1 = New Infragistics.Win.Misc.UltraLabel
        Me.lbSelected = New System.Windows.Forms.ListBox
        Me.ultraGroupBox2 = New Infragistics.Win.Misc.UltraGroupBox
        Me.btnOK = New Infragistics.Win.Misc.UltraButton
        Me.btnCancel = New Infragistics.Win.Misc.UltraButton
        Me.txtdash = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        CType(Me.ultraGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ultraGroupBox1.SuspendLayout()
        CType(Me.txtPad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGenerate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtString, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ultraGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ultraGroupBox2.SuspendLayout()
        CType(Me.txtdash, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ultraGroupBox1
        '
        Me.ultraGroupBox1.Controls.Add(Me.txtdash)
        Me.ultraGroupBox1.Controls.Add(Me.lbl2)
        Me.ultraGroupBox1.Controls.Add(Me.UltraLabel5)
        Me.ultraGroupBox1.Controls.Add(Me.lbl1)
        Me.ultraGroupBox1.Controls.Add(Me.UltraLabel3)
        Me.ultraGroupBox1.Controls.Add(Me.txtPad)
        Me.ultraGroupBox1.Controls.Add(Me.txtNumber)
        Me.ultraGroupBox1.Controls.Add(Me.btnClear)
        Me.ultraGroupBox1.Controls.Add(Me.UltraLabel2)
        Me.ultraGroupBox1.Controls.Add(Me.txtGenerate)
        Me.ultraGroupBox1.Controls.Add(Me.btnAdd)
        Me.ultraGroupBox1.Controls.Add(Me.btnGenerate)
        Me.ultraGroupBox1.Controls.Add(Me.txtString)
        Me.ultraGroupBox1.Controls.Add(Me.ultraLabel1)
        Me.ultraGroupBox1.Location = New System.Drawing.Point(5, 2)
        Me.ultraGroupBox1.Name = "ultraGroupBox1"
        Me.ultraGroupBox1.Size = New System.Drawing.Size(518, 115)
        Me.ultraGroupBox1.TabIndex = 0
        Me.ultraGroupBox1.Text = "  Serial Number"
        '
        'lbl2
        '
        Appearance1.TextVAlignAsString = "Middle"
        Me.lbl2.Appearance = Appearance1
        Me.lbl2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl2.Location = New System.Drawing.Point(367, 53)
        Me.lbl2.Name = "lbl2"
        Me.lbl2.Size = New System.Drawing.Size(146, 23)
        Me.lbl2.TabIndex = 12
        Me.lbl2.Text = "Serial Number"
        '
        'UltraLabel5
        '
        Appearance2.TextVAlignAsString = "Middle"
        Me.UltraLabel5.Appearance = Appearance2
        Me.UltraLabel5.Location = New System.Drawing.Point(261, 53)
        Me.UltraLabel5.Name = "UltraLabel5"
        Me.UltraLabel5.Size = New System.Drawing.Size(100, 23)
        Me.UltraLabel5.TabIndex = 11
        Me.UltraLabel5.Text = "Unprocessed SN"
        '
        'lbl1
        '
        Appearance3.TextVAlignAsString = "Middle"
        Me.lbl1.Appearance = Appearance3
        Me.lbl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl1.Location = New System.Drawing.Point(119, 52)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(146, 23)
        Me.lbl1.TabIndex = 10
        Me.lbl1.Text = "Serial Number"
        '
        'UltraLabel3
        '
        Appearance4.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance4
        Me.UltraLabel3.Location = New System.Drawing.Point(13, 52)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(100, 23)
        Me.UltraLabel3.TabIndex = 9
        Me.UltraLabel3.Text = "Serial Number"
        '
        'txtPad
        '
        Me.txtPad.Location = New System.Drawing.Point(366, 24)
        Me.txtPad.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.txtPad.Name = "txtPad"
        Me.txtPad.Size = New System.Drawing.Size(120, 20)
        Me.txtPad.TabIndex = 3
        Me.txtPad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNumber
        '
        Me.txtNumber.AutoSize = False
        Me.txtNumber.FormatString = "0"
        Me.txtNumber.Location = New System.Drawing.Point(255, 24)
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.NullText = "0"
        Me.txtNumber.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.txtNumber.Size = New System.Drawing.Size(106, 20)
        Me.txtNumber.TabIndex = 2
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(175, 86)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 6
        Me.btnClear.Text = "Clear"
        '
        'UltraLabel2
        '
        Appearance5.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance5
        Me.UltraLabel2.Location = New System.Drawing.Point(303, 86)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(58, 23)
        Me.UltraLabel2.TabIndex = 5
        Me.UltraLabel2.Text = "Generate"
        '
        'txtGenerate
        '
        Me.txtGenerate.FormatString = "0"
        Me.txtGenerate.Location = New System.Drawing.Point(380, 86)
        Me.txtGenerate.Name = "txtGenerate"
        Me.txtGenerate.NullText = "0"
        Me.txtGenerate.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.txtGenerate.Size = New System.Drawing.Size(106, 21)
        Me.txtGenerate.TabIndex = 4
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(13, 86)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 4
        Me.btnAdd.Text = "Add"
        '
        'btnGenerate
        '
        Me.btnGenerate.Location = New System.Drawing.Point(94, 86)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(75, 23)
        Me.btnGenerate.TabIndex = 5
        Me.btnGenerate.Text = "Generate"
        '
        'txtString
        '
        Me.txtString.AutoSize = False
        Me.txtString.Enabled = False
        Me.txtString.Location = New System.Drawing.Point(119, 24)
        Me.txtString.MaxLength = 20
        Me.txtString.Name = "txtString"
        Me.txtString.Size = New System.Drawing.Size(118, 20)
        Me.txtString.TabIndex = 0
        '
        'ultraLabel1
        '
        Appearance6.TextVAlignAsString = "Middle"
        Me.ultraLabel1.Appearance = Appearance6
        Me.ultraLabel1.Location = New System.Drawing.Point(13, 23)
        Me.ultraLabel1.Name = "ultraLabel1"
        Me.ultraLabel1.Size = New System.Drawing.Size(100, 23)
        Me.ultraLabel1.TabIndex = 0
        Me.ultraLabel1.Text = "Serial Number"
        '
        'lbSelected
        '
        Me.lbSelected.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbSelected.FormattingEnabled = True
        Me.lbSelected.ItemHeight = 16
        Me.lbSelected.Location = New System.Drawing.Point(6, 15)
        Me.lbSelected.Name = "lbSelected"
        Me.lbSelected.Size = New System.Drawing.Size(506, 404)
        Me.lbSelected.TabIndex = 1
        '
        'ultraGroupBox2
        '
        Me.ultraGroupBox2.Controls.Add(Me.lbSelected)
        Me.ultraGroupBox2.Location = New System.Drawing.Point(5, 123)
        Me.ultraGroupBox2.Name = "ultraGroupBox2"
        Me.ultraGroupBox2.Size = New System.Drawing.Size(518, 426)
        Me.ultraGroupBox2.TabIndex = 2
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(336, 555)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 28)
        Me.btnOK.TabIndex = 7
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(417, 555)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 28)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Text = "Cancel"
        '
        'txtdash
        '
        Me.txtdash.AutoSize = False
        Me.txtdash.Location = New System.Drawing.Point(238, 24)
        Me.txtdash.MaxLength = 20
        Me.txtdash.Name = "txtdash"
        Me.txtdash.Size = New System.Drawing.Size(15, 20)
        Me.txtdash.TabIndex = 1
        Me.txtdash.Text = "-"
        '
        'frmSerialNo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(526, 587)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.ultraGroupBox2)
        Me.Controls.Add(Me.ultraGroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSerialNo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Serial No"
        CType(Me.ultraGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ultraGroupBox1.ResumeLayout(False)
        Me.ultraGroupBox1.PerformLayout()
        CType(Me.txtPad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGenerate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtString, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ultraGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ultraGroupBox2.ResumeLayout(False)
        CType(Me.txtdash, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ultraGroupBox1 As Infragistics.Win.Misc.UltraGroupBox
    Friend WithEvents ultraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents btnGenerate As Infragistics.Win.Misc.UltraButton
    Friend WithEvents txtString As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtGenerate As Infragistics.Win.UltraWinEditors.UltraNumericEditor
    Friend WithEvents btnAdd As Infragistics.Win.Misc.UltraButton
    Friend WithEvents lbSelected As System.Windows.Forms.ListBox
    Friend WithEvents ultraGroupBox2 As Infragistics.Win.Misc.UltraGroupBox
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnCancel As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnClear As Infragistics.Win.Misc.UltraButton
    Friend WithEvents txtNumber As Infragistics.Win.UltraWinEditors.UltraNumericEditor
    Friend WithEvents txtPad As System.Windows.Forms.NumericUpDown
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents lbl1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents lbl2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtdash As Infragistics.Win.UltraWinEditors.UltraTextEditor
End Class
