<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReOrder
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraDataColumn1 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("ReOrder")
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReOrder))
        Me.lbl6 = New Infragistics.Win.Misc.UltraLabel()
        Me.ugExcelExporter = New Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(Me.components)
        Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ultraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraDataSource1 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.cmbItemGroup = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraDataSource2 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.UltraDataSource3 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.UG = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.lblActive = New Infragistics.Win.Misc.UltraLabel()
        Me.cbAllGroups = New System.Windows.Forms.CheckBox()
        Me.cnbItemDesc = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.cmbGroup = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.tsbExport = New System.Windows.Forms.ToolStripButton()
        Me.tsbConnect = New System.Windows.Forms.ToolStripButton()
        Me.tsbSaveMySetting = New System.Windows.Forms.ToolStripButton()
        Me.tsSI = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.tsbSaveMySettings = New System.Windows.Forms.ToolStripButton()
        Me.toolStrip1.SuspendLayout()
        CType(Me.UltraDataSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbItemGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraDataSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraDataSource3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cnbItemDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbGroup, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.lbl6.Size = New System.Drawing.Size(902, 18)
        Me.lbl6.TabIndex = 26
        Me.lbl6.Text = "Re-Order Item"
        '
        'toolStrip1
        '
        Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbExit, Me.tsbExport, Me.tsbConnect, Me.tsbSaveMySetting, Me.tsSI, Me.ToolStripButton1, Me.ToolStripButton3, Me.ToolStripButton2, Me.ToolStripButton4, Me.tsbSaveMySettings})
        Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.toolStrip1.Name = "toolStrip1"
        Me.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.toolStrip1.Size = New System.Drawing.Size(902, 25)
        Me.toolStrip1.TabIndex = 25
        Me.toolStrip1.Text = "ToolStrip1"
        '
        'ultraLabel1
        '
        Appearance2.TextVAlignAsString = "Middle"
        Me.ultraLabel1.Appearance = Appearance2
        Me.ultraLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ultraLabel1.Location = New System.Drawing.Point(0, 46)
        Me.ultraLabel1.Name = "ultraLabel1"
        Me.ultraLabel1.Size = New System.Drawing.Size(146, 22)
        Me.ultraLabel1.TabIndex = 27
        Me.ultraLabel1.Text = "Group Item "
        '
        'UltraDataSource1
        '
        UltraDataColumn1.DataType = GetType(Boolean)
        UltraDataColumn1.DefaultValue = True
        Me.UltraDataSource1.Band.Columns.AddRange(New Object() {UltraDataColumn1})
        Me.UltraDataSource1.UseBindingSource = True
        '
        'cmbItemGroup
        '
        Me.cmbItemGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Appearance3.BorderColor = System.Drawing.Color.Silver
        Me.cmbItemGroup.DisplayLayout.Appearance = Appearance3
        Me.cmbItemGroup.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn
        Appearance4.BackColor = System.Drawing.Color.PapayaWhip
        Me.cmbItemGroup.DisplayLayout.Override.RowAlternateAppearance = Appearance4
        Me.cmbItemGroup.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
        Me.cmbItemGroup.Location = New System.Drawing.Point(80, 46)
        Me.cmbItemGroup.Name = "cmbItemGroup"
        Me.cmbItemGroup.Size = New System.Drawing.Size(209, 22)
        Me.cmbItemGroup.TabIndex = 35
        '
        'UltraDataSource2
        '
        Me.UltraDataSource2.UseBindingSource = True
        '
        'UltraDataSource3
        '
        Me.UltraDataSource3.UseBindingSource = True
        '
        'UG
        '
        Me.UG.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance5.BorderColor = System.Drawing.Color.Silver
        Me.UG.DisplayLayout.Appearance = Appearance5
        Me.UG.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns
        Me.UG.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UG.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.[True]
        Me.UG.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit
        Me.UG.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell
        Me.UG.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange
        Me.UG.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.DropDownList
        Me.UG.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand
        Me.UG.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow
        Me.UG.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.UG.Location = New System.Drawing.Point(0, 102)
        Me.UG.Name = "UG"
        Me.UG.Size = New System.Drawing.Size(902, 371)
        Me.UG.TabIndex = 36
        '
        'lblActive
        '
        Appearance6.TextVAlignAsString = "Middle"
        Me.lblActive.Appearance = Appearance6
        Me.lblActive.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActive.Location = New System.Drawing.Point(90, 74)
        Me.lblActive.Name = "lblActive"
        Me.lblActive.Size = New System.Drawing.Size(146, 22)
        Me.lblActive.TabIndex = 37
        Me.lblActive.Text = "*Active*"
        '
        'cbAllGroups
        '
        Me.cbAllGroups.AutoSize = True
        Me.cbAllGroups.Location = New System.Drawing.Point(295, 79)
        Me.cbAllGroups.Name = "cbAllGroups"
        Me.cbAllGroups.Size = New System.Drawing.Size(74, 17)
        Me.cbAllGroups.TabIndex = 38
        Me.cbAllGroups.Text = "All Groups"
        Me.cbAllGroups.UseVisualStyleBackColor = True
        '
        'cnbItemDesc
        '
        Me.cnbItemDesc.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Appearance7.BorderColor = System.Drawing.Color.Silver
        Me.cnbItemDesc.DisplayLayout.Appearance = Appearance7
        Me.cnbItemDesc.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn
        Appearance8.BackColor = System.Drawing.Color.PapayaWhip
        Me.cnbItemDesc.DisplayLayout.Override.RowAlternateAppearance = Appearance8
        Me.cnbItemDesc.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
        Me.cnbItemDesc.Location = New System.Drawing.Point(542, 45)
        Me.cnbItemDesc.Name = "cnbItemDesc"
        Me.cnbItemDesc.Size = New System.Drawing.Size(225, 22)
        Me.cnbItemDesc.TabIndex = 39
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(295, 46)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 40
        Me.Button1.Text = "View"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(724, 71)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 41
        Me.Button2.Text = "View"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'UltraLabel2
        '
        Appearance9.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance9
        Me.UltraLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel2.Location = New System.Drawing.Point(419, 46)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(146, 22)
        Me.UltraLabel2.TabIndex = 42
        Me.UltraLabel2.Text = "Group Description"
        '
        'cmbGroup
        '
        Me.cmbGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Appearance10.BorderColor = System.Drawing.Color.Silver
        Me.cmbGroup.DisplayLayout.Appearance = Appearance10
        Me.cmbGroup.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn
        Appearance11.BackColor = System.Drawing.Color.PapayaWhip
        Me.cmbGroup.DisplayLayout.Override.RowAlternateAppearance = Appearance11
        Me.cmbGroup.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
        Me.cmbGroup.Location = New System.Drawing.Point(542, 72)
        Me.cmbGroup.Name = "cmbGroup"
        Me.cmbGroup.Size = New System.Drawing.Size(176, 22)
        Me.cmbGroup.TabIndex = 43
        '
        'UltraLabel3
        '
        Appearance12.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance12
        Me.UltraLabel3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel3.Location = New System.Drawing.Point(419, 74)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(146, 22)
        Me.UltraLabel3.TabIndex = 44
        Me.UltraLabel3.Text = "Item Group"
        '
        'tsbExit
        '
        Me.tsbExit.Image = CType(resources.GetObject("tsbExit.Image"), System.Drawing.Image)
        Me.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExit.Name = "tsbExit"
        Me.tsbExit.Size = New System.Drawing.Size(45, 22)
        Me.tsbExit.Text = "Exit"
        '
        'tsbExport
        '
        Me.tsbExport.Image = CType(resources.GetObject("tsbExport.Image"), System.Drawing.Image)
        Me.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExport.Name = "tsbExport"
        Me.tsbExport.Size = New System.Drawing.Size(62, 22)
        Me.tsbExport.Text = "Update"
        '
        'tsbConnect
        '
        Me.tsbConnect.Image = CType(resources.GetObject("tsbConnect.Image"), System.Drawing.Image)
        Me.tsbConnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbConnect.Name = "tsbConnect"
        Me.tsbConnect.Size = New System.Drawing.Size(49, 22)
        Me.tsbConnect.Text = "View"
        Me.tsbConnect.Visible = False
        '
        'tsbSaveMySetting
        '
        Me.tsbSaveMySetting.Image = CType(resources.GetObject("tsbSaveMySetting.Image"), System.Drawing.Image)
        Me.tsbSaveMySetting.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSaveMySetting.Name = "tsbSaveMySetting"
        Me.tsbSaveMySetting.Size = New System.Drawing.Size(110, 22)
        Me.tsbSaveMySetting.Text = "Save My Settings"
        Me.tsbSaveMySetting.Visible = False
        '
        'tsSI
        '
        Me.tsSI.Image = CType(resources.GetObject("tsSI.Image"), System.Drawing.Image)
        Me.tsSI.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsSI.Name = "tsSI"
        Me.tsSI.Size = New System.Drawing.Size(70, 22)
        Me.tsSI.Tag = "22224"
        Me.tsSI.Text = "Select All"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(87, 22)
        Me.ToolStripButton1.Tag = "22224"
        Me.ToolStripButton1.Text = "Un-Select All"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(99, 22)
        Me.ToolStripButton3.Text = "Activate Group"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(111, 22)
        Me.ToolStripButton2.Text = "Deactivate Group"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(92, 22)
        Me.ToolStripButton4.Text = "Update Items"
        Me.ToolStripButton4.Visible = False
        '
        'tsbSaveMySettings
        '
        Me.tsbSaveMySettings.Image = CType(resources.GetObject("tsbSaveMySettings.Image"), System.Drawing.Image)
        Me.tsbSaveMySettings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSaveMySettings.Name = "tsbSaveMySettings"
        Me.tsbSaveMySettings.Size = New System.Drawing.Size(73, 22)
        Me.tsbSaveMySettings.Text = "Save Grid"
        '
        'frmReOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(902, 472)
        Me.Controls.Add(Me.cmbGroup)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cnbItemDesc)
        Me.Controls.Add(Me.cbAllGroups)
        Me.Controls.Add(Me.lblActive)
        Me.Controls.Add(Me.UG)
        Me.Controls.Add(Me.cmbItemGroup)
        Me.Controls.Add(Me.lbl6)
        Me.Controls.Add(Me.ultraLabel1)
        Me.Controls.Add(Me.toolStrip1)
        Me.Controls.Add(Me.UltraLabel2)
        Me.Controls.Add(Me.UltraLabel3)
        Me.Name = "frmReOrder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Re Order Items"
        Me.toolStrip1.ResumeLayout(False)
        Me.toolStrip1.PerformLayout()
        CType(Me.UltraDataSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbItemGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraDataSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraDataSource3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cnbItemDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSaveMySetting As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbConnect As System.Windows.Forms.ToolStripButton
    Friend WithEvents lbl6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents ugExcelExporter As Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter
    Friend WithEvents toolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ultraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraDataSource1 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents cmbItemGroup As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraDataSource2 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents UltraDataSource3 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents UG As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents tsSI As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblActive As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cbAllGroups As System.Windows.Forms.CheckBox
    Friend WithEvents cnbItemDesc As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents cmbGroup As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents tsbSaveMySettings As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
End Class
