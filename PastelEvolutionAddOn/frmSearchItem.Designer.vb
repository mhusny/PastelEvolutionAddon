<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearchItem
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
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSearchItem))
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.txtCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.txtSimpleCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UltraLabel3 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.lbl6 = New Infragistics.Win.Misc.UltraLabel()
        Me.ugExcelExporter = New Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(Me.components)
        Me.ultraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.tsbSaveMySetting = New System.Windows.Forms.ToolStripButton()
        Me.tsbExport = New System.Windows.Forms.ToolStripButton()
        Me.tsbConnect = New System.Windows.Forms.ToolStripButton()
        Me.tsbBin = New System.Windows.Forms.ToolStripButton()
        Me.tsbCat = New System.Windows.Forms.ToolStripButton()
        Me.txtSearch = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.UG = New Infragistics.Win.UltraWinGrid.UltraGrid()
        CType(Me.txtCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSimpleCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.toolStrip1.SuspendLayout()
        CType(Me.txtSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCode
        '
        Appearance14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCode.Appearance = Appearance14
        Me.txtCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.Location = New System.Drawing.Point(106, 94)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.ReadOnly = True
        Me.txtCode.Size = New System.Drawing.Size(541, 21)
        Me.txtCode.TabIndex = 23
        '
        'txtSimpleCode
        '
        Appearance15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSimpleCode.Appearance = Appearance15
        Me.txtSimpleCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSimpleCode.Location = New System.Drawing.Point(106, 72)
        Me.txtSimpleCode.Name = "txtSimpleCode"
        Me.txtSimpleCode.ReadOnly = True
        Me.txtSimpleCode.Size = New System.Drawing.Size(541, 21)
        Me.txtSimpleCode.TabIndex = 22
        Me.txtSimpleCode.TabStop = False
        '
        'UltraLabel3
        '
        Appearance16.TextVAlignAsString = "Middle"
        Me.UltraLabel3.Appearance = Appearance16
        Me.UltraLabel3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel3.Location = New System.Drawing.Point(0, 94)
        Me.UltraLabel3.Name = "UltraLabel3"
        Me.UltraLabel3.Size = New System.Drawing.Size(146, 21)
        Me.UltraLabel3.TabIndex = 21
        Me.UltraLabel3.Text = " Code"
        '
        'UltraLabel2
        '
        Appearance17.TextVAlignAsString = "Middle"
        Me.UltraLabel2.Appearance = Appearance17
        Me.UltraLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel2.Location = New System.Drawing.Point(0, 72)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(146, 21)
        Me.UltraLabel2.TabIndex = 20
        Me.UltraLabel2.Text = " Simple Code"
        '
        'lbl6
        '
        Appearance18.BackColor = System.Drawing.Color.Maroon
        Appearance18.BackColor2 = System.Drawing.Color.DarkRed
        Appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
        Appearance18.FontData.BoldAsString = "True"
        Appearance18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Appearance18.TextVAlignAsString = "Middle"
        Me.lbl6.Appearance = Appearance18
        Me.lbl6.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(177, Byte))
        Me.lbl6.Location = New System.Drawing.Point(0, 25)
        Me.lbl6.Name = "lbl6"
        Me.lbl6.Size = New System.Drawing.Size(649, 18)
        Me.lbl6.TabIndex = 16
        Me.lbl6.Text = "Stock Enquiries"
        '
        'ultraLabel1
        '
        Appearance19.TextVAlignAsString = "Middle"
        Me.ultraLabel1.Appearance = Appearance19
        Me.ultraLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ultraLabel1.Location = New System.Drawing.Point(0, 49)
        Me.ultraLabel1.Name = "ultraLabel1"
        Me.ultraLabel1.Size = New System.Drawing.Size(146, 22)
        Me.ultraLabel1.TabIndex = 17
        Me.ultraLabel1.Text = " Inventory Item "
        '
        'toolStrip1
        '
        Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbExit, Me.tsbSaveMySetting, Me.tsbExport, Me.tsbConnect, Me.tsbBin, Me.tsbCat})
        Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.toolStrip1.Name = "toolStrip1"
        Me.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.toolStrip1.Size = New System.Drawing.Size(649, 25)
        Me.toolStrip1.TabIndex = 15
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
        Me.tsbExport.Visible = False
        '
        'tsbConnect
        '
        Me.tsbConnect.Image = CType(resources.GetObject("tsbConnect.Image"), System.Drawing.Image)
        Me.tsbConnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbConnect.Name = "tsbConnect"
        Me.tsbConnect.Size = New System.Drawing.Size(53, 22)
        Me.tsbConnect.Text = "VIEW"
        '
        'tsbBin
        '
        Me.tsbBin.Image = CType(resources.GetObject("tsbBin.Image"), System.Drawing.Image)
        Me.tsbBin.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbBin.Name = "tsbBin"
        Me.tsbBin.Size = New System.Drawing.Size(79, 22)
        Me.tsbBin.Text = "Update Bin"
        '
        'tsbCat
        '
        Me.tsbCat.Image = CType(resources.GetObject("tsbCat.Image"), System.Drawing.Image)
        Me.tsbCat.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbCat.Name = "tsbCat"
        Me.tsbCat.Size = New System.Drawing.Size(107, 20)
        Me.tsbCat.Text = "UpdateCategory"
        '
        'txtSearch
        '
        Me.txtSearch.AutoSize = False
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(106, 49)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(541, 22)
        Me.txtSearch.TabIndex = 1
        '
        'UG
        '
        Me.UG.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance7.BorderColor = System.Drawing.Color.Silver
        Me.UG.DisplayLayout.Appearance = Appearance7
        Me.UG.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn
        Me.UG.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UG.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.[True]
        Me.UG.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.UG.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell
        Me.UG.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange
        Me.UG.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.DropDownList
        Me.UG.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand
        Me.UG.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow
        Me.UG.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle
        Me.UG.Location = New System.Drawing.Point(0, 121)
        Me.UG.Name = "UG"
        Me.UG.Size = New System.Drawing.Size(647, 358)
        Me.UG.TabIndex = 25
        '
        'frmSearchItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(649, 480)
        Me.Controls.Add(Me.UG)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.txtSimpleCode)
        Me.Controls.Add(Me.UltraLabel3)
        Me.Controls.Add(Me.UltraLabel2)
        Me.Controls.Add(Me.lbl6)
        Me.Controls.Add(Me.ultraLabel1)
        Me.Controls.Add(Me.toolStrip1)
        Me.Name = "frmSearchItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Search Item"
        CType(Me.txtCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSimpleCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.toolStrip1.ResumeLayout(False)
        Me.toolStrip1.PerformLayout()
        CType(Me.txtSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents tsbConnect As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbSaveMySetting As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtSimpleCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents tsbExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraLabel3 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents lbl6 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents ugExcelExporter As Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter
    Friend WithEvents ultraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents toolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents txtSearch As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UG As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents tsbBin As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbCat As System.Windows.Forms.ToolStripButton
End Class
