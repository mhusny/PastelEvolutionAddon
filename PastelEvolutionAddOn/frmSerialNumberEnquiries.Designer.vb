<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSerialNumberEnquiries
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
        Me.components = New System.ComponentModel.Container
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSerialNumberEnquiries))
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Me.lbl6 = New Infragistics.Win.Misc.UltraLabel
        Me.UG = New Infragistics.Win.UltraWinGrid.UltraGrid
        Me.toolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbExit = New System.Windows.Forms.ToolStripButton
        Me.tsbSaveMySetting = New System.Windows.Forms.ToolStripButton
        Me.tsbExport = New System.Windows.Forms.ToolStripButton
        Me.txtSerialNumber = New Infragistics.Win.Misc.UltraLabel
        Me.txtSN = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.ugExcelExporter = New Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(Me.components)
        Me.cbAll = New System.Windows.Forms.CheckBox
        CType(Me.UG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.toolStrip1.SuspendLayout()
        CType(Me.txtSN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.lbl6.Size = New System.Drawing.Size(697, 18)
        Me.lbl6.TabIndex = 8
        Me.lbl6.Text = "Serial Number Enquiries"
        '
        'UG
        '
        Me.UG.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.UG.DisplayLayout.Appearance = Appearance2
        Me.UG.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns
        Me.UG.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance3.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.UG.DisplayLayout.GroupByBox.Appearance = Appearance3
        Appearance4.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Appearance4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.UG.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
        Me.UG.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.[True]
        Me.UG.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.UG.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row
        Me.UG.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange
        Me.UG.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.Combo
        Me.UG.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand
        Me.UG.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow
        Appearance5.BackColor = System.Drawing.Color.Honeydew
        Me.UG.DisplayLayout.Override.RowAlternateAppearance = Appearance5
        Me.UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton
        Me.UG.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.UG.Location = New System.Drawing.Point(0, 68)
        Me.UG.Name = "UG"
        Me.UG.Size = New System.Drawing.Size(697, 638)
        Me.UG.TabIndex = 7
        '
        'toolStrip1
        '
        Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbExit, Me.tsbSaveMySetting, Me.tsbExport})
        Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.toolStrip1.Name = "toolStrip1"
        Me.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.toolStrip1.Size = New System.Drawing.Size(697, 25)
        Me.toolStrip1.TabIndex = 6
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
        'tsbSaveMySetting
        '
        Me.tsbSaveMySetting.Image = CType(resources.GetObject("tsbSaveMySetting.Image"), System.Drawing.Image)
        Me.tsbSaveMySetting.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSaveMySetting.Name = "tsbSaveMySetting"
        Me.tsbSaveMySetting.Size = New System.Drawing.Size(110, 22)
        Me.tsbSaveMySetting.Text = "Save My Settings"
        '
        'tsbExport
        '
        Me.tsbExport.Image = CType(resources.GetObject("tsbExport.Image"), System.Drawing.Image)
        Me.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExport.Name = "tsbExport"
        Me.tsbExport.Size = New System.Drawing.Size(73, 22)
        Me.tsbExport.Text = "Save Grid"
        '
        'txtSerialNumber
        '
        Appearance6.TextVAlignAsString = "Middle"
        Me.txtSerialNumber.Appearance = Appearance6
        Me.txtSerialNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSerialNumber.Location = New System.Drawing.Point(0, 44)
        Me.txtSerialNumber.Name = "txtSerialNumber"
        Me.txtSerialNumber.Size = New System.Drawing.Size(136, 23)
        Me.txtSerialNumber.TabIndex = 9
        Me.txtSerialNumber.Text = "Enter Serial Number "
        '
        'txtSN
        '
        Appearance7.ForeColor = System.Drawing.Color.Navy
        Appearance7.TextHAlignAsString = "Center"
        Me.txtSN.Appearance = Appearance7
        Me.txtSN.AutoSize = False
        Me.txtSN.Location = New System.Drawing.Point(145, 44)
        Me.txtSN.Name = "txtSN"
        Me.txtSN.Size = New System.Drawing.Size(253, 23)
        Me.txtSN.TabIndex = 10
        '
        'cbAll
        '
        Me.cbAll.AutoSize = True
        Me.cbAll.Location = New System.Drawing.Point(404, 48)
        Me.cbAll.Name = "cbAll"
        Me.cbAll.Size = New System.Drawing.Size(63, 17)
        Me.cbAll.TabIndex = 11
        Me.cbAll.Text = "View All"
        Me.cbAll.UseVisualStyleBackColor = True
        '
        'frmSerialNumberEnquiries
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(697, 706)
        Me.Controls.Add(Me.cbAll)
        Me.Controls.Add(Me.txtSN)
        Me.Controls.Add(Me.txtSerialNumber)
        Me.Controls.Add(Me.lbl6)
        Me.Controls.Add(Me.UG)
        Me.Controls.Add(Me.toolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSerialNumberEnquiries"
        Me.Text = "Serial Number Enquiries"
        CType(Me.UG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.toolStrip1.ResumeLayout(False)
        Me.toolStrip1.PerformLayout()
        CType(Me.txtSN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbl6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UG As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents toolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSaveMySetting As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtSerialNumber As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtSN As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents ugExcelExporter As Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter
    Friend WithEvents cbAll As System.Windows.Forms.CheckBox
End Class
