Imports Infragistics.Win.UltraWinGrid
Public Class frmStockCount
    Public iWH As Integer = 0
    Public Sub Delete_Row()
        Dim R As UltraGridRow
        For Each R In UG.Rows.All
            If R.Cells("Line").Value = 0 Or R.Cells("StockID").Value = 0 Then
                R.Delete(False)
                Exit Sub
            End If
        Next
    End Sub
    Public Sub GET_DATA()
        'If iDocType = DocType.SalesOrder Then
        '    SQL = "SELECT DCLink, Account, Name, Physical1, Physical2," & _
        '   " Physical3, Post1, Post2, Post3,iARPriceListNameID FROM Client  Order By Name "
        'Else
        '    SQL = "SELECT DCLink, Account, Name, Physical1, Physical2," & _
        '   " Physical3, Post1, Post2, Post3 FROM Vendor"
        'End If
        'SQL += " SELECT idSalesRep, Code, Name FROM SalesRep Order By Name"
        'SQL += " SELECT ProjectLink, ProjectCode, ProjectName,ProjectDescription FROM Project Order By projectName"
        'SQL += " SELECT StatusCounter, StatusDescrip FROM OrdersSt"
        'SQL += " SELECT idIncidentPriority, cDescription FROM _rtblIncidentPriority"
        SQL = " SELECT StockLink, Code, Description_1, ItemGroup, SerialItem,WhseItem,Description_2 FROM StkItem Order By Description_1"

        'SQL += " SELECT idTaxRate, Code, Description, TaxRate FROM TaxRate"
        'SQL += " SELECT bIsInclusive FROM StDfTbl"
        'SQL += " SELECT WhseLink FROM WhseMst WHERE DefaultWhse = 1"
        SQL += " SELECT WhseLink, Code, Name, KnownAs FROM WhseMst WHERE DefaultWhse = 1"
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                'dsManu = .Get_Data_Sql(SQL)
                'cmbCustomer.DataSource = DS.Tables(0)
                'cmbCustomer.ValueMember = "DCLink"
                'cmbCustomer.DisplayMember = "Name"
                'cmbCustomer.DisplayLayout.Bands(0).Columns("DCLink").Hidden = True
                'cmbCustomer.DisplayLayout.Bands(0).Columns("Physical1").Hidden = True
                'cmbCustomer.DisplayLayout.Bands(0).Columns("Physical2").Hidden = True
                'cmbCustomer.DisplayLayout.Bands(0).Columns("Physical3").Hidden = True
                'cmbCustomer.DisplayLayout.Bands(0).Columns("Post1").Hidden = True
                'cmbCustomer.DisplayLayout.Bands(0).Columns("Post2").Hidden = True
                'cmbCustomer.DisplayLayout.Bands(0).Columns("Post3").Hidden = True

                ''Sales Rep
                'cmbSaleRep.DataSource = DS.Tables(1)
                'cmbSaleRep.ValueMember = "idSalesRep"
                'cmbSaleRep.DisplayMember = "Name"
                'cmbSaleRep.DisplayLayout.Bands(0).Columns("idSalesRep").Hidden = True
                ''Projects
                'cmbProject.DataSource = DS.Tables(2)
                'cmbProject.ValueMember = "ProjectLink"
                'cmbProject.DisplayMember = "ProjectName"
                'cmbProject.DisplayLayout.Bands(0).Columns("ProjectLink").Hidden = True
                ''Order Status
                'cmbOrderStatus.DataSource = DS.Tables(3)
                'cmbOrderStatus.ValueMember = "StatusCounter"
                'cmbOrderStatus.DisplayMember = "StatusDescrip"
                'cmbOrderStatus.DisplayLayout.Bands(0).Columns("StatusCounter").Hidden = True
                ''Priority
                'cmbPriority.DataSource = DS.Tables(4)
                'cmbPriority.ValueMember = "idIncidentPriority"
                'cmbPriority.DisplayMember = "cDescription"
                'cmbPriority.DisplayLayout.Bands(0).Columns("idIncidentPriority").Hidden = True
                'Stock
                'DDStock.DataSource = DS.Tables(0)
                'DDStock.ValueMember = "StockLink"
                'DDStock.DisplayMember = "Code"
                'DDStock.DisplayLayout.Bands(0).Columns("Description_1").Header.Caption = "Description"

                DDDescription.DataSource = DS.Tables(0)
                DDDescription.ValueMember = "Description_1"
                DDDescription.DisplayMember = "Description_1"
                DDDescription.DisplayLayout.Bands(0).Columns("Description_1").Header.Caption = "Description"
                'WH
                DDWH.DataSource = DS.Tables(1)
                DDWH.ValueMember = "WhseLink"
                DDWH.DisplayMember = "Code"
                DDWH.DisplayLayout.Bands(0).Columns("WhseLink").Hidden = True
                ''Tax
                'DDTaxType.DataSource = DS.Tables(7)
                'DDTaxType.ValueMember = "idTaxRate"
                'DDTaxType.DisplayMember = "Code"
                'DDTaxType.DisplayLayout.Bands(0).Columns("idTaxRate").Hidden = True

                'Dim Dr As DataRow
                'For Each Dr In DS.Tables(8).Rows
                '    If Dr("bIsInclusive") = False Then
                '        bIsInclusive = False
                '    ElseIf Dr("bIsInclusive") = True Then
                '        bIsInclusive = True
                '    End If
                'Next
                For Each Dr In DS.Tables(1).Rows
                    iWH = CInt(Dr("WhseLink"))
                Next
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                Exit Sub
            Finally
                objSQL = Nothing
            End Try
        End With

    End Sub
    Private Sub txtBarCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBarCode.KeyDown
        txtBarCode.Appearance.BackColor = Color.White
        If e.KeyCode = Keys.Enter Then
            Delete_Row()
            Dim sBarCode As String = UCase(txtBarCode.Text.Trim)
            For Each ugR In UG.Rows
                If ugR.ChildBands.HasChildRows = True Then
                    Dim ugR1 As UltraGridRow
                    For Each ugR1 In ugR.ChildBands(0).Rows
                        If sBarCode.Equals(ugR1.Cells("SerialNumber").Value) = True Then
                            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
                            'MessageBox.Show("Duplicate Serial Number found" & vbNewLine & "Line : " & ugR.Cells("Line").Value & "" & vbNewLine & "S/N : " & sBarCode & "", "Pastel Evolution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            txtBarCode.Appearance.BackColor = Color.Red
                            txtBarCode.SelectAll()
                            'txtBarCode.Focus()
                            Exit Sub
                        End If
                    Next
                End If
            Next

            Dim objSQL As New clsSqlConn
            With objSQL
                SQL = "SELECT SerialMF.SerialNumber, SerialMF.SNStockLink, StkItem.Code, StkItem.Description_1, StkItem.Description_2, StkItem.Description_3, StkItem.WhseItem, " & _
                " StkItem.SerialItem, StkItem.cSimpleCode ,StkItem.Qty_On_Hand , StkItem.ucIINoOfUnits " & _
                " FROM SerialMF INNER JOIN " & _
                " StkItem ON SerialMF.SNStockLink = StkItem.StockLink WHERE SerialNumber ='" & CStr(sBarCode) & "' AND CurrentLoc = 1 and CurrentAccLink = " & iWH & " "
                SQL += " Select * From sbStkCountSN_t "
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                'If DS.Tables(0).Rows.Count > 0 Then

                'Else
                Dim result As Integer
                If DS.Tables(0).Rows.Count = 0 Then
                    result = MessageBox.Show("Serial number not in stock", "Pastel Evolution", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    If result = DialogResult.Abort Then
                        Exit Sub
                    ElseIf result = DialogResult.Retry Then
                        'GoTo abc
                        Exit Sub
                    End If
                End If
                'End If
                Dim Dr1 As DataRow
                For Each Dr1 In DS.Tables(1).Rows
                    If UCase(txtBarCode.Text) = Dr1("Serialno") Then
                        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
                        'MessageBox.Show("Duplicate Serial Number found" & vbNewLine & "S/N : " & sBarCode & "", "Pastel Evolution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        txtBarCode.Appearance.BackColor = Color.Red
                        txtBarCode.SelectAll()
                        Exit Sub
                    End If
                Next

                Dim Dr As DataRow
                For Each Dr In DS.Tables(0).Rows
                    'StockLink, Code, Description_1, ItemGroup, SerialItem,WhseItem
                    Dim iStockLink As Integer = Dr("SNStockLink")
                    Dim sCode As String = Dr("Code")
                    Dim sDescription_1, sDescription_2 As String
                    sDescription_1 = Dr("Description_1")
                    sDescription_2 = Dr("Description_2")

                    Dim bSerialItem As Boolean = Dr("SerialItem")
                    Dim bWhseItem As Boolean = Dr("WhseItem")
                    'Dim ugR As UltraGridRow
                    'For Each ugR In DDStock.Rows
                    '    If ugR.Cells("StockLink").Value = Dr("SNStockLink") Then
                    '        iStockLink = ugR.Cells("StockLink").Value
                    '        sCode = ugR.Cells("Code").Value
                    '        sDescription_1 = ugR.Cells("Description_1").Value
                    '        sDescription_2 = ugR.Cells("Description_2").Value
                    '        bSerialItem = ugR.Cells("SerialItem").Value
                    '        bWhseItem = ugR.Cells("WhseItem").Value
                    '    End If
                    'Next

                    Dim ugR1 As UltraGridRow
                    Dim ugR2 As UltraGridRow
                    Dim iCount As Integer = 0
                    For Each ugR In UG.Rows
                        If ugR.Cells("StockID").Value = iStockLink Then
                            ugR1 = ugR
                            ugR1.Activated = True
                            iCount = iCount + 1
                            If ugR1.ChildBands.HasChildRows = True Then
                                ugR1 = UG.DisplayLayout.Bands(1).AddNew
                                'Autosave---------------------------------------------------------
                                .Begin_Trans()
                                SQL = "INSERT INTO sbStkCountSN_t(StockItem, Serialno, AID, BIN) " & _
                                    " VALUES (" & iStockLink & ", " & _
                                    " '" & Dr("SerialNumber") & "' , " & iAgent & " , '" & txtBin.Text & "')"
                                If .Execute_Sql_Trans(SQL) = 0 Then
                                    .Rollback_Trans()
                                    Exit Sub
                                End If
                                .Commit_Trans()
                                '---------------------------------------------------------------
                            Else
                                ugR1 = UG.ActiveRow.ChildBands(0).Band.AddNew
                            End If
                            ''ugR1.Cells("LN").Value = ugR1.Index + 1
                            ugR1.Cells("SerialNumber").Value = Dr("SerialNumber")
                            ''ugR1.Cells("SNStockLink").Value = iStockLink
                            ugR1.Cells("PrimaryLineID").Value = iStockLink
                            ugR1.Cells("BIN").Value = txtBin.Text
                        End If
                    Next
                    If iCount = 0 Then
                        ugR2 = UG.DisplayLayout.Bands(0).AddNew
                        ugR2.Cells("Line").Value = ugR2.Index + 1
                        ugR2.Cells("Warehouse").Value = iWH
                        ugR2.Cells("StockID").Value = iStockLink
                        ugR2.Cells("Code").Value = sCode
                        ugR2.Cells("Description_1").Value = sDescription_1
                        ugR2.Cells("Description_2").Value = sDescription_2
                        ugR2.Cells("IsSerialNo").Value = bSerialItem
                        ugR2.Cells("IsWH").Value = bWhseItem
                        ugR2.Cells("AvailableQty").Value = Dr("Qty_On_Hand")
                        ''ugR2.Cells("Warehouse").Value = iWH
                        'If iDocType = DocType.SalesOrder Then
                        '    ugR2.Cells("TaxType").Value = 1
                        '    For Each ugR3 As UltraGridRow In DDTaxType.Rows
                        '        If ugR2.Cells("TaxType").Value = ugR3.Cells("Code").Value Then
                        '            ugR2.Cells("TaxRate").Value = ugR3.Cells("TaxRate").Value
                        '        End If
                        '    Next
                        'ElseIf iDocType = DocType.PurchaseOrder Then
                        '    ugR2.Cells("TaxType").Value = 3
                        '    For Each ugR3 As UltraGridRow In DDTaxType.Rows
                        '        If ugR2.Cells("TaxType").Value = ugR3.Cells("Code").Value Then
                        '            ugR2.Cells("TaxRate").Value = ugR3.Cells("TaxRate").Value
                        '        End If
                        '    Next
                        'End If
                        ugR1 = ugR2.ChildBands(0).Band.AddNew()
                        'ugR1.Cells("LN").Value = ugR1.Index + 1
                        ugR1.Cells("SerialNumber").Value = Dr("SerialNumber")
                        'ugR1.Cells("SNStockLink").Value = iStockLink
                        ugR1.Cells("PrimaryLineID").Value = iStockLink
                        ugR1.Cells("BIN").Value = txtBin.Text

                        'Autosave---------------------------------------------------------
                        .Begin_Trans()
                        SQL = " INSERT INTO sbStkCount_t ( StockID, AID) Values (" & iStockLink & ", " & iAgent & ") "
                        If .Execute_Sql_Trans(SQL) = 0 Then
                            .Rollback_Trans()
                            Exit Sub
                        End If
                        .Commit_Trans()

                        .Begin_Trans()
                        SQL = "INSERT INTO sbStkCountSN_t(StockItem, Serialno, AID, BIN) " & _
                            " VALUES (" & iStockLink & ", " & _
                            " '" & Dr("SerialNumber") & "' , " & iAgent & ",  '" & txtBin.Text & "' )"
                        If .Execute_Sql_Trans(SQL) = 0 Then
                            .Rollback_Trans()
                            Exit Sub
                        End If
                        .Commit_Trans()
                        '------------------------------------------------------------
                    End If
                Next
                For Each ugR In UG.Rows
                    If ugR.Cells("IsSerialNo").Value = True Then
                        'ugR.Cells("ConfirmQty").Value = ugR.ChildBands(0).Rows.Count
                        ugR.Cells("Quantity").Value = ugR.ChildBands(0).Rows.Count
                    End If
                Next
                'Get_Total()

            End With

            For Each ugR In UG.Rows
                'For Each ugR1 In ugR.ChildBands
                '    If ugR.Cells("SerialNumber").Value = sBarCode Then
                '        ugR.Expanded = True
                '    Else
                '        ugR.Expanded = False
                '    End If
                'Next
                ugR.Expanded = False
            Next


            txtBarCode.ResetText()
            txtBarCode.Focus()
            My.Computer.Audio.Play(Application.StartupPath & "\beep.wav")
        End If

    End Sub

    Private Sub txtBarCode_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBarCode.ValueChanged

    End Sub

    Private Sub UG_AfterCellCancelUpdate(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UG.AfterCellUpdate
        'If e.Cell.Column.Key = "Description_1" Then
        '    Dim objSQL As New clsSqlConn
        '    With objSQL
        '        SQL = "SELECT SerialMF.SerialNumber, SerialMF.SNStockLink, StkItem.Code, StkItem.Description_1, StkItem.Description_2, StkItem.Description_3, StkItem.WhseItem, " & _
        '            " StkItem.SerialItem, StkItem.cSimpleCode ,StkItem.Qty_On_Hand , StkItem.ucIINoOfUnits " & _
        '            " FROM SerialMF INNER JOIN " & _
        '            " StkItem ON SerialMF.SNStockLink = StkItem.StockLink WHERE StkItem.StockLink = '" & UG.ActiveRow.Cells("StockID").Value & "' AND CurrentLoc = 1 and CurrentAccLink = " & iWH & ""
        '        DS = New DataSet
        '        DS = .Get_Data_Sql(SQL)
        '        If DS.Tables(0).Rows.Count > 0 Then
        '            Dim ugR2 As UltraGridRow
        '            For Each ugR2 In UG.Rows
        '                ugR2.Cells("Warehouse").Value = iWH
        '                ugR2.Cells("StockID").Value = DS.Tables(0).Rows(0)("SNStockLink")
        '                ugR2.Cells("Code").Value = DS.Tables(0).Rows(0)("Code")
        '                ugR2.Cells("Description_1").Value = DS.Tables(0).Rows(0)("Description_1")
        '                ugR2.Cells("Description_2").Value = DS.Tables(0).Rows(0)("Description_2")
        '                ugR2.Cells("IsSerialNo").Value = DS.Tables(0).Rows(0)("SerialItem")
        '                ugR2.Cells("IsWH").Value = DS.Tables(0).Rows(0)("WhseItem")
        '                ugR2.Cells("AvailableQty").Value = DS.Tables(0).Rows(0)("Qty_On_Hand")
        '            Next
        '        End If
        '    End With
        'End If
    End Sub

    Private Sub UG_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UG.Click
        'Try
        '    If DDDescription.ActiveRow.Activated = True Then
        '        UG.ActiveRow.Cells("StockID").Value = DDDescription.ActiveRow.Cells("StockLink").Value
        '        UG.ActiveRow.Cells("Code").Value = DDDescription.ActiveRow.Cells("StockLink").Value
        '        UG.ActiveRow.Cells("Description_1").Value = DDDescription.ActiveRow.Cells("Description_1").Value
        '        UG.ActiveRow.Cells("Description_2").Value = DDDescription.ActiveRow.Cells("Description_2").Value
        '        UG.ActiveRow.Cells("IsSerialNo").Value = IIf(DDDescription.ActiveRow.Cells("SerialItem").Value = True, True, False)
        '        UG.ActiveRow.Cells("IsWH").Value = IIf(DDDescription.ActiveRow.Cells("WhseItem").Value = True, True, False)
        '    End If
        'Catch ex As Exception
        '    Exit Sub
        'End Try
    End Sub
    Private Sub Get_Stock_Item()
        SQL = "SELECT     StockLink,cSimpleCode as SimpleCode ,Code   FROM _bvStockFull"
        Dim objSQL As New clsSqlConn
        With objSQL
            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            cmbInventoryItem.DataSource = DS.Tables(0)
            cmbInventoryItem.ValueMember = "Code"
            cmbInventoryItem.DisplayMember = "SimpleCode"
            cmbInventoryItem.DisplayLayout.Bands(0).Columns("StockLink").Hidden = True
            'cmbInventoryItem.DisplayLayout.Bands(0).Columns("Code").Hidden = True
        End With
    End Sub

    Private Sub frmStockCount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'tsbUpdateSO_Click(sender, e)

        GET_DATA()
        Get_Stock_Item()
    End Sub

    Private Sub UG_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles UG.KeyDown
        Try
            If e.KeyCode = Keys.Tab Then
                If UG.ActiveCell.Column.Key = "LineNote" Then
                    If UG.ActiveRow.Cells("Code").Value <> "" Then
                        If UG.ActiveRow.HasNextSibling = True Then Exit Sub
                        Dim ugR As UltraGridRow
                        ugR = UG.DisplayLayout.Bands(0).AddNew
                        ugR.Cells("Line").Value = ugR.Index + 1
                        ugR.Cells("Line").Selected = True
                        ugR.Cells("Warehouse").Value = iWH
                        ugR.Cells("Code").Activate()

                        'MsgBox("Item Code can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            Exit Sub

        End Try
    End Sub

    Private Sub DDDescription_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles DDDescription.InitializeLayout

    End Sub

    Private Sub DDDescription_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDDescription.Click
        Try
            If DDDescription.ActiveRow.Activated = True Then
                UG.ActiveRow.Cells("StockID").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                UG.ActiveRow.Cells("Code").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                UG.ActiveRow.Cells("Description_1").Value = DDDescription.ActiveRow.Cells("Description_1").Value
                UG.ActiveRow.Cells("Description_2").Value = DDDescription.ActiveRow.Cells("Description_2").Value
                UG.ActiveRow.Cells("IsSerialNo").Value = IIf(DDDescription.ActiveRow.Cells("SerialItem").Value = True, True, False)
                UG.ActiveRow.Cells("IsWH").Value = IIf(DDDescription.ActiveRow.Cells("WhseItem").Value = True, True, False)
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Public Sub autoSave(ByVal Item As String)
        'UG.PerformAction(UltraGridAction.CommitRow)
        'Delete_Row()
        'Dim ugR1 As UltraGridRow
        'Dim objSQL As New clsSqlConn
        'With objSQL
        '    For Each ugR1 In UG.Rows
        '        SQL = " SELECT sbStkCount_t.Line, SCID, sbStkCount_t.Qty,  sbStkCount_t.StockID, StkItem.Code, StkItem.Description_1, sbStkCount_t.WHID, sbStkCount_t.Qty,  StkItem.Qty_On_Hand, StkItem.SerialItem " & _
        '        " FROM sbStkCount_t INNER JOIN " & _
        '        " StkItem ON sbStkCount_t.StockID = StkItem.StockLink Where sbStkCount_t.StockID = " & Item & " "
        '        Try
        '            DS = New DataSet
        '            DS = .Get_Data_Sql(SQL)
        '            If DS.Tables(0).Rows.Count > 0 Then
        '                For Each Dr In DS.Tables(0).Rows
        '                    If Dr("StockID") = ugR1.Cells("StockID").Value Then
        '                        .Begin_Trans()
        '                        SQL = "Update [sbStkCount_t]" & _
        '                        " SET " & _
        '                        " [WHID] = " & ugR1.Cells("Warehouse").Value & " " & _
        '                        " ,[Qty] = " & Dr("Qty") + 1 & " " & _
        '                        " WHERE [StockID] = " & ugR1.Cells("StockID").Value & ""
        '                        If .Execute_Sql_Trans(SQL) = 0 Then
        '                            .Rollback_Trans()
        '                            Exit Sub
        '                        End If
        '                        .Commit_Trans()
        '                        .Begin_Trans()
        '                        SQL = "delete FROM sbStkCountSN_t Where StockItem = " & Item & " "
        '                        If .Execute_Sql_Trans(SQL) = 0 Then
        '                            .Rollback_Trans()
        '                            Exit Sub
        '                        End If
        '                        .Commit_Trans()
        '                        If ugR1.ChildBands.HasChildRows = True Then
        '                            Dim ugR2 As UltraGridRow
        '                            For Each ugR2 In ugR1.ChildBands(0).Rows

        '                                .Begin_Trans()
        '                                SQL = "delete FROM sbStkCountSN_t Where StockItem = " & Item & " "
        '                                If .Execute_Sql_Trans(SQL) = 0 Then
        '                                    .Rollback_Trans()
        '                                    Exit Sub
        '                                End If
        '                                .Commit_Trans()

        '                                .Begin_Trans()
        '                                SQL = "INSERT INTO sbStkCountSN_t(StockItem, Serialno, AID) " & _
        '                                    " VALUES (" & ugR2.ParentRow.Cells("StockID").Value & ", " & _
        '                                    " '" & CStr(ugR2.Cells("SerialNumber").Value) & "' , " & iAgent & " )"
        '                                If .Execute_Sql_Trans(SQL) = 0 Then
        '                                    .Rollback_Trans()
        '                                    Exit Sub
        '                                End If
        '                                .Commit_Trans()
        '                            Next
        '                        End If
        '                        Exit Sub
        '                    Else
        '                        .Begin_Trans()
        '                        SQL = " INSERT INTO sbStkCount_t ( StockID, WHID, Qty, AID) Values (" & ugR1.Cells("StockID").Value & ", " & ugR1.Cells("Warehouse").Value & ", " & _
        '                        " " & ugR1.Cells("Quantity").Value & ", " & iAgent & ") "
        '                        If .Execute_Sql_Trans(SQL) = 0 Then
        '                            .Rollback_Trans()
        '                            Exit Sub
        '                        End If
        '                        .Commit_Trans()

        '                        If ugR1.ChildBands.HasChildRows = True Then
        '                            Dim ugR2 As UltraGridRow
        '                            For Each ugR2 In ugR1.ChildBands(0).Rows

        '                                .Begin_Trans()
        '                                SQL = "INSERT INTO sbStkCountSN_t(StockItem, Serialno, AID) " & _
        '                                    " VALUES (" & ugR2.ParentRow.Cells("StockID").Value & ", " & _
        '                                    " '" & CStr(ugR2.Cells("SerialNumber").Value) & "' , " & iAgent & " )"
        '                                If .Execute_Sql_Trans(SQL) = 0 Then
        '                                    .Rollback_Trans()
        '                                    Exit Sub
        '                                End If
        '                                .Commit_Trans()
        '                            Next
        '                        End If
        '                    End If
        '                Next
        '            Else
        '                .Begin_Trans()
        '                SQL = " INSERT INTO sbStkCount_t ( StockID, WHID, Qty, AID) Values (" & ugR1.Cells("StockID").Value & ", " & ugR1.Cells("Warehouse").Value & ", " & _
        '                " " & 1 & ", " & iAgent & ") "
        '                If .Execute_Sql_Trans(SQL) = 0 Then
        '                    .Rollback_Trans()
        '                    Exit Sub
        '                End If
        '                .Commit_Trans()

        '                If ugR1.ChildBands.HasChildRows = True Then
        '                    Dim ugR2 As UltraGridRow
        '                    For Each ugR2 In ugR1.ChildBands(0).Rows

        '                        .Begin_Trans()
        '                        SQL = "INSERT INTO sbStkCountSN_t(StockItem, Serialno, AID) " & _
        '                            " VALUES (" & ugR2.ParentRow.Cells("StockID").Value & ", " & _
        '                            " '" & CStr(ugR2.Cells("SerialNumber").Value) & "' , " & iAgent & " )"
        '                        If .Execute_Sql_Trans(SQL) = 0 Then
        '                            .Rollback_Trans()
        '                            Exit Sub
        '                        End If
        '                        .Commit_Trans()
        '                    Next
        '                End If
        '            End If

        '            'End If
        '        Catch ex As Exception
        '            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
        '            Exit Sub
        '        Finally
        '            .Con_Close()
        '            objSQL = Nothing
        '            'objSalesOrder = Nothing
        '        End Try
        '    Next
        'End With

    End Sub

    Private Sub tsbUpdateSO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbUpdateSO.Click

        Dim objSQL As New clsSqlConn
        With objSQL

            .Begin_Trans()
            SQL = "delete FROM sbStkCount "
            SQL += "delete FROM sbStkCountSN "
            If .Execute_Sql_Trans(SQL) = 0 Then
                .Rollback_Trans()
                Exit Sub
            End If
            .Commit_Trans()

            Dim i As Integer = 1

            For Each ugrow In UG.Rows
                .Begin_Trans()
                SQL = " INSERT INTO sbStkCount (Line, StockID,  Qty, AID) Values (" & i & "," & ugrow.Cells("StockID").Value & ", " & _
                " " & ugrow.Cells("Quantity").Value & ", " & iAgent & ") "
                If .Execute_Sql_Trans(SQL) = 0 Then
                    .Rollback_Trans()
                    Exit Sub
                End If
                .Commit_Trans()

                i += 1
            Next

            'objSQL = Nothing

            For Each ugr In UG.Rows
                If ugr.Cells("IsSerialNo").Value = True Then
                    If ugr.ChildBands.HasChildRows = True Then
                        Dim ugR2 As UltraGridRow
                        For Each ugR2 In ugr.ChildBands(0).Rows
                            .Begin_Trans()
                            SQL = "INSERT INTO sbStkCountSN(StockItem, Serialno, AID) " & _
                                " VALUES (" & ugR2.ParentRow.Cells("StockID").Value & ", " & _
                                " '" & CStr(ugR2.Cells("SerialNumber").Value) & "' , " & iAgent & " )"
                            If .Execute_Sql_Trans(SQL) = 0 Then
                                .Rollback_Trans()
                                Exit Sub
                            End If
                            .Commit_Trans()
                        Next
                    End If
                    'ElseIf ugr.Cells("IsSerialNo").Value = False Then
                    '    .Begin_Trans()
                    '    SQL = " INSERT INTO sbStkCount (StockID, WHID, Qty) Values (" & ugr.Cells("StockID").Value & ", " & ugr.Cells("Warehouse").Value & ", " & _
                    '    " " & ugr.Cells("Quantity").Value & ") "
                    '    If .Execute_Sql_Trans(SQL) = 0 Then
                    '        .Rollback_Trans()
                    '        Exit Sub
                    '    End If
                    '    .Commit_Trans()
                End If
            Next

            '.Begin_Trans()
            'SQL = " DELETE FROM sbStkCount_t "
            'SQL += " DELETE FROM sbStkCountSN_t "
            'If .Execute_Sql_Trans(SQL) = 0 Then
            '    .Rollback_Trans()
            '    Exit Sub
            'End If
            '.Commit_Trans()

            MessageBox.Show("Successfully Saved", "Pastel Evolution", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End With
    End Sub

    Private Sub cmbInventoryItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbInventoryItem.KeyDown
        If e.KeyCode = Keys.Enter Then
            Delete_Row()
            Dim objSQL As New clsSqlConn
            Dim sBarCode As String = txtBarCode.Text.Trim
            For Each ugR In UG.Rows
                If ugR.Cells("Code").Value = cmbInventoryItem.Value Then
                    ugR.Cells("Quantity").Value += 1
                    'Autosave---------------------------------------------------------------------------
                    With objSQL
                        .Begin_Trans()
                        SQL = " UPDATE sbStkCount_t SET Qty = " & ugR.Cells("Quantity").Value & " WHERE StockID = " & ugR.Cells("StockID").Value & " AND AID =  " & iAgent & " "
                        If .Execute_Sql_Trans(SQL) = 0 Then
                            .Rollback_Trans()
                            Exit Sub
                        End If
                        .Commit_Trans()
                    End With
                    '-----------------------------------------------------------------------------------
                    cmbInventoryItem.ResetText()
                    txtBarCode.Focus()
                    Exit Sub
                End If
            Next

            objSQL = New clsSqlConn
            With objSQL
                SQL = "SELECT StkItem.StockLink, StkItem.Code, StkItem.Description_1, StkItem.Description_2, StkItem.Description_3, StkItem.WhseItem, " & _
                " StkItem.SerialItem, StkItem.cSimpleCode ,StkItem.Qty_On_Hand , StkItem.ucIINoOfUnits " & _
                " FROM " & _
                " StkItem WHERE Code = '" & cmbInventoryItem.Value & "' "
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                If DS.Tables(0).Rows.Count > 0 Then
                    Dim Dr As DataRow
                    For Each Dr In DS.Tables(0).Rows
                        If Dr("SerialItem") = True Then
                            MsgBox("This is a serial item. Please enter a serial number", MsgBoxStyle.Exclamation, "Pastel Evolution")
                            cmbInventoryItem.ResetText()
                            txtBarCode.Focus()
                            Exit Sub
                        End If
                        'StockLink, Code, Description_1, ItemGroup, SerialItem,WhseItem
                        Dim iStockLink As Integer = Dr("StockLink")
                        Dim sCode As String = Dr("Code")
                        Dim sDescription_1, sDescription_2 As String
                        sDescription_1 = Dr("Description_1")
                        sDescription_2 = Dr("Description_2")


                        Dim bWhseItem As Boolean = Dr("WhseItem")

                        Dim ugR2 As UltraGridRow

                        ugR2 = UG.DisplayLayout.Bands(0).AddNew
                        ugR2.Cells("Line").Value = ugR2.Index + 1
                        ugR2.Cells("Warehouse").Value = iWH
                        ugR2.Cells("StockID").Value = iStockLink
                        ugR2.Cells("Code").Value = sCode
                        ugR2.Cells("Description_1").Value = sDescription_1
                        ugR2.Cells("Description_2").Value = sDescription_2
                        ugR2.Cells("IsWH").Value = bWhseItem
                        ugR2.Cells("AvailableQty").Value = Dr("Qty_On_Hand")
                        ugR2.Cells("Quantity").Value = 1
                        ugR2.Cells("IsSerialNo").Value = False
                        'End If
                        'Autosave-----------------------------------------------------------------------------
                        .Begin_Trans()
                        SQL = " INSERT INTO sbStkCount_t ( StockID, Qty, AID) Values (" & iStockLink & ", 1 ," & iAgent & ") "
                        If .Execute_Sql_Trans(SQL) = 0 Then
                            .Rollback_Trans()
                            Exit Sub
                        End If
                        .Commit_Trans()
                        '--------------------------------------------------------------------------------------
                    Next
                    'Get_Total()
                Else
                    MsgBox("can not find Item", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    Exit Sub
                End If
            End With
            txtBarCode.ResetText()
            txtQty.Text = 1
            txtQty.Focus()
        End If
    End Sub

    Private Sub txtQty_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQty.KeyDown
        If e.KeyCode = Keys.Enter Then
            Delete_Row()
            Dim sBarCode As String = txtBarCode.Text.Trim
            For Each ugR In UG.Rows
                If ugR.Cells("Code").Value = cmbInventoryItem.Value Then
                    ugR.Cells("Quantity").Value = txtQty.Text
                    cmbInventoryItem.ResetText()
                    'Autosave---------------------------------------------------------------------------
                    Dim objSQL As New clsSqlConn
                    With objSQL
                        .Begin_Trans()
                        SQL = " UPDATE sbStkCount_t SET Qty = " & txtQty.Text & " WHERE StockID = " & ugR.Cells("StockID").Value & " AND AID =  " & iAgent & " "
                        If .Execute_Sql_Trans(SQL) = 0 Then
                            .Rollback_Trans()
                            Exit Sub
                        End If
                        .Commit_Trans()
                    End With
                    '-----------------------------------------------------------------------------------
                    cmbInventoryItem.Focus()
                    txtQty.Text = 0
                    Exit Sub
                End If
            Next

        End If
    End Sub

    Private Sub tsbNewlIne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ugR As UltraGridRow
        ugR = UG.DisplayLayout.Bands(0).AddNew
        ugR.Cells("Line").Value = ugR.Index + 1
        ugR.Cells("Line").Selected = True
        ugR.Cells("Warehouse").Value = iWH
        'If iDocType = DocType.SalesOrder Then
        '    ugR.Cells("TaxType").Value = 1
        '    For Each ugR1 As UltraGridRow In DDTaxType.Rows
        '        If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
        '            ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
        '        End If
        '    Next
        'ElseIf iDocType = DocType.PurchaseOrder Then
        '    ugR.Cells("TaxType").Value = 3
        '    For Each ugR1 As UltraGridRow In DDTaxType.Rows
        '        If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
        '            ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Me.Close()
    End Sub

    Private Sub tsbOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbOpen.Click
        Dim i As Integer
        Dim objSQL As New clsSqlConn
        With objSQL
            SQL = " SELECT     sbStkCount.Line, SCID, sbStkCount.StockID, StkItem.Code, StkItem.Description_1, sbStkCount.WHID, sbStkCount.Qty,  StkItem.Qty_On_Hand, StkItem.SerialItem,  sbStkCount.AID " & _
            " FROM sbStkCount INNER JOIN " & _
            " StkItem ON sbStkCount.StockID = StkItem.StockLink  "
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                For Each Dr In DS.Tables(0).Rows
                    Dim Row As UltraGridRow = UG.DisplayLayout.Bands(0).AddNew
                    Row.Cells("Line").Value = Dr.Item("Line")

                    Row.Cells("Code").Value = Dr.Item("Code")

                    Row.Cells("StockID").Value = Dr.Item("StockID")

                    Row.Cells("Description_1").Value = Dr.Item("Description_1")
                    'Row.Cells("Description_2").Value = Dr.Item("cDescription")
                    Row.Cells("Warehouse").Value = Dr.Item("WHID")
                    Row.Cells("Quantity").Value = Dr.Item("Qty")
                    Row.Cells("AvailableQty").Value = Dr.Item("Qty_On_Hand")
                    'Row.Cells("ConfirmQty").Value = IIf(Dr.Item("bIsSerialItem") = True, 0, Dr.Item("fQtyToProcess"))

                    'Row.Cells("ProcessedQty").Value = Dr.Item("fQtyProcessed")

                    'If Dr.Item("fQtyLastProcess") <> 0 Then
                    '    Row.Cells("ConfirmQty").Value = Dr.Item("fQtyLastProcess")
                    'End If
                    'Row.Cells("ConfirmQty").Value = 0

                    'Row.Cells("Price_Excl").Value = Math.Round(Dr.Item("fUnitPriceExcl"), 2, MidpointRounding.AwayFromZero)
                    'Row.Cells("Price_Incl").Value = Math.Round(Dr.Item("fUnitPriceIncl"), 2, MidpointRounding.AwayFromZero)
                    'Row.Cells("TaxType").Value = Dr.Item("iTaxTypeID")
                    'Row.Cells("Discount").Value = Math.Round(Dr.Item("fLineDiscount"), 6, MidpointRounding.AwayFromZero)
                    'Row.Cells("TaxRate").Value = Dr.Item("fTaxRate")
                    'Row.Cells("StockID").Value = Dr.Item("iStockCodeID")
                    'Row.Cells("IsWH").Value = IIf(Dr.Item("bIsWhseItem") = True, True, False)
                    'Row.Cells("LineNote").Value = Dr.Item("cLineNotes")
                    Row.Cells("IsSerialNo").Value = IIf(Dr.Item("SerialItem") = True, True, False)





                    SQL = "SELECT * FROM sbStkCountSN  " & _
                    " WHERE StockItem = " & Dr.Item("StockID")

                    DS = New DataSet
                    DS = .Get_Data_Sql(SQL)

                    Dim ugR As UltraGridRow

                    i = 0

                    For Each dr2 In DS.Tables(0).Rows

                        If Row.Band.Index = 0 Then
                            ugR = Row.ChildBands(0).Band.AddNew
                        Else
                            ugR = UG.DisplayLayout.Bands(1).AddNew
                        End If

                        'ugR.Cells("LN").Value = i + 1
                        ugR.Cells("SerialNumber").Value = dr2("Serialno")
                        ugR.Cells("BIN").Value = dr2("BIN")
                        'ugR.Cells("SNStockLink").Value = Dr.Item("StockItem")
                        ugR.Cells("PrimaryLineID").Value = ugR.ParentRow.Cells("StockID").Value
                        'ugR.Cells("Received").Value = IIf(dr2("Received") = True, 1, 0)
                        'ugR.ParentRow.Cells("ConfirmQty").Value = ugR.ParentRow.Cells("ConfirmQty").Value + IIf(dr2("Received") = True, 1, 0)

                        i = i + 1

                        'Row.Cells("Quantity").Value = ugR.ChildBands(0).Rows.Count
                    Next
                Next

                For Each ugR In UG.Rows
                    If ugR.Cells("IsSerialNo").Value = True Then
                        'ugR.Cells("ConfirmQty").Value = ugR.ChildBands(0).Rows.Count
                        ugR.Cells("Quantity").Value = ugR.ChildBands(0).Rows.Count
                    End If
                Next

                For Each ugR In UG.Rows
                    ugR.Expanded = False
                Next


            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
                'objSalesOrder = Nothing
            End Try
        End With

    End Sub

    Private Sub tsbDeleteLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbDeleteLine.Click
        If UG.Selected.Rows.Count > 0 Then
            Dim ugR As UltraGridRow
            For Each ugR In UG.Selected.Rows
                ugR.Delete(False)
            Next
        End If
    End Sub

    Private Sub txtQty_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.ValueChanged

    End Sub

    Private Sub cmbInventoryItem_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles cmbInventoryItem.InitializeLayout

    End Sub

    Private Sub cmbInventoryItem_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbInventoryItem.KeyPress

    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbUpdate.Click
        Dim DS1 As New DataSet
        Dim i, p As Integer
        Dim found As Boolean = False
        Dim objSQL As New clsSqlConn
        With objSQL
            SQL = " SELECT distinct StockID, WHID  FROM sbStkCount_t "
            SQL += " SELECT MAX(Line) AS Line FROM sbStkCount GROUP BY StockID"
            SQL += " SELECT DISTINCT Serialno  FROM sbStkCountSN_t"
            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            If IsDBNull(DS.Tables(1).Rows(0)(0)) Then
                i = 1
            Else
                i = DS.Tables(1).Rows(0)(0) + 1
            End If

            frmProgressbar.Show()
            For Each Dr In DS.Tables(0).Rows
                frmProgressbar.pb.Value = CInt(p / DS.Tables(0).Rows.Count * 100)
                SQL = " SELECT DISTINCT StockID FROM sbStkCount"
                DS1 = New DataSet
                DS1 = .Get_Data_Sql(SQL)
                For Each Dr1 In DS1.Tables(0).Rows
                    If Dr1("StockID") = Dr("StockID") Then
                        found = True
                    End If
                Next
                If found = False Then
                    .Begin_Trans()
                    SQL = " Insert  into sbStkCount ( Line, StockID, WHID, AID) SELECT distinct " & i & ", StockID, WHID, AID  FROM sbStkCount_t where StockID =  " & Dr("StockID") & " "
                    If .Execute_Sql_Trans(SQL) = 0 Then
                        .Rollback_Trans()
                        Exit Sub
                    End If
                    .Commit_Trans()
                    i += 1
                End If
                found = False
                p += 1
            Next
            i = 0

            found = False
            p = 0
            For Each Dr1 In DS.Tables(2).Rows
                frmProgressbar.pb.Value = p / DS.Tables(3).Rows.Count * 100
                SQL = " SELECT DISTINCT Serialno  FROM sbStkCountSN"
                DS1 = New DataSet
                DS1 = .Get_Data_Sql(SQL)
                For Each Dr In DS1.Tables(0).Rows
                    If Dr("Serialno") = Dr1("Serialno") Then
                        found = True
                        MessageBox.Show("Duplicate Serial Number found" & vbNewLine & "Line : " & vbNewLine & "S/N : " & Dr1("Serialno") & "", "Pastel Evolution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Next
                If found = False Then
                    .Begin_Trans()
                    SQL = " Insert into sbStkCountSN ( StockItem, WHID, Serialno, AID) SELECT distinct StockItem, WHID, Serialno, AID FROM sbStkCountSN_t WHERE Serialno = '" & Dr1("Serialno") & "' "
                    If .Execute_Sql_Trans(SQL) = 0 Then
                        .Rollback_Trans()
                        Exit Sub
                    End If
                    .Commit_Trans()
                End If
                found = False
                p += 1
            Next
            'frmProgressbar.pb.Value = 0
            'frmProgressbar.Close()

            '.Begin_Trans()
            'SQL = " DELETE FROM sbStkCount_t "
            'SQL += " DELETE FROM sbStkCountSN_t "
            'If .Execute_Sql_Trans(SQL) = 0 Then
            '    .Rollback_Trans()
            '    Exit Sub
            'End If
            '.Commit_Trans()
        End With
    End Sub

    Private Sub UG_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UG.InitializeLayout

    End Sub

    Private Sub txtBin_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBin.KeyDown
        Delete_Row()
        Dim sBinCode As String = UCase(txtBin.Text.Trim)
        If e.KeyCode = Keys.Enter Then
            txtBin.Enabled = False
            txtBarCode.Focus()
        End If

        'For Each ugR In UG.Rows
        '    If ugR.ChildBands.HasChildRows = True Then
        '        'Dim ugR1 As UltraGridRow
        '        'For Each ugR1 In ugR.ChildBands(0).Rows
        '        '    If sBarCode.Equals(ugR1.Cells("SerialNumber").Value) = True Then
        '        '        MessageBox.Show("Duplicate Serial Number found" & vbNewLine & "Line : " & ugR.Cells("Line").Value & "" & vbNewLine & "S/N : " & sBarCode & "", "Pastel Evolution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        '        txtBarCode.ResetText()
        '        '        txtBarCode.Focus()
        '        '        Exit Sub
        '        '    End If
        '        'Next
        '    End If
        'Next
    End Sub

    Private Sub txtBin_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBin.ValueChanged

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        txtBin.Enabled = True
        txtBin.ResetText()
        txtBin.Focus()
    End Sub

    Private Sub ToolStripButton2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Dim objSQL As New clsSqlConn
        With objSQL
            .Begin_Trans()
            SQL = " INSERT INTO sbTempCount SELECT WhseStk.WHStockLink, WhseStk.WHQtyOnHand, WhseStk.WHWhseID, StkItem.SerialItem FROM WhseStk INNER JOIN StkItem ON WhseStk.WHStockLink = StkItem.StockLink "
            SQL += " INSERT INTO sbTempCountSN SELECT SNStockLink, SerialNumber, SNDateLMove FROM SerialMF "
            SQL += " INSERT INTO sbTempCount ([StockID],[Qty],[WH]) VALUES (0, 0, 0) "
            SQL += " INSERT INTO sbTempCountSN ([StockID] ,[SerialNo] ,[Date]) VALUES (0, '0', '1/1/1999') "
            If .Execute_Sql_Trans(SQL) = 0 Then
                .Rollback_Trans()
                Exit Sub
            End If
            .Commit_Trans()
        End With
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Dim i As Integer
        Dim DS1 As New DataSet
        Dim found As Boolean
        Dim objSQL As New clsSqlConn
        With objSQL
            SQL = " SELECT iStockID, fQtyOut FROM _etblInvJrBatchLines "
            SQL += " SELECT SCID, Qty FROM sbStkCount_t "
            'SQL += " SELECT SNStockLink, SerialNumber FROM SerialMF WHERE WHERE SerialMF.CurrentLoc = 1 "

            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            For Each Dr In DS.Tables(0).Rows
                For Each Dr1 In DS.Tables(1).Rows
                    If Dr("iStockID") = Dr1("SCID") Then
                        found = True
                        i = Dr1("Qty")
                        Exit For
                    End If
                Next
                If found = True Then
                    SQL = " SELECT SerialNumber FROM SerialMF WHERE WHERE  SNStockLink =  " & Dr("iStockID") & "  CurrentLoc = 1 ORDER BY  SerialNumber "
                    DS1 = New DataSet
                    DS1 = .Get_Data_Sql(SQL)
                    While i <= Dr("fQtyOut")
                        For Each Dr2 In DS1.Tables(0).Rows
                            .Begin_Trans()
                            SQL = "INSERT INTO sbStkCountSN_t(StockItem, Serialno, AID, BIN, Random) " & _
                                " VALUES (" & Dr("iStockID") & ", " & _
                                " '" & Dr2("SerialNumber") & "' , " & iAgent & " , '" & txtBin.Text & "', 1)"
                            If .Execute_Sql_Trans(SQL) = 0 Then
                                .Rollback_Trans()
                                Exit Sub
                            End If
                            .Commit_Trans()
                            i += 1
                        Next
                    End While
                End If
            Next
        End With
    End Sub
End Class