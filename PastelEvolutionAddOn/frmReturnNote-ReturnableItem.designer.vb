<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReturnNote
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
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
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReturnNote))
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.UG = New Infragistics.Win.UltraWinGrid.UltraGrid
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtRef = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.UltraLabel6 = New Infragistics.Win.Misc.UltraLabel
        Me.UltraLabel5 = New Infragistics.Win.Misc.UltraLabel
        Me.cmbTxnTypeCom2 = New Infragistics.Win.UltraWinGrid.UltraCombo
        Me.cmbCom2 = New System.Windows.Forms.ComboBox
        Me.UltraLabel4 = New Infragistics.Win.Misc.UltraLabel
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel
        Me.cmbTxnTypeCom1 = New Infragistics.Win.UltraWinGrid.UltraCombo
        Me.dtpDate = New System.Windows.Forms.DateTimePicker
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbUpdate = New System.Windows.Forms.ToolStripButton
        Me.tsbDiscard = New System.Windows.Forms.ToolStripButton
        Me.tsbClose = New System.Windows.Forms.ToolStripButton
        Me.GroupBox2.SuspendLayout()
        CType(Me.UG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtRef, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTxnTypeCom2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbTxnTypeCom1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraLabel3
        '
        Appearance1.BackColor = System.Drawing.Color.Teal
        Appearance1.BackColor2 = System.Drawing.Color.DarkCyan
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
        Appearance1.ForeColor = System.Drawing.Color.White
        Appearance1.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance1
        Me.UltraLabel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraLabel3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel3.Location = New System.Drawing.Point(0, 28)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(748, 21)
        Me.UltraLabel3.TabIndex = 20
        Me.UltraLabel3.Text = " Item Return Note "
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.UG)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(3, 163)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(740, 355)
        Me.GroupBox2.TabIndex = 19
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Material Issue Note Details"
        '
        'UG
        '
        Me.UG.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.UG.DisplayLayout.Appearance = Appearance2
        Me.UG.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns
        Me.UG.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.UG.DisplayLayout.Override.CellAppearance = Appearance3
        Me.UG.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Appearance4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.UG.DisplayLayout.Override.RowAppearance = Appearance4
        Me.UG.Location = New System.Drawing.Point(6, 20)
        Me.UG.Name = "UG"
        Me.UG.Size = New System.Drawing.Size(728, 329)
        Me.UG.TabIndex = 6
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtRef)
        Me.GroupBox1.Controls.Add(Me.UltraLabel6)
        Me.GroupBox1.Controls.Add(Me.UltraLabel5)
        Me.GroupBox1.Controls.Add(Me.cmbTxnTypeCom2)
        Me.GroupBox1.Controls.Add(Me.cmbCom2)
        Me.GroupBox1.Controls.Add(Me.UltraLabel4)
        Me.GroupBox1.Controls.Add(Me.UltraLabel1)
        Me.GroupBox1.Controls.Add(Me.cmbTxnTypeCom1)
        Me.GroupBox1.Controls.Add(Me.dtpDate)
        Me.GroupBox1.Controls.Add(Me.UltraLabel2)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(4, 49)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(738, 108)
        Me.GroupBox1.TabIndex = 18
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Material Issue Note "
        '
        'txtRef
        '
        Me.txtRef.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRef.Location = New System.Drawing.Point(604, 43)
        Me.txtRef.Name = "txtRef"
        Me.txtRef.Size = New System.Drawing.Size(115, 21)
        Me.txtRef.TabIndex = 40
        '
        'UltraLabel6
        '
        Me.UltraLabel6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance5.BackColor = System.Drawing.Color.Teal
        Appearance5.BackColor2 = System.Drawing.Color.DarkCyan
        Appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
        Appearance5.BorderColor = System.Drawing.Color.DarkCyan
        Appearance5.ForeColor = System.Drawing.Color.White
        Appearance5.TextVAlignAsString = "Middle"
        Me.UltraLabel6.Appearance = Appearance5
        Me.UltraLabel6.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraLabel6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel6.Location = New System.Drawing.Point(498, 43)
        Me.UltraLabel6.Name = "UltraLabel6"
        Me.UltraLabel6.Size = New System.Drawing.Size(100, 21)
        Me.UltraLabel6.TabIndex = 39
        Me.UltraLabel6.Text = "Doc Ref"
        '
        'UltraLabel5
        '
        Me.UltraLabel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance6.BackColor = System.Drawing.Color.Teal
        Appearance6.BackColor2 = System.Drawing.Color.DarkCyan
        Appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
        Appearance6.BorderColor = System.Drawing.Color.DarkCyan
        Appearance6.ForeColor = System.Drawing.Color.White
        Appearance6.TextVAlignAsString = "Middle"
        Me.UltraLabel5.Appearance = Appearance6
        Me.UltraLabel5.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraLabel5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel5.Location = New System.Drawing.Point(6, 67)
        Me.UltraLabel5.Name = "UltraLabel5"
        Me.UltraLabel5.Size = New System.Drawing.Size(100, 21)
        Me.UltraLabel5.TabIndex = 38
        Me.UltraLabel5.Text = "Transaction Type"
        '
        'cmbTxnTypeCom2
        '
        Me.cmbTxnTypeCom2.AutoSize = False
        Me.cmbTxnTypeCom2.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbTxnTypeCom2.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
        Me.cmbTxnTypeCom2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTxnTypeCom2.Location = New System.Drawing.Point(112, 67)
        Me.cmbTxnTypeCom2.Name = "cmbTxnTypeCom2"
        Me.cmbTxnTypeCom2.Size = New System.Drawing.Size(186, 21)
        Me.cmbTxnTypeCom2.TabIndex = 37
        '
        'cmbCom2
        '
        Me.cmbCom2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCom2.FormattingEnabled = True
        Me.cmbCom2.Location = New System.Drawing.Point(112, 43)
        Me.cmbCom2.Name = "cmbCom2"
        Me.cmbCom2.Size = New System.Drawing.Size(210, 21)
        Me.cmbCom2.TabIndex = 36
        '
        'UltraLabel4
        '
        Me.UltraLabel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance7.BackColor = System.Drawing.Color.Teal
        Appearance7.BackColor2 = System.Drawing.Color.DarkCyan
        Appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
        Appearance7.BorderColor = System.Drawing.Color.DarkCyan
        Appearance7.ForeColor = System.Drawing.Color.White
        Appearance7.TextVAlignAsString = "Middle"
        Me.UltraLabel4.Appearance = Appearance7
        Me.UltraLabel4.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraLabel4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel4.Location = New System.Drawing.Point(6, 43)
        Me.UltraLabel4.Name = "UltraLabel4"
        Me.UltraLabel4.Size = New System.Drawing.Size(100, 23)
        Me.UltraLabel4.TabIndex = 35
        Me.UltraLabel4.Text = "Update To Loc"
        '
        'UltraLabel1
        '
        Me.UltraLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance8.BackColor = System.Drawing.Color.Teal
        Appearance8.BackColor2 = System.Drawing.Color.DarkCyan
        Appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
        Appearance8.BorderColor = System.Drawing.Color.DarkCyan
        Appearance8.ForeColor = System.Drawing.Color.White
        Appearance8.TextVAlignAsString = "Middle"
        Me.UltraLabel1.Appearance = Appearance8
        Me.UltraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel1.Location = New System.Drawing.Point(6, 21)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(100, 21)
        Me.UltraLabel1.TabIndex = 34
        Me.UltraLabel1.Text = "Transaction Type"
        '
        'cmbTxnTypeCom1
        '
        Me.cmbTxnTypeCom1.AutoSize = False
        Me.cmbTxnTypeCom1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbTxnTypeCom1.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
        Me.cmbTxnTypeCom1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTxnTypeCom1.Location = New System.Drawing.Point(112, 21)
        Me.cmbTxnTypeCom1.Name = "cmbTxnTypeCom1"
        Me.cmbTxnTypeCom1.Size = New System.Drawing.Size(186, 21)
        Me.cmbTxnTypeCom1.TabIndex = 33
        '
        'dtpDate
        '
        Me.dtpDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate.Location = New System.Drawing.Point(604, 21)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(115, 20)
        Me.dtpDate.TabIndex = 32
        '
        'UltraLabel2
        '
        Me.UltraLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance9.BackColor = System.Drawing.Color.Teal
        Appearance9.BackColor2 = System.Drawing.Color.DarkCyan
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
        Appearance9.BorderColor = System.Drawing.Color.DarkCyan
        Appearance9.ForeColor = System.Drawing.Color.White
        Appearance9.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance9
        Me.UltraLabel2.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel2.Location = New System.Drawing.Point(498, 21)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(100, 21)
        Me.UltraLabel2.TabIndex = 31
        Me.UltraLabel2.Text = "Return Date "
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbUpdate, Me.tsbDiscard, Me.tsbClose})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip1.Size = New System.Drawing.Size(748, 28)
        Me.ToolStrip1.TabIndex = 17
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbUpdate
        '
        Me.tsbUpdate.AutoSize = False
        Me.tsbUpdate.Image = CType(resources.GetObject("tsbUpdate.Image"), System.Drawing.Image)
        Me.tsbUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbUpdate.Name = "tsbUpdate"
        Me.tsbUpdate.Size = New System.Drawing.Size(80, 22)
        Me.tsbUpdate.Text = " Update"
        '
        'tsbDiscard
        '
        Me.tsbDiscard.AutoSize = False
        Me.tsbDiscard.Image = CType(resources.GetObject("tsbDiscard.Image"), System.Drawing.Image)
        Me.tsbDiscard.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbDiscard.Name = "tsbDiscard"
        Me.tsbDiscard.Size = New System.Drawing.Size(75, 22)
        Me.tsbDiscard.Text = " Discard"
        '
        'tsbClose
        '
        Me.tsbClose.AutoSize = False
        Me.tsbClose.Image = CType(resources.GetObject("tsbClose.Image"), System.Drawing.Image)
        Me.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbClose.Name = "tsbClose"
        Me.tsbClose.Size = New System.Drawing.Size(70, 22)
        Me.tsbClose.Text = "Close"
        '
        'frmReturnNote
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(748, 521)
        Me.Controls.Add(Me.UltraLabel3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmReturnNote"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ReturnNote "
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.UG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtRef, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTxnTypeCom2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbTxnTypeCom1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents UG As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbUpdate As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbDiscard As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraLabel4 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmbTxnTypeCom1 As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel5 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmbTxnTypeCom2 As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents cmbCom2 As System.Windows.Forms.ComboBox
    Friend WithEvents UltraLabel6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtRef As Infragistics.Win.UltraWinEditors.UltraTextEditor
End Class
