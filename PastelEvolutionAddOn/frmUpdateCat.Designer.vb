<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateCat
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdateCat))
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbBin = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.txtSimpleCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.ultraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmbCat = New System.Windows.Forms.ComboBox()
        Me.toolStrip1.SuspendLayout()
        CType(Me.txtSimpleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'toolStrip1
        '
        Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbBin, Me.tsbExit})
        Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.toolStrip1.Name = "toolStrip1"
        Me.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.toolStrip1.Size = New System.Drawing.Size(447, 25)
        Me.toolStrip1.TabIndex = 27
        Me.toolStrip1.Text = "ToolStrip1"
        '
        'tsbBin
        '
        Me.tsbBin.Image = CType(resources.GetObject("tsbBin.Image"), System.Drawing.Image)
        Me.tsbBin.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbBin.Name = "tsbBin"
        Me.tsbBin.Size = New System.Drawing.Size(110, 22)
        Me.tsbBin.Text = "Update Category"
        '
        'tsbExit
        '
        Me.tsbExit.Image = CType(resources.GetObject("tsbExit.Image"), System.Drawing.Image)
        Me.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExit.Name = "tsbExit"
        Me.tsbExit.Size = New System.Drawing.Size(45, 22)
        Me.tsbExit.Text = "Exit"
        '
        'UltraLabel2
        '
        Appearance1.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance1
        Me.UltraLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel2.Location = New System.Drawing.Point(4, 66)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(86, 22)
        Me.UltraLabel2.TabIndex = 26
        Me.UltraLabel2.Text = "Item Category"
        '
        'txtSimpleCode
        '
        Me.txtSimpleCode.AutoSize = False
        Me.txtSimpleCode.Enabled = False
        Me.txtSimpleCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSimpleCode.Location = New System.Drawing.Point(96, 38)
        Me.txtSimpleCode.Name = "txtSimpleCode"
        Me.txtSimpleCode.ReadOnly = True
        Me.txtSimpleCode.Size = New System.Drawing.Size(333, 22)
        Me.txtSimpleCode.TabIndex = 23
        '
        'ultraLabel1
        '
        Appearance2.TextVAlignAsString = "Middle"
        Me.ultraLabel1.Appearance = Appearance2
        Me.ultraLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ultraLabel1.Location = New System.Drawing.Point(4, 38)
        Me.ultraLabel1.Name = "ultraLabel1"
        Me.ultraLabel1.Size = New System.Drawing.Size(86, 22)
        Me.ultraLabel1.TabIndex = 24
        Me.ultraLabel1.Text = "Inventory Item "
        '
        'cmbCat
        '
        Me.cmbCat.FormattingEnabled = True
        Me.cmbCat.Items.AddRange(New Object() {"R (SLOW)", "G (MEDIUM)", "Y (FAST)"})
        Me.cmbCat.Location = New System.Drawing.Point(96, 67)
        Me.cmbCat.Name = "cmbCat"
        Me.cmbCat.Size = New System.Drawing.Size(121, 21)
        Me.cmbCat.TabIndex = 28
        '
        'frmUpdateCat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(447, 105)
        Me.Controls.Add(Me.cmbCat)
        Me.Controls.Add(Me.toolStrip1)
        Me.Controls.Add(Me.UltraLabel2)
        Me.Controls.Add(Me.txtSimpleCode)
        Me.Controls.Add(Me.ultraLabel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmUpdateCat"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update Category"
        Me.toolStrip1.ResumeLayout(False)
        Me.toolStrip1.PerformLayout()
        CType(Me.txtSimpleCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents toolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbBin As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtSimpleCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents ultraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmbCat As System.Windows.Forms.ComboBox
End Class
