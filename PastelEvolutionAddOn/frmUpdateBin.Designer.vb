<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateBin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdateBin))
        Me.txtSimpleCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.ultraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.txtBin = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbBin = New System.Windows.Forms.ToolStripButton()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        CType(Me.txtSimpleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.toolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtSimpleCode
        '
        Me.txtSimpleCode.AutoSize = False
        Me.txtSimpleCode.Enabled = False
        Me.txtSimpleCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSimpleCode.Location = New System.Drawing.Point(104, 47)
        Me.txtSimpleCode.Name = "txtSimpleCode"
        Me.txtSimpleCode.ReadOnly = True
        Me.txtSimpleCode.Size = New System.Drawing.Size(333, 22)
        Me.txtSimpleCode.TabIndex = 18
        '
        'ultraLabel1
        '
        Appearance1.TextVAlignAsString = "Middle"
        Me.ultraLabel1.Appearance = Appearance1
        Me.ultraLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ultraLabel1.Location = New System.Drawing.Point(12, 47)
        Me.ultraLabel1.Name = "ultraLabel1"
        Me.ultraLabel1.Size = New System.Drawing.Size(86, 22)
        Me.ultraLabel1.TabIndex = 19
        Me.ultraLabel1.Text = "Inventory Item "
        '
        'txtBin
        '
        Me.txtBin.AutoSize = False
        Me.txtBin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBin.Location = New System.Drawing.Point(104, 75)
        Me.txtBin.Name = "txtBin"
        Me.txtBin.Size = New System.Drawing.Size(101, 22)
        Me.txtBin.TabIndex = 20
        '
        'UltraLabel2
        '
        Appearance2.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance2
        Me.UltraLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel2.Location = New System.Drawing.Point(12, 75)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(86, 22)
        Me.UltraLabel2.TabIndex = 21
        Me.UltraLabel2.Text = "Bin Location"
        '
        'toolStrip1
        '
        Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbBin, Me.tsbExit})
        Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.toolStrip1.Name = "toolStrip1"
        Me.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.toolStrip1.Size = New System.Drawing.Size(449, 25)
        Me.toolStrip1.TabIndex = 22
        Me.toolStrip1.Text = "ToolStrip1"
        '
        'tsbBin
        '
        Me.tsbBin.Image = CType(resources.GetObject("tsbBin.Image"), System.Drawing.Image)
        Me.tsbBin.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbBin.Name = "tsbBin"
        Me.tsbBin.Size = New System.Drawing.Size(79, 22)
        Me.tsbBin.Text = "Update Bin"
        '
        'tsbExit
        '
        Me.tsbExit.Image = CType(resources.GetObject("tsbExit.Image"), System.Drawing.Image)
        Me.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExit.Name = "tsbExit"
        Me.tsbExit.Size = New System.Drawing.Size(45, 22)
        Me.tsbExit.Text = "Exit"
        '
        'frmUpdateBin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(449, 122)
        Me.Controls.Add(Me.toolStrip1)
        Me.Controls.Add(Me.txtBin)
        Me.Controls.Add(Me.UltraLabel2)
        Me.Controls.Add(Me.txtSimpleCode)
        Me.Controls.Add(Me.ultraLabel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUpdateBin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update Bin"
        CType(Me.txtSimpleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.toolStrip1.ResumeLayout(False)
        Me.toolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtSimpleCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents ultraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtBin As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents toolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbBin As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
End Class
