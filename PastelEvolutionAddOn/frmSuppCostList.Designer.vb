<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSuppCostList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSuppCostList))
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsbExit = New System.Windows.Forms.ToolStripButton()
        Me.tsbExport = New System.Windows.Forms.ToolStripButton()
        Me.tsbConnect = New System.Windows.Forms.ToolStripButton()
        Me.UG = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.cmbInventoryItem = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmbSupplier = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txtBarCode = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.toolStrip1.SuspendLayout()
        CType(Me.UG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbInventoryItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBarCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'toolStrip1
        '
        Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbExit, Me.tsbExport, Me.tsbConnect})
        Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.toolStrip1.Name = "toolStrip1"
        Me.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.toolStrip1.Size = New System.Drawing.Size(591, 25)
        Me.toolStrip1.TabIndex = 26
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
        'tsbExport
        '
        Me.tsbExport.Image = CType(resources.GetObject("tsbExport.Image"), System.Drawing.Image)
        Me.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExport.Name = "tsbExport"
        Me.tsbExport.Size = New System.Drawing.Size(70, 22)
        Me.tsbExport.Text = "Add New"
        Me.tsbExport.Visible = False
        '
        'tsbConnect
        '
        Me.tsbConnect.Image = CType(resources.GetObject("tsbConnect.Image"), System.Drawing.Image)
        Me.tsbConnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbConnect.Name = "tsbConnect"
        Me.tsbConnect.Size = New System.Drawing.Size(49, 22)
        Me.tsbConnect.Text = "View"
        '
        'UG
        '
        Me.UG.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance1.BorderColor = System.Drawing.Color.Silver
        Me.UG.DisplayLayout.Appearance = Appearance1
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
        Me.UG.Location = New System.Drawing.Point(0, 56)
        Me.UG.Name = "UG"
        Me.UG.Size = New System.Drawing.Size(591, 378)
        Me.UG.TabIndex = 37
        '
        'cmbInventoryItem
        '
        Me.cmbInventoryItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Appearance2.BorderColor = System.Drawing.Color.Silver
        Me.cmbInventoryItem.DisplayLayout.Appearance = Appearance2
        Me.cmbInventoryItem.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn
        Appearance3.BackColor = System.Drawing.Color.PapayaWhip
        Me.cmbInventoryItem.DisplayLayout.Override.RowAlternateAppearance = Appearance3
        Me.cmbInventoryItem.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
        Me.cmbInventoryItem.Location = New System.Drawing.Point(12, 28)
        Me.cmbInventoryItem.Name = "cmbInventoryItem"
        Me.cmbInventoryItem.Size = New System.Drawing.Size(178, 22)
        Me.cmbInventoryItem.TabIndex = 38
        '
        'cmbSupplier
        '
        Me.cmbSupplier.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Appearance4.BorderColor = System.Drawing.Color.Silver
        Me.cmbSupplier.DisplayLayout.Appearance = Appearance4
        Me.cmbSupplier.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn
        Appearance5.BackColor = System.Drawing.Color.PapayaWhip
        Me.cmbSupplier.DisplayLayout.Override.RowAlternateAppearance = Appearance5
        Me.cmbSupplier.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
        Me.cmbSupplier.Location = New System.Drawing.Point(196, 28)
        Me.cmbSupplier.Name = "cmbSupplier"
        Me.cmbSupplier.Size = New System.Drawing.Size(174, 22)
        Me.cmbSupplier.TabIndex = 39
        '
        'txtBarCode
        '
        Appearance6.TextHAlignAsString = "Right"
        Me.txtBarCode.Appearance = Appearance6
        Me.txtBarCode.AutoSize = False
        Me.txtBarCode.Location = New System.Drawing.Point(376, 28)
        Me.txtBarCode.Name = "txtBarCode"
        Me.txtBarCode.Size = New System.Drawing.Size(126, 22)
        Me.txtBarCode.TabIndex = 40
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(508, 28)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(71, 22)
        Me.Button1.TabIndex = 41
        Me.Button1.Text = "Add"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmSuppCostList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 434)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtBarCode)
        Me.Controls.Add(Me.cmbSupplier)
        Me.Controls.Add(Me.cmbInventoryItem)
        Me.Controls.Add(Me.UG)
        Me.Controls.Add(Me.toolStrip1)
        Me.Name = "frmSuppCostList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Supplier Cost List"
        Me.toolStrip1.ResumeLayout(False)
        Me.toolStrip1.PerformLayout()
        CType(Me.UG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbInventoryItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBarCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents toolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbConnect As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents UG As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents cmbInventoryItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents cmbSupplier As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents txtBarCode As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
