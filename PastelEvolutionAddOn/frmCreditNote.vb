Imports Infragistics.Win.UltraWinGrid
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Pastel.Evolution

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing

Public Class frmCreditNote

    Dim sDeliveryNo As String
    Dim sInvoiceNo As String
    Dim dblMargin As Double
    Dim R As UltraGridRow
    Dim strCustomer As String
    Dim strProject As String
    Dim strSalesRep As String
    Dim sAccount As String
    Dim sAdd1 As String
    Dim sAdd2 As String
    Dim sAdd3 As String
    Dim sPAdd1 As String
    Dim sPAdd2 As String
    Dim sPAdd3 As String
    Dim sQUONo As String
    Dim sLTNo As String
    Dim iLTNo As Integer
    Dim bIsInclusive As Boolean = False
    Public bIsAllowOverrideCreditLimit As Boolean = False
    Public iWH As Integer = 0
    Public iCRNNo As Int64 = 0
    Public sSONo As String
    Public iSONo As String
    Public iOldCRNNo As Integer = 0
    Public IsNew As Boolean = True
    Public iDocType As Integer
    Public intMstWH As Integer
    Public bValChange As Boolean = True
    'Public POLocation As Integer
    Public DS1 As DataSet

    Private Sub tsbApplySet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbApplySet.Click
        Try
            UG.DisplayLayout.SaveAsXml(Application.StartupPath & "\" & sAgent & "RTN.xml")
            'UG.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.ExternalSortSingle
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub frmReturnSuplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GET_DATA()
        'GET_NEXT_INVOICE_NO()
        'getmstWH()
        'Read_Company()

        dtpDeliDate.Value = Date.Now
        dtpDueDate.Value = Date.Now
        dtpInDate.Value = Date.Now
        dtpOrdDate.Value = Date.Now
    End Sub

    Private Sub GET_DATA()
        sSQL = ""


        sSQL = "SELECT DCLink, Account, Name, Physical1, Physical2," & _
           " Physical3, Post1, Post2, Post3 , Discount , cAccDescription FROM Client Order By Name  "

        sSQL += " SELECT   distinct  StkItem.WhseItem, StkItem.iUOMStockingUnitID, StkItem.StockLink, StkItem.Code, StkItem.Description_1, StkItem.Description_2, StkItem.ItemGroup, StkItem.bLotItem AS LotItem, StkItem.LatUCst, " & _
        " StkItem.AveUCst, _etblUnits.iUnitCategoryID, StkItem.iItemCostingMethod, GrpTbl.fCostMargine, StkItem.cSimpleCode " & _
        " FROM         StkItem INNER JOIN  _btblInvoiceLines ON StkItem.StockLink = _btblInvoiceLines.iStockCodeID LEFT OUTER JOIN " & _
        " GrpTbl ON StkItem.ItemGroup = GrpTbl.StGroup LEFT OUTER JOIN  _etblUnits ON StkItem.iUOMStockingUnitID = _etblUnits.idUnits " & _
        " WHERE     (StkItem.ServiceItem = 0) AND (StkItem.ItemActive = 1) ORDER BY StkItem.Code "
        sSQL += " SELECT WhseLink, Code, Name, KnownAs FROM WhseMst"
        sSQL += " SELECT idTaxRate, Code, Description, TaxRate FROM TaxRate"
        sSQL += " SELECT idUnits, cUnitCode,iUnitCategoryID FROM   _etblUnits"
        sSQL += " SELECT bIsInclusive FROM StDfTbl"
        sSQL += " SELECT WhseLink FROM WhseMst WHERE DefaultWhse = 1"
        sSQL += " SELECT StGroup FROM GrpTbl "
        sSQL += " SELECT idSalesRep, Code, Name FROM SalesRep WHERE Rep_On_Hold = 'N'  Order By Name "

        Try
            Con1.ConnectionString = sConStr
            CMD = New SqlCommand(sSQL, Con1)
            DS = New DataSet
            DA = New SqlDataAdapter(CMD)

            Con1.Open()
            DA.Fill(DS)
            Con1.Close()

            cmbCustomer.DataSource = DS.Tables(0)
            cmbCustomer.ValueMember = "DCLink"
            cmbCustomer.DisplayMember = "Name"
            cmbCustomer.DisplayLayout.Bands(0).Columns("DCLink").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Physical1").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Physical2").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Physical3").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Post1").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Post2").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Post3").Hidden = True
            cmbCustomer.DisplayLayout.Bands(0).Columns("Discount").Hidden = True


            'Stock
            DDStock.DataSource = DS.Tables(1)
            DDStock.ValueMember = "StockLink"
            DDStock.DisplayMember = "Code"
            DDStock.DisplayLayout.Bands(0).Columns("LatUCst").Hidden = True
            DDStock.DisplayLayout.Bands(0).Columns("WhseItem").Hidden = True
            DDStock.DisplayLayout.Bands(0).Columns("iUOMStockingUnitID").Hidden = True
            DDStock.DisplayLayout.Bands(0).Columns("iUnitCategoryID").Hidden = True
            DDStock.DisplayLayout.Bands(0).Columns("iItemCostingMethod").Hidden = True
            DDStock.DisplayLayout.Bands(0).Columns("AveUCst").Hidden = True
            'DDStock.DisplayLayout.Bands(0).Columns("SerialItem").Hidden = True

            'Description
            DDDescription.DataSource = DS.Tables(1)
            DDDescription.ValueMember = "Description_1"
            DDDescription.DisplayMember = "Description_1"
            DDDescription.DisplayLayout.Bands(0).Columns("LatUCst").Hidden = True
            DDDescription.DisplayLayout.Bands(0).Columns("WhseItem").Hidden = True
            DDDescription.DisplayLayout.Bands(0).Columns("iUOMStockingUnitID").Hidden = True
            DDDescription.DisplayLayout.Bands(0).Columns("iUnitCategoryID").Hidden = True
            DDDescription.DisplayLayout.Bands(0).Columns("iItemCostingMethod").Hidden = True
            DDDescription.DisplayLayout.Bands(0).Columns("AveUCst").Hidden = True

            'WH
            DDWH.DataSource = DS.Tables(2)
            DDWH.ValueMember = "WhseLink"
            DDWH.DisplayMember = "Code"
            DDWH.DisplayLayout.Bands(0).Columns("WhseLink").Hidden = True

            'Tax
            DDTaxType.DataSource = DS.Tables(3)
            DDTaxType.ValueMember = "idTaxRate"
            DDTaxType.DisplayMember = "Code"
            DDTaxType.DisplayLayout.Bands(0).Columns("idTaxRate").Hidden = True

            DDUnit.DataSource = DS.Tables(4)
            DDUnit.ValueMember = "idUnits"
            DDUnit.DisplayMember = "cUnitCode"
            DDUnit.DisplayLayout.Bands(0).Columns("idUnits").Hidden = True
            DDUnit.DisplayLayout.Bands(0).Columns("iUnitCategoryID").Hidden = True
            DDUnit.DisplayLayout.Bands(0).Columns("cUnitCode").Header.Caption = "Unit"
            Dim Dr As DataRow
            For Each Dr In DS.Tables(5).Rows
                If Dr("bIsInclusive") = False Then
                    bIsInclusive = False
                ElseIf Dr("bIsInclusive") = True Then
                    bIsInclusive = True
                End If
            Next

            For Each Dr In DS.Tables(6).Rows
                iWH = CInt(Dr("WhseLink"))
            Next

            'Sales Rep
            cmbSaleRep.DataSource = DS.Tables(8)
            cmbSaleRep.ValueMember = "idSalesRep"
            cmbSaleRep.DisplayMember = "Name"
            cmbSaleRep.DisplayLayout.Bands(0).Columns("idSalesRep").Hidden = True


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Evolution")
            Exit Sub
        Finally
            If Con1.State = ConnectionState.Open Then Con1.Close()
        End Try


    End Sub


    Private Sub tsbOpen_Click(sender As Object, e As EventArgs) Handles tsbOpen.Click
        bValChange = False
        frmOpen.Get_Customer()
        frmOpen.iDocType = DocType.CreditNote
        frmOpen.iDocType_1 = DocType.CreditNote
        frmOpen.POLocation = cmbCustomer.Value
        frmOpen.ShowDialog()
        'frmOpen.TopMost = True
        Me.Close()
        bValChange = True
    End Sub

    Public Sub Discard()
        Try
            iCRNNo = 0
            tsbUpdateSO.Enabled = True
            cmbCustomer.ResetText()
            txtDeliveryAdd.ResetText()
            txtPostalAdd.ResetText()
            cmbProject.ResetText()
            txtOrdNo.ResetText()
            txtDelNote.ResetText()
            txtExtOrder.ResetText()

            cmbSaleRep.ResetText()

            DeleteRows()

        Catch ex As Exception

        End Try

    End Sub
    Public Sub DeleteRows()
        Dim dr As UltraGridRow
        For Each dr In UG.Rows
            dr.Delete(False)
        Next
    End Sub

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
        txtConAmt.Value = Math.Round(dblLineTotal_Incl_Dis, 2, MidpointRounding.AwayFromZero)

        If txtConTax.Value > 0 Then
            dblLineTax_Dis = Math.Round((dblLineTotal_Incl_Dis * 100 / 112), 2, MidpointRounding.AwayFromZero)
            dblLineTax_Dis = Math.Round(dblLineTotal_Incl_Dis - dblLineTax_Dis, 2, MidpointRounding.AwayFromZero)
            txtConTax.Value = Math.Round(dblLineTax_Dis, 2, MidpointRounding.AwayFromZero)
        End If

    End Sub

    Public Sub SetSatus()
        'If IsNew = True Then
        '    lblStatus.Text = "NEW"
        '    If iDocType = DocType.SalesOrder Then
        '        tsbSaveSO.Enabled = True
        '        tsbUpdateSO.Enabled = True
        '        tsbSavePO.Visible = False
        '        tsbUpdatePO.Visible = False
        '        tsbProcessPO.Visible = False

        '    ElseIf iDocType = DocType.IBTReceive Then
        '        tsbSavePO.Enabled = True
        '        tsbUpdatePO.Enabled = False
        '        tsbProcessPO.Enabled = False
        '        tsbSaveSO.Visible = False
        '        tsbUpdateSO.Visible = False
        '    End If
        'Else
        '    lblStatus.Text = "EDIT"
        '    If iDocType = DocType.SalesOrder Then
        '        tsbSaveSO.Enabled = True
        '        tsbUpdateSO.Enabled = True
        '        tsbSavePO.Visible = False
        '        tsbUpdatePO.Visible = False
        '        tsbProcessPO.Visible = False
        '        tsbSaveSO.Visible = True
        '        tsbUpdatePO.Visible = False
        '    ElseIf iDocType = DocType.IBTReceive Then
        '        tsbSavePO.Enabled = False
        '        tsbUpdatePO.Enabled = True
        '        tsbProcessPO.Enabled = True
        '        tsbSaveSO.Visible = False
        '        tsbUpdatePO.Visible = False
        '        tsbSavePO.Visible = True
        '        tsbUpdatePO.Visible = True
        '        tsbProcessPO.Visible = True
        '    End If
        'End If
    End Sub
    Public Sub Delete_Row()
        Dim R As UltraGridRow
        For Each R In UG.Rows
            If R.Cells("Description_1").Text = "" Then
                R.Delete(False)
                Exit Sub
            End If
        Next
    End Sub

    Private Sub tsbNewlIne_Click(sender As Object, e As EventArgs) Handles tsbNewlIne.Click

        Dim ugR As UltraGridRow
        ugR = UG.DisplayLayout.Bands(0).AddNew
        ugR.Cells("Line").Value = ugR.Index + 1
        ugR.Cells("Line").Selected = True
        'ugR.Cells("Warehouse").Value = iWH
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
        UG.DisplayLayout.Bands(0).Columns("BranchQty").CellActivation = Activation.Disabled
        UG.DisplayLayout.Bands(0).Columns("Warehouse").CellActivation = Activation.Disabled
        UG.DisplayLayout.Bands(0).Columns("Max_lvl").CellActivation = Activation.Disabled
        'UG.DisplayLayout.Bands(0).Columns("Quantity").CellActivation = Activation.AllowEdit
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Me.Close()
    End Sub

    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        UG.PerformAction(UltraGridAction.CommitRow)

        Try



            'getDefaultwh(sError, iToWH)

            'If sError <> "" Then
            '    Exit Sub
            'End If

            'processRTN(iCRNNo, True, iCRNNo)

            'If iCRNNo = 0 Then 'if sales order didnt process
            '    Exit Sub
            'End If

            ProcessCRN()


            If MsgBox("Do you want to print CRN now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                Dim objRep As New ReportDocument
                Dim s As String = Application.StartupPath

                'If iDocType_1 = 10 Then
                objRep.Load(Application.StartupPath & "\CRN.rpt")
                'ElseIf iDocType_1 = 8 Then 
                '    objRep.Load(Application.StartupPath & "\IBT_R.rpt")
                'End If

                Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= dbKelaniya_new ")
                SQL = " SELECT OrderNum, DocState  from InvNum where ExtOrderNum = '" & sSONo & "' "
                CMD = New SqlCommand(SQL, Con2)
                CMD.CommandType = CommandType.Text
                DA = New SqlDataAdapter(CMD)
                DS1 = New DataSet
                Con2.Open()
                DA.Fill(DS1)
                Con2.Close()

                If DS1.Tables(0).Rows.Count > 0 Then
                    frmPrintPreview.vInvNo = DS1.Tables(0).Rows(0)(0)
                    If DS1.Tables(0).Rows(0)(1) = 4 Then
                        frmPrintPreview.vPState = "Processed"
                    Else
                        frmPrintPreview.vPState = "Unprocessed"
                    End If

                Else
                    frmPrintPreview.vInvNo = " "
                    frmPrintPreview.vPState = " "
                End If

                frmPrintPreview.vSelectionFormula = "{InvNum.AutoIndex}= " & iCRNNo & ""

                iRepoID = 2

                frmPrintPreview.ShowDialog()
                iRepoID = 0


            End If






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
    Private Sub ProcessCRN()

        Dim iInvoiceID As Integer = 0
        Dim sOrderNumber As String = ""
        Dim iCustomer As Integer = 0
        Dim iRTNNo As Integer = 0
        Dim sError As String = ""
        Dim iToWH As Integer = 0


        ReDim lots(UG.Rows.Count)
        ''to find the client database
        'Dim CDB As String = ""
        'If sSQLSrvDataBase = "dbNawinna_new" Then
        '    CDB = "UDA003"
        'ElseIf sSQLSrvDataBase = "dbKurunegala_new" Then
        '    CDB = "UDA004"
        'ElseIf sSQLSrvDataBase = "dbKiribathgoda_new" Then
        '    CDB = "UDA005"
        'ElseIf sSQLSrvDataBase = "dbUdawatta_new" Then
        '    CDB = "UDA006"
        'ElseIf sSQLSrvDataBase = "dbMathara" Then
        '    CDB = "DEA001"
        'End If

        getDefaultwh(sError, iToWH)

        If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
        If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)

        DatabaseContext.BeginTran()
        Dim CreditNote As New CreditNote()

        If lblState.Text <> "Archived" Then
            CreditNote = New CreditNote(lblAutoID.Text)
            CreditNote.Detail.Clear()
        End If

        Dim CRNDetail As OrderDetail
        Dim InventoryItem As InventoryItem
        Dim WH As Warehouse
        Dim LotN As Lot


        Dim Customer As Customer
        If cmbCustomer.Text.Length > 4 Then
            Customer = New Customer(CInt(cmbCustomer.Value))
        Else
            Customer = New Customer(CInt(cmbCustomer.Text))
        End If

        iCustomer = Customer.ID
        'CreditNote.OrderNo = sOrderNumber
        CreditNote.ExternalOrderNo = CreditNote.OrderNo
        CreditNote.Account = Customer
        CreditNote.InvoiceTo = Customer.PostalAddress
        CreditNote.DeliverTo = Customer.PhysicalAddress
        CreditNote.ExternalOrderNo = txtOrdNo.Text.ToString
        CreditNote.InvoiceDate = Format(Now.Date, "dd/MM/yyyy")
        CreditNote.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
        CreditNote.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")
        'CreditNote.SupplierInvoiceNo = "" ' CStr(txtInNo.Text)
        If bIsInclusive = False Then
            CreditNote.TaxMode = TaxMode.Exclusive
        ElseIf bIsInclusive = True Then
            CreditNote.TaxMode = TaxMode.Inclusive
        End If


        For Each ugR In UG.Rows
            If CDbl(ugR.Cells("Quantity").Value) > 0 Then
                CRNDetail = New OrderDetail
                InventoryItem = New InventoryItem(CStr(ugR.Cells("Code").Text))
                CRNDetail.InventoryItem = InventoryItem




                CRNDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                If bIsInclusive = False Then
                    CRNDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                    CRNDetail.TaxMode = TaxMode.Exclusive
                    CRNDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                ElseIf bIsInclusive = True Then
                    If CDbl(ugR.Cells("Price_Incl").Value) = 0 Then
                        CRNDetail.UnitSellingPrice = Get_Value(" SELECT fInclPrice FROM _etblPriceListPrices where iPriceListNameID = 1 and iStockID = " & CStr(ugR.Cells("StockID").Text) & " ")
                    Else
                        CRNDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                    End If
                    CRNDetail.Discount = CDbl(ugR.Cells("Discount").Value)
                    CRNDetail.TaxMode = TaxMode.Inclusive
                    CRNDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                End If
                CRNDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)
                CRNDetail.Discount = CDbl(ugR.Cells("Discount").Value)
                If ugR.Cells("IsWH").Value = True Then
                    WH = New Warehouse(iToWH)
                    CRNDetail.Warehouse = WH
                End If
                CRNDetail.Description = CStr(ugR.Cells("Description_1").Value)
                'PurchaseOrderDetail.Note = CStr(ugR.Cells("LineNote").Value)
                'CRNDetail.LotID = CStr(ugR.Cells("Lot").Value)


                If ugR.Cells("IsLot").Value = True Then
                    LotN = New Lot()
                    LotN.Code = ugR.Cells("Lot").Text
                    'LotN.ExpiryDate = ugR.Cells("Lot_Exp").Value
                    CRNDetail.Lot = LotN
                End If

                If ec.Text <> Nothing Then
                    sSONo = ec.Text
                End If


                CreditNote.Detail.Add(CRNDetail)
            End If
        Next

        CreditNote.Save()

        CreditNote.Process()
        PONo = CreditNote.Reference


        'AllocationEntry.Equals(PONo, PONo)

        DatabaseContext.CommitTran()


        MsgBox(PONo, MsgBoxStyle.Information, "Pastel Evolution")


    End Sub
    Private Sub processRTN(ByVal IBTNo As Long, Optional ByRef bProcess As Boolean = False, Optional ByRef imyIBTno As Integer = 0, Optional ByRef sError As String = "")
        UG.PerformAction(UltraGridAction.CommitRow)

        If cmbCustomer.Text = "" Then
            MsgBox("Customer Name can not be left blank", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If

        If UG.Rows.Count = 0 Then
            MsgBox("Enter Item Details", MsgBoxStyle.Exclamation, "Pastel Evolution")
            Exit Sub
        End If


        Dim iToWH As Integer = 0

        getDefaultwh(sError, iToWH)

        If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
        If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()

        Dim iInvoiceID As Integer = 0
        Dim sOrderNumber As String = ""
        Dim iCustomer As Integer = 0
        Dim iSupplier As Integer = 0

        If CInt(IBTNo) = 0 Then
            iOldCRNNo = 0
        Else
            iOldCRNNo = IBTNo
        End If

        DatabaseContext.CreateCommonDBConnection("UMdbSERVER\SQLExpress", sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
        DatabaseContext.SetLicense("DE09110022", "1428511")
        DatabaseContext.CreateConnection(sSQLSrvName, sSQLSrvDataBase, sSQLSrvUserName, sSQLSrvPassword, False)

        Try
            DatabaseContext.BeginTran()

            Dim ReturnSupplier As ReturnToSupplier

            ReturnSupplier = New ReturnToSupplier()


            Dim RTNDetail As OrderDetail

            Dim InventoryItem As InventoryItem
            Dim WH As Warehouse

            Dim Supplier As Supplier
            If cmbCustomer.Text.Length > 4 Then
                Supplier = New Supplier(CInt(cmbCustomer.Value))
            Else
                Supplier = New Supplier(CInt(cmbCustomer.Text))
            End If



            iSupplier = Supplier.ID

            'Dim Representative As New SalesRepresentative(CInt(cmbSaleRep.Value))

            'SalesOrder.OrderNo = CStr(txtOrdNo.Text.Trim)
            'SalesOrder.DeliveryNote = CStr(txtDelNote.Text.Trim)
            ReturnSupplier.Account = Supplier
            ReturnSupplier.InvoiceTo = Supplier.PostalAddress
            ReturnSupplier.DeliverTo = Supplier.PhysicalAddress
            ReturnSupplier.ExternalOrderNo = txtExtOrder.Text.ToString
            'SalesOrder.ExternalOrderNo = PONo
            ReturnSupplier.InvoiceDate = Format(dtpInDate.Value, "dd/MM/yyyy")
            ReturnSupplier.DeliveryDate = Format(dtpDeliDate.Value, "dd/MM/yyyy")
            ReturnSupplier.OrderDate = Format(dtpOrdDate.Value, "dd/MM/yyyy")

            If bIsInclusive = True Then
                ReturnSupplier.TaxMode = TaxMode.Inclusive
            ElseIf bIsInclusive = False Then
                ReturnSupplier.TaxMode = TaxMode.Exclusive
            End If

            'ReturnSupplier.UserFields("uiIDSOrdSuppID") = CInt(4)
            'ReturnSupplier.UserFields("ucIDSOrddbName") = cmbGRVLoc.Text.Trim

            Dim bLoop As Boolean = False

            Try

lbl1:           For Each RTNDetail In ReturnSupplier.Detail
                    bLoop = False

                    ReturnSupplier.Detail.Remove(RTNDetail)
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



                If CDbl(ugR.Cells("Quantity").Value) > 0 Then 'process only items that have quenty
                    RTNDetail = New OrderDetail

                    InventoryItem = New InventoryItem(CInt(ugR.Cells("StockID").Value))
                    RTNDetail.InventoryItem = InventoryItem
                    RTNDetail.Quantity = CDbl(ugR.Cells("Quantity").Value)
                    RTNDetail.ToProcess = CDbl(ugR.Cells("ConfirmQty").Value)
                    If bIsInclusive = False Then
                        RTNDetail.TaxMode = TaxMode.Exclusive
                        RTNDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Excl").Value)
                        RTNDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    ElseIf bIsInclusive = True Then
                        RTNDetail.TaxMode = TaxMode.Inclusive
                        RTNDetail.UnitSellingPrice = CDbl(ugR.Cells("Price_Incl").Value)
                        RTNDetail.TaxType = New TaxRate(CInt(ugR.Cells("TaxType").Value))
                    End If


                    Dim LotN As New Lot()
                    LotN.Code = ugR.Cells("Lot").Text
                    'LotN.ExpiryDate = ugR.Cells("Lot_Exp").Value
                    RTNDetail.Lot = LotN

                    If ugR.Cells("IsWH").Value = True Then
                        WH = New Warehouse(CInt(ugR.Cells("Warehouse").Value))
                        RTNDetail.Warehouse = WH
                    End If

                    RTNDetail.Description = CStr(ugR.Cells("Description_1").Value)
                    RTNDetail.DiscountPercent = CDbl(ugR.Cells("Discount").Value)
                    'RTNDetail.LotID = CStr(ugR.Cells("Lot").Value)
                    ReturnSupplier.Detail.Add(RTNDetail)


                End If
            Next

            ReturnSupplier.Save()

            If bProcess Then
                ReturnSupplier.Process()
                txtOrdNo.Text = ReturnSupplier.Reference
            End If

abc:        DatabaseContext.CommitTran()

            iCRNNo = ReturnSupplier.ID
            sSONo = ReturnSupplier.Reference

            imyIBTno = ReturnSupplier.ID

            'Call MemorizeStock()

            If bProcess Then
                MsgBox("Invoice No = " & ReturnSupplier.Reference, MsgBoxStyle.Information, "Pastel Evolution")
            Else
                MsgBox("Sales Order No = " & ReturnSupplier.OrderNo, MsgBoxStyle.Information, "Pastel Evolution")
            End If

            If bProcess Then
                Dim iSalesOrder As Integer = iCRNNo
                ''MsgBox(SalesOrder.Reference, MsgBoxStyle.Information, "Pastel Evolution")
                ''DatabaseContext.CommitTran()
                'If MsgBox("Do you want to print now ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Pastel Evolution") = MsgBoxResult.Yes Then
                '    Dim objRep As New ReportDocument
                '    Dim s As String = Application.StartupPath

                '    'If iDocType_1 = 10 Then
                '    objRep.Load(Application.StartupPath & "\IBT.rpt")
                '    'ElseIf iDocType_1 = 8 Then 
                '    '    objRep.Load(Application.StartupPath & "\IBT_R.rpt")
                '    'End If



                '    Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database= '" & cmbGRVLoc.Text & "' ")
                '    SQL = " SELECT OrderNum, DocState  from InvNum where ExtOrderNum = '" & SalesOrder.Reference & "' "
                '    CMD = New SqlCommand(SQL, Con2)
                '    CMD.CommandType = CommandType.Text
                '    DA = New SqlDataAdapter(CMD)
                '    DS1 = New DataSet
                '    Con2.Open()
                '    DA.Fill(DS1)
                '    Con2.Close()

                '    If DS1.Tables(0).Rows.Count > 0 Then
                '        frmPrintPreview.vInvNo = DS1.Tables(0).Rows(0)(0)
                '        If DS1.Tables(0).Rows(0)(1) = 4 Then
                '            frmPrintPreview.vPState = "Processed"
                '        Else
                '            frmPrintPreview.vPState = "Unprocessed"
                '        End If

                '    Else
                '        frmPrintPreview.vInvNo = " "
                '        frmPrintPreview.vPState = " "
                '    End If

                '    frmPrintPreview.vSelectionFormula = "{InvNum.AutoIndex}= " & SalesOrder.ID & ""

                '    iRepoID = 2

                '    frmPrintPreview.ShowDialog()
                '    iRepoID = 0

                '    Exit Sub


                '    'objRep.RecordSelectionFormula = "{InvNum.AutoIndex}=" & iSalesOrder & ""

                '    'ApplyLoginToTable(objRep)

                '    'objRep.SetDatabaseLogon(sSQLSrvUserName, sSQLSrvPassword, sSQLSrvReportSrv, sSQLSrvDataBase)

                'End If
            End If

            If Not bProcess Then
                Discard()
            End If


        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Critical, "Pastel Evolution")
            sError = ex.Message
            imyIBTno = 0
            Exit Sub
            'GoTo abc

        Finally
            PONo = ""
            If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
            If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
            If Con2.State = ConnectionState.Open Then Con2.Close()

        End Try
    End Sub

    Sub getDefaultwh(ByRef sError As String, ByRef iToWH1 As Integer)

        Con2 = New SqlConnection("Server=" & sSQLSrvName & ";User ID=" & sSQLSrvUserName & ";Password=" & sSQLSrvPassword & ";Database =" & sSQLSrvDataBase & "")

        Dim objSQL As New clsSqlConn
        Dim dr1 As DataRow
        For Each dr In DS.Tables(0).Rows
            Try

                'DatabaseContext.CreateCommonDBConnection(sSQLSrvName, sSQLSrvCommonDB, sSQLSrvUserName, sSQLSrvPassword, False)
                'DatabaseContext.SetLicense("DE09110022", "1428511")
                'DatabaseContext.CreateConnection(sSQLSrvName, "dbUdawatta_new", sSQLSrvUserName, sSQLSrvPassword, False)

                ' this is connection to PO Database open is correct

                Con2.Open()

                CMD = New SqlCommand(SQL, Con2)
                CMD.CommandType = CommandType.Text
                CMD.ExecuteNonQuery()

                SQL = " SELECT WhseLink FROM WhseMst WHERE DefaultWhse = 1"

                CMD = New SqlCommand(SQL, Con2)
                CMD.CommandType = CommandType.Text
                Dim supDS As New DataSet
                Dim supDA As New SqlDataAdapter(CMD)

                supDA.Fill(supDS)

                For Each dr1 In supDS.Tables(0).Rows

                    iToWH1 = dr1.Item("WhseLink")

                Next

                sError = ""

            Catch ex As Exception
                MsgBox(ex.Message)
                iToWH1 = 0
                sError = ex.Message
                Exit Sub
            Finally
                If DatabaseContext.IsCommonConnectionOpen = True Then DatabaseContext.CommonDBConnection.Close()
                If DatabaseContext.IsConnectionOpen = True Then DatabaseContext.DBConnection.Close()
                If Con2.State = ConnectionState.Open Then Con2.Close()
                dr1 = Nothing
            End Try
        Next
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
    Private Function Get_Value(ByVal SQL As String) As Decimal
        Try
            Dim oQty As Object
            Con1.ConnectionString = sConStr
            CMD = New SqlCommand(SQL, Con1)
            CMD.CommandType = CommandType.Text
            Con1.Open()
            oQty = CMD.ExecuteScalar
            Con1.Close()
            Return CDec(oQty)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Sub UG_AfterCellUpdate(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UG.AfterCellUpdate
        If IsNew = True Then
            If e.Cell.Column.Key = "StockID" Then
                'If iDocType = DocType.SalesOrder Then
                '    SQL = "SELECT     fExclPrice, fInclPrice FROM _etblPriceListPrices WHERE iPriceListNameID =" & iCusPriceList & " AND iStockID =" & e.Cell.Value & ""
                '    Dim objSQL As New clsSqlConn
                '    With objSQL
                '        DS = New DataSet
                '        DS = .Get_Data_Sql(SQL)
                '        If DS.Tables(0).Rows.Count > 0 Then
                '            Dim Dr As DataRow
                '            For Each Dr In DS.Tables(0).Rows
                '                e.Cell.Row.Cells("Price_Excl").Value = Dr("fExclPrice")
                '                e.Cell.Row.Cells("Price_Incl").Value = Dr("fInclPrice")
                '            Next
                '        Else
                '            e.Cell.Row.Cells("Price_Excl").Value = 0
                '            e.Cell.Row.Cells("Price_Incl").Value = 0
                '        End If
                '    End With
                'End If
                e.Cell.Row.Cells("AvailableQty").Value = Get_Available_Qty("SELECT WHQtyOnHand FROM WhseStk WHERE WHStockLink =" & e.Cell.Value & " AND WHWhseID =" & e.Cell.Row.Cells("Warehouse").Value & "")

                'OEPrice = UG.ActiveRow.Cells("Price_Excl").Value
                'OIPrice = UG.ActiveRow.Cells("Price_Incl").Value
            ElseIf e.Cell.Column.Key = "Code" Then
                'bValChange = False

                SQL = " SELECT StkItem.WhseItem, StkItem.iUOMStockingUnitID, StkItem.StockLink, StkItem.Code, " & _
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
                    '.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    '.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    '.Cells("OrderTax").Value = LineTax(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    '.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    '.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    '.Cells("LineTax").Value = LineTax(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    '.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    '.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
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
                    'e.Cell.Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("OrderTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("LineTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                End With
            ElseIf e.Cell.Column.Key = "Price_Incl" Then
                With e.Cell.Row
                    '.Value = .Text
                    If iDocType = DocType.SalesOrder Then
                        e.Cell.Row.Cells("ConfirmQty").Value = e.Cell.Row.Cells("Quantity").Value
                    End If
                    'e.Cell.Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("OrderTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("LineTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                End With
            ElseIf e.Cell.Column.Key = "StockID" Then
                With e.Cell.Row


                    'If iDocType = DocType.SalesOrder Then
                    '    SQL = "SELECT     fExclPrice, fInclPrice FROM _etblPriceListPrices WHERE iPriceListNameID =" & iCusPriceList & " AND iStockID =" & e.Cell.Value & ""
                    '    Dim objSQL As New clsSqlConn
                    '    With objSQL
                    '        DS = New DataSet
                    '        DS = .Get_Data_Sql(SQL)
                    '        If DS.Tables(0).Rows.Count > 0 Then
                    '            Dim Dr As DataRow
                    '            For Each Dr In DS.Tables(0).Rows
                    '                e.Cell.Row.Cells("Price_Excl").Value = Dr("fExclPrice")
                    '                e.Cell.Row.Cells("Price_Incl").Value = Dr("fInclPrice")
                    '            Next
                    '        Else
                    '            e.Cell.Row.Cells("Price_Excl").Value = 0
                    '            e.Cell.Row.Cells("Price_Incl").Value = 0
                    '        End If
                    '    End With
                    'End If



                    '.Value = .Text
                    If iDocType = DocType.SalesOrder Then
                        e.Cell.Row.Cells("ConfirmQty").Value = e.Cell.Row.Cells("Quantity").Value
                    End If
                    'e.Cell.Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("OrderTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("LineTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)

                    e.Cell.Row.Cells("AvailableQty").Value = Get_Available_Qty("SELECT WHQtyOnHand FROM WhseStk WHERE WHStockLink =" & e.Cell.Value & " AND WHWhseID =" & e.Cell.Row.Cells("Warehouse").Value & "")

                End With
            ElseIf e.Cell.Column.Key = "LineTax" Then
                With e.Cell.Row
                    '.Value = .Text
                    If iDocType = DocType.SalesOrder Then
                        e.Cell.Row.Cells("ConfirmQty").Value = e.Cell.Row.Cells("Quantity").Value
                    End If
                    'e.Cell.Row.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("OrderTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("Quantity").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("LineTax").Value = LineTax(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                    'e.Cell.Row.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, e.Cell.Row.Cells("ConfirmQty").Value, e.Cell.Row.Cells("Discount").Value, e.Cell.Row.Cells("Price_Excl").Value, e.Cell.Row.Cells("Price_Incl").Value, e.Cell.Row.Cells("TaxRate").Value)
                End With

            ElseIf e.Cell.Column.Key = "Quantity" Or e.Cell.Column.Key = "Price_Excl" Or e.Cell.Column.Key = "Price_Incl" Or _
            e.Cell.Column.Key = "TaxRate" Or e.Cell.Column.Key = "Discount" Then
                With e.Cell.Row
                    If iDocType = DocType.SalesOrder Then
                        .Cells("ConfirmQty").Value = .Cells("Quantity").Value
                    End If
                    '.Cells("OrderTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    '.Cells("OrderTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    '.Cells("OrderTax").Value = LineTax(bIsInclusive, .Cells("Quantity").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    '.Cells("LineTotal_Excl").Value = LineTotal_Excl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    '.Cells("LineTotal_Incl").Value = LineTotal_Incl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    '.Cells("LineTax").Value = LineTax(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    '.Cells("Line_Dis_Excl").Value = LineDis_Excl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                    '.Cells("Line_Dis_Incl").Value = LineDis_Incl(bIsInclusive, .Cells("ConfirmQty").Value, .Cells("Discount").Value, .Cells("Price_Excl").Value, .Cells("Price_Incl").Value, .Cells("TaxRate").Value)
                End With

            End If

        End If
    End Sub

    Private Sub UG_AfterExitEditMode(sender As Object, e As EventArgs) Handles UG.AfterExitEditMode
        If UG.ActiveCell.Column.Key = "Quantity" Then

            UG.ActiveRow.Cells("ConfirmQty").Value = UG.ActiveRow.Cells("Quantity").Value

        End If
    End Sub

    Private Sub UG_CellChange(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UG.CellChange
        Try
            If UG.ActiveCell.Column.Index = 1 Then  'Code
                If DDStock.ActiveRow.Activated = True Then
                    UG.ActiveRow.Cells("StockID").Value = DDStock.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Code").Value = DDStock.ActiveRow.Cells("StockLink").Value
                    UG.ActiveRow.Cells("Description_1").Value = DDStock.ActiveRow.Cells("Description_1").Value
                    UG.ActiveRow.Cells("Description_2").Value = DDStock.ActiveRow.Cells("Description_2").Value
                    UG.ActiveRow.Cells("iUnit").Value = DDStock.ActiveRow.Cells("iUOMStockingUnitID").Value

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
            End If
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Private Sub UG_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UG.InitializeLayout

    End Sub
End Class