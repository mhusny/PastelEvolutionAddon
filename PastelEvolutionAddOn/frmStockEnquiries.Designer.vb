<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStockEnquiries
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStockEnquiries))
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.tsbConnect = New System.Windows.Forms.ToolStripButton()
        Me.tsbSaveMySetting = New System.Windows.Forms.ToolStripButton()
        Me.tsbExport = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.tsbUpdate = New System.Windows.Forms.ToolStripButton()
        Me.lbl6 = New Infragistics.Win.Misc.UltraLabel()
        Me.ugExcelExporter = New Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(Me.components)
        Me.txtCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txtSimpleCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.cmbInventoryItem = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.ultraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.txtItem = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UG = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.toolStrip1.SuspendLayout()
        CType(Me.txtCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSimpleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbInventoryItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'toolStrip1
        '
        Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbExit, Me.tsbConnect, Me.tsbSaveMySetting, Me.tsbExport, Me.ToolStripButton1, Me.tsbUpdate})
        Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.toolStrip1.Name = "toolStrip1"
        Me.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.toolStrip1.Size = New System.Drawing.Size(696, 25)
        Me.toolStrip1.TabIndex = 0
        Me.toolStrip1.Text = "ToolStrip1"
        '
        'tsbExit
        '
        Me.tsbExit.Image = CType(resources.GetObject("tsbExit.Image"), System.Drawing.Image)
        Me.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExit.Name = "tsbExit"
        Me.tsbExit.Size = New System.Drawing.Size(45, 22)
        Me.tsbExit.Text = "Exit"
        '
        'tsbConnect
        '
        Me.tsbConnect.Image = CType(resources.GetObject("tsbConnect.Image"), System.Drawing.Image)
        Me.tsbConnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbConnect.Name = "tsbConnect"
        Me.tsbConnect.Size = New System.Drawing.Size(52, 22)
        Me.tsbConnect.Text = "View"
        '
        'tsbSaveMySetting
        '
        Me.tsbSaveMySetting.Image = CType(resources.GetObject("tsbSaveMySetting.Image"), System.Drawing.Image)
        Me.tsbSaveMySetting.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSaveMySetting.Name = "tsbSaveMySetting"
        Me.tsbSaveMySetting.Size = New System.Drawing.Size(116, 22)
        Me.tsbSaveMySetting.Text = "Save My Settings"
        '
        'tsbExport
        '
        Me.tsbExport.Image = CType(resources.GetObject("tsbExport.Image"), System.Drawing.Image)
        Me.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExport.Name = "tsbExport"
        Me.tsbExport.Size = New System.Drawing.Size(76, 22)
        Me.tsbExport.Text = "Save Grid"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(92, 22)
        Me.ToolStripButton1.Text = "Stock Count"
        '
        'tsbUpdate
        '
        Me.tsbUpdate.Enabled = False
        Me.tsbUpdate.Image = CType(resources.GetObject("tsbUpdate.Image"), System.Drawing.Image)
        Me.tsbUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbUpdate.Name = "tsbUpdate"
        Me.tsbUpdate.Size = New System.Drawing.Size(108, 22)
        Me.tsbUpdate.Text = "Update Max Lvl"
        '
        'lbl6
        '
        Appearance1.BackColor = System.Drawing.Color.Maroon
        Appearance1.BackColor2 = System.Drawing.Color.DarkRed
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
        Appearance1.FontData.BoldAsString = "True"
        Appearance1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Appearance1.TextVAlignAsString = "Middle"
        Me.lbl6.Appearance = Appearance1
        Me.lbl6.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(177, Byte))
        Me.lbl6.Location = New System.Drawing.Point(0, 25)
        Me.lbl6.Name = "lbl6"
        Me.lbl6.Size = New System.Drawing.Size(696, 18)
        Me.lbl6.TabIndex = 5
        Me.lbl6.Text = "Stock Enquiries"
        '
        'txtCode
        '
        Appearance2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCode.Appearance = Appearance2
        Me.txtCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.Location = New System.Drawing.Point(393, 50)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.ReadOnly = True
        Me.txtCode.Size = New System.Drawing.Size(99, 21)
        Me.txtCode.TabIndex = 14
        '
        'txtSimpleCode
        '
        Appearance3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSimpleCode.Appearance = Appearance3
        Me.txtSimpleCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSimpleCode.Location = New System.Drawing.Point(269, 50)
        Me.txtSimpleCode.Name = "txtSimpleCode"
        Me.txtSimpleCode.ReadOnly = True
        Me.txtSimpleCode.Size = New System.Drawing.Size(118, 21)
        Me.txtSimpleCode.TabIndex = 13
        '
        'cmbInventoryItem
        '
        Me.cmbInventoryItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Appearance4.BorderColor = System.Drawing.Color.Silver
        Me.cmbInventoryItem.DisplayLayout.Appearance = Appearance4
        Me.cmbInventoryItem.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn
        Appearance5.BackColor = System.Drawing.Color.PapayaWhip
        Me.cmbInventoryItem.DisplayLayout.Override.RowAlternateAppearance = Appearance5
        Me.cmbInventoryItem.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
        Me.cmbInventoryItem.Location = New System.Drawing.Point(538, 49)
        Me.cmbInventoryItem.Name = "cmbInventoryItem"
        Me.cmbInventoryItem.Size = New System.Drawing.Size(121, 22)
        Me.cmbInventoryItem.TabIndex = 9
        Me.cmbInventoryItem.Visible = False
        '
        'ultraLabel1
        '
        Appearance6.TextVAlignAsString = "Middle"
        Me.ultraLabel1.Appearance = Appearance6
        Me.ultraLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ultraLabel1.Location = New System.Drawing.Point(12, 49)
        Me.ultraLabel1.Name = "ultraLabel1"
        Me.ultraLabel1.Size = New System.Drawing.Size(146, 22)
        Me.ultraLabel1.TabIndex = 8
        Me.ultraLabel1.Text = " Inventory Item "
        '
        'txtItem
        '
        Appearance7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtItem.Appearance = Appearance7
        Me.txtItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.Location = New System.Drawing.Point(118, 49)
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(145, 21)
        Me.txtItem.TabIndex = 11
        '
        'UG
        '
        Me.UG.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance8.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.UG.DisplayLayout.GroupByBox.Appearance = Appearance8
        Appearance9.ForeColor = System.Drawing.Color.White
        Me.UG.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance9
        Appearance10.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Appearance10.FontData.Name = "Times New Roman"
        Appearance10.FontData.SizeInPoints = 9.0!
        Appearance10.ForeColor = System.Drawing.Color.White
        Me.UG.DisplayLayout.GroupByBox.PromptAppearance = Appearance10
        Me.UG.DisplayLayout.MaxColScrollRegions = 1
        Me.UG.DisplayLayout.MaxRowScrollRegions = 1
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.UG.DisplayLayout.Override.CellAppearance = Appearance11
        Me.UG.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit
        Me.UG.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance12.BackColor = System.Drawing.Color.White
        Me.UG.DisplayLayout.Override.RowAlternateAppearance = Appearance12
        Appearance13.BorderColor = System.Drawing.Color.Silver
        Me.UG.DisplayLayout.Override.RowAppearance = Appearance13
        Me.UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton
        Me.UG.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.UG.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.UG.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.UG.Location = New System.Drawing.Point(0, 77)
        Me.UG.Name = "UG"
        Me.UG.Size = New System.Drawing.Size(696, 359)
        Me.UG.TabIndex = 16
        '
        'frmStockEnquiries
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(696, 436)
        Me.Controls.Add(Me.UG)
        Me.Controls.Add(Me.txtItem)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.txtSimpleCode)
        Me.Controls.Add(Me.cmbInventoryItem)
        Me.Controls.Add(Me.ultraLabel1)
        Me.Controls.Add(Me.lbl6)
        Me.Controls.Add(Me.toolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmStockEnquiries"
        Me.Text = "Stock Enquiries"
        Me.toolStrip1.ResumeLayout(False)
        Me.toolStrip1.PerformLayout()
        CType(Me.txtCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSimpleCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbInventoryItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents toolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents lbl6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents ugExcelExporter As Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter
    Friend WithEvents tsbSaveMySetting As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txtSimpleCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents cmbInventoryItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents ultraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents tsbConnect As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbUpdate As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtItem As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UG As Infragistics.Win.UltraWinGrid.UltraGrid
End Class
