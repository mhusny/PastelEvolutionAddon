'Imports Infragistics.Win.UltraWinGrid.UltraGridAction
Imports Infragistics.Win.UltraWinGrid
Imports System.Data
Imports System.Data.SqlClient
Imports Pastel.Evolution
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Imports System.IO

Public Class frmSalesOrder
    Dim sQuoteNo As String
    Dim sOrderNo As String
    Dim sDeliveryNo As String
    Dim sInvoiceNo As String
    Dim sGrvNo As String
    Dim sPONo As String
    Public iPONo As Integer
    Public iSONo As Integer
    Dim mDr As DataRow
    Public IsNew As Boolean
    Public iDocType As Integer
    Public iDocState As Integer
    Public iCusPriceList As Integer = 0
    Public iCusGroup As Integer = 0
    Public bIsInclusive As Boolean = False
    Public iWH As Integer = 0
    Public bIsAllowOverrideCreditLimit As Boolean = False
    Public bHideCost As Boolean = False
    Public conCLimit As String = 0
    Dim aDataBase As ArrayList
    Public convertion As Double

    Public OEPrice As Double
    Public OIPrice As Double
    Public isQuote As Boolean
    Public index As Integer
    Public group As String
    Public sSalesOrder As String


    Public Trans As SqlTransaction
    'Private Sub GetDatabaseName()
    '    aDataBase = New ArrayList()
    '    aDataBase.Add("dbUdawatta_new")
    '    'aDataBase.Add("dbKiribathgoda_new")
    '    'aDataBase.Add("dbKurunegala_new")
    '    aDataBase.Add("dbNawinna_new")
    '    aDataBase.Add("dbKelaniya_new")
    '    aDataBase.Add("dbMathara")
    'End Sub

    Public Sub DeleteRows()
        Dim ugR As UltraGridRow
        For Each ugR In UG.Rows.All
            ugR.Delete(False)
        Next
    End Sub

    Public Sub GET_DATA()
        If iDocType = DocType.SalesOrder Then
            SQL = "SELECT DCLink, Account, Name, Physical1, Physical2," & _
           " Physical3, Post1, Post2, Post3,iARPriceListNameID,iClassID, Credit_Limit, DCBalance, bCODAccount FROM Client  Order By Name "
          Else
            SQL = "SELECT DCLink, Account, Name, Physical1, Physical2," & _
           " Physical3, Post1, Post2, Post3 FROM Vendor"
          End If
        SQL += " SELECT idSalesRep, Code, Name FROM SalesRep WHERE Rep_On_Hold = 'N'  Order By Name "
        SQL += " SELECT ProjectLink, ProjectCode, ProjectName,ProjectDescription FROM Project Order By projectName"
        SQL += " SELECT StatusCounter, StatusDescrip FROM OrdersSt"
        SQL += " SELECT idIncidentPriority, cDescription FROM _rtblIncidentPriority"
        If iDocType = DocType.SalesOrder Then
            SQL += " SELECT     StkItem.StockLink, StkItem.Code, StkItem.Description_1, StkItem.Description_2, StkItem.ItemGroup, StkItem.bLotItem AS LotItem, StkItem.WhseItem, " & _
           " StkItem.iUOMStockingUnitID, _etblInvSegValue.cValue " & _
           " FROM         StkItem INNER JOIN" & _
           "  _etblInvSegValue ON StkItem.iInvSegValue1ID = _etblInvSegValue.idInvSegValue " & _
           "  WHERE(StkItem.ItemActive = 1) AND  (Description_1 LIKE 'WH%') " & _
           " ORDER BY StkItem.Description_2 "
        Else
            SQL += " SELECT     StkItem.StockLink, StkItem.Code, StkItem.Description_1, StkItem.ItemGroup, StkItem.bLotItem AS LotItem, StkItem.WhseItem, StkItem.Description_2, " & _
          " StkItem.iUOMStockingUnitID, _etblInvSegValue.cValue , _btblBINLocation.cBinLocationName as Bin " & _
          " FROM         StkItem INNER JOIN" & _
          "  _etblInvSegValue ON StkItem.iInvSegValue1ID = _etblInvSegValue.idInvSegValue  LEFT OUTER JOIN " & _
          " _btblBINLocation ON StkItem.iBinLocationID = _btblBINLocation.idBinLocation " & _
          "  WHERE(StkItem.ItemActive = 1)  " & _
          " ORDER BY StkItem.Description_1 "
        End If
        SQL += " SELECT WhseLink, Code, Name, KnownAs FROM WhseMst WHERE KnownAs = 'PKW1' OR KnownAs = 'PKW2'"
        SQL += " SELECT idTaxRate, Code, Description, TaxRate FROM TaxRate"
        SQL += " SELECT bIsInclusive FROM StDfTbl"
        SQL += " SELECT WhseLink FROM WhseMst WHERE DefaultWhse = 1"
        SQL += " SELECT idAreas, Code, Description FROM Areas"
        SQL += " SELECT idAgents, cAgentName, iDefProjectID from _rtblAgents WHERE idAgents = " & iAgent & " "


        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                'If DS.Tables(0).Rows.Count > 0 Then
                cmbCustomer.DataSource = DS.Tables(0)
                cmbCustomer.ValueMember = "DCLink"
                cmbCustomer.DisplayMember = "Name"
                'End If
                cmbCustomer.DisplayLayout.Bands(0).Columns("DCLink").Hidden = True
                cmbCustomer.DisplayLayout.Bands(0).Columns("Physical1").Hidden = True
                cmbCustomer.DisplayLayout.Bands(0).Columns("Physical2").Hidden = True
                cmbCustomer.DisplayLayout.Bands(0).Columns("Physical3").Hidden = True
                cmbCustomer.DisplayLayout.Bands(0).Columns("Post1").Hidden = True
                cmbCustomer.DisplayLayout.Bands(0).Columns("Post2").Hidden = True
                cmbCustomer.DisplayLayout.Bands(0).Columns("Post3").Hidden = True
                If iDocType = DocType.SalesOrder Then
                    cmbCustomer.DisplayLayout.Bands(0).Columns("bCODAccount").Hidden = True
                End If

                'Sales Rep
                'If DS.Tables(1).Rows.Count > 0 Then
                cmbSaleRep.DataSource = DS.Tables(1)
                cmbSaleRep.ValueMember = "idSalesRep"
                cmbSaleRep.DisplayMember = "Name"
                'End If
                cmbSaleRep.DisplayLayout.Bands(0).Columns("idSalesRep").Hidden = True

                'Projects
                'If DS.Tables(2).Rows.Count > 0 Then
                cmbProject.DataSource = DS.Tables(2)
                cmbProject.ValueMember = "ProjectLink"
                cmbProject.DisplayMember = "ProjectName"
                'End If
                cmbProject.DisplayLayout.Bands(0).Columns("ProjectLink").Hidden = True

                'Order Status
                'If DS.Tables(3).Rows.Count > 0 Then
                cmbOrderStatus.DataSource = DS.Tables(3)
                cmbOrderStatus.ValueMember = "StatusCounter"
                cmbOrderStatus.DisplayMember = "StatusDescrip"
                'End If
                cmbOrderStatus.DisplayLayout.Bands(0).Columns("StatusCounter").Hidden = True

                'Priority
                'If DS.Tables(4).Rows.Count > 0 Then
                cmbPriority.DataSource = DS.Tables(4)
                cmbPriority.ValueMember = "idIncidentPriority"
                cmbPriority.DisplayMember = "cDescription"
                'End If
                cmbPriority.DisplayLayout.Bands(0).Columns("idIncidentPriority").Hidden = True

                'Stock
                'If DS.Tables(5).Rows.Count > 0 Then
                DDStock.DataSource = DS.Tables(5)
                DDStock.ValueMember = "StockLink"
                DDStock.DisplayMember = "Code"
                'End If
                DDStock.DisplayLayout.Bands(0).Columns("Description_1").Header.Caption = "Description"
                'If DS.Tables(5).Rows.Count > 0 Then
                DDDescription.DataSource = DS.Tables(5)
                DDDescription.ValueMember = "Description_1"
                DDDescription.DisplayMember = "Description_1"
                'End If
                DDDescription.DisplayLayout.Bands(0).Columns("Description_1").Header.Caption = "Description"

                'WH
                'If DS.Tables(6).Rows.Count > 0 Then
                DDWH.DataSource = DS.Tables(6)
                DDWH.ValueMember = "WhseLink"
                DDWH.DisplayMember = "Code"
                'End If
                DDWH.DisplayLayout.Bands(0).Columns("WhseLink").Hidden = True

                'Tax
                'If DS.Tables(7).Rows.Count > 0 Then
                DDTaxType.DataSource = DS.Tables(7)
                DDTaxType.ValueMember = "idTaxRate"
                DDTaxType.DisplayMember = "Code"
                'End If
                DDTaxType.DisplayLayout.Bands(0).Columns("idTaxRate").Hidden = True

                Dim Dr As DataRow
                For Each Dr In DS.Tables(8).Rows
                    If Dr("bIsInclusive") = False Then
                        bIsInclusive = False
                    ElseIf Dr("bIsInclusive") = True Then
                        bIsInclusive = True
                    End If
                Next
                For Each Dr In DS.Tables(9).Rows
                    iWH = CInt(Dr("WhseLink"))
                Next

                'Area
                cmbArea.DataSource = DS.Tables(10)
                cmbArea.ValueMember = "idAreas"
                cmbArea.DisplayMember = "Description"
                'End If
                cmbArea.DisplayLayout.Bands(0).Columns("idAreas").Hidden = True
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                Exit Sub
            Finally
                objSQL = Nothing
            End Try
        End With

    End Sub
    Public Sub DocumentSettings(ByVal iDocType As Integer)
        Select Case iDocType
            Case DocType.PurchaseOrder
                lbl1.Text = " GRV Number"
                lbl2.Text = " Order Number"
                lbl3.Text = " Supplier Invoice"
                txtDelNote_SupInvNo.Enabled = True
                cmbSaleRep.Visible = False
                lbl4.Text = " Supplier"
                lbl5.Appearance.BackColor = Color.Maroon
                lbl5.Appearance.BackColor2 = Color.DarkRed
                lbl5.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
                lbl6.Appearance.BackColor = Color.Maroon
                lbl6.Appearance.BackColor2 = Color.DarkRed
                lbl6.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
                lblSalesRep.Visible = False
                lblBarcode.Visible = True
                txtBarCode.Visible = True
                lblOrderType.Visible = True
                cmbType.Visible = True
            Case DocType.SalesOrder
                lbl1.Text = " Invoice Number"
                lbl2.Text = " Order Number"
                lbl3.Text = " Delivery Note"
                txtDelNote_SupInvNo.Enabled = False
                cmbSaleRep.Visible = True
                lbl4.Text = " Customer"
                lbl5.Appearance.BackColor = Color.Green
                lbl5.Appearance.BackColor2 = Color.DarkGreen
                lbl5.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
                lbl6.Appearance.BackColor = Color.Green
                lbl6.Appearance.BackColor2 = Color.DarkGreen
                lbl6.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50
                lblSalesRep.Visible = True
                lblBarcode.Visible = True
                txtBarCode.Visible = True
                lblOrderType.Visible = False
                cmbType.Visible = False

        End Select
    End Sub

    Private Sub frmSalesOrder_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Discard()
    End Sub
    Public Sub SetSatus()
        If IsNew = True Then
            lblStatus.Text = "NEW"
            If iDocType = DocType.SalesOrder Then
                tsbSaveSO.Enabled = True
                tsbUpdateSO.Enabled = True
                tsbSavePO.Visible = False
                tsbUpdatePO.Visible = False
                tsbProcessPO.Visible = False

            ElseIf iDocType = DocType.PurchaseOrder Then
                tsbSavePO.Enabled = True
                tsbUpdatePO.Enabled = False
                tsbProcessPO.Enabled = False
                tsbSaveSO.Visible = False
                tsbUpdateSO.Visible = False
            End If
        Else
            lblStatus.Text = "EDIT"
            If iDocType = DocType.SalesOrder Then
                tsbSaveSO.Enabled = True
                tsbUpdateSO.Enabled = True
                tsbSavePO.Visible = False
                tsbUpdatePO.Visible = False
                tsbProcessPO.Visible = False
                tsbSaveSO.Visible = True
                tsbUpdatePO.Visible = False
            ElseIf iDocType = DocType.PurchaseOrder Then
                tsbSavePO.Enabled = False
                tsbUpdatePO.Enabled = True
                tsbProcessPO.Enabled = True
                tsbSaveSO.Visible = False
                tsbUpdatePO.Visible = False
                tsbSavePO.Visible = True
                tsbUpdatePO.Visible = True
                tsbProcessPO.Visible = True
            End If
        End If
    End Sub

    Private Sub frmSalesOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyData = Keys.F9 Then
            If iDocType = DocType.SalesOrder Then
                tsbSaveSO_Click(tsbSaveSO, e)
            End If
        End If
    End Sub
    Public Sub Get_Access_Permission_For_Agent()
        '    Dim objSQL As New clsSqlConn
        '    With objSQL
        '        SQL = "SELECT iManu FROM Spil_Access_Rights WHERE idAgents =" & iAgent & ""
        '        DS = New DataSet
        '        DS = .GET_DATA_SQL(SQL)
        '        If DS.Tables(0).Rows.Count > 0 Then
        '            Dim R As DataRow
        '            For Each R In DS.Tables(0).Rows
        '                Dim mRootNode As UltraTreeNode = TC.GetNodeByKey("2000")
        '                If R("iManu") = mRootNode.Tag Then
        '                    mRootNode.CheckedState = CheckState.Checked
        '                End If
        '                If mRootNode.HasNodes = True Then
        '                    Dim iNode1 As UltraTreeNode
        '                    For Each iNode1 In mRootNode.Nodes
        '                        If R("iManu") = iNode1.Tag Then
        '                            iNode1.CheckedState = CheckState.Checked
        '                        End If
        '                        If iNode1.HasNodes = True Then
        '                            Dim iNode2 As UltraTreeNode
        '                            For Each iNode2 In iNode1.Nodes
        '                                If R("iManu") = iNode2.Tag Then
        '                                    iNode2.CheckedState = CheckState.Checked
        '                                End If
        '                                If iNode2.HasNodes = True Then
        '                                    Dim iNode3 As UltraTreeNode
        '                                    For Each iNode3 In iNode2.Nodes
        '                                        If R("iManu") = iNode3.Tag Then
        '                                            iNode3.CheckedState = CheckState.Checked
        '                                        End If
        '                                        If iNode3.HasNodes = True Then
        '                                            Dim iNode4 As UltraTreeNode
        '                                            For Each iNode4 In iNode3.Nodes
        '                                                If R("iManu") = iNode4.Tag Then
        '                                                    iNode4.CheckedState = CheckState.Checked
        '                                                End If
        '                                                If iNode4.HasNodes = True Then
        '                                                    Dim iNode5 As UltraTreeNode
        '                                                    For Each iNode5 In iNode4.Nodes
        '                                                        If R("iManu") = iNode5.Tag Then
        '                                                            iNode5.CheckedState = CheckState.Checked
        '                                                        End If
        '                                                    Next
        '                                                End If
        '                                            Next
        '                                        End If
        '                                    Next
        '                                End If
        '                            Next
        '                        End If
        '                    Next
        '                End If

        '            Next
        '        End If
        '    End With


    End Sub
    Private Sub frmSalesOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UG.DisplayLayout.Bands(0).Columns("Code").CellActivation = Activation.AllowEdit
        UG.DisplayLayout.Bands(0).Columns("Description_1").CellActivation = Activation.AllowEdit
        UG.DisplayLayout.Bands(0).Columns("Description_2").CellActivation = Activation.AllowEdit

        UG.DisplayLayout.Bands(0).Columns("Discount").CellActivation = Activation.AllowEdit
        UG.DisplayLayout.Bands(0).Columns("DiscountA").CellActivation = Activation.AllowEdit
        UG.DisplayLayout.Bands(0).Columns("Line_Dis_Incl").CellActivation = Activation.AllowEdit
        UG.DisplayLayout.Bands(0).Columns("Line_Dis_Excl").CellActivation = Activation.AllowEdit

        UG.DisplayLayout.Bands(0).Columns("Price_Incl").CellActivation = Activation.AllowEdit
        UG.DisplayLayout.Bands(0).Columns("Price_Excl").CellActivation = Activation.AllowEdit

        convertion = 1 'if items not a unit of measure quantity will bw multiplied by 1

        If IsDBNull(DS.Tables(11).Rows(0)(2)) Then
            cmbProject.Value = 0
        Else
            cmbProject.Value = DS.Tables(11).Rows(0)(2)
        End If


        If iDocType = DocType.SalesOrder Then




            If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "SO.xml") Then
                UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "SO.xml")
            End If

            'If comDocState = DocState.Quote Then
            '    dtpInDate.Enabled = True
            '    dtpInDate.DateTime = Now.Date()
            'Else
            '    dtpInDate.Enabled = False
            'End If

            Dim agent = New Agent(iAgent)

            If agent.Description = 16 Then
                cmbArea.Value = 32
            End If

            cmbSaleRep.Text = agent.Name

        ElseIf iDocType = DocType.PurchaseOrder Then
            'If sSQLSrvDataBase = "dbUdawatta_new" Or sSQLSrvDataBase = "dbKelaniya_new" Then
            tsbProcessPO.Visible = True
            tsbUpdatePO.Visible = True
            tsbSavePO.Visible = True
            tsbKelaniya.Visible = True
            'Else
            '    tsbProcessPO.Visible = False
            '    tsbUpdatePO.Visible = False
            '    tsbSavePO.Visible = False
            '    tsbKelaniya.Visible = False
            'End If



            If My.Computer.FileSystem.FileExists(Application.StartupPath & "\" & sAgent & "PO.xml") Then
                UG.DisplayLayout.LoadFromXml(Application.StartupPath & "\" & sAgent & "PO.xml")
                UG.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortSingle
            End If

            tsbKelaniya.Visible = True

            dtpInDate.Enabled = True

        End If

        If bIsInclusive = True Then
            cbInEx.Checked = True
            cbInEx_CheckedChanged(cbInEx, e)
        ElseIf bIsInclusive = False Then
            cbInEx.Checked = False
            cbInEx_CheckedChanged(cbInEx, e)
        End If

        'UG.DisplayLayout.c = Infragistics.Win.DefaultableBoolean.False
        UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton

        Dim Dr As DataRow
        For Each Dr In dsManu.Tables(0).Rows
            If iDocType = DocType.SalesOrder Then

                UG.DisplayLayout.Bands(0).Columns("SerialLot").Hidden = True

                If 22213 = Dr("iManu") Then
                    UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                    UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                    'UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default
                ElseIf 22215 = Dr("iManu") Then
                    bIsAllowOverrideCreditLimit = True
                ElseIf 22216 = Dr("iManu") Then
                    dtpInDate.Enabled = False
                ElseIf 22217 = Dr("iManu") Then
                    'UG.DisplayLayout.Bands(0).Columns("Description_1").CellActivation = Activation.Disabled
                    UG.DisplayLayout.Bands(0).Columns("Code").CellActivation = Activation.Disabled
                    UG.DisplayLayout.Bands(0).Columns("Description_2").CellActivation = Activation.Disabled
                ElseIf 22218 = Dr("iManu") Then
                    cmbSaleRep.Enabled = False
                ElseIf 22219 = Dr("iManu") Then
                    UG.DisplayLayout.Bands(0).Columns("Discount").CellActivation = Activation.Disabled
                    UG.DisplayLayout.Bands(0).Columns("DiscountA").CellActivation = Activation.Disabled
                    UG.DisplayLayout.Bands(0).Columns("Line_Dis_Incl").CellActivation = Activation.Disabled
                    UG.DisplayLayout.Bands(0).Columns("Line_Dis_Excl").CellActivation = Activation.Disabled
                ElseIf 22220 = Dr("iManu") Then
                    UG.DisplayLayout.Bands(0).Columns("Price_Incl").CellActivation = Activation.Disabled
                    UG.DisplayLayout.Bands(0).Columns("Price_Excl").CellActivation = Activation.Disabled
                End If

            ElseIf iDocType = DocType.PurchaseOrder Then
                If 22223 = Dr("iManu") Then
                    UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
                    UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
                    'UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default

                ElseIf 22227 = Dr("iManu") Then
                    UG.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.Default

                End If
            End If
        Next
        'GetDatabaseName()
        SetSatus()


        UG.DisplayLayout.Bands(0).Columns("UOM").ValueList = DDUnit
        UG.DisplayLayout.Bands(0).Columns("Lot").ValueList = DDLot
        UG.DisplayLayout.Bands(0).Columns("DiscountA").DefaultCellValue = 0.0
        'UG.DisplayLayout.Bands(0).Columns("Code").CellActivation = Activation.AllowEdit

        'cmbArea.Value = 5

        cmbClass.SelectedIndex = 1

    End Sub
    Private Sub DDStock_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDStock.Click
        Try
            If DDStock.ActiveRow.Activated = True Then
                UG.ActiveRow.Cells("StockID").Value = DDStock.ActiveRow.Cells("StockLink").Value
                UG.ActiveRow.Cells("Code").Value = DDStock.ActiveRow.Cells("StockLink").Value
                UG.ActiveRow.Cells("Description_1").Value = DDStock.ActiveRow.Cells("Description_1").Value
                UG.ActiveRow.Cells("Description_2").Value = DDStock.ActiveRow.Cells("Description_2").Value
                UG.ActiveRow.Cells("IsLot").Value = IIf(DDStock.ActiveRow.Cells("LotItem").Value = True, True, False)
                UG.ActiveRow.Cells("IsWH").Value = IIf(DDStock.ActiveRow.Cells("WhseItem").Value = True, True, False)
                UG.ActiveRow.Cells("iUnit").Value = DDStock.ActiveRow.Cells("iUOMStockingUnitID").Value
                UG.ActiveRow.Cells("cBin").Value = DDStock.ActiveRow.Cells("Bin").Value

            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Private Sub DDDescription_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDDescription.Click
        Try
            If DDDescription.ActiveRow.Activated = True Then
                UG.ActiveRow.Cells("StockID").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                UG.ActiveRow.Cells("Code").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                UG.ActiveRow.Cells("Description_1").Value = DDDescription.ActiveRow.Cells("Description_1").Value
                UG.ActiveRow.Cells("Description_2").Value = DDDescription.ActiveRow.Cells("Description_2").Value
                UG.ActiveRow.Cells("IsLot").Value = IIf(DDDescription.ActiveRow.Cells("LotItem").Value = True, True, False)
                UG.ActiveRow.Cells("IsWH").Value = IIf(DDDescription.ActiveRow.Cells("WhseItem").Value = True, True, False)
                UG.ActiveRow.Cells("cBin").Value = DDStock.ActiveRow.Cells("Bin").Value
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Private Sub DDDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DDDescription.KeyDown
        'If e.KeyCode = Keys.Enter Then
        '    If DDDescription.ActiveRow.Activated = True Then
        '        UG.ActiveRow.Cells("StockID").Value = DDDescription.ActiveRow.Cells("StockLink").Value
        '        UG.ActiveRow.Cells("Code").Value = DDDescription.ActiveRow.Cells("StockLink").Value
        '        UG.ActiveRow.Cells("Description_1").Value = DDDescription.ActiveRow.Cells("Description_1").Value
        '        UG.ActiveRow.Cells("Description_2").Value = DDDescription.ActiveRow.Cells("Description_2").Value
        '        UG.ActiveRow.Cells("IsLot").Value = IIf(DDDescription.ActiveRow.Cells("LotItem").Value = True, True, False)
        '        UG.ActiveRow.Cells("IsWH").Value = IIf(DDDescription.ActiveRow.Cells("WhseItem").Value = True, True, False)

        '    End If
        'End If
    End Sub

    Private Sub DDTaxType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDTaxType.Click
        Try
            If DDTaxType.ActiveRow.Activated = True Then
                UG.ActiveRow.Cells("TaxType").Value = DDTaxType.ActiveRow.Cells("Code").Value
                UG.ActiveRow.Cells("TaxRate").Value = DDTaxType.ActiveRow.Cells("TaxRate").Value
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Private Sub tsmToggleInclusiveExclusive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmToggleInclusiveExclusive.Click
        If cbInEx.Checked = True Then
            If MsgBox(" Change From Tax Inclusive To Tax Exclusive ? ", MsgBoxStyle.YesNo, "New Sales Order " & txtOrdNo.Text & "  " & txtInNo.Text) = MsgBoxResult.Yes Then
                bIsInclusive = False
                cbInEx.Checked = False
                cbInEx_CheckedChanged(cbInEx, e)
            End If
        ElseIf cbInEx.Checked = False Then
            If MsgBox(" Change From Tax Excusive To Tax Inclusive ? ", MsgBoxStyle.YesNo, "New Sales Order " & txtOrdNo.Text & "  " & txtInNo.Text) = MsgBoxResult.Yes Then
                bIsInclusive = True
                cbInEx.Checked = True
                cbInEx_CheckedChanged(cbInEx, e)
            End If
        End If
    End Sub
    Private Sub cbInEx_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbInEx.CheckedChanged
        If cbInEx.Checked = True Then
            UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("OrderTotal_Excl").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("LineTotal_Excl").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = False
            UG.DisplayLayout.Bands(0).Columns("OrderTotal_Incl").Hidden = False
            UG.DisplayLayout.Bands(0).Columns("LineTotal_Incl").Hidden = False
            lblDis.Text = "Incl"
        ElseIf cbInEx.Checked = False Then
            UG.DisplayLayout.Bands(0).Columns("Price_Incl").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("OrderTotal_Incl").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("LineTotal_Incl").Hidden = True
            UG.DisplayLayout.Bands(0).Columns("Price_Excl").Hidden = False
            UG.DisplayLayout.Bands(0).Columns("OrderTotal_Excl").Hidden = False
            UG.DisplayLayout.Bands(0).Columns("LineTotal_Excl").Hidden = False
            lblDis.Text = "Excl"
        End If

    End Sub
    Public Function LineTotal_Incl(ByVal bInclusive As Boolean, ByVal Qty As Double, ByVal Discount As Double, ByVal PriceExcl As Double, ByVal PriceIncl As Double, ByVal TaxRate As Double) As Double
        Dim dblLineTotalIncl As Double = 0
        'round( [ConfirmQty]   * [Price_Incl] - ((  [ConfirmQty]   * [Price_Incl] )* [Discount] /100) , 2 , 0 )
        If Discount = 0 Then
            dblLineTotalIncl = Math.Round((Qty * PriceIncl), 2, MidpointRounding.AwayFromZero)
        ElseIf Discount > 0 Then
            dblLineTotalIncl = Math.Round((Qty * PriceIncl) - ((Qty * PriceIncl) * Discount / 100), 2, MidpointRounding.AwayFromZero)
        End If

        Return dblLineTotalIncl
    End Function

    Public Function LineTotal_Excl(ByVal bInclusive As Boolean, ByVal Qty As Double, ByVal Discount As Double, ByVal PriceExcl As Double, ByVal PriceIncl As Double, ByVal TaxRate As Double) As Double
        Dim dblLineTotalExcl As Double = 0
        'LineTotal_Excl = round( [ConfirmQty]   * [Price_Excl] - ((  [ConfirmQty]   * [Price_Excl] )* [Discount] /100) , 2 , 0 )

        dblLineTotalExcl = Math.Round((Qty * PriceExcl) - ((Qty * PriceExcl) * Discount / 100), 2, MidpointRounding.AwayFromZero)
        Return dblLineTotalExcl
    End Function
    Public Function LineTax(ByVal bInclusive As Boolean, ByVal Qty As Double, ByVal Discount As Double, ByVal PriceExcl As Double, ByVal PriceIncl As Double, ByVal TaxRate As Double) As Double
        Dim dblLineTax As Double = 0
        Dim dblLineTotalExcl As Double = 0
        Dim dblLineTotalInclDis As Double = 0
        ' round( ([ConfirmQty] * [Price_Excl]  - (([ConfirmQty] * [Price_Excl] )* [Discount] /100))* [TaxRate] /100 , 2 , 0 )
        If bInclusive = True Then
            If TaxRate > 0 Then
                If Discount > 0 Then
                    dblLineTotalExcl = Math.Round((Qty * PriceIncl) * 100 / (100 + TaxRate), 2, MidpointRounding.AwayFromZero)
                    dblLineTotalInclDis = dblLineTotalExcl - dblLineTotalExcl * Discount / 100
                    dblLineTax = dblLineTotalInclDis * TaxRate / 100
                Else
                    dblLineTax = Math.Round((Qty * PriceIncl) - ((Qty * PriceIncl) * 100 / (100 + TaxRate)), 2, MidpointRounding.AwayFromZero)
                End If

            ElseIf TaxRate = 0 Then
                dblLineTax = 0
            End If
        ElseIf bInclusive = False Then
            If TaxRate > 0 Then
                dblLineTax = Math.Round(((Qty * PriceExcl) - ((Qty * PriceExcl) * Discount / 100)) * TaxRate / 100, 2, MidpointRounding.AwayFromZero)
            ElseIf TaxRate = 0 Then
                dblLineTax = 0
            End If
        End If
        Return dblLineTax
    End Function
    Public Function LineDis_Excl(ByVal bInclusive As Boolean, ByVal Qty As Double, ByVal Discount As Double, ByVal PriceExcl As Double, ByVal PriceIncl As Double, ByVal TaxRate As Double) As Double
        Dim dblLineDisExcl As Double = 0
        ' round( ( [ConfirmQty] * [Price_Excl] )* [Discount] /100 , 2 , 0 )
        dblLineDisExcl = Math.Round((Qty * PriceExcl) * Discount / 100, 2, MidpointRounding.AwayFromZero)
        Return dblLineDisExcl
    End Function
    Public Function LineDis_Incl(ByVal bInclusive As Boolean, ByVal Qty As Double, ByVal Discount As Double, ByVal PriceExcl As Double, ByVal PriceIncl As Double, ByVal TaxRate As Double) As Double
        Dim dblLineDisIncl As Double = 0
        ' round( ([ConfirmQty] * [Price_Incl] )* [Discount] /100 , 2 , 0 )
        dblLineDisIncl = Math.Round((Qty * PriceIncl) * Discount / 100, 2, MidpointRounding.AwayFromZero)
        Return dblLineDisIncl
    End Function
    Private Sub UG_AfterCellUpdate(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UG.AfterCellUpdate
        If IsNew = True Then
            If e.Cell.Column.Key = "StockID" Then
                If iDocType = DocType.SalesOrder Then
                    SQL = "SELECT     fExclPrice, fInclPrice FROM _etblPriceListPrices WHERE iPriceListNameID =" & iCusPriceList & " AND iStockID =" & e.Cell.Value & ""
                    Dim objSQL As New clsSqlConn
                    With objSQL
                        DS = New DataSet
                        DS = .Get_Data_Sql(SQL)
                        If DS.Tables(0).Rows.Count > 0 Then
                            Dim Dr As DataRow
                            For Each Dr In DS.Tables(0).Rows
                                e.Cell.Row.Cells("Price_Excl").Value = Dr("fExclPrice")
                                e.Cell.Row.Cells("Price_Incl").Value = Dr("fInclPrice")
                            Next
                        Else
                            e.Cell.Row.Cells("Price_Excl").Value = 0
                            e.Cell.Row.Cells("Price_Incl").Value = 0
                        End If
                    End With
                End If
                e.Cell.Row.Cells("AvailableQty").Value = Get_Available_Qty("SELECT WHQtyOnHand FROM WhseStk WHERE WHStockLink =" & e.Cell.Value & " AND WHWhseID =" & e.Cell.Row.Cells("Warehouse").Value & "")

                OEPrice = UG.ActiveRow.Cells("Price_Excl").Value
                OIPrice = UG.ActiveRow.Cells("Price_Incl").Value
            ElseIf e.Cell.Column.Key = "Code" Then
                'bValChange = False

                SQL = " SELECT StkItem.WhseItem, StkItem.iUOMDefPurchaseUnitID, StkItem.StockLink, StkItem.Code, " & _
                        " StkItem.Description_1, StkItem.Description_2, StkItem.ItemGroup, StkItem.bLotItem as LotItem , StkItem.LatUCst, " & _
                        " StkItem.AveUCst, _etblUnits.iUnitCategoryID, StkItem.iItemCostingMethod, GrpTbl.fCostMargine, StkItem.ServiceItem FROM StkItem LEFT OUTER JOIN " & _
                        " GrpTbl ON StkItem.ItemGroup = GrpTbl.StGroup LEFT OUTER JOIN _etblUnits ON StkItem.iUOMStockingUnitID = _etblUnits.idUnits " & _
                        " WHERE  StkItem.StockLink  = " & e.Cell.Value & " ORDER BY StkItem.Code"

                Dim objSQL As New clsSqlConn
                With objSQL

                    Dim DS1 = New DataSet
                    DS1 = .Get_Data_Sql(SQL)

                    Dim Dr1 As DataRow

                    If DS1.Tables(0).Rows(0)("ServiceItem") = 1 Then
                        e.Cell.Row.Cells("IsLot").Value = 0
                    Else
                        If DS1.Tables(0).Rows.Count > 0 Then

                            For Each Dr1 In DS1.Tables(0).Rows
                                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                e.Cell.Row.Cells("IsLot").Value = Dr1.Item("LotItem")
                                e.Cell.Row.Cells("IsWH").Value = Dr1.Item("WhseItem")
                            Next
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        End If
                    End If

                End With


            ElseIf e.Cell.Column.Key = "Quantity" Or e.Cell.Column.Key = "Price_Excl" Or e.Cell.Column.Key = "Price_Incl" Or _
                e.Cell.Column.Key = "TaxRate" Or e.Cell.Column.Key = "Discount" Then
                With e.Cell.Row
                    If iDocType = DocType.SalesOrder Then
                        .Cells("ConfirmQty").Value = .Cells("Quantity").Value
                    End If
                    If iDocType = DocType.PurchaseOrder Then
                        .Cells("ConfirmQty").Value = .Cells("Quantity").Value
                    End If
                    .Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("OrderTax").Value = LineTax(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("LineTax").Value = LineTax(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    .Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                End With

            
            End If

            'if it is a serial item disable qty and confirm qty
            'If iDocType = DocType.SalesOrder Then
            '    If e.Cell.Column.Key = "Description_1" Then
            '        If e.Cell.Row.Cells("IsLot").Value = True Then
            '            e.Cell.Row.Cells("ConfirmQty").Activation = Activation.Disabled
            '            e.Cell.Row.Cells("ConfirmQty").Appearance.BackColorDisabled = Color.Silver
            '            e.Cell.Row.Cells("Quantity").Activation = Activation.Disabled
            '            e.Cell.Row.Cells("Quantity").Appearance.BackColorDisabled = Color.Silver
            '        Else
            '            e.Cell.Row.Cells("ConfirmQty").Activation = Activation.AllowEdit
            '            e.Cell.Row.Cells("ConfirmQty").Appearance.BackColorDisabled = Color.White
            '            e.Cell.Row.Cells("Quantity").Activation = Activation.AllowEdit
            '            e.Cell.Row.Cells("Quantity").Appearance.BackColorDisabled = Color.White
            '        End If
            '    End If
            'End If
        Else
           


        If e.Cell.Column.Key = "Discount" Then
            With e.Cell.Row
                '.Value = .Text
                If iDocType = DocType.SalesOrder Then
                    e.Cell.Row.Cells("ConfirmQty").Value = e.Cell.Row.Cells("Quantity").Value
                End If
                e.Cell.Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("OrderTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("LineTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
            End With
        ElseIf e.Cell.Column.Key = "Price_Incl" Then
            With e.Cell.Row
                '.Value = .Text
                If iDocType = DocType.SalesOrder Then
                    e.Cell.Row.Cells("ConfirmQty").Value = e.Cell.Row.Cells("Quantity").Value
                End If
                e.Cell.Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("OrderTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("LineTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
            End With
        ElseIf e.Cell.Column.Key = "StockID" Then
                With e.Cell.Row


                    If iDocType = DocType.SalesOrder Then
                        SQL = "SELECT     fExclPrice, fInclPrice FROM _etblPriceListPrices WHERE iPriceListNameID =" & iCusPriceList & " AND iStockID =" & e.Cell.Value & ""
                        Dim objSQL As New clsSqlConn
                        With objSQL
                            DS = New DataSet
                            DS = .Get_Data_Sql(SQL)
                            If DS.Tables(0).Rows.Count > 0 Then
                                Dim Dr As DataRow
                                For Each Dr In DS.Tables(0).Rows
                                    e.Cell.Row.Cells("Price_Excl").Value = Dr("fExclPrice")
                                    e.Cell.Row.Cells("Price_Incl").Value = Dr("fInclPrice")
                                Next
                            Else
                                e.Cell.Row.Cells("Price_Excl").Value = 0
                                e.Cell.Row.Cells("Price_Incl").Value = 0
                            End If
                        End With
                    End If



                    '.Value = .Text
                    If iDocType = DocType.SalesOrder Then
                        e.Cell.Row.Cells("ConfirmQty").Value = e.Cell.Row.Cells("Quantity").Value
                    End If
                    e.Cell.Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("OrderTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("LineTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    e.Cell.Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)

                    e.Cell.Row.Cells("AvailableQty").Value = Get_Available_Qty("SELECT WHQtyOnHand FROM WhseStk WHERE WHStockLink =" & e.Cell.Value & " AND WHWhseID =" & e.Cell.Row.Cells("Warehouse").Value & "")

                End With
        ElseIf e.Cell.Column.Key = "LineTax" Then
            With e.Cell.Row
                '.Value = .Text
                If iDocType = DocType.SalesOrder Then
                    e.Cell.Row.Cells("ConfirmQty").Value = e.Cell.Row.Cells("Quantity").Value
                End If
                e.Cell.Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("OrderTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("LineTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                e.Cell.Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
            End With

        ElseIf e.Cell.Column.Key = "Quantity" Or e.Cell.Column.Key = "Price_Excl" Or e.Cell.Column.Key = "Price_Incl" Or _
        e.Cell.Column.Key = "TaxRate" Or e.Cell.Column.Key = "Discount" Then
            With e.Cell.Row
                If iDocType = DocType.SalesOrder Then
                    .Cells("ConfirmQty").Value = .Cells("Quantity").Value
                End If
                .Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                .Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                .Cells("OrderTax").Value = LineTax(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                .Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                .Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                .Cells("LineTax").Value = LineTax(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                .Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                .Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
            End With

        End If

            End If
    End Sub

    Private Function Get_Available_Qty(ByVal SQL As String) As Double
        Try
            Dim oQty As Object
            Con1.ConnectionString = sConStr
            CMD = New SqlCommand(SQL, Con1)
            CMD.CommandType = CommandType.Text
            Con1.Open()
            oQty = CMD.ExecuteScalar
            Con1.Close()
            Return CDbl(oQty)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function Get_UOM(ByVal SQL As String) As Double

        sSQL = " SELECT DISTINCT _etblUnits.idUnits, _etblUnits.cUnitCode, _etblUnits.cUnitDescription " & _
                " FROM         _etblUnits INNER JOIN " & _
                " StkItem ON _etblUnits.idUnits = StkItem.iUOMDefSellUnitID CROSS JOIN " & _
                "  _etblUnitConversion   WHERE StkItem.StockLink = " & UG.ActiveRow.Cells("StockID").Value & "  "
        Con1.ConnectionString = sConStr
        CMD = New SqlCommand(sSQL, Con1)
        DS = New DataSet
        DA = New SqlDataAdapter(CMD)

        Con1.Open()
        DA.Fill(DS)
        Con1.Close()

        sSQL = " SELECT iUnitAID, fUnitAQty, iUnitBID, fUnitBQty, fMarkup FROM _etblUnitConversion " & _
                    " WHERE (iUnitAID = " & DDUnit.ActiveRow.Cells("idUnits").Value & " AND iUnitBID = " & UG.ActiveRow.Cells("iUnit").Value & ") OR " & _
                    " (iUnitAID = " & UG.ActiveRow.Cells("iUnit").Value & " AND iUnitBID = " & DDUnit.ActiveRow.Cells("idUnits").Value & ") "

        Try
            Con1.ConnectionString = sConStr
            CMD = New SqlCommand(sSQL, Con1)
            DS = New DataSet
            DA = New SqlDataAdapter(CMD)

            Con1.Open()
            DA.Fill(DS)
            Con1.Close()

            Dim UEPrice As Double
            Dim UIPrice As Double

            If DS.Tables(0).Rows.Count > 0 Then
                If UG.ActiveRow.Cells("iUnit").Value = DS.Tables(0).Rows(0)(0) Then
                    UEPrice = (DS.Tables(0).Rows(0)(1) / DS.Tables(0).Rows(0)(3) * OEPrice) + (OEPrice * DS.Tables(0).Rows(0)(4) / 100)
                    UIPrice = (DS.Tables(0).Rows(0)(1) / DS.Tables(0).Rows(0)(3) * OIPrice) + (OIPrice * DS.Tables(0).Rows(0)(4) / 100)
                ElseIf UG.ActiveRow.Cells("iUnit").Value = DS.Tables(0).Rows(0)(2) Then
                    UEPrice = (DS.Tables(0).Rows(0)(3) / DS.Tables(0).Rows(0)(1) * OEPrice) + (OEPrice * DS.Tables(0).Rows(0)(4) / 100)
                    UIPrice = (DS.Tables(0).Rows(0)(3) / DS.Tables(0).Rows(0)(1) * OIPrice) + (OIPrice * DS.Tables(0).Rows(0)(4) / 100)
                End If
                'UG.ActiveRow.Cells("Price_Excl").Value = DS.Tables(0).Rows(0)(1) / DS.Tables(0).Rows(0)(0) * UG.ActiveRow.Cells("Price_Excl").Value
                With UG.ActiveCell
                    .Value = .Text
                    .Row.Cells("Price_Excl").Value = UEPrice
                    .Row.Cells("Price_Incl").Value = UIPrice
                    .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, UEPrice, UIPrice, .Row.Cells("TaxRate").Value)
                    .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, UEPrice, UIPrice, .Row.Cells("TaxRate").Value)
                    .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, UEPrice, UIPrice, .Row.Cells("TaxRate").Value)
                    .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, UEPrice, UIPrice, .Row.Cells("TaxRate").Value)
                    .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, UEPrice, UIPrice, .Row.Cells("TaxRate").Value)
                    .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, UEPrice, UIPrice, .Row.Cells("TaxRate").Value)
                    .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, UEPrice, UIPrice, .Row.Cells("TaxRate").Value)
                    .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, UEPrice, UIPrice, .Row.Cells("TaxRate").Value)
                End With
            Else
                MessageBox.Show("No Conversion exists between these Units of Measure!", "Pastel Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)
                UG.ActiveRow.Cells("UOM").Value = DDUnit.ActiveRow.Cells("idUnits").Value
                Exit Function
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
            Exit Function
        Finally
            If Con1.State = ConnectionState.Open Then Con1.Close()
        End Try


        'UG.ActiveRow.Cells("StockID").Value = DDStock.ActiveRow.Cells("StockLink").Value


        'MsgBox("Quantity not available in Lot", MsgBoxStyle.Information, "Pastel Evolution AddOn")
        'UG.ActiveRow.Cells("Quantity").Value = DDLot.ActiveRow.Cells("Qty On Hand").Value
        ''End If
        'Lot = DDLot.ActiveRow.Cells("Description").Value
        'Lot_Exp = DDLot.ActiveRow.Cells("Expiry Date").Value

    End Function
    

    Private Sub UG_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UG.CellChange
        Try
            If UG.ActiveCell.Column.Index = 1 Then  'Code
                If DDStock.ActiveRow.Activated = True Then
                    UG.ActiveRow.Cells("StockID").Value = DDStock.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Code").Value = DDStock.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Description_1").Value = DDStock.ActiveRow.Cells("Description_1").Value
                    UG.ActiveRow.Cells("Description_2").Value = DDStock.ActiveRow.Cells("Description_2").Value
                    UG.ActiveRow.Cells("iUnit").Value = DDStock.ActiveRow.Cells("iUOMStockingUnitID").Value
                    UG.ActiveRow.Cells("ItemGroup").Value = DDStock.ActiveRow.Cells("ItemGroup").Value
                    UG.ActiveRow.Cells("cValue").Value = DDStock.ActiveRow.Cells("cValue").Value
                    UG.ActiveRow.Cells("cBin").Value = DDStock.ActiveRow.Cells("Bin").Value

                    If DDStock.ActiveRow.Cells("iItemCostingMethod").Value = 0 Then
                        UG.ActiveRow.Cells("CostPrice").Value = Math.Round(DDStock.ActiveRow.Cells("AveUCst").Value, 2, MidpointRounding.AwayFromZero)
                    ElseIf DDStock.ActiveRow.Cells("iItemCostingMethod").Value = 1 Then
                        UG.ActiveRow.Cells("CostPrice").Value = Math.Round(DDStock.ActiveRow.Cells("LatUCst").Value, 2, MidpointRounding.AwayFromZero)
                    Else
                    End If
                    UG.ActiveRow.Cells("Line").Value = UG.ActiveRow.Index + 1
                    UG.ActiveRow.Cells("iUnitCate").Value = DDStock.ActiveRow.Cells("iUnitCategoryID").Value
                    UG.ActiveRow.Cells("IsSerialNo").Value = IIf(DDStock.ActiveRow.Cells("SerialItem").Value = True, True, False)
                    UG.ActiveRow.Cells("IsWH").Value = IIf(DDStock.ActiveRow.Cells("WhseItem").Value = True, True, False)
                    UG.ActiveRow.Cells("fCostMargine").Value = DDStock.ActiveRow.Cells("fCostMargine").Value

                    If UG.ActiveRow.Cells("IsWH").Value = True And sSQLSrvDataBase = "dbUdawatta_new" Then
                        UG.ActiveRow.Cells("Warehouse").Activation = Activation.AllowEdit
                    Else
                        UG.ActiveRow.Cells("Warehouse").Activation = Activation.Disabled
                    End If


                End If
            ElseIf UG.ActiveCell.Column.Index = 2 Then  'Description_1
                If DDDescription.ActiveRow.Activated = True Then
                    UG.ActiveRow.Cells("Quantity").Value = 0
                    UG.ActiveRow.Cells("StockID").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Code").Value = DDDescription.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Description_1").Value = DDDescription.ActiveRow.Cells("Description_1").Value
                    UG.ActiveRow.Cells("Description_2").Value = DDDescription.ActiveRow.Cells("Description_2").Value
                    UG.ActiveRow.Cells("iUnit").Value = DDDescription.ActiveRow.Cells("iUOMStockingUnitID").Value
                    UG.ActiveRow.Cells("ItemGroup").Value = DDDescription.ActiveRow.Cells("ItemGroup").Value
                    UG.ActiveRow.Cells("cValue").Value = DDDescription.ActiveRow.Cells("cValue").Value
                    UG.ActiveRow.Cells("cBin").Value = DDDescription.ActiveRow.Cells("Bin").Value

                    'Commented by Husny coz error
                    'If DDDescription.ActiveRow.Cells("iItemCostingMethod").Value = 0 Then
                    '    UG.ActiveRow.Cells("CostPrice").Value = Math.Round(DDDescription.ActiveRow.Cells("AveUCst").Value, 2, MidpointRounding.AwayFromZero)
                    'ElseIf DDDescription.ActiveRow.Cells("iItemCostingMethod").Value = 1 Then
                    '    UG.ActiveRow.Cells("CostPrice").Value = Math.Round(DDDescription.ActiveRow.Cells("LatUCst").Value, 2, MidpointRounding.AwayFromZero)
                    'Else
                    'End If

                    If DDDescription.ActiveRow.Cells("iUOMStockingUnitID").Value = 0 Then
                        UG.ActiveRow.Cells("UOM").Value = 0
                        UG.ActiveRow.Cells("UOM").Activated = False
                        UG.ActiveRow.Cells("UOM").Appearance.BackColorDisabled = Color.Gray
                        'Else
                        '    UG.ActiveRow.Cells("UOM").Activated = True
                        '    UG.ActiveRow.Cells("UOM").Appearance.BackColorDisabled = Color.White
                    End If

                    'UG.ActiveRow.Cells("iUnit").Value = DDDescription.ActiveRow.Cells("iUOMStockingUnitID").Value
                    'UG.ActiveRow.Cells("IsSerialNo").Value = IIf(DDDescription.ActiveRow.Cells("SerialItem").Value = True, True, False)
                    UG.ActiveRow.Cells("IsWH").Value = IIf(DDDescription.ActiveRow.Cells("WhseItem").Value = True, True, False)
                    '-----------important-----------------------------------------------------------------------------------
                    'UG.ActiveRow.Cells("fCostMargine").Value = DDDescription.ActiveRow.Cells("fCostMargine").Value         |
                    '-------------------------------------------------------------------------------------------------------
                    If UG.ActiveRow.Cells("IsWH").Value = True And sSQLSrvDataBase = "dbUdawatta_new" Then
                        UG.ActiveRow.Cells("Warehouse").Activation = Activation.AllowEdit
                    Else
                        UG.ActiveRow.Cells("Warehouse").Activation = Activation.Disabled
                    End If
                    UG.ActiveRow.Cells("Line").Value = UG.ActiveRow.Index + 1

                    UG.ActiveRow.Cells("lot").Value = 0

                End If
            ElseIf UG.ActiveCell.Column.Index = 11 Then 'TaxType
                If DDTaxType.ActiveRow.Activated = True Then
                    UG.ActiveRow.Cells("TaxType").Value = DDTaxType.ActiveRow.Cells("Code").Value
                    UG.ActiveRow.Cells("TaxRate").Value = DDTaxType.ActiveRow.Cells("TaxRate").Value
                End If
            ElseIf UG.ActiveCell.Column.Index = 5 Then  'WareHouse
                If DDWH.ActiveRow.Activated = True Then
                    UG.ActiveRow.Cells("Warehouse").Value = DDWH.ActiveRow.Cells("WhseLink").Value
                    UG.ActiveRow.Cells("AvailableQty").Value = Get_Available_Qty("SELECT WHQtyOnHand FROM WhseStk WHERE WHStockLink =" & e.Cell.Row.Cells("Code").Value & " AND WHWhseID =" & e.Cell.Row.Cells("Warehouse").Value & "")
                    UG.ActiveRow.Cells("Lot").Value = ""
                End If


            ElseIf UG.ActiveCell.Column.Index = 36 Then  'Lot
                If DDLot.ActiveRow.Activated = True Then
                    UG.ActiveRow.Cells("Lot").Value = DDLot.ActiveRow.Cells("idLotTracking").Value
                    'UG.ActiveRow.Cells("AvailableQty").Value = Get_Available_Qty("SELECT WHQtyOnHand FROM WhseStk WHERE WHStockLink =" & e.Cell.Row.Cells("Code").Value & " AND WHWhseID =" & e.Cell.Row.Cells("Warehouse").Value & "")
                End If
                If UG.ActiveRow.Cells("Lot").Text.Substring(0, 3) = "AIR" Then
                    UG.ActiveRow.Cells("Discount").Value = 0
                End If
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Private Sub UG_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles UG.KeyDown
        Try
            ' perform action needed to move cursor
            'Select Case e.KeyValue

            'Case Keys.Up

            '    UG.PerformAction(ExitEditMode, False, False)
            '    UG.PerformAction(AboveCell, False, False)
            '    e.Handled = True
            '    UG.PerformAction(EnterEditMode, False, False)

            'Case Keys.Down

            '    UG.PerformAction(ExitEditMode, False, False)
            '    UG.PerformAction(BelowCell, False, False)
            '    e.Handled = True
            '    UG.PerformAction(EnterEditMode, False, False)

            'Case Keys.Right

            '    UG.PerformAction(ExitEditMode, False, False)
            '    UG.PerformAction(NextCellByTab, False, False)
            '    e.Handled = True
            '    UG.PerformAction(EnterEditMode, False, False)

            'Case Keys.Left

            '    UG.PerformAction(ExitEditMode, False, False)
            '    UG.PerformAction(PrevCellByTab, False, False)
            '    e.Handled = True
            '    UG.PerformAction(EnterEditMode, False, False)

            'End Select
            'If (e.KeyCode = Keys.Down) Then
            '    Go down one row
            '    UG.Rows(UG.ActiveRow.Index + 1).Cells(UG.ActiveCell.Column.Index - 1).Activate()

            '    UG.PerformAction(UltraGridAction.BelowCell)
            'End If


            If iDocType = DocType.SalesOrder Then
                If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then

                    Select Case (e.KeyCode)

                        Case Keys.Up
                            UG.PerformAction(UltraGridAction.ExitEditMode, False, False)
                            UG.PerformAction(UltraGridAction.AboveRow, False, False)
                            e.Handled = True
                            UG.ActiveRow.Cells("Code").Activate()


                            UG.PerformAction(UltraGridAction.BelowCell, False, False)
                            UG.PerformAction(UltraGridAction.EnterEditMode, False, False)
                            Exit Select
                        Case Keys.Down
                            If UG.ActiveCell.Column.Key <> "Lot" Then
                                UG.PerformAction(UltraGridAction.ExitEditMode, False, False)
                                UG.PerformAction(UltraGridAction.BelowRow, False, False)
                                e.Handled = True
                                'UG.PerformAction(UltraGridAction.EnterEditMode, False, False)
                                UG.ActiveRow.Cells("Code").Activate()
                                'UG.ActiveRow.Cells("Code").Value = ""
                                'UG.ActiveRow.Cells("Description_1").
                                UG.PerformAction(UltraGridAction.BelowCell, False, False)
                                UG.PerformAction(UltraGridAction.EnterEditMode, False, False)

                                'nwe line code---------------------------------------------------------------------
                                'Dim ugR As UltraGridRow
                                'If UG.DisplayLayout.Rows(UG.ActiveRow.Index + 1) = Nothing Then

                                'End If
                                'ugR = UG.DisplayLayout.Bands(0).AddNew
                                'ugR.Cells("Line").Value = ugR.Index + 1
                                'ugR.Cells("Line").Selected = True
                                ''ugR.Cells("islot").Value = 0
                                'ugR.Cells("Warehouse").Value = iWH
                                'If iDocType = DocType.SalesOrder Then
                                '    UG.DisplayLayout.Bands(0).Columns("Warehouse").CellActivation = Activation.Disabled
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
                                '---------------------------------------------------------------------------------
                            End If
                            Exit Select
                        Case Keys.Right
                            UG.PerformAction(UltraGridAction.ExitEditMode, False, False)
                            UG.PerformAction(UltraGridAction.NextCellByTab, False, False)
                            e.Handled = True
                            UG.PerformAction(UltraGridAction.EnterEditMode, False, False)
                            Exit Select
                        Case Keys.Left
                            UG.PerformAction(UltraGridAction.ExitEditMode, False, False)
                            UG.PerformAction(UltraGridAction.PrevCellByTab, False, False)
                            e.Handled = True
                            UG.PerformAction(UltraGridAction.EnterEditMode, False, False)
                            Exit Select
                    End Select


                End If
            End If

            If iDocType = DocType.PurchaseOrder Then
                If UG.ActiveCell.Column.Key = "SerialLot" Then
                    If e.KeyCode = Keys.Enter Then
                        With frmCreateLot
                            .Label3.Text = UG.ActiveRow.Cells("StockID").Value.ToString
                        End With
                        frmCreateLot.txtLot.Text = txtDelNote_SupInvNo.Text + "-" + UG.ActiveRow.Cells("Description_1").Value
                        frmCreateLot.ShowDialog()


                        If LotCN = True Then
                            UG.ActiveRow.Cells("SerialLot").Value = frmCreateLot.txtLot.Text
                        Else
                            UG.ActiveRow.Cells("SerialLot").Value = frmCreateLot.cmbLot.Text
                            'e.Cell.Row.Cells("SerialLot"). = frmCreateLot.cmbLot.Text
                        End If

                        UG.ActiveRow.Cells("Lot_Exp").Value = frmCreateLot.dtpExpDate.Value


                    End If
                End If
            End If








            If e.KeyCode = Keys.Tab Then
                Dim Col As String = ""
                If UG.DisplayLayout.Bands(0).Columns("Discount").CellActivation = Activation.Disabled Then
                    'Col = "Quantity"
                    Col = "Price_Incl"
                Else
                    'Col = "Discount"
                    Col = "Price_Incl"

                End If
                If UG.ActiveCell.Column.Key = Col And iDocType = DocumentType.SalesOrder Then
                    If UG.ActiveRow.Cells("Code").Value <> 0 Then
                        If UG.ActiveRow.HasNextSibling = True Then Exit Sub
                        Dim ugR As UltraGridRow
                        ugR = UG.DisplayLayout.Bands(0).AddNew
                        ugR.Cells("Line").Value = ugR.Index + 1
                        ugR.Cells("Line").Selected = True


                        'Dim agent = New Agent(iAgent)

                        'If agent.Description = "4" Then
                        '    ugR.Cells("Warehouse").Value = 4
                        'Else
                        ugR.Cells("Warehouse").Value = iWH
                        'End If


                        ugR.Cells(1).Activate()
                        If iDocType = DocType.SalesOrder Then
                            ugR.Cells("TaxType").Value = 1
                            For Each ugR1 As UltraGridRow In DDTaxType.Rows
                                If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
                                    ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
                                End If
                            Next
                        ElseIf iDocType = DocType.PurchaseOrder Then
                            ugR.Cells("TaxType").Value = 3
                            For Each ugR1 As UltraGridRow In DDTaxType.Rows
                                If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
                                    ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
                                End If
                            Next
                        End If
                    Else
                        MsgBox("Item Code can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                        Exit Sub
                    End If
                ElseIf UG.ActiveCell.Column.Key = "Discount" And iDocType = DocumentType.PurchaseOrder Then
                    If UG.ActiveRow.Cells("Code").Value <> 0 Then
                        If UG.ActiveRow.HasNextSibling = True Then Exit Sub
                        Dim ugR As UltraGridRow
                        ugR = UG.DisplayLayout.Bands(0).AddNew
                        ugR.Cells("Line").Value = ugR.Index + 1
                        'ugR.Cells("Line").Selected = True


                        'Dim agent = New Agent(iAgent)

                        'If agent.Description = "4" Then
                        '    ugR.Cells("Warehouse").Value = 4
                        'Else
                        ugR.Cells("Warehouse").Value = iWH
                        'End If


                        'ugR.Cells("Code").Activate()

                        If iDocType = DocType.SalesOrder Then
                            ugR.Cells("TaxType").Value = 1
                            For Each ugR1 As UltraGridRow In DDTaxType.Rows
                                If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
                                    ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
                                End If
                            Next
                        ElseIf iDocType = DocType.PurchaseOrder Then
                            ugR.Cells("TaxType").Value = 3
                            For Each ugR1 As UltraGridRow In DDTaxType.Rows
                                If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
                                    ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
                                End If
                            Next
                        End If

                        ugR.Cells("Description_1").Activate()
                        ugR.Cells("Description_1").Value = ""
                    Else
                        MsgBox("Item Code can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                        Exit Sub
                    End If
                End If
            ElseIf e.KeyData = Keys.F9 Then
                If iDocType = DocType.SalesOrder Then
                    tsbSaveSO_Click(tsbSaveSO, e)
                End If
            End If
        Catch ex As Exception
            Exit Sub

        End Try
    End Sub
    Public Sub GET_NEXT_GRV_NO()
        SQL = "SELECT GrvPref, GrvNum, GRVPad FROM StDfTbl"
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                If DS.Tables(0).Rows.Count = 0 Then
                    sGrvNo = "DNU"
                Else
                    For Each Dr In DS.Tables(0).Rows
                        Select Case Dr("GRVPad")
                            Case 1
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "0")
                            Case 2
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "00")
                            Case 3
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "000")
                            Case 4
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "0000")
                            Case 5
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "00000")
                            Case 6
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "000000")
                            Case 7
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "0000000")
                            Case 8
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "00000000")
                            Case 9
                                sGrvNo = Dr("GrvPref") & Format(CInt(Dr("GrvNum")), "000000000")
                        End Select
                    Next
                End If
                txtInNo.Text = sGrvNo
            Catch ex As Exception
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With
    End Sub
    Public Sub GET_NEXT_INVOICE_NO()
        SQL = "SELECT InvNum, InvPref, InvPad FROM StDfTbl"
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                If DS.Tables(0).Rows.Count = 0 Then
                    sInvoiceNo = "DNU"
                Else
                    For Each Dr In DS.Tables(0).Rows
                        Select Case Dr("InvPad")
                            Case 1
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "0")
                            Case 2
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "00")
                            Case 3
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "000")
                            Case 4
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "0000")
                            Case 5
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "00000")
                            Case 6
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "000000")
                            Case 7
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "0000000")
                            Case 8
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "00000000")
                            Case 9
                                sInvoiceNo = Dr("InvPref") & Format(CInt(Dr("InvNum")), "000000000")
                        End Select
                    Next
                End If
                txtInNo.Text = sInvoiceNo
            Catch ex As Exception
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With
    End Sub
    Public Sub GET_NEXT_DELIVERY_NO()
        SQL = "SELECT  NextDNNo,DN_POPrefix, DNoPadLgth FROM OrdersDf WHERE DefaultCounter = 1"
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                If DS.Tables(0).Rows.Count = 0 Then
                    sDeliveryNo = "DNU"
                Else
                    For Each Dr In DS.Tables(0).Rows
                        Select Case Dr("DNoPadLgth")
                            Case 1
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "0")
                            Case 2
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "00")
                            Case 3
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "000")
                            Case 4
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "0000")
                            Case 5
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "00000")
                            Case 6
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "000000")
                            Case 7
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "0000000")
                            Case 8
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "00000000")
                            Case 9
                                sDeliveryNo = Dr("DN_POPrefix") & Format(Dr("NextDNNo"), "000000000")
                        End Select
                    Next
                End If
                txtDelNote_SupInvNo.Text = sDeliveryNo
            Catch ex As Exception
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With
    End Sub
    Public Sub GET_NEXT_PO_NO()
        SQL = "SELECT     OrderPrefix, NextCustNo, CNoPadLgth FROM OrdersDf WHERE DefaultCounter = 2"
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                If DS.Tables(0).Rows.Count = 0 Then
                    sPONo = "DNU"   'Default Not Updated
                Else
                    For Each Dr In DS.Tables(0).Rows
                        Select Case Dr("CNoPadLgth")
                            Case 1
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "0")
                            Case 2
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "00")
                            Case 3
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "000")
                            Case 4
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "0000")
                            Case 5
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "00000")
                            Case 6
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "000000")
                            Case 7
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "0000000")
                            Case 8
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "00000000")
                            Case 9
                                sPONo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "000000000")
                        End Select
                    Next
                End If
                txtOrdNo.Text = sPONo
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With

    End Sub
    Public Sub GET_NEXT_ORDER_NO()
        SQL = "SELECT  NextCustNo,OrderPrefix, CNoPadLgth FROM OrdersDf WHERE DefaultCounter = 1"
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                If DS.Tables(0).Rows.Count = 0 Then
                    sOrderNo = "DNU"   'Default Not Updated
                Else
                    For Each Dr In DS.Tables(0).Rows
                        Select Case Dr("CNoPadLgth")
                            Case 1
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "0")
                            Case 2
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "00")
                            Case 3
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "000")
                            Case 4
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "0000")
                            Case 5
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "00000")
                            Case 6
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "000000")
                            Case 7
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "0000000")
                            Case 8
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "00000000")
                            Case 9
                                sOrderNo = Dr("OrderPrefix") & Format(Dr("NextCustNo"), "000000000")
                        End Select
                    Next
                End If
                txtOrdNo.Text = sOrderNo
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With

    End Sub
    Public Sub GET_NEXT_QUO_NO()
        SQL = "SELECT NextQuoteNo, QuotePrefix, PadQuoteLngth, DefaultCounter FROM OrdersDf WHERE DefaultCounter = 1"
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                If DS.Tables(0).Rows.Count = 0 Then
                    sQuoteNo = "DNU"   'Default Not Updated
                Else
                    For Each Dr In DS.Tables(0).Rows
                        Select Case Dr("PadQuoteLngth")
                            Case 1
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "0")
                            Case 2
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "00")
                            Case 3
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "000")
                            Case 4
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "0000")
                            Case 5
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "00000")
                            Case 6
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "000000")
                            Case 7
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "0000000")
                            Case 8
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "00000000")
                            Case 9
                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "000000000")
                        End Select
                    Next
                End If
            Catch ex As Exception
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try

        End With
    End Sub
    Public Sub Discard()

        Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & sSQLSrvDataBase & "")
        SQL = "Select GetDate()"
        CMD = New SqlCommand(SQL, Con2)
        CMD.CommandType = CommandType.Text
        DA = New SqlDataAdapter(CMD)
        DS = New DataSet
        Con2.Open()
        DA.Fill(DS)
        Con2.Close()



        cmbCustomer.Text = Nothing
        cmbCustomer.Enabled = True
        cmbOrderStatus.Text = Nothing
        cmbPriority.Text = Nothing
        cmbJrRep.Text = Nothing
        'cmbProject.Text = Nothing
        cmbSalesOpp.Text = Nothing
        txtExtOrder.Text = Nothing
        txtDelNote_SupInvNo.Text = Nothing
        txtOrdNo.Text = Nothing
        txtInNo.Text = Nothing
        cmbType.ResetText()
        dtpDeliDate.Value = Format(DS.Tables(0).Rows(0)(0), "dd/MM/yyyy")
        dtpDueDate.Value = Format(DS.Tables(0).Rows(0)(0), "dd/MM/yyyy")
        'dtpInDate.Value = Format(Date.Now, "dd/MM/yyyy")
        dtpInDate.Value = Format(DS.Tables(0).Rows(0)(0), "dd/MM/yyyy")
        dtpOrdDate.Value = Format(DS.Tables(0).Rows(0)(0), "dd/MM/yyyy")
        txtDAdd1.ResetText()
        txtDAdd2.ResetText()
        txtDAdd3.ResetText()
        txtDAdd4.ResetText()
        txtDAdd5.ResetText()
        txtPAdd1.ResetText()
        txtPAdd2.ResetText()
        txtPAdd3.ResetText()
        txtPAdd4.ResetText()
        txtPAdd5.ResetText()
        IsNew = True
        iPONo = 0
        If iDocType = DocType.SalesOrder Then
            GET_NEXT_DELIVERY_NO()
            GET_NEXT_INVOICE_NO()
            GET_NEXT_ORDER_NO()
            GET_DATA()
        ElseIf iDocType = DocType.PurchaseOrder Then
            GET_NEXT_GRV_NO()
            GET_NEXT_PO_NO()
            GET_DATA()
        End If
        DeleteRows()
        Get_Total()
    End Sub
    Private Sub tsbPlaceOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSOPlaceOrder.Click
        If cmbCustomer.Text = "" Then
            MsgBox("Customer Name can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If
        If UG.Rows.Count = 0 Then
            MsgBox("Enter Item Details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                .Begin_Trans()

                .Commit_Trans()
                Discard()
            Catch ex As Exception
                .Rollback_Trans()
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Pastel Evolution")
                Exit Sub
            Finally
                .Con_Close()
                objSQL = Nothing
            End Try
        End With
        GET_NEXT_ORDER_NO()

    End Sub
    Public Sub Delete_Row()
        Dim R As UltraGridRow
        For Each R In UG.Rows.All
            If R.Cells("Line").Value = 0 Or R.Cells("StockID").Value = 0 Then
                R.Delete(False)
                Exit Sub
            End If
        Next
    End Sub
    Private Sub UG_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles UG.Leave
        Delete_Row()
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Me.Close()
    End Sub

    Private Sub cmbCustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbCustomer.KeyDown
        If e.KeyData = Keys.F9 Then
            If iDocType = DocType.SalesOrder Then
                tsbSaveSO_Click(tsbSaveSO, e)
            End If
        End If
    End Sub

    Private Sub cmbCustomer_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCustomer.Leave
        'get_ConsolidatedCredit()
    End Sub
    Public Sub get_ConsolidatedCredit()
        'Dim sDB As String
        'Dim Obj As New Object
        'For Each Obj In aDataBase
        '    sDB = Obj.ToString
        '    'sDB = "dbUdawatta"
        '    Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & sDB & "")

        '    SQL = " SELECT Client.Account, Client.Credit_Limit, SUM(PostAR.Outstanding) " & _
        '    " FROM Client INNER JOIN " & _
        '    " PostAR ON Client.DCLink = PostAR.AccountLink " & _
        '    " WHERE DCLink ='" & cmbCustomer.Value & "' GROUP BY Client.Account, Client.Credit_Limit"
        '    CMD = New SqlCommand(SQL, Con2)
        '    CMD.CommandType = CommandType.Text
        '    DA = New SqlDataAdapter(CMD)
        '    DS = New DataSet
        '    Con2.Open()
        '    DA.Fill(DS)
        '    Con2.Close()

        '    If DS.Tables(0).Rows.Count > 0 Then
        '        conCLimit = conCLimit + DS.Tables(0).Rows(0)(2)
        '    End If

        'Next

    End Sub
    Private Sub cmbCustomer_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCustomer.ValueChanged
        Try
            txtDAdd1.ResetText()
            txtDAdd2.ResetText()
            txtDAdd3.ResetText()
            txtDAdd4.ResetText()
            txtDAdd5.ResetText()
            txtPAdd1.ResetText()
            txtPAdd2.ResetText()
            txtPAdd3.ResetText()
            txtPAdd4.ResetText()
            txtPAdd5.ResetText()
            If cmbCustomer.ActiveRow.Activated = True Then
                txtDAdd1.Text = cmbCustomer.ActiveRow.Cells("Post1").Value
                txtDAdd2.Text = cmbCustomer.ActiveRow.Cells("Post2").Value
                txtDAdd3.Text = cmbCustomer.ActiveRow.Cells("Post3").Value

                txtPAdd1.Text = cmbCustomer.ActiveRow.Cells("Physical1").Value
                txtPAdd2.Text = cmbCustomer.ActiveRow.Cells("Physical2").Value
                txtPAdd3.Text = cmbCustomer.ActiveRow.Cells("Physical3").Value
                If iDocType = DocType.SalesOrder Then
                    iCusPriceList = cmbCustomer.ActiveRow.Cells("iARPriceListNameID").Value
                    iCusGroup = cmbCustomer.ActiveRow.Cells("iClassID").Value
                    lblCreditLimit.Text = cmbCustomer.ActiveRow.Cells("Credit_Limit").Value
                    lblRemainingAmt.Text = cmbCustomer.ActiveRow.Cells("Credit_Limit").Value - cmbCustomer.ActiveRow.Cells("DCBalance").Value
                    lblCOD.Text = IIf(cmbCustomer.ActiveRow.Cells("bCODAccount").Value = True, "Cash on Delivery", "")
                End If
            End If

            If iDocType = DocType.SalesOrder Then
                'If iCusPriceList = 2 Or iCusGroup <> 1 And iCusGroup <> 6 Then
                UG.DisplayLayout.Bands(0).Columns("Discount").CellActivation = Activation.Disabled
                UG.DisplayLayout.Bands(0).Columns("DiscountA").CellActivation = Activation.Disabled
                'Else
                '    UG.DisplayLayout.Bands(0).Columns("Discount").CellActivation = Activation.AllowEdit
                '    UG.DisplayLayout.Bands(0).Columns("DiscountA").CellActivation = Activation.AllowEdit
                'End If
            End If
            Dim ugR As UltraGridRow
            If UG.Rows.Count = 0 Then
                ugR = UG.DisplayLayout.Bands(0).AddNew
                ugR.Cells("Line").Value = ugR.Index + 1

                'Dim agent = New Agent(iAgent)

                'If agent.Description = "4" Then
                '    ugR.Cells("Warehouse").Value = 4
                'Else
                ugR.Cells("Warehouse").Value = iWH
                'End If

                If iDocType = DocType.SalesOrder Then
                    'If iCusPriceList = 2 Then
                    '    UG.DisplayLayout.Bands(0).Columns("Discount").CellActivation = Activation.Disabled
                    '    UG.DisplayLayout.Bands(0).Columns("DiscountA").CellActivation = Activation.Disabled
                    'Else
                    '    UG.DisplayLayout.Bands(0).Columns("Discount").CellActivation = Activation.AllowEdit
                    '    UG.DisplayLayout.Bands(0).Columns("DiscountA").CellActivation = Activation.AllowEdit
                    'End If

                    If sSQLSrvDataBase = "dbUdawatta_new" Then
                        UG.DisplayLayout.Bands(0).Columns("Warehouse").CellActivation = Activation.AllowEdit
                    Else
                        UG.DisplayLayout.Bands(0).Columns("Warehouse").CellActivation = Activation.Disabled
                    End If
                    ugR.Cells("TaxType").Value = 1
                    For Each ugR1 As UltraGridRow In DDTaxType.Rows
                        If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
                            ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value

                        End If
                    Next
                ElseIf iDocType = DocType.PurchaseOrder Then
                    ugR.Cells("TaxType").Value = 3
                    For Each ugR1 As UltraGridRow In DDTaxType.Rows
                        If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
                            ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
                        End If
                    Next
                End If
            Else
                For Each ugR In UG.Rows.All
                    'ugR.Delete(False)
                    ugR.Cells("Quantity").Value = 0
                Next

                If UG.Rows.Count = 0 Then
                    ugR = UG.DisplayLayout.Bands(0).AddNew



                    ugR.Cells("Line").Value = ugR.Index + 1


                    'Dim agent = New Agent(iAgent)

                    'If agent.Description = "4" Then
                    '    ugR.Cells("Warehouse").Value = 4
                    'Else
                    ugR.Cells("Warehouse").Value = iWH
                    'End If


                    If iDocType = DocType.SalesOrder Then
                        ugR.Cells("TaxType").Value = 1


                        For Each ugR1 As UltraGridRow In DDTaxType.Rows
                            If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
                                ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
                            End If
                        Next
                    ElseIf iDocType = DocType.PurchaseOrder Then
                        ugR.Cells("TaxType").Value = 3
                        For Each ugR1 As UltraGridRow In DDTaxType.Rows
                            If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
                                ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
                            End If
                        Next
                    End If
                End If
            End If

        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
    Private Sub tsbQuote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSOQuote.Click
        ''frmPW.lblDueAmt.Text = txtConAmt.Value
        'frmPW.ShowDialog()

        Delete_Row()

        UG.PerformAction(UltraGridAction.CommitRow)

        If cmbCustomer.Value = Nothing Then
            MsgBox("Customer can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbCustomer.Text.Trim.Length = 0 Then
            MsgBox("Customer can not be left balnk", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        Dim ugR As UltraGridRow
        For Each ugR In UG.Rows
            If ugR.Cells("IsWH").Value = True Then
                If ugR.Cells("Warehouse").Value = Nothing Then
                    MsgBox("Warehouse can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    Exit Sub
                End If
            End If
            If ugR.Cells("Quantity").Value = 0 Then
                MsgBox("Quantity can not zero", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End If
            If ugR.Cells("ConfirmQty").Value = 0 Then
                MsgBox("Confirm Quantity can not be zero", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End If
        Next


        If cmbSaleRep.Text <> "" And cmbSaleRep.Value = Nothing Then
            cmbSaleRep.Value = CInt(cmbSaleRep.Text)
        End If

        If cmbSaleRep.Value = 0 Then
            MsgBox("Select Sales Person", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If UG.Rows.Count = 0 Then
            MsgBox("Please enter item details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)

        Try
            DatabaseContext.BeginTran()
            Dim SalesOrderQ As New SalesOrderQuotation
            Dim SalesOrderDetail As OrderDetail
            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse

            Dim Customer As New Customer(CInt(cmbCustomer.Value))
            Dim bIsCheckTerms As Boolean
            Dim dblAccountBalance As Double = 0
            Dim dblCreditLimit As Double = 0
            bIsCheckTerms = Customer.CheckTerms
            If bIsCheckTerms = True Then
                'dblAccountBalance = Customer.AccountBalance
                get_ConsolidatedCredit()
                dblAccountBalance = conCLimit
                dblCreditLimit = Customer.CreditLimit
                If dblAccountBalance + txtConAmt.Value > dblCreditLimit And dblCreditLimit > 0.0 Then
                    If bIsAllowOverrideCreditLimit = False Then
                        MsgBox("Account is Over Credit Limit", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    End If

                    Dim dd As Integer
                    Dim AccTrms As Integer
                    AccTrms = Customer.AccountTerms
                    Select Case AccTrms
                        Case 0
                            AccTrms = 0
                        Case 1
                            AccTrms = 30
                        Case 2
                            AccTrms = 60
                        Case 3
                            AccTrms = 90
                        Case 4
                            AccTrms = 120
                        Case 5
                            AccTrms = 150
                        Case 6
                            AccTrms = 180
                    End Select
                    'GLAccount.Find(Customer.AccountBalance)
                    'dd = DateDiff(DateInterval.Day, Now(), CDate("4/3/2011 6:00:00"))

                End If
            End If

            Dim Representative As New SalesRepresentative(CInt(cmbSaleRep.Value))
            SalesOrderQ.Account = Customer
            SalesOrderQ.InvoiceTo = Customer.PostalAddress
            SalesOrderQ.DeliverTo = Customer.PhysicalAddress
            SalesOrderQ.ExternalOrderNo = txtExtOrder.Text.ToString
            SalesOrderQ.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            SalesOrderQ.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            SalesOrderQ.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
            SalesOrderQ.Representative = Representative
            SalesOrderQ.DiscountPercent = txtDisPer.Value
            SalesOrderQ.Description = txtDescription.Text

            If bIsInclusive = True Then
                SalesOrderQ.TaxMode = TaxMode.Inclusive
            ElseIf bIsInclusive = False Then
                SalesOrderQ.TaxMode = TaxMode.Exclusive
            End If

            For Each ugR In UG.Rows
                SalesOrderDetail = New OrderDetail
                InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))

                SalesOrderDetail.InventoryItem = InventoryItem
                SalesOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                SalesOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)
                If bIsInclusive = True Then
                    SalesOrderDetail.TaxMode = TaxMode.Inclusive
                    SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                    SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                ElseIf bIsInclusive = False Then
                    SalesOrderDetail.TaxMode = TaxMode.Exclusive
                    SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                    SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                End If
                'If ugR.Cells("IsLot").Value = True Then
                '    If ugR.Cells("Lot").Value = Nothing Then
                '        MsgBox("Enter Lot Numbers" & vbCrLf & " Item = " & ugR.Cells("Code").Value, MsgBoxStyle.Exclamation, "Pastel Evolution")
                '        DatabaseContext.RollbackTran()
                '        If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                '        If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                '        Exit Sub
                '    End If
                '    'If ugR.ChildBands.HasChildRows = False Then
                '    '    MsgBox("Duplicate Serial No" & vbCrLf & "Item Code - " & ugR.Cells("Description_1").Value & vbCrLf & "Line No - " & ugR.Index + 1, MsgBoxStyle.Exclamation, "Pastel Evolution")
                '    '    'MsgBox("Enter Serial Numbers" & vbCrLf & "Item Code - " & ugR.Cells("Description_1").Value & vbCrLf & ugR.Index + 1, MsgBoxStyle.Exclamation, "Pastel Evolution")
                '    '    DatabaseContext.RollbackTran()
                '    '    If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                '    '    If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                '    '    Exit Sub
                '    'End If
                '    'Dim ugR2 As UltraGridRow
                '    'For Each ugR2 In ugR.ChildBands(0).Rows
                '    '    Dim sn As String = CStr(ugR2.Cells("SerialNumber").Value)
                '    '    SalesOrderDetail.SerialNumbers.Add(sn)
                '    'Next
                'End If

                If ugR.Cells("IsWH").Value = True Then
                    WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                    If ugR.Cells("IsWH").Value = True Then
                        SalesOrderDetail.Warehouse = WH
                    End If
                End If

                SalesOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                'SalesOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                SalesOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)
                'SalesOrderDetail.l = CStr(ugR.Cells("Lot").Text)

                Dim LotN As New Lot()
                LotN.Code = ugR.Cells("Lot").Text
                'LotN.ExpiryDate = ugR.Cells("Lot_Exp").Value
                SalesOrderDetail.Lot = LotN
                SalesOrderQ.Detail.Add(SalesOrderDetail)
            Next

            SalesOrderQ.Save()
            'SalesOrderQ.Process()

            Dim iSalesOrder As Integer = SalesOrderQ.ID
            MsgBox(SalesOrderQ.Reference, MsgBoxStyle.Information, "Pastel Evolution")
            DatabaseContext.CommitTran()
            If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                Dim objRep As New ReportDocument
                Dim s As String = Application.StartupPath
                If sSQLSrvDataBase = "dbNawinna_new" Then
                    objRep.Load(s & "\SalesOrderQN.rpt")
                Else
                    objRep.Load(s & "\SalesOrderQ.rpt")
                End If
                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSalesOrder & ""
                ApplyLoginToTable(objRep)

                Dim objCRV As New frmPrintPreview
                With objCRV
                    .CRV.ReportSource = objRep
                    .Text = "Sales Order Printing"
                    .Show()
                    .TopMost = True
                End With

                'If MsgBox("Do you want to preview the sales order", MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.No Then
                'Dim DefaultPrinter As String = DefaultPrinterName
                'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                'doctoprint.PrinterSettings.PrinterName = DefaultPrinter
                'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                '    Dim rawKind As Integer
                '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inv" Then
                '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                '        objRep.PrintOptions.PaperSize = rawKind
                '        objRep.PrintToPrinter(1, False, 0, 0)
                '        Exit For
                '    End If
                'Next
                'Else
                '    Dim objCRV As New frmPrintPreview
                '    With objCRV
                '        .CRV.ReportSource = objRep
                '        .Text = "Sales Order Printing"
                '        .Show()
                '        .TopMost = True
                '    End With
                'End If



            End If

            'frmPW.ref = SalesOrder.Reference
            'frmPW.cusID = cmbCustomer.Value
            'frmPW.repID = cmbSaleRep.Value
            'frmPW.lblDueAmt.Text = txtConAmt.Value
            'frmPW.ShowDialog()

            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
            Discard()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
            DatabaseContext.RollbackTran()
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        Finally
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        End Try

        '-----------------------------------------------------------------------------------------------------------------------------------------
        '    If cmbCustomer.Text = "" Then
        '        MsgBox("Customer Name can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
        '        Exit Sub
        '    End If
        '    If UG.Rows.Count = 0 Then
        '        MsgBox("Enter Item Details", MsgBoxStyle.Exclamation, "Pastel Evolution")
        '        Exit Sub
        '    End If

        '    Dim Dr As DataRow
        '    Dim objSQL As New clsSqlConn
        '    With objSQL
        '        Try
        '            .Begin_Trans()
        '            If IsNew = True Then
        '                SQL = "SELECT NextQuoteNo, QuotePrefix, PadQuoteLngth, DefaultCounter FROM OrdersDf WHERE DefaultCounter = 1"
        '                DS = New DataSet
        '                DS = .Get_Data_Sql_Trans(SQL)
        '                If DS.Tables(0).Rows.Count = 0 Then
        '                    MsgBox("Please check your Order Default Settings", MsgBoxStyle.Exclamation, "Pastel Evolution")    'Default Not Updated
        '                    .Rollback_Trans()
        '                    Exit Sub
        '                Else
        '                    For Each Dr In DS.Tables(0).Rows
        '                        Select Case Dr("PadQuoteLngth")
        '                            Case 1
        '                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "0")
        '                            Case 2
        '                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "00")
        '                            Case 3
        '                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "000")
        '                            Case 4
        '                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "0000")
        '                            Case 5
        '                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "00000")
        '                            Case 6
        '                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "000000")
        '                            Case 7
        '                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "0000000")
        '                            Case 8
        '                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "00000000")
        '                            Case 9
        '                                sQuoteNo = Dr("QuotePrefix") & Format(Dr("NextQuoteNo"), "000000000")
        '                        End Select
        '                    Next
        '                End If
        '                SQL = "UPDATE OrdersDf SET NextQuoteNo =NextQuoteNo+1  WHERE DefaultCounter = 1"
        '                If .Execute_Sql_Trans(SQL) = 0 Then
        '                    .Rollback_Trans()
        '                    Exit Sub
        '                End If

        '            End If


        '            SQL = "INSERT INTO InvNum(DocType, DocVersion, DocState, DocFlag, OrigDocID, " & _
        '            " GrvID,InvNumber, GrvNumber, AccountID, Description, InvDate, OrderDate, DueDate, " & _
        '            " DeliveryDate, TaxInclusive, Email_Sent, Address1, Address2, Address3, " & _
        '            " PAddress1, PAddress2, PAddress3,DelMethodID, DocRepID,OrderNum,DeliveryNote,  ProjectID," & _
        '            " TillID, ExtOrderNum, InvTotExclDEx, InvTotTaxDEx, InvTotInclDEx, InvTotExcl, InvTotTax, " & _
        '            " InvTotIncl,OrdTotExclDEx, OrdTotTaxDEx, OrdTotInclDEx, OrdTotExcl, OrdTotTax, OrdTotIncl, " & _
        '            " cAccountName, iOpportunityID, bInvRounding,InvTotInclExRounding, " & _
        '            " OrdTotInclExRounding,OrderStatusID,OrderPriorityID) VALUES (" & DocType.SalesOrder & ",'1'," & DocState.Quote & ",'0','0','0','" & sQuoteNo & "',''," & cmbCustomer.Value & "," & _
        '            " '" & txtDescription.Text.ToString.Replace("'", "") & "','" & Format(dtpInDate.Value, "MM/dd/yyyy") & "','" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "','" & Format(dtpDueDate.Value, "MM/dd/yyyy") & "'," & _
        '            " '" & Format(dtpDeliDate.Value, "MM/dd/yyyy") & "'," & IIf(cbInEx.Checked = True, 1, 0) & "," & _
        '            " '1','" & txtDAdd1.Text.ToString.Replace("'", "") & "','" & txtDAdd2.Text.ToString.Replace("'", "") & "','" & txtDAdd3.Text.ToString.Replace("'", "") & "','" & txtPAdd1.Text.ToString.Replace("'", "") & "','" & txtPAdd2.Text.ToString.Replace("'", "") & "','" & txtPAdd3.Text.ToString.Replace("'", "") & "'," & _
        '            " '0'," & cmbSaleRep.Value & ",'Quote',''," & IIf(cmbProject.Value = Nothing, 0, cmbProject.Value) & "," & _
        '            " '0','" & txtExtOrder.Text & "'," & txtConAmt.Value - txtConTax.Value & "," & _
        '            " " & txtConTax.Value & "," & txtConAmt.Value & "," & txtConAmt.Value - txtConTax.Value & "," & _
        '            " " & txtConTax.Value & "," & txtConAmt.Value & "," & txtOrdAmt.Value - txtOrdTax.Value & "," & _
        '            " " & txtOrdTax.Value & "," & txtOrdAmt.Value & "," & txtOrdAmt.Value - txtOrdTax.Value & "," & txtOrdTax.Value & "," & _
        '            " " & txtOrdAmt.Value & ",'" & cmbCustomer.Text & "'," & IIf(cmbSalesOpp.Value = Nothing, 0, cmbSalesOpp.Value) & ",'1'," & txtConAmt.Value & "," & txtOrdAmt.Value & "," & _
        '            " " & IIf(cmbOrderStatus.Value = Nothing, 0, cmbOrderStatus.Value) & "," & IIf(cmbPriority.Value = Nothing, 0, cmbPriority.Value) & " )"
        '            If .Execute_Sql_Trans(SQL) = 0 Then
        '                .Rollback_Trans()
        '                Exit Sub
        '            End If

        '            Dim iInvoiceID As Integer
        '            SQL = "SELECT MAX(AutoIndex) FROM InvNum"
        '            iInvoiceID = .Get_Max_No(SQL)
        '            If iInvoiceID = -1 Then
        '                .Rollback_Trans()
        '                Exit Sub
        '            End If

        '            Dim ugR As UltraGridRow
        '            For Each ugR In UG.Rows
        '                SQL = "INSERT INTO _btblInvoiceLines(iInvoiceID, iOrigLineID, iGrvLineID, cDescription, " & _
        '                " iUnitsOfMeasureStockingID, iUnitsOfMeasureCategoryID, iUnitsOfMeasureID, fQuantity, " & _
        '                " fQtyToProcess, fUnitPriceExcl, fUnitPriceIncl, fLineDiscount, fTaxRate, bIsWhseItem," & _
        '                " iStockCodeID, iWarehouseID, iTaxTypeID, iPriceListNameID, fQuantityLineTotIncl," & _
        '                " fQuantityLineTotExcl, fQuantityLineTotInclNoDisc, fQuantityLineTotExclNoDisc," & _
        '                " fQuantityLineTaxAmount, fQuantityLineTaxAmountNoDisc, fQtyToProcessLineTotIncl," & _
        '                " fQtyToProcessLineTotExcl, fQtyToProcessLineTotInclNoDisc, fQtyToProcessLineTotExclNoDisc," & _
        '                " fQtyToProcessLineTaxAmount, fQtyToProcessLineTaxAmountNoDisc, iLineRepID, iLineProjectID, " & _
        '                " bChargeCom,iLineID,fUnitCost,fQtyLastProcess,fQtyProcessed,fQtyReserved,cLineNotes,fAddCost,iJobID,fQtyChange," & _
        '                " fQtyChangeLineTotIncl,fQtyChangeLineTotExcl,fQtyChangeLineTotInclNoDisc," & _
        '                " fQtyChangeLineTotExclNoDisc,fQtyChangeLineTaxAmount,fQtyChangeLineTaxAmountNoDisc, bIsLotItem, iLotID, cLotNumber, dLotExpiryDate) " & _
        '                " VALUES  (" & iInvoiceID & ",'0','0','" & ugR.Cells("Description_1").Value & "', 0, 0, " & _
        '                " 0," & ugR.Cells("Quantity").Value & "," & ugR.Cells("ConfirmQty").Value & "," & _
        '                " " & ugR.Cells("Price_Excl").Value & "," & ugR.Cells("Price_Incl").Value & " ," & _
        '                " " & ugR.Cells("Discount").Value & "," & ugR.Cells("TaxRate").Value & " ," & _
        '                " " & IIf(ugR.Cells("Warehouse").Value = 0, 0, 1) & "," & ugR.Cells("Code").Value & "," & _
        '                " " & CInt(IIf(ugR.Cells("Warehouse").Value = Nothing, 0, ugR.Cells("Warehouse").Value)) & "," & IIf(IsDBNull(ugR.Cells("TaxType").Value) = True, 0, ugR.Cells("TaxType").Value) & ", " & _
        '                " 1," & ugR.Cells("OrderTotal_Incl").Value & "," & ugR.Cells("OrderTotal_Excl").Value & "," & _
        '                " " & ugR.Cells("OrderTotal_Incl").Value + ugR.Cells("Order_Dis_Incl").Value & "," & _
        '                " " & ugR.Cells("OrderTotal_Excl").Value + ugR.Cells("Order_Dis_Excl").Value & "," & _
        '                " " & ugR.Cells("OrderTax").Value & "," & ugR.Cells("Ord_Tax_NoDis_Excl").Value & "," & _
        '                " " & ugR.Cells("LineTotal_Incl").Value & "," & ugR.Cells("LineTotal_Excl").Value & "," & _
        '                " " & ugR.Cells("LineTotal_Incl").Value + ugR.Cells("Line_Dis_Incl").Value & "," & _
        '                " " & ugR.Cells("LineTotal_Excl").Value + ugR.Cells("Line_Dis_Excl").Value & "," & _
        '                " " & ugR.Cells("LineTax").Value & "," & ugR.Cells("Line_Tax_NoDis_Excl").Value & "," & IIf(cmbSaleRep.Value = Nothing, 0, cmbSaleRep.Value) & "," & IIf(cmbProject.Value = Nothing, 0, cmbProject.Value) & "," & _
        '                " '1'," & ugR.Cells("Line").Value & ",0,0,0,0,'',0,0," & ugR.Cells("Quantity").Value & "," & _
        '                " " & ugR.Cells("OrderTotal_Incl").Value & "," & ugR.Cells("OrderTotal_Excl").Value & "," & _
        '                " " & ugR.Cells("OrderTotal_Incl").Value + ugR.Cells("Order_Dis_Incl").Value & "," & _
        '                " " & ugR.Cells("OrderTotal_Excl").Value + ugR.Cells("Order_Dis_Excl").Value & "," & _
        '                " " & ugR.Cells("OrderTax").Value & "," & ugR.Cells("Ord_Tax_NoDis_Excl").Value & ", 1, " & ugR.Cells("Lot").Value & ", " & ugR.Cells("Lot").Value & ", " & ugR.Cells("Lot_Exp").Value & ")"
        '                If .Execute_Sql_Trans(SQL) = 0 Then
        '                    .Rollback_Trans()
        '                    Exit Sub
        '                End If

        '            Next
        '            .Commit_Trans()
        '            Discard()
        '        Catch ex As Exception
        '            .Rollback_Trans()
        '            MsgBox(ex.Message, MsgBoxStyle.Critical, "Pastel Evolution")
        '            Exit Sub
        '        Finally
        '            .Con_Close()
        '            objSQL = Nothing
        '        End Try
        '    End With
    End Sub
    Private Sub SaveMySettingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveMySettingToolStripMenuItem.Click
        Try
            If iDocType = DocType.SalesOrder Then
                UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "SO.xml")
            ElseIf iDocType = DocType.PurchaseOrder Then
                UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "PO.xml")
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub tsbOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbOpen.Click
        If iDocType = DocType.SalesOrder Then
            frmOpen.iDocType_1 = 0
            frmOpen.Get_Customer()
            frmOpen.iDocType = DocType.SalesOrder
        ElseIf iDocType = DocType.PurchaseOrder Then
            frmOpen.iDocType_1 = 0
            frmOpen.Get_Supplier()
            frmOpen.iDocType = DocType.PurchaseOrder
        ElseIf iDocType = DocType.Invoice Then
            frmOpen.iDocType_1 = 10
            frmOpen.Get_Customer()
            frmOpen.iDocType = DocType.SalesOrder
        ElseIf iDocType = DocType.IBTReceive Then
            frmOpen.iDocType_1 = 11
            frmOpen.Get_Supplier()
            frmOpen.iDocType = DocType.PurchaseOrder
        End If
        frmOpen.ShowDialog()
        frmOpen.TopMost = True
        Me.Close()
    End Sub
    Private Sub tsbSaveSO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSaveSO.Click
        ''frmPW.lblDueAmt.Text = txtConAmt.Value
        'frmPW.ShowDialog()

        Delete_Row()

        UG.PerformAction(UltraGridAction.CommitRow)

        If cmbCustomer.Value = Nothing Then
            MsgBox("Customer can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbCustomer.Text.Trim.Length = 0 Then
            MsgBox("Customer can not be left balnk", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        Dim ugR As UltraGridRow
        For Each ugR In UG.Rows
            If ugR.Cells("IsWH").Value = True Then
                If ugR.Cells("Warehouse").Value = Nothing Then
                    MsgBox("Warehouse can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    Exit Sub
                End If
            End If
            If ugR.Cells("Quantity").Value = 0 Then
                MsgBox("Quantity can not zero", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End If
            If ugR.Cells("ConfirmQty").Value = 0 Then
                MsgBox("Confirm Quantity can not be zero", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End If
        Next


        If cmbSaleRep.Text <> "" And cmbSaleRep.Value = Nothing Then
            cmbSaleRep.Value = CInt(cmbSaleRep.Text)
        End If

        If cmbSaleRep.Value = 0 Then
            MsgBox("Select Sales Person", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If


        If txtExtOrder.Text.Length = 0 Then
            MsgBox("Please insert a vehicle number", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If


        If UG.Rows.Count = 0 Then
            MsgBox("Please enter item details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If
        Try
            'Dim j = 1
            'Dim db = ""
            'db = sSQLSrvDataBase

            'For j = 1 To 2

            'If j = 1 Then
            '    db = sSQLSrvDataBase
            'Else
            '    db = "dbKelaniya_new"
            '    Con1 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database=" & db & "")
            '    'Dim objSQL = New clsSqlConn
            '    'With objSQL
            'End If


            DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
            DatabaseContext.SetLicense("DE09110022", "1428511")
            DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)


            DatabaseContext.BeginTran()
            Dim SalesOrder
            If iPONo = 0 Then
                SalesOrder = New SalesOrder
            Else
                SalesOrder = New SalesOrder(iPONo)
                SalesOrder.Detail.Clear()
            End If
            Dim SalesOrderDetail As OrderDetail
            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse




            Dim Customer As New Customer(CInt(cmbCustomer.Value))
            Dim bIsCheckTerms As Boolean
            Dim dblAccountBalance As Double = 0
            Dim dblCreditLimit As Double = 0
            bIsCheckTerms = Customer.CheckTerms
            If bIsCheckTerms = True Then
                '' ''dblAccountBalance = Customer.AccountBalance
                'get_ConsolidatedCredit()
                dblAccountBalance = conCLimit
                dblCreditLimit = Customer.CreditLimit
                If dblAccountBalance + txtConAmt.Value > dblCreditLimit And dblCreditLimit > 0.0 Then
                    If bIsAllowOverrideCreditLimit = False Then
                        MsgBox("Account is Over Credit Limit or Over Terms", MsgBoxStyle.Exclamation, "Pastel Evolution")
                        Exit Sub
                    End If

                    Dim dd As Integer
                    Dim AccTrms As Integer
                    AccTrms = Customer.AccountTerms
                    Select Case AccTrms
                        Case 0
                            AccTrms = 0
                        Case 1
                            AccTrms = 30
                        Case 2
                            AccTrms = 60
                        Case 3
                            AccTrms = 90
                        Case 4
                            AccTrms = 120
                        Case 5
                            AccTrms = 150
                        Case 6
                            AccTrms = 180
                    End Select
                    'GLAccount.Find(Customer.AccountBalance)
                    'dd = DateDiff(DateInterval.Day, Now(), CDate("4/3/2011 6:00:00"))

                End If
            End If

            Dim Representative As New SalesRepresentative(CInt(cmbSaleRep.Value))
            SalesOrder.Account = Customer
            SalesOrder.InvoiceTo = Customer.PostalAddress
            SalesOrder.DeliverTo = Customer.PhysicalAddress

            'If j = 2 Then
            '    SalesOrder.ExternalOrderNo = sSalesOrder
            'Else
            SalesOrder.ExternalOrderNo = txtExtOrder.Text.ToString
            'End If


            SalesOrder.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            SalesOrder.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            SalesOrder.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
            SalesOrder.Representative = Representative
            SalesOrder.DiscountPercent = txtDisPer.Value
            SalesOrder.RawFieldData("ulIDSOrdJrRep") = cmbJrRep.Text
            SalesOrder.ProjectID = cmbProject.Value

            If bIsInclusive = True Then
                SalesOrder.TaxMode = TaxMode.Inclusive
            ElseIf bIsInclusive = False Then
                SalesOrder.TaxMode = TaxMode.Exclusive
            End If

            For Each ugR In UG.Rows
                'If j = 1 And ugR.Cells("ItemGroup").Value = "BODY PANELS" Then
                '    Continue For
                'ElseIf j = 2 And ugR.Cells("ItemGroup").Value <> "BODY PANELS" Then
                '    Continue For
                'End If

                SalesOrderDetail = New OrderDetail
                InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))

                SalesOrderDetail.InventoryItem = InventoryItem

                If ugR.Cells("iUnit").Value > 0 Then
                    Dim d As Double = Math.Round(ugR.Cells("Quantity").Value / convertion, 10)
                    SalesOrderDetail.Quantity = d
                    SalesOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value) / convertion
                Else
                    SalesOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                    SalesOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)
                End If
                If bIsInclusive = True Then
                    SalesOrderDetail.TaxMode = TaxMode.Inclusive
                    SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                    SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                ElseIf bIsInclusive = False Then
                    SalesOrderDetail.TaxMode = TaxMode.Exclusive
                    SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                    SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                End If

                If ugR.Cells("IsLot").Value = True Then
                    If ugR.Cells("Lot").Value = Nothing Then
                        MsgBox("Enter Lot Numbers" & vbCrLf & " Item = " & ugR.Cells("Code").Value, MsgBoxStyle.Exclamation, "Pastel Evolution")
                        DatabaseContext.RollbackTran()
                        If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                        If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                        Exit Sub
                    End If
                    'If ugR.ChildBands.HasChildRows = False Then
                    '    MsgBox("Duplicate Serial No" & vbCrLf & "Item Code - " & ugR.Cells("Description_1").Value & vbCrLf & "Line No - " & ugR.Index + 1, MsgBoxStyle.Exclamation, "Pastel Evolution")
                    '    'MsgBox("Enter Serial Numbers" & vbCrLf & "Item Code - " & ugR.Cells("Description_1").Value & vbCrLf & ugR.Index + 1, MsgBoxStyle.Exclamation, "Pastel Evolution")
                    '    DatabaseContext.RollbackTran()
                    '    If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                    '    If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                    '    Exit Sub
                    'End If
                    'Dim ugR2 As UltraGridRow
                    'For Each ugR2 In ugR.ChildBands(0).Rows
                    '    Dim sn As String = CStr(ugR2.Cells("SerialNumber").Value)
                    '    SalesOrderDetail.SerialNumbers.Add(sn)
                    'Next
                End If

                If ugR.Cells("IsWH").Value = True Then
                    WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                    If ugR.Cells("IsWH").Value = True Then

                        SalesOrderDetail.Warehouse = WH


                    End If
                End If

                SalesOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                'SalesOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                SalesOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)





                If ugR.Cells("IsLot").Value = True Then
                    'If j = 1 Then
                    SalesOrderDetail.LotID = CStr(ugR.Cells("Lot").Value)
                    'Else



                    '    Try
                    '        sSQL = " SELECT top(1)  _etblLotTracking.idLotTracking,_etblLotTracking.cLotDescription AS Description, _etblLotTracking.dExpiryDate AS [Expiry Date], _etblLotTrackingQty.fQtyOnHand AS [Qty On Hand], " & _
                    '        " _etblLotTrackingQty.fQtyReserved AS [Qty Reserved]" & _
                    '        " FROM         _etblLotTracking INNER JOIN " & _
                    '        " _etblLotTrackingQty ON _etblLotTracking.idLotTracking = _etblLotTrackingQty.iLotTrackingID WHERE  _etblLotTracking.iStockID = " & ugR.Cells("StockID").Value & " AND _etblLotTrackingQty.fQtyOnHand > 0 AND _etblLotTrackingQty.iWarehouseID =  " & ugR.Cells("warehouse").Value & " "


                    '        'Con1.ConnectionString = sConStr
                    '        CMD = New SqlCommand(sSQL, Con1)
                    '        DS = New DataSet
                    '        DA = New SqlDataAdapter(CMD)

                    '        Con1.Open()
                    '        DA.Fill(DS)
                    '        Con1.Close()

                    '        SalesOrderDetail.LotID = DS.Tables(0).Rows(0)(0)
                    '    Catch ex As Exception
                    '        MsgBox("There are no lot quantity in Kelaniya database PKW Warehouse", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    '    End Try
                    'End If





                End If

                SalesOrder.Detail.Add(SalesOrderDetail)


            Next
            'Dim test As Boolean = IsNew
            'If isQuote = True Then
            '    DeleteQuote(index)
            'End If

            SalesOrder.Save()
            SalesOrder.Process()
            tsbSaveSO.Enabled = False
            Dim iSalesOrder As Integer = SalesOrder.ID
            'sSalesOrder = SalesOrder.ID.OrderNo.ToString()
            MsgBox(SalesOrder.Reference, MsgBoxStyle.Information, "Pastel Evolution")
            DatabaseContext.CommitTran()



            If MsgBox("Do you want to print Sales Order ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                Dim objRep As New ReportDocument
                Dim s As String = Application.StartupPath
                If Customer.TaxNumber = Nothing Then
                    objRep.Load(s & "\SalesOrder.rpt")
                Else
                    objRep.Load(s & "\SalesOrder_VAT.rpt")
                End If
                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSalesOrder & ""
                ApplyLoginToTable(objRep)


                'If MsgBox("Do you want to preview the sales order", MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.No Then
                Dim DefaultPrinter As String = DefaultPrinterName
                Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                doctoprint.PrinterSettings.PrinterName = DefaultPrinter
                For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    Dim rawKind As Integer
                    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inv" Or doctoprint.PrinterSettings.PaperSizes(i).PaperName = "INV" Then
                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        objRep.PrintOptions.PaperSize = rawKind
                        objRep.PrintToPrinter(1, False, 0, 0)
                        Exit For
                    End If
                Next
                'Else
                '    Dim objCRV As New frmPrintPreview
                '    With objCRV
                '        .CRV.ReportSource = objRep
                '        .Text = "Sales Order Printing"
                '        .Show()
                '        .TopMost = True
                '    End With
                'End If



            End If




            'Dim aloc As New AllocationEntry(SalesOrder, 1000)

            'aloc.Transaction.Allocations.Add(SalesOrder)
            'aloc.Transaction.Post()




            '--------------------------------Print to differnt floors----------------------------
            If sSQLSrvDataBase = "dbKelaniya_new" Then
                Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                Dim objRep As New ReportDocument

                objRep.Load(Application.StartupPath & "\F1.rpt")
                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSONo & ""
                ApplyLoginToTable(objRep)
                'If MsgBox("Do you want to preview the sales order", MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.No Then

                doctoprint = New System.Drawing.Printing.PrintDocument()
                doctoprint.PrinterSettings.PrinterName = "F1"
                For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    Dim rawKind As Integer
                    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inv" Or doctoprint.PrinterSettings.PaperSizes(i).PaperName = "INV" Then
                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        objRep.PrintOptions.PaperSize = rawKind
                        objRep.PrintToPrinter(1, False, 0, 0)
                        Exit For
                    End If
                Next


                objRep.Load(Application.StartupPath & "\F2.rpt")
                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSONo & ""
                ApplyLoginToTable(objRep)
                'If MsgBox("Do you want to preview the sales order", MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.No Then

                doctoprint = New System.Drawing.Printing.PrintDocument()
                doctoprint.PrinterSettings.PrinterName = "F2"
                For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    Dim rawKind As Integer
                    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inv" Or doctoprint.PrinterSettings.PaperSizes(i).PaperName = "INV" Then
                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        objRep.PrintOptions.PaperSize = rawKind
                        objRep.PrintToPrinter(1, False, 0, 0)
                        Exit For
                    End If
                Next

                objRep.Load(Application.StartupPath & "\F3.rpt")
                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSONo & ""
                ApplyLoginToTable(objRep)
                'If MsgBox("Do you want to preview the sales order", MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.No Then

                doctoprint = New System.Drawing.Printing.PrintDocument()
                doctoprint.PrinterSettings.PrinterName = "F3"
                For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    Dim rawKind As Integer
                    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inv" Or doctoprint.PrinterSettings.PaperSizes(i).PaperName = "INV" Then
                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        objRep.PrintOptions.PaperSize = rawKind
                        objRep.PrintToPrinter(1, False, 0, 0)
                        Exit For
                    End If
                Next

                objRep.Load(Application.StartupPath & "\F4.rpt")
                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSONo & ""
                ApplyLoginToTable(objRep)
                'If MsgBox("Do you want to preview the sales order", MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.No Then

                doctoprint = New System.Drawing.Printing.PrintDocument()
                doctoprint.PrinterSettings.PrinterName = "F4"
                For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    Dim rawKind As Integer
                    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inv" Or doctoprint.PrinterSettings.PaperSizes(i).PaperName = "INV" Then
                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        objRep.PrintOptions.PaperSize = rawKind
                        objRep.PrintToPrinter(1, False, 0, 0)
                        Exit For
                    End If
                Next

            End If



            '---------------------------------------Picking Slip----------------------------------------------
            If iCusGroup <> 1 And iCusGroup <> 6 Then
                If MsgBox("Do you want to print Picking Slip ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                    Dim objRep As New ReportDocument
                    Dim s As String = Application.StartupPath

                    objRep.Load(s & "\PickingSlip.rpt")

                    objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSalesOrder & ""
                    ApplyLoginToTable(objRep)


                    'If MsgBox("Do you want to preview the sales order", MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.No Then
                    'Dim DefaultPrinter As String = DefaultPrinterName
                    'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    'doctoprint.PrinterSettings.PrinterName = DefaultPrinter
                    'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    '    Dim rawKind As Integer
                    '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Inv" Or doctoprint.PrinterSettings.PaperSizes(i).PaperName = "INV" Then
                    '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    '        objRep.PrintOptions.PaperSize = rawKind
                    '        objRep.PrintToPrinter(1, False, 0, 0)
                    '        Exit For
                    '    End If
                    'Next
                    'Else
                    Dim objCRV As New frmPrintPreview
                    With objCRV
                        .CRV.ReportSource = objRep
                        .Text = "Sales Order Picking Slip"
                        .Show()
                        .TopMost = True
                    End With
                    'End If


                End If
            End If



            'If j = 1 Then
            If Customer.Group.Code <> "CRD" Then
                frmPW.project = cmbProject.Text
                frmPW.ref = SalesOrder.Reference
                frmPW.cusID = cmbCustomer.Value
                frmPW.repID = cmbSaleRep.Value
                frmPW.lblDueAmt.Text = txtConAmt.Value
                frmPW.ShowDialog()
            End If
            'End If
            'Next
            tsbSaveSO.Enabled = True

            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
            Discard()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
            DatabaseContext.RollbackTran()
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        Finally
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        End Try
    End Sub
    Private Sub DeleteQuote(ByVal index As Integer)
        Dim objSQL As New clsSqlConn
        With objSQL
            Try
                .Begin_Trans()

                SQL = "delete FROM _btblInvoiceLines WHERE AutoIndex =" & index
                If .Execute_Sql_Trans(SQL) = 0 Then
                    .Rollback_Trans()
                    Exit Sub
                End If

                .Commit_Trans()

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
            Finally
                objSQL.Con_Close()
                GC.Collect()
            End Try

        End With
    End Sub
    Private ReadOnly Property DefaultPrinterName() As String
        Get
            Dim ps As New PrinterSettings()
            Return ps.PrinterName
        End Get
    End Property
    Private Sub stbUpdateSO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbUpdateSO.Click
        Dim bVAT As Boolean

        Delete_Row()

        UG.PerformAction(UltraGridAction.CommitRow)

        If cmbCustomer.Value = Nothing Then
            MsgBox("Customer can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbCustomer.Text.Trim.Length = 0 Then
            MsgBox("Customer can not be left balnk", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If UG.Rows.Count = 0 Then
            MsgBox("Please enter item details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        Dim ugR As UltraGridRow
        For Each ugR In UG.Rows
            If ugR.Cells("IsWH").Value = True Then
                If ugR.Cells("Warehouse").Value = Nothing Then
                    MsgBox("Warehouse can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    Exit Sub
                End If
            End If
            If ugR.Cells("Quantity").Value = 0 Then
                MsgBox("Quantity can not zero", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End If
            If ugR.Cells("ConfirmQty").Value = 0 Then
                MsgBox("Confirm Quantity can not be zero", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End If
        Next

        If cmbSaleRep.Value = Nothing Then
            MsgBox("Select Sales Person", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)

        Try
            Dim SalesOrder
            DatabaseContext.BeginTran()
            If iPONo = 0 Then
                SalesOrder = New SalesOrder
            Else
                SalesOrder = New SalesOrder(iPONo)
            End If

            Dim SalesOrderDetail As OrderDetail
            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse

            Dim Customer As New Customer(CInt(cmbCustomer.Value))
            Dim Representative As New SalesRepresentative(CInt(cmbSaleRep.Value))

            If Customer.TaxNumber <> "" Then
                bVAT = True
            Else
                bVAT = False
            End If

            SalesOrder.Account = Customer
            SalesOrder.InvoiceTo = Customer.PostalAddress
            SalesOrder.DeliverTo = Customer.PhysicalAddress
            SalesOrder.ExternalOrderNo = txtExtOrder.Text.ToString
            SalesOrder.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            SalesOrder.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            SalesOrder.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
            SalesOrder.Representative = Representative
            SalesOrder.DiscountPercent = txtDisPer.Value


            If bIsInclusive = True Then
                SalesOrder.TaxMode = TaxMode.Inclusive
            ElseIf bIsInclusive = False Then
                SalesOrder.TaxMode = TaxMode.Exclusive
            End If


            Dim bLoop As Boolean = False

            Try

lbl1:           For Each SalesOrderDetail In SalesOrder.Detail
                    bLoop = False
                    SalesOrder.Detail.Remove(SalesOrderDetail)
                    bLoop = True
                    Exit For
                Next
                If bLoop Then
                    bLoop = False
                    GoTo lbl1
                End If

            Catch ex As Exception

            Finally

                bLoop = False
            End Try


            For Each ugR In UG.Rows
                SalesOrderDetail = New OrderDetail
                InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))

                SalesOrderDetail.InventoryItem = InventoryItem
                SalesOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                SalesOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)
                If bIsInclusive = True Then
                    SalesOrderDetail.TaxMode = TaxMode.Inclusive
                    SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                    SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                ElseIf bIsInclusive = False Then
                    SalesOrderDetail.TaxMode = TaxMode.Exclusive
                    SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                    SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                End If
                If ugR.Cells("IsWH").Value = True Then
                    WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                    If ugR.Cells("IsWH").Value = True Then
                        SalesOrderDetail.Warehouse = WH
                    End If
                End If
                SalesOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                SalesOrderDetail.Reserved = CDbl(ugR.Cells("Quantity").Value)
                'SalesOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                SalesOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)

                'Dim LotN As New Lot()

                'LotN.Code = CStr(ugR.Cells("Lot").Text)
                ''LotN. = CStr(ugR.Cells("Lot").Text)
                'SalesOrderDetail.Lot = LotN

                SalesOrderDetail.LotID = CStr(ugR.Cells("Lot").Value)


                SalesOrder.Detail.Add(SalesOrderDetail)
            Next
            SalesOrder.Save()
            Dim iSalesOrder As Integer = SalesOrder.ID
            MsgBox(SalesOrder.OrderNo, MsgBoxStyle.Information, "Pastel Evolution")
            DatabaseContext.CommitTran()
            If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                Dim ps As New PrinterSettings
                Dim objRep As New ReportDocument

                If bVAT Then
                    objRep.Load(Application.StartupPath & "\SalesOrder_VAT")
                Else
                    objRep.Load(Application.StartupPath & "\SalesOrder.rpt")
                End If


                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSalesOrder & ""
                ApplyLoginToTable(objRep)
                'objRep.SetDatabaseLogon(sSQLSrvUserName, sSQLSrvPassword, sSQLSrvReportSrv, sSQLSrvDataBase)
                Dim objCRV As New frmPrintPreview
                With objCRV
                    .CRV.ReportSource = objRep
                    .Text = "Sales Order Printing"
                    .ShowDialog()
                End With
            End If
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
            Discard()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
            DatabaseContext.RollbackTran()
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        Finally
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        End Try
    End Sub

    Private Sub tsbSavePO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSavePO.Click

        Delete_Row()

        UG.PerformAction(UltraGridAction.CommitRow)

        If cmbCustomer.Value = Nothing Then
            MsgBox("Supplier can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbCustomer.Text.Trim.Length = 0 Then
            MsgBox("Supplier can not be left balnk", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If UG.Rows.Count = 0 Then
            MsgBox("Please enter item details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbType.Text.Trim.Length = 0 Then
            MsgBox("Enter Purchase Order Type", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        Dim ugR As UltraGridRow
        Dim ugcr As UltraGridRow
        'For Each ugR In UG.Rows
        '    If ugR.Cells("IsWH").Value = True Then
        '        If ugR.Cells("Warehouse").Value = Nothing Then
        '            MsgBox("Warehouse can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
        '            Exit Sub
        '        End If
        '    End If
        'Next
        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)
        Try

            DatabaseContext.BeginTran()
            Dim PurchaseOrder As New PurchaseOrder
            Dim PurchaseOrderDetail As OrderDetail
            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse

            Dim Supplier As New Supplier(CInt(cmbCustomer.Value))

            PurchaseOrder.Account = Supplier
            PurchaseOrder.InvoiceTo = Supplier.PostalAddress
            PurchaseOrder.DeliverTo = Supplier.PhysicalAddress
            PurchaseOrder.ExternalOrderNo = txtExtOrder.Text.ToString
            PurchaseOrder.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            PurchaseOrder.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            PurchaseOrder.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
            PurchaseOrder.SupplierInvoiceNo = txtDelNote_SupInvNo.Text.ToString
            PurchaseOrder.UserFields("ulIDPOrdType") = cmbType.Value
            If bIsInclusive = True Then
                PurchaseOrder.TaxMode = TaxMode.Inclusive
            ElseIf bIsInclusive = False Then
                PurchaseOrder.TaxMode = TaxMode.Exclusive
            End If


            For Each ugR In UG.Rows
                PurchaseOrderDetail = New OrderDetail
                InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))
                PurchaseOrderDetail.InventoryItem = InventoryItem
                PurchaseOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                'PurchaseOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                PurchaseOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)

                If bIsInclusive = True Then
                    PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                    PurchaseOrderDetail.TaxMode = TaxMode.Inclusive
                    PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                ElseIf bIsInclusive = False Then
                    PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                    PurchaseOrderDetail.TaxMode = TaxMode.Exclusive
                    PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    PurchaseOrderDetail.UnitID = CInt(ugR.Cells("iUnit").Value)
                End If

                If ugR.Cells("IsWH").Value = True Then
                    WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                    PurchaseOrderDetail.Warehouse = WH
                End If
                PurchaseOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                PurchaseOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)
                'PurchaseOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                'Dim ugcr As UltraGridRow
                'For Each ugcr In ugR.ChildBands.Item(0).Rows
                '    PurchaseOrderDetail.SerialNumbers.Add(ugcr.Cells("SerialNumber").Value)
                'Next
                Dim LotN As New Lot()
                LotN.Code = ugR.Cells("SerialLot").Text
                LotN.ExpiryDate = ugR.Cells("Lot_Exp").Value
                PurchaseOrderDetail.Lot = LotN

                PurchaseOrder.Detail.Add(PurchaseOrderDetail)

            Next

            PurchaseOrder.Save()

            DatabaseContext.CommitTran()

            iPONo = PurchaseOrder.ID

            'Call MemorizeStock()

            MsgBox(PurchaseOrder.OrderNo, MsgBoxStyle.Information, "Pastel Evolution")

            Dim iPO As Integer = PurchaseOrder.ID

            If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                Dim objRep As New ReportDocument
                objRep.Load(Application.StartupPath & "\PO.rpt")
                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iPO & ""
                ApplyLoginToTable(objRep)
                'objRep.SetDatabaseLogon(sSQLSrvUserName, sSQLSrvPassword, sSQLSrvReportSrv, sSQLSrvDataBase)
                Dim objCRV As New frmPrintPreview
                With objCRV
                    .CRV.ReportSource = objRep
                    .Text = "Purchase Order Printing"
                    .ShowDialog()
                End With
            End If

            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
            Discard()
        Catch ex As Exception
            DatabaseContext.RollbackTran()
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
            MsgBox(Err.Description)
        Finally
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        End Try
    End Sub
    Public Sub ApplyLoginToTable(ByVal ObjReport As ReportDocument)
        Dim ConInf As New TableLogOnInfo
        Dim crTables As Tables = ObjReport.Database.Tables
        For Each crTable As Table In crTables
            With ConInf.ConnectionInfo
                .ServerName = sSQLSrvReportSrv
                .DatabaseName = sSQLSrvDataBase
                .UserID = sSQLSrvUserName
                .Password = sSQLSrvPassword
            End With
            crTable.ApplyLogOnInfo(ConInf)
        Next
    End Sub
    Private Sub tsbUpdatePO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbUpdatePO.Click
        Delete_Row()
        If cmbCustomer.Value = Nothing Then
            MsgBox("Supplier can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbCustomer.Text.Trim.Length = 0 Then
            MsgBox("Supplier can not be left balnk", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbType.Text.Trim.Length = 0 Then
            MsgBox("Enter Purchase Order Type", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If UG.Rows.Count = 0 Then
            MsgBox("Please enter item details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        Dim ugR As UltraGridRow
        For Each ugR In UG.Rows
            If ugR.Cells("IsWH").Value = True Then
                If ugR.Cells("Warehouse").Value = Nothing Then
                    MsgBox("Warehouse can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    Exit Sub
                End If
            End If
        Next

        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)
        Try
            If iPONo = 0 Then
                MsgBox("save the purchase order before processed", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End If

            DatabaseContext.BeginTran()
            Dim PurchaseOrder As New PurchaseOrder(CInt(iPONo))
            Dim PurchaseOrderDetail As OrderDetail
            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse

            Dim Supplier As New Supplier(CInt(cmbCustomer.Value))

            'Dim si As New SupplierTransaction(iPONo) // commented becose update purchase order wont work


            PurchaseOrder.Account = Supplier
            PurchaseOrder.InvoiceTo = Supplier.PostalAddress
            PurchaseOrder.DeliverTo = Supplier.PhysicalAddress
            PurchaseOrder.ExternalOrderNo = txtExtOrder.Text.ToString
            PurchaseOrder.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            PurchaseOrder.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            PurchaseOrder.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
            PurchaseOrder.SupplierInvoiceNo = txtDelNote_SupInvNo.Text.ToString
            PurchaseOrder.UserFields("ulIDPOrdType") = cmbType.Value
            If bIsInclusive = True Then
                PurchaseOrder.TaxMode = TaxMode.Inclusive
            ElseIf bIsInclusive = False Then
                PurchaseOrder.TaxMode = TaxMode.Exclusive
            End If

            Dim bLoop As Boolean = False

            Try

lbl1:           For Each PurchaseOrderDetail In PurchaseOrder.Detail
                    bLoop = False
                    PurchaseOrder.Detail.Remove(PurchaseOrderDetail)
                    bLoop = True
                    Exit For
                Next
                If bLoop Then
                    bLoop = False
                    GoTo lbl1
                End If

            Catch ex As Exception

            Finally

                bLoop = False
            End Try


            For Each ugR In UG.Rows
                Dim i As Integer = 0
                'For Each PurchaseOrderDetail In PurchaseOrder.Detail
                '    If ugR.Cells("LineID").Value = PurchaseOrderDetail.ID Then
                '        i = i + 1
                '        InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))
                '        PurchaseOrderDetail.InventoryItem = InventoryItem
                '        PurchaseOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                '        PurchaseOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)
                '        If bIsInclusive = True Then
                '            PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                '            PurchaseOrderDetail.TaxMode = TaxMode.Inclusive
                '            PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                '        ElseIf bIsInclusive = False Then
                '            PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                '            PurchaseOrderDetail.TaxMode = TaxMode.Exclusive
                '            PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                '        End If
                '        If ugR.Cells("IsWH").Value = True Then
                '            WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                '            PurchaseOrderDetail.Warehouse = WH
                '        End If
                '        PurchaseOrderDetail.Description = CStr(ugR.Cells("Description_2").Value)
                '        PurchaseOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)
                '        PurchaseOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                '        Exit For
                '    End If
                'Next

                If i = 0 Then

                    'If CStr(ugR.Cells("Description_1").Value) = 172 Then
                    '    MsgBox("aaa")
                    'End If

                    PurchaseOrderDetail = New OrderDetail
                    InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))
                    PurchaseOrderDetail.InventoryItem = InventoryItem


                    'calculate unit of mesure

                    PurchaseOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                    PurchaseOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)

                    'Row.Cells("ProcessedQty").Value = Dr.Item("fQtyProcessed")
                    'If Dr.Item("fQtyLastProcess") <> 0 Then
                    '    Row.Cells("ConfirmQty").Value = Dr.Item("fQtyLastProcess")
                    'End If

                    If bIsInclusive = True Then
                        PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                        PurchaseOrderDetail.TaxMode = TaxMode.Inclusive
                        PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    ElseIf bIsInclusive = False Then
                        PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                        PurchaseOrderDetail.TaxMode = TaxMode.Exclusive
                        PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    End If
                    If ugR.Cells("IsWH").Value = True Then
                        WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                        PurchaseOrderDetail.Warehouse = WH

                        Dim OS As New OrderStatus
                        If sSQLSrvDataBase = "dbNawinna_new" Then
                            If WH.ID = 3 Then
                                OS = New OrderStatus("NAWINNA")
                                PurchaseOrder.OrderStatus = OS
                            ElseIf WH.ID = 18 Then
                                OS = New OrderStatus("KIRIBATHGODA")
                                PurchaseOrder.OrderStatus = OS
                            ElseIf WH.ID = 4 Then
                                OS = New OrderStatus("NEGOMBO")
                                PurchaseOrder.OrderStatus = OS
                            End If
                        ElseIf sSQLSrvDataBase = "dbUdawatta_new" Then
                            If WH.ID = 2 Then
                                OS = New OrderStatus("PANCHIKAWATTA")
                                PurchaseOrder.OrderStatus = OS
                            ElseIf WH.ID = 18 Then
                                OS = New OrderStatus("MATHARA")
                                PurchaseOrder.OrderStatus = OS
                            ElseIf WH.ID = 4 Then
                                OS = New OrderStatus("KURUNEGALA")
                                PurchaseOrder.OrderStatus = OS
                            End If
                        End If


                    End If

                    'PurchaseOrderDetail.SerialNumbers

                    PurchaseOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                    PurchaseOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)
                    'PurchaseOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)

                    'For Each ugcr In ugR.ChildBands.Item(0).Rows
                    '    PurchaseOrderDetail.SerialNumbers.Add(ugcr.Cells("SerialNumber").Value)
                    'Next
                    'PurchaseOrder.Detail.Add(PurchaseOrderDetail)


                    ' 'update unit of mesure quantity to lot---------------------------------------
                    ' sSQL = " SELECT iUnitAID, fUnitAQty, iUnitBID, fUnitBQty, fMarkup FROM _etblUnitConversion " & _
                    '" WHERE (iUnitAID = " & UG.ActiveRow.Cells("iUnit").Value & " or iUnitBID = " & UG.ActiveRow.Cells("iUnit").Value & ") "

                    ' Try
                    '     Con1.ConnectionString = sConStr
                    '     CMD = New SqlCommand(sSQL, Con1)
                    '     DS = New DataSet
                    '     DA = New SqlDataAdapter(CMD)

                    '     Con1.Open()
                    '     DA.Fill(DS)
                    '     Con1.Close()





                    '     If DS.Tables(0).Rows.Count > 0 Then
                    '         If UG.ActiveRow.Cells("iUnit").Value = DS.Tables(0).Rows(0)(0) Then
                    '             PurchaseOrderDetail.Quantity = PurchaseOrderDetail.Quantity * DS.Tables(0).Rows(0)(3)
                    '           convertion = DS.Tables(0).Rows(0)(3)
                    '         ElseIf UG.ActiveRow.Cells("iUnit").Value = DS.Tables(0).Rows(0)(2) Then
                    '             PurchaseOrderDetail.Quantity = PurchaseOrderDetail.Quantity * DS.Tables(0).Rows(0)(1)
                    '        convertion = DS.Tables(0).Rows(0)(1)
                    '         End If
                    '     End If
                    '           Catch ex As Exception
                    '     MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                    '     Exit Sub
                    ' Finally
                    '     If Con1.State = ConnectionState.Open Then Con1.Close()
                    ' End Try
                    ' '--------------------------------------------------------------------------





                    Dim LotN As New Lot()
                    LotN.Code = ugR.Cells("SerialLot").Text
                    LotN.ExpiryDate = ugR.Cells("Lot_Exp").Value
                    PurchaseOrderDetail.Lot = LotN

                    PurchaseOrder.Detail.Add(PurchaseOrderDetail)

                End If
            Next

            PurchaseOrder.Save()

            'Call MemorizeStock()

            MsgBox(PurchaseOrder.OrderNo, MsgBoxStyle.Information, "Pastel Evolution")

            Dim iPO As Integer = PurchaseOrder.ID
            DatabaseContext.CommitTran()
            If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                Dim objRep As New ReportDocument
                objRep.Load(Application.StartupPath & "\PO.rpt")
                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iPO & ""
                ApplyLoginToTable(objRep)
                'objRep.SetDatabaseLogon(sSQLSrvUserName, sSQLSrvPassword, sSQLSrvReportSrv, sSQLSrvDataBase)
                Dim objCRV As New frmPrintPreview
                With objCRV
                    .CRV.ReportSource = objRep
                    .Text = "Sales Order Printing"
                    .ShowDialog()
                End With
            End If

            Discard()

            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

        Catch ex As Exception
            DatabaseContext.RollbackTran()
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        Finally
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        End Try
    End Sub

    Private Sub tsbProcessPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbProcessPO.Click
        Delete_Row()
        If cmbCustomer.Value = Nothing Then
            MsgBox("Supplier can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbCustomer.Text.Trim.Length = 0 Then
            MsgBox("Supplier can not be left balnk", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If
        If UG.Rows.Count = 0 Then
            MsgBox("Please enter item details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbType.Text.Trim.Length = 0 Then
            MsgBox("Enter Purchase Order Type", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        Dim ugR As UltraGridRow
        For Each ugR In UG.Rows
            If ugR.Cells("IsWH").Value = True Then
                If ugR.Cells("Warehouse").Value = Nothing Then
                    MsgBox("Warehouse can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    Exit Sub
                End If
            End If
            'If ugR.Cells("Price_Excl").Value = 0 Then
            '    MsgBox("Price(Excl) can not be zero", MsgBoxStyle.Exclamation, "Pastel Evoltion")
            '    Exit Sub
            'End If
            Dim result As Integer
abc:        If ugR.Cells("Price_Incl").Value = 0 Or ugR.Cells("Price_Excl").Value = 0 Then
                result = MessageBox.Show("Item '" & ugR.Cells("Description_1").Value & "' price is set to zero", "Pastel Evolution", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning)
                If result = DialogResult.Abort Then
                    'ugR.Cells("Price_Incl").Activation = Activation.AllowEdit
                    Exit Sub
                ElseIf result = DialogResult.Retry Then
                    GoTo abc
                End If
            End If
        Next

        'For Each ugR In UG.Rows
        '    If ugR.Cells("IsLot").Value = True Then
        '        Dim ugR2 As UltraGridRow
        '        For Each ugR2 In ugR.ChildBands(0).Rows
        '            Dim sn As String = CStr(ugR2.Cells("SerialNumber").Value)
        '            SQL = "SELECT * FROM SerialMF WHERE SerialNumber ='" & sn & "'"
        '            DS = New DataSet
        '            Dim OBJsql As New clsSqlConn
        '            With OBJsql
        '                DS = .Get_Data_Sql(SQL)
        '                If DS.Tables(0).Rows.Count > 0 Then
        '                    'MsgBox("Duplicate Serial No", MsgBoxStyle.Exclamation, "Pastel Evolution")
        '                    MsgBox("Duplicate Serial No" & vbCrLf & "Item Code - " & ugR.Cells("Description_1").Value & vbCrLf & "Line No - " & ugR.Index + 1 & vbCrLf & "Serial No - " & ugR2.Cells("SerialNumber").Value, MsgBoxStyle.Exclamation, "Pastel Evolution")
        '                    Exit Sub
        '                End If
        '            End With
        '            OBJsql = Nothing
        '            DS.Dispose()
        '        Next
        '    End If

        'Next

        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)

        Try

            If iPONo = 0 Then
                MsgBox("save the purchase order before processed", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End If

            DatabaseContext.BeginTran()

            Dim PurchaseOrder As New PurchaseOrder(CInt(iPONo))
            Dim PurchaseOrderDetail As OrderDetail
            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse
            Dim Supplier As New Supplier(CInt(cmbCustomer.Value))
            PurchaseOrder.Account = Supplier
            PurchaseOrder.InvoiceTo = Supplier.PostalAddress
            PurchaseOrder.DeliverTo = Supplier.PhysicalAddress
            PurchaseOrder.ExternalOrderNo = txtExtOrder.Text.ToString
            PurchaseOrder.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            PurchaseOrder.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            PurchaseOrder.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
            PurchaseOrder.SupplierInvoiceNo = txtDelNote_SupInvNo.Text.ToString
            PurchaseOrder.Description = txtDescription.Text.ToString
            PurchaseOrder.UserFields("ulIDPOrdType") = cmbType.Value
            PurchaseOrder.ProjectID = cmbProject.Value

            If bIsInclusive = True Then
                PurchaseOrder.TaxMode = TaxMode.Inclusive
            ElseIf bIsInclusive = False Then
                PurchaseOrder.TaxMode = TaxMode.Exclusive
            End If
            For Each ugR In UG.Rows
                Dim i As Integer = 0
                For Each PurchaseOrderDetail In PurchaseOrder.Detail
                    If ugR.Cells("LineID").Value = PurchaseOrderDetail.ID Then
                        i = i + 1
                        InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))
                        PurchaseOrderDetail.InventoryItem = InventoryItem
                        PurchaseOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                        PurchaseOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)

                        If PurchaseOrder.TaxMode = TaxMode.Inclusive Then
                            PurchaseOrderDetail.TaxMode = TaxMode.Inclusive
                            PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                            PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                        ElseIf PurchaseOrder.TaxMode = TaxMode.Exclusive Then
                            PurchaseOrderDetail.TaxMode = TaxMode.Exclusive
                            PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                            PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                        End If

                        'If ugR.Cells("IsLot").Value = True Then
                        '    If ugR.ChildBands.HasChildRows = False Then
                        '        'MsgBox("Enter Serial Numbers", MsgBoxStyle.Exclamation, "Pastel Evolution")
                        '        MsgBox("Enter Serial Numbers" & vbCrLf & "Item Code - " & ugR.Cells("Description_1").Value & vbCrLf & "Line No - " & ugR.Index + 1, MsgBoxStyle.Exclamation, "Pastel Evolution")
                        '        DatabaseContext.RollbackTran()
                        '        If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                        '        If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                        '        Exit Sub
                        '    End If
                        '    Dim ugR2 As UltraGridRow
                        '    For Each ugR2 In ugR.ChildBands(0).Rows
                        '        Dim sn As String = CStr(ugR2.Cells("SerialNumber").Value)
                        '        PurchaseOrderDetail.SerialNumbers.Add(sn)
                        '    Next
                        'End If
                        If ugR.Cells("IsWH").Value = True Then
                            WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                            PurchaseOrderDetail.Warehouse = WH

                            Dim OS As New OrderStatus
                            If sSQLSrvDataBase = "dbNawinna_new" Then
                                If WH.ID = 3 Then
                                    OS = New OrderStatus("NAWINNA")
                                    PurchaseOrder.OrderStatus = OS
                                ElseIf WH.ID = 18 Then
                                    OS = New OrderStatus("KIRIBATHGODA")
                                    PurchaseOrder.OrderStatus = OS
                                ElseIf WH.ID = 4 Then
                                    OS = New OrderStatus("NEGOMBO")
                                    PurchaseOrder.OrderStatus = OS
                                End If
                            ElseIf sSQLSrvDataBase = "dbUdawatta_new" Then
                                If WH.ID = 2 Then
                                    OS = New OrderStatus("PANCHIKAWATTA")
                                    PurchaseOrder.OrderStatus = OS
                                ElseIf WH.ID = 18 Then
                                    OS = New OrderStatus("MATHARA")
                                    PurchaseOrder.OrderStatus = OS
                                ElseIf WH.ID = 4 Then
                                    OS = New OrderStatus("KURUNEGALA")
                                    PurchaseOrder.OrderStatus = OS
                                End If
                            End If

                        End If

                        PurchaseOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                        PurchaseOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)
                        'PurchaseOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                        PurchaseOrderDetail.UnitID = CDbl(ugR.Cells("iUnit").Value)




                        ' 'update unit of mesure quantity to lot---------------------------------------
                        ' sSQL = " SELECT iUnitAID, fUnitAQty, iUnitBID, fUnitBQty, fMarkup FROM _etblUnitConversion " & _
                        '" WHERE (iUnitAID = " & UG.ActiveRow.Cells("iUnit").Value & " or iUnitBID = " & UG.ActiveRow.Cells("iUnit").Value & ") "

                        ' Try
                        '     Con1.ConnectionString = sConStr
                        '     CMD = New SqlCommand(sSQL, Con1)
                        '     DS = New DataSet
                        '     DA = New SqlDataAdapter(CMD)

                        '     Con1.Open()
                        '     DA.Fill(DS)
                        '     Con1.Close()





                        '     If DS.Tables(0).Rows.Count > 0 Then
                        '         If UG.ActiveRow.Cells("iUnit").Value = DS.Tables(0).Rows(0)(0) Then
                        '             UG.ActiveRow.Cells("Quantity").Value = UG.ActiveRow.Cells("Quantity").Value * DS.Tables(0).Rows(0)(3)
                        '             PurchaseOrderDetail.Quantity = PurchaseOrderDetail.Quantity * DS.Tables(0).Rows(0)(3)
                        '             convertion = DS.Tables(0).Rows(0)(3)
                        '         ElseIf UG.ActiveRow.Cells("iUnit").Value = DS.Tables(0).Rows(0)(2) Then
                        '             UG.ActiveRow.Cells("Quantity").Value = UG.ActiveRow.Cells("Quantity").Value * DS.Tables(0).Rows(0)(1)
                        '             PurchaseOrderDetail.Quantity = PurchaseOrderDetail.Quantity * DS.Tables(0).Rows(0)(1)
                        '             convertion = DS.Tables(0).Rows(0)(1)
                        '         End If
                        '     End If
                        ' Catch ex As Exception
                        '     MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                        '     Exit Sub
                        ' Finally
                        '     If Con1.State = ConnectionState.Open Then Con1.Close()
                        ' End Try
                        ' '--------------------------------------------------------------------------





                        Dim LotN As New Lot()
                        LotN.Code = ugR.Cells("SerialLot").Text
                        LotN.ExpiryDate = ugR.Cells("Lot_Exp").Value
                        LotN.StatusID = 2
                        PurchaseOrderDetail.Lot = LotN

                    End If
                Next

                If i = 0 Then
                    PurchaseOrderDetail = New OrderDetail
                    InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))
                    PurchaseOrderDetail.InventoryItem = InventoryItem
                    PurchaseOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                    PurchaseOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)

                    If PurchaseOrder.TaxMode = TaxMode.Inclusive Then
                        PurchaseOrderDetail.TaxMode = TaxMode.Inclusive
                        PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                        PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    ElseIf PurchaseOrder.TaxMode = TaxMode.Exclusive Then
                        PurchaseOrderDetail.TaxMode = TaxMode.Exclusive
                        PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                        PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    End If

                    If ugR.Cells("IsLot").Value = True Then
                        '    If ugR.ChildBands.HasChildRows = False Then
                        '        'MsgBox("Enter Serial Numbers", MsgBoxStyle.Exclamation, "Pastel Evolution")
                        '        MsgBox("Enter Serial Numbers" & vbCrLf & "Item Code - " & ugR.Cells("Description_1").Value & vbCrLf & ugR.Index + 1, MsgBoxStyle.Exclamation, "Pastel Evolution")

                        '        DatabaseContext.RollbackTran()
                        '        If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                        '        If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                        '        Exit Sub
                        '    End If
                        'Dim ugR2 As UltraGridRow
                        'For Each ugR2 In ugR.ChildBands(0).Rows
                        '    Dim sn As String = CStr(ugR2.Cells("SerialNumber").Value)
                        '    PurchaseOrderDetail.SerialNumbers.Add(sn)
                        'Next
                        '
                    End If

                    If ugR.Cells("IsWH").Value = True Then
                        WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                        PurchaseOrderDetail.Warehouse = WH

                        Dim OS As New OrderStatus
                        If sSQLSrvDataBase = "dbNawinna_new" Then
                            If WH.ID = 3 Then
                                OS = New OrderStatus("NAWINNA")
                                PurchaseOrder.OrderStatus = OS
                            ElseIf WH.ID = 18 Then
                                OS = New OrderStatus("KIRIBATHGODA")
                                PurchaseOrder.OrderStatus = OS
                            ElseIf WH.ID = 4 Then
                                OS = New OrderStatus("NEGOMBO")
                                PurchaseOrder.OrderStatus = OS
                            End If
                        ElseIf sSQLSrvDataBase = "dbUdawatta_new" Then
                            If WH.ID = 2 Then
                                OS = New OrderStatus("PANCHIKAWATTA")
                                PurchaseOrder.OrderStatus = OS
                            ElseIf WH.ID = 18 Then
                                OS = New OrderStatus("MATHARA")
                                PurchaseOrder.OrderStatus = OS
                            ElseIf WH.ID = 4 Then
                                OS = New OrderStatus("KURUNEGALA")
                                PurchaseOrder.OrderStatus = OS
                            End If
                        End If

                    End If


                    PurchaseOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                    PurchaseOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)
                    'PurchaseOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)



                    ' 'update unit of mesure quantity to lot---------------------------------------
                    ' sSQL = " SELECT iUnitAID, fUnitAQty, iUnitBID, fUnitBQty, fMarkup FROM _etblUnitConversion " & _
                    '" WHERE (iUnitAID = " & UG.ActiveRow.Cells("iUnit").Value & " or iUnitBID = " & UG.ActiveRow.Cells("iUnit").Value & ") "

                    ' Try
                    '     Con1.ConnectionString = sConStr
                    '     CMD = New SqlCommand(sSQL, Con1)
                    '     DS = New DataSet
                    '     DA = New SqlDataAdapter(CMD)

                    '     Con1.Open()
                    '     DA.Fill(DS)
                    '     Con1.Close()





                    '     If DS.Tables(0).Rows.Count > 0 Then
                    '         If UG.ActiveRow.Cells("iUnit").Value = DS.Tables(0).Rows(0)(0) Then
                    '             UG.ActiveRow.Cells("Quantity").Value = UG.ActiveRow.Cells("Quantity").Value * DS.Tables(0).Rows(0)(3)
                    '             convertion = DS.Tables(0).Rows(0)(3)
                    '         ElseIf UG.ActiveRow.Cells("iUnit").Value = DS.Tables(0).Rows(0)(2) Then
                    '             UG.ActiveRow.Cells("Quantity").Value = UG.ActiveRow.Cells("Quantity").Value * DS.Tables(0).Rows(0)(1)
                    '             convertion = DS.Tables(0).Rows(0)(1)
                    '         End If
                    '     End If
                    ' Catch ex As Exception
                    '     MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                    '     Exit Sub
                    ' Finally
                    '     If Con1.State = ConnectionState.Open Then Con1.Close()
                    ' End Try
                    ' '--------------------------------------------------------------------------




                    Dim LotN As New Lot()
                    LotN.Code = ugR.Cells("SerialLot").Text
                    LotN.ExpiryDate = ugR.Cells("Lot_Exp").Value
                    PurchaseOrderDetail.Lot = LotN

                    PurchaseOrder.Detail.Add(PurchaseOrderDetail)

                End If
            Next
            PurchaseOrder.Save()

            PurchaseOrder.Process() 'to process grv and supplier invoice together
            'PurchaseOrder.ProcessStock() 'to process only grv....supplier invoice is unprecessed

            'Dim si As New SupplierTransaction(PurchaseOrder.SupplierInvoiceNo)
            'si.Post()

            DatabaseContext.CommitTran()

            MsgBox(PurchaseOrder.Reference, MsgBoxStyle.Information, "Pastel Evolution")
            Dim iPO As Integer = PurchaseOrder.ID

            If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                Dim objRep As New ReportDocument
                objRep.Load(Application.StartupPath & "\PO.rpt")
                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iPO & ""
                ApplyLoginToTable(objRep)
                'objRep.SetDatabaseLogon(sSQLSrvUserName, sSQLSrvPassword, sSQLSrvReportSrv, sSQLSrvDataBase)
                Dim objCRV As New frmPrintPreview
                With objCRV
                    .CRV.ReportSource = objRep
                    .Text = "Sales Order Printing"
                    .ShowDialog()
                End With
            End If

            Discard()
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

        Catch ex As Exception

            DatabaseContext.RollbackTran()
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

            MsgBox(Err.Description)

        Finally
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        End Try
    End Sub

    Private Sub UG_ClickCellButton(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UG.ClickCellButton

        With frmCreateLot
            .Label3.Text = e.Cell.Row.Cells("StockID").Value.ToString
        End With
        frmCreateLot.txtLot.Text = txtDelNote_SupInvNo.Text + "-" + e.Cell.Row.Cells("Description_1").Value
        frmCreateLot.ShowDialog()


        If LotCN = True Then
            e.Cell.Row.Cells("SerialLot").Value = frmCreateLot.txtLot.Text
        Else
            e.Cell.Row.Cells("SerialLot").Value = frmCreateLot.cmbLot.Text
            'e.Cell.Row.Cells("SerialLot"). = frmCreateLot.cmbLot.Text
        End If

        e.Cell.Row.Cells("Lot_Exp").Value = frmCreateLot.dtpExpDate.Value


        frmCreateLot.Dispose()
        'If e.Cell.Row.Band.Index = 0 Then
        '    If e.Cell.Row.ChildBands.HasChildRows = True Then
        '        Dim ugCR As UltraGridRow
        '        Dim a As Integer
        '        a = e.Cell.Row.Index
        '        For Each ugCR In e.Cell.Row.ChildBands(0).Rows.All
        '            ugCR.Delete(False)
        '        Next
        '    End If
        'End If
        'b = 0
        'Dim ugR As UltraGridRow
        'For Each ListItem In .lbAdded.Items
        '    If ListItem.ToString <> "" Then
        '        b = b + 1
        '        If e.Cell.Row.Band.Index = 0 Then
        '            ugR = e.Cell.Row.ChildBands(0).Band.AddNew
        '        Else
        '            ugR = UG.DisplayLayout.Bands(1).AddNew
        '        End If
        '        ugR.Cells("LN").Value = ugR.Index + 1
        '        ugR.Cells("SerialNumber").Value = ListItem
        '        ugR.Cells("SNStockLink").Value = ugR.ParentRow.Cells("StockID").Value
        '        ugR.Cells("PrimaryLineID").Value = ugR.ParentRow.Cells("Line").Value
        '    End If
        'Next
        'For Each ugR In UG.Rows
        '    ugR.Expanded = False
        'Next


        '    e.Cell.Row.Cells("ConfirmQty").Value = b
        '    e.Cell.Row.Cells("Quantity").Value = b


        '    .Dispose()




        'End With
        'USED for serial numbers
        'If iDocType = DocType.SalesOrder Then
        '    If e.Cell.Column.Key = "SerialLot" Then
        '        Dim b As Integer
        '        'If e.Cell.Row.Cells("ConfirmQty").Value = 0 Then
        '        '    MsgBox("Confirm Qty can not be Zero", MsgBoxStyle.Exclamation, "Pastel Evolution")
        '        '    Exit Sub
        '        'End If

        '        Dim objSQL As New clsSqlConn
        '        With objSQL
        '            Try
        '                SQL = "SELECT SerialNumber, SNStockLink FROM SerialMF WHERE SNStockLink = " & e.Cell.Row.Cells("StockID").Value & " AND CurrentLoc = 1 and CurrentAccLink = " & iWH
        '                DS = New DataSet
        '                DS = .Get_Data_Sql(SQL)
        '                If DS.Tables(0).Rows.Count > 0 Then
        '                    Dim dr As DataRow
        '                    Dim a As Boolean
        '                    'frmSelectSerialNo.lbSelected.Items.Clear()
        '                    For Each dr In DS.Tables(0).Rows
        '                        a = False
        '                        If e.Cell.Row.ChildBands(0).Rows.Count > 0 Then
        '                            For Each ugCR In e.Cell.Row.ChildBands(0).Rows
        '                                If dr("SerialNumber") = ugCR.Cells("SerialNumber").Value Then
        '                                    a = True
        '                                    Exit For
        '                                End If
        '                            Next
        '                            If Not a Then
        '                                frmSelectSerialNo.lbSelected.Items.Add(CStr(dr("SerialNumber")))
        '                            End If
        '                        Else
        '                            frmSelectSerialNo.lbSelected.Items.Add(CStr(dr("SerialNumber")))
        '                        End If
        '                    Next


        '                    If e.Cell.Row.ChildBands.HasChildRows = True Then
        '                        Dim ugCR As UltraGridRow
        '                        frmSelectSerialNo.lbAdded.Items.Clear()
        '                        For Each ugCR In e.Cell.Row.ChildBands(0).Rows
        '                            frmSelectSerialNo.lbAdded.Items.Add(CStr(ugCR.Cells("SerialNumber").Value))
        '                            'b = b + 1
        '                        Next
        '                    End If



        '                End If
        '            Catch ex As Exception
        '                Exit Sub
        '            Finally
        '                DS.Dispose()
        '                objSQL = Nothing
        '                .Con_Close()
        '            End Try
        '        End With

        '        frmSelectSerialNo.ShowDialog()

        '        With frmSelectSerialNo

        '            If e.Cell.Row.Band.Index = 0 Then
        '                If e.Cell.Row.ChildBands.HasChildRows = True Then
        '                    Dim ugCR As UltraGridRow
        '                    Dim a As Integer
        '                    a = e.Cell.Row.Index
        '                    For Each ugCR In e.Cell.Row.ChildBands(0).Rows.All
        '                        ugCR.Delete(False)
        '                    Next
        '                End If
        '            End If
        '            b = 0
        '            Dim ugR As UltraGridRow
        '            For Each ListItem In .lbAdded.Items
        '                If ListItem.ToString <> "" Then
        '                    b = b + 1
        '                    If e.Cell.Row.Band.Index = 0 Then
        '                        ugR = e.Cell.Row.ChildBands(0).Band.AddNew
        '                    Else
        '                        ugR = UG.DisplayLayout.Bands(1).AddNew
        '                    End If
        '                    ugR.Cells("LN").Value = ugR.Index + 1
        '                    ugR.Cells("SerialNumber").Value = ListItem
        '                    ugR.Cells("SNStockLink").Value = ugR.ParentRow.Cells("StockID").Value
        '                    ugR.Cells("PrimaryLineID").Value = ugR.ParentRow.Cells("Line").Value
        '                End If
        '            Next
        '            For Each ugR In UG.Rows
        '                ugR.Expanded = False
        '            Next


        '            e.Cell.Row.Cells("ConfirmQty").Value = b
        '            e.Cell.Row.Cells("Quantity").Value = b


        '            .Dispose()




        '        End With

        '    End If
        'End If

        'If iDocType = DocType.PurchaseOrder Then
        '    If e.Cell.Column.Key = "SerialLot" Then
        '        'If e.Cell.Row.Cells("ConfirmQty").Value = 0 Then
        '        '    MsgBox("Confirm Qty can not be Zero", MsgBoxStyle.Exclamation, "Pastel Evolution")
        '        '    Exit Sub
        '        'End If

        '        frmSerialNo.intItem = e.Cell.Row.Cells("StockID").Value
        '        frmSerialNo.strCode = e.Cell.Row.Cells("Description_1").Value
        '        frmSerialNo.lbSelected.Items.Clear()
        '        frmSerialNo.txtNumber.Value = 0
        '        frmSerialNo.txtPad.Value = 0
        '        frmSerialNo.txtString.Text = Nothing
        '        frmSerialNo.txtGenerate.Value = e.Cell.Row.Cells("ConfirmQty").Value
        '        frmSerialNo.txtGenerate.Enabled = False

        '        If e.Cell.Row.Band.Index = 0 Then
        '            If e.Cell.Row.ChildBands.HasChildRows = True Then
        '                Dim ugCR As UltraGridRow
        '                frmSerialNo.lbSelected.Items.Clear()
        '                For Each ugCR In e.Cell.Row.ChildBands(0).Rows
        '                    frmSerialNo.lbSelected.Items.Add(CStr(ugCR.Cells("SerialNumber").Value))
        '                Next
        '            End If
        '        End If

        '        frmSerialNo.ShowDialog()

        '        With frmSerialNo

        '            If e.Cell.Row.Band.Index = 0 Then
        '                If e.Cell.Row.ChildBands.HasChildRows = True Then
        '                    Dim ugCR As UltraGridRow
        '                    Dim a As Integer
        '                    a = e.Cell.Row.Index
        '                    For Each ugCR In e.Cell.Row.ChildBands(0).Rows.All
        '                        ugCR.Delete(False)
        '                    Next
        '                End If
        '            End If

        '            Dim ugR As UltraGridRow
        '            For Each ListItem In .lbSelected.Items
        '                If e.Cell.Row.Band.Index = 0 Then
        '                    ugR = e.Cell.Row.ChildBands(0).Band.AddNew
        '                Else
        '                    ugR = UG.DisplayLayout.Bands(1).AddNew
        '                End If
        '                ugR.Cells("LN").Value = ugR.Index + 1
        '                ugR.Cells("SerialNumber").Value = ListItem
        '                ugR.Cells("SNStockLink").Value = ugR.ParentRow.Cells("StockID").Value
        '                ugR.Cells("PrimaryLineID").Value = ugR.ParentRow.Cells("Line").Value
        '            Next

        '            For Each ugR In UG.Rows
        '                ugR.Expanded = False
        '                If ugR.ChildBands.HasChildRows = True Then
        '                    ugR.CellAppearance.BackColor = Color.Yellow
        '                Else
        '                    ugR.CellAppearance.BackColor = Color.White
        '                End If
        '            Next

        '        End With

        '    End If
        'End If
    End Sub

    Private Sub txtBarCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBarCode.KeyDown
        If e.KeyCode = Keys.Enter Then
            Delete_Row()
            Dim sBarCode As String = txtBarCode.Text.Trim
            For Each ugR In UG.Rows
                If ugR.ChildBands.HasChildRows = True Then
                    Dim ugR1 As UltraGridRow
                    For Each ugR1 In ugR.ChildBands(0).Rows
                        If ugR1.Cells("SerialNumber").Value = sBarCode Then
                            txtBarCode.ResetText()
                            txtBarCode.Focus()
                            Exit Sub
                        End If
                    Next
                End If
            Next
            Dim objSQL As New clsSqlConn
            With objSQL
                SQL = "SELECT SerialNumber, SNStockLink FROM SerialMF WHERE SerialNumber ='" & CStr(sBarCode) & "' AND CurrentLoc = 1 and CurrentAccLink = " & iWH
                DS = New DataSet
                DS = .Get_Data_Sql(SQL)
                If DS.Tables(0).Rows.Count > 0 Then
                    Dim Dr As DataRow
                    For Each Dr In DS.Tables(0).Rows
                        'StockLink, Code, Description_1, ItemGroup, LotItem,WhseItem
                        Dim iStockLink As Integer = 0
                        Dim sCode As String = ""
                        Dim sDescription_1, sDescription_2 As String
                        sDescription_1 = ""
                        sDescription_2 = ""

                        Dim bLotItem As Boolean = False
                        Dim bWhseItem As Boolean = False

                        Dim ugR As UltraGridRow
                        For Each ugR In DDStock.Rows
                            If ugR.Cells("StockLink").Value = Dr("SNStockLink") Then
                                iStockLink = ugR.Cells("StockLink").Value
                                sCode = ugR.Cells("Code").Value
                                sDescription_1 = ugR.Cells("Description_1").Value
                                sDescription_2 = ugR.Cells("Description_2").Value
                                bLotItem = ugR.Cells("LotItem").Value
                                bWhseItem = ugR.Cells("WhseItem").Value
                            End If
                        Next

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

                                Else
                                    ugR1 = UG.ActiveRow.ChildBands(0).Band.AddNew
                                End If
                                ugR1.Cells("LN").Value = ugR1.Index + 1
                                ugR1.Cells("SerialNumber").Value = Dr("SerialNumber")
                                ugR1.Cells("SNStockLink").Value = iStockLink
                                ugR1.Cells("PrimaryLineID").Value = ugR.Cells("Line").Value
                            End If
                        Next
                        If iCount = 0 Then
                            ugR2 = UG.DisplayLayout.Bands(0).AddNew
                            ugR2.Cells("Line").Value = ugR2.Index + 1
                            ugR2.Cells("Warehouse").Value = iWH
                            ugR2.Cells("StockID").Value = iStockLink
                            ugR2.Cells("Code").Value = iStockLink
                            ugR2.Cells("Description_1").Value = sDescription_1
                            ugR2.Cells("Description_2").Value = sDescription_2
                            ugR2.Cells("IsLot").Value = bLotItem
                            ugR2.Cells("IsWH").Value = bWhseItem
                            'ugR2.Cells("Warehouse").Value = iWH
                            If iDocType = DocType.SalesOrder Then
                                ugR2.Cells("TaxType").Value = 1
                                For Each ugR3 As UltraGridRow In DDTaxType.Rows
                                    If ugR2.Cells("TaxType").Value = ugR3.Cells("Code").Value Then
                                        ugR2.Cells("TaxRate").Value = ugR3.Cells("TaxRate").Value
                                    End If
                                Next
                            ElseIf iDocType = DocType.PurchaseOrder Then
                                ugR2.Cells("TaxType").Value = 3
                                For Each ugR3 As UltraGridRow In DDTaxType.Rows
                                    If ugR2.Cells("TaxType").Value = ugR3.Cells("Code").Value Then
                                        ugR2.Cells("TaxRate").Value = ugR3.Cells("TaxRate").Value
                                    End If
                                Next
                            End If
                            ugR1 = ugR2.ChildBands(0).Band.AddNew()
                            ugR1.Cells("LN").Value = ugR1.Index + 1
                            ugR1.Cells("SerialNumber").Value = Dr("SerialNumber")
                            ugR1.Cells("SNStockLink").Value = iStockLink
                            ugR1.Cells("PrimaryLineID").Value = ugR2.Cells("Line").Value
                        End If
                    Next
                    For Each ugR In UG.Rows
                        If ugR.Cells("IsLot").Value = True Then
                            ugR.Cells("ConfirmQty").Value = ugR.ChildBands(0).Rows.Count
                            ugR.Cells("Quantity").Value = ugR.ChildBands(0).Rows.Count
                        End If
                    Next
                    Get_Total()
                Else
                    MsgBox("can not find the Serial Number", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    Exit Sub
                End If
            End With
            For Each ugR In UG.Rows
                ugR.Expanded = False
            Next
            txtBarCode.ResetText()
        End If


    End Sub
    Private Sub tsbDeleteLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbDeleteLine.Click
        If UG.Selected.Rows.Count > 0 Then
            Dim ugR As UltraGridRow
            For Each ugR In UG.Selected.Rows
                ugR.Delete(False)
            Next

            'For Each ugR In UG.Rows
            '    ugR.Expanded = False
            '    If ugR.ChildBands.HasChildRows = True Then
            '        ugR.CellAppearance.BackColor = Color.Yellow
            '    Else
            '        ugR.CellAppearance.BackColor = Color.White
            '    End If
            'Next

        End If
        Get_Total()
    End Sub
    Private Sub tsbNewlIne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbNewlIne.Click
        If iDocType = DocType.SalesOrder Then
            If cmbCustomer.Value = Nothing Then
                MessageBox.Show("Please select a customer", "Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        Dim ugR As UltraGridRow
        ugR = UG.DisplayLayout.Bands(0).AddNew
        ugR.Cells("Line").Value = ugR.Index + 1
        ugR.Cells("Line").Selected = True
        'ugR.Cells("islot").Value = 0


        'Dim agent = New Agent(iAgent)

        'If agent.Description = "4" Then
        '    ugR.Cells("Warehouse").Value = 4
        'Else
        ugR.Cells("Warehouse").Value = iWH
        'End If


        If iDocType = DocType.SalesOrder Then

            If sSQLSrvDataBase = "dbUdawatta_new" Then
                UG.DisplayLayout.Bands(0).Columns("Warehouse").CellActivation = Activation.AllowEdit
            Else
                UG.DisplayLayout.Bands(0).Columns("Warehouse").CellActivation = Activation.Disabled
            End If
            'If iCusPriceList = 2 Or iCusGroup <> 1 And iCusGroup <> 6 Then
            UG.DisplayLayout.Bands(0).Columns("Discount").CellActivation = Activation.Disabled
            UG.DisplayLayout.Bands(0).Columns("DiscountA").CellActivation = Activation.Disabled
            'Else
            '    UG.DisplayLayout.Bands(0).Columns("Discount").CellActivation = Activation.AllowEdit
            '    UG.DisplayLayout.Bands(0).Columns("DiscountA").CellActivation = Activation.AllowEdit
            'End If

            ugR.Cells("TaxType").Value = 1
            For Each ugR1 As UltraGridRow In DDTaxType.Rows
                If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
                    ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
                End If
            Next
        ElseIf iDocType = DocType.PurchaseOrder Then
            ugR.Cells("TaxType").Value = 3
            For Each ugR1 As UltraGridRow In DDTaxType.Rows
                If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
                    ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
                End If
            Next
        End If

    End Sub

    Private Sub txtBarCode_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBarCode.ValueChanged

    End Sub
    Private Function PriceExcl(ByVal TaxRate As Double, ByVal PriceIncl As Double) As Double
        Dim dblPriceExcl As Double = 0
        dblPriceExcl = Math.Round(PriceIncl * 100 / (100 + TaxRate), 2, MidpointRounding.AwayFromZero)
        Return dblPriceExcl
    End Function
    Private Function PriceIncl(ByVal TaxRate As Double, ByVal PriceExcl As Double) As Double
        Dim dblPriceIncl As Double = 0
        dblPriceIncl = Math.Round(PriceExcl + (PriceExcl * TaxRate / 100), 2, MidpointRounding.AwayFromZero)
        Return dblPriceIncl
    End Function
    Public Sub Get_Total()
        Dim ugR As UltraGridRow
        Dim dblOrderTax As Double = 0
        Dim dblLineTax As Double = 0
        Dim dblOrderTotal_Incl As Double = 0
        Dim dblLineTotal_Incl As Double = 0
        Dim dblLineTotal_Incl_Dis As Double = 0
        Dim dblLineTax_Dis As Double = 0
        For Each ugR In UG.Rows
            dblOrderTax = dblOrderTax + Math.Round(ugR.Cells("OrderTax").Value, 2, MidpointRounding.AwayFromZero)
            dblOrderTotal_Incl = dblOrderTotal_Incl + Math.Round(ugR.Cells("OrderTotal_Incl").Value, 2, MidpointRounding.AwayFromZero)
            dblLineTax = dblLineTax + Math.Round(ugR.Cells("LineTax").Value, 2, MidpointRounding.AwayFromZero)
            dblLineTotal_Incl = dblLineTotal_Incl + Math.Round(ugR.Cells("LineTotal_Incl").Value, 2, MidpointRounding.AwayFromZero)
        Next

        txtOrdTax.Value = Math.Round(dblOrderTax, 2, MidpointRounding.AwayFromZero)
        txtOrdAmt.Value = Math.Round(dblOrderTotal_Incl, 2, MidpointRounding.AwayFromZero)
        txtConTax.Value = Math.Round(dblLineTax, 2, MidpointRounding.AwayFromZero)
        txtConAmt.Value = Math.Round(dblLineTotal_Incl, 2, MidpointRounding.AwayFromZero)
        dblLineTotal_Incl_Dis = txtConAmt.Value - txtDisAmount.Value
        txtConAmt.Value = Math.Round(dblLineTotal_Incl_Dis, 2, MidpointRounding.AwayFromZero)
        If txtConTax.Value > 0 Then
            dblLineTax_Dis = Math.Round((dblLineTotal_Incl_Dis * 100 / 112), 2, MidpointRounding.AwayFromZero)
            dblLineTax_Dis = Math.Round(dblLineTotal_Incl_Dis - dblLineTax_Dis, 2, MidpointRounding.AwayFromZero)
            txtConTax.Value = Math.Round(dblLineTax_Dis, 2, MidpointRounding.AwayFromZero)
        End If

    End Sub
    Private Sub UG_BeforeExitEditMode(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs) Handles UG.BeforeExitEditMode
        If iDocType = DocType.PurchaseOrder Then
            For Each ugR In UG.Rows
                If UG.ActiveCell.Column.Key = "Description_1" Then
                    If ugR.Cells("StockID").Row.Index <> UG.ActiveRow.Cells("StockID").Row.Index Then
                        If ugR.Cells("StockID").Value = UG.ActiveRow.Cells("StockID").Value Then
                            MessageBox.Show("Stock Item " & ugR.Cells("Description_1").Value & " already added", "Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            UG.ActiveRow.Cells("Description_1").Value = ""
                            'UG.ActiveRow.Cells("Description_2").Value = ""
                            Exit Sub
                        End If
                    End If
                End If
            Next
        End If







        If UG.ActiveCell.Column.Key = "Quantity" Then
            With UG.ActiveCell
                .Value = .Text
                If iDocType = DocType.SalesOrder Then
                    .Row.Cells("ConfirmQty").Value = .Row.Cells("Quantity").Value
                End If
                .Row.Cells("Discount").Value = DiscountGrp(bIsInclusive, .Row.Cells("Quantity").Value, 0, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value, .Row.Cells("StockID").Value, .Value)
                .Row.Cells("DiscountA").Value = DiscountAGrp(bIsInclusive, .Row.Cells("Quantity").Value, 0, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value, .Row.Cells("StockID").Value, .Value)
                .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
            End With
        ElseIf UG.ActiveCell.Column.Key = "ConfirmQty" Then
            With UG.ActiveCell
                .Value = .Text
                If iDocType = DocType.SalesOrder Then
                    .Row.Cells("ConfirmQty").Value = .Row.Cells("Quantity").Value
                End If
                .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
            End With
        ElseIf UG.ActiveCell.Column.Key = "Price_Excl" Then
            With UG.ActiveCell
                .Value = .Text
                .Row.Cells("Price_Incl").Value = PriceIncl(.Row.Cells("TaxRate").Value, .Value)
                .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
            End With
        ElseIf UG.ActiveCell.Column.Key = "Price_Incl" Then
            With UG.ActiveCell
                .Value = .Text
                .Row.Cells("Price_Excl").Value = PriceExcl(.Row.Cells("TaxRate").Value, .Value)
                .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
            End With
        ElseIf UG.ActiveCell.Column.Key = "TaxRate" Then
            With UG.ActiveCell
                .Value = .Text
                .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
            End With
        ElseIf UG.ActiveCell.Column.Key = "Discount" Then
            With UG.ActiveCell
                .Value = .Text
                '.Row.Cells("DiscountA").Value = DiscountA(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value, .Row.Cells("StockID").Value, .Value)
                .Row.Cells("DiscountA").Value = DiscountA(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value, .Row.Cells("StockID").Value, .Value)
                If iDocType = DocType.SalesOrder Then
                    .Row.Cells("Discount").Value = DiscountPP(.Row.Cells("Discount").Value, .Row.Cells("StockID").Value)
                End If
                .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
            End With
        ElseIf UG.ActiveCell.Column.Key = "DiscountA" Then
            With UG.ActiveCell
                .Value = .Text
                .Row.Cells("Discount").Value = DiscountP(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value, .Row.Cells("StockID").Value, .Value)
                .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
                .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, .Row.Cells("Price_Excl").Value, .Row.Cells("Price_Incl").Value, .Row.Cells("TaxRate").Value)
            End With

            'ElseIf UG.ActiveCell.Column.Key = "Description_1" Then
            '    With UG.ActiveCell
            '        sSQL = " SELECT   _etblLotTracking.idLotTracking,_etblLotTracking.cLotDescription AS Description, _etblLotTracking.dExpiryDate AS [Expiry Date], _etblLotTrackingQty.fQtyOnHand AS [Qty On Hand], " & _
            '        " _etblLotTrackingQty.fQtyReserved AS [Qty Reserved]" & _
            '        " FROM         _etblLotTracking INNER JOIN " & _
            '        " _etblLotTrackingQty ON _etblLotTracking.idLotTracking = _etblLotTrackingQty.iLotTrackingID WHERE  _etblLotTracking.iStockID = " & .Row.Cells("StockID").Value & " AND _etblLotTrackingQty.fQtyOnHand > 0 "

            '        Try
            '            Con1.ConnectionString = sConStr
            '            CMD = New SqlCommand(sSQL, Con1)
            '            DS = New DataSet
            '            DA = New SqlDataAdapter(CMD)

            '            Con1.Open()
            '            DA.Fill(DS)
            '            Con1.Close()

            '            DDLot.DataSource = DS.Tables(0)
            '            DDLot.ValueMember = "idLotTracking"
            '            DDLot.DisplayMember = "Description"
            '            DDLot.DisplayLayout.Bands(0).("idColumnsLotTracking").Hidden = True

            '        Catch ex As Exception
            '            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
            '            Exit Sub
            '        Finally
            '            If Con1.State = ConnectionState.Open Then Con1.Close()
            '        End Try


            '    End With
        End If
        Get_Total()
    End Sub
    Public Function DiscountA(ByVal bInclusive As Boolean, ByVal Qty As Double, ByVal Discount As Double, ByVal PriceExcl As Double, ByVal PriceIncl As Double, ByVal TaxRate As Double, ByVal StkID As Double, ByVal DicountA As Double) As Double
        Dim dblDiscountA As Double = 0

        'Check for group discount margine
        If iDocType = DocType.SalesOrder Then

            Dim classID As Integer
            If cmbCustomer.Value <> Nothing Then
                SQL = " SELECT iClassID, Account FROM Client WHERE DCLink = " & cmbCustomer.Value & " "

                Dim objSQL As New clsSqlConn
                With objSQL
                    DS = New DataSet
                    DS = .Get_Data_Sql(SQL)

                    If DS.Tables(0).Rows.Count > 0 Then
                        'If DS.Tables(0).Rows(0)(0) = 1 Then
                        classID = DS.Tables(0).Rows(0)(0)
                        'End If
                    End If
                End With

                objSQL = Nothing


                SQL = " SELECT     DrDiscHd.Description, DrDiscMx.Percentage, DrDiscMx.YPos, DrDiscHd.Place, StkItem.StockLink, DrDiscMx.XPos " & _
                " FROM    DrDiscHd INNER JOIN " & _
                "   DrDiscMx ON DrDiscHd.Position = DrDiscMx.YPos INNER JOIN " & _
                " StkItem ON DrDiscHd.Description = StkItem.ItemGroup " & _
                " WHERE     (DrDiscHd.Place = 'Y') AND (DrDiscMx.XPos = " & IIf(classID = 1, 1, IIf(classID = 2, 2, IIf(classID = 4, 3, IIf(classID = 5, 4, IIf(classID = 6, 5, 6))))) & ") AND StkItem.StockLink = " & StkID & ""

                objSQL = New clsSqlConn
                With objSQL
                    DS = New DataSet
                    DS = .Get_Data_Sql(SQL)

                    If DS.Tables(0).Rows.Count > 0 Then
                        If DS.Tables(0).Rows(0)(1) < Discount Then
                            Discount = DS.Tables(0).Rows(0)(1)
                        End If
                    Else
                        Discount = 0
                    End If

                End With
            End If


        End If



        If bInclusive = True Then
            If Discount <> 0 Then
                dblDiscountA = Math.Round((Discount * (Qty * PriceIncl)) / 100, 6, MidpointRounding.AwayFromZero)
            End If
        ElseIf bInclusive = False Then
            If Discount <> 0 Then
                dblDiscountA = Math.Round((Discount * (Qty * PriceExcl)) / 100, 6, MidpointRounding.AwayFromZero)
            End If
        End If

        If PriceIncl < 100 Then
            dblDiscountA = 0
        End If

        Return dblDiscountA
    End Function
    Public Function DiscountP(ByVal bInclusive As Boolean, ByVal Qty As Double, ByVal Discount As Double, ByVal PriceExcl As Double, ByVal PriceIncl As Double, ByVal TaxRate As Double, ByVal StkID As Double, ByVal DicountA As Double) As Double
        Dim dblDiscount As Double = 0
        If bInclusive = True Then
            If DicountA <> 0 Then
                dblDiscount = Math.Round(DicountA / (Qty * PriceIncl) * 100, 6, MidpointRounding.AwayFromZero)
            End If
        ElseIf bInclusive = False Then
            If DicountA <> 0 Then
                dblDiscount = Math.Round(DicountA / (Qty * PriceExcl) * 100, 6, MidpointRounding.AwayFromZero)
            End If
        End If

        'Check for group discount margine
        If iDocType = DocType.SalesOrder Then

            Dim classID As Integer
            If cmbCustomer.Value <> Nothing Then
                SQL = " SELECT iClassID FROM Client WHERE DCLink = " & cmbCustomer.Value & " "

                Dim objSQL As New clsSqlConn
                With objSQL
                    DS = New DataSet
                    DS = .Get_Data_Sql(SQL)

                    If DS.Tables(0).Rows.Count > 0 Then
                        'If DS.Tables(0).Rows(0)(0) = 1 Then
                        classID = DS.Tables(0).Rows(0)(0)
                        'End If
                    End If
                End With

                objSQL = Nothing


                SQL = " SELECT     DrDiscHd.Description, DrDiscMx.Percentage, DrDiscMx.YPos, DrDiscHd.Place, StkItem.StockLink, DrDiscMx.XPos " & _
                " FROM    DrDiscHd INNER JOIN " & _
                "   DrDiscMx ON DrDiscHd.Position = DrDiscMx.YPos INNER JOIN " & _
                " StkItem ON DrDiscHd.Description = StkItem.ItemGroup " & _
                " WHERE     (DrDiscHd.Place = 'Y') AND (DrDiscMx.XPos = " & IIf(classID = 1, 1, IIf(classID = 2, 2, IIf(classID = 4, 3, IIf(classID = 5, 4, IIf(classID = 6, 5, 6))))) & ") AND StkItem.StockLink = " & StkID & ""

                objSQL = New clsSqlConn
                With objSQL
                    DS = New DataSet
                    DS = .Get_Data_Sql(SQL)

                    If DS.Tables(0).Rows.Count > 0 Then
                        If DS.Tables(0).Rows(0)(1) < dblDiscount Then
                            dblDiscount = DS.Tables(0).Rows(0)(1)
                        End If
                    Else
                        dblDiscount = 0
                    End If

                End With
            End If
        End If

        If PriceIncl < 100 Then
            dblDiscount = 0
        End If
        Return dblDiscount
    End Function

    Public Function DiscountPP(ByVal Discount As Double, ByVal StkID As Double) As Double
        Dim dblDiscount As Double = 0


        'Check for group discount margine
        If iDocType = DocType.SalesOrder Then

            Dim classID As Integer
            If cmbCustomer.Value <> Nothing Then
                SQL = " SELECT iClassID FROM Client WHERE DCLink = " & cmbCustomer.Value & " "

                Dim objSQL As New clsSqlConn
                With objSQL
                    DS = New DataSet
                    DS = .Get_Data_Sql(SQL)

                    If DS.Tables(0).Rows.Count > 0 Then
                        'If DS.Tables(0).Rows(0)(0) = 1 Then
                        classID = DS.Tables(0).Rows(0)(0)
                        'End If
                    End If
                End With

                objSQL = Nothing


                SQL = " SELECT     DrDiscHd.Description, DrDiscMx.Percentage, DrDiscMx.YPos, DrDiscHd.Place, StkItem.StockLink, DrDiscMx.XPos " & _
                " FROM    DrDiscHd INNER JOIN " & _
                "   DrDiscMx ON DrDiscHd.Position = DrDiscMx.YPos INNER JOIN " & _
                " StkItem ON DrDiscHd.Description = StkItem.ItemGroup " & _
                " WHERE     (DrDiscHd.Place = 'Y') AND (DrDiscMx.XPos = " & IIf(classID = 1, 1, IIf(classID = 2, 2, IIf(classID = 4, 3, IIf(classID = 5, 4, IIf(classID = 6, 5, 6))))) & ") AND StkItem.StockLink = " & StkID & ""

                objSQL = New clsSqlConn
                With objSQL
                    DS = New DataSet
                    DS = .Get_Data_Sql(SQL)

                    If DS.Tables(0).Rows.Count > 0 Then
                        If DS.Tables(0).Rows(0)(1) < Discount Then
                            dblDiscount = DS.Tables(0).Rows(0)(1)
                        Else
                            dblDiscount = Discount
                        End If
                    Else
                        dblDiscount = 0
                    End If

                End With
            End If
        End If

        'If PriceIncl() < 100 Then
        '    dblDiscount = 0
        'End If
        Return dblDiscount
    End Function
    Public Function DiscountGrp(ByVal bInclusive As Boolean, ByVal Qty As Double, ByVal Discount As Double, ByVal PriceExcl As Double, ByVal PriceIncl As Double, ByVal TaxRate As Double, ByVal StkID As Double, ByVal DicountA As Double) As Double
        Dim dblDiscount As Double = 0


        'Check for group discount margine
        If iDocType = DocType.SalesOrder Then

            Dim classID As Integer
            If cmbCustomer.Value <> Nothing Then
                SQL = " SELECT iClassID FROM Client WHERE DCLink = " & cmbCustomer.Value & " "

                Dim objSQL As New clsSqlConn
                With objSQL
                    DS = New DataSet
                    DS = .Get_Data_Sql(SQL)

                    If DS.Tables(0).Rows.Count > 0 Then
                        'If DS.Tables(0).Rows(0)(0) = 1 Then
                        classID = DS.Tables(0).Rows(0)(0)
                        'End If
                    End If
                End With

                objSQL = Nothing


                SQL = " SELECT     DrDiscHd.Description, DrDiscMx.Percentage, DrDiscMx.YPos, DrDiscHd.Place, StkItem.StockLink, DrDiscMx.XPos " & _
                " FROM    DrDiscHd INNER JOIN " & _
                "   DrDiscMx ON DrDiscHd.Position = DrDiscMx.YPos INNER JOIN " & _
                " StkItem ON DrDiscHd.Description = StkItem.ItemGroup " & _
                " WHERE     (DrDiscHd.Place = 'Y') AND (DrDiscMx.XPos = " & IIf(classID = 1, 1, IIf(classID = 2, 2, IIf(classID = 4, 3, IIf(classID = 5, 4, IIf(classID = 6, 5, 6))))) & ") AND StkItem.StockLink = " & StkID & ""

                objSQL = New clsSqlConn
                With objSQL
                    DS = New DataSet
                    DS = .Get_Data_Sql(SQL)

                    If DS.Tables(0).Rows.Count > 0 Then
                        'If iCusGroup <> 1 And iCusGroup <> 6 Then
                        dblDiscount = DS.Tables(0).Rows(0)(1)
                        'Else
                        '    dblDiscount = Discount
                        'End If
                    Else
                        dblDiscount = 0
                    End If

                End With
            End If
        End If

        If bInclusive = True Then
            If Discount <> 0 Then
                dblDiscount = Math.Round((Discount * (Qty * PriceIncl)) / 100, 6, MidpointRounding.AwayFromZero)
            End If
        ElseIf bInclusive = False Then
            If Discount <> 0 Then
                dblDiscount = Math.Round((Discount * (Qty * PriceExcl)) / 100, 6, MidpointRounding.AwayFromZero)
            End If
        End If

        If PriceIncl < 100 Then
            dblDiscount = 0
        End If

        Return dblDiscount
    End Function
    Public Function DiscountAGrp(ByVal bInclusive As Boolean, ByVal Qty As Double, ByVal Discount As Double, ByVal PriceExcl As Double, ByVal PriceIncl As Double, ByVal TaxRate As Double, ByVal StkID As Double, ByVal DicountA As Double) As Double
        Dim dblDiscountA As Double = 0

        'Check for group discount margine
        If iDocType = DocType.SalesOrder Then

            Dim classID As Integer
            If cmbCustomer.Value <> Nothing Then
                SQL = " SELECT iClassID, Account FROM Client WHERE DCLink = " & cmbCustomer.Value & " "

                Dim objSQL As New clsSqlConn
                With objSQL
                    DS = New DataSet
                    DS = .Get_Data_Sql(SQL)

                    If DS.Tables(0).Rows.Count > 0 Then
                        'If DS.Tables(0).Rows(0)(0) = 1 Then
                        classID = DS.Tables(0).Rows(0)(0)
                        'End If
                    End If
                End With

                objSQL = Nothing


                SQL = " SELECT     DrDiscHd.Description, DrDiscMx.Percentage, DrDiscMx.YPos, DrDiscHd.Place, StkItem.StockLink, DrDiscMx.XPos " & _
                " FROM    DrDiscHd INNER JOIN " & _
                "   DrDiscMx ON DrDiscHd.Position = DrDiscMx.YPos INNER JOIN " & _
                " StkItem ON DrDiscHd.Description = StkItem.ItemGroup " & _
                " WHERE     (DrDiscHd.Place = 'Y') AND (DrDiscMx.XPos = " & IIf(classID = 1, 1, IIf(classID = 2, 2, IIf(classID = 4, 3, IIf(classID = 5, 4, IIf(classID = 6, 5, 6))))) & ") AND StkItem.StockLink = " & StkID & ""

                objSQL = New clsSqlConn
                With objSQL
                    DS = New DataSet
                    DS = .Get_Data_Sql(SQL)

                    If DS.Tables(0).Rows.Count > 0 Then
                        'If iCusGroup <> 1 And iCusGroup <> 6 Then
                        Discount = DS.Tables(0).Rows(0)(1)
                        'End If
                    Else
                        Discount = 0
                    End If

                End With
            End If


        End If



        If bInclusive = True Then
            If Discount <> 0 Then
                dblDiscountA = Math.Round((Discount * (Qty * PriceIncl)) / 100, 6, MidpointRounding.AwayFromZero)
            End If
        ElseIf bInclusive = False Then
            If Discount <> 0 Then
                dblDiscountA = Math.Round((Discount * (Qty * PriceExcl)) / 100, 6, MidpointRounding.AwayFromZero)
            End If
        End If

        If PriceIncl < 100 Then
            dblDiscountA = 0
        End If
        Return dblDiscountA
    End Function

    Private Sub txtDisAmount_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDisAmount.Leave
        Dim ugR As UltraGridRow
        Dim dblOrderTax As Double = 0
        Dim dblLineTax As Double = 0
        Dim dblOrderTotal_Incl As Double = 0
        Dim dblLineTotal_Incl As Double = 0
        Dim dblLineTotal_Incl_Dis As Double = 0
        Dim dblLineTax_Dis As Double = 0
        For Each ugR In UG.Rows
            dblOrderTax = dblOrderTax + ugR.Cells("OrderTax").Value
            dblOrderTotal_Incl = dblOrderTotal_Incl + ugR.Cells("OrderTotal_Incl").Value
            dblLineTax = dblLineTax + ugR.Cells("LineTax").Value
            dblLineTotal_Incl = dblLineTotal_Incl + ugR.Cells("LineTotal_Incl").Value
        Next

        txtOrdTax.Value = Math.Round(dblOrderTax, 2, MidpointRounding.AwayFromZero)
        txtOrdAmt.Value = Math.Round(dblOrderTotal_Incl, 2, MidpointRounding.AwayFromZero)
        txtConTax.Value = Math.Round(dblLineTax, 2, MidpointRounding.AwayFromZero)
        txtConAmt.Value = Math.Round(dblLineTotal_Incl, 2, MidpointRounding.AwayFromZero)
        txtDisPer.Value = Math.Round(txtDisAmount.Value / dblLineTotal_Incl * 100, 6, MidpointRounding.AwayFromZero)
        dblLineTotal_Incl_Dis = txtConAmt.Value - txtDisAmount.Value
        txtConAmt.Value = Math.Round(dblLineTotal_Incl_Dis, 2, MidpointRounding.AwayFromZero)
        If txtConTax.Value > 0 Then
            dblLineTax_Dis = Math.Round((dblLineTotal_Incl_Dis * 100 / 112), 2, MidpointRounding.AwayFromZero)
            dblLineTax_Dis = Math.Round(dblLineTotal_Incl_Dis - dblLineTax_Dis, 2, MidpointRounding.AwayFromZero)
            txtConTax.Value = Math.Round(dblLineTax_Dis, 2, MidpointRounding.AwayFromZero)
        End If
    End Sub

    Private Sub txtDisPer_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDisPer.Leave
        Dim ugR As UltraGridRow
        Dim dblOrderTax As Double = 0
        Dim dblLineTax As Double = 0
        Dim dblOrderTotal_Incl As Double = 0
        Dim dblLineTotal_Incl As Double = 0
        Dim dblLineTotal_Incl_Dis As Double = 0
        Dim dblLineTax_Dis As Double = 0
        For Each ugR In UG.Rows
            dblOrderTax = dblOrderTax + Math.Round(ugR.Cells("OrderTax").Value, 2, MidpointRounding.AwayFromZero)
            dblOrderTotal_Incl = dblOrderTotal_Incl + Math.Round(ugR.Cells("OrderTotal_Incl").Value, 2, MidpointRounding.AwayFromZero)
            dblLineTax = dblLineTax + Math.Round(ugR.Cells("LineTax").Value, 2, MidpointRounding.AwayFromZero)
            dblLineTotal_Incl = dblLineTotal_Incl + Math.Round(ugR.Cells("LineTotal_Incl").Value, 2, MidpointRounding.AwayFromZero)
        Next

        txtOrdTax.Value = Math.Round(dblOrderTax, 2, MidpointRounding.AwayFromZero)
        txtOrdAmt.Value = Math.Round(dblOrderTotal_Incl, 2, MidpointRounding.AwayFromZero)
        txtConTax.Value = Math.Round(dblLineTax, 2, MidpointRounding.AwayFromZero)
        txtConAmt.Value = Math.Round(dblLineTotal_Incl, 2, MidpointRounding.AwayFromZero)
        txtDisAmount.Value = Math.Round(dblLineTotal_Incl * txtDisPer.Value / 100, 2, MidpointRounding.AwayFromZero)
        dblLineTotal_Incl_Dis = txtConAmt.Value - txtDisAmount.Value
        txtConAmt.Value = Math.Round(dblLineTotal_Incl_Dis, 2, MidpointRounding.AwayFromZero)
        If txtConTax.Value > 0 Then
            dblLineTax_Dis = Math.Round((dblLineTotal_Incl_Dis * 100 / 112), 2, MidpointRounding.AwayFromZero)
            dblLineTax_Dis = Math.Round(dblLineTotal_Incl_Dis - dblLineTax_Dis, 2, MidpointRounding.AwayFromZero)
            txtConTax.Value = Math.Round(dblLineTax_Dis, 2, MidpointRounding.AwayFromZero)
        End If


    End Sub





    Private Sub cmbSaleRep_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbSaleRep.KeyDown
        If e.KeyData = Keys.F9 Then
            If iDocType = DocType.SalesOrder Then
                tsbSaveSO_Click(tsbSaveSO, e)
            End If
        End If
    End Sub

    Private Sub txtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDescription.KeyDown
        If e.KeyData = Keys.F9 Then
            If iDocType = DocType.SalesOrder Then
                tsbSaveSO_Click(tsbSaveSO, e)
            End If
        End If
    End Sub

    Private Sub txtExtOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtExtOrder.KeyDown
        If e.KeyData = Keys.F9 Then
            If iDocType = DocType.SalesOrder Then
                tsbSaveSO_Click(tsbSaveSO, e)
            End If
        End If
    End Sub


    Sub MemorizeStock()
        Dim ugr As UltraGridRow
        'Dim intStockID As Integer
        'Dim sn As String
        Dim objSQL As New clsSqlConn
        With objSQL

            Try

                .Begin_Trans()

                SQL = "delete FROM sbSerialMF WHERE AutoIndex =" & CInt(iPONo)
                If .Execute_Sql_Trans(SQL) = 0 Then
                    .Rollback_Trans()
                    Exit Sub
                End If

                For Each ugr In UG.Rows

                    If ugr.Cells("IsLot").Value = True Then
                        If ugr.ChildBands.HasChildRows = True Then
                            Dim ugR2 As UltraGridRow
                            For Each ugR2 In ugr.ChildBands(0).Rows
                                SQL = "INSERT INTO sbSerialMF(AutoIndex, SNStockLink, SerialNumber, " & _
                                    " PrimaryLineID) VALUES (" & CInt(iPONo) & "," & CInt(ugr.Cells("StockID").Value) & ", " & _
                                    " '" & CStr(ugR2.Cells("SerialNumber").Value) & "' , " & ugR2.ParentRow.Cells("Line").Value & " )"
                                If .Execute_Sql_Trans(SQL) = 0 Then
                                    .Rollback_Trans()
                                    Exit Sub
                                End If
                            Next
                        End If
                    End If
                Next

                .Commit_Trans()

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                '.Rollback_Trans()
            Finally

                objSQL.Con_Close()
                GC.Collect()
            End Try

        End With

    End Sub


    Private Sub UG_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UG.InitializeLayout

    End Sub

    Private Sub tsbSaveStock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSaveStock.Click
        Try
            If iDocType = DocType.SalesOrder Then
                UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "SO.xml")
            ElseIf iDocType = DocType.PurchaseOrder Then
                UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "PO.xml")
                UG.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.ExternalSortSingle
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub UG_MarginChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles UG.MarginChanged

    End Sub

    Private Sub UG_SelectionDrag(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)

    End Sub

    Private Sub cmbSaleRep_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles cmbSaleRep.InitializeLayout

    End Sub

    Private Sub cmbCustomer_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles cmbCustomer.InitializeLayout

    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbKelaniya.Click


        'frmStockCount.Show()

        SavePO()
        'If MessageBox.Show("Do you want to delete unprocessed PO from dbUdawatta_new", "Pastel Evolution AddOn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
        '    If txtOrdNo.Text.Length > 0 Then
        '        Dim PO As PurchaseOrder = New PurchaseOrder(txtOrdNo.Text)
        '        PO.Delete()
        '    End If
        'End If
    End Sub


    Private Sub DDDescription_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles DDDescription.InitializeLayout

    End Sub

    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

    Private Sub cmbSaleRep_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSaleRep.Leave

        'SQL = " SELECT idSalesRep, Code, Name FROM SalesRep Order By Name where Name =  " & cmbSaleRep.Text & ""

        'Dim objSQL As New clsSqlConn
        'With objSQL
        '    DS = New DataSet
        '    DS = .Get_Data_Sql(SQL)

        '    cmbSaleRep.Value = DS.Tables(0).Rows(0)(2)

        'End With

    End Sub

    Private Sub UltraTabControl2_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl2.SelectedTabChanged

    End Sub

    Private Sub lblBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblBarcode.Click

    End Sub

    Private Sub DDLot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDLot.Click
        If DDLot.ActiveRow.Activated = True Then
            If UG.ActiveRow.Cells("iUnit").Value = 0 Then 'if not a UOM item
                If UG.ActiveRow.Cells("Quantity").Value > DDLot.ActiveRow.Cells("Qty On Hand").Value Then
                    MsgBox("Quantity not available in Lot", MsgBoxStyle.Information, "Pastel Evolution AddOn")
                    UG.ActiveRow.Cells("Quantity").Value = DDLot.ActiveRow.Cells("Qty On Hand").Value
                    UG.ActiveRow.Cells("Lot").Value = 0
                End If
                Lot = DDLot.ActiveRow.Cells("Description").Value
                Lot_Exp = DDLot.ActiveRow.Cells("Expiry Date").Value
            End If
        End If
    End Sub

    Private Sub UG_AfterEnterEditMode(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UG.AfterEnterEditMode
        If UG.ActiveCell.Column.Key = "Lot" Then
            With UG.ActiveCell
                sSQL = " SELECT   _etblLotTracking.idLotTracking,_etblLotTracking.cLotDescription AS Description, _etblLotTracking.dExpiryDate AS [Expiry Date], _etblLotTrackingQty.fQtyOnHand AS [Qty On Hand], " & _
                " _etblLotTrackingQty.fQtyReserved AS [Qty Reserved]" & _
                " FROM         _etblLotTracking INNER JOIN " & _
                " _etblLotTrackingQty ON _etblLotTracking.idLotTracking = _etblLotTrackingQty.iLotTrackingID WHERE  _etblLotTracking.iStockID = " & UG.ActiveRow.Cells("StockID").Value & " AND _etblLotTrackingQty.fQtyOnHand > 0 AND _etblLotTrackingQty.iWarehouseID =  " & UG.ActiveRow.Cells("warehouse").Value & " "

                Try
                    Con1.ConnectionString = sConStr
                    CMD = New SqlCommand(sSQL, Con1)
                    DS = New DataSet
                    DA = New SqlDataAdapter(CMD)

                    Con1.Open()
                    DA.Fill(DS)
                    Con1.Close()

                    DDLot.DataSource = DS.Tables(0)
                    DDLot.ValueMember = "idLotTracking"
                    DDLot.DisplayMember = "Description"
                    DDLot.DisplayLayout.Bands(0).Columns("idLotTracking").Hidden = True

                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                    Exit Sub
                Finally
                    If Con1.State = ConnectionState.Open Then Con1.Close()
                End Try


            End With
        ElseIf UG.ActiveCell.Column.Key = "UOM" Then
            With UG.ActiveCell

                'With conversion-----------------------------------------------------------------------------------------
                'sSQL = "   SELECT DISTINCT _etblUnits.idUnits, _etblUnits.cUnitCode, _etblUnits.cUnitDescription " & _
                '        " FROM         _etblUnits CROSS JOIN    _etblUnitConversion " & _
                '        " where _etblUnits.iUnitCategoryID IN  " & _
                '        " (SELECT DISTINCT  _etblUnitCategory.idUnitCategory " & _
                '        " FROM         StkItem INNER JOIN " & _
                '        "    _etblUnits ON StkItem.iUOMStockingUnitID = _etblUnits.idUnits INNER JOIN " & _
                '        "   _etblUnitCategory ON _etblUnits.iUnitCategoryID = _etblUnitCategory.idUnitCategory CROSS JOIN " & _
                '        "   _etblUnitConversion " & _
                '        " WHERE StkItem.StockLink = " & UG.ActiveRow.Cells("StockID").Value & ")  "

                sSQL = " SELECT DISTINCT _etblUnits.idUnits, _etblUnits.cUnitCode, _etblUnits.cUnitDescription " & _
                " FROM         _etblUnits INNER JOIN " & _
                " StkItem ON _etblUnits.idUnits = StkItem.iUOMDefPurchaseUnitID CROSS JOIN " & _
                "  _etblUnitConversion   WHERE StkItem.StockLink = " & UG.ActiveRow.Cells("StockID").Value & "  "

                Try
                    Con1.ConnectionString = sConStr
                    CMD = New SqlCommand(sSQL, Con1)
                    DS = New DataSet
                    DA = New SqlDataAdapter(CMD)

                    Con1.Open()
                    DA.Fill(DS)
                    Con1.Close()

                    DDUnit.DataSource = DS.Tables(0)
                    DDUnit.ValueMember = "idUnits"
                    DDUnit.DisplayMember = "cUnitCode"
                    DDUnit.DisplayLayout.Bands(0).Columns("idUnits").Hidden = True

                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                    Exit Sub
                Finally
                    If Con1.State = ConnectionState.Open Then Con1.Close()
                End Try


            End With
        End If
    End Sub

    Private Sub DDLot_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles DDLot.InitializeLayout

    End Sub

    Private Sub frmSalesOrder_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove

    End Sub

    Private Sub DDUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDUnit.Click
        If DDUnit.ActiveRow.Activated = True Then
            'If UG.ActiveRow.Cells("Quantity").Value > DDLot.ActiveRow.Cells("Qty On Hand").Value Then





            sSQL = " SELECT iUnitAID, fUnitAQty, iUnitBID, fUnitBQty, fMarkup FROM _etblUnitConversion " & _
                    " WHERE (iUnitAID = " & DDUnit.ActiveRow.Cells("idUnits").Value & " or iUnitBID = " & UG.ActiveRow.Cells("iUnit").Value & ") OR " & _
                    " (iUnitAID = " & UG.ActiveRow.Cells("iUnit").Value & " OR iUnitBID = " & DDUnit.ActiveRow.Cells("idUnits").Value & ") "

            Try
                Con1.ConnectionString = sConStr
                CMD = New SqlCommand(sSQL, Con1)
                DS = New DataSet
                DA = New SqlDataAdapter(CMD)

                Con1.Open()
                DA.Fill(DS)
                Con1.Close()

                Dim UEPrice As Double
                Dim UIPrice As Double



                If DS.Tables(0).Rows.Count > 0 Then
                    If UG.ActiveRow.Cells("iUnit").Value = DS.Tables(0).Rows(0)(0) Then
                        UEPrice = (DS.Tables(0).Rows(0)(1) / DS.Tables(0).Rows(0)(3) * OEPrice)
                        UIPrice = (DS.Tables(0).Rows(0)(1) / DS.Tables(0).Rows(0)(3) * OIPrice)
                        'UG.ActiveCell.Row.Cells("AvailableQty").Value = UG.ActiveCell.Row.Cells("AvailableQty").Value * DS.Tables(0).Rows(0)(3)
                        UEPrice = UEPrice * DS.Tables(0).Rows(0)(4) / 100 + UEPrice
                        UIPrice = UIPrice * DS.Tables(0).Rows(0)(4) / 100 + UIPrice
                        convertion = DS.Tables(0).Rows(0)(3)
                    ElseIf UG.ActiveRow.Cells("iUnit").Value = DS.Tables(0).Rows(0)(2) Then
                        UEPrice = (DS.Tables(0).Rows(0)(3) / DS.Tables(0).Rows(0)(1) * OEPrice)
                        UIPrice = (DS.Tables(0).Rows(0)(3) / DS.Tables(0).Rows(0)(1) * OIPrice)
                        'UG.ActiveCell.Row.Cells("AvailableQty").Value = UG.ActiveCell.Row.Cells("AvailableQty").Value * DS.Tables(0).Rows(0)(1)
                        UEPrice = UEPrice * DS.Tables(0).Rows(0)(4) / 100 + UEPrice
                        UIPrice = UIPrice * DS.Tables(0).Rows(0)(4) / 100 + UIPrice
                        convertion = DS.Tables(0).Rows(0)(1)
                    End If
                    'UG.ActiveRow.Cells("Price_Excl").Value = DS.Tables(0).Rows(0)(1) / DS.Tables(0).Rows(0)(0) * UG.ActiveRow.Cells("Price_Excl").Value
                    With UG.ActiveCell
                        .Value = .Text

                        .Row.Cells("Price_Excl").Value = UEPrice
                        .Row.Cells("Price_Incl").Value = UIPrice
                        .Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, UEPrice, UIPrice, .Row.Cells("TaxRate").Value)
                        .Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, UEPrice, UIPrice, .Row.Cells("TaxRate").Value)
                        .Row.Cells("OrderTax").Value = LineTax(bIsInclusive, .Row.Cells("Quantity").Value, .Row.Cells("Discount").Value, UEPrice, UIPrice, .Row.Cells("TaxRate").Value)
                        .Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, UEPrice, UIPrice, .Row.Cells("TaxRate").Value)
                        .Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, UEPrice, UIPrice, .Row.Cells("TaxRate").Value)
                        .Row.Cells("LineTax").Value = LineTax(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, UEPrice, UIPrice, .Row.Cells("TaxRate").Value)
                        .Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, UEPrice, UIPrice, .Row.Cells("TaxRate").Value)
                        .Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Row.Cells("ConfirmQty").Value, .Row.Cells("Discount").Value, UEPrice, UIPrice, .Row.Cells("TaxRate").Value)
                    End With

                Else
                    MessageBox.Show("No Conversion exists between these Units of Measure!", "Pastel Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    UG.ActiveRow.Cells("UOM").Value = DDUnit.ActiveRow.Cells("idUnits").Value
                    Exit Sub
                End If

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
                Exit Sub
            Finally
                If Con1.State = ConnectionState.Open Then Con1.Close()
            End Try


            'UG.ActiveRow.Cells("StockID").Value = DDStock.ActiveRow.Cells("StockLink").Value


            'MsgBox("Quantity not available in Lot", MsgBoxStyle.Information, "Pastel Evolution AddOn")
            'UG.ActiveRow.Cells("Quantity").Value = DDLot.ActiveRow.Cells("Qty On Hand").Value
            ''End If
            'Lot = DDLot.ActiveRow.Cells("Description").Value
            'Lot_Exp = DDLot.ActiveRow.Cells("Expiry Date").Value
        End If
    End Sub

    Private Sub UG_AfterColPosChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.AfterColPosChangedEventArgs) Handles UG.AfterColPosChanged

    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Dim bVAT As Boolean

        Delete_Row()

        UG.PerformAction(UltraGridAction.CommitRow)

        If cmbCustomer.Value = Nothing Then
            MsgBox("Customer can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If cmbCustomer.Text.Trim.Length = 0 Then
            MsgBox("Customer can not be left balnk", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If UG.Rows.Count = 0 Then
            MsgBox("Please enter item details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        Dim ugR As UltraGridRow
        For Each ugR In UG.Rows
            If ugR.Cells("IsWH").Value = True Then
                If ugR.Cells("Warehouse").Value = Nothing Then
                    MsgBox("Warehouse can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    Exit Sub
                End If
            End If
            If ugR.Cells("Quantity").Value = 0 Then
                MsgBox("Quantity can not zero", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End If
            If ugR.Cells("ConfirmQty").Value = 0 Then
                MsgBox("Confirm Quantity can not be zero", MsgBoxStyle.Exclamation, "Pastel Evolution")
                Exit Sub
            End If
        Next

        If cmbSaleRep.Value = Nothing Then
            MsgBox("Select Sales Person", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)

        Try
            Dim SalesOrder
            DatabaseContext.BeginTran()
            If iPONo = 0 Then
                SalesOrder = New SalesOrder
            Else
                SalesOrder = New SalesOrder(iPONo)
            End If

            Dim SalesOrderDetail As OrderDetail
            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse

            Dim Customer As New Customer(CInt(cmbCustomer.Value))
            Dim Representative As New SalesRepresentative(CInt(cmbSaleRep.Value))

            If Customer.TaxNumber <> "" Then
                bVAT = True
            Else
                bVAT = False
            End If

            SalesOrder.Account = Customer
            SalesOrder.InvoiceTo = Customer.PostalAddress
            SalesOrder.DeliverTo = Customer.PhysicalAddress
            SalesOrder.ExternalOrderNo = txtExtOrder.Text.ToString
            SalesOrder.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            SalesOrder.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            SalesOrder.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
            SalesOrder.Representative = Representative
            SalesOrder.DiscountPercent = txtDisPer.Value


            If bIsInclusive = True Then
                SalesOrder.TaxMode = TaxMode.Inclusive
            ElseIf bIsInclusive = False Then
                SalesOrder.TaxMode = TaxMode.Exclusive
            End If


            Dim bLoop As Boolean = False

            Try

lbl1:           For Each SalesOrderDetail In SalesOrder.Detail
                    bLoop = False
                    SalesOrder.Detail.Remove(SalesOrderDetail)
                    bLoop = True
                    Exit For
                Next
                If bLoop Then
                    bLoop = False
                    GoTo lbl1
                End If

            Catch ex As Exception

            Finally

                bLoop = False
            End Try


            For Each ugR In UG.Rows
                SalesOrderDetail = New OrderDetail
                InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))

                SalesOrderDetail.InventoryItem = InventoryItem
                SalesOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                SalesOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)
                If bIsInclusive = True Then
                    SalesOrderDetail.TaxMode = TaxMode.Inclusive
                    SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                    SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                ElseIf bIsInclusive = False Then
                    SalesOrderDetail.TaxMode = TaxMode.Exclusive
                    SalesOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                    SalesOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                End If
                If ugR.Cells("IsWH").Value = True Then
                    WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                    If ugR.Cells("IsWH").Value = True Then
                        SalesOrderDetail.Warehouse = WH
                    End If
                End If
                SalesOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)
                'SalesOrderDetail.Reserved = CDbl(ugR.Cells("Quantity").Value)
                'SalesOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                SalesOrderDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)

                'Dim LotN As New Lot()

                'LotN.Code = CStr(ugR.Cells("Lot").Text)
                ''LotN. = CStr(ugR.Cells("Lot").Text)
                'SalesOrderDetail.Lot = LotN

                'correct way to save lot..but will process two lines when processed
                'SalesOrderDetail.LotID = CStr(ugR.Cells("Lot").Value)


                SalesOrder.Detail.Add(SalesOrderDetail)
            Next
            SalesOrder.Save()
            Dim iSalesOrder As Integer = SalesOrder.ID
            MsgBox(SalesOrder.OrderNo, MsgBoxStyle.Information, "Pastel Evolution")
            DatabaseContext.CommitTran()
            If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                Dim ps As New PrinterSettings
                Dim objRep As New ReportDocument

                If bVAT Then
                    objRep.Load(Application.StartupPath & "\SalesOrder_VAT")
                Else
                    objRep.Load(Application.StartupPath & "\SalesOrder.rpt")
                End If

                objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSalesOrder & ""
                ApplyLoginToTable(objRep)
                'objRep.SetDatabaseLogon(sSQLSrvUserName, sSQLSrvPassword, sSQLSrvReportSrv, sSQLSrvDataBase)
                Dim objCRV As New frmPrintPreview
                With objCRV
                    .CRV.ReportSource = objRep
                    .Text = "Sales Order Printing"
                    .ShowDialog()
                End With
            End If
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
            Discard()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Pastel Evolution")
            DatabaseContext.RollbackTran()
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        Finally
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
        End Try
    End Sub

    Private Sub UG_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles UG.KeyUp
        ''Dim grid As UltraGrid

        ''If e.KeyCode = Keys.Down Then

        ''    'Go down one row
        ''    UG.PerformAction(UltraGridAction.BelowCell)
        ''End If

        'Select (e.KeyCode)

        '    Case Keys.Up
        '        UG.PerformAction(UltraGridAction.ExitEditMode, False, False)
        '        UG.PerformAction(UltraGridAction.AboveRow, False, False)
        '        e.Handled = True
        '        UG.ActiveRow.Cells("Code").Activate()


        '        UG.PerformAction(UltraGridAction.BelowCell, False, False)
        '        UG.PerformAction(UltraGridAction.EnterEditMode, False, False)
        '        Exit Select
        '    Case Keys.Down

        '        UG.PerformAction(UltraGridAction.ExitEditMode, False, False)
        '        UG.PerformAction(UltraGridAction.BelowRow, False, False)
        '        'e.Handled = True
        '        'UG.PerformAction(UltraGridAction.EnterEditMode, False, False)
        '        UG.ActiveRow.Cells("Code").Activate()
        '        'UG.ActiveRow.Cells("Code").Value = ""
        '        'UG.ActiveRow.Cells("Description_1").
        '        UG.PerformAction(UltraGridAction.BelowCell, False, False)
        '        UG.PerformAction(UltraGridAction.EnterEditMode, False, False)

        '        'nwe line code---------------------------------------------------------------------
        '        'Dim ugR As UltraGridRow
        '        'If UG.DisplayLayout.Rows(UG.ActiveRow.Index + 1) = Nothing Then

        '        'End If
        '        'ugR = UG.DisplayLayout.Bands(0).AddNew
        '        'ugR.Cells("Line").Value = ugR.Index + 1
        '        'ugR.Cells("Line").Selected = True
        '        ''ugR.Cells("islot").Value = 0
        '        'ugR.Cells("Warehouse").Value = iWH
        '        'If iDocType = DocType.SalesOrder Then
        '        '    UG.DisplayLayout.Bands(0).Columns("Warehouse").CellActivation = Activation.Disabled
        '        '    ugR.Cells("TaxType").Value = 1
        '        '    For Each ugR1 As UltraGridRow In DDTaxType.Rows
        '        '        If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
        '        '            ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
        '        '        End If
        '        '    Next
        '        'ElseIf iDocType = DocType.PurchaseOrder Then
        '        '    ugR.Cells("TaxType").Value = 3
        '        '    For Each ugR1 As UltraGridRow In DDTaxType.Rows
        '        '        If ugR.Cells("TaxType").Value = ugR1.Cells("Code").Value Then
        '        '            ugR.Cells("TaxRate").Value = ugR1.Cells("TaxRate").Value
        '        '        End If
        '        '    Next
        '        'End If
        '        '---------------------------------------------------------------------------------

        '        Exit Select
        '    Case Keys.Right
        '        UG.PerformAction(UltraGridAction.ExitEditMode, False, False)
        '        UG.PerformAction(UltraGridAction.NextCellByTab, False, False)
        '        e.Handled = True
        '        UG.PerformAction(UltraGridAction.EnterEditMode, False, False)
        '        Exit Select
        '    Case Keys.Left
        '        UG.PerformAction(UltraGridAction.ExitEditMode, False, False)
        '        UG.PerformAction(UltraGridAction.PrevCellByTab, False, False)
        '        e.Handled = True
        '        UG.PerformAction(UltraGridAction.EnterEditMode, False, False)
        '        Exit Select
        'End Select
    End Sub

    Private Sub txtPAdd4_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPAdd4.ValueChanged

    End Sub
    Private Sub SavePO()

        Dim iSupplier As Integer = 0

        Try
            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse

            'to find the mother database
            'Dim MDB As String = ""
            'If sSQLSrvDataBase = "dbNawinna_new" Then
            '    MDB = "UDA004"
            'ElseIf sSQLSrvDataBase = "dbKurunegala_new" Then
            '    MDB = "UDA002"
            'ElseIf sSQLSrvDataBase = "dbKiribathgoda_new" Then
            '    MDB = "UDA003"
            'ElseIf sSQLSrvDataBase = "dbUdawatta_new" Then
            '    MDB = "UDA001"
            'ElseIf sSQLSrvDataBase = "dbKelaniya_new" Then
            '    MDB = "UDA008"
            'End If

            'If sSQLSrvDataBase = "dbUdawatta_test1" Then
            '    MDB = "UDA001"
            'ElseIf sSQLSrvDataBase = "UDAWATTA_TEST2" Then
            '    MDB = "UDA002"
            '    'ElseIf sSQLSrvDataBase = "dbKiribathgoda_test" Then
            '    '    MDB = "UDA003"
            '    'ElseIf sSQLSrvDataBase = "UDAWATTA_TEST1" Then
            '    '    MDB = "UDA001"
            'End If


            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

            DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
            DatabaseContext.SetLicense("DE09110022", "1428511")
            DatabaseContext.CreateConnection(sSQLSrvName, "dbKelaniya_new", sSQLSrvUserName, sSQLSrvPassword, False)
            'DatabaseContext.CreateConnection(sSQLSrvName, "UDAWATTA-TEST1", sSQLSrvUserName, sSQLSrvPassword, False)

            DatabaseContext.BeginTran()
            Dim PurchaseOrder As New PurchaseOrder
            Dim PurchaseOrderDetail As OrderDetail

            Dim Supplier As New Supplier(cmbCustomer.Text)
            iSupplier = Supplier.ID
            'PurchaseOrder.OrderNo = sOrderNumber
            PurchaseOrder.ExternalOrderNo = PurchaseOrder.OrderNo
            PurchaseOrder.Account = Supplier
            PurchaseOrder.InvoiceTo = Supplier.PostalAddress
            PurchaseOrder.DeliverTo = Supplier.PhysicalAddress
            PurchaseOrder.ExternalOrderNo = txtDelNote_SupInvNo.Text.ToString
            PurchaseOrder.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            PurchaseOrder.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            PurchaseOrder.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
            PurchaseOrder.SupplierInvoiceNo = "" ' CStr(txtInNo.Text)
            If bIsInclusive = False Then
                PurchaseOrder.TaxMode = TaxMode.Exclusive
            ElseIf bIsInclusive = True Then
                PurchaseOrder.TaxMode = TaxMode.Inclusive
            End If

            Dim LotN As New Lot()
            For Each ugR In UG.Rows
                PurchaseOrderDetail = New OrderDetail
                InventoryItem = New InventoryItem(CStr(ugR.Cells("Code").Text))
                PurchaseOrderDetail.InventoryItem = InventoryItem
                If ugR.Cells("IsLot").Value = True Then
                    'If ugR.ChildBands.HasChildRows = False Then
                    '    MsgBox("Enter Lot Numbers", MsgBoxStyle.Exclamation, "Pastel Evolution")
                    '    DatabaseContext.RollbackTran()
                    '    If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                    '    If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                    '    Exit Sub
                    'End If
                    'Dim ugR2 As UltraGridRow
                    'For Each ugR2 In ugR.ChildBands(0).Rows
                    '    PurchaseOrderDetail.SerialNumbers.Add(CStr(ugR2.Cells("SerialNumber").Value))
                    'Next
                End If



                PurchaseOrderDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                If bIsInclusive = False Then
                    PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                    PurchaseOrderDetail.TaxMode = TaxMode.Exclusive
                    PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                ElseIf bIsInclusive = True Then
                    PurchaseOrderDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                    PurchaseOrderDetail.TaxMode = TaxMode.Inclusive
                    PurchaseOrderDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                End If
                PurchaseOrderDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)

                If ugR.Cells("IsWH").Value = True Then
                    WH = New Warehouse(16)
                    PurchaseOrderDetail.Warehouse = WH
                End If
                PurchaseOrderDetail.Description = CStr(ugR.Cells("Description_1").Value)

                Dim LotNo As New Lot()
                LotNo.Code = ugR.Cells("SerialLot").Text
                LotNo.ExpiryDate = ugR.Cells("Lot_Exp").Value
                PurchaseOrderDetail.Lot = LotNo

                'If ec.Text <> Nothing Then
                '    sSONo = ec.Text
                'End If


                'SQL = " select cLotDescription from _etblLotTracking where idLotTracking  = '" & ugR.Cells("Lot").Value & "' "
                'Dim objSQL As New clsSqlConn
                'With objSQL
                '    Dim DS1 = New DataSet
                '    DS1 = .Get_Data_Sql(SQL)
                '    If DS1.Tables(0).Rows.Count > 0 Then
                '        LotN = New Lot()
                '        'sSONo = "PKW72118"
                '        'If InStr(DS1.Tables(0).Rows(0)(0).ToString(), "-") = 0 Then 'IF LOT NUMBER IS 001
                '        '    LotN.Code = sSONo.Substring(3) & "-" & CStr(ugR.Cells("Description_1").Value)
                '        'Else
                '        '    If cmbGRVLoc.Text = "dbKelaniya_new" Then 'if isuing to kalaniya database
                '        LotN.Code = DS1.Tables(0).Rows(0)(0).ToString()
                '        'Else
                '        '    'LotN.Code = sSONo.Substring(3) & "-" & DS1.Tables(0).Rows(0)(0).ToString().Substring(0, InStr(DS1.Tables(0).Rows(0)(0).ToString(), "-") - 1)
                '        '    LotN.Code = sSONo.Substring(3) & "-" & DS1.Tables(0).Rows(0)(0).ToString().Substring(InStr(DS1.Tables(0).Rows(0)(0).ToString(), "-"))
                '        'End If
                '        '    End If
                '        LotN.ExpiryDate = DateAdd(DateInterval.Day, 60, Date.Now)
                '    End If
                'End With

                PurchaseOrderDetail.Lot = LotNo

                PurchaseOrder.Detail.Add(PurchaseOrderDetail)
            Next

            PurchaseOrder.Save()

            DatabaseContext.CommitTran()

            'MemorizePOStock(PurchaseOrder.ID)

            PONo = PurchaseOrder.OrderNo.ToString()
            MsgBox(PurchaseOrder.OrderNo, MsgBoxStyle.Information, "Pastel Evolution")


            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

            'saveIBT(iSONo, True, iIBTNo)

            Call Discard()

        Catch ex As Exception
            'Trans1.Rollback()
            'If Con1.State = ConnectionState.Open Then Con1.Close()
            'Trans2.Rollback()
            'If Con2.State = ConnectionState.Open Then Con2.Close()
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Pastel Evolution")
            Exit Sub

        Finally

            'If Con1.State = ConnectionState.Open Then Con1.Close()
            If Con2.State = ConnectionState.Open Then Con2.Close()

        End Try

    End Sub
    'Private Sub GetDatabaseName()
    '    aDataBase = New ArrayList()
    '    'aDataBase.Add("dbUdawatta_new")
    '    'aDataBase.Add("dbNawinna_new")
    '    aDataBase.Add("dbKelaniya_new")
    'End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Validate Customer already exist

        If txtName1.Text.Length = 0 Then
            MessageBox.Show("Please enter a customer name", "Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If



        If txtNumber.Text.Length < 3 Then
            MessageBox.Show("Please enter a valied customer name", "Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If cmbClass.Text = "" Then
            MessageBox.Show("Please Select a cusomer class", "Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Try

            Dim cdb As String = sSQLSrvDataBase
            Dim sDB As String
            Dim Code As String

            Code = cmbRejon.Text + " " + txtReg.Text.Trim() + " " + txtNumber.Text

            'For Each Obj In aDataBase
            sDB = sSQLSrvDataBase

            'Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= " & sDB & "  ")
            'sSQL = " SELECT Name FROM " & Obj & ".dbo.Client WHERE Name = '" & txtNumber.Text & "' "
            sSQL = " SELECT Account FROM " & sSQLSrvDataBase & ".dbo.Client WHERE Account = '" & Code & "' "


            Dim objSQL As New clsSqlConn
            With objSQL

                DS = New DataSet
                DS = .Get_Data_Sql(sSQL)

                If DS.Tables(0).Rows.Count > 0 Then
                    MessageBox.Show("Cusomer already exists", "Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End With



            'sSQL = " SELECT count(Account) FROM Client WHERE Account like '" & UCase(Mid(txtName.Text, 1, 3)) & "%' "
            'sSQL = " SELECT count(Account) FROM " & Obj & ".dbo.Client WHERE Account like 'AAA%' "


            'Dim pfx As String = 0
            'objSQL = New clsSqlConn
            'With objSQL

            '    DS = New DataSet
            '    DS = .Get_Data_Sql(sSQL)
            '    pfx = Integer.Parse(DS.Tables(0).Rows(0)(0)) + 1
            '    While pfx.Length < 3
            '        pfx = "0" + pfx
            '    End While
            'End With



            Dim nCustomer As New Customer()
            'nCustomer.Code = UCase(Mid(txtName.Text, 1, 3)) + pfx
            'nCustomer.Code = "AAA" + pfx
            nCustomer.Code = Code
            nCustomer.Description = txtName1.Text
            nCustomer.Telephone = txtTP.Text
            'nCustomer.EmailAddress = txtEmail.Text
            nCustomer.AreaID = cmbArea.Value
            nCustomer.GroupID = 1
            nCustomer.DeliverTo = txtDelTo.Text
            nCustomer.CellPhone = txtChassis.Text
            nCustomer.Webpage = txtPaint.Text
            nCustomer.EmailAddress = txtYear.Text
            nCustomer.Title = cmbTitle.Text

            Dim cntr As New Country(1)
            nCustomer.Country = cntr


            Dim bClass As New BusinessClass(cmbClass.Text)
            'bClass.Save()

            nCustomer.BusinessClass = bClass



            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

            DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
            DatabaseContext.SetLicense("DE09110022", "1428511")
            DatabaseContext.CreateConnection("UMSERVER", sDB, sSQLSrvUserName, sSQLSrvPassword, False)


            nCustomer.Save()
            'pfx = 0

            objSQL = New clsSqlConn
            With objSQL
                Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= " & sDB & "  ")
                SQL = " Update Client SET cIDNumber = '" & txtDelTo.Text & "' , cAccDescription = '" & txtName1.Text & "' , Post1 = '" & txtadd1.Text & "' , Post2 = '" & txtadd2.Text & "' , Post3 = '" & txtadd3.Text & "' WHERE DCLink = " & nCustomer.ID & " "
                .Begin_Trans()
                If .Execute_Sql_Trans(sSQL) = 0 Then
                    .Rollback_Trans()
                    Exit Sub
                End If
                .Commit_Trans()
            End With
            'Next


            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

            DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
            DatabaseContext.SetLicense("DE09110022", "1428511")
            DatabaseContext.CreateConnection("UMSERVER", cdb, sSQLSrvUserName, sSQLSrvPassword, False)




            GET_DATA()

            'cmbCustomer.Value = nCustomer.ID
            'cmbCustomer.Text = txtName.Text


            txtNumber.Text = ""
            txtTP.Text = ""
            txtDelTo.Text = ""
            'txtEmail.Text = ""
            txtadd1.Text = ""
            txtadd2.Text = ""
            txtadd3.Text = ""
            cmbArea.Value = ""
            cmbClass.Text = ""
            pnlNewCustomer.Visible = False


        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        txtNumber.Text = ""
        txtTP.Text = ""
        txtDelTo.Text = ""
        'txtEmail.Text = ""
        txtadd1.Text = ""
        txtadd2.Text = ""
        txtadd3.Text = ""
        cmbArea.Value = ""
        cmbClass.Text = ""
        pnlNewCustomer.Visible = False
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        pnlNewCustomer.Visible = True
    End Sub

    Private Sub ToolStripButton2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click




        Dim objSQL As New clsSqlConn
        With objSQL


            SQL = " SELECT dbUdawatta_new.dbo.StkItem.cSimpleCode, dbUdawatta_new.dbo.StkItem.ucIICategory FROM dbUdawatta_new.dbo.StkItem WHERE dbUdawatta_new.dbo.StkItem.ucIICategory <> '' and dbUdawatta_new.dbo.StkItem.ItemActive = 1 "



            'SQL = " SELECT DISTINCT " & _
            '"  _etblLotTracking.iStockID, WhseStk.WHStockLink, SUM(_etblLotTrackingQty.fQtyOnHand) AS LotQ,  WhseStk.WHQtyOnHand AS WHQ, " & _
            '"  MIN(_etblLotTrackingQty.iLotTrackingID) AS lot " & _
            '" FROM         WhseStk INNER JOIN " & _
            '"  _etblLotTracking ON WhseStk.WHStockLink = _etblLotTracking.iStockID INNER JOIN " & _
            '"  _etblLotTrackingQty ON _etblLotTracking.idLotTracking = _etblLotTrackingQty.iLotTrackingID AND " & _
            '"  WhseStk.WHWhseID = _etblLotTrackingQty.iWarehouseID " & _
            '" GROUP BY _etblLotTracking.iStockID, WhseStk.WHStockLink, WhseStk.WHQtyOnHand " & _
            '" ORDER BY _etblLotTracking.iStockID "

            DS = New DataSet
            DS = .Get_Data_Sql(SQL)
            If DS.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow
                .Begin_Trans()
                For Each dr In DS.Tables(0).Rows

                    'SQL = " SELECT _btblInvoiceLines.iStockCodeID, StkItem.SerialItem " & _
                    '" FROM _btblInvoiceLines INNER JOIN " & _
                    '" StkItem ON _btblInvoiceLines.iStockCodeID = StkItem.StockLink where iInvoiceID = " & dr("AutoIndex") & ""
                    'SQL = " SELECT     StkItem.cSimpleCode, dbUdawatta.dbo._etblPriceListPrices.fInclPrice, dbUdawatta.dbo._etblPriceListPrices.fExclPrice " & _
                    '" FROM  dbUdawatta.dbo._etblPriceListPrices RIGHT OUTER JOIN " & _
                    '"                      StkItem ON dbUdawatta.dbo._etblPriceListPrices.iStockID = StkItem.StockLink  where StkItem.ItemActive = 1"

                    'DS1 = New DataSet
                    'DS1 = .Get_Data_Sql(SQL)
                    'If DS1.Tables(0).Rows.Count > 0 Then
                    '    Dim dr1 As DataRow
                    '    For Each dr1 In DS1.Tables(0).Rows
                    'Line += 1
                    'SQL = " Update sbSerialMF Set PrimaryLineID = " & Line & " Where AutoIndex = " & dr("AutoIndex") & " and SNStockLink = " & dr1("iStockCodeID") & "  "
                    'If dr1("ItemGroup") <> "" Then

                    'SQL = " SELECT StockLink from StkItem where csimplecode = '" & dr1("csimplecode") & "' "
                    'DS = New DataSet
                    'DS = .Get_Data_Sql(SQL)

                    'If DS.Tables(0).Rows(0)(0) <> "" Then
                    'Dim sa As Integer
                    'sa = 0


                    'SQL = " SELECT  StockLink, ItemGroup FROM   dbUdawatta_new.DBO.StkItem WHERE ItemGroup <> '' "
                    'DS3 = New DataSet
                    'DS3 = .Get_Data_Sql(SQL)

                    'If DS3.Tables(0).Rows.Count > 0 Then
                    '    Dim s As Integer
                    '    For Each dr3 In DS3.Tables(0).Rows
                    'SQL = " Update StkItem Set ItemGroup = '" & dr3("ItemGroup")  & "' where  StockLink = '" & dr3("StockLink") & "'"
                    'If dr("LotQ") < dr("WHQ") Then
                    'SQL = " update _etblLotTrackingQty set fQtyOnHand = '" & dr("WHQ")  & "' where  iLotTrackingID =  '" & dr("lot") & "'  "

                    'If dr("Re_Ord_Qty") > 0 Then
                    SQL = " update StkItem set ucIICategory = '" & dr("ucIICategory") & "' where cSimpleCode = '" & dr("cSimpleCode") & "' and StkItem.ItemActive = 1  "

                    .Execute_Sql_Trans(SQL)
                    'Else
                    '    SQL = " update StkItem set Re_Ord_Lvl = 0, Re_Ord_Qty = 0  where cSimpleCode = '" & dr("cSimpleCode") & "'  "

                    '    .Execute_Sql_Trans(SQL)
                    'End If


                    'End If

                    's = s + 1
                    '    Next
                    'Else
                    'For s As Integer = 1 To 3
                    '    If IsDBNull(dr1("fExclPrice")) Then
                    '    Else
                    '        SQL = " insert into _etblPriceListPrices (iPriceListNameID,[iStockID]  ,[bUseMarkup]  ,[iMarkupOnCost]   ,[fMarkupRate]  ,[fExclPrice] ,[fInclPrice]) values (" & s & ",'" & DS.Tables(0).Rows(0)(0) & "', 0,0,0, " & dr1("fExclPrice") & ", " & dr1("fInclPrice") & " ) "
                    '        .Begin_Trans()
                    '        .Get_Data_Sql_Trans(SQL)
                    '        .Commit_Trans()
                    '        s = s + 1
                    '    End If
                    'Next
                    'End If
                    'End If
                Next
                .Commit_Trans()
                'Line = 0
            End If
            'Next
            'End If
        End With
    End Sub

    Private Sub UG_ContextMenuStripChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles UG.ContextMenuStripChanged

    End Sub

    Private Sub UG1_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs)

    End Sub
   

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbBarcode.Click
        Dim ItemCode As String
        Dim Lot As String
        Dim bin As String
        Dim group As String

        Dim Qty As Double
        Dim price As Double
        Dim ugr As UltraGridRow
        Dim Desc1 As String
        Dim Desc2 As String
        Dim SN As String
        Dim inventory As New InventoryItem


        Dim file As System.IO.StreamWriter

        'file = My.Computer.FileSystem.OpenTextFileWriter("item.txt", True)



        'clear text file
        Dim fs = New FileStream("D:\item.txt", FileMode.Truncate)
        fs.Close()



        file = My.Computer.FileSystem.OpenTextFileWriter("D:\item.txt", True)

        For Each ugr In UG.Rows
            ItemCode = ugr.Cells("Code").Text
            'Lot = txtDelNote_SupInvNo.Text
            bin = ugr.Cells("cBin").Text
            group = ugr.Cells("ItemGroup").Text
            Qty = ugr.Cells("Quantity").Value

            Dim i = 1
            For i = 1 To Qty

                'create inventory

                Desc1 = Mid(ItemCode, 1, ItemCode.IndexOf("/"))
                Desc2 = Mid(ItemCode, ItemCode.IndexOf("/") + 1)

                SN = Desc1 + "-" + Lot.ToString() + "-" + i.ToString()
                Desc1 = Desc1.ToString()



                file.WriteLine(Desc1 + "," + Lot + "," + bin)







                'Dim objSQL As New clsSqlConn
                'With objSQL
                '    .Begin_Trans()

                '    SQL = " INSERT INTO StkItem ( Code, Description_1, Description_2, Description_3, ItemGroup, Pack, TTI, TTC, TTG, TTR, Bar_Code, Re_Ord_Lvl, Re_Ord_Qty, Min_Lvl, Max_Lvl, AveUCst, " & _
                '     "LatUCst, LowUCst, HigUCst, StdUCst, Qty_On_Hand, LGrvCount, ServiceItem, ItemActive, ReservedQty, QtyOnPO, QtyOnSO, WhseItem, SerialItem, DuplicateSN, " & _
                '     " StrictSN, BomCode, SMtrxCol, PMtrxCol, JobQty, cModel, cRevision, cComponent, dDateReleased, iBinLocationID, dStkitemTimeStamp, iInvSegValue1ID,  " & _
                '     " iInvSegValue2ID, iInvSegValue3ID, iInvSegValue4ID, iInvSegValue5ID, iInvSegValue6ID, iInvSegValue7ID, cExtDescription, cSimpleCode, bCommissionItem, MFPQty,  " & _
                '     " bLotItem, iLotStatus, bLotMustExpire, iItemCostingMethod, fItemLastGRVCost, iEUCommodityID, iEUSupplementaryUnitID, fNetMass, iUOMStockingUnitID,  " & _
                '     " iUOMDefPurchaseUnitID, iUOMDefSellUnitID, StkItem_iBranchID, StkItem_dCreatedDate, StkItem_dModifiedDate, StkItem_iCreatedBranchID,  " & _
                '     " StkItem_iModifiedBranchID, StkItem_iCreatedAgentID, StkItem_iModifiedAgentID, fStockGPPercent, StkItem_iChangeSetID, bAllowNegStock, fQtyToDeliver,  " & _
                '     " ucIINoOfUnits, ubIIReOrder, ucIIHSCode, ucIICategory, ucIICID, StkItem_fLeadDays, ucIIBrandName, ucIIRemark)  " & _
                '    " SELECT     '" & Desc1.ToString() + Desc2.ToString() & "', Description_1, Description_2, Description_3, ItemGroup, Pack, TTI, TTC, TTG, TTR, Bar_Code, Re_Ord_Lvl, Re_Ord_Qty, Min_Lvl, Max_Lvl, AveUCst, " & _
                '     " LatUCst, LowUCst, HigUCst, StdUCst, 0, LGrvCount, ServiceItem, ItemActive, ReservedQty, 0, 0, WhseItem, SerialItem, DuplicateSN, " & _
                '     " StrictSN, BomCode, SMtrxCol, PMtrxCol, JobQty, cModel, cRevision, cComponent, dDateReleased, iBinLocationID, dStkitemTimeStamp, iInvSegValue1ID, " & _
                '     " iInvSegValue2ID, iInvSegValue3ID, iInvSegValue4ID, iInvSegValue5ID, iInvSegValue6ID, iInvSegValue7ID, cExtDescription, '" & Desc1 & "', bCommissionItem, MFPQty, " & _
                '     " bLotItem, iLotStatus, bLotMustExpire, iItemCostingMethod, fItemLastGRVCost, iEUCommodityID, iEUSupplementaryUnitID, fNetMass, iUOMStockingUnitID, " & _
                '     " iUOMDefPurchaseUnitID, iUOMDefSellUnitID, StkItem_iBranchID, StkItem_dCreatedDate, StkItem_dModifiedDate, StkItem_iCreatedBranchID, " & _
                '     " StkItem_iModifiedBranchID, StkItem_iCreatedAgentID, StkItem_iModifiedAgentID, fStockGPPercent, StkItem_iChangeSetID, bAllowNegStock, fQtyToDeliver, " & _
                '     " ucIINoOfUnits, ubIIReOrder, ucIIHSCode, ucIICategory, ucIICID, StkItem_fLeadDays, ucIIBrandName, ucIIRemark " & _
                '     " FROM StkItem WHERE Code = '" & ItemCode & "'"

                '    .Execute_Sql_Trans(SQL)



                '    'inv.Code = Desc1
                '    'inv.Description = ItemCode

                '    .Commit_Trans()
                'End With
                'inventory = New InventoryItem(ItemCode)

                'Dim inv As InventoryItem
                'inv = inventory



                'inv.Save()

                'ugr.Delete(False)

                'add a new line to grid

            Next
        Next
        file.Close()



        Dim p = New Process()
        p.StartInfo = New ProcessStartInfo("print.bat")
        p.Start()
        p.WaitForExit()
        'Delete_Row()
        'Me.Close()

    End Sub

    Private Sub UG_CellDataError(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellDataErrorEventArgs) Handles UG.CellDataError

    End Sub

    Private Sub txtNumber_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNumber.Leave

        Dim Code As String

        Code = cmbRejon.Text + " " + txtReg.Text.Trim() + " " + txtNumber.Text

        'sSQL = " SELECT Account FROM " & Obj & ".dbo.Client WHERE Account = '" & Code & "' "
        sSQL = " SELECT Account FROM dbo.Client WHERE Account = '" & Code & "' "

        Dim objSQL As New clsSqlConn
        With objSQL

            DS = New DataSet
            DS = .Get_Data_Sql(sSQL)

            If DS.Tables(0).Rows.Count > 0 Then
                MessageBox.Show("Cusomer already exists", "Evolution AddOn", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End With
    End Sub
End Class