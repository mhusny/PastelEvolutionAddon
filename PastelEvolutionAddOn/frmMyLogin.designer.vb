<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMyLogin
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
        Dim UltraTreeNode1 As Infragistics.Win.UltraWinTree.UltraTreeNode = New Infragistics.Win.UltraWinTree.UltraTreeNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMyLogin))
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraLabel1 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraLabel2 = New Infragistics.Win.Misc.UltraLabel()
        Me.txtPassword = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.btnOK = New Infragistics.Win.Misc.UltraButton()
        Me.btnClose = New Infragistics.Win.Misc.UltraButton()
        Me.cmbAgent = New System.Windows.Forms.ComboBox()
        Me.cbSavePW = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.UltraButton4 = New Infragistics.Win.Misc.UltraButton()
        Me.UltraButton3 = New Infragistics.Win.Misc.UltraButton()
        Me.UltraButton2 = New Infragistics.Win.Misc.UltraButton()
        Me.UltraButton1 = New Infragistics.Win.Misc.UltraButton()
        Me.ut = New Infragistics.Win.UltraWinTree.UltraTree()
        Me.UltraLabel8 = New Infragistics.Win.Misc.UltraLabel()
        Me.UltraButton5 = New Infragistics.Win.Misc.UltraButton()
        CType(Me.txtPassword, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.ut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UltraLabel1
        '
        Me.UltraLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel1.Location = New System.Drawing.Point(12, 67)
        Me.UltraLabel1.Name = "UltraLabel1"
        Me.UltraLabel1.Size = New System.Drawing.Size(100, 21)
        Me.UltraLabel1.TabIndex = 0
        Me.UltraLabel1.Text = " Agent "
        '
        'UltraLabel2
        '
        Me.UltraLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel2.Location = New System.Drawing.Point(12, 92)
        Me.UltraLabel2.Name = "UltraLabel2"
        Me.UltraLabel2.Size = New System.Drawing.Size(100, 21)
        Me.UltraLabel2.TabIndex = 1
        Me.UltraLabel2.Text = " Password"
        '
        'txtPassword
        '
        Me.txtPassword.AutoSize = False
        Me.txtPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(97, 91)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(213, 21)
        Me.txtPassword.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Location = New System.Drawing.Point(144, 290)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 26)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(225, 290)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 26)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Cancel"
        '
        'cmbAgent
        '
        Me.cmbAgent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbAgent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAgent.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgent.FormattingEnabled = True
        Me.cmbAgent.Location = New System.Drawing.Point(97, 67)
        Me.cmbAgent.Name = "cmbAgent"
        Me.cmbAgent.Size = New System.Drawing.Size(213, 21)
        Me.cmbAgent.TabIndex = 0
        '
        'cbSavePW
        '
        Me.cbSavePW.AutoSize = True
        Me.cbSavePW.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbSavePW.Location = New System.Drawing.Point(97, 116)
        Me.cbSavePW.Name = "cbSavePW"
        Me.cbSavePW.Size = New System.Drawing.Size(100, 17)
        Me.cbSavePW.TabIndex = 8
        Me.cbSavePW.Text = "Save Password"
        Me.cbSavePW.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.UltraButton4)
        Me.GroupBox1.Controls.Add(Me.UltraButton3)
        Me.GroupBox1.Controls.Add(Me.UltraButton2)
        Me.GroupBox1.Controls.Add(Me.UltraButton1)
        Me.GroupBox1.Controls.Add(Me.ut)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(7, 136)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(307, 143)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Company"
        '
        'UltraButton4
        '
        Me.UltraButton4.Enabled = False
        Me.UltraButton4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraButton4.Location = New System.Drawing.Point(237, 101)
        Me.UltraButton4.Name = "UltraButton4"
        Me.UltraButton4.Size = New System.Drawing.Size(66, 26)
        Me.UltraButton4.TabIndex = 13
        Me.UltraButton4.Text = "New"
        '
        'UltraButton3
        '
        Me.UltraButton3.Enabled = False
        Me.UltraButton3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraButton3.Location = New System.Drawing.Point(237, 74)
        Me.UltraButton3.Name = "UltraButton3"
        Me.UltraButton3.Size = New System.Drawing.Size(66, 26)
        Me.UltraButton3.TabIndex = 12
        Me.UltraButton3.Text = "Remove"
        '
        'UltraButton2
        '
        Me.UltraButton2.Enabled = False
        Me.UltraButton2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraButton2.Location = New System.Drawing.Point(237, 47)
        Me.UltraButton2.Name = "UltraButton2"
        Me.UltraButton2.Size = New System.Drawing.Size(66, 26)
        Me.UltraButton2.TabIndex = 11
        Me.UltraButton2.Text = "Edit"
        '
        'UltraButton1
        '
        Me.UltraButton1.Enabled = False
        Me.UltraButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraButton1.Location = New System.Drawing.Point(237, 20)
        Me.UltraButton1.Name = "UltraButton1"
        Me.UltraButton1.Size = New System.Drawing.Size(66, 26)
        Me.UltraButton1.TabIndex = 10
        Me.UltraButton1.Text = "Locate"
        '
        'ut
        '
        Me.ut.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ut.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ut.Location = New System.Drawing.Point(10, 19)
        Me.ut.Name = "ut"
        Me.ut.NodeConnectorColor = System.Drawing.SystemColors.ControlDark
        UltraTreeNode1.Key = "0"
        UltraTreeNode1.LeftImages.Add(CType(resources.GetObject("UltraTreeNode1.LeftImages"), Object))
        UltraTreeNode1.Text = "DATABASES"
        Me.ut.Nodes.AddRange(New Infragistics.Win.UltraWinTree.UltraTreeNode() {UltraTreeNode1})
        Me.ut.Size = New System.Drawing.Size(224, 113)
        Me.ut.TabIndex = 0
        '
        'UltraLabel8
        '
        Appearance1.BackColor = System.Drawing.Color.Black
        Appearance1.FontData.Name = "Verdana"
        Appearance1.FontData.SizeInPoints = 17.0!
        Appearance1.ForeColor = System.Drawing.Color.White
        Appearance1.Image = CType(resources.GetObject("Appearance1.Image"), Object)
        Appearance1.ImageHAlign = Infragistics.Win.HAlign.Right
        Appearance1.TextHAlignAsString = "Left"
        Appearance1.TextVAlignAsString = "Middle"
        Me.UltraLabel8.Appearance = Appearance1
        Me.UltraLabel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.UltraLabel8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraLabel8.ImageSize = New System.Drawing.Size(70, 50)
        Me.UltraLabel8.Location = New System.Drawing.Point(0, 0)
        Me.UltraLabel8.Name = "UltraLabel8"
        Me.UltraLabel8.Size = New System.Drawing.Size(322, 55)
        Me.UltraLabel8.TabIndex = 28
        Me.UltraLabel8.Text = " Evolution AddOn"
        '
        'UltraButton5
        '
        Me.UltraButton5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraButton5.Location = New System.Drawing.Point(17, 290)
        Me.UltraButton5.Name = "UltraButton5"
        Me.UltraButton5.Size = New System.Drawing.Size(95, 26)
        Me.UltraButton5.TabIndex = 29
        Me.UltraButton5.Text = "Add Company"
        '
        'frmMyLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(322, 324)
        Me.Controls.Add(Me.UltraButton5)
        Me.Controls.Add(Me.UltraLabel8)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cbSavePW)
        Me.Controls.Add(Me.cmbAgent)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.UltraLabel2)
        Me.Controls.Add(Me.UltraLabel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMyLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login "
        CType(Me.txtPassword, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.ut, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents UltraLabel1 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraLabel2 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents txtPassword As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents btnOK As Infragistics.Win.Misc.UltraButton
    Friend WithEvents btnClose As Infragistics.Win.Misc.UltraButton
    Friend WithEvents cmbAgent As System.Windows.Forms.ComboBox
    Friend WithEvents cbSavePW As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ut As Infragistics.Win.UltraWinTree.UltraTree
    Friend WithEvents UltraButton4 As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraButton2 As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraButton1 As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraLabel8 As Infragistics.Win.Misc.UltraLabel
    Friend WithEvents UltraButton3 As Infragistics.Win.Misc.UltraButton
    Friend WithEvents UltraButton5 As Infragistics.Win.Misc.UltraButton
End Class
