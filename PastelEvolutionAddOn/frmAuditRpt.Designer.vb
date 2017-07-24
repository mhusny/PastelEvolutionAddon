<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAuditRpt
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
        Me.btnOK = New Infragistics.Win.Misc.UltraButton
        Me.cmbAgent = New Infragistics.Win.UltraWinGrid.UltraCombo
        Me.dtpInDate = New System.Windows.Forms.DateTimePicker
        Me.Ad1 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Ad2 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.Ad3 = New Infragistics.Win.UltraWinEditors.UltraTextEditor
        Me.UltraButton1 = New Infragistics.Win.Misc.UltraButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        CType(Me.cmbAgent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ad1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ad2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ad3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Location = New System.Drawing.Point(202, 231)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 26)
        Me.btnOK.TabIndex = 16
        Me.btnOK.Text = "View"
        '
        'cmbAgent
        '
        Me.cmbAgent.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Appearance1.BorderColor = System.Drawing.Color.Silver
        Me.cmbAgent.DisplayLayout.Appearance = Appearance1
        Me.cmbAgent.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn
        Appearance2.BackColor = System.Drawing.Color.PapayaWhip
        Me.cmbAgent.DisplayLayout.Override.RowAlternateAppearance = Appearance2
        Me.cmbAgent.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.[Default]
        Me.cmbAgent.Location = New System.Drawing.Point(153, 63)
        Me.cmbAgent.Name = "cmbAgent"
        Me.cmbAgent.Size = New System.Drawing.Size(203, 22)
        Me.cmbAgent.TabIndex = 12
        '
        'dtpInDate
        '
        Me.dtpInDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpInDate.Location = New System.Drawing.Point(212, 37)
        Me.dtpInDate.Name = "dtpInDate"
        Me.dtpInDate.Size = New System.Drawing.Size(144, 20)
        Me.dtpInDate.TabIndex = 1
        '
        'Ad1
        '
        Me.Ad1.AutoSize = False
        Me.Ad1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ad1.Location = New System.Drawing.Point(60, 140)
        Me.Ad1.Name = "Ad1"
        Me.Ad1.Size = New System.Drawing.Size(226, 22)
        Me.Ad1.TabIndex = 14
        '
        'Ad2
        '
        Me.Ad2.AutoSize = False
        Me.Ad2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ad2.Location = New System.Drawing.Point(60, 168)
        Me.Ad2.Name = "Ad2"
        Me.Ad2.Size = New System.Drawing.Size(226, 22)
        Me.Ad2.TabIndex = 15
        '
        'Ad3
        '
        Me.Ad3.AutoSize = False
        Me.Ad3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Ad3.Location = New System.Drawing.Point(60, 196)
        Me.Ad3.Name = "Ad3"
        Me.Ad3.Size = New System.Drawing.Size(226, 22)
        Me.Ad3.TabIndex = 16
        '
        'UltraButton1
        '
        Me.UltraButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraButton1.Location = New System.Drawing.Point(283, 231)
        Me.UltraButton1.Name = "UltraButton1"
        Me.UltraButton1.Size = New System.Drawing.Size(75, 26)
        Me.UltraButton1.TabIndex = 17
        Me.UltraButton1.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Customer"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Customer Address"
        '
        'frmAuditRpt
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(370, 269)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.UltraButton1)
        Me.Controls.Add(Me.Ad3)
        Me.Controls.Add(Me.Ad2)
        Me.Controls.Add(Me.Ad1)
        Me.Controls.Add(Me.dtpInDate)
        Me.Controls.Add(Me.cmbAgent)
        Me.Controls.Add(Me.btnOK)
        Me.MaximizeBox = False
        Me.Name = "frmAuditRpt"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Audit Confirmation Report"
        CType(Me.cmbAgent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ad1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ad2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ad3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents cmbAgent As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents dtpInDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Ad1 As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Ad2 As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents Ad3 As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents UltraButton1 As Infragistics.Win.Misc.UltraButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
